from google.protobuf import empty_pb2 as _empty_pb2
from google.protobuf.internal import containers as _containers
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Iterable as _Iterable, Mapping as _Mapping, Optional as _Optional, Union as _Union

DESCRIPTOR: _descriptor.FileDescriptor

class InterrogatorListing(_message.Message):
    __slots__ = ["interrogator_names"]
    INTERROGATOR_NAMES_FIELD_NUMBER: _ClassVar[int]
    interrogator_names: _containers.RepeatedScalarFieldContainer[str]
    def __init__(self, interrogator_names: _Optional[_Iterable[str]] = ...) -> None: ...

class InterrogationRequest(_message.Message):
    __slots__ = ["interrogator_network", "interrogator_threshold"]
    INTERROGATOR_NETWORK_FIELD_NUMBER: _ClassVar[int]
    INTERROGATOR_THRESHOLD_FIELD_NUMBER: _ClassVar[int]
    interrogator_network: str
    interrogator_threshold: float
    def __init__(self, interrogator_network: _Optional[str] = ..., interrogator_threshold: _Optional[float] = ...) -> None: ...

class TagEntry(_message.Message):
    __slots__ = ["tag", "probability"]
    TAG_FIELD_NUMBER: _ClassVar[int]
    PROBABILITY_FIELD_NUMBER: _ClassVar[int]
    tag: str
    probability: float
    def __init__(self, tag: _Optional[str] = ..., probability: _Optional[float] = ...) -> None: ...

class ImageTagResults(_message.Message):
    __slots__ = ["tags", "interrogate_ok", "error_msg"]
    TAGS_FIELD_NUMBER: _ClassVar[int]
    INTERROGATE_OK_FIELD_NUMBER: _ClassVar[int]
    ERROR_MSG_FIELD_NUMBER: _ClassVar[int]
    tags: _containers.RepeatedCompositeFieldContainer[TagEntry]
    interrogate_ok: bool
    error_msg: str
    def __init__(self, tags: _Optional[_Iterable[_Union[TagEntry, _Mapping]]] = ..., interrogate_ok: bool = ..., error_msg: _Optional[str] = ...) -> None: ...
