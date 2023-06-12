using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public static class DataGridViewExtensions
    {
        public static int IdxFromName(this string name, DataGridView dgv)
        {
            return dgv.Columns[name]?.Index ?? -1;
        }
    }
}