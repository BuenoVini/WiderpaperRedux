namespace Widerpaper;

public static class WiderpaperManager<TPixel> where TPixel : unmanaged, IPixel<TPixel>
{
    public static Image<TPixel> LoadImage(string path) => Image.Load<TPixel>(path);
    public static void SaveImage(Image img, string path) => img.Save(path);

    public static Image<TPixel> ApplyGaussianBlur(Image<TPixel> imgSource, int sigmaValue = 3) 
        => imgSource.Clone(img => img.GaussianBlur(sigmaValue));

    public static Image<TPixel> ApplyMirror(Image<TPixel> imgSource)
    {
        Image<TPixel> imgDest = new(imgSource.Height * 21 / 9, imgSource.Height);

        int qtyPixelsToCopy = (imgDest.Width - imgSource.Width) / 2;

        imgSource.ProcessPixelRows(imgDest, (src, dest) =>
        {
            for (int y = 0; y < src.Height; y++)
            {
                Span<TPixel> srcRow = src.GetRowSpan(y);
                Span<TPixel> destRow = dest.GetRowSpan(y);

                srcRow.CopyTo(destRow[qtyPixelsToCopy..^qtyPixelsToCopy]);

                Span<TPixel> leftSpan = srcRow[..qtyPixelsToCopy];
                leftSpan.Reverse();
                leftSpan.CopyTo(destRow[..qtyPixelsToCopy]);

                Span<TPixel> rightSpan = srcRow[^qtyPixelsToCopy..];
                rightSpan.Reverse();
                rightSpan.CopyTo(destRow[^qtyPixelsToCopy..]);
            }
        });

        return imgDest;
    }
}