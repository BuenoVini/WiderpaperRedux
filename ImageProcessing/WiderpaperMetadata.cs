namespace Widerpaper;

public class WiderpaperMetadata
{
    public string Path { get; }
    public int Width { get; }
    public int Height { get; }
    public long Size { get; }

    public WiderpaperMetadata(string path, long size)
    {
        Path = path;
        Size = size;

        using Image image = Image.Load(path);
        Width = image.Width;
        Height = image.Height;
    }
}