@echo on

echo %*

if exist "%~2KeyboardRedirector\bin" goto Copy

echo Creating bin folder
mkdir "%~2KeyboardRedirector\bin"

:Copy
copy /Y "%~1" "%~2KeyboardRedirector\bin\"

