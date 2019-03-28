@echo off
TITLE Modifying your HOSTS file
COLOR 03
ECHO.

SET NEWLINE=^& echo.
ECHO Carrying out requested modifications to your HOSTS file
FIND /C /I "jpproject" %WINDIR%\system32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 ECHO %NEWLINE%>>%WINDIR%\system32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 ECHO 127.0.0.1    jpproject>>%WINDIR%\system32\drivers\etc\hosts
ECHO Finished
ECHO.
EXIT