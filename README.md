<div align="center">

**English** | [中文简体](./README_zh_CN.md) | [Português do Brasil](./docs/pt-BR/README_pt_BR.md)

</div>

# BooruDatasetTagManager
A simple tag editor for a dataset created for training hypernetworks, embeddings, lora, etc. You can create a dataset from scratch using only images, or you can use a program to edit a dataset created using automatic tagging ([wd14-tagger](https://github.com/toriato/stable-diffusion-webui-wd14-tagger), [stable-diffusion-webui](https://github.com/AUTOMATIC1111/stable-diffusion-webui), etc.)
The editor is primarily intended for booru-style tagged data, but you can adapt it for other datasets as well. 

Since version 2.5.0 the editor also supports video tagging.

# Using
You need a dataset like the following:

*You can also specify a dataset without text files if you want to create tags from scratch. In this case, text files will be created on save.*

![](https://user-images.githubusercontent.com/1236582/198582869-be2938a7-f7b2-4ad9-8e8c-a53604a24c2d.jpg)


In the program, select "File->Load folder" and specify the directory with the dataset.

![](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/4d5a1a31-5909-4706-a3d1-980f82d58c6a)

The left pane displays images from the dataset. The central panel displays tags for the selected images, which you can edit. The right panel has two tabs. The first tab displays all (or common) tags present in the dataset. In the second tab you can generate tags using the built-in service (interrogator_rpc).

After editing, you will select "File->Save all changes".

You can select multiple images at once in a dataset. This will allow you to easily edit tags for images of the same type.

![bdtm03](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/72a450dd-93d9-4cef-9a73-8460c77e9b7d)

Through the "Setting" menu, you can open the settings window to customize the application for yourself. Users who have Google Translate blocked can change the translation service to Chinese. On the "UI" tab, you can select a color scheme, and on the "Hotkeys" tab, configure the key layout that is convenient for you.

![bdtm04](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/2adb081f-b11c-480e-b137-1cb801d0474f)

# Tag translation

Before using tag translation, you need to select the translation language and translation service in the settings.
From the "view" menu, select "Translate tags" to display columns with translated values. When displaying columns, all tags will be automatically translated into the language you selected. The translation is saved in the "Translations" folder with the name of the selected language. You can manually edit the translation in this file as the translation is taken from this file first. Manual translation is recommended to be marked with the "*" symbol.

Translation file example:
```bash
//Translation format: <original>=<translation>
black hair=÷åðíûå âîëîñû
*solo=Ñîëî
1girl=1 äåâóøêà
```

Currently, the manual translation filter can only be used in tag autocompletion (with the option enabled in the settings). But in the future, it can be used somewhere else.

# Tag list for autocomplete

The application supports loading tags from csv files of the format used in "[Booru tag autocompletion for A1111](https://github.com/DominikDoom/a1111-sd-webui-tagcomplete)". You can also create your own txt files with a list of tags (line by line). But since loading data from these files takes a long time, the program converts them to its own format and loads data from it. Therefore, if you change the list of tags, be prepared to wait quite a long time. All files with tags are located in the "Tags" folder.

# AutoTagger (AiApiServer)

You can generate tags for images directly in the program. To do this, you need to configure and run the "AiApiServer" service. Python must be installed for it to work.
To configure AiApiServer, run the command:
```bash
pip install -r requirements.txt
```
Since the latest version of onnxruntime requires msvc runtime 2015 version, it is recommended to install this package.
If you use anaconda:
```bash
conda install conda-forge::vs2015_runtime
```
If you install it normally with pip:
```bash
pip install msvc-runtime
```
To start the service run
```bash
python main.py
```
If you have problems running a service in pure python, try using [anaconda](https://www.anaconda.com/download) or [miniconda](https://docs.conda.io/projects/miniconda/en/latest/).

After installing anaconda, run the console, create a new conda environment and install the necessary dependencies.
```bash
#Creating new environment with python
conda create -n bdtm python=3.12.9
#Activating the created environment
conda activate bdtm
#Installing the necessary dependencies.
pip install -r requirements.txt
#Run service
python main.py
```
To start an already configured service, you need to launch the console and run the following commands
```bash
conda activate bdtm
python main.py
```
After launching the service, in the editor itself you can generate tags for all images using the "Tools" menu, generate tags for selected images using ![](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/230f47f9-5cef-49bc-8b44-a67890433c42) icon, and also generate tags in a separate tab "AutoTagger preview window". To configure generation parameters, you can use the corresponding generation menu item, or the "Settings" -> "Auto tagger settings..." menu.

![bdtm06](https://github.com/user-attachments/assets/5bcc14c8-b505-4e58-9d76-7910f4b40c84)

The generator allows you to select several models at once and specify a method for combining the results.

# AiApiServer problems

At the moment, the latest version of transformers that Florence2 models work with is 4.49.0. So if you want to use Florence2 models you need to downgrade transformers by running `pip install transformers==4.49.0 --upgrade`, but this may break other models, so choose for yourself what you need.

The `briaai/RMBG-2.0` model (the background removal model) also doesn't work with the latest versions of transformers, so use the `BiRefNet` models instead.

Model `Kwai-Keye/Keye-VL-1_5-8B` requires `Flash Attention 2` and `triton`. If you are using Windows, then you will encounter the problem that Flash Attention does not install on Windows, and it needs to be manually builded for your computer configuration. You can use [this article](https://github.com/Dao-AILab/flash-attention/issues/1469), or look for ready-made builds for your configuration. If you are using python 3.12+ and a blackwell 2.0 (rtx 50), ada (rtx 40) or ampere (rtx 30) video card, you can try installing [this build](https://huggingface.co/Panchovix/flash-attentionv2-blackwell2.0-nightly/tree/main).

To install triton, run the command `pip install triton-windows`.

# Weighted tags

The editor supports working with weighted tags. When loading tags, brackets are automatically converted to weights. To change the weight of a tag, you need to select it and move the "weight" track bar to the required number of positions. One position equals one bracket.

# Color scheme

Currently, the program offers two color schemes (Classic and Dark). You can create or change the color scheme yourself. There is no window-based color scheme editor yet, but you can open the ColorScheme.json file using a text editor and make the necessary changes.

# Interface translation

All language files are located in the `Languages` ​​folder. You can translate the application interface into the language you are interested in. To do this, you need to copy any `xx-XX.txt` file you like, rename it according to your [language code](https://learn.microsoft.com/en-us/openspecs/office_standards/ms-oe376/6c085406-a698-4e12-9d4d-c3b0ee3dbc4a) and translate the contents after the `=` sign. You can create a topic in Issues or discussions and attach your translation. I will include your translation in the next release.

# Dependencies

- [ScreenLister](https://github.com/starik222/ScreenLister) - Used to obtain images from videos.

# Build

This is a tool designed in C and you will need to run it in Visual Studio (not Visual Studio Code). Steps to achieve this are:
1. Download [visual Studio](https://visualstudio.microsoft.com/downloads/)
2. Clone this repo into a folder somewhere on your computer
3. Open the repo using Visual Studio: `File` > `Open` > `Project/Solution` > select the `BooruDatasetTagManager.sln` file
4. Build the solution by selecting `Build` > `Build Solution` from the menu (or by pressing Ctrl+Shift+B.
Run the Application)

# Other

Using the "View" menu you can hide panels you don't need.
In the "Tools" menu there is a function to automatically replace the transparent background with the color you need.
