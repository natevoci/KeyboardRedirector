// KeyboardHook.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "KeyboardHook.h"

#pragma data_seg(".KEYBOARDHOOK")
HWND s_hWndServer = NULL;
UINT s_message = 0;
#pragma data_seg()
#pragma comment(linker, "/section:.KEYBOARDHOOK,rws")

HINSTANCE hInst;
HHOOK hook;
static LRESULT CALLBACK msghook(UINT nCode, WPARAM wParam, LPARAM lParam);

#ifdef _MANAGED
#pragma managed(push, off)
#endif


BOOL APIENTRY DllMain( HINSTANCE hInstance,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		hInst = hInstance;
		break;
	case DLL_PROCESS_DETACH:
		if(s_hWndServer != NULL)
			ClearHook(s_hWndServer);
		break;
	}
    return TRUE;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif

KEYBOARDHOOK_API BOOL SetHook(HWND hWnd, UINT message)
{
	if (s_hWndServer != NULL)
		return FALSE; // already hooked

	hook = SetWindowsHookEx(WH_KEYBOARD, (HOOKPROC)msghook, hInst, 0);
	if (hook == FALSE)
		return FALSE;

	s_hWndServer = hWnd;
	s_message = message;
	return TRUE;
}

KEYBOARDHOOK_API BOOL ClearHook(HWND hWnd)
{
	if ((hWnd == NULL) || (s_hWndServer == NULL) || (hWnd != s_hWndServer))
		return FALSE;

	BOOL unhooked = UnhookWindowsHookEx(hook);
	if (unhooked)
	{
		s_hWndServer = NULL;
		s_message = 0;
	}
	return unhooked;
}

union KeyboardProcLParam
{
	unsigned int lParam;
	struct _keyboardProcLParam {
	   unsigned short repeatCount      : 16;
	   unsigned short scanCode         : 8;
	   unsigned short extendedKey      : 1;
	   unsigned short reserved         : 4;
	   unsigned short contextCode      : 1;
	   unsigned short previousKeyState : 1;
	   unsigned short transitionCode   : 1;
	} values;
};

static LRESULT CALLBACK msghook(UINT nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode < 0) // The specs say if nCode is < 0 then we just pass straight on.
	{
		CallNextHookEx(hook, nCode, wParam, lParam);
		return 0;
	}

	WPARAM newWParam = wParam;
	if (nCode == HC_NOREMOVE)
	{
		newWParam = wParam | 0x80000000;
		CallNextHookEx(hook, nCode, wParam, lParam);
		return 0;
	}

	KeyboardProcLParam l;
	memset(&l, 0, sizeof(l));
	l.lParam = (unsigned int)lParam;

	LRESULT result;
	result = ::SendMessage(s_hWndServer, s_message, newWParam, lParam);
	//if (wParam == VK_NUMPAD3)
	if (result != 0)
	{

		INPUT input[1];
		memset(input, 0, sizeof(input));
		input[0].type = INPUT_KEYBOARD;

		input[0].ki.wVk = VK_NUMPAD7;
		input[0].ki.dwFlags = (l.values.transitionCode == 1) ? KEYEVENTF_KEYUP : 0;
		input[0].ki.time = 0;
		input[0].ki.dwExtraInfo = 0;

		SendInput(1, input, sizeof(INPUT));
		return -1;
	}

	return CallNextHookEx(hook, nCode, wParam, lParam);

}
