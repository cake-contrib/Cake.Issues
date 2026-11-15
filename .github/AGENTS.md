# GitHub configuration

Follow [the shared instructions](../AGENTS.md). Copilot-specific instructions
remain in [copilot-instructions.md](copilot-instructions.md); shared skills live
in [.agents/skills](../.agents/skills).

## Workflows

- Use the `update-ci-pipelines` skill for GitHub Actions changes.
- Reuse existing local actions under `actions` instead of duplicating setup.
- Keep `CiStatus.md` synchronized with pipeline jobs, matrices, and status badges.
- Agentic workflow sources under `workflows/*.md` are compiled into matching
  `.lock.yml` files. Edit the Markdown source and regenerate the lock file with
  `gh aw compile <workflow-id>`. Do not hand-edit generated lock files.
- Preserve pinned action and container versions when regenerating workflows;
  a compiler upgrade is a separate change.
- Workflow prompts should read root and applicable nested `AGENTS.md` files for
  shared guidance, rather than relying on the Copilot-only instruction file.
