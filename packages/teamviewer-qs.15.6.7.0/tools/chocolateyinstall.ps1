$packageArgs = @{
  packageName            = "$env:chocolateyPackageName"
  FileFullPath           = "$(Split-Path -Parent $MyInvocation.MyCommand.Definition)\TeamViewerQS.exe"
  url                    = 'https://download.teamviewer.com/download/TeamViewerQS.exe'
  checksum               = '20f58e8081d252a767c665dfa39d50e6cb5b488e003a7a239dbc3050c9237109'
  checksumType           = 'sha256'
}
Get-ChocolateyWebFile @packageArgs
