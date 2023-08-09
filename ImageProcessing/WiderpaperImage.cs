using TPixel = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace Widerpaper;

public class WiderpaperImage : IDisposable
{
    internal Image<TPixel>? _image;

    public WiderpaperImage(string path) => _image = Load(path);
    public WiderpaperImage() => _image = null;
    internal WiderpaperImage(Image<TPixel> img) => _image = img;

    private Image<TPixel> Load(string path)
    {
        try
        {
            return Image.Load<TPixel>(path);
        }
        catch (UnknownImageFormatException e)
        {
            throw new WiderpaperException(e.Message);
        }
    }

    public WiderpaperImage LoadImage(string path) => new(Load(path));
    public void SaveImage(string path) => _image?.Save(path);

    public void Dispose() => _image?.Dispose();
}
