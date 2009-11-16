using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CSharpControls
{
	/// <summary>
	/// A replacement for the Windows Button Control.
	/// </summary>
	[DefaultEvent("Click")]
	public class VistaButton : System.Windows.Forms.UserControl, System.Windows.Forms.IButtonControl
	{

		#region -  Designer  -

			private System.ComponentModel.Container components = null;

			/// <summary>
			/// Initialize the component with it's
			/// default settings.
			/// </summary>
			public VistaButton()
			{
				InitializeComponent();

				this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				this.SetStyle(ControlStyles.DoubleBuffer, true);
				this.SetStyle(ControlStyles.ResizeRedraw, true);
				this.SetStyle(ControlStyles.Selectable, true);
				this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
				this.SetStyle(ControlStyles.UserPaint, true);
				this.BackColor = Color.Transparent;
				mGlowFadeIn.Interval = 40;
				mGlowFadeOut.Interval = 40;
                mFocusFadeIn.Interval = 40;
                mFocusFadeOut.Interval = 40;
			}

			/// <summary>
			/// Release resources used by the control.
			/// </summary>
			protected override void Dispose( bool disposing )
			{
				if( disposing )
				{
					if(components != null)
					{
						components.Dispose();
					}
				}
				base.Dispose( disposing );
			}

			#region -  Component Designer generated code  -

				private void InitializeComponent()
				{
					// 
					// VistaButton
					// 
					this.Name = "VistaButton";
					this.Size = new System.Drawing.Size(100, 32);
					this.Paint += new System.Windows.Forms.PaintEventHandler(this.VistaButton_Paint);
					this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VistaButton_KeyUp);
					this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VistaButton_KeyDown);
					this.MouseEnter += new System.EventHandler(this.VistaButton_MouseEnter);
					this.MouseLeave += new System.EventHandler(this.VistaButton_MouseLeave);
					this.MouseUp +=new MouseEventHandler(VistaButton_MouseUp);
					this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VistaButton_MouseDown);
					//this.GotFocus +=new EventHandler(VistaButton_MouseEnter);
					this.GotFocus +=new EventHandler(VistaButton_GotFocus);
					//this.LostFocus +=new EventHandler(VistaButton_MouseLeave);
					this.LostFocus += new EventHandler(VistaButton_LostFocus);
					this.mGlowFadeIn.Tick += new EventHandler(mGlowFadeIn_Tick);
					this.mGlowFadeOut.Tick += new EventHandler(mGlowFadeOut_Tick);
                    this.mFocusFadeIn.Tick += new EventHandler(mFocusFadeIn_Tick);
                    this.mFocusFadeOut.Tick += new EventHandler(mFocusFadeOut_Tick);
                    this.Resize += new EventHandler(VistaButton_Resize);
				}

			#endregion

		#endregion
		
		#region -  Enums  -
		
			/// <summary>
			/// A private enumeration that determines 
			/// the mouse state in relation to the 
			/// current instance of the control.
			/// </summary>
			enum State {None, Hover, Pressed};

			/// <summary>
			/// A public enumeration that determines whether
			/// the button background is painted when the 
			/// mouse is not inside the ClientArea.
			/// </summary>
			public enum Style
			{
				/// <summary>
				/// Draw the button as normal
				/// </summary>
				Default, 
				/// <summary>
				/// Only draw the background on mouse over.
				/// </summary>
				Flat
			};

            enum RectCorners
            {
                None = 0,
                TopLeft = 1,
                TopRight = 2,
                BottomLeft = 4,
                BottomRight = 8,
                Top = TopLeft | TopRight,
                Bottom = BottomLeft | BottomRight,
                Left = TopLeft | BottomLeft,
                Right = TopRight | BottomRight,
                All = Left | Right
            }

		#endregion

		#region -  Properties  -

			#region -  Private Variables  -
        
				private bool calledbykey = false;
				private State mButtonState = State.None;
				private bool _defaultButton = false;
				private Timer mGlowFadeIn = new Timer();
				private Timer mGlowFadeOut = new Timer();
				private int mGlowAlpha = 0;
                private const int GlowFadeSpeed = 60;
                private Timer mFocusFadeIn = new Timer();
                private Timer mFocusFadeOut = new Timer();
				private int mFocusAlpha = 0;
                private const int FocusFadeSpeed = 100;
                private string _keyText = "";

			#endregion

			#region -  Text  -

				//private string mText;
				/// <summary>
				/// The text that is displayed on the button.
				/// </summary>
				[Category("Text"),
				 Description("The text that is displayed on the button.")]
				public string ButtonText
				{
					get { return this.Text; }
					set { this.Text = value; this.InvalidateEx(); }
				}

                [Category("Text"),
                 Description("The text that is displayed on the right hand side of the button")]
                public string KeyText
                {
                    get { return this._keyText; }
                    set { this._keyText = value; this.InvalidateEx(); }
                }

				private Color mForeColor = Color.White;
				/// <summary>
				/// The color with which the text is drawn.
				/// </summary>
				[Category("Text"), 
				 Browsable(true),
				 DefaultValue(typeof(Color),"White"),
				 Description("The color with which the text is drawn.")]
				public override Color ForeColor
				{
					get { return mForeColor; }
					set { mForeColor = value; this.InvalidateEx(); }
				}

				private ContentAlignment mTextAlign = ContentAlignment.MiddleCenter;
				/// <summary>
				/// The alignment of the button text
				/// that is displayed on the control.
				/// </summary>
				[Category("Text"), 
				 DefaultValue(typeof(ContentAlignment),"MiddleCenter"),
				 Description("The alignment of the button text " + 
							 "that is displayed on the control.")]
				public ContentAlignment TextAlign
				{
					get { return mTextAlign; }
					set { mTextAlign = value; this.InvalidateEx(); }
				}
	
			#endregion

			#region -  Image  -

				private Image mImage;
				/// <summary>
				/// The image displayed on the button that 
				/// is used to help the user identify
				/// it's function if the text is ambiguous.
				/// </summary>
				[Category("Image"), 
				 DefaultValue(null),
				 Description("The image displayed on the button that " +
							 "is used to help the user identify" + 
							 "it's function if the text is ambiguous.")]
				public Image Image
				{
					get { return mImage; }
					set { mImage = value; this.InvalidateEx(); }
				}

				private ContentAlignment mImageAlign = ContentAlignment.MiddleLeft;
				/// <summary>
				/// The alignment of the image 
				/// in relation to the button.
				/// </summary>
				[Category("Image"), 
				 DefaultValue(typeof(ContentAlignment),"MiddleLeft"),
				 Description("The alignment of the image " + 
							 "in relation to the button.")]
				public ContentAlignment ImageAlign
				{
					get { return mImageAlign; }
					set { mImageAlign = value; this.InvalidateEx(); }
				}

				private Size mImageSize = new Size(24,24);
				/// <summary>
				/// The size of the image to be displayed on the
				/// button. This property defaults to 24x24.
				/// </summary>
				[Category("Image"), 
				 DefaultValue(typeof(Size),"24, 24"),
				 Description("The size of the image to be displayed on the" + 
							 "button. This property defaults to 24x24.")]
				public Size ImageSize
				{
					get { return mImageSize; }
					set { mImageSize = value; this.InvalidateEx(); }
				}
	
			#endregion

			#region -  Appearance  -
							
				private Style mButtonStyle = Style.Default;
				/// <summary>
				/// Sets whether the button background is drawn 
				/// while the mouse is outside of the client area.
				/// </summary>
				[Category("Appearance"), 
				 DefaultValue(typeof(Style),"Default"),
				 Description("Sets whether the button background is drawn " +
							 "while the mouse is outside of the client area.")]
				public Style ButtonStyle
				{
					get { return mButtonStyle; }
					set { mButtonStyle = value; this.InvalidateEx(); }
				}

				private int mCornerRadius = 6;
				/// <summary>
				/// The radius for the button corners. The 
				/// greater this value is, the more 'smooth' 
				/// the corners are. This property should
				///  not be greater than half of the 
				///  controls height.
				/// </summary>
				[Category("Appearance"), 
				 DefaultValue(6),
				 Description("The radius for the button corners. The " +
							 "greater this value is, the more 'smooth' " +
							 "the corners are. This property should " +
							 "not be greater than half of the " +
							 "controls height.")]
				public int CornerRadius
				{
					get { return mCornerRadius; }
					set { mCornerRadius = value; this.InvalidateEx(); }
				}

				private Color mHighlightColor = Color.White;
				/// <summary>
				/// The colour of the highlight on the top of the button.
				/// </summary>
				[Category("Appearance"), 
				 DefaultValue(typeof(Color), "White"),
				 Description("The colour of the highlight on the top of the button.")]
				public Color HighlightColor
				{
					get { return mHighlightColor; }
					set { mHighlightColor = value; this.InvalidateEx(); }
				}

				private Color mButtonColor = Color.Black;
				/// <summary>
				/// The bottom color of the button that 
				/// will be drawn over the base color.
				/// </summary>
				[Category("Appearance"), 
				 DefaultValue(typeof(Color), "Black"), 
				 Description("The bottom color of the button that " + 
							 "will be drawn over the base color.")]
				public Color ButtonColor
				{
					get { return mButtonColor; }
					set { mButtonColor = value; this.InvalidateEx(); }
				}

                private Color mFocusColor = Color.Transparent;
                [Category("Appearance"),
                 DefaultValue(typeof(Color), "Transparent"),
                 Description("The bottom color of the button that " +
                             "will be drawn over the base color when the button has the focus.")]
                public Color FocusColor
                {
                    get { return mFocusColor; }
                    set { mFocusColor = value; this.InvalidateEx(); }
                }

                private Color mFocusGlowColor = Color.Silver;
                /// <summary>
                /// The colour that the button glows when
                /// the mouse is inside the client area.
                /// </summary>
                [Category("Appearance"),
                 DefaultValue(typeof(Color), "Wheat"),
                 Description("The colour that the button glows when " +
                             "the mouse is inside the client area.")]
                public Color FocusGlowColor
                {
                    get { return mFocusGlowColor; }
                    set { mFocusGlowColor = value; this.InvalidateEx(); }
                }

				//private Color mGlowColor = Color.FromArgb(141,189,255);
				private Color mGlowColor = Color.Silver;
				/// <summary>
				/// The colour that the button glows when
				/// the mouse is inside the client area.
				/// </summary>
				[Category("Appearance"), 
				 DefaultValue(typeof(Color), "Silver"), 
				 Description("The colour that the button glows when " + 
							 "the mouse is inside the client area.")]
				public Color GlowColor
				{
					get { return mGlowColor; }
					set { mGlowColor = value; this.InvalidateEx(); }
				}

				private Image mBackImage;
				/// <summary>
				/// The background image for the button, 
				/// this image is drawn over the base 
				/// color of the button.
				/// </summary>
				[Category("Appearance"), 
				 DefaultValue(null), 
				 Description("The background image for the button, " + 
							 "this image is drawn over the base " + 
							 "color of the button.")]
				public Image BackImage
				{
					get { return mBackImage; }
					set { mBackImage = value; this.InvalidateEx(); }
				}

				private Color mBaseColor = Color.Black;
				/// <summary>
				/// The backing color that the rest of 
				/// the button is drawn. For a glassier 
				/// effect set this property to Transparent.
				/// </summary>
				[Category("Appearance"), 
				 DefaultValue(typeof(Color), "Black"), 
				 Description("The backing color that the rest of" + 
							 "the button is drawn. For a glassier " + 
							 "effect set this property to Transparent.")]
				public Color BaseColor
				{
					get { return mBaseColor; }
					set { mBaseColor = value; this.InvalidateEx(); }
				}

				private bool _allowDefaultButtonBorder = true;
				/// <summary>
				/// Specified if a dark border is drawn around the button
				/// when it is the default button on a form.
				/// </summary>
				public bool AllowDefaultButtonBorder
				{
					get { return _allowDefaultButtonBorder; }
					set { _allowDefaultButtonBorder = value; this.InvalidateEx(); }
				}

			#endregion

		#endregion

		#region -  Functions  -

			private GraphicsPath RoundRect(RectangleF rect, float rad, RectCorners corners)
			{
                float l = rect.Left, t = rect.Top, r = rect.Right - 1, b = rect.Bottom - 1;
				GraphicsPath rr = new GraphicsPath();
                rr.StartFigure();
                
                PointF ptTL1 = new PointF(l, t + rad);
                PointF ptTL2 = new PointF(l, t);
                PointF ptTL3 = new PointF(l + rad, t);

                PointF ptTR1 = new PointF(r - rad, t);
                PointF ptTR2 = new PointF(r, t);
                PointF ptTR3 = new PointF(r, t + rad);

                PointF ptBR1 = new PointF(r, b - rad);
                PointF ptBR2 = new PointF(r, b);
                PointF ptBR3 = new PointF(r - rad, b);

                PointF ptBL1 = new PointF(l + rad, b);
                PointF ptBL2 = new PointF(l, b);
                PointF ptBL3 = new PointF(l, b - rad);

                RectangleF rectTL = new RectangleF(l, t, rad * 2, rad * 2);
                RectangleF rectTR = rectTL;
                rectTR.X = r - rad * 2;
                RectangleF rectBR = rectTR;
                rectBR.Y = b - rad * 2;
                RectangleF rectBL = rectTL;
                rectBL.Y = b - rad * 2;

                // Top Left
                if ((corners & RectCorners.TopLeft) != 0)
                {
                    rr.AddArc(rectTL, 180f, 90f);
                }
                else
                {
                    rr.AddLines(new PointF[] { ptTL1, ptTL2, ptTL3 });
                }

                // Top
				rr.AddLine(ptTL3, ptTR1);

                // Top Right
                if ((corners & RectCorners.TopRight) != 0)
                {
                    rr.AddArc(rectTR, 270f, 90f);
                }
                else
                {
                    rr.AddLines(new PointF[] { ptTR1, ptTR2, ptTR3 });
                }

                // Right
                rr.AddLine(ptTR3, ptBR1);

                // Bottom Right
                if ((corners & RectCorners.BottomRight) != 0)
                {
                    rr.AddArc(rectBR, 0f, 90f);
                }
                else
                {
                    rr.AddLines(new PointF[] { ptBR1, ptBR2, ptBR3 });
                }

                // Bottom
                rr.AddLine(ptBR3, ptBL1);

                // Bottom Left
                if ((corners & RectCorners.BottomLeft) != 0)
                {
                    rr.AddArc(rectBL, 90f, 90f);
                }
                else
                {
                    rr.AddLines(new PointF[] { ptBL1, ptBL2, ptBL3 });
                }

                // Left
                rr.AddLine(ptBL3, ptTL1);

                rr.CloseFigure();
				return rr;
			}

			private StringFormat StringFormatAlignment(ContentAlignment textalign)
			{
				StringFormat sf = new StringFormat();
				switch (textalign)
				{
					case ContentAlignment.TopLeft:
					case ContentAlignment.TopCenter:
					case ContentAlignment.TopRight:
						sf.LineAlignment = StringAlignment.Near;
						break;
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.MiddleCenter:
					case ContentAlignment.MiddleRight:
						sf.LineAlignment = StringAlignment.Center;
						break;
					case ContentAlignment.BottomLeft:
					case ContentAlignment.BottomCenter:
					case ContentAlignment.BottomRight:
						sf.LineAlignment = StringAlignment.Far;
						break;
				}
				switch (textalign)
				{
					case ContentAlignment.TopLeft:
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.BottomLeft:
						sf.Alignment = StringAlignment.Near;
						break;
					case ContentAlignment.TopCenter:
					case ContentAlignment.MiddleCenter:
					case ContentAlignment.BottomCenter:
						sf.Alignment = StringAlignment.Center;
						break;
					case ContentAlignment.TopRight:
					case ContentAlignment.MiddleRight:
					case ContentAlignment.BottomRight:
						sf.Alignment = StringAlignment.Far;
						break;
				}
				return sf;
			}

		#endregion

		#region -  Drawing  -

            private void DrawPath(Graphics g, Brush brush, GraphicsPath rr)
            {
                using (Pen pen = new Pen(brush))
                {
                    g.DrawPath(pen, rr);
                }
            }

            private void DrawPath(Graphics g, Color color, GraphicsPath rr)
            {
                using (Pen pen = new Pen(color))
                {
                    g.DrawPath(pen, rr);
                }
            }

            private void FillPath(Graphics g, Color color, GraphicsPath rr)
            {
                using (Brush br = new SolidBrush(color))
                {
                    g.FillPath(br, rr);
                }
                DrawPath(g, color, rr);
            }

            private int AlphaMultiply(int alpha1, int alpha2)
            {
                if (alpha1 < 0)
                    alpha1 = 0;
                if (alpha2 < 0)
                    alpha2 = 0;
                if (alpha1 > 255)
                    alpha1 = 255;
                if (alpha2 > 255)
                    alpha2 = 255;
                return alpha1 * alpha2 / 255;
            }

			/// <summary>
			/// Draws the drop shadow border for the control
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawDropShadow(Graphics g)
			{
				if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None){return;}
				Rectangle r = this.ClientRectangle;

				r.Width -= 1; r.Height -= 1;
				r.Offset(1, 1);
				
				float radius = CornerRadius;
				using (GraphicsPath rr = RoundRect(r, radius, RectCorners.All))
				{
                    Color color = Color.FromArgb(255, 0, 0, 0);
                    FillPath(g, color, rr);
                }
			}

			/// <summary>
			/// Draws the outer border for the control
			/// using the ButtonColor property.
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawOuterStroke(Graphics g)
			{
				if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None){return;}
				Rectangle r = this.ClientRectangle;
				r.Width -= 2; r.Height -= 2;
                //r.Inflate(-2, -2);
                using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
				{
                    int alpha = AlphaMultiply(150, this.ButtonColor.A);
                    Color color = Color.FromArgb(alpha, this.ButtonColor);
                    DrawPath(g, color, rr);
				}
			}

			/// <summary>
			/// Draws the inner border for the control
			/// using the HighlightColor property.
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawInnerStroke(Graphics g)
			{
				if (_allowDefaultButtonBorder == false) return;
				if (_defaultButton == false) return;
				if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None){return;}
				Rectangle r = this.ClientRectangle;
				r.Width -= 2; r.Height -= 2;
				//r.Inflate(-1, -1);
                using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
				{
                    DrawPath(g, Color.FromArgb(96, Color.Black), rr);
				}
				r.Inflate(-1, -1);
                using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
				{
                    DrawPath(g, Color.FromArgb(64, Color.Black), rr);
				}
				r.Inflate(-1, -1);
                using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
				{
                    DrawPath(g, Color.FromArgb(32, Color.Black), rr);
				}
			}

			/// <summary>
			/// Draws the background for the control
			/// using the background image and the 
			/// BaseColor.
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawBackground(Graphics g)
			{
				if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None){return;}
				//int alpha = (mButtonState == State.Pressed) ? 204 : 127;
				Rectangle r = this.ClientRectangle;
                r.Width -= 1; r.Height -= 1;
                r.Inflate(-1, -1);
                using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
				{
                    FillPath(g, this.BaseColor, rr);

                    if (this.BackImage != null)
					{
						SetClip(g);
						g.DrawImage(this.BackImage, this.ClientRectangle);
						g.ResetClip();
					}

                    if (this.FocusColor.A > 0)
                    {
                        int alpha;
                        alpha = AlphaMultiply(mFocusAlpha, this.FocusColor.A);
                        FillPath(g, Color.FromArgb(alpha, this.FocusColor), rr);

                        alpha = AlphaMultiply(255 - mFocusAlpha, this.ButtonColor.A);
                        FillPath(g, Color.FromArgb(alpha, this.ButtonColor), rr);
                    }
                    else
                    {
                        FillPath(g, this.ButtonColor, rr);
                    }
				}
			}

			/// <summary>
			/// Draws the Highlight over the top of the
			/// control using the HightlightColor.
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawHighlight(Graphics g)
			{
				if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None){return;}
				
				if (mButtonState == State.Pressed)
				{
					int alpha = AlphaMultiply(70, this.HighlightColor.A);
                    Rectangle r = this.ClientRectangle;
                    r.Width -= 1; r.Height -= 1;
                    r.Inflate(-1, -1);

                    using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
					{
						RectangleF rectBounds = rr.GetBounds();
						using (LinearGradientBrush lg = new LinearGradientBrush(rectBounds, 
								   Color.FromArgb(alpha / 3, this.HighlightColor),
								   Color.FromArgb(alpha, this.HighlightColor), 
								   LinearGradientMode.Vertical))
						{
							g.FillPath(lg, rr);
                            DrawPath(g, lg, rr);
						}
					}
				}
				else
				{
                    int alpha = AlphaMultiply(150, this.HighlightColor.A);

                    Rectangle r = this.ClientRectangle;
                    r.Width -= 1; r.Height -= 1;
                    r.Inflate(-1, -1);
                    r.Height /= 2;

                    using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.Top))
					{
						RectangleF rectBounds = rr.GetBounds();
						using (LinearGradientBrush lg = new LinearGradientBrush(rectBounds, 
								   Color.FromArgb(alpha, this.HighlightColor),
								   Color.FromArgb(alpha / 10, this.HighlightColor), 
								   LinearGradientMode.Vertical))
						{
							g.FillPath(lg, rr);
                            DrawPath(g, lg, rr);
                        }
					}
				}
			}

			/// <summary>
			/// Draws the focus for the button when the
			/// mouse is inside the client area using
			/// the GlowColor property.
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawFocus(Graphics g)
			{
				if (this.Enabled == false)
					return;
                if (this.FocusGlowColor.A == 0)
					return;

				SetClip(g);
				using (GraphicsPath glow = new GraphicsPath())
				{
					int height = (int)(this.Height * 0.8);
					int width = (int)(this.Width * 0.5);

					int centreWidth = (int)(this.Width * 0.5);
					int centreHeight = (int)(this.Height * 0.6);

					glow.AddEllipse(centreWidth - width, centreHeight - height, width * 2, height * 2);

					using (PathGradientBrush gl = new PathGradientBrush(glow))
					{
                        int alpha = AlphaMultiply(mFocusAlpha, this.FocusGlowColor.A);
						gl.CenterColor = Color.FromArgb(alpha, this.FocusGlowColor);
						gl.SurroundColors = new Color[] {Color.FromArgb(0, this.FocusGlowColor)};
						g.FillPath(gl, glow);
                        DrawPath(g, gl, glow);
					}
				}
			}

			/// <summary>
			/// Draws the glow for the button when the
			/// mouse is inside the client area using
			/// the GlowColor property.
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawGlow(Graphics g)
			{
				if (this.Enabled == false)
					return;
				if (this.GlowColor.A == 0)
					return;

				SetClip(g);
				using (GraphicsPath glow = new GraphicsPath())
				{
					int size = (int)(this.Height * 0.75);
					int centre = this.Width / 2;
					glow.AddEllipse(centre - (size * 3),this.Height - size, size * 6, size * 2);
					using (PathGradientBrush gl = new PathGradientBrush(glow))
					{
						gl.CenterColor = Color.FromArgb(mGlowAlpha, this.GlowColor);
						gl.SurroundColors = new Color[] {Color.FromArgb(0, this.GlowColor)};
						g.FillPath(gl, glow);
                        DrawPath(g, gl, glow);
					}
				}
				g.ResetClip();
			}

			/// <summary>
			/// Draws the text for the button.
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawText(Graphics g)
			{
				if (this.Text.Length == 0)
					return;

				StringFormat sf = StringFormatAlignment(this.TextAlign);
				sf.Trimming = StringTrimming.EllipsisCharacter;
				sf.FormatFlags = StringFormatFlags.NoWrap;
				Rectangle r = new Rectangle(0,0,this.Width - 1,this.Height - 1);

                int inflateX = -4;
                int inflateY = -4;
				inflateX += (int)(this.CornerRadius * -0.75);
				r.Inflate(inflateX, inflateY);

                if (this.Image != null)
                {
                    Rectangle imageRect = GetImageRectangle();
                    switch (this.ImageAlign)
                    {
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.BottomLeft:
                            r.Width = r.Right - (imageRect.Right + 4);
                            r.X = imageRect.Right + 4;
                            break;
                        case ContentAlignment.TopRight:
                        case ContentAlignment.MiddleRight:
                        case ContentAlignment.BottomRight:
                            r.Width = r.Right - (imageRect.Right + 4);
                            break;
                    }
                }

				int alpha = (this.Enabled) ? 255 : 128;
				Color textColor = Color.FromArgb(alpha, this.ForeColor);

				g.DrawString(this.Text,this.Font,new SolidBrush(textColor),r,sf);
			}

            private void DrawKeyText(Graphics g)
            {
                if (this.KeyText.Length == 0)
                    return;

                ContentAlignment align = ContentAlignment.MiddleRight;
                if (this.TextAlign == ContentAlignment.TopLeft)
                    align = ContentAlignment.TopRight;
                if (this.TextAlign == ContentAlignment.TopCenter)
                    align = ContentAlignment.TopRight;
                if (this.TextAlign == ContentAlignment.TopRight)
                    align = ContentAlignment.TopLeft;
                if (this.TextAlign == ContentAlignment.MiddleLeft)
                    align = ContentAlignment.MiddleRight;
                if (this.TextAlign == ContentAlignment.MiddleCenter)
                    align = ContentAlignment.MiddleRight;
                if (this.TextAlign == ContentAlignment.MiddleRight)
                    align = ContentAlignment.MiddleLeft;
                if (this.TextAlign == ContentAlignment.BottomLeft)
                    align = ContentAlignment.BottomRight;
                if (this.TextAlign == ContentAlignment.BottomCenter)
                    align = ContentAlignment.BottomRight;
                if (this.TextAlign == ContentAlignment.BottomRight)
                    align = ContentAlignment.BottomLeft;


                StringFormat sf = StringFormatAlignment(align);
                sf.Trimming = StringTrimming.None;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

                int inflateX = -4;
                int inflateY = -4;
                inflateX += (int)(this.CornerRadius * -0.75);
                r.Inflate(inflateX, inflateY);

                // SizeF size = g.MeasureString(this.KeyText, this.Font, r.Width, sf);

                int alpha = (this.Enabled) ? 255 : 128;
                Color textColor = Color.FromArgb(alpha, this.ForeColor);

                g.DrawString(this.KeyText, this.Font, new SolidBrush(textColor), r, sf);
            }

			/// <summary>
			/// Draws the image for the button
			/// </summary>
			/// <param name="g">The graphics object used in the paint event.</param>
			private void DrawImage(Graphics g)
			{
				if (this.Image == null) {return;}
                Rectangle r = GetImageRectangle();
				g.DrawImage(this.Image,r);
			}

            private Rectangle GetImageRectangle()
            {
                if (this.Image == null)
                    return new Rectangle();

                Rectangle r = this.ClientRectangle;
                // r.Width -= 1; r.Height -= 1;  skip these so the /2 rounds up
                r.Inflate(-1, -1);

                const int padding = 8;

                switch (this.ImageAlign)
                {
                    case ContentAlignment.TopCenter:
                        return new Rectangle(r.Width / 2 - this.ImageSize.Width / 2, padding, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.TopRight:
                        return new Rectangle(r.Width - padding - this.ImageSize.Width, padding, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.MiddleLeft:
                        return new Rectangle(padding, r.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.MiddleCenter:
                        return new Rectangle(r.Width / 2 - this.ImageSize.Width / 2, r.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.MiddleRight:
                        return new Rectangle(r.Width - padding - this.ImageSize.Width, r.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.BottomLeft:
                        return new Rectangle(padding, r.Height - padding - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.BottomCenter:
                        return new Rectangle(r.Width / 2 - this.ImageSize.Width / 2, r.Height - padding - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.BottomRight:
                        return new Rectangle(r.Width - padding - this.ImageSize.Width, r.Height - padding - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    case ContentAlignment.TopLeft:
                    default:
                        return new Rectangle(padding, padding, this.ImageSize.Width, this.ImageSize.Height);
                }
            }

			private void SetClip(Graphics g)
			{
				Rectangle r = this.ClientRectangle;
                r.Width -= 1; r.Height -= 1;
                r.Inflate(-1, -1);
                using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
				{
					g.SetClip(rr);
				}		
			}

		private void InvalidateEx()
		{
//			if (this.Parent == null)
//				return;
//			Rectangle rc = new Rectangle(this.Location, this.Size);
//			this.Parent.Invalidate(rc, true);
			Invalidate();
		}

		#endregion

		#region -  Private Subs  -

			private void VistaButton_Paint(object sender, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				DrawDropShadow(e.Graphics);
				DrawBackground(e.Graphics);
				DrawHighlight(e.Graphics);
				DrawImage(e.Graphics);
				DrawFocus(e.Graphics);
				DrawText(e.Graphics);
                DrawKeyText(e.Graphics);
				DrawGlow(e.Graphics);
				DrawOuterStroke(e.Graphics);
				DrawInnerStroke(e.Graphics);
			}

			private void VistaButton_Resize(object sender, EventArgs e)
			{
				Rectangle r = this.ClientRectangle;
                r.Width += 1; r.Height += 1;
                using (GraphicsPath rr = RoundRect(r, CornerRadius, RectCorners.All))
				{
					this.Region = new Region(rr);
				}
			}

		#region -  Mouse and Keyboard Events  -

			private void VistaButton_MouseEnter(object sender, EventArgs e)
			{
				mButtonState = State.Hover;
				mGlowFadeOut.Stop();
				mGlowFadeIn.Start();
			}
			private void VistaButton_MouseLeave(object sender, EventArgs e)
			{
				mButtonState = State.None;
				if (this.GlowColor.A == 0)
					return;
				if (this.mButtonStyle == Style.Flat) { mGlowAlpha = 0; }
				mGlowFadeIn.Stop();
				mGlowFadeOut.Start();
			}

			private void VistaButton_MouseDown(object sender, MouseEventArgs e)
			{
				if (this.Enabled == false)
					return;
				if (e.Button == MouseButtons.Left)
				{
					mButtonState = State.Pressed;
					if (this.mButtonStyle != Style.Flat) { mGlowAlpha = 255; }
					if (this.GlowColor.A == 0) { mGlowAlpha = 255; }
					mGlowFadeIn.Stop();
					mGlowFadeOut.Stop();
					this.InvalidateEx();
				}
			}

			private void mGlowFadeIn_Tick(object sender, EventArgs e)
			{
				if (this.ButtonStyle == Style.Flat) {mGlowAlpha = 255;}
				if (this.GlowColor.A == 0) {mGlowAlpha = 255;}
				if (mGlowAlpha + GlowFadeSpeed >= 255)
				{
					mGlowAlpha = 255;
					mGlowFadeIn.Stop();
				}
				else
				{
					mGlowAlpha += GlowFadeSpeed;
				}
				this.InvalidateEx();
			}

			private void mGlowFadeOut_Tick(object sender, EventArgs e)
			{
				if (this.ButtonStyle == Style.Flat) {mGlowAlpha = 0;}
				if (this.GlowColor.A == 0) {mGlowAlpha = 0;}
				if (mGlowAlpha - GlowFadeSpeed <= 0)
				{
					mGlowAlpha = 0;
					mGlowFadeOut.Stop();
				}
				else
				{
					mGlowAlpha -= GlowFadeSpeed;
				}
				this.InvalidateEx();
			}

			private void VistaButton_KeyDown(object sender, KeyEventArgs e)
			{
				if ((e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Enter))
				{
					MouseEventArgs m = new MouseEventArgs(MouseButtons.Left,0,0,0,0);
					VistaButton_MouseDown(sender, m);
				}
			}

			private void VistaButton_KeyUp(object sender, KeyEventArgs e)
			{
                if ((e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Enter))
                {
					MouseEventArgs m = new MouseEventArgs(MouseButtons.Left,0,0,0,0);
					calledbykey = true;
					VistaButton_MouseUp(sender, m);
				}
			}

			private void VistaButton_MouseUp(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left) 
				{
					mButtonState = State.Hover;
					mGlowFadeIn.Stop();
					mGlowFadeOut.Stop();
					this.InvalidateEx();
					if (calledbykey == true) {this.OnClick(EventArgs.Empty); calledbykey = false;}
				}
			}

			private void VistaButton_GotFocus(object sender, EventArgs e)
			{
                mFocusFadeOut.Stop();
                mFocusFadeIn.Start();
				this.InvalidateEx();
			}

			private void VistaButton_LostFocus(object sender, EventArgs e)
			{
                mFocusFadeIn.Stop();
                mFocusFadeOut.Start();
				this.InvalidateEx();
			}

            private void mFocusFadeIn_Tick(object sender, EventArgs e)
            {
                if (this.ButtonStyle == Style.Flat) { mFocusAlpha = 255; }
                if (mFocusAlpha + FocusFadeSpeed >= 255)
                {
                    mFocusAlpha = 255;
                    mFocusFadeIn.Stop();
                }
                else
                {
                    mFocusAlpha += FocusFadeSpeed;
                }
                this.InvalidateEx();
            }

            private void mFocusFadeOut_Tick(object sender, EventArgs e)
            {
                if (this.ButtonStyle == Style.Flat) { mFocusAlpha = 0; }
                if (mFocusAlpha - FocusFadeSpeed <= 0)
                {
                    mFocusAlpha = 0;
                    mFocusFadeOut.Stop();
                }
                else
                {
                    mFocusAlpha -= FocusFadeSpeed;
                }
                this.InvalidateEx();
            }

		#endregion
		
		#endregion

		#region IButtonControl Members

		private System.Windows.Forms.DialogResult _dialogResult = System.Windows.Forms.DialogResult.None;
		public System.Windows.Forms.DialogResult DialogResult
		{
			get
			{
				return _dialogResult;
			}
			set
			{
				_dialogResult = value;
			}
		}

		public void PerformClick()
		{
			this.OnClick(new EventArgs());
		}

		public void NotifyDefault(bool value)
		{
			_defaultButton = value;
		}

		#endregion

	}
}