# Metrics UI Tests (Selenium + xUnit + Reqnroll)

Este projeto é o **skeleton** para automatizar os cenários Gherkin definidos no spec deck
(`specs/frontend/09-testing/gherkin/*.feature`).

## Pré-requisitos

- .NET SDK 8+
- Browser instalado (Chrome/Edge/Firefox)
- App rodando localmente (ex.: `http://localhost:5173`)

## Configuração

Você pode configurar via **appsettings.json** ou variáveis de ambiente:

- `UI_BASE_URL` (ex.: `http://localhost:5173`)
- `UI_BROWSER` (`chrome` | `edge` | `firefox`)
- `UI_HEADLESS` (`true` | `false`)
- `SELENIUM_GRID_URL` (opcional, ex.: `http://localhost:4444/wd/hub`)
- `ADMIN_USERNAME` / `ADMIN_PASSWORD`

> Observação: as feature files do spec deck usam `"ADMIN_USERNAME"` e `"ADMIN_PASSWORD"` como placeholders.

## Executar

```bash
cd tests/ui
dotnet test Metrics.UiTests.sln
```

## Estrutura

- `Features/` arquivos `.feature` (exemplos). Em um setup real, copie/sincronize a pasta de Gherkin do spec deck.
- `Steps/` step definitions em português (pt-BR)
- `Pages/` Page Objects
- `Hooks/` lifecycle do WebDriver + screenshots
- `Support/` utilitários e catálogo de `data-testid`

## Próximo passo recomendado

Implementar/alinhar um catálogo de `data-testid` no frontend para evitar seletores frágeis (CSS/XPath).
