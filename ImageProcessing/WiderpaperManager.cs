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

        _imgOutput = Mirror.Apply(_imgInput);
    }
}