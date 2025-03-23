using System;
using System.IO;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
        }

        //private void CreateTitleRow()
        //{
        //    if(this.ParseCastCheckBox.Checked)
        //    {
        //        CastInfo title;

        //        title = new CastInfo(-1);
        //        title.FirstName = DataGridViewHelper.FirstNames.Title;
        //        title.MiddleName = String.Empty;
        //        title.LastName = this.MovieTitle;
        //        title.BirthYear = String.Empty;
        //        title.Role = String.Empty;
        //        title.Voice = "False";
        //        title.Uncredited = "False";
        //        title.CreditedAs = String.Empty;
        //        title.PersonLink = this.MovieTitleLink;
        //        this.CastList.Add(title);
        //    }
        //    if(this.ParseCrewCheckBox.Checked)
        //    {
        //        CrewInfo title;

        //        title = new CrewInfo();
        //        title.FirstName = DataGridViewHelper.FirstNames.Title;
        //        title.MiddleName = String.Empty;
        //        title.LastName = this.MovieTitle;
        //        title.BirthYear = String.Empty;
        //        title.CreditType = null;
        //        title.CreditSubtype = null;
        //        title.CreditedAs = String.Empty;
        //        title.CustomRole = String.Empty;
        //        title.PersonLink = this.MovieTitleLink;
        //        this.CrewList.Add(title);
        //    }
        //}

        private void OnMainFormLoad(Object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.LayoutForm();
            EditingPane.Init(this.GetType().Assembly);
            this.ResumeLayout();
            if (Program.Settings.CurrentVersion != this.GetType().Assembly.GetName().Version.ToString())
            {
                this.OpenReadme();
                Program.Settings.CurrentVersion = this.GetType().Assembly.GetName().Version.ToString();
            }
        }

        private void OpenReadme()
        {
            if (File.Exists(Application.StartupPath + @"\Readme\readme.html"))
            {
                using (HelpForm helpForm = new HelpForm())
                {
                    helpForm.Text = "Readme";
                    helpForm.ShowDialog(this);
                }
            }
        }

        private void LayoutForm()
        {
            if (Program.Settings.MainForm.WindowState == FormWindowState.Normal)
            {
                this.Left = Program.Settings.MainForm.Left;
                this.Top = Program.Settings.MainForm.Top;
                if (Program.Settings.MainForm.Width > this.MinimumSize.Width)
                {
                    this.Width = Program.Settings.MainForm.Width;
                }
                else
                {
                    this.Width = this.MinimumSize.Width;
                }
                if (Program.Settings.MainForm.Height > this.MinimumSize.Height)
                {
                    this.Height = Program.Settings.MainForm.Height;
                }
                else
                {
                    this.Height = this.MinimumSize.Height;
                }
            }
            else
            {
                this.Left = Program.Settings.MainForm.RestoreBounds.X;
                this.Top = Program.Settings.MainForm.RestoreBounds.Y;
                if (Program.Settings.MainForm.RestoreBounds.Width > this.MinimumSize.Width)
                {
                    this.Width = Program.Settings.MainForm.RestoreBounds.Width;
                }
                else
                {
                    this.Width = this.MinimumSize.Width;
                }
                if (Program.Settings.MainForm.RestoreBounds.Height > this.MinimumSize.Height)
                {
                    this.Height = Program.Settings.MainForm.RestoreBounds.Height;
                }
                else
                {
                    this.Height = this.MinimumSize.Height;
                }
            }
            if (Program.Settings.MainForm.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = Program.Settings.MainForm.WindowState;
            }
        }

        private void OnMainFormClosing(Object sender, FormClosingEventArgs e)
        {
            Program.Settings.MainForm.Left = this.Left;
            Program.Settings.MainForm.Top = this.Top;
            Program.Settings.MainForm.Width = this.Width;
            Program.Settings.MainForm.Height = this.Height;
            Program.Settings.MainForm.WindowState = this.WindowState;
            Program.Settings.MainForm.RestoreBounds = this.RestoreBounds;
        }
    }
}