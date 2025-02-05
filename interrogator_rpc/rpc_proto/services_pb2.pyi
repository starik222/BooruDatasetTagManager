from google.protobuf.internal import containers as _containers
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Iterable as _Iterable, Mapping as _Mapping, Optional as _Optional, Union as _Union

DESCRIPTOR: _descriptor.FileDescriptor

class InterrogatorListing(_message.Message):
    __slots__ = ("interrogator_names",)
    INTERROGATOR_NAMES_FIELD_NUMBER: _ClassVar[int]
    interrogator_names: _containers.RepeatedScalarFieldContainer[str]
    def __init__(self, interrogator_names: _Optional[_Iterable[str]] = ...) -> None: ...

class NetworkInterrogationParameters(_message.Message):
    __slots__ = ("interrogator_network", "AdditionalParameters")
    INTERROGATOR_NETWORK_FIELD_NUMBER: _ClassVar[int]
    ADDITIONALPARAMETERS_FIELD_NUMBER: _ClassVar[int]
    interrogator_network: str
    AdditionalParameters: _containers.RepeatedCompositeFieldContainer[AdditionalNetworkParameter]
    def __init__(self, interrogator_network: _Optional[str] = ..., AdditionalParameters: _Optional[_Iterable[_Union[AdditionalNetworkParameter, _Mapping]]] = ...) -> None: ...

class InterrogationRequest(_message.Message):
    __slots__ = ("params", "interrogate_image", "skip_internet_requests", "serialize_vram_usage", "image_name")
    PARAMS_FIELD_NUMBER: _ClassVar[int]
    INTERROGATE_IMAGE_FIELD_NUMBER: _ClassVar[int]
    SKIP_INTERNET_REQUESTS_FIELD_NUMBER: _ClassVar[int]
    SERIALIZE_VRAM_USAGE_FIELD_NUMBER: _ClassVar[int]
    IMAGE_NAME_FIELD_NUMBER: _ClassVar[int]
    params: _containers.RepeatedCompositeFieldContainer[NetworkInterrogationParameters]
    interrogate_image: bytes
    skip_internet_requests: bool
    serialize_vram_usage: bool
    image_name: str
    def __init__(self, params: _Optional[_Iterable[_Union[NetworkInterrogationParameters, _Mapping]]] = ..., interrogate_image: _Optional[bytes] = ..., skip_internet_requests: bool = ..., serialize_vram_usage: bool = ..., image_name: _Optional[str] = ...) -> None: ...

class InterrogatorParamRequest(_message.Message):
    __slots__ = ("interrogator_network",)
    INTERROGATOR_NETWORK_FIELD_NUMBER: _ClassVar[int]
    interrogator_network: str
    def __init__(self, interrogator_network: _Optional[str] = ...) -> None: ...

class InterrogatorParamResponse(_message.Message):
    __slots__ = ("Result", "ErrMes", "Type", "Parameters")
    RESULT_FIELD_NUMBER: _ClassVar[int]
    ERRMES_FIELD_NUMBER: _ClassVar[int]
    TYPE_FIELD_NUMBER: _ClassVar[int]
    PARAMETERS_FIELD_NUMBER: _ClassVar[int]
    Result: bool
    ErrMes: str
    Type: str
    Parameters: _containers.RepeatedCompositeFieldContainer[AdditionalNetworkParameter]
    def __init__(self, Result: bool = ..., ErrMes: _Optional[str] = ..., Type: _Optional[str] = ..., Parameters: _Optional[_Iterable[_Union[AdditionalNetworkParameter, _Mapping]]] = ...) -> None: ...

class TagEntry(_message.Message):
    __slots__ = ("tag", "probability")
    TAG_FIELD_NUMBER: _ClassVar[int]
    PROBABILITY_FIELD_NUMBER: _ClassVar[int]
    tag: str
    probability: float
    def __init__(self, tag: _Optional[str] = ..., probability: _Optional[float] = ...) -> None: ...

class AdditionalNetworkParameter(_message.Message):
    __slots__ = ("key", "value", "type", "comment")
    KEY_FIELD_NUMBER: _ClassVar[int]
    VALUE_FIELD_NUMBER: _ClassVar[int]
    TYPE_FIELD_NUMBER: _ClassVar[int]
    COMMENT_FIELD_NUMBER: _ClassVar[int]
    key: str
    value: str
    type: str
    comment: str
    def __init__(self, key: _Optional[str] = ..., value: _Optional[str] = ..., type: _Optional[str] = ..., comment: _Optional[str] = ...) -> None: ...

class InterrogationResponse(_message.Message):
    __slots__ = ("network_name", "tags")
    NETWORK_NAME_FIELD_NUMBER: _ClassVar[int]
    TAGS_FIELD_NUMBER: _ClassVar[int]
    network_name: str
    tags: _containers.RepeatedCompositeFieldContainer[TagEntry]
    def __init__(self, network_name: _Optional[str] = ..., tags: _Optional[_Iterable[_Union[TagEntry, _Mapping]]] = ...) -> None: ...

class ImageTagResults(_message.Message):
    __slots__ = ("responses", "interrogate_ok", "error_msg")
    RESPONSES_FIELD_NUMBER: _ClassVar[int]
    INTERROGATE_OK_FIELD_NUMBER: _ClassVar[int]
    ERROR_MSG_FIELD_NUMBER: _ClassVar[int]
    responses: _containers.RepeatedCompositeFieldContainer[InterrogationResponse]
    interrogate_ok: bool
    error_msg: str
    def __init__(self, responses: _Optional[_Iterable[_Union[InterrogationResponse, _Mapping]]] = ..., interrogate_ok: bool = ..., error_msg: _Optional[str] = ...) -> None: ...

class InterrogatorListingRequest(_message.Message):
    __slots__ = ()
    def __init__(self) -> None: ...
