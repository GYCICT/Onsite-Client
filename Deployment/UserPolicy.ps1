# Touch keyboard
reg.exe add "HKCU\SOFTWARE\Microsoft\TabletTip\1.7" /f /v "EdgeTargetDockedState" /t REG_DWORD /d 1
reg.exe add "HKCU\SOFTWARE\Microsoft\TabletTip\1.7" /f /v "EnableAutocorrection" /t REG_DWORD /d 0
reg.exe add "HKCU\SOFTWARE\Microsoft\TabletTip\1.7" /f /v "EnableAutocorrection" /t REG_DWORD /d 0
reg.exe add "HKCU\SOFTWARE\Microsoft\TabletTip\1.7" /f /v "EnableTextPrediction" /t REG_DWORD /d 0
reg.exe add "HKCU\SOFTWARE\Microsoft\TabletTip\1.7" /f /v "EnableDesktopModeAutoInvoke" /t REG_DWORD /d 1

