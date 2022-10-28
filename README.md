# BooruDatasetTagManager
A simple tag editor for a dataset created in [stable-diffusion-webui](https://github.com/AUTOMATIC1111/stable-diffusion-webui) with the deepdanbooru option enabled.


#Using
You need a dataset like the following:

![](https://user-images.githubusercontent.com/1236582/198582869-be2938a7-f7b2-4ad9-8e8c-a53604a24c2d.jpg)

In the program, select "File->Load folder" and specify the directory with the dataset.

![](https://user-images.githubusercontent.com/1236582/198584302-318bb8bb-a6b5-464e-bf0f-181bbeeedd9e.jpg)

In the left column, tags are edited for the selected image, in the right column, tags are edited for all images of the dataset.

After editing, you will select "File->Save all changes".

Also, you can load loss statistics after training.
After pressing the "Interrupt" button, in the console you will see the loss statistics for each image.
![](https://user-images.githubusercontent.com/1236582/198585578-1a958600-cc95-466e-b926-3cfed44b28e4.jpg)

Copy all text to file. File should look like this:
Loss statistics for file C:\NAI\stable-diffusion-webui\train\NishinoOut2\00006-0-00003-0-98028336_p0.png
loss:0.045±(0.002)
recent 32 loss:0.055±(0.007)
Loss statistics for file C:\NAI\stable-diffusion-webui\train\NishinoOut2\00014-0-00007-0-98909113_p0.png
loss:0.045±(0.002)
recent 32 loss:0.048±(0.007)
...

In the program, select "File->Load loss from file", and you will see:
![](https://user-images.githubusercontent.com/1236582/198586476-6094d32f-b31d-48a2-8ad7-f043417cd78c.jpg)

You can automatically translate tags into the language you need. Specify the code of the language you need in the setting.json file. In the program select "View->Translate tags".