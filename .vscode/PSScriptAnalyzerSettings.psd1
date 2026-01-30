@{
    # PowerShell Script Analyzer settings
    # Exclude .git directory from analysis
    ExcludeRules        = @()
    IncludeDefaultRules = $true
    Rules               = @{
        PSAvoidUsingCmdletAliases = @{
            # Allow 'echo' alias in bash scripts (not PowerShell)
            Enable = $false
        }
    }
}
