using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.DVDProfiler.DVDProfilerXML.Version390;
using Invelos.DVDProfilerPlugin;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    internal partial class MainForm : Form
    {
        IDVDProfilerAPI Api;

        Boolean AcceptChange;

        public MainForm(IDVDProfilerAPI api)
        {
            this.Api = api;
            this.AcceptChange = false;
            InitializeComponent();
        }

        private void OkButtonClick(Object sender, EventArgs e)
        {
            this.AcceptChange = true;
            this.Close();
        }

        private void OnCancelButtonClick(Object sender, EventArgs e)
        {
            this.AcceptChange = false;
            this.Close();
        }

        private void OnReadMeToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.OpenReadme();
        }

        private void OnAboutToolStripMenuItemClick(Object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox(this.GetType().Assembly))
            {
                aboutBox.ShowDialog();
            }
        }

        private void OnMainFormLoad(Object sender, EventArgs e)
        {
            CastInformation castInformation;
            CrewInformation crewInformation;

            this.SuspendLayout();
            this.LayoutForm();
            this.GetInitInformation(out castInformation, out crewInformation);
            this.EditingPane.Init(castInformation, crewInformation, this.GetType().Assembly);
            this.ResumeLayout();
            if (Plugin.Settings.CurrentVersion != this.GetType().Assembly.GetName().Version.ToString())
            {
                this.OpenReadme();
                Plugin.Settings.CurrentVersion = this.GetType().Assembly.GetName().Version.ToString();
            }
        }

        private void GetInitInformation(out CastInformation castInformation, out CrewInformation crewInformation)
        {
            IDVDInfo dvdInfo;
            DVD dvd;
            String xml;

            dvdInfo = this.Api.GetDisplayedDVD();
            this.Api.DVDByProfileID(out dvdInfo, dvdInfo.GetProfileID(), -1, -1);
            xml = dvdInfo.GetXML(false);
            dvd = Serializer<DVD>.FromString(xml, DVD.DefaultEncoding);
            castInformation = new CastInformation();
            castInformation.Title = dvdInfo.GetTitle();
            castInformation.CastList = dvd.CastList;
            crewInformation = new CrewInformation();
            crewInformation.Title = dvdInfo.GetTitle();
            crewInformation.CrewList = dvd.CrewList;
        }

        private void LayoutForm()
        {
            if (Plugin.Settings.MainForm.WindowState == FormWindowState.Normal)
            {
                this.Left = Plugin.Settings.MainForm.Left;
                this.Top = Plugin.Settings.MainForm.Top;
                if (Plugin.Settings.MainForm.Width > this.MinimumSize.Width)
                {
                    this.Width = Plugin.Settings.MainForm.Width;
                }
                else
                {
                    this.Width = this.MinimumSize.Width;
                }
                if (Plugin.Settings.MainForm.Height > this.MinimumSize.Height)
                {
                    this.Height = Plugin.Settings.MainForm.Height;
                }
                else
                {
                    this.Height = this.MinimumSize.Height;
                }
            }
            else
            {
                this.Left = Plugin.Settings.MainForm.RestoreBounds.X;
                this.Top = Plugin.Settings.MainForm.RestoreBounds.Y;
                if (Plugin.Settings.MainForm.RestoreBounds.Width > this.MinimumSize.Width)
                {
                    this.Width = Plugin.Settings.MainForm.RestoreBounds.Width;
                }
                else
                {
                    this.Width = this.MinimumSize.Width;
                }
                if (Plugin.Settings.MainForm.RestoreBounds.Height > this.MinimumSize.Height)
                {
                    this.Height = Plugin.Settings.MainForm.RestoreBounds.Height;
                }
                else
                {
                    this.Height = this.MinimumSize.Height;
                }
            }
            if (Plugin.Settings.MainForm.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = Plugin.Settings.MainForm.WindowState;
            }
        }

        private void OnMainFormClosing(Object sender, FormClosingEventArgs e)
        {
            if ((this.EditingPane.HasChanged) && (this.AcceptChange == false))
            {
                if (MessageBox.Show(MessageBoxTexts.CancelEdit, MessageBoxTexts.QuestionHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if ((this.EditingPane.HasChanged) && (this.AcceptChange))
            {
                IDVDInfo dvdInfo;

                if (this.EditingPane.CheckDataValidity())
                {
                    dvdInfo = this.Api.GetDisplayedDVD();
                    this.Api.DVDByProfileID(out dvdInfo, dvdInfo.GetProfileID(), -1, -1);
                    this.FillCastData(dvdInfo);
                    this.FillCrewData(dvdInfo);
                    this.Api.SaveDVDToCollection(dvdInfo);
                    this.Api.ReloadCurrentDVD();
                    this.Api.UpdateProfileInListDisplay(dvdInfo.GetProfileID());
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            Plugin.Settings.MainForm.Left = this.Left;
            Plugin.Settings.MainForm.Top = this.Top;
            Plugin.Settings.MainForm.Width = this.Width;
            Plugin.Settings.MainForm.Height = this.Height;
            Plugin.Settings.MainForm.WindowState = this.WindowState;
            Plugin.Settings.MainForm.RestoreBounds = this.RestoreBounds;
        }

        private void FillCastData(IDVDInfo dvdInfo)
        {
            CastInformation castInformation;

            dvdInfo.ClearCast();
            castInformation = this.EditingPane.GetCastInformation();
            if ((castInformation.CastList != null) && (castInformation.CastList.Length > 0))
            {
                for (Int32 i = 0; i < castInformation.CastList.Length; i++)
                {
                    CastMember castMember;

                    castMember = castInformation.CastList[i] as CastMember;
                    if (castMember != null)
                    {
                        dvdInfo.AddCast(castMember.FirstName, castMember.MiddleName, castMember.LastName, castMember.BirthYear
                            , castMember.Role, castMember.CreditedAs, castMember.Voice, castMember.Uncredited);
                    }
                    else
                    {
                        Divider divider;
                        Int32 dividerType;

                        divider = (Divider)(castInformation.CastList[i]);
                        dividerType = GetDividertype(divider.Type);
                        dvdInfo.AddCastDivider(divider.Caption, dividerType);
                    }
                }
            }
        }

        private void FillCrewData(IDVDInfo dvdInfo)
        {
            CrewInformation crewInformation;

            dvdInfo.ClearCrew();
            crewInformation = this.EditingPane.GetCrewInformation();
            crewInformation = CrewSorter.GetSortedCrew(crewInformation);
            if ((crewInformation.CrewList != null) && (crewInformation.CrewList.Length > 0))
            {
                Int32 previousDividerCreditType;

                previousDividerCreditType = -1;
                for (Int32 i = 0; i < crewInformation.CrewList.Length; i++)
                {
                    CrewMember crewMember;

                    crewMember = crewInformation.CrewList[i] as CrewMember;
                    if (crewMember != null)
                    {
                        Int32 creditType;
                        Int32 creditSubtype;

                        creditType = GetCreditType(crewMember.CreditType);
                        creditSubtype = GetCreditSubtype(crewMember.CreditSubtype);
                        dvdInfo.AddCrew(crewMember.FirstName, crewMember.MiddleName, crewMember.LastName, crewMember.BirthYear
                            , creditType, creditSubtype, crewMember.CreditedAs);
                        if (crewMember.CustomRoleSpecified)
                        {
                            dvdInfo.SetCrewCustomRoleByIndex(i, crewMember.CustomRole);
                        }
                        else
                        {
                            dvdInfo.SetCrewCustomRoleByIndex(i, String.Empty);
                        }
                    }
                    else
                    {
                        CrewDivider divider;
                        Int32 dividerType;

                        divider = (CrewDivider)(crewInformation.CrewList[i]);
                        dividerType = GetDividertype(divider.Type);
                        switch (divider.Type)
                        {
                            case (DividerType.Episode):
                                {
                                    dvdInfo.AddCrewDivider(divider.Caption, dividerType, 0);
                                    break;
                                }
                            case (DividerType.Group):
                                {
                                    previousDividerCreditType = GetCreditType(divider.CreditType);
                                    dvdInfo.AddCrewDivider(divider.Caption, dividerType, previousDividerCreditType);
                                    break;
                                }
                            case (DividerType.EndDiv):
                                {
                                    dvdInfo.AddCrewDivider(divider.Caption, dividerType, previousDividerCreditType);
                                    break;
                                }
                        }

                    }
                }
            }
            for (Int32 i = 0; i < dvdInfo.GetCrewCount(); i++)
            {
                String fn;
                String mn;
                String ln;
                Int32 by;
                Int32 ct;
                Int32 cst;
                String ca;

                dvdInfo.GetCrewByIndex(i, out fn, out mn, out ln, out by, out ct, out cst, out ca);
            }
        }

        private static Int32 GetCreditSubtype(String creditSubType)
        {
            switch (creditSubType)
            {
                #region Direction
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Direction.Director):
                    {
                        return (PluginConstants.CREDITSUB_Director);
                    }
                #endregion
                #region Writing
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Writing.OriginalMaterialBy):
                    {
                        return (PluginConstants.CREDITSUB_OriginalMaterialBy);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Writing.Screenwriter):
                    {
                        return (PluginConstants.CREDITSUB_Screenwriter);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Writing.Writer):
                    {
                        return (PluginConstants.CREDITSUB_Writer);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Writing.OriginalCharactersBy):
                    {
                        return (PluginConstants.CREDITSUB_OriginalCharactersBy);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Writing.CreatedBy):
                    {
                        return (4);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Writing.StoryBy):
                    {
                        return (5);
                    }
                #endregion
                #region Production
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Production.Producer):
                    {
                        return (PluginConstants.CREDITSUB_Producer);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Production.ExecutiveProducer):
                    {
                        return (PluginConstants.CREDITSUB_ExecutiveProducer);
                    }
                #endregion
                #region Cinematography
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Cinematography.DirectorOfPhotography):
                    {
                        return (PluginConstants.CREDITSUB_DirectorOfPhotography);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Cinematography.Cinematographer):
                    {
                        return (PluginConstants.CREDITSUB_Cinematographer);
                    }
                #endregion
                #region Film Editing
                case (CreditTypesDataGridViewHelper.CreditSubtypes.FilmEditing.FilmEditor):
                    {
                        return (PluginConstants.CREDITSUB_FilmEditor);
                    }
                #endregion
                #region Music
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Music.Composer):
                    {
                        return (PluginConstants.CREDITSUB_Composer);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Music.SongWriter):
                    {
                        return (PluginConstants.CREDITSUB_SongWriter);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Music.ThemeBy):
                    {
                        return (2);
                    }
                #endregion
                #region Sound
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Sound.SoundEditor):
                    {
                        return (PluginConstants.CREDITSUB_SoundEditor);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Sound.SoundReRecordingMixer):
                    {
                        return (PluginConstants.CREDITSUB_SoundReRecordingMixer);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Sound.SoundDesigner):
                    {
                        return (PluginConstants.CREDITSUB_SoundDesigner);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Sound._Sound):
                    {
                        return (PluginConstants.CREDITSUB_Sound);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Sound.SupervisingSoundEditor):
                    {
                        return (PluginConstants.CREDITSUB_SupervisingSoundEditor);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Sound.ProductionSoundMixer):
                    {
                        return (PluginConstants.CREDITSUB_ProductionSoundMixer);
                    }
                #endregion
                #region Art
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Art.ProductionDesigner):
                    {
                        return (PluginConstants.CREDITSUB_ProductionDesigner);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Art.ArtDirector):
                    {
                        return (PluginConstants.CREDITSUB_ArtDirector);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Art.CostumeDesigner):
                    {
                        return (2);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Art.MakeUpArtist):
                    {
                        return (3);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Art.VisualEffects):
                    {
                        return (4);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Art.MakeUpEffects):
                    {
                        return (5);
                    }
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Art.CreatureDesign):
                    {
                        return (6);
                    }
                #endregion
                #region Other
                case (CreditTypesDataGridViewHelper.CreditSubtypes.Custom):
                    {
                        return (254);
                    }
                #endregion
                default:
                    {
                        Debug.Assert(false, "Should not happen!");
                        return (254);
                    }
            }
        }

        private static Int32 GetCreditType(String creditType)
        {
            switch (creditType)
            {
                case (CreditTypesDataGridViewHelper.CreditTypes.Direction):
                    {
                        return (PluginConstants.CREDIT_Direction);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.Writing):
                    {
                        return (PluginConstants.CREDIT_Writing);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.Production):
                    {
                        return (PluginConstants.CREDIT_Production);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.Cinematography):
                    {
                        return (PluginConstants.CREDIT_Cinematography);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.FilmEditing):
                    {
                        return (PluginConstants.CREDIT_FilmEditing);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.Music):
                    {
                        return (PluginConstants.CREDIT_Music);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.Sound):
                    {
                        return (PluginConstants.CREDIT_Sound);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.Art):
                    {
                        return (PluginConstants.CREDIT_Art);
                    }
                case (CreditTypesDataGridViewHelper.CreditTypes.Other):
                    {
                        return (8);
                    }
                default:
                    {
                        Debug.Assert(false, "Should not happen!");
                        return (8);
                    }
            }
        }

        private static Int32 GetDividertype(DividerType dividerType)
        {
            switch (dividerType)
            {
                case (DividerType.Episode):
                    {
                        return (PluginConstants.DIVIDER_Episode);
                    }
                case (DividerType.Group):
                    {
                        return (PluginConstants.DIVIDER_Group);
                    }
                case (DividerType.EndDiv):
                    {
                        return (PluginConstants.DIVIDER_EndDiv);
                    }
                default:
                    {
                        Debug.Assert(false, "Should not happen!");
                        return (-1);
                    }
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
    }
}