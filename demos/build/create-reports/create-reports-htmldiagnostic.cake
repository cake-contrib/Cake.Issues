#load create-reports-htmldiagnostic-default.cake

Task("Create-Reports-HtmlDiagnostic")
    .Description("Creates HtmlDiagnostic demo reports")
    .IsDependentOn("Create-Reports-HtmlDiagnostic-Default");