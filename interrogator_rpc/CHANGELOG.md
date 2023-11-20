# Changelog
All notable changes to this project will be documented in this file.

## 0.0.1 - 20 November 2023
### About
This PR introduces a number of improvements for the Tagger's RPC service, mostly notably the Middleman postprocessor. To use the postprocessor, create `middleman.json` in your `interrogator_rpc` directory.

The Middleman JSON key is a tag name, and the value is a dictionary of settings. For example, `"handshake":{"name":"shaking_hands","confidence":0.75}` will replace the `handshake` tag with `shaking_hands` and force its confidence value to 0.75 (which can be used to influence its placement on the tag list.)

Here is a complete list of the available settings:

- `name`: The new name of the tag that will be returned to the client
- `confidence`: The new confidence value of the tag, applied before the threshold check
- `post_confidence`: The confidence value that will be returned to the client if the original confidence passes the threshold check
- `threshold`: The threshold value to compare the tag's confidence value against, useful if the sensitivity of certain tags doesn't quite match the global threshold value
- `ban`: If `true`, the tag will be removed from the list of tags that will be returned to the client
- `aliases`: Additional tags that will be returned to the client

Additionally, a couple special keys are supported:

- `_BANNED_TAGS` takes a list of tag names to prohibit, e.g. `"_BANNED_TAGS":["handshake","dancing"]`
- `_UNPROMPTED` takes a path to a copy of [Unprompted](https://github.com/ThereforeGames/unprompted), my templating language that transforms plaintext with complex operations, e.g. `"handshake":{"confidence":"[random _min=0.5 _max=0.9]"}`

If using Unprompted, you can reference the original tag properties with `[get]`, e.g. `"handshake":{"confidence":"[max '{get confidence}' 0.75]"}`, which will cap the returned confidence to 0.75.

### Added
- Support for `middleman.json` that enables postprocessing of the returned tags
- `CHANGELOG.md` to track changes
- `run.bat` for Windows users
- Global `DEBUG` flag in for diagnostics in lieu of a proper logger