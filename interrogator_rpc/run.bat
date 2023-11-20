@echo off
echo (NOTICE) Make a copy of this file and edit the call statements to match your environment path!

call T:/programs/anaconda3/Scripts/activate.bat
if %ERRORLEVEL% neq 0 goto Error

python main.py

pause

:Error
echo (ERROR) Failed to activate the Python environment. Did you set the path correctly?
pause
exit /b 1