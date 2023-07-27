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
WiderpaperImage imgInput = new (fileChosen);
WiderpaperImage imgOutput;

Console.WriteLine("[0] Apply Mirror\n[1] Apply Blur\n[2] Apply Mirror with Blur\n[3] Apply Mirror with Gradient Blur");

Console.Write("\nSelect algorithm: ");
int algorithmChosen = int.Parse(Console.ReadLine());

Console.WriteLine("\nProcessing...");

Stopwatch stopwatch = new();
switch (algorithmChosen)
{
    case 0: 
        imgOutput = WiderpaperProcessing.ApplyMirror(imgInput);
        break;

    case 1: 
        imgOutput = WiderpaperProcessing.ApplyBlur(imgInput);
        break;

    case 2:
        imgOutput = WiderpaperProcessing.ApplyBlurMirror(imgInput, 50);
        break;

    case 3:
        imgOutput = WiderpaperProcessing.ApplyGradientBlurMirror(imgInput, 50);
        break;

    default:
        imgOutput = WiderpaperImage.LoadImage("");
        break;
}

Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");

imgOutput.SaveImage(IMAGE_DIR_PATH + "output.jpg");
Console.WriteLine("Image successfully resized!");
#endregion