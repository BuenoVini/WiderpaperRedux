namespace Widerpaper;

public class WiderpaperMetadata
{
    public enum ProcessingState { Loaded, Processing, Done };
    public ProcessingState State { get; set; }
    
    public string Path { get; }
    public string PathThumbnail { get; }
    public string Extension { get; }
    public int Width { get; }
    public int Height { get; }
    public long Size { get; }

    public WiderpaperMetadata(string path, long size)
    {
        // TODO: perform file signature validation
        
        Path = path;
        Size = size;
        Extension = path.Split('.')[1];

        using Image image = Image.Load(path);
        Width = image.Width;
        Height = image.Height;

        image.Mutate(img => img.Resize(Width * 150 / Height, 150));
        PathThumbnail = path.Split('.')[0] + "-thumbnail.webp";
        image.SaveAsWebp(PathThumbnail);

        State = ProcessingState.Loaded;
    }
}