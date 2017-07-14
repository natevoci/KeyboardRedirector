@echo off

REM Set the working folder to the folder this batch file is in just to be sure
%~d0
cd %~dp0

time /T

if not exist "SetEnvVars.bat" (
	copy "SetEnvVars.bat.template" "SetEnvVars.bat"
	
	call exec ".\bin\fregex.exe" "r|MSBUILDPATH=.*$|MSBUILDPATH=$r|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSBuild\ToolsVersions\14.0\MSBuildToolsPath|" -i "SetEnvVars.bat" -o "SetEnvVars.bat"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
)

call SetEnvVars.bat

if not exist "%MSBUILDPATH%" (
	echo Error: MSBUILDPATH does not exist: "%MSBUILDPATH%"
	echo "Please edit your SetEnvVars.bat file to enter the correct location for msbuild. eg. C:\Program Files (x86)\MSBuild\14.0\Bin\amd64\"
	goto ConfigIsWrong
)

if not exist .\Temp mkdir .\Temp

:Update-VersionNumbers
echo ######################## Update-VersionNumbers ########################

	REM Use the rev of the repo for the version
	git rev-list HEAD --count > .\Temp\version.txt
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

	set /p VERSION_D=<.\Temp\version.txt
	call SetVersion.bat
	echo Version is %VERSION%
	del .\Temp\version.txt

    REM back up files that hold versions so they can be restored after the build
    REM  (this is just so that these files don't keep showing up in the svn commit dialog)
    call exec copy /Y "..\KeyboardRedirector\Properties\AssemblyInfo.cs" ".\Temp\KeyboardRedirectorAssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    call exec copy /Y "..\ApplicationLauncher\Properties\AssemblyInfo.cs" ".\Temp\ApplicationLauncherAssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    call exec copy /Y "..\VolumeChanger\Properties\AssemblyInfo.cs" ".\Temp\VolumeChangerAssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    call exec copy /Y "..\OSD\Properties\AssemblyInfo.cs" ".\Temp\OSDAssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    call exec copy /Y "..\kXToggle\Properties\AssemblyInfo.cs" ".\Temp\kXToggleAssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    call exec copy /Y "..\KeyboardHook\Hook.rc" ".\Temp\Hook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    call exec copy /Y "..\KeyboardHook\KeyboardHook.rc" ".\Temp\KeyboardHook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

    REM Replace the version numbers
	call exec ".\bin\fregex.exe" "s/AssemblyVersion.*$/AssemblyVersion(\"%VERSION%\")]/" "s/AssemblyFileVersion.*$/AssemblyFileVersion(\"%VERSION%\")]/" -i "..\KeyboardRedirector\Properties\AssemblyInfo.cs" -o "..\KeyboardRedirector\Properties\AssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

	call exec ".\bin\fregex.exe" "s/AssemblyVersion.*$/AssemblyVersion(\"%VERSION%\")]/" "s/AssemblyFileVersion.*$/AssemblyFileVersion(\"%VERSION%\")]/" -i "..\ApplicationLauncher\Properties\AssemblyInfo.cs" -o "..\ApplicationLauncher\Properties\AssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
	call exec ".\bin\fregex.exe" "s/AssemblyVersion.*$/AssemblyVersion(\"%VERSION%\")]/" "s/AssemblyFileVersion.*$/AssemblyFileVersion(\"%VERSION%\")]/" -i "..\VolumeChanger\Properties\AssemblyInfo.cs" -o "..\VolumeChanger\Properties\AssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

	call exec ".\bin\fregex.exe" "s/AssemblyVersion.*$/AssemblyVersion(\"%VERSION%\")]/" "s/AssemblyFileVersion.*$/AssemblyFileVersion(\"%VERSION%\")]/" -i "..\OSD\Properties\AssemblyInfo.cs" -o "..\OSD\Properties\AssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
	call exec ".\bin\fregex.exe" "s/AssemblyVersion.*$/AssemblyVersion(\"%VERSION%\")]/" "s/AssemblyFileVersion.*$/AssemblyFileVersion(\"%VERSION%\")]/" -i "..\kXToggle\Properties\AssemblyInfo.cs" -o "..\kXToggle\Properties\AssemblyInfo.cs"
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

	call exec "%MSBUILDPATH%\msbuild.exe" "..\Hooks.sln" /t:Rebuild /p:Configuration=Release /p:Platform="Win32"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
	call exec "%MSBUILDPATH%\msbuild.exe" "..\Hooks.sln" /t:Rebuild /p:Configuration=Release /p:Platform="x64"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

    REM restore resource files to original (unaltered version number)
    copy /Y ".\Temp\KeyboardHook.rc" "..\KeyboardHook\KeyboardHook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    copy /Y ".\Temp\Hook.rc" "..\KeyboardHook\Hook.rc"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
:Build-Redirector
echo ################## Build-KeyboardRedirector ##################

	call exec "%MSBUILDPATH%\msbuild.exe" "..\KeyboardRedirector.sln" /t:Rebuild /p:Configuration=Release
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

    REM restore AssemblyInfo.cs to original (unaltered version number)
    call exec copy /Y ".\Temp\KeyboardRedirectorAssemblyInfo.cs" "..\KeyboardRedirector\Properties\AssemblyInfo.cs"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    

:Build-ApplicationLauncher
echo ################## Build-ApplicationLauncher ##################

	call exec "%MSBUILDPATH%\msbuild.exe" "..\ApplicationLauncher.sln" /t:Rebuild /p:Configuration=Release
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

    REM restore AssemblyInfo.cs to original (unaltered version number)
    call exec copy /Y ".\Temp\ApplicationLauncherAssemblyInfo.cs" "..\ApplicationLauncher\Properties\AssemblyInfo.cs"
    if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%


:Build-VolumeChanger
echo ################## Build-VolumeChanger ##################

	call exec "%MSBUILDPATH%\msbuild.exe" "..\VolumeChanger\VolumeChanger.sln" /t:Rebuild /p:Configuration=Release
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

    REM restore AssemblyInfo.cs to original (unaltered version number)
    call exec copy /Y ".\Temp\VolumeChangerAssemblyInfo.cs" "..\VolumeChanger\Properties\AssemblyInfo.cs"
    if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%


:Build-OSD
echo ################## Build-OSD ##################

	call exec "%MSBUILDPATH%\msbuild.exe" "..\OSD.sln" /t:Rebuild /p:Configuration=Release
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

    REM restore AssemblyInfo.cs to original (unaltered version number)
    call exec copy /Y ".\Temp\OSDAssemblyInfo.cs" "..\OSD\Properties\AssemblyInfo.cs"
    if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%


:Build-kXToggle
echo ################## Build-kXToggle ##################

	call exec "%MSBUILDPATH%\msbuild.exe" "..\kXToggle\kXToggle.sln" /t:Rebuild /p:Configuration=Release
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%

    REM restore AssemblyInfo.cs to original (unaltered version number)
    call exec copy /Y ".\Temp\kXToggleAssemblyInfo.cs" "..\kXToggle\Properties\AssemblyInfo.cs"
    if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%


:CreateBuildOutput

    if not exist "..\builds" mkdir "..\builds"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
    if not exist "..\builds\KeyboardRedirector-%VERSION%" mkdir "..\builds\KeyboardRedirector-%VERSION%"
	if not %ERRORLEVEL%==0 exit /B %ERRORLEVEL%
    
    copy /Y "..\ApplicationLauncher\bin\ApplicationLauncher.exe" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\ApplicationLauncher\bin\*.dll" "..\builds\KeyboardRedirector-%VERSION%"

    copy /Y "..\VolumeChanger\bin\VolumeChanger.exe" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\VolumeChanger\bin\VolumeChanger.exe.config" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\VolumeChanger\bin\*.dll" "..\builds\KeyboardRedirector-%VERSION%"

    copy /Y "..\OSD\bin\OSD.exe" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\OSD\bin\*.dll" "..\builds\KeyboardRedirector-%VERSION%"

    copy /Y "..\kXToggle\bin\kXToggle.exe" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\kXToggle\bin\kXToggle.exe.config" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\kXToggle\bin\*.dll" "..\builds\KeyboardRedirector-%VERSION%"
    
    copy /Y "..\KeyboardRedirector\bin\*.exe" "..\builds\KeyboardRedirector-%VERSION%"
    copy /Y "..\KeyboardRedirector\bin\*.dll" "..\builds\KeyboardRedirector-%VERSION%"
    if exist "..\builds\KeyboardRedirector-%VERSION%\KeyboardRedirector.vshost.exe" (
        del "..\builds\KeyboardRedirector-%VERSION%\KeyboardRedirector.vshost.exe"
    )
    
    
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
