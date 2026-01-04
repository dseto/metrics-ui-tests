# Delta Pack — Frontend — UI Automated Tests (Selenium + xUnit + Reqnroll)

Data: 2026-01-03

## Contexto (source of truth)

O spec deck já define **E2E Gherkin** para o frontend em `specs/frontend/09-testing/gherkin/*` (ex.: `core-flow.feature`, `03-connectors.feature`, `04-processes.feature`, `05-process-versions.feature`, `06-preview.feature`).

Este delta adiciona o **contrato implementável** para executar esses cenários como testes de UI automatizados.

## Decisão de arquitetura (repo/projeto)

- **Dentro do mesmo repo do produto (recomendado)**:
  - pasta `tests/ui/` no root do repo do produto
  - solução .NET separada (sem misturar com o build do frontend)
  - vantagens: versionamento junto do frontend + enforcement de `data-testid` e rotas
- **Repo separado**: permitido, mas só depois que os contratos (`data-testid`, env vars, tags) estiverem maduros.

## Arquivos adicionados (spec deck)

Adicionar:

- `specs/frontend/09-testing/ui-automation/README.md`
- `specs/frontend/09-testing/ui-automation/ui-testid-catalog.md`
- `specs/frontend/09-testing/ui-automation/step-catalog.md`
- `specs/frontend/09-testing/ui-automation/execution-contract.md`

## Arquivos adicionados (codebase do produto)

Adicionar skeleton de projeto:

- `tests/ui/Metrics.UiTests.sln`
- `tests/ui/Metrics.UiTests/*`

## Critérios de aceite

- [ ] `dotnet test tests/ui/Metrics.UiTests.sln` roda localmente e em CI (headless).
- [ ] Config via env vars: `UI_BASE_URL`, `UI_BROWSER`, `UI_HEADLESS`, `SELENIUM_GRID_URL`, `ADMIN_USERNAME`, `ADMIN_PASSWORD`.
- [ ] Seletores de UI priorizam `data-testid` (sem CSS/XPath como contrato).
- [ ] Existe catálogo versionado de `data-testid` para telas/ações cobertas pelos Gherkins.
