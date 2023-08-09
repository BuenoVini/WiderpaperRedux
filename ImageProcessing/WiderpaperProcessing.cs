using TPixel = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace Widerpaper;

public static class WiderpaperProcessing
{
    const string EX_MSG_NO_IMAGE_LOADED = "No image was loaded in memory.\nTry using WiderpaperImage.LoadImage(path).";
    const string EX_MSG_IMAGE_DEST = "An exception ocurred when processing the Image Source provided.";

    /// <summary>
    /// Applies Gaussian Blur to a copy of Source Image and then returns the new blurred image.
    /// </summary>
    public static WiderpaperImage ApplyGaussianBlur(WiderpaperImage imgSource, int sigmaValue = 3)
    {
        if (imgSource._image is null)
            throw new NullReferenceException(EX_MSG_NO_IMAGE_LOADED);

        return new (imgSource._image.Clone(img => img.GaussianBlur(sigmaValue)));
    }


    /// <summary>
    /// Mirror both sides of a copy of Source Image and then returns the new image in Ultrawide aspect ratio.
    /// </summary>
    public static WiderpaperImage ApplyMirror(WiderpaperImage imgSource)
    {
        if (imgSource._image is null)
            throw new NullReferenceException(EX_MSG_NO_IMAGE_LOADED);

        Image<TPixel> imgSrc = imgSource._image;
        Image<TPixel> imgDest = new (imgSrc.Height * 21 / 9, imgSrc.Height);

        int qtyPixelsToCopy = (imgDest.Width - imgSrc.Width) / 2;

        imgSrc.ProcessPixelRows(imgDest, (src, dest) =>
        {
            for (int y = 0; y < src.Height; y++)
            {
                Span<TPixel> srcRow = src.GetRowSpan(y);
                Span<TPixel> destRow = dest.GetRowSpan(y);

                /* Copying the original Source Image to the new resize Destination Image, leaving the both sides empty */
                srcRow.CopyTo(destRow[qtyPixelsToCopy..^qtyPixelsToCopy]);

                /* Copying the left side of Source Image reversed to Destination Image */
                Span<TPixel> leftSpan = srcRow[..qtyPixelsToCopy];
                leftSpan.Reverse();
                leftSpan.CopyTo(destRow[..qtyPixelsToCopy]);

                /* Copying the right side of Source Image reversed to Destination Image */
                Span<TPixel> rightSpan = srcRow[^(qtyPixelsToCopy + 1)..];
                rightSpan.Reverse();
                rightSpan.CopyTo(destRow[^(qtyPixelsToCopy + 1)..]);
            }
        });

        return new WiderpaperImage(imgDest);
    }


    /// <summary>
    /// Mirror both sides of a copy of Source Image blurred.
    /// Returns the new image in Ultrawide aspect ratio.
    /// </summary>
    public static WiderpaperImage ApplyBlurMirror(WiderpaperImage imgSource, int blurStrenght)
    {
        if (imgSource._image is null)
            throw new NullReferenceException(EX_MSG_NO_IMAGE_LOADED);

        /* Applying Gaussian Blur to a copy of Image Source */
        WiderpaperImage imgDest = ApplyGaussianBlur(imgSource, blurStrenght);

        /* Mirroring the copy of Image Source blurred to Ultrawide */
        imgDest = ApplyMirror(imgDest);

        if (imgDest._image is null)
            throw new NullReferenceException(EX_MSG_IMAGE_DEST);

        /* Copying original, untouched, Image Source to the blurred Ultrawide image */
        imgDest._image.Mutate(img =>
        {
            img.DrawImage(imgSource._image, new Point((imgDest._image.Width - imgSource._image.Width) / 2, 0), 1f);
        });

        return imgDest;
    }


    /// <summary>
    /// Mirror both sides of a copy of Source Image, blur it, and then apply gradient to its vertival edges. 
    /// Returns the new image in Ultrawide aspect ratio.
    /// </summary>
    public static WiderpaperImage ApplyGradientBlurMirror(WiderpaperImage imgSource, int blurStrenght)
    {
        if (imgSource._image is null)
            throw new NullReferenceException(EX_MSG_NO_IMAGE_LOADED);

        /* Applying Gaussian Blur to a copy of Image Source */
        WiderpaperImage imgDest = ApplyGaussianBlur(imgSource, blurStrenght);

        /* Mirroring the copy of Image Source blurred to Ultrawide */
        imgDest = ApplyMirror(imgDest);

        if (imgDest._image is null)
            throw new NullReferenceException(EX_MSG_IMAGE_DEST);

        /* Creating a copy of Image Source untouched to fade its vertical edges */
        Image<TPixel> imgFaded = imgSource._image.Clone();
        imgFaded.ProcessPixelRows(img =>
        {
            for (int y = 0; y < img.Height; y++)
            {
                Span<TPixel> row = img.GetRowSpan(y);

                for (int x = 0; x < row.Length; x++)
                {
                    /* The further away the current pixel is from the center, the more transparent it will be */
                    ref TPixel pixel = ref row[x];
                    float distanceFromCenter = Math.Abs(1 - (float)x / (row.Length / 2));
                    double sigmoidCurve = 1 / (1 + Math.Pow(Math.E, -50 * distanceFromCenter + 45));

                    pixel.A = (byte)(255 * (1 - sigmoidCurve));
                }
            }
        });

        /* Copying the faded image to the blurred Ultrawide copy */
        imgDest._image.Mutate(img =>
        {
            img.DrawImage(imgFaded, new Point((imgDest._image.Width - imgSource._image.Width) / 2, 0), 1f);
        });

        return imgDest;
    }
}