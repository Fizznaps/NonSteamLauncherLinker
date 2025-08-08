public class LauncherDetails
{
    public string LauncherPath { get; set; }
    public string LauncherURIPrefix { get; set; }
    public string GameInstallPath { get; set; }
    public bool UseUri { get; set; } = false;  // default false if missing
}

public class LauncherInfoManager
{
    private const string LauncherInfoFileName = "launcherinfo.json";
    public Dictionary<string, LauncherDetails> Launchers { get; private set; }

    public LauncherInfoManager(string basePath)
    {
        LoadLauncherInfo(basePath);
    }

    private void LoadLauncherInfo(string basePath)
    {
        string jsonPath = Path.Combine(basePath, LauncherInfoFileName);
        if (!File.Exists(jsonPath))
            throw new FileNotFoundException($"Launcher info file not found at {jsonPath}");

        string json = File.ReadAllText(jsonPath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        Launchers = JsonSerializer.Deserialize<Dictionary<string, LauncherDetails>>(json, options)
            ?? new Dictionary<string, LauncherDetails>();
    }

    public LauncherDetails? GetLauncherDetails(string launcherKey)
    {
        if (string.IsNullOrEmpty(launcherKey))
            return null;
        Launchers.TryGetValue(launcherKey, out var details);
        return details;
    }
}
