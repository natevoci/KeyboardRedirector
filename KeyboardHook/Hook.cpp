// Hook.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Hook.h"
#include "KeyboardHook.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
HWND _hwnd;

UINT hwnd = 0;
UINT msgId = 0;
UINT msgId_LL = 0;


// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_HOOK, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	int length = lstrlen(lpCmdLine); //_tcslen
	if (length > 0)
	{
		// Perform application initialization:
		if (!InitInstance (hInstance, SW_HIDE))
		//if (!InitInstance (hInstance, SW_SHOW))
		{
			return FALSE;
		}

		int pos = 0;
		int paramIndex = 0;
		bool paramUsed = false;
		while (pos < length)
		{
			if ((lpCmdLine[pos] >= '0') && (lpCmdLine[pos] <= '9'))
			{
				paramUsed = true;
				int val = (lpCmdLine[pos] - '0');
				switch (paramIndex)
				{
					case 0:
						hwnd *= 10;
						hwnd += val;
						break;
					case 1:
						msgId *= 10;
						msgId += val;
						break;
					case 2:
						msgId_LL *= 10;
						msgId_LL += val;
						break;
					default:
						break;
				}
			}
			else if (lpCmdLine[pos] == ' ')
			{
				if (paramUsed == true)
				{
					paramIndex++;
					paramUsed = false;
				}
			}
			else
			{
				return -1; // Invalid characters detected parsing parameters.
			}
			pos++;
		}

		if (paramUsed == false)
			paramIndex--;

		if (paramIndex != 2)
			return -2; // Invalid number of arguments
	}
	else
	{
		// Perform application initialization:
		if (!InitInstance (hInstance, SW_SHOW))
		{
			return FALSE;
		}

		// No params passed. We'll send to ourself for debugging.
		hwnd = (UINT)_hwnd;
		msgId = 0x401;
		msgId_LL = 0x402;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_HOOK));


	//HWND hwnd = FindWindow(NULL, L"Keyboard Redirector");
	if (hwnd == 1)
		hwnd = (UINT)FindWindow(NULL, L"Keyboard Redirector");
	if (hwnd == 0)
		return -3; // Invalid window handle;

	if (msgId != 0)
		SetHook((HWND)hwnd, msgId);

	if (msgId_LL != 0)
		SetHook_LL((HWND)hwnd, msgId_LL);

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	if (msgId != 0)
		ClearHook((HWND)hwnd);

	if (msgId != 0)
		ClearHook_LL((HWND)hwnd);

	return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
//  COMMENTS:
//
//    This function and its usage are only necessary if you want this code
//    to be compatible with Win32 systems prior to the 'RegisterClassEx'
//    function that was added to Windows 95. It is important to call this function
//    so that the application will get 'well formed' small icons associated
//    with it.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_HOOK));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_HOOK);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   HWND hWnd;

   hInst = hInstance; // Store instance handle in our global variable

   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, 0, 400, 200, NULL, NULL, hInstance, NULL);

   if (!hWnd)
   {
      return FALSE;
   }
   _hwnd = hWnd;

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message)
	{
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case 0x401:
		{
			HDC hdc = GetDC(hWnd);
			LPWSTR str = new WCHAR[100];
			memset(str, 0, 100 * sizeof(WCHAR));
			wsprintf(str, L"msghook: wparam=%#08x %#08x", wParam, lParam);
			TextOut(hdc, 30, 30, str, 100);
			ReleaseDC(hWnd, hdc);
		}
		break;
	case 0x402:
		{
			HDC hdc = GetDC(hWnd);
			LPWSTR str = new WCHAR[100];
			memset(str, 0, 100 * sizeof(WCHAR));
			wsprintf(str, L"msghook: wparam=%#08x %#08x", wParam, lParam);
			TextOut(hdc, 30, 80, str, 100);
			ReleaseDC(hWnd, hdc);
		}
		break;
	case WM_PAINT:
		{
			hdc = BeginPaint(hWnd, &ps);

			LPWSTR str = new WCHAR[100];
			memset(str, 0, 100 * sizeof(WCHAR));
			wsprintf(str, L"hwnd: %d msgId=%#x msgId_LL %#x", hwnd, msgId, msgId_LL);
			TextOut(hdc, 10, 10, str, 100);

			EndPaint(hWnd, &ps);
		}
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}
