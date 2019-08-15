#load create-reports-htmldxdatagrid-default.cake
#load create-reports-htmldxdatagrid-theme-light.cake
#load create-reports-htmldxdatagrid-theme-dark.cake
#load create-reports-htmldxdatagrid-theme-contrast.cake
#load create-reports-htmldxdatagrid-theme-carmine.cake
#load create-reports-htmldxdatagrid-theme-darkmoon.cake
#load create-reports-htmldxdatagrid-theme-softblue.cake
#load create-reports-htmldxdatagrid-theme-darkviolet.cake
#load create-reports-htmldxdatagrid-theme-greenmist.cake
#load create-reports-htmldxdatagrid-theme-lightcompact.cake
#load create-reports-htmldxdatagrid-theme-darkcompact.cake
#load create-reports-htmldxdatagrid-theme-contrastcompact.cake
#load create-reports-htmldxdatagrid-theme-materialbluelight.cake
#load create-reports-htmldxdatagrid-theme-materiallimelight.cake
#load create-reports-htmldxdatagrid-theme-materialorangelight.cake
#load create-reports-htmldxdatagrid-theme-materialpurplelight.cake
#load create-reports-htmldxdatagrid-theme-materialteallight.cake
#load create-reports-htmldxdatagrid-theme-materialbluedark.cake
#load create-reports-htmldxdatagrid-theme-materiallimedark.cake
#load create-reports-htmldxdatagrid-theme-materialorangedark.cake
#load create-reports-htmldxdatagrid-theme-materialpurpledark.cake
#load create-reports-htmldxdatagrid-theme-materialtealdark.cake
#load create-reports-htmldxdatagrid-theme-materialbluelightcompact.cake
#load create-reports-htmldxdatagrid-theme-materiallimelightcompact.cake
#load create-reports-htmldxdatagrid-theme-materialorangelightcompact.cake
#load create-reports-htmldxdatagrid-theme-materialpurplelightcompact.cake
#load create-reports-htmldxdatagrid-theme-materialteallightcompact.cake
#load create-reports-htmldxdatagrid-theme-materialbluedarkcompact.cake
#load create-reports-htmldxdatagrid-theme-materiallimedarkcompact.cake
#load create-reports-htmldxdatagrid-theme-materialorangedarkcompact.cake
#load create-reports-htmldxdatagrid-theme-materialpurpledarkcompact.cake
#load create-reports-htmldxdatagrid-theme-materialtealdarkcompact.cake
#load create-reports-htmldxdatagrid-hide-columns.cake
#load create-reports-htmldxdatagrid-additional-columns.cake
#load create-reports-htmldxdatagrid-sorting.cake
#load create-reports-htmldxdatagrid-grouping.cake
#load create-reports-htmldxdatagrid-disable-grouping.cake
#load create-reports-htmldxdatagrid-change-title.cake
#load create-reports-htmldxdatagrid-disable-header.cake
#load create-reports-htmldxdatagrid-disable-filtering.cake
#load create-reports-htmldxdatagrid-disable-searching.cake
#load create-reports-htmldxdatagrid-file-linking.cake
#load create-reports-htmldxdatagrid-custom-script-location.cake

Task("Create-Reports-HtmlDxDataGrid")
    .Description("Creates HtmlDxDataGrid demo reports")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Default")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-Light")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-Dark")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-Contrast")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-Carmine")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-DarkMoon")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-SoftBlue")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-DarkViolet")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-GreenMist")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-LightCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-DarkCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-ContrastCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialBlueLight")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialLimeLight")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialOrangeLight")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialPurpleLight")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialTealLight")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialBlueDark")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialLimeDark")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialOrangeDark")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialPurpleDark")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialTealDark")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialBlueLightCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialLimeLightCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialOrangeLightCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialPurpleLightCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialTealLightCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialBlueDarkCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialLimeDarkCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialOrangeDarkCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialPurpleDarkCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-MaterialTealDarkCompact")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Hide-Columns")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Additional-Columns")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Sorting")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Grouping")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Disable-Grouping")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Change-Title")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Disable-Header")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Disable-Filtering")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Disable-Searching")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-File-Linking")
    .IsDependentOn("Create-Reports-HtmlDxDataGrid-Custom-Script-Location");