using TPixel = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace Widerpaper;

public class WiderpaperImage
{
    internal Image<TPixel> _image;

    public WiderpaperImage(string path) => _image = Image.Load<TPixel>(path);
    internal WiderpaperImage(Image<TPixel> img) => _image = img;

    public static WiderpaperImage LoadImage(string path) => new (Image.Load<TPixel>(path));
    public void SaveImage(string path) => _image.Save(path);
}
