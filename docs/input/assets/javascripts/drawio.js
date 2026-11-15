// Renders images referencing .drawio files as interactive diagrams using the
// diagrams.net viewer. Replaces the mkdocs-drawio plugin, which is not yet
// supported by Zensical (https://github.com/zensical/zensical/issues/73).
// Configuration matches what mkdocs-drawio rendered with `edit: false`.
// mkdocs-drawio: Copyright 2024 Sergey Lukin, MIT License.
// See assets/THIRD-PARTY-NOTICES.txt for the full license text.
const drawioConfig = {
  "toolbar-position": "bottom",
  "toolbar-nohide": "0",
  "tooltips": "1",
  "border": 5,
  "resize": "1",
  "lightbox": "1",
  "toolbar": "pages tags zoom layers lightbox"
}

async function renderDrawioDiagram(image) {
  // Image might be wrapped in a lightbox link, which needs to be replaced as well.
  const element = image.closest("a.glightbox") ?? image
  const response = await fetch(image.src)
  if (!response.ok) {
    console.error(`Could not load diagram ${image.src}: ${response.status}`)
    return
  }

  const container = document.createElement("div")
  container.className = "mxgraph"
  container.style.maxWidth = "100%"
  container.style.border = "1px solid transparent"
  container.dataset.mxgraph = JSON.stringify({
    ...drawioConfig,
    xml: await response.text()
  })
  element.replaceWith(container)
}

document$.subscribe(async () => {
  const images = document.querySelectorAll("img[src$='.drawio' i]")
  if (images.length === 0)
    return

  const results = await Promise.allSettled(Array.from(images, renderDrawioDiagram))
  for (const result of results) {
    if (result.status === "rejected")
      console.error("Could not render diagram", result.reason)
  }
  GraphViewer.processElements()
})
