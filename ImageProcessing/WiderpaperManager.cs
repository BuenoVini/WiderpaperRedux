namespace Widerpaper;

public static class WiderpaperManager
{
    private static Image<Rgba32>? _imgInput;
    private static Image<Rgba32>? _imgOutput;

    #region Private Methods
    private static void ValidateImageInput()
    {
        if (_imgInput is null)
            throw new NullReferenceException("No image was loaded to memory.");
    }

    private static void ValidateImageOutput()
    {
        if (_imgOutput is null)
            throw new NullReferenceException("No image was resized yet to be saved.");
    }
    #endregion

    #region Public Methods
    public static void LoadImage(string imgPath) => _imgInput = Image.Load<Rgba32>(imgPath);
    public static void SaveImage(string imgPath)
    {
        ValidateImageOutput();

        _imgOutput!.Save(imgPath);
    }

    public static void ApplyMirror()
    {
        ValidateImageInput();

        /* ALGORITHM FROM MY PREVIOUS PROJECT (https://github.com/BuenoVini/Widerpaper/) */

        _imgOutput = new(_imgInput!.Height * 21 / 9, _imgInput.Height);

        /* an odd image has one extra column of pixels on the right side. so it must be treated separately */
        bool isOddImage = (_imgOutput.Width - _imgInput!.Width) % 2 != 0;

        /* the quantity of pixels to be mirrored copied to the side of the input image */
        int qtyPixelsToCopy = (_imgOutput.Width - _imgInput.Width) / 2;

        /* making */
        for (int y = 0; y < _imgInput.Height; y++)
        {
            for (int x = 0; x < _imgInput.Width; x++)
            {
                Rgba32 currentPixel = _imgInput[x, y];

                /* copy inside the main box */
                _imgOutput[x + qtyPixelsToCopy, y] = currentPixel;

                /* copy mirrored outside the main box */
                if (x < qtyPixelsToCopy)
                {
                    /* left side of the main box */
                    _imgOutput[qtyPixelsToCopy - x - 1, y] = currentPixel;
                }
                else if (x >= _imgInput.Width - qtyPixelsToCopy)
                {
                    /* right side of the main box */
                    _imgOutput[_imgOutput.Width - (x - _imgInput.Width + qtyPixelsToCopy) - 1, y] = currentPixel;
                }

                /* fill missing pixel if this is an odd image */
                if (isOddImage)
                    _imgOutput[qtyPixelsToCopy + _imgInput.Width, y] = _imgInput[_imgInput.Width - 1, y];
            }
        }
    }

    public static void ApplyMeanBlur(int kernelSize = 3)
    {
        ValidateImageInput();

        _imgOutput = new(_imgInput!.Width, _imgInput.Height);

        /* creating the convolution matrix */
        int[,] kernel = new int[kernelSize, kernelSize];
        int kernelNormalizationValue = 0;
        for (int i = 0; i < kernelSize; i++)
        {
            for (int j = 0; j < kernelSize; j++)
            {
                kernel[i, j] = 1;
                kernelNormalizationValue += kernel[i, j];
            }
        }

        /* applying the kernel to the entire image */
        //for (int y = 0; y < _imgInput.Height; y++)
        Parallel.For(0, _imgInput.Height, y =>
        {
            for (int x = 0; x < _imgInput.Width; x++)
            {
                int sumR = 0, sumG = 0, sumB = 0;

                for (int i = 0; i < kernelSize; i++)
                {
                    for (int j = 0; j < kernelSize; j++)
                    {
                        int relativeX = x + j - (kernelSize - 1) / 2;
                        int relativeY = y + i - (kernelSize - 1) / 2;

                        if (relativeX < 0 || relativeY < 0 || relativeX >= _imgInput.Width || relativeY >= _imgInput.Height)
                        {
                            sumR += 0;
                            sumG += 0;
                            sumB += 0;
                        }
                        else
                        {
                            sumR += _imgInput[relativeX, relativeY].R * kernel[i, j];
                            sumG += _imgInput[relativeX, relativeY].G * kernel[i, j];
                            sumB += _imgInput[relativeX, relativeY].B * kernel[i, j];
                        }
                    }
                }

                sumR /= kernelNormalizationValue;
                sumG /= kernelNormalizationValue;
                sumB /= kernelNormalizationValue;

                _imgOutput[x, y] = new Rgba32((byte)sumR, (byte)sumG, (byte)sumB);
            }
        });
    }
    #endregion
}