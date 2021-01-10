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
    public partial class SettingForm : Form
    {
        public string HomeURL { get; set; } = "https://www.google.com";
        public string SearchEngineUrl { get; set; }
        public bool SaveClicked { get; set; }= false;

        public List<string> newBrowsers;
        public SettingForm(ref List<string> browsers)
        {
            InitializeComponent();
            newBrowsers = browsers;
            searchComboBox.SelectedIndex = 0;
        }

        private void SearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (searchComboBox.SelectedIndex)
            {
                case 0:
                    SearchEngineUrl= "https://www.google.com";
                    break;
                case 1:
                    SearchEngineUrl = "https://yandex.ru";
                    break;
                case 2:
                    SearchEngineUrl = "https://www.bing.com";
                    break;
                default:
                    break;
            }
        }
        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            newBrowsers.Clear();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            HomeURL = homeTextBox.Text;
            SaveClicked = true;
            HistoryForm.ActiveForm.Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            HistoryForm.ActiveForm.Close();
        }

    }
}
