using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BooruDatasetTagManager
{
    public partial class Form_ImageSorterSettings : Form
    {
        public Form_ImageSorterSettings()
        {
            InitializeComponent();
        }

        private void Form_ImageSorterSettings_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
                selectedNode = treeView1.Nodes["Root"];
            bool addToAll = checkBox1.Checked;
            if (selectedNode.Parent == null)
            {
                if (!selectedNode.Nodes.ContainsKey(textBoxNodeName.Text))
                    selectedNode.Nodes.Add(textBoxNodeName.Text, textBoxNodeName.Text);
            }
            else
            {
                if (addToAll)
                {
                    foreach (TreeNode item in selectedNode.Parent.Nodes)
                    {
                        if (!selectedNode.Nodes.ContainsKey(item.Name + "|" + textBoxNodeName.Text))
                            item.Nodes.Add(item.Name + "|" + textBoxNodeName.Text, textBoxNodeName.Text);
                    }
                }
                else
                {
                    if (!selectedNode.Nodes.ContainsKey(selectedNode.Name + "|" + textBoxNodeName.Text))
                        selectedNode.Nodes.Add(selectedNode.Name + "|" + textBoxNodeName.Text, textBoxNodeName.Text);
                }
            }
            textBoxNodeName.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Specify the root folder where the images will be copied.";
            if (openFolderDialog.ShowDialog() != DialogResult.OK)
                return;
            textBoxRootPath.Text = openFolderDialog.Folder;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxRootPath.Text))
            {
                MessageBox.Show("Root folder not selected");
                return;
            }
            ImageSorter sorter = new ImageSorter(textBoxRootPath.Text);
            sorter.CreateFromTreeNode(treeView1.Nodes["Root"]);
            sorter.FileIndex = Convert.ToInt32(textBoxIndex.Text);
            Form_ImageSorter sorterForm = new Form_ImageSorter(sorter);
            sorterForm.Show();
            //DialogResult = DialogResult.OK;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Name != "Root")
                treeView1.SelectedNode.Remove();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(textBoxRootPath.Text, "*.*", SearchOption.AllDirectories);
            List<long> indexes = new List<long>();
            long index = 0;
            foreach (var item in files)
            {
                if (long.TryParse(Path.GetFileNameWithoutExtension(item), out index))
                {
                    indexes.Add(index);
                }
            }
            textBoxIndex.Text = (indexes.Max()+1).ToString();
        }
    }
}
