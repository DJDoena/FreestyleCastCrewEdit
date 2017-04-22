using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Invelos.DVDProfilerPlugin;
using System.IO;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerXML;
using System.Globalization;
using System.Threading;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    [ComVisible(true)]
    [Guid("07457294-7E9E-4A08-A129-79DCF283B2A1")]
    public class Plugin : IDVDProfilerPlugin, IDVDProfilerPluginInfo
    {
        internal static Settings Settings;

        private static readonly XmlSerializer XmlSerializerSettings;

        private readonly String SettingsFile;

        private readonly String ErrorFile;

        private readonly String ApplicationPath;

        private IDVDProfilerAPI Api;

        private const Int32 MenuId = 1;

        private String MenuTokenISCP = "";

        //private CultureInfo PreviousCultureInfo;

        static Plugin()
        {
            XmlSerializerSettings = new XmlSerializer(typeof(Settings));
        }

        public Plugin()
        {
            this.ApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Doena Soft\FCCE\";
            this.SettingsFile = this.ApplicationPath + "FCCEpluginSettings.xml";
            this.ErrorFile = Environment.GetEnvironmentVariable("TEMP") + @"\FreestyleCastCrewEditPluginCrash.xml";
        }

        #region IDVDProfilerPlugin Members
        public void Load(IDVDProfilerAPI api)
        {
            this.Api = api;
            if(Directory.Exists(this.ApplicationPath) == false)
            {
                Directory.CreateDirectory(this.ApplicationPath);
            }
            if(File.Exists(this.SettingsFile))
            {
                try
                {
                    using(FileStream fs = new FileStream(this.SettingsFile, FileMode.Open, FileAccess.Read
                        , FileShare.Read))
                    {
                        Settings = (Settings)(XmlSerializerSettings.Deserialize(fs));
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeRead, this.SettingsFile, ex.Message)
                        , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            CreateSettings();
            //if(Settings.DefaultValues.UiCulture != 0)
            //{
            //    this.PreviousCultureInfo = Thread.CurrentThread.CurrentUICulture;
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture);
            //}
            MenuTokenISCP = this.Api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , "DVD", "Freestyle Cast/Crew Edit", MenuId);
            //Thread.CurrentThread.CurrentUICulture = this.PreviousCultureInfo;
        }

        public void Unload()
        {
            try
            {
                using(FileStream fs = new FileStream(this.SettingsFile, FileMode.Create, FileAccess.Write
                       , FileShare.None))
                {
                    XmlSerializerSettings.Serialize(fs, Settings);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, this.SettingsFile, ex.Message)
                    , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Api = null;
        }

        public void HandleEvent(Int32 EventType, Object EventData)
        {
            if(EventType == PluginConstants.EVENTID_CustomMenuClick)
            {
                this.HandleMenuClick((Int32)EventData);
            }
        }
        #endregion

        #region IDVDProfilerPluginInfo Members
        public String GetName()
        {
            //if(Settings.DefaultValues.UiCulture!=0)
            //{
            //    return (Texts.ResourceManager.GetString("PluginName", CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture)));
            //}
            return ("Freestyle Cast/Crew Edit");
        }

        public String GetDescription()
        {
            //if(Settings.DefaultValues.UiCulture != 0)
            //{
            //    return (Texts.ResourceManager.GetString("PluginDescription", CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture)));
            //}
            return ("This plugin helps to edit Cast & Crew.");
        }

        public String GetAuthorName()
        {
            return ("Doena Soft.");
        }

        public String GetAuthorWebsite()
        {
            //if(Settings.DefaultValues.UiCulture != 0)
            //{
            //    return (Texts.ResourceManager.GetString("PluginUrl", CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture)));
            //}
            return ("http://doena-journal.net/en/dvd-profiler-tools/");
        }

        public Int32 GetPluginAPIVersion()
        {
            return (PluginConstants.API_VERSION);
        }

        public Int32 GetVersionMajor()
        {
            Version version;

            version = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Version;
            return (version.Major);
        }

        public Int32 GetVersionMinor()
        {
            Version version;

            version = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Version;
            return (version.Minor * 100 + version.Build * 10 + version.Revision);
        }
        #endregion

         private void HandleMenuClick(Int32 MenuEventID)
        {
            if(MenuEventID == MenuId)
            {
                //this.PreviousCultureInfo = Thread.CurrentThread.CurrentUICulture;
                //if(Settings.DefaultValues.UiCulture != 0)
                //{
                //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.DefaultValues.UiCulture);
                //}
                try
                {
                    IDVDInfo dvdInfo;

                    dvdInfo = this.Api.GetDisplayedDVD();
                    if(dvdInfo.GetProfileID() != null)
                    {
                        using(MainForm mainForm = new MainForm(this.Api))
                        {
                            mainForm.ShowDialog();
                        }
                    }
                }
                catch(Exception ex)
                {
                    try
                    {
                        ExceptionXml exceptionXml;

                        MessageBox.Show(String.Format(MessageBoxTexts.CriticalError, ex.Message, this.ErrorFile)
                            , MessageBoxTexts.CriticalErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        if(File.Exists(this.ErrorFile))
                        {
                            File.Delete(ErrorFile);
                        }
                        exceptionXml = new ExceptionXml(ex);
                        Serializer<ExceptionXml>.Serialize(ErrorFile, exceptionXml);
                    }
                    catch(Exception inEx)
                    {
                        MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, this.ErrorFile, inEx.Message), MessageBoxTexts.ErrorHeader
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //Thread.CurrentThread.CurrentUICulture = this.PreviousCultureInfo;
            }
        }

        private static void CreateSettings()
        {
            if(Settings == null)
            {
                Settings = new Settings();
            }
            if(Settings.MainForm == null)
            {
                Settings.MainForm = new SizableForm();
            }
            //if(Settings.DefaultValues == null)
            //{
            //    Settings.DefaultValues = new DefaultValues();
            //}
        }

        #region Plugin Registering
        [DllImport("user32.dll")]
        public extern static int SetParent(int child, int parent);

        [ComImport(), Guid("0002E005-0000-0000-C000-000000000046")]
        internal class StdComponentCategoriesMgr { }

        [ComRegisterFunction()]
        public static void RegisterServer(Type t)
        {
            CategoryRegistrar.ICatRegister cr = (CategoryRegistrar.ICatRegister)new StdComponentCategoriesMgr();
            Guid clsidThis = new Guid("07457294-7E9E-4A08-A129-79DCF283B2A1");
            Guid catid = new Guid("833F4274-5632-41DB-8FC5-BF3041CEA3F1");

            cr.RegisterClassImplCategories(ref clsidThis, 1,
                new Guid[] { catid });
        }

        [ComUnregisterFunction()]
        public static void UnregisterServer(Type t)
        {
            CategoryRegistrar.ICatRegister cr = (CategoryRegistrar.ICatRegister)new StdComponentCategoriesMgr();
            Guid clsidThis = new Guid("07457294-7E9E-4A08-A129-79DCF283B2A1");
            Guid catid = new Guid("833F4274-5632-41DB-8FC5-BF3041CEA3F1");

            cr.UnRegisterClassImplCategories(ref clsidThis, 1,
                new Guid[] { catid });
        }
        #endregion
    }
}