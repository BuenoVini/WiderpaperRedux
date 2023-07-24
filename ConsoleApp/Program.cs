// See https://aka.ms/new-console-template for more information

using ImageProcessing;

const string IMAGE_DIR_PATH = "C:\\Users\\vinic\\OneDrive\\Imagens\\Saved Pictures\\";

string[] files = Directory.GetFiles(IMAGE_DIR_PATH);

for (int i = 0; i < files.Length; i++)
    Console.WriteLine($"[{i}] {files[i].Replace(IMAGE_DIR_PATH, "")}");

Console.Write("\nFile #: ");
string fileChosen = files[int.Parse(Console.ReadLine())];

ImageManager imageManager = new(fileChosen);