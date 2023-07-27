using TPixel = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace Widerpaper;

public static class WiderpaperProcessing
{
    public static WiderpaperImage ApplyGaussianBlur(WiderpaperImage imgSource, int sigmaValue = 3)
        => new (imgSource._image.Clone(img => img.GaussianBlur(sigmaValue)));

    public static WiderpaperImage ApplyMirror(WiderpaperImage imgSource)
    {
        Image<TPixel> imgSrc = imgSource._image;
        Image<TPixel> imgDest = new (imgSrc.Height * 21 / 9, imgSrc.Height);

        int qtyPixelsToCopy = (imgDest.Width - imgSrc.Width) / 2;

        imgSrc.ProcessPixelRows(imgDest, (src, dest) =>
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

        return new WiderpaperImage(imgDest);
    }
}