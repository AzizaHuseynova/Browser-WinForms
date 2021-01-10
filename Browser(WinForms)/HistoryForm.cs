using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Browser_WinForms_
{
    public partial class HistoryForm : Form
    {
        public string hURL { get; set; }
        public List<string> newBrowsers;
        public HistoryForm(ref List<string> browsers)
        {
            InitializeComponent();
            newBrowsers = browsers;
            historyListBox.Items.AddRange(browsers.ToArray());
            goButton.Enabled = false;
            deleteButton.Enabled = false;
        }
       private void HistoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyListBox.SelectedIndex < historyListBox.Items.Count && historyListBox.SelectedIndex>=0)
            {
                goButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else
            {
                goButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }
        private void GoButton_Click(object sender, EventArgs e)
        {
            hURL = newBrowsers[historyListBox.SelectedIndex];
            HistoryForm.ActiveForm.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            newBrowsers.Remove(newBrowsers[historyListBox.SelectedIndex]);
            historyListBox.Items.RemoveAt(historyListBox.SelectedIndex);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            HistoryForm.ActiveForm.Close();
        }
    }
}
