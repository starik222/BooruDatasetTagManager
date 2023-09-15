using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class EditableTagHistory
    {
        public int Index { get; set; } = -1;
        public int OldIndex { get; set; } = -1;
        public EditableTag TagOld { get; set; } = null;
        public EditableTag TagNew { get; set; } = null;
        public HistoryType Type { get; set; } = HistoryType.None;

        public enum HistoryType
        {
            None,
            Add,
            Remove,
            Modify,
            Move
        }
    }
}
