﻿// See https://aka.ms/new-console-template for more information

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

WiderpaperManager.LoadImage(fileChosen);

Console.WriteLine("[0] Apply Mirror\n[1] Apply Mean Blur");

Console.Write("\nSelect algorithm: ");
int algorithmChosen = int.Parse(Console.ReadLine());

Console.WriteLine("\nProcessing...");

switch (algorithmChosen)
{
    case 0: WiderpaperManager.ApplyMirror(); break;
    case 1: WiderpaperManager.ApplyMeanBlur(); break;
}

WiderpaperManager.SaveImage(IMAGE_DIR_PATH + "output.jpg");
Console.WriteLine("Image successfully resized!");
#endregion