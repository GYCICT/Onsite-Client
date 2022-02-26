# Add the driver to the driver store
pnputil -i -a C:\Deployment\HPOS58x64\POS58ENG.INF

# Install the printer driver
Add-PrinterDriver -Name "POS58ENG"

# Add the printer
Add-Printer -Port USB001 -DriverName POS58ENG -Name "POS58 Printer" -ErrorAction SilentlyContinue

# make default. this might need to be executed in the kiosk user profile
$wsnet=new-object -ComObject Wscript.Network
$wsnet.SetDefaultPrinter("POS58 Printer")