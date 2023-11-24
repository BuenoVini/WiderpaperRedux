using BlazorApp.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Widerpaper;

namespace BlazorApp.Pages;

public partial class Index
{
	#region Constants
	const int _MAX_FILE_SIZE = 10 * 1024 * 1024;
    const int _MAX_ALLOWED_FILES = 20;
	#endregion

	
	#region Fields
	private List<WiderpaperMetadata> _loadedMetadataImages = new();
	
	private Toast _toastFinishedProcessing;
    private Toast _toastUnselectedFiles;
    private Toast _toastUnsupportedFile;
    private Toast _toastFileTooLarge;
    private Toast _toastTooManyFiles;

    public enum Algorithm { SimpleMirror, BlurMirror }
    private Algorithm _algorithmChosen = Algorithm.SimpleMirror;

    public enum Upscaling { Original, TwoTimes, FourTimes}
    private Upscaling _upscalingChosen = Upscaling.Original;

	public enum Format { Original, Jpeg, Png }
	private Format _formatChosen = Format.Original;

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

    private string SetAlgorithmButtonsState(string tagClass, Algorithm algorithm, string tokenToReplace = "?")
        => tagClass.Replace(tokenToReplace, _algorithmChosen == algorithm ? "primary no-hover" : "secondary");

    private string SetUpscalingButtonsState(string tagClass, Upscaling upscaling, string tokenToReplace = "?")
		=> tagClass.Replace(tokenToReplace, _upscalingChosen == upscaling ? "primary no-hover" : "secondary");

	private string SetFormatButtonsState(string tagClass, Format format, string tokenToReplace = "?")
		=> tagClass.Replace(tokenToReplace, _formatChosen == format ? "primary no-hover" : "secondary");
	#endregion


	#region On Click Handlers
	private void OnClickSimpleMirrorBtn() => _algorithmChosen = Algorithm.SimpleMirror;
	private void OnClickBlurMirrorBtn() => _algorithmChosen = Algorithm.BlurMirror;

    private void OnClickUpscalingOriginalBtn() => _upscalingChosen = Upscaling.Original;
    private void OnClickUpscalingTwoTimesBtn() => _upscalingChosen = Upscaling.TwoTimes;
    private void OnClickUpscalingFourTimesBtn() => _upscalingChosen = Upscaling.FourTimes;

	private void OnClickFormatOriginalBtn() => _formatChosen = Format.Original;
	private void OnClickFormatJpegBtn() => _formatChosen = Format.Jpeg;
	private void OnClickFormatPngBtn() => _formatChosen = Format.Png;

	private void OnClickDeleteRowBtn(WiderpaperMetadata imageToDelete) =>
		_loadedMetadataImages.RemoveAt(_loadedMetadataImages.FindIndex(image => image.Path == imageToDelete.Path));

    private async Task OnClickOpenOutputFolderAsync() => 
	    await Launcher.Default.OpenAsync(GetWiderpaperFolderPath(Environment.SpecialFolder.MyPictures));
    
    private async Task OnClickStartProcessingAsync()
    {
	    if (_loadedMetadataImages.Count <= 0)
	    {
		    await _toastUnselectedFiles.ShowToastAsync();
		    return;
	    }

	    _isProcessingImage = true;

	    foreach (WiderpaperMetadata image in _loadedMetadataImages)
	    {
		    try
		    {
			    using WiderpaperImage imgInput = new(image.Path);
			    
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
					
					SaveImage(imgOutput);
			    }
			    
			    // await _toastFinishedProcessing.ShowToastAsync();
		    }
		    catch (WiderpaperException)
		    {
			    await _toastUnsupportedFile.ShowToastAsync();
		    }
	    }

	    _isProcessingImage = false;
    }
	#endregion


	#region On Input Handlers
	private void OnInputBlurStrenght(ChangeEventArgs e)
	{
		if (!int.TryParse(e.Value.ToString(), out _blurStrenght))
			_blurStrenght = 5;
	}
	#endregion


	#region On Change Handlers
	private async Task OnChangeLoadImagesAsync(InputFileChangeEventArgs e)
    {
        /* clearing the loaded images metadata list */
        _loadedMetadataImages.Clear();

		/* checking for forbidden files selected by the user */
		if (e.FileCount > _MAX_ALLOWED_FILES)
        {
            await _toastTooManyFiles.ShowToastAsync();
            return;
        }
		
		// TODO: perform file signature validation

        if (e.GetMultipleFiles().Any(file => file.Size > _MAX_FILE_SIZE))
        {
	        await _toastFileTooLarge.ShowToastAsync();
	        return;
        }

        /* deleting previous images in wwwroot/images/ */
        foreach (FileInfo file in new DirectoryInfo("wwwroot/images").GetFiles())
            file.Delete();

		/* saving a copy of the selected images in wwwroot/images/ */
		foreach (IBrowserFile file in e.GetMultipleFiles(_MAX_ALLOWED_FILES))
		{
			string fileName = $"{Path.GetRandomFileName().Split('.')[0]}.{file.ContentType.Split('/')[1]}";
			string filePath = Path.Combine("wwwroot/images", fileName);

			FileStream stream = new(filePath, FileMode.Create);

            try
            {
                await file.OpenReadStream(_MAX_FILE_SIZE).CopyToAsync(stream);
                stream.Close();
                
                _loadedMetadataImages.Add(new WiderpaperMetadata(Path.Combine("wwwroot/images", fileName), file.Size));
            }
            catch (Exception)
            {
                stream.Close();
                File.Delete(filePath);
            }
        }
    }

    private void OnChangeBlurTransition(ChangeEventArgs e)
    {
        if (!bool.TryParse(e.Value.ToString(), out _shouldBlurTransition))
            _shouldBlurTransition = false;
    }
    #endregion


    #region Buttons Action
    private async Task OpenProcessedImageAsync() => await Launcher.Default.OpenAsync(_previousOuputFileName);
    #endregion
}