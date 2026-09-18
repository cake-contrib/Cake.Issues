---
name: write-release-notes
description: Write a Cake Issues release-note news post. Use this when asked to draft release notes or add a release post for this repository.
---

Use this skill when creating a release-note post for the Cake Issues website.

See [copilot-instructions](../../copilot-instructions) for more details about structure of documentation and tooling used.

## Source material

Gather the release content from these sources:

1. The fixed issues in the GitHub milestone for the release.
2. The GitHub release notes at https://github.com/cake-contrib/Cake.Issues/releases when a release already exists.
3. Recent examples in `/docs/input/news/posts`, especially:
   - `2025-09-25-cake-issues-v5.9.1-released.md`
   - `2025-09-24-cake-issues-v5.9.0-released.md`
   - `2025-08-22-cake-issues-v5.8.0-released.md`

Prefer the published GitHub release notes when they exist, and use the milestone to fill gaps or confirm the scope of the release.

## Expected output

Create a new Markdown post in `/docs/input/news/posts` using this filename pattern:

`YYYY-MM-DD-cake-issues-vX.Y.Z-released.md`

Match the established structure used by other release-note posts:

1. YAML frontmatter with:
   - `title`
   - `date`
   - `categories` including `Release Notes`
   - `links` for the most relevant documentation pages affected by the release
2. Short opening summary naming the release and its main highlights.
3. `<!-- more -->`
4. Introductory text with a pointer to the update instructions section.
5. Community thank-you text.
6. `People working on this release:` followed by GitHub profile links.
7. `People reporting issues:` followed by GitHub profile links, when identifiable from the issues included in the release.
8. Highlight sections grouped by feature area, addin, or theme.
9. `## Updating from previous versions`
10. A final link to the GitHub release notes.

## Writing guidelines

- Follow the tone and level of detail of the recent release posts.
- Summarize the user-facing improvements instead of copying issue titles verbatim.
- Group related fixes together under clear headings when the release contains multiple changes.
- Keep the post factual and concise.
- Mention the affected addin names when relevant.
- For architectural or structural changes, explain the motivation
- Keep each major highlight in its own heading; do not merge unrelated highlights into the same section.
- When calling out support for a tool or format version, explicitly mention which addin gained that support.
- Use the release version consistently in the title, summary, update guidance, and final link.
- If a release does not yet exist, build the post from the milestone and omit claims that cannot be verified elsewhere.
- If the release includes an exceptional compatibility break, minimum supported version change, or similar upgrade risk, present that warning as a MkDocs Material admonition of type `danger` with the title `Breaking Change`.

If the milestone, release, or target version is unclear, ask for the missing detail before drafting the post.

## Pull request guidelines

- Use `Add x.y.z release news post` as the pull request title, where `x.y.z` is the version of the release being added.
