# Folder Move Validator for Unity

## Introduction
`FolderMoveValidator.cs` is a script for Unity designed to prevent users from accidentally moving folders within the Unity Editor. It does so by confirming user intent if the attempted move meets certain conditions.

## Features
- Folder move confirmation dialog for or quickly moved folders
- Customizable settings for threshold on file count and move speed

## Installation
1. Add the `FolderMoveValidator.cs` script into your Unity project's `Editor` folder.
2. If the `Editor` folder doesn't exist, create it in your project's root directory (`Assets`) before adding the script.

## Configuration
After installation, you can configure the FolderMoveValidator settings:
1. Open Unity and go to **Edit** > **Preferences**.
2. Find the **Folder Move Validator Settings** in the Preferences window.
3. Set the `Milliseconds Threshold` to your desired speed sensitivity. This determines how quickly consecutive clicks are interpreted as a possible accidental folder move.
4. Set the `File Threshold` to determine the minimum number of files in a folder for the confirmation dialog to appear.

## Usage
When you attempt to move a folder that has more files than your `File Threshold` within a shorter time than your `Milliseconds Threshold`, Unity will prompt you with a confirmation dialog. If you confirm, the folder will be moved; otherwise, the move will be cancelled.

## Support
If you encounter issues or have questions, please submit them to the project's Issues section on GitHub.

## License
This script is released under the [GNU License](https://ru.wikipedia.org/wiki/GNU_General_Public_License).
