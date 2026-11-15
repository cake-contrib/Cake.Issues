#load create-reports-htmldatatable-default.cake

Task("Create-Reports-HtmlDataTable")
    .Description("Creates HtmlDataTable demo reports")
    .IsDependentOn("Create-Reports-HtmlDataTable-Default");