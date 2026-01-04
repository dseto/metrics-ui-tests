namespace Metrics.UiTests.Support;

/// <summary>
/// Catálogo mínimo de mapeamentos entre termos do Gherkin (ex.: "Novo Conector")
/// e `data-testid` na UI.
///
/// IMPORTANTE: este catálogo deve virar contrato do frontend (spec + implementação),
/// para evitar seletores frágeis.
/// </summary>
public static class UiTestIdCatalog
{
    // Navegação
    public const string AppShell = "app-shell";
    public const string NavConnectors = "nav-connectors";
    public const string NavProcesses = "nav-processes";
    public const string NavAiAssistant = "nav-ai-assistant";

    // Auth
    public const string LoginUsername = "login-username";
    public const string LoginPassword = "login-password";
    public const string LoginSubmit = "login-submit";

    // Connectors
    public const string ConnectorsNew = "connectors-new";
    public const string ConnectorName = "connector-name";
    public const string ConnectorBaseUrl = "connector-baseUrl";
    public const string ConnectorAuthRef = "connector-authRef";
    public const string ConnectorTimeoutSeconds = "connector-timeoutSeconds";
    public const string ConnectorsSave = "connectors-save";
    public const string ConnectorsTable = "connectors-table";

    public static string? TryResolveButton(string label) => label switch
    {
        "Novo Conector" => ConnectorsNew,
        "Salvar" => ConnectorsSave,
        _ => null
    };

    public static string? TryResolveScreenNav(string screen) => screen switch
    {
        "Conectores" => NavConnectors,
        "Processos" => NavProcesses,
        "AI Assistant" => NavAiAssistant,
        _ => null
    };
}
