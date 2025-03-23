using System;
using System.IO;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.ToolBox.Generics;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public static class Program
    {
        internal static Settings Settings;

        private static readonly String SettingsFile;

        private static readonly String ErrorFile;

        private static readonly String ApplicationPath;

        private static readonly WindowHandle WindowHandle;

        static Program()
        {
            WindowHandle = new WindowHandle();
            ApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Doena Soft\FCCE\";
            SettingsFile = ApplicationPath + "FCCESettings.xml";
            ErrorFile = Environment.GetEnvironmentVariable("TEMP") + @"\FreestyleCastCrewEditCrash.xml";
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread()]
        private static void Main(String[] args)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("de-DE");
            //if((args != null) && (args.Length > 0))
            //{
            //    for(Int32 i = 0; i < args.Length; i++)
            //    {
            //        if(args[i] == "/lang=de")
            //        {
            //            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de");
            //            break;
            //        }
            //        else if(args[i] == "/lang=en")
            //        {
            //            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            //            break;
            //        }
            //    }
            //}
            try
            {
                MainForm mainForm;

                if (Directory.Exists(ApplicationPath) == false)
                {
                    Directory.CreateDirectory(ApplicationPath);
                }
                if (File.Exists(SettingsFile))
                {
                    try
                    {
                        Settings = Settings.Deserialize(SettingsFile);
                    }
                    catch
                    {
                        MessageBox.Show(WindowHandle, String.Format(MessageBoxTexts.FileCantBeRead, SettingsFile)
                            , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                CreateSettings();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                mainForm = new MainForm();
                Application.Run(mainForm);
                try
                {
                    Settings.Serialize(SettingsFile);
                }
                catch
                {
                    MessageBox.Show(WindowHandle, String.Format(MessageBoxTexts.FileCantBeWritten, SettingsFile)
                        , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    ExceptionXml exceptionXml;

                    MessageBox.Show(WindowHandle, String.Format(MessageBoxTexts.CriticalError, ex.Message, ErrorFile)
                        , MessageBoxTexts.CriticalErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (File.Exists(ErrorFile))
                    {
                        File.Delete(ErrorFile);
                    }
                    exceptionXml = new ExceptionXml(ex);
                    XmlSerializer<ExceptionXml>.Serialize(ErrorFile, exceptionXml);
                }
                catch
                {
                    MessageBox.Show(WindowHandle, String.Format(MessageBoxTexts.FileCantBeWritten, ErrorFile), MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void CreateSettings()
        {
            if (Settings == null)
            {
                Settings = new Settings();
            }
            if (Settings.MainForm == null)
            {
                Settings.MainForm = new SizableForm();
            }
        }
    }
}