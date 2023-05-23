using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public class AutoCompleteTextBox : TextBox
    {
        private ListBox _listBox;
        private bool _isAdded;
        //private String[] _values;
        private List<TagsDB.TagItem> _values;
        private String _formerValue = String.Empty;
        private AutocompleteMode _mode = AutocompleteMode.StartWithAndContains;
        private AutocompleteSort _sort = AutocompleteSort.Alphabetical;

        public AutoCompleteTextBox()
        {
            InitializeComponent();
            ResetListBox();
        }

        private void InitializeComponent()
        {
            _listBox = new ListBox();
            _listBox.Font = Program.Settings.AutocompleteFont.GetFont();
            this.KeyDown += this_KeyDown;
            this.KeyUp += this_KeyUp;
            this.PreviewKeyDown += AutoCompleteTextBox_PreviewKeyDown;
            _listBox.MouseDoubleClick += AutoCompleteTextBox_MouseClick;
            this.LostFocus += AutoCompleteTextBox_LostFocus;
        }

        private void AutoCompleteTextBox_LostFocus(object sender, EventArgs e)
        {
            if (Parent is Form && !_listBox.Focused)
                ResetListBox();
        }

        public event EventHandler ItemSelectionComplete;

        private void AutoCompleteTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (_listBox.Visible)
            {
                Text = ((TagsDB.TagItem)_listBox.SelectedItem).GetTag();
                ResetListBox();
                _formerValue = Text;
                //_parent.Focus();
                //Parent.Parent.Focus();
                _listBox.Parent.Focus();
                ItemSelectionComplete?.Invoke(this, e);
                //this.Select(this.Text.Length, 0);
            }
        }

        public bool IsListBoxFocused()
        {
            return _listBox.Focused;
        }

        public bool IsListBoxVisible()
        {
            return _listBox.Visible;
        }

        private void AutoCompleteTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (_listBox.Visible)
                {
                    Text = ((TagsDB.TagItem)_listBox.SelectedItem).GetTag();
                    ResetListBox();
                    _formerValue = Text;
                    //this.Select(this.Text.Length, 0);
                    ItemSelectionComplete?.Invoke(this, e);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ResetListBox();
            }
        }

        public void SetAutocompleteMode(AutocompleteMode mode, AutocompleteSort sort)
        {
            _mode = mode;
            _sort = sort;
        }

        private void ShowListBox()
        {
            if (!_isAdded)
            {
                //Parent.Controls.Add(_listBox);
                //_parent.Controls.Add(_listBox);
                //_listBox.Parent = Parent;
                if (Parent is Form)
                {
                    Parent.Controls.Add(_listBox);
                    _listBox.Left = Left;
                    _listBox.Top = Top + Height;
                }
                else
                {
                    Parent.Parent.Controls.Add(_listBox);
                }
                _isAdded = true;
            }

            //int gridHeight = Parent.Parent.Height;
            //int upMaxSize = Parent.Top;
            //int downMaxSize = gridHeight - Parent.Top + Parent.Height;

            //if (upMaxSize > downMaxSize)
            //{

            //}


            //_listBox.Left = Left;
            //_listBox.Top = Parent.Top + Parent.Height;
            _listBox.Visible = true;
            _listBox.BringToFront();
            //_listBox.Focus();
        }

        //private Control GetRootControl(Control control)
        //{
        //    if(control.Parent!=null)
        //        return GetRootControl(control.Parent);
        //    else
        //        return control;
        //}

        public void ResetListBox()
        {
            _listBox.Visible = false;
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateListBox();
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    {
                        if (_listBox.Visible)
                        {
                            Text = ((TagsDB.TagItem)_listBox.SelectedItem).GetTag();
                            ResetListBox();
                            _formerValue = Text;
                            this.Select(this.Text.Length, 0);
                            ItemSelectionComplete?.Invoke(this, e);
                            e.Handled = true;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                            _listBox.SelectedIndex++;
                        e.Handled = true;
                        break;
                    }
                case Keys.Up:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                            _listBox.SelectedIndex--;
                        e.Handled = true;
                        break;
                    }


            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    if (_listBox.Visible)
                        return true;
                    else
                        return false;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void UpdateListBox()
        {
            if (Text == _formerValue)
                return;

            _formerValue = this.Text;
            string word = this.Text;

            if (_values != null && word.Length > 0 && word.Length >= Program.Settings.ShowAutocompleteAfterCharCount)
            {
                //string[] matches = null;
                TagsDB.TagItem[] matches = null;
                IEnumerable<TagsDB.TagItem> tempMatches = null;
                if (_mode == AutocompleteMode.StartWith)
                {
                    tempMatches = _values.Where(a => a.Tag.StartsWith(word));
                    if (_sort == AutocompleteSort.Alphabetical)
                        matches = tempMatches.OrderBy(a => a.Tag).ToArray();
                    else
                        matches = tempMatches.OrderByDescending(a => a.Count).ToArray();

                }
                else if (_mode == AutocompleteMode.StartWithIncludeTranslations)
                {
                    tempMatches = _values.Where(a => a.Tag.StartsWith(word) || (a.Translation != null && a.Translation.StartsWith(word)));
                    if (_sort == AutocompleteSort.Alphabetical)
                        matches = tempMatches.OrderBy(a => a.Tag).ToArray();
                    else
                        matches = tempMatches.OrderByDescending(a => a.Count).ToArray();
                }
                else if(_mode == AutocompleteMode.StartWithAndContains)
                {
                    if (_sort == AutocompleteSort.ByCount)
                    {
                        matches = _values.Where(a => a.Tag.StartsWith(word))
                            .OrderByDescending(a => a.Count)
                            .Concat(_values.Where(a => a.Tag.Contains(word)).OrderByDescending(a => a.Count))
                            .Distinct().ToArray();
                    }
                    else
                    {
                        matches = _values.Where(a => a.Tag.StartsWith(word))
                            .OrderBy(a => a.Tag)
                            .Concat(_values.Where(a => a.Tag.Contains(word)).OrderBy(a => a.Tag))
                            .Distinct().ToArray();
                    }
                }
                else if (_mode == AutocompleteMode.StartWithAndContainsIncludeTranslations)
                {
                    if (_sort == AutocompleteSort.ByCount)
                    {
                        matches = _values.Where(a => a.Tag.StartsWith(word) || (a.Translation != null && a.Translation.StartsWith(word)))
                            .OrderByDescending(a => a.Count)
                            .Concat(_values.Where(a => a.Tag.Contains(word)).OrderByDescending(a => a.Count))
                            .Distinct().ToArray();
                    }
                    else
                    {
                        matches = _values.Where(a => a.Tag.StartsWith(word) || (a.Translation != null && a.Translation.StartsWith(word)))
                            .OrderBy(a => a.Tag)
                            .Concat(_values.Where(a => a.Tag.Contains(word) || (a.Translation != null && a.Translation.Contains(word))).OrderBy(a => a.Tag))
                            .Distinct().ToArray();
                    }
                }
                if (matches.Length > 0)
                {
                    ShowListBox();
                    _listBox.BeginUpdate();
                    _listBox.Items.Clear();
                    _listBox.Items.AddRange(matches);
                    _listBox.SelectedIndex = 0;
                    _listBox.Height = 0;
                    _listBox.Width = 0;
                    Focus();

                    int upMaxSize = Parent.Top;
                    int downMaxSize = 0;
                    if (Parent is Form)
                    {
                        upMaxSize = Parent.Height - (this.Top + this.Height + 40);
                    }
                    else
                    {
                        downMaxSize = Parent.Parent.Height - (Parent.Top + Parent.Height);
                    }
                    int maxSize = 0;
                    bool isDown = false;
                    if (upMaxSize > downMaxSize)
                    {
                        maxSize = upMaxSize;
                    }
                    else
                    {
                        maxSize = downMaxSize;
                        isDown = true;
                    }
                    for (int i = 0; i < _listBox.Items.Count; i++)
                    {
                        if (i < 20 && _listBox.Height + _listBox.GetItemHeight(i) < maxSize)
                            _listBox.Height += _listBox.GetItemHeight(i);
                    }
                    _listBox.Width = this.Width;

                    //using (Graphics graphics = _listBox.CreateGraphics())
                    //{
                    //    for (int i = 0; i < _listBox.Items.Count; i++)
                    //    {
                    //        if (i < 20 && _listBox.Height < maxSize)
                    //            _listBox.Height += _listBox.GetItemHeight(i);
                    //        // it item width is larger than the current one
                    //        // set it to the new max item width
                    //        // GetItemRectangle does not work for me
                    //        // we add a little extra space by using '_'
                    //        int itemWidth = (int)graphics.MeasureString(((string)_listBox.Items[i]) + "_", _listBox.Font).Width;
                    //        _listBox.Width = (_listBox.Width < itemWidth) ? itemWidth : this.Width; ;
                    //    }
                    //}
                    if (!(Parent is Form))
                    {
                        if (isDown)
                        {
                            _listBox.Left = Left;
                            _listBox.Top = Parent.Top + Parent.Height;
                        }
                        else
                        {
                            _listBox.Left = Left;
                            _listBox.Top = Parent.Top - _listBox.Height;
                        }
                    }
                    _listBox.EndUpdate();
                }
                else
                {
                    ResetListBox();
                }
            }
            else
            {
                ResetListBox();
            }
        }

        public List<TagsDB.TagItem> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }

        public List<String> SelectedValues
        {
            get
            {
                String[] result = Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return new List<String>(result);
            }
        }
    }
}
