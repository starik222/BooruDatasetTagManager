# BooruDatasetTagManager
A simple tag editor for a dataset created for training hypernetworks, embeddings, lora, etc. You can create a dataset from scratch using only images, or you can use a program to edit a dataset created using automatic tagging ([wd14-tagger](https://github.com/toriato/stable-diffusion-webui-wd14-tagger), [stable-diffusion-webui](https://github.com/AUTOMATIC1111/stable-diffusion-webui), etc.)
The editor is primarily intended for booru-style tagged data, but you can adapt it for other datasets as well.
# Using
You need a dataset like the following:

*You can also specify a dataset without text files if you want to create tags from scratch. In this case, text files will be created on save.*

![](https://user-images.githubusercontent.com/1236582/198582869-be2938a7-f7b2-4ad9-8e8c-a53604a24c2d.jpg)



In the program, select "File->Load folder" and specify the directory with the dataset.

![](https://user-images.githubusercontent.com/1236582/230425218-7718cc79-ba36-48c9-b08f-c36d72633eee.png)

In the left column, tags are edited for the selected image, in the right column, tags are edited for all images of the dataset.

After editing, you will select "File->Save all changes".

You can select multiple images at once in a dataset. This will allow you to easily edit tags for images of the same type.

![](https://user-images.githubusercontent.com/1236582/230428077-a1e3a724-d5fc-4cf6-a187-e7090c381762.png)

Through the "file" menu, you can open the settings window to customize the application for yourself. Users who have Google Translate blocked can change the translation service to Chinese.

![](https://user-images.githubusercontent.com/1236582/230429522-9ae76b82-f8d7-4f24-81e4-8c7072bd412c.png)

# Tag translation

Before using tag translation, you need to select the translation language and translation service in the settings.
From the "view" menu, select "Translate tags" to display columns with translated values. When displaying columns, all tags will be automatically translated into the language you selected. The translation is saved in the "Translations" folder with the name of the selected language. You can manually edit the translation in this file as the translation is taken from this file first. Manual translation is recommended to be marked with the "*" symbol.

Translation file example:
```bash
//Translation format: <original>=<translation>
black hair=черные волосы
*solo=Соло
1girl=1 девушка
```

Currently, the manual translation filter can only be used in tag autocompletion (with the option enabled in the settings). But in the future, it can be used somewhere else.

# Tag list for autocomplete

The application supports loading tags from csv files of the format used in "[Booru tag autocompletion for A1111](https://github.com/DominikDoom/a1111-sd-webui-tagcomplete)". You can also create your own txt files with a list of tags (line by line). But since loading data from these files takes a long time, the program converts them to its own format and loads data from it. Therefore, if you change the list of tags, be prepared to wait quite a long time. All files with tags are located in the "Tags" folder.
