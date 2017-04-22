using System;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public partial class SearchForm : Form
    {
        public String FindString { get; private set; }

        public SearchForm(String findString)
        {
            InitializeComponent();
            this.SearchTermTextBox.Text = findString;
        }

        private void OnKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void OnSearchFormClosing(Object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                this.FindString = this.SearchTermTextBox.Text;
            }
            else
            {
                this.FindString = null;
            }
        }

        private void OnSearchButtonClick(Object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}