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

Image<Rgba32> imgInput = WiderpaperManager<Rgba32>.LoadImage(fileChosen);
Image<Rgba32> imgOutput;

Console.WriteLine("[0] Apply Mirror\n[1] Apply Mean Blur");

Console.Write("\nSelect algorithm: ");
int algorithmChosen = int.Parse(Console.ReadLine());

Console.WriteLine("\nProcessing...");

Stopwatch stopwatch = new();
switch (algorithmChosen)
{
    case 0: imgOutput = WiderpaperManager<Rgba32>.ApplyMirror(imgInput); break;
    case 1: 
        imgOutput = WiderpaperManager<Rgba32>.ApplyMirror(imgInput);
        imgOutput = WiderpaperManager<Rgba32>.ApplyGaussianBlur(imgOutput, sigmaValue: 15);
        break;

    default: imgOutput = new Image<Rgba32>(500, 500); break;
}

Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");

WiderpaperManager<Rgba32>.SaveImage(imgOutput, IMAGE_DIR_PATH + "output.jpg");
Console.WriteLine("Image successfully resized!");
#endregion