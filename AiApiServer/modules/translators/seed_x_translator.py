# pylint: disable=bad-indentation
import os

from transformers import AutoModelForCausalLM, AutoTokenizer
import torch

from .. import settings
from .. import devices as devices
from .. import launch, utilities, paths

LANGUAGES = {
    "Auto Detect": "auto",
    "English": "en",
    "Chinese": "zh",
    "Russian": "ru",
    "Japanese": "ka",
    "Korean": "ko",
    "Spanish": "es",
    "French": "fr",
    "Portuguese": "pt",
    "German": "de",
    "Italian": "it",
    "Thai": "th",
    "Vietnamese": "vi",
    "Indonesian": "id",
    "Malay": "ms",
    "Arabic": "ar",
    "Polish": "pl",
    "Dutch": "nl",
    "Romanian": "ro",
    "Turkish": "tr",
    "Czech": "cs",
    "Danish": "da",
    "Finnish": "fi",
    "Ukrainian": "uk",
    "Norwegian Bokmal": "nb",
    "Norwegian": "no",
    "Croatian": "hr",
    "Swedish": "sv",
    "Hungarian": "hu"
}


class seed_x_translator:

    def __init__(self, model_name):
        self.MODEL_REPO = model_name
        self.model = None
        self.tokenizer = None

    def load(self, skip_online: bool = False):
        if self.model is None:
            self.model = AutoModelForCausalLM.from_pretrained(self.MODEL_REPO,
                                                              trust_remote_code=True,
                                                              torch_dtype=devices.get_torch_dtype(),
                                                              cache_dir=paths.setting_model_path,
                                                              local_files_only=skip_online
                                                              ).to(devices.device)
            self.tokenizer = AutoTokenizer.from_pretrained(self.MODEL_REPO, legacy=False)

            if self.tokenizer.pad_token_id is None:
                self.tokenizer.pad_token_id = self.tokenizer.eos_token_id

            if torch.cuda.is_available():
                torch.backends.cuda.enable_flash_sdp(True)
                torch.backends.cuda.enable_mem_efficient_sdp(True)

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            self.tokenizer = None
            devices.torch_gc()
            print("unloading model")

    def translate(self, original_text: str, from_lang: str, to_lang: str):
        if self.model is None:
            return ""
        if not original_text.strip():
            return ""
        dest_lang_code = LANGUAGES[to_lang]
        if dest_lang_code == "auto":
            prompt = f"Translate the following sentence into {to_lang} and explain it in detail:\n{original_text} <{dest_lang_code}>"
        else:
            prompt = f"Translate the following {from_lang} sentence into {to_lang} and explain it in detail:\n{original_text} <{dest_lang_code}>"
        # input_tokens = (
        #     self.tokenizer(prompt, return_tensors="pt")
        #     .input_ids[0]
        #     .cpu()
        #     .numpy()
        #     .tolist()
        # )
        # translated_chunk = self.model.generate(
        #     input_ids=torch.tensor([input_tokens]).to(devices.device),
        #     max_length=512,
        #     num_beams=4,
        #     num_return_sequences=1,
        #     pad_token_id=self.tokenizer.pad_token_id,
        #     eos_token_id=self.tokenizer.eos_token_id,
        # )

        input_tokens = self.tokenizer(
            prompt,
            return_tensors="pt",
            padding=True,
            return_attention_mask=True
        ).to(devices.device)

        translated_chunk = self.model.generate(
            **input_tokens,
            max_new_tokens=512,
            num_beams=4,
            early_stopping=True,
            pad_token_id=self.tokenizer.pad_token_id,
            eos_token_id=self.tokenizer.eos_token_id,
        )
        full_output = self.tokenizer.decode(translated_chunk[0], skip_special_tokens=True)
        full_output = full_output.replace(prompt.strip(), "")
        return full_output
