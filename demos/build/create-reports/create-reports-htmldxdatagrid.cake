#load create-reports-htmldxdatagrid-default.cake

Task("Create-Reports-HtmlDxDataGrid")
    .Description("Creates HtmlDxDataGrid demo reports")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Default");