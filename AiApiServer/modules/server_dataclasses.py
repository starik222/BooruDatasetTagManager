from dataclasses import dataclass, field
from dataclasses_json import dataclass_json, config
from typing import List
import base64


def decode_base64(value: str) -> bytes:
    return base64.b64decode(value)


def encode_base64(value: bytes) -> str:
    return base64.b64encode(value).decode('ascii')


@dataclass_json
@dataclass
class ModelAdditionalParameters:
    Key: str
    Value: str
    Type: str
    Comment: str


@dataclass_json
@dataclass
class ModelParameters:
    ModelName: str
    AdditionalParameters: List[ModelAdditionalParameters]


@dataclass_json
@dataclass
class InterrogateImageRequest:
    Image: bytes = field(
        metadata=config(
            encoder=encode_base64,
            decoder=decode_base64
        )
    )
    SkipInternetRequests: bool
    SerializeVramUsage: bool
    ImageName: str
    Models: List[ModelParameters]


@dataclass_json
@dataclass
class EditImageRequest:
    Image: bytes = field(
        metadata=config(
            encoder=encode_base64,
            decoder=decode_base64
        )
    )
    SkipInternetRequests: bool
    SerializeVramUsage: bool
    ImageName: str
    Model: ModelParameters


@dataclass_json
@dataclass
class TranslateRequest:
    Text: str
    FromLang: str
    ToLang: str
    SkipInternetRequests: bool
    SerializeVramUsage: bool
    Model: ModelParameters


@dataclass_json
@dataclass
class TagEntry:
    Tag: str
    Probability: float


@dataclass_json
@dataclass
class InterrogateImageResult:
    ModelName: str
    Tags: List[TagEntry]

    def __init__(self):
        self.ModelName = ""
        self.Tags = []


@dataclass_json
@dataclass
class InterrogateImageResponse:
    Success: bool
    ErrorMessage: str
    Result: List[InterrogateImageResult]

    def __init__(self):
        self.Success = False
        self.ErrorMessage = ""
        self.Result = []


@dataclass_json
@dataclass
class ModelParamResponse:
    Success: bool
    ErrorMessage: str
    Type: str
    Parameters: List[ModelAdditionalParameters]

    def __init__(self):
        self.Success = False
        self.ErrorMessage = ""
        self.Type = ""
        self.Parameters = []


@dataclass_json
@dataclass
class EditImageResponse:
    Success: bool
    ErrorMessage: str
    Image: bytes = field(
        metadata=config(
            encoder=encode_base64,
            decoder=decode_base64
        )
    )

    def __init__(self):
        self.Success = False
        self.ErrorMessage = ""
        self.Image = bytes()


@dataclass_json
@dataclass
class TranslateTextResponse:
    Success: bool
    ErrorMessage: str
    TranslatedText: str

    def __init__(self):
        self.Success = False
        self.ErrorMessage = ""
        self.TranslatedText = ""


@dataclass_json
@dataclass
class ConfigResponse:
    Interrogators: List[str]
    Editors: List[str]
    Translators: List[str]

    def __init__(self):
        self.Interrogators = []
        self.Editors = []
        self.Translators = []
