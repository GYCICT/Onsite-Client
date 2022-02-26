# Set up POS printer

# Install the printer driver
Add-PrinterDriver -Name "POS58ENG"

# Add the printer
Add-Printer -Port USB001 -DriverName POS58ENG -Name "POS58 Printer" -ErrorAction SilentlyContinue

# make default. this might need to be executed in the kiosk user profile
$wsnet=new-object -ComObject Wscript.Network
$wsnet.SetDefaultPrinter("POS58 Printer")

# Install certs and allow sideload

# Allow apps to be sideloaded.
reg.exe add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\AppModelUnlock" /f /v "AllowAllTrustedApps" /t REG_DWORD /d 1

# Install the cert
powershell.exe -executionpolicy unrestricted "C:\Deployment\cert.ps1"

# Install Kiosk App for Kiosk user
reg.exe add "HKLM\System\CurrentControlSet\Control\Lsa" /f /v "LimitBlankPasswordUse" /t REG_DWORD /d 0
$creds = New-Object System.Management.Automation.PSCredential ("Kiosk", (new-object System.Security.SecureString))
start-process -FilePath powershell.exe -wait -Credential $creds -ArgumentList @("-executionpolicy","Unrestricted","C:\Deployment\InstallKiosk.ps1")
# User Policy for Kiosk user
start-process -FilePath powershell.exe -wait -Credential $creds -ArgumentList @("-executionpolicy","Unrestricted","C:\Deployment\UserPolicy.ps1")
reg.exe add "HKLM\System\CurrentControlSet\Control\Lsa" /f /v "LimitBlankPasswordUse" /t REG_DWORD /d 1

# Install Kiosk App for Admin user. Not needed, kept here for reference
# start-process -FilePath powershell.exe -wait -ArgumentList @("-executionpolicy","Unrestricted","C:\Deployment\InstallKiosk.ps1")

# Machine Policy
start-process -FilePath powershell.exe -wait -ArgumentList @("-executionpolicy","Unrestricted","C:\Deployment\MachinePolicy.ps1")

# Assigned Access
Set-AssignedAccess -Username Kiosk -AppName "7acd2f74-b62d-4dbb-a73f-31ef89e95a0c"

