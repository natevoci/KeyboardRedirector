@echo off

REM Set the working folder to the folder this batch file is in just to be sure
%~d0
cd %~dp0

time /T

if not exist "SetEnvVars.bat" (
	copy "SetEnvVars.bat.template" "SetEnvVars.bat"
	
	call exec ".\bin\fregex.exe" "r|VS9PATH=.*$|VS9PATH=$r|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0\InstallDir|" -i "SetEnvVars.bat" -o "SetEnvVars.bat"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

	call exec ".\bin\fregex.exe" "r|TSVNPATH=.*$|TSVNPATH=$r|HKEY_LOCAL_MACHINE\SOFTWARE\TortoiseSVN\Directory|" -i "SetEnvVars.bat" -o "SetEnvVars.bat"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
)

call SetEnvVars.bat

if not exist "%TSVNPATH%" (
	echo Error: TSVNPATH does not exist: "%TSVNPATH%"
	echo Please edit your SetEnvVars.bat file to enter the correct location for the TortoiseSVN installation folder
	goto ConfigIsWrong
)

if not exist "%VS9PATH%" (
	echo Error: VS9PATH does not exist: "%VS9PATH%"
	echo Please edit your SetEnvVars.bat file to enter the correct location for your Visual Studio 2008 installation
	goto ConfigIsWrong
)

if exist devenv.log del devenv.log

:Update-VersionNumbers
echo ######################## Update-VersionNumbers ########################

	REM Use the rev of the repo for the version
	call exec "%TSVNPATH%\bin\subwcrev.exe" ".." "SetVersion.bat.template" "SetVersion.bat"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
	call SetVersion.bat
	echo Version is %VERSION%
	del SetVersion.bat

	call exec ".\bin\fregex.exe" "s/AssemblyVersion.*$/AssemblyVersion(\"%VERSION%\")]/" "s/AssemblyFileVersion.*$/AssemblyFileVersion(\"%VERSION%\")]/" -i "..\KeyboardRedirector\Properties\AssemblyInfo.cs" -o "..\KeyboardRedirector\Properties\AssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

	call exec ".\bin\fregex.exe" "s/FILEVERSION .*$/FILEVERSION %VERSION_A%,%VERSION_B%,%VERSION_C%,%VERSION_D%/" "s/FileVersion\".*$/FileVersion\", \"%VERSION%\"/" -i "..\KeyboardHook\Hook.rc" -o "..\KeyboardHook\Hook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
	call exec ".\bin\fregex.exe" "s/PRODUCTVERSION .*$/PRODUCTVERSION %VERSION_A%,%VERSION_B%,%VERSION_C%,%VERSION_D%/" "s/ProductVersion\".*$/ProductVersion\", \"%VERSION%\"/" -i "..\KeyboardHook\Hook.rc" -o "..\KeyboardHook\Hook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
	call exec ".\bin\fregex.exe" "s/FILEVERSION .*$/FILEVERSION %VERSION_A%,%VERSION_B%,%VERSION_C%,%VERSION_D%/" "s/FileVersion\".*$/FileVersion\", \"%VERSION%\"/" -i "..\KeyboardHook\KeyboardHook.rc" -o "..\KeyboardHook\KeyboardHook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
	call exec ".\bin\fregex.exe" "s/PRODUCTVERSION .*$/PRODUCTVERSION %VERSION_A%,%VERSION_B%,%VERSION_C%,%VERSION_D%/" "s/ProductVersion\".*$/ProductVersion\", \"%VERSION%\"/" -i "..\KeyboardHook\KeyboardHook.rc" -o "..\KeyboardHook\KeyboardHook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
:End-VersionNumbers

:Build-Hooks
echo ######################## Build-Hooks ########################

	call exec "%VS9PATH%\devenv.exe" "..\Hooks.sln" /rebuild "Release|Win32" /out devenv.log
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
	call exec "%VS9PATH%\devenv.exe" "..\Hooks.sln" /rebuild "Release|x64" /out devenv.log
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

:Build-Redirector
echo ################## Build-KeyboardRedirector ##################

	call exec "%VS9PATH%\devenv.exe" "..\KeyboardRedirector.sln" /rebuild "Release" /out devenv.log
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%


:CreateBuildOutput

    if not exist "..\builds" mkdir "..\builds"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
    if not exist "..\builds\KeyboardRedirector-%VERSION%" mkdir "..\builds\KeyboardRedirector-%VERSION%"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
    copy /Y "..\KeyboardRedirector\bin\*.exe" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\KeyboardRedirector\bin\*.dll" "..\builds\KeyboardRedirector-%VERSION%"
    del "..\builds\KeyboardRedirector-%VERSION%\*.vshost.exe"
    
GOTO end

:ConfigIsWrong
	echo -------------------
	echo The settings file will now be opened in notepad.
	echo Please confirm that each of the path variables is pointing to the correct location.
	echo -------------------
	pause
	start notepad "SetEnvVars.bat"
	exit /B 1

:end
echo Build Successful
time /T
