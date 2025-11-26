using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;
using Directory = MetadataExtractor.Directory;
using Dir = System.IO.Directory;
using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using MetadataExtractor;
using MetadataExtractor.Formats.Jpeg;
using MetadataExtractor.Formats.Png;
using MetadataExtractor.Formats.WebP;
using MetadataExtractor.Formats.QuickTime;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Threading;

namespace Diffusion.IO;

public class Metadata
{
    private static ConcurrentDictionary<string, List<string>> DirectoryTextFileCache = new ConcurrentDictionary<string, List<string>>();

    public static List<string> GetDirectoryTextFileCache(string path)
    {
        var files = Dir.GetFiles(path, "*.txt").ToList();
        // TODO: Fix possible duplicate path
        return DirectoryTextFileCache.AddOrUpdate(path, files, (a, b) =>
        {
            return b.Concat(files).Distinct().ToList();
        });
    }

    public enum MetaFormat
    {
        A1111,
        NovelAI,
        InvokeAI,
        InvokeAINew,
        InvokeAI2,
        EasyDiffusion,
        ComfyUI,
        RuinedFooocus,
        FooocusMRE,
        Fooocus,
        SwarmUI,
        Unknown,
    }

    public enum FileType
    {
        PNG,
        JPEG,
        WebP,
        MP4,
        Other,
    }

    private static byte[] PNGMagic = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
    private static byte[] JPEGMagic = new byte[] { 0xFF, 0xD8, 0xFF };
    private static byte[] RIFFMagic = new byte[] { 0x52, 0x49, 0x46, 0x46 };
    private static byte[] WebPMagic = new byte[] { 0x57, 0x45, 0x42, 0x50 };

    private static FileType GetFileType(Stream stream)
    {
        var buffer = new byte[12];

        stream.Read(buffer, 0, 12);

        var span = buffer.AsSpan();

        if (span.Slice(0, 4).SequenceEqual(PNGMagic))
        {
            return FileType.PNG;
        }
        if (span.Slice(0, 3).SequenceEqual(JPEGMagic) && ((span[3] & 0xE0) == 0xE0))
        {
            return FileType.JPEG;
        }
        if (span.Slice(0, 4).SequenceEqual(RIFFMagic) && span.Slice(8, 4).SequenceEqual(WebPMagic))
        {
            return FileType.WebP;
        }

        return FileType.Other;
    }



    public static FileParameters? ReadFromFile(string file)
    {
        FileParameters? fileParameters = null;

        try
        {
            bool failed = false;
            int retry = 0;
            do
            {
                try
                {
                    fileParameters = Metadata.ReadFromFileInternal(file);
                    failed = false;
                }
                catch (IOException) when (retry < 10)
                {
                    failed = true;
                    Thread.Sleep(500);
                    retry++;
                }
            } while (failed);



        }
        catch (Exception e)
        {
            //Logger.Log($"An error occurred while reading {file}: {e.Message}\r\n\r\n{e.StackTrace}");
            fileParameters ??= new FileParameters();
            fileParameters.HasError = true;
            fileParameters.ErrorMessage = $"{e.Message}\r\n\r\n{e.StackTrace}";
        }
        finally
        {
            fileParameters ??= new FileParameters()
            {
                NoMetadata = true
            };

            FileInfo fileInfo = new FileInfo(file);
            fileParameters.Path = file;
            fileParameters.FileSize = fileInfo.Length;
        }

        return fileParameters;
    }

    public static FileParameters? ReadFromFileInternal(string file)
    {
        FileParameters? fileParameters = null;

        var ext = Path.GetExtension(file).ToLowerInvariant();

        using var stream = new MemoryStream();
        string hash;

        // Read the entire file into memory since we're going to hash it anyway, and also read metadata
        using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            // Custom copy with hash to avoid reading the entire stream twice
            hash = fileStream.CopyAndHash(stream).ToHexString();
        }

        stream.Seek(0, SeekOrigin.Begin);

        // Some PNGs are JPEGs in disguise, read the magic to make sure we have the correct file type
        var fileType = GetFileType(stream);

        if (fileType == FileType.Other)
        {
            if (Path.GetExtension(file).ToLowerInvariant() == ".mp4")
            {
                fileType = FileType.MP4;
            }
        }

        stream.Seek(0, SeekOrigin.Begin);

        // Now, attempt to read the metadata
        switch (fileType)
        {
            case FileType.MP4:
                {
                    break;
                }
            case FileType.PNG:
                {
                    IEnumerable<Directory> directories = PngMetadataReader.ReadMetadata(stream);

                    var format = MetaFormat.Unknown;

                    decimal aestheticScore = 0;

                    //string tagData = null;

                    foreach (var directory in directories)
                    {
                        if (directory.Name == "PNG-tEXt")
                        {
                            foreach (var tag in directory.Tags)
                            {
                                if (tag.Name == "Textual Data")
                                {
                                    if (tag.Description.StartsWith("parameters:"))
                                    {
                                        if (fileParameters == null)
                                        {
                                            var isJson = tag.Description.Substring("parameters: ".Length).Trim().StartsWith("{");
                                            if (isJson)
                                            {
                                                if (tag.Description.Contains("sui_image_params"))
                                                {
                                                    fileParameters = ReadStableSwarmParameters(tag.Description);
                                                    format = MetaFormat.SwarmUI;
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        fileParameters = ReadRuinedFooocusParameters(tag.Description);
                                                        format = MetaFormat.RuinedFooocus;
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        fileParameters = ReadA111Parameters(tag.Description);
                                                        format = MetaFormat.A1111;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                fileParameters = ReadA111Parameters(tag.Description);
                                                format = MetaFormat.A1111;
                                            }
                                        }
                                    }
                                    else if (tag.Description.StartsWith("Comment:"))
                                    {

                                        if (fileParameters == null)
                                        {
                                            //if (directory.Tags.Any(t => t.Description == "Software: NovelAI"))
                                            if (directory.Tags.Any(t => t.Description == "Software: NovelAI") || directories.Any(d => d.Tags.Any(t => t.Description == "Software: NovelAI")))
                                            {
                                                format = MetaFormat.NovelAI;
                                                fileParameters = ReadNovelAIParameters(file, directories);
                                            }
                                            else
                                            {
                                                format = MetaFormat.FooocusMRE;
                                                fileParameters = ReadFooocusMREParameters(tag.Description);
                                            }
                                        }
                                    }
                                    else if (tag.Description == "Software: NovelAI")
                                    {
                                        if (fileParameters == null)
                                        {
                                            format = MetaFormat.NovelAI;
                                            fileParameters = ReadNovelAIParameters(file, directories);
                                        }
                                    }
                                    else if (tag.Description.StartsWith("Dream: "))
                                    {
                                        format = MetaFormat.InvokeAI;
                                        fileParameters = ReadInvokeAIParameters(file, tag.Description);
                                    }
                                    else if (tag.Description.StartsWith("sd-metadata: "))
                                    {
                                        format = MetaFormat.InvokeAINew;
                                        fileParameters = ReadInvokeAIParametersNew(file, tag.Description);
                                    }
                                    else if (tag.Description.StartsWith("invokeai_metadata: "))
                                    {
                                        format = MetaFormat.InvokeAI2;
                                        fileParameters = ReadInvokeAIParameters2(file, tag.Description);
                                    }
                                    else if (tag.Description.StartsWith("prompt: "))
                                    {
                                        var isJson = tag.Description.Substring("prompt: ".Length).Trim().StartsWith("{");
                                        format = isJson ? MetaFormat.ComfyUI : MetaFormat.EasyDiffusion;
                                        var tempParameters = isJson ? ReadComfyUIParameters(tag.Description) : ReadEasyDiffusionParameters(file, directories);

                                        if (fileParameters == null)
                                        {
                                            fileParameters = tempParameters;
                                        }
                                        else
                                        {
                                            fileParameters.WorkflowId = tempParameters.WorkflowId;
                                            fileParameters.Workflow = tempParameters.Workflow;
                                            fileParameters.Nodes = tempParameters.Nodes;
                                        }
                                    }
                                    else if (tag.Description.StartsWith("Score:"))
                                    {
                                        decimal.TryParse(tag.Description[6..], NumberStyles.Any, CultureInfo.InvariantCulture, out aestheticScore);
                                    }
                                    else if (tag.Description.StartsWith("aesthetic_score:"))
                                    {
                                        decimal.TryParse(tag.Description[16..], NumberStyles.Any, CultureInfo.InvariantCulture, out aestheticScore);
                                    }

                                }
                            }
                        }
                        else if (directory.Name == "PNG-iTXt")
                        {
                            foreach (var tag in directory.Tags)
                            {
                                if (tag.Name == "Textual Data" && tag.Description.StartsWith("parameters:"))
                                {
                                    format = MetaFormat.A1111;
                                    fileParameters = ReadA111Parameters(tag.Description);
                                }
                            }

                        }
                        else if (directory.Name == "Exif SubIFD")
                        {
                            foreach (var tag in directory.Tags)
                            {
                                if (tag.Name == "User Comment")
                                {
                                    format = MetaFormat.A1111;
                                    fileParameters = ReadA111Parameters(tag.Description);
                                }
                            }
                        }
                    }


                    if (aestheticScore > 0)
                    {
                        fileParameters ??= new FileParameters();
                        fileParameters.AestheticScore = aestheticScore;
                        if (fileParameters.OtherParameters == null)
                        {
                            fileParameters.OtherParameters = $"aesthetic_score: {fileParameters.AestheticScore}";
                        }
                        else
                        {
                            fileParameters.OtherParameters += $"\naesthetic_score: {fileParameters.AestheticScore}";
                        }
                    }


                    if (fileParameters != null && (fileParameters.Width == 0 || fileParameters.Height == 0))
                    {
                        var imageMetaData = directories.FirstOrDefault(d => d.Name == "PNG-IHDR");

                        foreach (var tag in imageMetaData.Tags)
                        {
                            if (tag.Name == "Image Width")
                            {
                                fileParameters.Width = int.Parse(tag.Description);
                            }
                            else if (tag.Name == "Image Height")
                            {
                                fileParameters.Height = int.Parse(tag.Description);
                            }
                        }
                    }

                    break;
                }

            case FileType.JPEG:
                {
                    var format = MetaFormat.Unknown;

                    IEnumerable<Directory> directories = JpegMetadataReader.ReadMetadata(stream);

                    try
                    {
                        var isFoocus = false;
                        foreach (var directory in directories)
                        {
                            if (directory.Name == "Exif SubIFD")
                            {
                                foreach (var tag in directory.Tags)
                                {
                                    switch (tag.Name)
                                    {
                                        case "User Comment":
                                            var description = tag.Description;
                                            if (description.Contains("sui_image_params"))
                                            {
                                                fileParameters = ReadStableSwarmParameters(description);
                                            }
                                            break;
                                    }
                                }
                            }
                            else if (directory.Name == "Exif IFD0")
                            {
                                foreach (var tag in directory.Tags)
                                {
                                    switch (tag.Name)
                                    {
                                        case "Software" when tag.Description.StartsWith("Fooocus"):
                                            isFoocus = true;
                                            break;

                                        case "Makernote":
                                            if (isFoocus)
                                            {
                                                format = tag.Description switch
                                                {
                                                    "fooocus" => MetaFormat.Fooocus,
                                                    "a1111" => MetaFormat.A1111,
                                                    _ => format
                                                };
                                            }
                                            break;

                                        case "User Comment":
                                            if (isFoocus)
                                            {
                                                fileParameters = format switch
                                                {
                                                    MetaFormat.Fooocus => ReadFooocusParameters(tag.Description),
                                                    MetaFormat.A1111 => ReadA111Parameters(tag.Description),
                                                    _ => fileParameters
                                                };
                                            }
                                            else
                                            {
                                                if (tag.Description.StartsWith("{\"prompt\":"))
                                                {
                                                    format = MetaFormat.ComfyUI;
                                                    var tempParameters =  ReadComfyUIParameters(tag.Description, true);

                                                    if (fileParameters == null)
                                                    {
                                                        fileParameters = tempParameters;
                                                    }
                                                    else
                                                    {
                                                        fileParameters.WorkflowId = tempParameters.WorkflowId;
                                                        fileParameters.Workflow = tempParameters.Workflow;
                                                        fileParameters.Nodes = tempParameters.Nodes;
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }


                            }
                        }

                        if (fileParameters == null)
                        {
                            format = MetaFormat.A1111;

                            fileParameters = ReadAutomatic1111Parameters(file, directories);
                        }
                    }
                    catch (Exception e)
                    {
                        //Logger.Log($"An error occurred while reading {file}: {e.Message}\r\n\r\n{e.StackTrace}");
                    }

                    break;
                }

            case FileType.WebP:
                {
                    IEnumerable<Directory> directories = WebPMetadataReader.ReadMetadata(stream);

                    try
                    {
                        fileParameters = ReadAutomatic1111Parameters(file, directories);
                    }
                    catch (Exception e)
                    {
                        //Logger.Log($"An error occurred while reading {file}: {e.Message}\r\n\r\n{e.StackTrace}");
                    }

                    break;
                }
        }

        // Nothing matched and we still don't have any metadata
        if (fileParameters == null)
        {
            try
            {
                // Check if there is a .TXT metadata file (Automatic1111)
                var parameterFile = file.Replace(ext, ".txt", StringComparison.InvariantCultureIgnoreCase);

                if (File.Exists(parameterFile))
                {
                    var parameters = File.ReadAllText(parameterFile);
                    fileParameters = DetectAndReadMetaType(parameters);
                }
                else
                {
                    var currPath = Path.GetDirectoryName(parameterFile);
                    var textFiles = GetDirectoryTextFileCache(currPath);

                    var matchingFile = textFiles.FirstOrDefault(t => Path.GetFileNameWithoutExtension(parameterFile).StartsWith(Path.GetFileNameWithoutExtension(t)));

                    if (matchingFile != null)
                    {
                        var parameters = File.ReadAllText(matchingFile);
                        fileParameters = DetectAndReadMetaType(parameters);
                    }
                }

            }
            catch (Exception e)
            {
                //Logger.Log($"An error occurred while reading {file}: {e.Message}\r\n\r\n{e.StackTrace}");
            }
        }

        // Try to read from Stealth Alpha channel if PNG
        if (fileParameters == null && fileType == FileType.PNG)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var metadata = StealthPng.Read(stream);
            fileParameters = ReadA111Parameters(metadata);
        }

        if (fileParameters == null)
        {
            fileParameters = new FileParameters();
            fileParameters.NoMetadata = true;
            fileParameters.Hash = hash;
        }

        stream.Seek(0, SeekOrigin.Begin);

        var (width, height) = GetImageSize(stream);

        fileParameters.Width = width;
        fileParameters.Height = height;

        return fileParameters;
    }


    public static (int width, int height) GetImageSize(Stream stream)
    {
        ImageInfo info = Image.Identify(stream);
        return (info.Width, info.Height);
    }

    private static FileParameters DetectAndReadMetaType(string parameters)
    {
        if (IsStableDiffusion(parameters))
        {
            return ReadStableDiffusionParameters(parameters);
        }
        else
        {
            return ReadA111Parameters(parameters);
        }
    }

    private static bool IsStableDiffusion(string metadata)
    {
        return metadata.Contains("\nWidth:") && metadata.Contains("\nHeight:") && metadata.Contains("\nSeed:");
    }

    private static FileParameters ReadInvokeAIParameters(string file, string description)
    {
        var command = description.Substring("Dream: ".Length);

        var fp = new FileParameters();

        fp.WorkflowId = command.GetHashCode().ToString("X");
        fp.Workflow = command;

        var start = command.IndexOf("\"");
        var end = command.IndexOf("\"", start + 1);
        fp.Prompt = command.Substring(start + 1, end - start - 1);
        var others = command.Substring(end + 1);
        var args = others.Split(new char[] { ' ' });
        for (var index = 0; index < args.Length; index++)
        {
            var arg = args[index];
            switch (arg)
            {
                case "-s":
                    fp.Steps = int.Parse(args[index + 1], CultureInfo.InvariantCulture);
                    index++;
                    break;
                case "-S":
                    fp.Seed = long.Parse(args[index + 1], CultureInfo.InvariantCulture);
                    index++;
                    break;
                case "-W":
                    fp.Width = int.Parse(args[index + 1], CultureInfo.InvariantCulture);
                    index++;
                    break;
                case "-H":
                    fp.Height = int.Parse(args[index + 1], CultureInfo.InvariantCulture);
                    index++;
                    break;
                case "-C":
                    fp.CFGScale = decimal.Parse(args[index + 1], CultureInfo.InvariantCulture);
                    index++;
                    break;
                case "-A":
                    fp.Sampler = args[index + 1];
                    index++;
                    break;
            }
        }

        fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Size: {fp.Width}x{fp.Height}";

        return fp;
    }



    private static FileParameters ReadEasyDiffusionParameters(string file, IEnumerable<Directory> directories)
    {
        var fp = new FileParameters();

        var workflowBuilder = new StringBuilder();

        string? GetTag(string key)
        {
            if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith($"{key}: "), out var tag))
            {
                workflowBuilder.AppendLine(tag.Description);
                return tag.Description.Substring($"{key}: ".Length);
            }

            return null;
        }

        float GetFloatTag(string key)
        {
            if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith($"{key}: "), out var tag))
            {
                workflowBuilder.AppendLine(tag.Description);
                var value = tag.Description.Substring($"{key}: ".Length);

                return float.Parse(value);
            }

            return 0f;
        }

        decimal GetDecimalTag(string key)
        {
            if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith($"{key}: "), out var tag))
            {
                workflowBuilder.AppendLine(tag.Description);
                var value = tag.Description.Substring($"{key}: ".Length);

                return decimal.Parse(value);
            }

            return 0m;
        }

        long GetLongTag(string key)
        {
            if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith($"{key}: "), out var tag))
            {
                workflowBuilder.AppendLine(tag.Description);
                var value = tag.Description.Substring($"{key}: ".Length);

                return long.Parse(value);
            }

            return 0;
        }

        int GetIntTag(string key)
        {
            if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith($"{key}: "), out var tag))
            {
                workflowBuilder.AppendLine(tag.Description);
                var value = tag.Description.Substring($"{key}: ".Length);

                return int.Parse(value);
            }

            return 0;
        }

        fp.Prompt = GetTag("prompt");
        fp.NegativePrompt = GetTag("negative_prompt");
        fp.Width = GetIntTag("width");
        fp.Height = GetIntTag("height");
        fp.Steps = GetIntTag("num_inference_steps");
        fp.CFGScale = GetDecimalTag("guidance_scale");
        fp.Seed = GetLongTag("seed");
        fp.Sampler = GetTag("sampler_name");
        fp.Model = GetTag("use_stable_diffusion_model");


        fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Seed: {fp.Seed} Size: {fp.Width}x{fp.Height}";

        var workflow = workflowBuilder.ToString();

        fp.WorkflowId = workflow.GetHashCode().ToString("X");
        fp.Workflow = workflow;

        //    return fp;

        return fp;
    }

    private static int GetHashCode(JsonElement node)
    {
        int result = 0;
        if (node.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in node.EnumerateArray())
            {
                if (element.ValueKind == JsonValueKind.Object)
                {
                    result = (result * 397) ^ GetHashCode(element);
                }
            }
        }
        else if (node.ValueKind == JsonValueKind.Object)
        {
            unchecked
            {
                foreach (var property in node.EnumerateObject())
                {
                    var hash = property.Name.GetHashCode();
                    result = (result * 397) ^ hash;
                    result = (result * 397) ^ GetHashCode(property.Value);
                }
                return result;
            }
        }
        return 0;
    }



    private static FileParameters ReadComfyUIParameters(string description, bool isProperJson = false)
    {
        var fp = new FileParameters();

        try
        {
            if (!isProperJson)
            {
                var json = description.Substring("prompt: ".Length);

                // fix for errant nodes
                json = json.Replace("NaN", "null");

                fp.Workflow = json;

                var root = JsonDocument.Parse(json);
                fp.WorkflowId = GetHashCode(root.RootElement).ToString("X");

                var parser = new ComfyUIParser();
                var pnodes = parser.Parse(fp.WorkflowId, fp.Workflow);

                fp.Nodes = pnodes;
            }
            else
            {
                fp.Workflow = description;

                var parser = new ComfyUIParser();
                var pnodes = parser.Parse(fp.WorkflowId, fp.Workflow);

                fp.Nodes = pnodes;
            }


            return fp;
        }
        catch
        {
            return fp;
        }
    }

    private static FileParameters ReadInvokeAIParametersNew(string file, string description)
    {
        var json = description.Substring("sd-metadata: ".Length);

        var fp = new FileParameters();
        fp.WorkflowId = json.GetHashCode().ToString("X");
        fp.Workflow = json;

        var root = JsonDocument.Parse(json);
        var image = root.RootElement.GetProperty("image");
        var prompt = image.GetProperty("prompt");
        if (prompt.ValueKind == JsonValueKind.Array)
        {
            var promptArrayEnumerator = prompt.EnumerateArray();
            promptArrayEnumerator.MoveNext();
            var promptObject = promptArrayEnumerator.Current;
            fp.Prompt = promptObject.GetProperty("prompt").GetString();
            fp.PromptStrength = promptObject.GetProperty("weight").GetDecimal();
        }
        else if (prompt.ValueKind == JsonValueKind.String)
        {
            fp.Prompt = prompt.GetString();
        }

        fp.ModelHash = root.RootElement.GetProperty("model_hash").GetString();
        fp.Steps = image.GetProperty("steps").GetInt32();
        fp.CFGScale = image.GetProperty("cfg_scale").GetDecimal();
        fp.Height = image.GetProperty("height").GetInt32();
        fp.Width = image.GetProperty("width").GetInt32();
        fp.Seed = image.GetProperty("seed").GetInt64();
        fp.Sampler = image.GetProperty("sampler").GetString();

        fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Seed: {fp.Seed} Size: {fp.Width}x{fp.Height}";

        return fp;
    }

    private static FileParameters ReadInvokeAIParameters2(string file, string description)
    {
        var json = description.Substring("invokeai_metadata: ".Length);

        var fp = new FileParameters();
        fp.WorkflowId = json.GetHashCode().ToString("X");
        fp.Workflow = json;

        var root = JsonDocument.Parse(json);
        var image = root.RootElement;

        fp.Prompt = image.GetProperty("positive_prompt").GetString();
        fp.NegativePrompt = image.GetProperty("negative_prompt").GetString();
        fp.Steps = image.GetProperty("steps").GetInt32();
        fp.CFGScale = image.GetProperty("cfg_scale").GetDecimal();
        fp.Height = image.GetProperty("height").GetInt32();
        fp.Width = image.GetProperty("width").GetInt32();
        fp.Seed = image.GetProperty("seed").GetInt64();
        fp.Sampler = image.GetProperty("scheduler").GetString();

        fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Seed: {fp.Seed} Size: {fp.Width}x{fp.Height}";

        return fp;
    }


    private static FileParameters ReadNovelAIParameters(string file, IEnumerable<Directory> directories)
    {
        var fileParameters = new FileParameters();

        fileParameters.Path = file;

        Tag tag;

        var workflowBuilder = new StringBuilder();

        if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith("Description:"), out tag))
        {
            workflowBuilder.AppendLine(tag.Description);
            fileParameters.Prompt = tag.Description.Substring("Description: ".Length);
        }

        if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith("Source:"), out tag))
        {
            var hashRegex = new Regex("[0-9A-F]{8}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var match = hashRegex.Match(tag.Description.Substring("Source: ".Length));
            if (match.Success)
            {
                workflowBuilder.AppendLine(tag.Description);
                fileParameters.ModelHash = match.Groups[0].Value;
            }
        }

        var propList = new List<string>();


        if (TryFindTag(directories, "PNG-tEXt", "Textual Data", tag => tag.Description.StartsWith("Comment:"), out tag))
        {
            workflowBuilder.AppendLine(tag.Description);

            var json = JsonDocument.Parse(tag.Description.Substring("Comment: ".Length));

            fileParameters.Steps = json.RootElement.GetProperty("steps").GetInt32();
            fileParameters.Sampler = json.RootElement.GetProperty("sampler").GetString();
            fileParameters.Seed = json.RootElement.GetProperty("seed").GetInt64();
            fileParameters.CFGScale = json.RootElement.GetProperty("scale").GetDecimal();

            var properties = json.RootElement.EnumerateObject();

            foreach (var property in properties)
            {
                if (property.Name != "uc" && property.Name != "prompt")
                {
                    propList.Add($"{property.Name}: {property.Value.ToString()}");
                }
            }


            fileParameters.NegativePrompt = json.RootElement.GetProperty("uc").GetString();
        }

        propList.Add($"model hash: {fileParameters.ModelHash}");

        fileParameters.OtherParameters = string.Join(", ", propList);

        var pngIHDR = directories.FirstOrDefault(d => d.Name == "PNG-IHDR");

        if (pngIHDR != null)
        {
            foreach (var ptag in pngIHDR.Tags)
            {
                switch (ptag.Name)
                {
                    case "Image Width":
                        fileParameters.Width = int.Parse(ptag.Description, CultureInfo.InvariantCulture);
                        break;
                    case "Image Height":
                        fileParameters.Height = int.Parse(ptag.Description, CultureInfo.InvariantCulture);
                        break;
                }
            }
        }


        var workflow = workflowBuilder.ToString();
        fileParameters.WorkflowId = workflow.GetHashCode().ToString("X");
        fileParameters.Workflow = workflow;


        return fileParameters;
    }

    private static FileParameters ReadStableDiffusionParameters(string data)
    {
        var fileParameters = new FileParameters();

        fileParameters.WorkflowId = data.GetHashCode().ToString("X");
        fileParameters.Workflow = data;

        var parts = data.Split(new[] { '\n' });

        const string negativePromptKey = "Negative prompt: ";
        const string modelKey = "Stable Diffusion model: ";
        const string widthKey = "Width: ";

        fileParameters.Parameters = data;

        var state = 0;

        fileParameters.Prompt = "";
        fileParameters.NegativePrompt = "";

        string otherParam = "";

        foreach (var part in parts)
        {
            var isNegativePrompt = part.StartsWith(negativePromptKey, StringComparison.InvariantCultureIgnoreCase);
            var isModel = part.StartsWith(modelKey, StringComparison.InvariantCultureIgnoreCase);
            var isWidth = part.StartsWith(widthKey, StringComparison.InvariantCultureIgnoreCase);

            if (isWidth)
            {
                state = 1;
            }
            else if (isNegativePrompt)
            {
                state = 2;
            }
            else if (isModel)
            {
                state = 3;
            }

            switch (state)
            {
                case 0:
                    fileParameters.Prompt += part + "\n";
                    break;
                case 2:
                    if (isNegativePrompt)
                    {
                        fileParameters.NegativePrompt += part.Substring(negativePromptKey.Length);
                    }
                    else
                    {
                        fileParameters.NegativePrompt += part + "\n";
                    }
                    break;
                case 1:


                    var subParts = part.Split(new[] { ',' });
                    foreach (var keyValue in subParts)
                    {
                        var kvp = keyValue.Split(new[] { ':' });
                        switch (kvp[0].Trim())
                        {
                            case "Steps":
                                fileParameters.Steps = int.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                break;
                            case "Sampler":
                                fileParameters.Sampler = kvp[1].Trim();
                                break;
                            case "Guidance Scale":
                                fileParameters.CFGScale = decimal.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                break;
                            case "Seed":
                                fileParameters.Seed = long.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                break;
                            case "Width":
                                fileParameters.Width = int.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                break;
                            case "Height":
                                fileParameters.Height = int.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                break;
                            case "Prompt Strength":
                                fileParameters.PromptStrength = decimal.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                break;
                                //case "Model hash":
                                //    fileParameters.ModelHash = kvp[1].Trim();
                                //    break;
                                //case "Batch size":
                                //    fileParameters.BatchSize = int.Parse(kvp[1].Trim());
                                //    break;
                                //case "Hypernet":
                                //    fileParameters.HyperNetwork = kvp[1].Trim();
                                //    break;
                                //case "Hypernet strength":
                                //    fileParameters.HyperNetworkStrength = decimal.Parse(kvp[1].Trim());
                                //    break;
                                //case "aesthetic_score":
                                //    fileParameters.AestheticScore = decimal.Parse(kvp[1].Trim());
                                //    break;
                        }
                    }

                    otherParam += part + "\n";

                    break;
                case 3:
                    otherParam += part + "\n";
                    break;
            }

        }

        fileParameters.OtherParameters = otherParam;

        return fileParameters;
    }

    private static FileParameters ReadFooocusMREParameters(string data)
    {
        var fp = new FileParameters();
        var json = data.Substring("Comment: ".Length);
        var root = JsonDocument.Parse(json);

        fp.WorkflowId = json.GetHashCode().ToString("X");
        fp.Workflow = json;

        fp.Prompt = root.RootElement.GetProperty("prompt").GetString();

        string real_prompt;

        try
        {
            var real_prompt_array = root.RootElement.GetProperty("real_prompt").EnumerateArray();
            real_prompt = string.Join(", ", real_prompt_array);

            if (!string.IsNullOrEmpty(real_prompt))
            {
                if (!string.IsNullOrEmpty(fp.Prompt))
                    fp.Prompt += ", ";

                fp.Prompt += real_prompt;
            }
        }
        catch
        {
            // Ignore real_prompt if it fails
        }

        fp.NegativePrompt = root.RootElement.GetProperty("negative_prompt").GetString();

        string real_negative_prompt;

        try
        {
            var real_negative_prompt_array = root.RootElement.GetProperty("real_negative_prompt").EnumerateArray();
            real_negative_prompt = string.Join(", ", real_negative_prompt_array);

            if (!string.IsNullOrEmpty(real_negative_prompt))
            {
                if (!string.IsNullOrEmpty(fp.NegativePrompt))
                    fp.NegativePrompt += ", ";

                fp.NegativePrompt += real_negative_prompt;
            }
        }
        catch
        {
            // Ignore real_negative_prompt if it fails
        }

        fp.PromptStrength = root.RootElement.GetProperty("positive_prompt_strength").GetDecimal();
        fp.Steps = root.RootElement.GetProperty("steps").GetInt32();
        fp.CFGScale = root.RootElement.GetProperty("cfg").GetDecimal();
        fp.Height = root.RootElement.GetProperty("height").GetInt32();
        fp.Width = root.RootElement.GetProperty("width").GetInt32();
        fp.Seed = root.RootElement.GetProperty("seed").GetInt64();
        fp.Sampler = root.RootElement.GetProperty("sampler").GetString();
        fp.Model = root.RootElement.GetProperty("base_model").GetString();

        fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Size: {fp.Width}x{fp.Height}";

        return fp;
    }


    private static FileParameters ReadStableSwarmParameters(string data)
    {
        if (data.StartsWith("parameters: "))
        {
            data = data.Substring("parameters: ".Length);
        }

        var json = JsonDocument.Parse(data);

        var fp = new FileParameters();
        fp.WorkflowId = data.GetHashCode().ToString("X");
        fp.Workflow = data;

        var root = json.RootElement;


        var suiRoot = root.GetProperty("sui_image_params");

        fp.Prompt = suiRoot.GetProperty("prompt").GetString();
        fp.NegativePrompt = suiRoot.GetProperty("negativeprompt").GetString();
        fp.Steps = suiRoot.GetProperty("steps").GetInt32();
        fp.CFGScale = suiRoot.GetProperty("cfgscale").GetDecimal();

        //var resolution = root.GetProperty("resolution").GetString();
        //resolution = resolution.Substring(1, resolution.Length - 2);
        //var parts = resolution.Split(new[] { ',' }, StringSplitOptions.TrimEntries);

        fp.Width = suiRoot.GetProperty("width").GetInt32();
        fp.Height = suiRoot.GetProperty("height").GetInt32();
        fp.Seed = suiRoot.GetProperty("seed").GetInt64();

        if (suiRoot.TryGetProperty("sampler", out var sampler))
        {
            fp.Sampler = sampler.GetString();
        }

        if (suiRoot.TryGetProperty("model", out var model))
        {
            fp.Model = model.GetString();
        }

        //fp.ModelHash = root.GetProperty("base_model_hash").GetString();
        fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Seed: {fp.Seed} Size: {fp.Width}x{fp.Height}";

        return fp;
    }

    private static FileParameters ReadFooocusParameters(string data)
    {
        var fp = new FileParameters();

        fp.WorkflowId = data.GetHashCode().ToString("X");
        fp.Workflow = data;

        var json = JsonDocument.Parse(data);

        var root = json.RootElement;

        var fullPrompt = root.GetProperty("full_prompt").EnumerateArray().Select(x => x.GetString());
        var fullNegativePrompt = root.GetProperty("full_negative_prompt").EnumerateArray().Select(x => x.GetString());

        fp.Prompt = string.Join("\r\n", fullPrompt);
        fp.NegativePrompt = string.Join("\r\n", fullNegativePrompt);
        fp.Steps = root.GetProperty("steps").GetInt32();
        fp.CFGScale = root.GetProperty("guidance_scale").GetDecimal();

        var resolution = root.GetProperty("resolution").GetString();
        resolution = resolution.Substring(1, resolution.Length - 2);
        var parts = resolution.Split(new[] { ',' }, StringSplitOptions.TrimEntries);

        fp.Width = int.Parse(parts[0]);
        fp.Height = int.Parse(parts[1]);
        fp.Seed = root.GetProperty("seed").GetInt64();
        fp.Sampler = root.GetProperty("sampler").GetString();
        fp.Model = root.GetProperty("base_model").GetString();
        fp.ModelHash = root.GetProperty("base_model_hash").GetString();
        fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Seed: {fp.Seed} Size: {fp.Width}x{fp.Height}";

        return fp;
    }

    private static FileParameters ReadRuinedFooocusParameters(string data)
    {
        var parameters = data.Substring("parameters: ".Length);
        var json = JsonDocument.Parse(parameters);

        var root = json.RootElement;

        var fp = new FileParameters();


        if (root.TryGetProperty("software", out var softwareProperty))
        {
            fp.WorkflowId = parameters.GetHashCode().ToString("X");
            fp.Workflow = parameters;

            var software = softwareProperty.GetString();

            if (software == "RuinedFooocus")
            {
                fp.Prompt = root.GetProperty("Prompt").GetString();
                fp.NegativePrompt = root.GetProperty("Negative").GetString();
                fp.Steps = root.GetProperty("steps").GetInt32();
                fp.CFGScale = root.GetProperty("cfg").GetDecimal();
                fp.Width = root.GetProperty("width").GetInt32();
                fp.Height = root.GetProperty("height").GetInt32();
                fp.Seed = root.GetProperty("seed").GetInt64();
                fp.Sampler = root.GetProperty("sampler_name").GetString();
                fp.Model = root.GetProperty("base_model_name").GetString();
                fp.ModelHash = root.GetProperty("base_model_hash").GetString();
                fp.OtherParameters = $"Steps: {fp.Steps} Sampler: {fp.Sampler} CFG Scale: {fp.CFGScale} Seed: {fp.Seed} Size: {fp.Width}x{fp.Height}";
            }
        }
        else
        {
            fp = ReadFooocusParameters(data.Substring("parameters: ".Length));
        }



        return fp;
    }

    private static FileParameters ReadA111Parameters(string data)
    {
        var fileParameters = new FileParameters();

        var parts = data.Split(new[] { '\n' });

        const string parametersKey = "parameters:";
        const string negativePromptKey = "Negative prompt:";
        const string stepsKey = "Steps:";

        fileParameters.WorkflowId = data.GetHashCode().ToString("X");
        fileParameters.Workflow = data;
        fileParameters.Parameters = data;

        var state = 0;

        fileParameters.Prompt = "";
        fileParameters.NegativePrompt = "";

        foreach (var part in parts)
        {
            var isNegativePrompt = part.StartsWith(negativePromptKey, StringComparison.InvariantCultureIgnoreCase);
            var isPromptStart = part.StartsWith(parametersKey, StringComparison.InvariantCultureIgnoreCase);
            var isOther = part.StartsWith(stepsKey, StringComparison.InvariantCultureIgnoreCase);

            if (isPromptStart)
            {
                state = 0;
            }
            else if (isNegativePrompt)
            {
                state = 1;
            }
            else if (isOther)
            {
                state = 2;
            }

            switch (state)
            {
                case 0:
                    if (isPromptStart)
                    {
                        fileParameters.Prompt += part.Substring(parametersKey.Length + 1) + "\n";
                    }
                    else
                    {
                        fileParameters.Prompt += part + "\n";
                    }
                    break;
                case 1:
                    if (isNegativePrompt)
                    {
                        fileParameters.NegativePrompt += part.Substring(negativePromptKey.Length);
                    }
                    else
                    {
                        fileParameters.NegativePrompt += part + "\n";
                    }
                    break;
                case 2:

                    fileParameters.OtherParameters = part;

                    //var decimalFormatter = new DecimalFormatter();

                    var subParts = part.Split(new[] { ',' });
                    foreach (var keyValue in subParts)
                    {
                        var kvp = keyValue.Split(new[] { ':' });
                        try
                        {
                            switch (kvp[0].Trim())
                            {
                                case "Steps":
                                    fileParameters.Steps = int.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                    break;
                                case "Sampler":
                                    fileParameters.Sampler = kvp[1].Trim();
                                    break;
                                case "CFG scale":
                                    fileParameters.CFGScale = decimal.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                    break;
                                case "Seed":
                                    fileParameters.Seed = long.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                    break;
                                case "Size":
                                    var size = kvp[1].Split(new[] { 'x' });
                                    fileParameters.Width = int.Parse(size[0].Trim(), CultureInfo.InvariantCulture);
                                    fileParameters.Height = int.Parse(size[1].Trim(), CultureInfo.InvariantCulture);
                                    break;
                                case "Model hash":
                                    fileParameters.ModelHash = kvp[1].Trim();
                                    break;
                                case "Model":
                                    fileParameters.Model = kvp[1].Trim();
                                    break;
                                case "Batch size":
                                    fileParameters.BatchSize = int.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                    break;
                                case "Hypernet":
                                    fileParameters.HyperNetwork = kvp[1].Trim();
                                    break;
                                case "Hypernet strength":
                                    fileParameters.HyperNetworkStrength = decimal.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                    break;
                                case "aesthetic_score":
                                case "Score":
                                    fileParameters.AestheticScore = decimal.Parse(kvp[1].Trim(), CultureInfo.InvariantCulture);
                                    break;
                            }
                        }
                        catch (Exception)
                        {

                        }

                    }

                    state = 3;

                    break;
            }

        }

        return fileParameters;
    }

    private static FileParameters ReadAutomatic1111Parameters(string file, IEnumerable<Directory> directories)
    {
        FileParameters fileParameters = null;

        var ext = Path.GetExtension(file).ToLower();

        //var parameters = directories.FirstOrDefault(d => d.Name == "PNG-tEXt")?.Tags
        //    .FirstOrDefault(t => t.Name == "Textual Data")?.Description;

        decimal aestheticScore = 0m;


        foreach (var directory in directories)
        {
            if (directory.Name == "PNG-tEXt")
            {
                foreach (var tag in directory.Tags)
                {
                    if (tag.Name == "Textual Data")
                    {
                        if (tag.Description.StartsWith("parameters:"))
                        {
                            fileParameters = ReadA111Parameters(tag.Description);
                        }
                        else if (tag.Description.StartsWith("Score:"))
                        {
                            decimal.TryParse(tag.Description[6..], NumberStyles.Any, CultureInfo.InvariantCulture, out aestheticScore);
                        }
                        else if (tag.Description.StartsWith("aesthetic_score:"))
                        {
                            decimal.TryParse(tag.Description[16..], NumberStyles.Any, CultureInfo.InvariantCulture, out aestheticScore);
                        }
                    }
                }
            }
            else if (directory.Name == "PNG-iTXt")
            {
                foreach (var tag in directory.Tags)
                {
                    if (tag.Name == "Textual Data" && tag.Description.StartsWith("parameters:"))
                    {
                        fileParameters = ReadA111Parameters(tag.Description);
                    }
                }

            }
            else if (directory.Name == "Exif SubIFD")
            {
                foreach (var tag in directory.Tags)
                {
                    if (tag.Name == "User Comment")
                    {
                        fileParameters = ReadA111Parameters(tag.Description);
                    }
                }
            }

        }


        if (fileParameters == null)
        {
            var parameterFile = file.Replace(ext, ".txt", StringComparison.InvariantCultureIgnoreCase);

            if (File.Exists(parameterFile))
            {
                var parameters = File.ReadAllText(parameterFile);
                fileParameters = ReadA111Parameters(parameters);
            }
        }



        if (aestheticScore > 0)
        {
            fileParameters ??= new FileParameters();
            fileParameters.AestheticScore = aestheticScore;
            if (fileParameters.OtherParameters == null)
            {
                fileParameters.OtherParameters = $"aesthetic_score: {fileParameters.AestheticScore}";
            }
            else
            {
                fileParameters.OtherParameters += $"\naesthetic_score: {fileParameters.AestheticScore}";
            }
        }

        return fileParameters;
    }


    private static bool TryFindTag(IEnumerable<Directory> directories, string directoryName, string tagName, Func<Tag, bool> matchTag, out Tag foundTag)
    {
        foreach (var directory in directories)
        {
            if (directory.Name == directoryName)
            {
                foreach (var tag in directory.Tags)
                {
                    if (tag.Name == tagName)
                    {
                        if (matchTag(tag))
                        {
                            foundTag = tag;
                            return true;
                        }
                    }
                }
            }
        }
        foundTag = null;
        return false;
    }
}
