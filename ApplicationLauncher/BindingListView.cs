#region Copyright (C) 2009 Nate

/* 
 *	Copyright (C) 2009 Nate
 *	http://nate.dynalias.net
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace ApplicationLauncher
{
    class BindingListView : ListView
    {
        public interface IFilter
        {
            bool Match(string filter);
        }

        private IList _dataSource;
        private List<string> _bindings;
        private List<int> _columnSize;

        private BindingListViewColumnSorter _columnSorter;

        private string _filter;

        int _dragToPosition;
        bool _allowReorder;
        bool _allowColumnSort;

        public IList DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                Populate();
            }
        }

        public bool AllowReorder
        {
            get { return _allowReorder; }
            set { _allowReorder = value; }
        }

        public bool AllowColumnSort
        {
            get { return _allowColumnSort; }
            set { _allowColumnSort = value; }
        }

        public object SelectedItem
        {
            get
            {
                if (base.SelectedItems.Count > 0)
                    return base.SelectedItems[0].Tag;
                return null;
            }
            set
            {
                if (value == null)
                {
                    base.SelectedIndices.Clear();
                    return;
                }

                foreach (ListViewItem item in base.Items)
                {
                    if (item.Tag == value)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (base.SelectedItems.Count == 1)
                    return base.SelectedItems[0].Index;
                else
                    return -1;
            }
            set
            {
                foreach (ListViewItem item in base.Items)
                {
                    item.Selected = (item.Index == value);
                }
            }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    if ((_filter == "") || (value.Contains(_filter)))
                    {
                        _filter = value;
                        FilterDown();
                    }
                    else
                    {
                        _filter = value;
                        Populate();
                    }
                }
            }
        }

        public BindingListView()
        {
            _bindings = new List<string>();
            _columnSize = new List<int>();
            _columnSorter = new BindingListViewColumnSorter(_bindings);
            _filter = "";
            _allowReorder = false;
            _allowColumnSort = false;
            _dragToPosition = -1;

            base.View = View.Details;
            base.FullRowSelect = true;
            base.HideSelection = false;
            base.MultiSelect = false;
            this.ListViewItemSorter = _columnSorter;
            this.AllowDrop = true;
            this.OwnerDraw = true;
            this.ResizeRedraw = true;
            this.ColumnClick += new ColumnClickEventHandler(BindingListView_ColumnClick);
        }

        #region Events
        public delegate void BindingListViewEvent(object sender, EventArgs e);

        public event BindingListViewEvent ItemsReordered;

        [System.ComponentModel.Category("CatBehavior")]
        protected void OnItemsReordered()
        {
            if (ItemsReordered != null)
                ItemsReordered(this, new EventArgs());
        }
        #endregion
        
        void BindingListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!_allowColumnSort)
                return;

            if (e.Column == _columnSorter.SortColumn)
            {
                if (_columnSorter.Order == SortOrder.Ascending)
                    _columnSorter.Order = SortOrder.Descending;
                else
                    _columnSorter.Order = SortOrder.Ascending;
            }
            else
            {
                _columnSorter.SortColumn = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }

            base.Sort();
        }

        public void AddColumn(string heading, int width, string propertyName)
        {
            _bindings.Add(propertyName);

            _columnSize.Add(width);
            base.Columns.Add(heading, width);

            ResizeLastColumnHeader();
        }

        private void Populate()
        {
            if (_dataSource == null)
                return;

            ResizeLastColumnHeader();

            object currentlySelectedItem = null;
            if (base.SelectedItems.Count > 0)
                currentlySelectedItem = base.SelectedItems[0].Tag;

            this.ListViewItemSorter = null;
            this.BeginUpdate();

            base.Items.Clear();
            foreach (object obj in _dataSource)
            {
                if (obj == null)
                    continue;

                if ((_filter != null) && (_filter != ""))
                {
                    IFilter filterObj = obj as IFilter;
                    if (filterObj != null)
                    {
                        if (filterObj.Match(_filter) == false)
                            continue;
                    }
                }

                string[] items = new string[_bindings.Count];

                for (int bindingIndex = 0; bindingIndex < _bindings.Count; bindingIndex++)
                {
                    object data = null;

                    FieldInfo fieldInfo = obj.GetType().GetField(_bindings[bindingIndex]);
                    PropertyInfo propInfo = obj.GetType().GetProperty(_bindings[bindingIndex]);
                    if (fieldInfo != null)
                    {
                        data = fieldInfo.GetValue(obj);
                    }
                    else if (propInfo != null)
                    {
                        MethodInfo methodInfo = propInfo.GetGetMethod();
                        if (methodInfo != null)
                            data = methodInfo.Invoke(obj, new object[] { });
                    }

                    if (data == null)
                        items[bindingIndex] = "null";
                    else if (data.GetType() == typeof(DateTime))
                        items[bindingIndex] = ((DateTime)data).ToString("dd/MM/yyyy");
                    else
                        items[bindingIndex] = data.ToString();
                }
                ListViewItem newItem = new ListViewItem(items);
                newItem.Tag = obj;
                base.Items.Add(newItem);
            }

            this.ListViewItemSorter = _columnSorter;
            this.Sort();
            this.EndUpdate();

            ReselectObject(currentlySelectedItem);
        }

        private void FilterDown()
        {
            if (_dataSource == null)
                return;

            object currentlySelectedItem = null;
            if (base.SelectedItems.Count > 0)
                currentlySelectedItem = base.SelectedItems[0].Tag;

            this.BeginUpdate();

            int index = 0;
            while (index < base.Items.Count)
            {
                object obj = base.Items[index].Tag;

                IFilter filterObj = obj as IFilter;
                if (filterObj != null)
                {
                    if (filterObj.Match(_filter) == false)
                    {
                        base.Items.RemoveAt(index);
                        continue;
                    }
                }

                index++;
            }

            this.EndUpdate();

            ReselectObject(currentlySelectedItem);
        }

        private void ReselectObject(object selectedObject)
        {
            if (selectedObject == null)
                return;

            if (selectedObject != null)
            {
                bool bFound = false;
                foreach (ListViewItem item in base.Items)
                {
                    if (item.Tag == selectedObject)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        bFound = true;
                    }
                }
                if (!bFound)
                    base.OnSelectedIndexChanged(new EventArgs());
            }
        }

        protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            if (e.ColumnIndex != Columns.Count - 1)
            {
                ResizeLastColumnHeader();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ResizeLastColumnHeader();
        }

        protected void ResizeLastColumnHeader()
        {
            int x = 0;
            for (int i = 0; i < base.Columns.Count; i++)
            {
                if (i == base.Columns.Count - 1)
                {
                    int newWidth = this.ClientRectangle.Width - x;
                    if (newWidth < _columnSize[i])
                        newWidth = _columnSize[i];
                    if (base.Columns[i].Width != newWidth)
                        base.Columns[i].Width = newWidth;
                }
                x += base.Columns[i].Width;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (!_allowReorder)
                return;

            DragDropEffects effects = base.DoDragDrop("Reorder", DragDropEffects.Move);
            if (effects != DragDropEffects.Move)
                return;
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            if (!_allowReorder)
                return;

            drgevent.Effect = DragDropEffects.None;
            if (drgevent.Data.GetDataPresent(DataFormats.Text) == false)
                return;

            string text = (string)drgevent.Data.GetData(typeof(string));
            if (text == "Reorder")
                drgevent.Effect = DragDropEffects.Move;
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            if (!_allowReorder)
                return;

            drgevent.Effect = DragDropEffects.None;
            if (drgevent.Data.GetDataPresent(DataFormats.Text) == false)
                return;

            string text = (string)drgevent.Data.GetData(typeof(string));
            if (text != "Reorder")
                return;

            Point cp = base.PointToClient(new Point(drgevent.X, drgevent.Y));
            ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
            if (hoverItem == null)
                return;

            hoverItem.EnsureVisible();

            int dragToPosition = hoverItem.Index;
            ListViewItem nextItem = base.GetItemAt(cp.X, cp.Y + (this.Font.Height / 2));
            if (hoverItem != nextItem)
                dragToPosition++;

            if ((dragToPosition == this.SelectedIndex) || (dragToPosition == this.SelectedIndex + 1))
                return;

            if (_dragToPosition != dragToPosition)
            {
                _dragToPosition = dragToPosition;
                Invalidate();
                System.Diagnostics.Debug.WriteLine("Position = " + _dragToPosition);
            }
            drgevent.Effect = DragDropEffects.Move;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (!_allowReorder)
                return;

            if (base.SelectedItems.Count == 0)
                return;

            int dropIndex = _dragToPosition;
            if (dropIndex > this.SelectedIndex)
                dropIndex--;

            _dragToPosition = -1;

            ListViewItem prevItem = this.SelectedItems[0];

            base.Items.Remove(prevItem);
            base.Items.Insert(dropIndex, prevItem);

            OnItemsReordered();
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
            return;

            Color textColor;
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, 36, 106)), e.Bounds);
                textColor = Color.White;
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
                textColor = this.ForeColor;
            }

            StringFormat stringFormat = new StringFormat(StringFormatFlags.NoWrap);
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            stringFormat.Alignment = StringAlignment.Near;

            Rectangle rect = e.Bounds;
            foreach (ColumnHeader ch in this.Columns)
            {
                int index = ch.Index;
                rect.Width = ch.Width;
                e.Graphics.DrawString(e.Item.SubItems[index].Text, this.Font, new SolidBrush(textColor), rect, stringFormat);
                rect.X += rect.Width;
            }

            if (_dragToPosition >= 0)
            {
                if (_dragToPosition == e.Item.Index)
                {
                    e.Graphics.DrawLine(Pens.Black, e.Bounds.X + 5, e.Bounds.Y, e.Bounds.Right - 10, e.Bounds.Y);
                }
                if (_dragToPosition == e.Item.Index + 1)
                {
                    e.Graphics.DrawLine(Pens.Black, e.Bounds.X + 5, e.Bounds.Bottom - 1, e.Bounds.Right - 10, e.Bounds.Bottom - 1);
                }
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
            return;
        }

        public class BindingListViewColumnSorter : IComparer
        {
            private int _columnToSort;
            private SortOrder _orderOfSort;
            private CaseInsensitiveComparer _objectCompare;
            private List<string> _bindings;
            private MethodInfo _columnPropertyMethod;
            private Type _columnType;

            public int SortColumn
            {
                set
                {
                    _columnToSort = value;
                    _columnPropertyMethod = null;
                }
                get { return _columnToSort; }
            }

            public SortOrder Order
            {
                set { _orderOfSort = value; }
                get { return _orderOfSort; }
            }

            public BindingListViewColumnSorter(List<string> bindings)
            {
                _bindings = bindings;
                _columnToSort = 0;
                _orderOfSort = SortOrder.None;
                _objectCompare = new CaseInsensitiveComparer();
            }

            private object GetData(ListViewItem item)
            {
                if (item.Tag == null)
                    return null;

                if (_columnPropertyMethod == null)
                    return null;

                return _columnPropertyMethod.Invoke(item.Tag, new object[] { });
            }

            public int Compare(object x, object y)
            {
                if (_orderOfSort == SortOrder.None)
                    return 0;

                ListViewItem listviewX = (ListViewItem)x;
                ListViewItem listviewY = (ListViewItem)y;

                if (_columnPropertyMethod == null)
                {
                    PropertyInfo propInfo = listviewX.Tag.GetType().GetProperty(_bindings[_columnToSort]);
                    if (propInfo != null)
                    {
                        _columnPropertyMethod = propInfo.GetGetMethod();

                        _columnType = null;
                        if (_columnPropertyMethod != null)
                            _columnType = _columnPropertyMethod.ReturnType;
                    }
                }

                int compareResult;
                if (_columnType == typeof(DateTime))
                {
                    object dataX = GetData(listviewX);
                    object dataY = GetData(listviewY);
                    compareResult = ((DateTime)dataX).CompareTo(dataY);
                }
                else
                {
                    compareResult = _objectCompare.Compare(listviewX.SubItems[_columnToSort].Text, listviewY.SubItems[_columnToSort].Text);
                }

                if (_orderOfSort == SortOrder.Ascending)
                    return compareResult;
                else if (_orderOfSort == SortOrder.Descending)
                    return (-compareResult);
                else
                    return 0;
            }

        }

    }
}
