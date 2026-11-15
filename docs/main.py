"""Macros available in the documentation pages."""


def define_env(env):
    @env.macro
    def badge_commercial():
        """Creates a badge marking a feature which requires a commercial license."""
        icon = (
            "[:material-currency-usd: Commercial]"
            "(#using-commercial-version 'Requires a commercial license')"
        )
        return (
            '<span class="mdx-badge">'
            f'<span class="mdx-badge__icon">{icon}</span>'
            "</span>"
        )
