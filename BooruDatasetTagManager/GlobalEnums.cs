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

    public enum AutocompleteSort
    {
        Alphabetical,
        ByCount
    }

    public enum FilterType
    {
        And,
        Or,
        Not,
        Xor
    }
}
