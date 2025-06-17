@echo off
echo ✅ Launching PowerShell Provisioning Script...
powershell -ExecutionPolicy Bypass -File "%~dp0src\SharePointListProvisioner.ps1"
pause
