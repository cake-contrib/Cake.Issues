// Opens external links and links to downloadable files in a new tab.
// Based on mkdocs-open-in-new-tab (https://github.com/JakubAndrysek/mkdocs-open-in-new-tab),
// Copyright (c) 2023 Kuba Andrýsek, MIT License, which is not supported by Zensical.
const downloadExtensions = [
  ".pdf", ".doc", ".docx", ".json", ".xls", ".xlsx", ".ppt", ".pptx",
  ".zip", ".rar", ".tar", ".gz", ".7z", ".bz2", ".xz", ".tgz", ".tar.gz"
]

document$.subscribe(() => {
  for (const link of document.getElementsByTagName("a")) {
    if (link.getAttribute("href") && link.host !== location.host) {
      link.target = "_blank"
      link.rel = "noopener"
    }

    const href = link.href.toLowerCase()
    if (downloadExtensions.some(extension => href.endsWith(extension))) {
      link.onclick = function () {
        window.open(this.href)
        return false
      }
    }
  }
})
