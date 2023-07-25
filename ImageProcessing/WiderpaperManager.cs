using Widerpaper.ResizeMethods;

namespace Widerpaper;

public static class WiderpaperManager
{
	private static Image<Rgba32>? _imgInput;
	private static Image<Rgba32>? _imgOutput;

    public static void LoadImage(string imgPath) => _imgInput = Image.Load<Rgba32>(imgPath);
	public static void SaveImage(string imgPath)
	{
		if (_imgOutput is null)
			throw new NullReferenceException("No image was resized yet to be saved.");

		_imgOutput.Save(imgPath);
	}

	public static void ApplyMirror()
	{
		if (_imgInput is null)
			throw new NullReferenceException("No image was loaded to memory.");

        /* ALGORITHM FROM MY PREVIOUS PROJECT (https://github.com/BuenoVini/Widerpaper/) */

        _imgOutput = new(_imgInput.Height * 21 / 9, _imgInput.Height);

        /* an odd image has one extra column of pixels on the right side. so it must be treated separately */
        bool isOddImage = (_imgOutput.Width - _imgInput.Width) % 2 != 0;

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
}