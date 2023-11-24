namespace Widerpaper;

public class WiderpaperMetadata
{
    public enum ProcessingState { Loaded, Processing, Done };
    public ProcessingState State { get; set; }
    
    public string Path { get; }
    public string Extension { get; }
    public int Width { get; }
    public int Height { get; }
    public long Size { get; }

    public WiderpaperMetadata(string path, long size)
    {
        Path = path;
        Size = size;
        
        // TODO: perform file signature validation

        using Image image = Image.Load(path);
        Width = image.Width;
        Height = image.Height;

        Extension = path.Split('.')[1];

        State = ProcessingState.Loaded;
    }
}