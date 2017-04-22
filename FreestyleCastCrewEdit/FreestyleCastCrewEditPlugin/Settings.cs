using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    [ComVisible(false)]
    [Serializable()]
    public class Settings
    {
        public SizableForm MainForm;

        public String CurrentVersion;

        //public DefaultValues DefaultValues;
    }

    [ComVisible(false)]
    [Serializable()]
    public class BaseForm
    {
        public Int32 Top = 50;

        public Int32 Left = 50;
    }

    [ComVisible(false)]
    [Serializable()]
    public class SizableForm : BaseForm
    {
        public Int32 Height = 500;

        public Int32 Width = 900;

        public FormWindowState WindowState = FormWindowState.Normal;

        public Rectangle RestoreBounds;
    }

    //[Serializable()]
    //public class DefaultValues
    //{
    //    public Boolean CheckOnlyTitles = false;

    //    public Boolean IgnoreProductionYear = false;

    //    public Boolean IgnoreWishListTitles = false;

    //    public Boolean IgnoreTelevisonTitles = false;

    //    public Boolean IgnoreSameDatePurchases = true;

    //    public Int32 UiCulture = CultureInfo.CurrentUICulture.LCID;
    //}
}