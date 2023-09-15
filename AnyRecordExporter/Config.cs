namespace AnyRecordExporter;

internal class Config
{
    public string Path { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public string Plugin { get; set; } = "Skyrim.esm";
    public bool IgnoreBookText { get; set; } = false;
}