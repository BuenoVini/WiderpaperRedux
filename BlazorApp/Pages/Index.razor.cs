using BlazorApp.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Widerpaper;

namespace BlazorApp.Pages;

public partial class Index
{
    #region Private Fields
    private Toast _toastFinishedProcessing;
    private Toast _toastUnselectedFile;
    private Toast _toastUnsupportedFile;
    private Toast _toastFileTooLarge;

    private enum Algorithm { None = -1, SimpleMirror, BlurMirror }
    private Algorithm _algorithmChosen = Algorithm.SimpleMirror;

    //private string _inputFilePath; /* BUG in MAUI Blazor App when using LocalApplicationData. See: https://github.com/dotnet/runtime/issues/74884 */
    private string _inputFileName;
    private string _previousOuputFileName;
    private int _blurStrenght = 25;

    private bool _shouldBlurTransition = true;
    private bool _isProcessingImage = false;
    #endregion


    #region Util Methods
    private string GetWiderpaperFolderPath(Environment.SpecialFolder specialFolder)
    {
        string folderPath = Path.Combine(Environment.GetFolderPath(specialFolder), "Widerpaper");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        return folderPath;
    }

    private void SaveImage(WiderpaperImage img)
    {
        _previousOuputFileName = Path.Combine(GetWiderpaperFolderPath(Environment.SpecialFolder.MyPictures), $"{(new DateTimeOffset(DateTime.UtcNow)).ToUnixTimeMilliseconds()}.jpg");

        img.SaveImage(_previousOuputFileName);
    }
    #endregion


    #region On Tag Change
    private async Task OnSelectImageAsync(InputFileChangeEventArgs e)
    {
        const int MAX_FILE_SIZE = 10 * 1024 * 1024;
        if (e.File.Size > MAX_FILE_SIZE)
            await _toastFileTooLarge.ShowToastAsync();

        _inputFileName = e.File.Name;

        string _inputFilePath;  // TODO: delete this line when BUG is fixed: https://github.com/dotnet/runtime/issues/74884
        _inputFilePath = Path.Combine(GetWiderpaperFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.jpg");

        await using FileStream stream = new(_inputFilePath, FileMode.Create);
        await e.File.OpenReadStream(MAX_FILE_SIZE).CopyToAsync(stream);
    }

    private void OnChangeAlgorithm(ChangeEventArgs e)
    {
        int temp;
        if (!int.TryParse(e.Value.ToString(), out temp))
            temp = -1;

        _algorithmChosen = (Algorithm)temp;
    }

    private void OnChangeBlurStrenght(ChangeEventArgs e)
    {
        if (!int.TryParse(e.Value.ToString(), out _blurStrenght))
            _blurStrenght = 5;
    }

    private void OnChangeBlurTransition(ChangeEventArgs e)
    {
        if (!bool.TryParse(e.Value.ToString(), out _shouldBlurTransition))
            _shouldBlurTransition = false;
    }
    #endregion


    #region Buttons Action
    private async Task OpenWiderpaperImageFolderAsync() => await Launcher.Default.OpenAsync(GetWiderpaperFolderPath(Environment.SpecialFolder.MyPictures));

    private async Task OpenProcessedImageAsync() => await Launcher.Default.OpenAsync(_previousOuputFileName);

    private async Task ProcessImageAsync()
    {
        if (string.IsNullOrEmpty(_inputFileName))
        {
            await _toastUnselectedFile.ShowToastAsync();
            return;
        }

        _isProcessingImage = true;

        string _inputFilePath = Path.Combine(GetWiderpaperFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.jpg");  // TODO: delete this line when BUG is fixed: https://github.com/dotnet/runtime/issues/74884
        try
        {
            using WiderpaperImage imgInput = new(_inputFilePath);
            WiderpaperImage imgOutput;

            using (imgOutput = new())
            {
                switch (_algorithmChosen)
                {
                    case Algorithm.SimpleMirror:
                        imgOutput = WiderpaperProcessing.ApplyMirror(imgInput);
                        break;

                    case Algorithm.BlurMirror:
                        await Task.Run(() => imgOutput = _shouldBlurTransition ? WiderpaperProcessing.ApplyGradientBlurMirror(imgInput, _blurStrenght) : WiderpaperProcessing.ApplyBlurMirror(imgInput, _blurStrenght));
                        break;
                }
            }

            SaveImage(imgOutput);

            await _toastFinishedProcessing.ShowToastAsync();
        }
        catch (WiderpaperException)
        {
            await _toastUnsupportedFile.ShowToastAsync();
        }

        _isProcessingImage = false;
    }
    #endregion
}