import re

from mkdocs.config.defaults import MkDocsConfig
from mkdocs.structure.files import Files
from mkdocs.structure.pages import Page
from re import Match

# Hook to replace shortcodes in Markdown
def on_page_markdown(
    markdown: str, *, page: Page, config: MkDocsConfig, files: Files
):
    def replace(match: Match):
        type, args = match.groups()
        args = args.strip()
        if type == "badge": return badge(args, page, files)
        raise RuntimeError(f"Unknown shortcode: {type}")

    return re.sub(
        r"<!-- md:(\w+)(.*?) -->",
        replace, markdown, flags = re.I | re.M
    )

# Creates a badge
def badge(args: str, page: Page, files: Files):
    type, *_ = args.split(" ", 1)
    if type == "commercial": return _badge_commercial(page, files)
    raise RuntimeError(f"Unknown type: {type}")

# Create commercial badge
def _badge_commercial(page: Page, files: Files):
    icon = "material-currency-usd"
    href = "#using-commercial-version"
    return _badge(
        icon = f"[:{icon}: Commercial]({href} 'Requires a commercial license')"
    )

# Create badge
def _badge(icon: str, text: str = "", type: str = ""):
    classes = f"mdx-badge mdx-badge--{type}" if type else "mdx-badge"
    return "".join([
        f"<span class=\"{classes}\">",
        *([f"<span class=\"mdx-badge__icon\">{icon}</span>"] if icon else []),
        *([f"<span class=\"mdx-badge__text\">{text}</span>"] if text else []),
        f"</span>",
    ])