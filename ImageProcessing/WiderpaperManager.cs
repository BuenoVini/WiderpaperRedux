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

        _imgOutput = new(_imgInput!.Height * 21 / 9, _imgInput.Height);

        int qtyPixelsToCopy = (_imgOutput.Width - _imgInput.Width) / 2;

        _imgInput.ProcessPixelRows(_imgOutput, (input, output) =>
        {
            for (int y = 0; y < input.Height; y++)
            {
                Span<Rgba32> inputRow = input.GetRowSpan(y);
                Span<Rgba32> outputRow = output.GetRowSpan(y);

                inputRow.CopyTo(outputRow[qtyPixelsToCopy..^qtyPixelsToCopy]);

                Span<Rgba32> leftSpan = inputRow[..qtyPixelsToCopy];
                leftSpan.Reverse();
                leftSpan.CopyTo(outputRow[..qtyPixelsToCopy]);

                Span<Rgba32> rightSpan = inputRow[^qtyPixelsToCopy..];
                rightSpan.Reverse();
                rightSpan.CopyTo(outputRow[^qtyPixelsToCopy..]);
            }
        });
    }

    public static void ApplyGaussianBlur(int sigmaValue = 3)
    {
        ValidateImageInput();

       _imgOutput = _imgInput!.Clone(x => x.GaussianBlur(sigmaValue));
    }
    #endregion
}