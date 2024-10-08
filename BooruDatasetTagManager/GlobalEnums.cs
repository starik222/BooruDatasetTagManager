﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public enum TranslationService
    {
        GoogleTranslate,
        ChineseTranslate
    }

    public enum AutocompleteMode
    {
        Disable,
        StartWith,
        StartWithAndContains,
        StartWithIncludeTranslations,
        StartWithAndContainsIncludeTranslations
    }

    public enum AutocompleteSort
    {
        Alphabetical,
        ByCount
    }

    public enum AutoTaggerSort
    {
        None,
        Confidence,
        Alphabetical
    }


    public enum FilterType
    {
        And,
        Or,
        Not,
        Xor
    }

    public enum ImagePreviewType
    {
        PreviewInMainWindow,
        SeparateWindow
    }

    public enum NetworkUnionMode
    {
        Addition,
        Intersection,
        Subtraction
    }

    public enum NetworkResultSetMode
    {
        AllWithReplacement,
        OnlyNewWithAddition,
        SkipExistTagList
    }

    public enum TagFilteringMode
    {
        None,
        Equal,
        Containing,
        NotEqual,
        NotContaining,
        Regex
    }

    public enum DataSourceType
    {
        None,
        Single,
        Multi
    }
}
