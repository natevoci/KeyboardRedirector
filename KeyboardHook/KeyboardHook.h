#ifndef _DEFINED_KEYBOARD_HOOK
#define _DEFINED_KEYBOARD_HOOK

#if _MSC_VER > 1000
#pragma once
#endif

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the KEYBOARDHOOK_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// KEYBOARDHOOK_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef KEYBOARDHOOK_EXPORTS
#define KEYBOARDHOOK_API __declspec(dllexport)
#else
#define KEYBOARDHOOK_API __declspec(dllimport)
#endif

KEYBOARDHOOK_API BOOL SetHook(HWND hWnd, UINT message);
KEYBOARDHOOK_API BOOL ClearHook(HWND hWnd);

#ifdef __cplusplus
}
#endif

#endif