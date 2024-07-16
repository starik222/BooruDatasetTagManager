<div align="center">

[English](./README.md) | **中文简体** | [Português do Brasil](./docs/pt-BR/README_pt_BR.md)

</div>

# BooruDatasetTagManager ~Booru資料標標機~
這是一個簡單的標籤編輯器，用於編輯訓練超網路、嵌入、LoRA建立的資料集。tagger、stable-diffusion-webui等所建立的資料集。
這個編輯器主要用於booru風格的標記數據，但您也可以根據需要將其用於其他類型的資料集。

# Using
您需要如下所示的資料集：
*如果您想從頭開始建立標籤，也可以指定一個不包含文字檔案的資料集。*
![](https://user-images.githubusercontent.com/1236582/198582869-be2938a7-f7b2-4ad9-8e8c-a53604a24c2d.jpg)

在程式中，選擇“檔案->載入資料夾”並指定包含資料集的目錄。
![](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/4d5a1a31-5909-4706-a3d1-980f82d58c6a)

面板面板顯示資料集中的圖像。個標籤頁中,您可以使用內建服務(interrogator_rpc)產生標籤。
編輯完成後，選擇“文件->儲存所有變更”。
您可以在集中資料一次選擇多張圖片。

![bdtm03](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/72a450dd-93d9-4cef-9a73-8460c77e9b7d)

透過「設定」選單，您可以開啟設定視窗來自訂應用程式。 "標籤頁中，可以設定適合您的鍵位佈局。

![bdtm04](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/2adb081f-b11c-480e-b137-1cb801d0474f)

# 標籤翻譯

在使用標籤翻譯功能選擇之前，您需要在設定中翻譯語言和翻譯服務。所選的語言。Google翻譯之。
目前，手動翻譯過濾器只能用於標籤自動完成（需要在設定中啟用該選項）。

# 用於自動完成的標籤列表

本應用程式支援從「A1111的Booru標籤自動完成」使用的csv格式文件載入標籤。很長時間，程式把它們轉換為自己的格式並加載資料。

# 自動標標機 (interrogator_rpc)

您可以直接在程式中為映像產生標籤。
```bash
pip install -r requirements.txt
```
要啟動服務，運行：
```bash
python main.py
```
如果您在純Python環境中執行服務遇到問題，可以嘗試使用anaconda或miniconda。
安裝 anaconda 之後，執行控制台，建立一個新的 conda 環境並安裝必要的依賴項。
```bash
#Creating new environment with python
conda create -n bdtm python=3.10.9
#Activating the created environment
conda activate bdtm
#Installing the necessary dependencies.
pip install -r requirements.txt
#Run service
python main.py
```
想啟動已配置的服務，您需要啟動控制台並執行以下命令：
```bash
conda activate bdtm
python main.py
```
啟動服務後，在編輯器中，您可以使用“工具”選單產生所有圖像標籤，使用 ![](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/230f47f9-5cef-49bc-8b44-a67890433c42) 圖示為選定的影像產生標籤，也可以單獨在“自動標記器預覽視窗”標籤頁中產生標籤。器設定...”選單。

![bdtm06](https://github.com/starik222/BooruDatasetTagManager/assets/1236582/88c3ab34-b96e-411c-b0b9-2a92729b822c)

生成器可讓您同時選擇模型，並指定多個結果合併方法。

###翻譯者:哞哞糖
我累了，翻譯到這樣
