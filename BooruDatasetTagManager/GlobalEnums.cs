using System;
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

    public enum AutocompleteMode_ZH_CN
    {
        不使用,
        前缀字母符合,
        前缀字母符合或者包含,
        前缀字母符合或者前缀翻译符合,
        字母包括或者翻译包含
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

    public enum AutocompleteSort_ZH_CN
    {
        字母,
        频率
    }

    public enum FilterType
    {
        And,
        Or,
        Not,
        Xor
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
        OnlyNewWithAddition
    }

    public enum DataSourceType
    {
        None,
        Single,
        Multi
    }
}
