<!-- A big shout out to Othneil Drew (https://github.com/othneildrew/) for such an amazing template!!! -->

<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
<!-- [![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url] -->

<p align="center">
  <a href="https://github.com/BuenoVini/WiderpaperRedux/releases/tag/v1.0.0">
    <img src="https://img.shields.io/badge/current_version-1.0.0-blue?style=for-the-badge" alt="Current Version">
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/license-none_yet-blue?style=for-the-badge" alt="License">
  </a>
</p>



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="">
    <img src="/.readme_images/appicon.png" alt="Logo" width="170">
  </a>

<h1 align="center" style="border-bottom: none; font-size: 3em;">
  <strong>Widerpaper</strong> 
  <p style="font-weight: lighter">Redux</p>
</h1>

  <p align="center">
    Transform your 16:9 wallpapers into 21:9 Ultrawide!
    <br />
    <br />
    <a href="https://github.com/BuenoVini/WiderpaperRedux/issues/new">Report Bug</a>
    ·
    <a href="https://github.com/BuenoVini/WiderpaperRedux/issues/new">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#motivation">Motivation</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![Welcome Image][product-screenshot]

**Widerpaper** is a solution tailored to the needs of Ultrawide monitor users that often encounter difficulties when selecting and setting up new wallpapers. This project aims to solve those issues!

When applying a conventional 16:9 wallpaper to an Ultrawide screen, the result is often a distorted, stretched, or improperly cropped image that fails to take full advantage of the monitor's extra space.

**Widerpaper**'s primary objective is to adapt 16:9 wallpapers to the 21:9 aspect ratio of Ultrawide monitors. By applying mirror algorithms and blur techniques, this solution expands the original image content allowing users to enjoy a visually engaging wallpaper that fully covers their Ultrawide screen space.


<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

[![Visual Studio 2022][vs22-shield]][vs22-url]
[![.NET MAUI][maui-shield]][maui-url]
[![Blazor][blazor-shield]][blazor-url]
[![ImageSharp][imagesharp-shield]][imagesharp-url]
[![Bootstrap][bootstrap-shield]][bootstrap-url]

<!-- <p align="center">
  <a href="https://visualstudio.microsoft.com/vs/">
    <img src="https://img.shields.io/badge/visual_studio_2022-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white" alt="Visual Studio 2022">
  </a>
  <a href="https://dotnet.microsoft.com/en-us/apps/maui">
    <img src="https://img.shields.io/badge/.net%20maui-5027d5?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET MAUI">
  </a>
  <a href="https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor">
    <img src="https://img.shields.io/badge/blazor-5027d5?style=for-the-badge&logo=blazor&logoColor=whitee" alt="Blazor">
  </a>
  <a href="https://sixlabors.com/products/imagesharp/">
    <img src="https://img.shields.io/badge/imagesharp-ab1534?style=for-the-badge&logo=nuget&logoColor=whitee" alt="ImageSharp">
  </a>
  <a href="https://getbootstrap.com">
    <img src="https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white" alt="Bootstrap">
  </a>
</p> -->


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

Unlike standard .NET MAUI apps, which are distributed as [MSIX files](https://learn.microsoft.com/en-us/windows/msix/overview), **Widerpaper** is shared as an unpackaged application. This approach requires the users to manually install the necessary dependencies for the application to function properly on their system.

Also, note that while .NET MAUI is designed for cross-platform compatibility, **Widerpaper** is written exclusively for Windows computers. More especifically Windows 10 and Windows 11.

### Prerequisites

* [Windows App SKD](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads)
* [Microsoft Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170)
* [Microsoft Edge WebView2](https://developer.microsoft.com/pt-br/microsoft-edge/webview2/)
* Windows 10 version 1809 or higher (Windows 11)


### Installation

1. Download the latest version of the **Widerpaper** app from the [Releases](https://github.com/BuenoVini/WiderpaperRedux/releases) page.
2. Unzip the downloaded file and place the folder somewhere accessible on your computer (e.g.: `Desktop`, `Documents`)
3. Inside the unzipped folder, launch the app by clicking the `_WiderpaperRedux.exe` file.
4. (Optional) Create a shortcut of the executable file to launch the app from the Start menu or from the desktop.


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

![Features][features]

There are a couple of algorithms at your disposal to convert your wallpapers into Ultrawide.

**Simple Mirror:** This algorithm extends the image by mirroring its left and right sides until it fits the Ultrawide aspect ratio (21:9).

**Mirror with Gaussian Blur:** This algorithm first duplicates the left and right sides of the image using the *Simple Mirror* technique to be Ultrawide. Lastly, Gaussian Blur is applyed in the mirrored area.

**Smooth Blur Transition:** This algorithm applies a sigmoid function to the blurred section within the *Mirror with Gaussian Blur* image.

![Before and after][before-after]


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

Below, are listed my plans for future enhancements and features that will be implemented within the app. Stay tuned for the exciting developments on the horizon! Be sure to Star the project to stay updated on all the latest developments!

- [ ] Localization to PT-BR
- [ ] Distribute as MSIX file
    - [ ] Release on Microsoft Store?
- [ ] Add app personalization
    - [ ] UI Theme
    - [ ] App behavior 

See the [open issues](https://github.com/github_username/repo_name/issues) for a full list of proposed features (and known issues).


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MOTIVATION -->
## Motivation
First and foremost, this project is a result of both passion and learning. It all began because of the consistent frustration I faced while attempting to set new Ultrawide wallpapers to my personal computer. I then realized I could continue advancing my skills in C# and .NET through the development of a solution to this specific problem. In the end, it also presented an exciting chance to explore MAUI development, a technology I've been eager to learn more about. This combination gave rise to the creation of **Widerpaper**.

**Widerpaper Redux** is a continuation of my prior attempt to solve said issue, published here on GitHub called simply [Widerpaper](https://github.com/BuenoVini/Widerpaper). The key difference is the broadening of the original scope. Unlike the initial version that featured only the **Simple Mirror** algorithm and employed Blazor Server for hosting, this iteration aims for multiple algorithms to be choosen from and is targeted for desktop users.

Through the course of development, I've had support from friends who assisted me in testing, provided valuable insights, and backed my crazy ideas. Thanks guys!!

I really hope this app can help you and others set up new wallpapers!


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Any feedback, bug report, and feature request are **greatly appreciated**.

If you have a suggestion that would make **Widerpaper** better, please create an issue with the tag "enhancement".
(<a href="https://github.com/BuenoVini/WiderpaperRedux/issues/new">Request Feature</a>)

If it is a bug or another problem you come across while using the app, open an issue with the tag "bug". (<a href="https://github.com/BuenoVini/WiderpaperRedux/issues/new">Report Bug</a>)

Don't forget to give the project a Star! Your support is greatly appreciated. Thank you once again!



<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- LICENSE -->
## License

Currently, **Widerpaper** does not have a specific license in place. If you're interested in contributing with your code, kindly initiate the process by opening an issue and sharing your ideas.

To be fair, I'm new to licensing (mostly because I've never needed it before). If you have recommendations for a suitable license, don't hesitate to open an issue, and we can engage in a discussion!

I am looking forward to have a conversation and I will be very happy to chat!

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
[vs22-shield]: https://img.shields.io/badge/visual_studio_2022-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white
[vs22-url]: https://visualstudio.microsoft.com/vs/

[maui-shield]: https://img.shields.io/badge/.net%20maui-5027d5?style=for-the-badge&logo=dotnet&logoColor=white
[maui-url]: https://dotnet.microsoft.com/en-us/apps/maui

[blazor-shield]: https://img.shields.io/badge/blazor-5027d5?style=for-the-badge&logo=blazor&logoColor=white
[blazor-url]: https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor

[imagesharp-shield]: https://img.shields.io/badge/imagesharp-ab1534?style=for-the-badge&logo=nuget&logoColor=white
[imagesharp-url]: https://sixlabors.com/products/imagesharp/

[bootstrap-shield]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[bootstrap-url]: https://getbootstrap.com

[product-screenshot]: /.readme_images/welcome.gif
[before-after]: /.readme_images/before-after.gif
[features]: /.readme_images/features.gif

