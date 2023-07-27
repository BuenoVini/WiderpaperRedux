using SixLabors.ImageSharp.Processing;
using TPixel = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace Widerpaper;

public static class WiderpaperProcessing
{
    public static WiderpaperImage ApplyBlur(WiderpaperImage imgSource, int sigmaValue = 3)
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

    public static WiderpaperImage ApplyBlurMirror(WiderpaperImage imgSource, int blurStrenght)
    {
        WiderpaperImage imgDest = ApplyBlur(imgSource, blurStrenght);

        imgDest = ApplyMirror(imgDest);

        imgDest._image.Mutate(img =>
        {
            img.DrawImage(imgSource._image, new Point((imgDest._image.Width - imgSource._image.Width) / 2, 0), 1f);
        });

        return imgDest;
    }

    public static WiderpaperImage ApplyGradientBlurMirror(WiderpaperImage imgSource, int blurStrenght)
    {
        Image<TPixel> imgTemp = imgSource._image.Clone();

        WiderpaperImage imgDest = ApplyBlur(imgSource, blurStrenght);

        imgTemp.ProcessPixelRows(img =>
        {
            for (int y = 0; y < img.Height; y++)
            {
                Span<TPixel> row = img.GetRowSpan(y);

                for (int x = 0; x < row.Length; x++)
                {
                    ref TPixel pixel = ref row[x];
                    float distanceFromCenter = Math.Abs(1 - (float)x / (row.Length / 2));
                    double sigmoidCurve = 1 / (1 + Math.Pow(Math.E, -50 * distanceFromCenter + 45));

                    pixel.A = (byte)(255 * (1 - sigmoidCurve));
                }
            }
        });

        imgDest = ApplyMirror(imgDest);

        imgDest._image.Mutate(img =>
        {
            img.DrawImage(imgTemp, new Point((imgDest._image.Width - imgSource._image.Width) / 2, 0), 1f);
        });

        return imgDest;
    }
}