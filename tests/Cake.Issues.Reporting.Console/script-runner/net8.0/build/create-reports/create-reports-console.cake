#load create-reports-console-default.cake
#load create-reports-console-grouped.cake
#load create-reports-console-summaries.cake
#load create-reports-console-combined.cake

Task("Create-Reports-Console-Examples")
    .Description("Creates all Console report examples")
    .IsDependentOn("Create-Reports-Console-Default")
    .IsDependentOn("Create-Reports-Console-Grouped")
    .IsDependentOn("Create-Reports-Console-Summaries")
    .IsDependentOn("Create-Reports-Console-Combined");
