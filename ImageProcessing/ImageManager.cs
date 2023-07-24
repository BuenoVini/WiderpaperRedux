namespace ImageProcessing;

public class ImageManager
{
    public ImageManager(string imagePath)
    {
		try
		{
			Image<Rgba32> image = Image.Load<Rgba32>(imagePath);
			Console.WriteLine(">> DEBUG: image loaded");
		}
		catch (UnknownImageFormatException _)
		{
            Console.WriteLine(">> DEBUG: image NOT loaded");
		}
    }
}