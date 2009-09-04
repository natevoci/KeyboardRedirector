@echo off

if "%~x1" == ".bat" (
	echo call %*
	call %*
) else (
	echo %*
	%*
)

set RETURNCODE=%ERRORLEVEL%
if not %RETURNCODE%==0 echo Exited with code %RETURNCODE%
echo _ %RETURNCODE%

%~d0
cd %~dp0

exit /B %RETURNCODE%
