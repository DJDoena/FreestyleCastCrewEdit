namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    partial class EditingPane
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CastCrewTabControl = new System.Windows.Forms.TabControl();
            this.CastTab = new System.Windows.Forms.TabPage();
            this.CastDataGridView = new DoenaSoft.DVDProfiler.FreestyleCastCrewEdit.CustomDataGridView();
            this.CrewTab = new System.Windows.Forms.TabPage();
            this.CrewDataGridView = new DoenaSoft.DVDProfiler.FreestyleCastCrewEdit.CustomDataGridView();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.CopyPasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopySelectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutSelectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppendDataFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertRowsFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddEpisodeDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGroupStartDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGroupEndDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertEpisodeDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertGroupStartDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertGroupEndDividerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearGridsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SortCrewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowVerificationCheckboxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllowSortingByClickingOnHeaderRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PutCastRoleAsFirstColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoRedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckDataValidityHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckDataValidityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyNamesToCreditedAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CastCrewTabControl.SuspendLayout();
            this.CastTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CastDataGridView)).BeginInit();
            this.CrewTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CrewDataGridView)).BeginInit();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CastCrewTabControl
            // 
            this.CastCrewTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CastCrewTabControl.Controls.Add(this.CastTab);
            this.CastCrewTabControl.Controls.Add(this.CrewTab);
            this.CastCrewTabControl.Location = new System.Drawing.Point(3, 27);
            this.CastCrewTabControl.Name = "CastCrewTabControl";
            this.CastCrewTabControl.SelectedIndex = 0;
            this.CastCrewTabControl.Size = new System.Drawing.Size(856, 467);
            this.CastCrewTabControl.TabIndex = 30;
            this.CastCrewTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnMovieCastCrewTabControlSelected);
            // 
            // CastTab
            // 
            this.CastTab.Controls.Add(this.CastDataGridView);
            this.CastTab.Location = new System.Drawing.Point(4, 22);
            this.CastTab.Name = "CastTab";
            this.CastTab.Padding = new System.Windows.Forms.Padding(3);
            this.CastTab.Size = new System.Drawing.Size(848, 441);
            this.CastTab.TabIndex = 0;
            this.CastTab.Text = "Cast";
            this.CastTab.UseVisualStyleBackColor = true;
            // 
            // CastDataGridView
            // 
            this.CastDataGridView.AllowDrop = true;
            this.CastDataGridView.AllowUserToResizeRows = false;
            this.CastDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CastDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CastDataGridView.Location = new System.Drawing.Point(6, 6);
            this.CastDataGridView.Name = "CastDataGridView";
            this.CastDataGridView.Size = new System.Drawing.Size(837, 429);
            this.CastDataGridView.TabIndex = 0;
            this.CastDataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.OnCastDataGridViewDefaultValuesNeeded);
            this.CastDataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.OnDataGridViewRowPostPaint);
            this.CastDataGridView.Sorted += new System.EventHandler(this.OnDataGridViewSorted);
            this.CastDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnDataGridViewKeyDown);
            // 
            // CrewTab
            // 
            this.CrewTab.Controls.Add(this.CrewDataGridView);
            this.CrewTab.Location = new System.Drawing.Point(4, 22);
            this.CrewTab.Name = "CrewTab";
            this.CrewTab.Padding = new System.Windows.Forms.Padding(3);
            this.CrewTab.Size = new System.Drawing.Size(848, 441);
            this.CrewTab.TabIndex = 1;
            this.CrewTab.Text = "Crew";
            this.CrewTab.UseVisualStyleBackColor = true;
            // 
            // CrewDataGridView
            // 
            this.CrewDataGridView.AllowDrop = true;
            this.CrewDataGridView.AllowUserToResizeRows = false;
            this.CrewDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CrewDataGridView.Location = new System.Drawing.Point(6, 6);
            this.CrewDataGridView.Name = "CrewDataGridView";
            this.CrewDataGridView.Size = new System.Drawing.Size(837, 429);
            this.CrewDataGridView.TabIndex = 1;
            this.CrewDataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCrewDataGridViewCellEnter);
            this.CrewDataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.OnCrewDataGridViewDefaultValuesNeeded);
            this.CrewDataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.OnDataGridViewRowPostPaint);
            this.CrewDataGridView.Sorted += new System.EventHandler(this.OnDataGridViewSorted);
            this.CrewDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnDataGridViewKeyDown);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyPasteToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.UndoRedoToolStripMenuItem,
            this.CheckDataValidityHeaderToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(859, 24);
            this.MenuStrip.TabIndex = 31;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // CopyPasteToolStripMenuItem
            // 
            this.CopyPasteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyDataToolStripMenuItem,
            this.CopySelectedRowsToolStripMenuItem,
            this.CutSelectedRowsToolStripMenuItem,
            this.PasteDataToolStripMenuItem,
            this.AppendDataFromClipboardToolStripMenuItem,
            this.InsertRowsFromClipboardToolStripMenuItem,
            this.CopyNamesToCreditedAsToolStripMenuItem});
            this.CopyPasteToolStripMenuItem.Name = "CopyPasteToolStripMenuItem";
            this.CopyPasteToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.CopyPasteToolStripMenuItem.Text = "&Copy && Paste";
            // 
            // CopyDataToolStripMenuItem
            // 
            this.CopyDataToolStripMenuItem.Name = "CopyDataToolStripMenuItem";
            this.CopyDataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.CopyDataToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.CopyDataToolStripMenuItem.Text = "&Copy All Rows";
            this.CopyDataToolStripMenuItem.Click += new System.EventHandler(this.OnCopyDataToolStripMenuItemClick);
            // 
            // CopySelectedRowsToolStripMenuItem
            // 
            this.CopySelectedRowsToolStripMenuItem.Name = "CopySelectedRowsToolStripMenuItem";
            this.CopySelectedRowsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.CopySelectedRowsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.CopySelectedRowsToolStripMenuItem.Text = "Copy &Selected Rows ";
            this.CopySelectedRowsToolStripMenuItem.Click += new System.EventHandler(this.OnCopySelectedRowsToolStripMenuItemClick);
            // 
            // CutSelectedRowsToolStripMenuItem
            // 
            this.CutSelectedRowsToolStripMenuItem.Name = "CutSelectedRowsToolStripMenuItem";
            this.CutSelectedRowsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.X)));
            this.CutSelectedRowsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.CutSelectedRowsToolStripMenuItem.Text = "Cut Selected &Rows";
            this.CutSelectedRowsToolStripMenuItem.Click += new System.EventHandler(this.OnCutSelectedRowsToolStripMenuItemClick);
            // 
            // PasteDataToolStripMenuItem
            // 
            this.PasteDataToolStripMenuItem.Name = "PasteDataToolStripMenuItem";
            this.PasteDataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
            this.PasteDataToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.PasteDataToolStripMenuItem.Text = "&Paste Data ";
            this.PasteDataToolStripMenuItem.Click += new System.EventHandler(this.OnPasteDataToolStripMenuItemClick);
            // 
            // AppendDataFromClipboardToolStripMenuItem
            // 
            this.AppendDataFromClipboardToolStripMenuItem.Name = "AppendDataFromClipboardToolStripMenuItem";
            this.AppendDataFromClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.V)));
            this.AppendDataFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.AppendDataFromClipboardToolStripMenuItem.Text = "&Append Data ";
            this.AppendDataFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.OnAppendDataFromClipboardToolStripMenuItemClick);
            // 
            // InsertRowsFromClipboardToolStripMenuItem
            // 
            this.InsertRowsFromClipboardToolStripMenuItem.Name = "InsertRowsFromClipboardToolStripMenuItem";
            this.InsertRowsFromClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.I)));
            this.InsertRowsFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.InsertRowsFromClipboardToolStripMenuItem.Text = "&Insert Rows ";
            this.InsertRowsFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.OnInsertRowsFromClipboardToolStripMenuItemClick);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindToolStripMenuItem,
            this.FindNextToolStripMenuItem,
            this.AddDividerToolStripMenuItem,
            this.InsertDividerToolStripMenuItem,
            this.InsertRowToolStripMenuItem,
            this.ClearGridToolStripMenuItem,
            this.ClearGridsToolStripMenuItem,
            this.SortCrewToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "&Edit";
            this.EditToolStripMenuItem.DropDownOpened += new System.EventHandler(this.OnEditToolStripMenuItemDropDownOpened);
            // 
            // FindToolStripMenuItem
            // 
            this.FindToolStripMenuItem.Name = "FindToolStripMenuItem";
            this.FindToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.FindToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.FindToolStripMenuItem.Text = "&Find";
            this.FindToolStripMenuItem.Click += new System.EventHandler(this.OnFindToolStripMenuItemClick);
            // 
            // FindNextToolStripMenuItem
            // 
            this.FindNextToolStripMenuItem.Name = "FindNextToolStripMenuItem";
            this.FindNextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.FindNextToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.FindNextToolStripMenuItem.Text = "Find &Next";
            this.FindNextToolStripMenuItem.Click += new System.EventHandler(this.OnFindNextToolStripMenuItemClick);
            // 
            // AddDividerToolStripMenuItem
            // 
            this.AddDividerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddEpisodeDividerToolStripMenuItem,
            this.AddGroupStartDividerToolStripMenuItem,
            this.AddGroupEndDividerToolStripMenuItem});
            this.AddDividerToolStripMenuItem.Name = "AddDividerToolStripMenuItem";
            this.AddDividerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.AddDividerToolStripMenuItem.Text = "&Append Divider";
            // 
            // AddEpisodeDividerToolStripMenuItem
            // 
            this.AddEpisodeDividerToolStripMenuItem.Name = "AddEpisodeDividerToolStripMenuItem";
            this.AddEpisodeDividerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.AddEpisodeDividerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.AddEpisodeDividerToolStripMenuItem.Text = "&Episode";
            this.AddEpisodeDividerToolStripMenuItem.Click += new System.EventHandler(this.OnAddEpisodeDividerToolStripMenuItemClick);
            // 
            // AddGroupStartDividerToolStripMenuItem
            // 
            this.AddGroupStartDividerToolStripMenuItem.Name = "AddGroupStartDividerToolStripMenuItem";
            this.AddGroupStartDividerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.AddGroupStartDividerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.AddGroupStartDividerToolStripMenuItem.Text = "&Group Start";
            this.AddGroupStartDividerToolStripMenuItem.Click += new System.EventHandler(this.OnAddGroupStartDividerToolStripMenuItemClick);
            // 
            // AddGroupEndDividerToolStripMenuItem
            // 
            this.AddGroupEndDividerToolStripMenuItem.Name = "AddGroupEndDividerToolStripMenuItem";
            this.AddGroupEndDividerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.AddGroupEndDividerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.AddGroupEndDividerToolStripMenuItem.Text = "Group En&d";
            this.AddGroupEndDividerToolStripMenuItem.Click += new System.EventHandler(this.OnAddGroupEndDividerToolStripMenuItemClick);
            // 
            // InsertDividerToolStripMenuItem
            // 
            this.InsertDividerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InsertEpisodeDividerToolStripMenuItem,
            this.InsertGroupStartDividerToolStripMenuItem,
            this.InsertGroupEndDividerToolStripMenuItem});
            this.InsertDividerToolStripMenuItem.Name = "InsertDividerToolStripMenuItem";
            this.InsertDividerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.InsertDividerToolStripMenuItem.Text = "&Insert Divider";
            // 
            // InsertEpisodeDividerToolStripMenuItem
            // 
            this.InsertEpisodeDividerToolStripMenuItem.Name = "InsertEpisodeDividerToolStripMenuItem";
            this.InsertEpisodeDividerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.InsertEpisodeDividerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.InsertEpisodeDividerToolStripMenuItem.Text = "&Episode";
            this.InsertEpisodeDividerToolStripMenuItem.Click += new System.EventHandler(this.OnInsertEpisodeDividerToolStripMenuItemClick);
            // 
            // InsertGroupStartDividerToolStripMenuItem
            // 
            this.InsertGroupStartDividerToolStripMenuItem.Name = "InsertGroupStartDividerToolStripMenuItem";
            this.InsertGroupStartDividerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.InsertGroupStartDividerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.InsertGroupStartDividerToolStripMenuItem.Text = "&Group Start";
            this.InsertGroupStartDividerToolStripMenuItem.Click += new System.EventHandler(this.OnInsertGroupStartDividerToolStripMenuItemClick);
            // 
            // InsertGroupEndDividerToolStripMenuItem
            // 
            this.InsertGroupEndDividerToolStripMenuItem.Name = "InsertGroupEndDividerToolStripMenuItem";
            this.InsertGroupEndDividerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.InsertGroupEndDividerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.InsertGroupEndDividerToolStripMenuItem.Text = "Group En&d";
            this.InsertGroupEndDividerToolStripMenuItem.Click += new System.EventHandler(this.OnInsertGroupEndDividerToolStripMenuItemClick);
            // 
            // InsertRowToolStripMenuItem
            // 
            this.InsertRowToolStripMenuItem.Name = "InsertRowToolStripMenuItem";
            this.InsertRowToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.InsertRowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.InsertRowToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.InsertRowToolStripMenuItem.Text = "Insert &Row";
            this.InsertRowToolStripMenuItem.Click += new System.EventHandler(this.OnInsertRowToolStripMenuItem1Click);
            // 
            // ClearGridToolStripMenuItem
            // 
            this.ClearGridToolStripMenuItem.Name = "ClearGridToolStripMenuItem";
            this.ClearGridToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.ClearGridToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ClearGridToolStripMenuItem.Text = "&Clear Grid";
            this.ClearGridToolStripMenuItem.Click += new System.EventHandler(this.OnClearGridToolStripMenuItemClick);
            // 
            // ClearGridsToolStripMenuItem
            // 
            this.ClearGridsToolStripMenuItem.Name = "ClearGridsToolStripMenuItem";
            this.ClearGridsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.ClearGridsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ClearGridsToolStripMenuItem.Text = "Clear &Grids";
            this.ClearGridsToolStripMenuItem.Click += new System.EventHandler(this.OnClearGridsToolStripMenuItemClick);
            // 
            // SortCrewToolStripMenuItem
            // 
            this.SortCrewToolStripMenuItem.Enabled = false;
            this.SortCrewToolStripMenuItem.Name = "SortCrewToolStripMenuItem";
            this.SortCrewToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.SortCrewToolStripMenuItem.Text = "Sort Crew";
            this.SortCrewToolStripMenuItem.Click += new System.EventHandler(this.OnSortCrewToolStripMenuItemClick);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowVerificationCheckboxesToolStripMenuItem,
            this.AllowSortingByClickingOnHeaderRowsToolStripMenuItem,
            this.PutCastRoleAsFirstColumnToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStripMenuItem.Text = "&View";
            // 
            // ShowVerificationCheckboxesToolStripMenuItem
            // 
            this.ShowVerificationCheckboxesToolStripMenuItem.CheckOnClick = true;
            this.ShowVerificationCheckboxesToolStripMenuItem.Name = "ShowVerificationCheckboxesToolStripMenuItem";
            this.ShowVerificationCheckboxesToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.ShowVerificationCheckboxesToolStripMenuItem.Text = "&Show Verification Checkboxes";
            this.ShowVerificationCheckboxesToolStripMenuItem.Click += new System.EventHandler(this.OnShowVerificationCheckboxesToolStripMenuItemClick);
            // 
            // AllowSortingByClickingOnHeaderRowsToolStripMenuItem
            // 
            this.AllowSortingByClickingOnHeaderRowsToolStripMenuItem.CheckOnClick = true;
            this.AllowSortingByClickingOnHeaderRowsToolStripMenuItem.Name = "AllowSortingByClickingOnHeaderRowsToolStripMenuItem";
            this.AllowSortingByClickingOnHeaderRowsToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.AllowSortingByClickingOnHeaderRowsToolStripMenuItem.Text = "&Allow Sorting by Clicking on Header Rows";
            this.AllowSortingByClickingOnHeaderRowsToolStripMenuItem.Click += new System.EventHandler(this.OnAllowSortingByClickingOnHeaderRowsToolStripMenuItemClick);
            // 
            // PutCastRoleAsFirstColumnToolStripMenuItem
            // 
            this.PutCastRoleAsFirstColumnToolStripMenuItem.CheckOnClick = true;
            this.PutCastRoleAsFirstColumnToolStripMenuItem.Name = "PutCastRoleAsFirstColumnToolStripMenuItem";
            this.PutCastRoleAsFirstColumnToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.PutCastRoleAsFirstColumnToolStripMenuItem.Text = "Put &Cast Role as First Column";
            this.PutCastRoleAsFirstColumnToolStripMenuItem.Click += new System.EventHandler(this.OnPutCastRoleAsFirstColumnToolStripMenuItemClick);
            // 
            // UndoRedoToolStripMenuItem
            // 
            this.UndoRedoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem,
            this.RedoToolStripMenuItem});
            this.UndoRedoToolStripMenuItem.Name = "UndoRedoToolStripMenuItem";
            this.UndoRedoToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.UndoRedoToolStripMenuItem.Text = "&Undo && Redo";
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.UndoToolStripMenuItem.Text = "&Undo";
            this.UndoToolStripMenuItem.Click += new System.EventHandler(this.OnUndoToolStripMenuItemClick);
            // 
            // RedoToolStripMenuItem
            // 
            this.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem";
            this.RedoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.RedoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.RedoToolStripMenuItem.Text = "&Redo";
            this.RedoToolStripMenuItem.Click += new System.EventHandler(this.OnRedoToolStripMenuItemClick);
            // 
            // CheckDataValidityHeaderToolStripMenuItem
            // 
            this.CheckDataValidityHeaderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckDataValidityToolStripMenuItem});
            this.CheckDataValidityHeaderToolStripMenuItem.Name = "CheckDataValidityHeaderToolStripMenuItem";
            this.CheckDataValidityHeaderToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.CheckDataValidityHeaderToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.CheckDataValidityHeaderToolStripMenuItem.Text = "Check &Data Validity";
            // 
            // CheckDataValidityToolStripMenuItem
            // 
            this.CheckDataValidityToolStripMenuItem.Name = "CheckDataValidityToolStripMenuItem";
            this.CheckDataValidityToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F2)));
            this.CheckDataValidityToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.CheckDataValidityToolStripMenuItem.Text = "Check &Data Validity";
            this.CheckDataValidityToolStripMenuItem.Click += new System.EventHandler(this.OnCheckDataValidityToolStripMenuItemClick);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReadmeToolStripMenuItem,
            this.CheckForUpdatesToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "&Help";
            // 
            // ReadmeToolStripMenuItem
            // 
            this.ReadmeToolStripMenuItem.Name = "ReadmeToolStripMenuItem";
            this.ReadmeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.ReadmeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.ReadmeToolStripMenuItem.Text = "&Read Me";
            this.ReadmeToolStripMenuItem.Click += new System.EventHandler(this.OnReadmeToolStripMenuItemClick);
            // 
            // CheckForUpdatesToolStripMenuItem
            // 
            this.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem";
            this.CheckForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.CheckForUpdatesToolStripMenuItem.Text = "&Check for Updates";
            this.CheckForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.OnCheckForUpdatesToolStripMenuItemClick);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.AboutToolStripMenuItem.Text = "&About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutToolStripMenuItemClick);
            // 
            // copyNamesToCreditedAsToolStripMenuItem
            // 
            this.CopyNamesToCreditedAsToolStripMenuItem.Name = "copyNamesToCreditedAsToolStripMenuItem";
            this.CopyNamesToCreditedAsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.CopyNamesToCreditedAsToolStripMenuItem.Text = "Copy Name(s) to \"Credited As\"";
            this.CopyNamesToCreditedAsToolStripMenuItem.Click += new System.EventHandler(this.OnCopyNamesToCreditedAsToolStripMenuItemClick);
            // 
            // EditingPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CastCrewTabControl);
            this.Controls.Add(this.MenuStrip);
            this.Name = "EditingPane";
            this.Size = new System.Drawing.Size(859, 497);
            this.Load += new System.EventHandler(this.OnEditingPaneLoad);
            this.CastCrewTabControl.ResumeLayout(false);
            this.CastTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CastDataGridView)).EndInit();
            this.CrewTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CrewDataGridView)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl CastCrewTabControl;
        private System.Windows.Forms.TabPage CastTab;
        private System.Windows.Forms.TabPage CrewTab;
        private CustomDataGridView CastDataGridView;
        private CustomDataGridView CrewDataGridView;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReadmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddEpisodeDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AppendDataFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearGridsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddGroupStartDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddGroupEndDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopySelectedRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyPasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UndoRedoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RedoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertEpisodeDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertGroupStartDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertGroupEndDividerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckDataValidityHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckDataValidityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertRowsFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CutSelectedRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowVerificationCheckboxesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AllowSortingByClickingOnHeaderRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PutCastRoleAsFirstColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SortCrewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyNamesToCreditedAsToolStripMenuItem;
    }
}