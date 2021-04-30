# Filename: ProxyDisable.ps1
# Requires an Elevated PowerShell

# Disable the Proxy (Global)
Set-ItemProperty -Path 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\' -Name 'ProxyEnable' -Value 0
# Unset any Proxy Variables (Local)
$pvars=@('http_proxy','https_proxy', 'no_proxy')
foreach ($pvar in $pvars) {
  Remove-Item "ENV:\${pvar}" -ErrorAction SilentlyContinue
}

# Might have to search for and Disable the McAfee Proxy
Stop-Service -Name 'mcpservice' -Force