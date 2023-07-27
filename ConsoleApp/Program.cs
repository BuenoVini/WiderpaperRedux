// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
using Widerpaper;

#region Selecting Image
const string IMAGE_DIR_PATH = "C:\\Users\\vinic\\OneDrive\\Imagens\\Saved Pictures\\";

string[] files = Directory.GetFiles(IMAGE_DIR_PATH);

for (int i = 0; i < files.Length; i++)
    Console.WriteLine($"[{i}] {files[i].Replace(IMAGE_DIR_PATH, "")}");

Console.Write("\nSelect file #: ");
string fileChosen = files[int.Parse(Console.ReadLine())];
#endregion

Console.Clear();

#region Selecting Resize Algorithm
using WiderpaperImage imgInput = new (fileChosen);
WiderpaperImage imgOutput;

Console.WriteLine("[0] Apply Mirror\n[1] Apply Blur\n[2] Apply Mirror with Blur\n[3] Apply Mirror with Gradient Blur");

Console.Write("\nSelect algorithm: ");
int algorithmChosen = int.Parse(Console.ReadLine());

Console.WriteLine("\nProcessing...");

using (imgOutput = new())
{
    imgOutput = algorithmChosen switch
    {
        0 => WiderpaperProcessing.ApplyMirror(imgInput),
        1 => WiderpaperProcessing.ApplyBlur(imgInput),
        2 => WiderpaperProcessing.ApplyBlurMirror(imgInput, 50),
        3 => WiderpaperProcessing.ApplyGradientBlurMirror(imgInput, 50),
        _ => WiderpaperImage.LoadImage(""),
    };
}

imgOutput.SaveImage(IMAGE_DIR_PATH + "output.jpg");
Console.WriteLine("Image successfully resized!");
#endregion