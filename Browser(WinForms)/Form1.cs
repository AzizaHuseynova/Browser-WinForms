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
    public partial class Form1 : Form
    {
        public string HomeURL { get; set; } = "https://www.google.com";
        public string SearchEngineUrl { get; set; } = "https://www.google.com";

        List<string> webPages = new List<string>();
        List<string> favWebPages = new List<string>();
        List<WebBrowser> webBrowsers;
        HistoryForm historyForm;
        SettingForm settingsForm;
        public Form1()
        {
            InitializeComponent();
            favListBox.Hide();
            webBrowsers = new List<WebBrowser>();
            backButton.Enabled = false;
            nextButton.Enabled = false;
            refreshButton.Enabled = false;
            tabDeliteButton.Enabled = false;
            AddTabb(SearchEngineUrl);

        }
        private void FavListButton_Click_1(object sender, EventArgs e)
        {
            if (favListBox.Visible)
                favListBox.Hide();
            else
                favListBox.Show();
        }

        private void TabAddbutton_Click_1(object sender, EventArgs e)
        {
            AddTabb(SearchEngineUrl);
        }

        private void TabDeliteButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex >= 0)
            {
                webBrowsers.Remove(webBrowsers[tabControl.SelectedIndex]);
                tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);
            }
            if (tabControl.TabPages.Count <= 0)
            {
                urlTextBox.Text = "";
                backButton.Enabled = false;
                nextButton.Enabled = false;
                refreshButton.Enabled = false;
                favoriteButton.Enabled = false;
                historyButton.Enabled = false;
                tabDeliteButton.Enabled = false;
            }
        }

        private void WebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (tabControl.SelectedIndex >= 0)
            {
                webPages.Add(webBrowsers[tabControl.SelectedIndex].Url.ToString());
               urlTextBox.Text = webBrowsers[tabControl.SelectedIndex].Url.ToString();
                if (favWebPages.Find(x => x == urlTextBox.Text) == null)
                    favoriteButton.Enabled = true;
                else
                    favoriteButton.Enabled = false;

                historyButton.Enabled = true;
                backButton.Enabled = true;
                tabDeliteButton.Enabled = true;
                nextButton.Enabled = true;
                refreshButton.Enabled = true;
            }
        }

        private void Favoritebutton_Click(object sender, EventArgs e)
        {
            if (webBrowsers[tabControl.SelectedIndex].Url != null)
            {
                if(favWebPages.Find(x => x==urlTextBox.Text)==null)
                {
                    favWebPages.Add(webBrowsers[tabControl.SelectedIndex].Url.ToString());
                    favListBox.Items.Add(webBrowsers[tabControl.SelectedIndex].Url.ToString());
                    favoriteButton.Enabled = false;
                }
            }
            favoriteButton.Enabled = false;
        }

        private void FavListBox_DoubleClick(object sender, EventArgs e)
        {
            AddTabb(favListBox.SelectedItem.ToString());
        }


        public void AddTabb(string URL)
        {
            webPages.Add(URL);
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Url = new Uri(SearchEngineUrl);
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Navigate(URL);
            webBrowser.Navigated += WebBrowser_Navigated;
            TabPage tabPage = new TabPage("New Tab");
            tabPage.Controls.Add(webBrowser);
            tabControl.TabPages.Add(tabPage);
            webBrowsers.Add(webBrowser);
            tabControl.SelectedTab = tabPage;
            favoriteButton.Enabled = false;
            historyButton.Enabled = false;
            tabDeliteButton.Enabled = false;
            backButton.Enabled = true;
            nextButton.Enabled = true;
            refreshButton.Enabled = true;
        }
        private void HistoryButton_Click(object sender, EventArgs e)
        {
            historyForm = new HistoryForm(ref webPages);
            historyForm.ShowDialog();
            if (!string.IsNullOrEmpty(historyForm.hURL))
            {
                AddTabb(historyForm.hURL);
            }
            historyForm.hURL = "";
        }

        private void Settingsbutton5_Click(object sender, EventArgs e)
        {
            settingsForm = new SettingForm(ref webPages);
            settingsForm.ShowDialog();
            if (settingsForm.SaveClicked)
            {
                HomeURL = settingsForm.HomeURL;
                SearchEngineUrl = settingsForm.SearchEngineUrl;
            }
           settingsForm.SaveClicked = false;
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            AddTabb(HomeURL);
            historyButton.Enabled = false;
        }
        private void UrlTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                switch (SearchEngineUrl)
                {
                    case "https://www.google.com":
                        AddTabb("https://www.google.com/search?q=" + urlTextBox.Text);
                        break;
                    case "https://yandex.ru":
                        AddTabb("https://yandex.ru/search/?text=" + urlTextBox.Text);
                        break;
                    case "https://www.bing.com":
                        AddTabb("https://www.bing.com/search?q=" + urlTextBox.Text);
                        break;
                    default:
                        break;
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (webBrowsers[tabControl.SelectedIndex].CanGoBack)
            {
                webBrowsers[tabControl.SelectedIndex].GoBack();
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (webBrowsers[tabControl.SelectedIndex].CanGoForward)
            {
                webBrowsers[tabControl.SelectedIndex].GoForward();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            webBrowsers[tabControl.SelectedIndex].Refresh();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex >= 0)
            {
                if (webBrowsers[tabControl.SelectedIndex].Url != null)
                {
                    urlTextBox.Text = webBrowsers[tabControl.SelectedIndex].Url.ToString();
                    if (favWebPages.Find(x => x == urlTextBox.Text) == null)
                        favoriteButton.Enabled = true;
                    else
                        favoriteButton.Enabled = false;
                }
            }

        }
    }
}
