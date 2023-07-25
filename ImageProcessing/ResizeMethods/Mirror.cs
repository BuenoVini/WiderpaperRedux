namespace Widerpaper.ResizeMethods
{
    internal static class Mirror
    {
        public static Image<Rgba32> Apply(Image<Rgba32> imgInput)
        {
            /* ALGORITHM FROM MY PREVIOUS PROJECT (https://github.com/BuenoVini/Widerpaper/) */

            Image<Rgba32> imgOutput = new(imgInput.Height * 21 / 9, imgInput.Height);

            /* an odd image has one extra column of pixels on the right side. so it must be treated separately */
            bool isOddImage = (imgOutput.Width - imgInput.Width) % 2 != 0;

            /* the quantity of pixels to be mirrored copied to the side of the input image */
            int qtyPixelsToCopy = (imgOutput.Width - imgInput.Width) / 2;

            /* making */
            for (int y = 0; y < imgInput.Height; y++)
            {
                for (int x = 0; x < imgInput.Width; x++)
                {
                    Rgba32 currentPixel = imgInput[x, y];

                    /* copy inside the main box */
                    imgOutput[x + qtyPixelsToCopy, y] = currentPixel;

                    /* copy mirrored outside the main box */
                    if (x < qtyPixelsToCopy)
                    {
                        /* left side of the main box */
                        imgOutput[qtyPixelsToCopy - x - 1, y] = currentPixel;
                    }
                    else if (x >= imgInput.Width - qtyPixelsToCopy)
                    {
                        /* right side of the main box */
                        imgOutput[imgOutput.Width - (x - imgInput.Width + qtyPixelsToCopy) - 1, y] = currentPixel;
                    }

                    /* fill missing pixel if this is an odd image */
                    if (isOddImage)
                        imgOutput[qtyPixelsToCopy + imgInput.Width, y] = imgInput[imgInput.Width - 1, y];
                }
            }

            return imgOutput;
        }
    }
}
