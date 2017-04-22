using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.DVDProfiler.DVDProfilerXML.Version390;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public partial class EditingPane : UserControl
    {
        private Boolean m_CastTabIsActive;

        private UndoStack m_UndoStack;

        private Assembly m_HostAssembly;

        private event EventHandler<UndoStackEventArgs> m_UndoStateChanged;

        private event EventHandler<UndoStackEventArgs> m_RedoStateChanged;

        private String FindString { get; set; }

        public EditingPane()
        {
            this.m_CastTabIsActive = true;
            InitializeComponent();
        }

        private UndoStack UndoStack
        {
            [DebuggerStepThrough()]
            get
            {
                return (this.m_UndoStack);
            }
        }

        private Assembly HostAssembly
        {
            [DebuggerStepThrough()]
            get
            {
                return (this.m_HostAssembly);
            }
        }

        private Boolean CastTabIsActive
        {
            get
            {
                return (this.m_CastTabIsActive);
            }
            set
            {
                this.m_CastTabIsActive = value;
            }
        }

        public void Init(Assembly hostAssembly)
        {
            this.m_HostAssembly = hostAssembly;
            this.m_UndoStack = new UndoStack(this.CastDataGridView, this.CrewDataGridView, this);
            this.UndoStateChanged += this.OnEditingPaneUndoStateChanged;
            this.RedoStateChanged += this.OnEditingPaneRedoStateChanged;
        }

        public void Init(CastInformation castInformation, CrewInformation crewInformation, Assembly hostAssembly)
        {
            Int32 token;

            this.m_HostAssembly = hostAssembly;
            this.m_UndoStack = new UndoStack(this.CastDataGridView, this.CrewDataGridView, this, castInformation, crewInformation);
            token = this.UndoStack.BeginDo(ActionTexts.InitializeGrid);
            this.AddCast(castInformation);
            this.AddCrew(crewInformation);
            this.UndoStack.AbortDo(token);
            this.UndoStateChanged += this.OnEditingPaneUndoStateChanged;
            this.RedoStateChanged += this.OnEditingPaneRedoStateChanged;
        }

        public Boolean HasChanged
        {
            get
            {
                return (this.UndoStack.HasChanged);
            }
        }

        private Boolean CanUndo
        {
            get
            {
                return (this.UndoStack.CanUndo);
            }
        }

        private Boolean CanRedo
        {
            get
            {
                return (this.UndoStack.CanRedo);
            }
        }

        private event EventHandler<UndoStackEventArgs> UndoStateChanged
        {
            add
            {
                if (this.m_UndoStateChanged == null)
                {
                    this.UndoStack.UndoStateChanged += this.OnUndoStateChanged;
                }
                this.m_UndoStateChanged += value;
            }
            remove
            {
                this.m_UndoStateChanged -= value;
                if (this.m_UndoStateChanged == null)
                {
                    this.UndoStack.UndoStateChanged -= this.OnUndoStateChanged;
                }
            }
        }

        void OnUndoStateChanged(Object sender, UndoStackEventArgs e)
        {
            if (this.m_UndoStateChanged != null)
            {
                this.m_UndoStateChanged(sender, e);
            }
        }


        private event EventHandler<UndoStackEventArgs> RedoStateChanged
        {
            add
            {
                if (this.m_RedoStateChanged == null)
                {
                    this.UndoStack.RedoStateChanged += this.OnRedoStateChanged;
                }
                this.m_RedoStateChanged += value;
            }
            remove
            {
                this.m_RedoStateChanged -= value;
                if (this.m_RedoStateChanged == null)
                {
                    this.UndoStack.RedoStateChanged -= this.OnRedoStateChanged;
                }
            }
        }

        void OnRedoStateChanged(Object sender, UndoStackEventArgs e)
        {
            if (this.m_RedoStateChanged != null)
            {
                this.m_RedoStateChanged(sender, e);
            }
        }

        private void Undo()
        {
            this.UndoStack.Undo();
        }

        private void Redo()
        {
            this.UndoStack.Redo();
        }

        private void CreateDataGridViewColumns()
        {
            DataGridViewHelper.CreateCastColumns(this.CastDataGridView);
            DataGridViewHelper.CreateCrewColumns(this.CrewDataGridView);
        }

        private void OnCrewDataGridViewCellValueChanged(Object sender, DataGridViewCellEventArgs e)
        {
            Int32 token;

            if (e.ColumnIndex == 3)
            {
                if (this.CheckBirthYear(e, this.CrewDataGridView, "Crew") == false)
                {
                    return;
                }
            }
            token = this.UndoStack.BeginDo(ActionTexts.ChangeValue);
            DataGridViewHelper.OnCrewDataGridViewCellValueChanged(sender, e);
            this.UndoStack.CommitDo(token);
        }

        private void OnCastDataGridViewCellValueChanged(Object sender, DataGridViewCellEventArgs e)
        {
            Int32 token;

            if (e.ColumnIndex == 3)
            {
                if (this.CheckBirthYear(e, this.CastDataGridView, "Cast") == false)
                {
                    return;
                }
            }
            token = this.UndoStack.BeginDo(ActionTexts.ChangeValue);
            this.UndoStack.CommitDo(token);
        }

        private Boolean CheckBirthYear(DataGridViewCellEventArgs e, DataGridView dataGridView, String gridType)
        {
            Int32 token;
            DataGridViewCell cell;
            String value;

            cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            value = cell.Value as String;
            if (String.IsNullOrEmpty(value) == false)
            {
                UInt16 birthYear;

                if (UInt16.TryParse(value.ToString(), out birthYear) == false)
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.BirthYearIsNoNumber
                        , gridType, e.RowIndex + 1), MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    token = this.UndoStack.BeginDo(ActionTexts.SetDefaultValues);
                    cell.Value = String.Empty;
                    this.UndoStack.CommitDo(token);
                    cell.Selected = true;
                    return (false);
                }
                return (true);
            }
            return (true);
        }

        private void AddDivider(Boolean insert, DividerType dividerType)
        {
            this.InsertDividerRow(this.CastTabIsActive, insert, dividerType);
        }

        private DataGridViewRow InsertDividerRow(Boolean isCast, Boolean insert, DividerType dividerType)
        {
            DataGridViewRow row;
            Int32 token;

            token = this.UndoStack.BeginDo(ActionTexts.AddDivider);
            if (isCast)
            {
                row = this.InsertCastRow(insert);
            }
            else
            {
                row = this.InsertCrewRow(insert);
            }
            GetDividerRowDefaultValues(isCast, dividerType, row, token);
            this.UndoStack.CommitDo(token);
            return (row);
        }

        private DataGridViewRow CreateDividerRow(Boolean isCast, DividerType dividerType)
        {
            DataGridViewRow row;
            Int32 token;

            token = this.UndoStack.BeginDo(ActionTexts.AddDivider);
            if (isCast)
            {
                row = this.CreateCastRow();
            }
            else
            {
                row = this.CreateCrewRow();
            }
            GetDividerRowDefaultValues(isCast, dividerType, row, token);
            this.UndoStack.CommitDo(token);
            return (row);
        }

        private void GetDividerRowDefaultValues(Boolean isCast, DividerType dividerType, DataGridViewRow row, Int32 token)
        {
            List<Int32> readWriteCells;

            readWriteCells = new List<Int32>(row.Cells.Count);
            row.Cells[0].Value = DataGridViewHelper.FirstNames.Divider;
            readWriteCells.Add(1);
            row.Cells[7].Value = dividerType;
            switch (dividerType)
            {
                case (DividerType.Episode):
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.Cells[1].Value = ActionTexts.EnterEpisodeNameHere;
                        if (isCast == false)
                        {
                            row.Cells[4].Value = String.Empty;
                            row.Cells[5].Value = String.Empty;
                        }
                        break;
                    }
                case (DividerType.Group):
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                        row.Cells[1].Value = ActionTexts.EnterGroupNameHere;
                        if (isCast == false)
                        {
                            readWriteCells.Add(4);
                            GetDefaultCreditType(row);
                            row.Cells[5].Value = String.Empty;
                        }
                        break;
                    }
                case (DividerType.EndDiv):
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                        readWriteCells.Remove(1);
                        if (isCast == false)
                        {
                            readWriteCells.Add(4);
                            GetDefaultCreditType(row);
                            row.Cells[5].Value = String.Empty;
                        }
                        break;
                    }
            }
            for (Int32 i = 0; i < row.Cells.Count; i++)
            {
                if (readWriteCells.Contains(i) == false)
                {
                    row.Cells[i].ReadOnly = true;
                }
            }
        }

        private DataGridViewRow InsertCrewRow(Boolean currentPosition)
        {
            DataGridViewRow row;
            Int32 index;
            Int32 token;

            token = this.UndoStack.BeginDo(ActionTexts.InsertRow);
            if (currentPosition)
            {
                index = GetCurrentRowIndex(this.CrewDataGridView);
            }
            else
            {
                index = GetLastRowRowIndex(this.CrewDataGridView);
            }
            this.CrewDataGridView.Rows.Insert(index, 1);
            row = this.CrewDataGridView.Rows[index];
            this.OnCrewDataGridViewDefaultValuesNeeded(this.CrewDataGridView, new DataGridViewRowEventArgs(row));
            this.UndoStack.CommitDo(token);
            return (row);
        }

        private DataGridViewRow InsertCastRow(Boolean currentPosition)
        {
            DataGridViewRow row;
            Int32 index;
            Int32 token;

            token = this.UndoStack.BeginDo(ActionTexts.InsertRow);
            if (currentPosition)
            {
                index = GetCurrentRowIndex(this.CastDataGridView);
            }
            else
            {
                index = GetLastRowRowIndex(this.CastDataGridView);
            }
            this.CastDataGridView.Rows.Insert(index, 1);
            row = this.CastDataGridView.Rows[index];
            this.OnCastDataGridViewDefaultValuesNeeded(this.CastDataGridView, new DataGridViewRowEventArgs(row));
            this.UndoStack.CommitDo(token);
            return (row);
        }

        private Int32 GetCurrentRowIndex(DataGridView dataGridView)
        {
            Int32 index;

            if ((dataGridView.CurrentRow != null) && (dataGridView.CurrentRow.Index >= 0))
            {
                index = dataGridView.CurrentRow.Index;
            }
            else
            {
                index = 0;
            }
            return (index);
        }

        private DataGridViewRow CreateCastRow()
        {
            DataGridViewRow row;

            row = new DataGridViewRow();
            for (Int32 i = 0; i < this.CastDataGridView.Columns.Count; i++)
            {
                DataGridViewCell cell;
                DataGridViewColumn column;

                column = this.CastDataGridView.Columns[i];
                cell = (DataGridViewCell)(column.CellTemplate.Clone());
                row.Cells.Add(cell);
            }
            return (row);
        }

        private DataGridViewRow CreateCrewRow()
        {
            DataGridViewRow row;

            row = new DataGridViewRow();
            for (Int32 i = 0; i < this.CrewDataGridView.Columns.Count; i++)
            {
                DataGridViewCell cell;
                DataGridViewColumn column;

                column = this.CrewDataGridView.Columns[i];
                cell = (DataGridViewCell)(column.CellTemplate.Clone());
                if (i == 5)
                {
                    CreditTypesDataGridViewHelper.AllowAllCreditSubtypes(((DataGridViewComboBoxCell)cell).Items);
                }
                row.Cells.Add(cell);
            }
            return (row);
        }

        private int GetLastRowRowIndex(CustomDataGridView dataGridView)
        {
            Int32 index;

            if (dataGridView.RowCount > 0)
            {
                index = dataGridView.RowCount - 1;
            }
            else
            {
                index = 0;
            }
            return (index);
        }

        private void CopyDataToClipboard()
        {
            if (this.CastTabIsActive)
            {
                DataGridViewHelper.CopyCastToClipboard(this.CastDataGridView);
            }
            else
            {
                DataGridViewHelper.CopyCrewToClipboard(this.CrewDataGridView);
            }
        }

        private void OnCrewDataGridViewDefaultValuesNeeded(Object sender, DataGridViewRowEventArgs e)
        {
            Int32 token;
            DataGridViewRow row;

            token = this.UndoStack.BeginDo(ActionTexts.SetDefaultValues);
            row = e.Row;
            row.Cells[DataGridViewHelper.ColumnNames.FirstName].Value = String.Empty;
            row.Cells[DataGridViewHelper.ColumnNames.MiddleName].Value = String.Empty;
            row.Cells[DataGridViewHelper.ColumnNames.LastName].Value = String.Empty;
            row.Cells[DataGridViewHelper.ColumnNames.BirthYear].Value = String.Empty;
            GetDefaultCreditType(row);
            row.Cells[DataGridViewHelper.ColumnNames.CustomRole].Value = String.Empty;
            row.Cells[DataGridViewHelper.ColumnNames.CreditedAs].Value = String.Empty;
            this.UndoStack.AbortDo(token);
        }

        private static void GetDefaultCreditType(DataGridViewRow row)
        {
            Boolean takeDefault;

            takeDefault = true;
            if (row.Index > 0)
            {
                DataGridViewRow oneRowUp;
                DataGridViewCell cellOneRowUp;

                oneRowUp = row.DataGridView.Rows[row.Index - 1];
                cellOneRowUp = oneRowUp.Cells[4];
                if (String.IsNullOrEmpty((String)(cellOneRowUp.Value)) == false)
                {
                    row.Cells[4].Value = cellOneRowUp.Value;
                    cellOneRowUp = oneRowUp.Cells[DataGridViewHelper.ColumnNames.CreditSubtype];
                    if (String.IsNullOrEmpty((String)(cellOneRowUp.Value)) == false)
                    {
                        row.Cells[5].Value = cellOneRowUp.Value;
                    }
                    else
                    {
                        row.Cells[5].Value = CreditTypesDataGridViewHelper.CreditSubtypes.Custom;
                    }
                    takeDefault = false;
                }
            }
            if (takeDefault)
            {
                row.Cells[4].Value = CreditTypesDataGridViewHelper.CreditTypes.Other;
                row.Cells[5].Value = CreditTypesDataGridViewHelper.CreditSubtypes.Custom;
            }
        }

        private void OnCastDataGridViewDefaultValuesNeeded(Object sender, DataGridViewRowEventArgs e)
        {
            Int32 token;

            token = this.UndoStack.BeginDo(ActionTexts.SetDefaultValues);
            e.Row.Cells[DataGridViewHelper.ColumnNames.FirstName].Value = String.Empty;
            e.Row.Cells[DataGridViewHelper.ColumnNames.MiddleName].Value = String.Empty;
            e.Row.Cells[DataGridViewHelper.ColumnNames.LastName].Value = String.Empty;
            e.Row.Cells[DataGridViewHelper.ColumnNames.BirthYear].Value = String.Empty;
            e.Row.Cells[DataGridViewHelper.ColumnNames.Role].Value = String.Empty;
            e.Row.Cells[DataGridViewHelper.ColumnNames.Voice].Value = false;
            e.Row.Cells[DataGridViewHelper.ColumnNames.Uncredited].Value = false;
            e.Row.Cells[DataGridViewHelper.ColumnNames.CreditedAs].Value = String.Empty;
            this.UndoStack.AbortDo(token);
        }

        private void PasteData(Boolean question)
        {
            if (this.CastTabIsActive)
            {
                CastInformation castInformation;

                if (Utilities.TryGetCastInformationFromClipboard(out castInformation))
                {
                    if ((this.CastDataGridView.Rows.Count > 1) && question)
                    {
                        if (MessageBox.Show(MessageBoxTexts.CastOverwriteData, MessageBoxTexts.WarningHeader
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Int32 token;

                            token = this.UndoStack.BeginDo(ActionTexts.PasteFromClipboard);
                            this.CastDataGridView.Rows.Clear();
                            this.AddCast(castInformation);
                            this.UndoStack.CommitDo(token);
                        }
                    }
                    else
                    {
                        Int32 token;

                        token = this.UndoStack.BeginDo(ActionTexts.PasteFromClipboard);
                        this.AddCast(castInformation);
                        this.UndoStack.CommitDo(token);
                    }
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.CastDataCantBeRead, MessageBoxTexts.ErrorHeader
                                            , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                CrewInformation crewInformation;

                if (Utilities.TryGetCrewInformationFromClipboard(out crewInformation))
                {
                    if ((this.CrewDataGridView.Rows.Count > 1) && question)
                    {
                        if (MessageBox.Show(MessageBoxTexts.CrewOverwriteData, MessageBoxTexts.WarningHeader
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Int32 token;

                            token = this.UndoStack.BeginDo(ActionTexts.PasteFromClipboard);
                            this.CrewDataGridView.Rows.Clear();
                            this.AddCrew(crewInformation);
                            this.UndoStack.CommitDo(token);
                        }
                    }
                    else
                    {
                        Int32 token;

                        token = this.UndoStack.BeginDo(ActionTexts.PasteFromClipboard);
                        this.AddCrew(crewInformation);
                        this.UndoStack.CommitDo(token);
                    }
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.CrewDataCantBeRead, MessageBoxTexts.ErrorHeader
                                             , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        internal void AddCast(CastInformation ci)
        {
            if ((ci.CastList != null) && (ci.CastList.Length > 0))
            {
                List<DataGridViewRow> rows;

                rows = new List<DataGridViewRow>(ci.CastList.Length);
                for (Int32 i = 0; i < ci.CastList.Length; i++)
                {
                    CastMember actor;

                    actor = ci.CastList[i] as CastMember;
                    if (actor != null)
                    {
                        DataGridViewRow row;

                        row = this.CreateCastRow();
                        row.Cells[0].Value = actor.FirstName;
                        row.Cells[1].Value = actor.MiddleName;
                        row.Cells[2].Value = actor.LastName;
                        if (actor.BirthYear != 0)
                        {
                            row.Cells[3].Value = actor.BirthYear;
                        }
                        row.Cells[4].Value = actor.Role;
                        row.Cells[5].Value = actor.Voice;
                        row.Cells[6].Value = actor.Uncredited;
                        row.Cells[7].Value = actor.CreditedAs;
                        rows.Add(row);
                    }
                    else
                    {
                        Divider divider;
                        DataGridViewRow row;

                        divider = (Divider)(ci.CastList[i]);
                        row = this.CreateDividerRow(true, divider.Type);
                        row.Cells[1].Value = divider.Caption;
                        rows.Add(row);
                    }
                }
                this.CastDataGridView.Rows.AddRange(rows.ToArray());
            }
        }

        internal void AddCrew(CrewInformation ci)
        {
            if ((ci.CrewList != null) && (ci.CrewList.Length > 0))
            {
                List<DataGridViewRow> rows;

                rows = new List<DataGridViewRow>(ci.CrewList.Length);
                for (Int32 i = 0; i < ci.CrewList.Length; i++)
                {
                    CrewMember crew;

                    crew = ci.CrewList[i] as CrewMember;
                    if (crew != null)
                    {
                        DataGridViewRow row;

                        row = this.CreateCrewRow();
                        row.Cells[0].Value = crew.FirstName;
                        row.Cells[1].Value = crew.MiddleName;
                        row.Cells[2].Value = crew.LastName;
                        if (crew.BirthYear != 0)
                        {
                            row.Cells[3].Value = crew.BirthYear;
                        }
                        row.Cells[4].Value = crew.CreditType;
                        row.Cells[5].Value = crew.CreditSubtype;
                        row.Cells[6].Value = crew.CustomRole;
                        row.Cells[7].Value = crew.CreditedAs;
                        rows.Add(row);
                    }
                    else
                    {
                        CrewDivider divider;
                        DataGridViewRow row;

                        divider = (CrewDivider)(ci.CrewList[i]);
                        row = this.CreateDividerRow(false, divider.Type);
                        row.Cells[1].Value = divider.Caption;
                        row.Cells[4].Value = divider.CreditType;
                        rows.Add(row);
                    }
                }
                //this.CrewDataGridView.CellValueChanged -= this.OnCrewDataGridViewCellValueChanged;
                this.CrewDataGridView.Rows.AddRange(rows.ToArray());
                //for(Int32 i = 0; i < this.CrewDataGridView.Rows.Count - 1; i++)
                //{
                //    DataGridViewRow row;

                //    row = this.CrewDataGridView.Rows[i];
                //    if(row.Cells[0].Value.ToString() != DataGridViewHelper.FirstNames.Divider)
                //    {
                //        String creditSubtype;

                //        creditSubtype = row.Cells[5].Value.ToString();
                //        row.Cells[5].Value = String.Empty;
                //        CreditTypesDataGridViewHelper.FillCreditSubtypes(row.Cells[4].Value.ToString()
                //            , ((DataGridViewComboBoxCell)(row.Cells[5])).Items);
                //        row.Cells[5].Value = creditSubtype;
                //    }
                //}
                //this.CrewDataGridView.CellValueChanged += this.OnCrewDataGridViewCellValueChanged;
            }
        }

        private void OnMovieCastCrewTabControlSelected(Object sender, TabControlEventArgs e)
        {
            this.CastTabIsActive = (e.TabPage == this.CastTab);
        }

        private Boolean CopySelectedRows()
        {
            if (this.CastTabIsActive)
            {
                if (this.CastDataGridView.SelectedRows.Count > 0)
                {
                    return (DataGridViewHelper.CopyCastToClipboard(this.CastDataGridView.SelectedRows));
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.NoRowsSelected, MessageBoxTexts.WarningHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (this.CrewDataGridView.SelectedRows.Count > 0)
                {
                    return (DataGridViewHelper.CopyCrewToClipboard(this.CrewDataGridView.SelectedRows));
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.NoRowsSelected, MessageBoxTexts.WarningHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return (false);
        }

        private void ClearGrid()
        {
            if (this.CastTabIsActive)
            {
                if (this.CastDataGridView.Rows.Count > 1)
                {
                    if (MessageBox.Show(MessageBoxTexts.CastEmptyData, MessageBoxTexts.WarningHeader
                                 , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Int32 token;

                        token = this.UndoStack.BeginDo(ActionTexts.ClearGrid);
                        this.CastDataGridView.Rows.Clear();
                        this.UndoStack.CommitDo(token);
                    }
                }
            }
            else
            {
                if (this.CrewDataGridView.Rows.Count > 1)
                {
                    if (MessageBox.Show(MessageBoxTexts.CrewEmptyData, MessageBoxTexts.WarningHeader
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Int32 token;

                        token = this.UndoStack.BeginDo(ActionTexts.ClearGrid);
                        this.CrewDataGridView.Rows.Clear();
                        this.UndoStack.CommitDo(token);
                    }
                }
            }
        }

        private void ClearGrids()
        {
            if ((this.CastDataGridView.Rows.Count > 1) || (this.CrewDataGridView.Rows.Count > 1))
            {
                if (MessageBox.Show(MessageBoxTexts.CastCrewEmptyData, MessageBoxTexts.WarningHeader
                          , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Int32 token;

                    token = this.UndoStack.BeginDo(ActionTexts.ClearGrids);
                    this.CastDataGridView.Rows.Clear();
                    this.CrewDataGridView.Rows.Clear();
                    this.UndoStack.CommitDo(token);
                }
            }
        }

        private void RegisterEvents()
        {
            this.CrewDataGridView.CellValueChanged += this.OnCrewDataGridViewCellValueChanged;
            this.CastDataGridView.CellValueChanged += this.OnCastDataGridViewCellValueChanged;
            this.CrewDataGridView.RowsRemoved += this.OnDataGridViewRowsRemoved;
            this.CastDataGridView.RowsRemoved += this.OnDataGridViewRowsRemoved;
            this.CrewDataGridView.RowDragDrop += this.OnDataGridViewRowDragDrop;
            this.CastDataGridView.RowDragDrop += this.OnDataGridViewRowDragDrop;
        }

        private void OnDataGridViewRowDragDrop(Object sender, CustomDataGridView.RowDragDropEventArgs e)
        {
            if (e.SoureRow != e.TargetRow)
            {
                Int32 token;

                token = this.UndoStack.BeginDo(ActionTexts.MoveRow);
                this.UndoStack.CommitDo(token);
            }
        }

        private void OnDataGridViewRowsRemoved(Object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CustomDataGridView dataGridView;

            dataGridView = (CustomDataGridView)sender;
            if ((dataGridView.IsRowDragDrop == false) && (dataGridView.SelectedRows.Count == 0))
            {
                Int32 token;

                token = this.UndoStack.BeginDo(ActionTexts.RemoveRows);
                this.UndoStack.CommitDo(token);
            }
        }

        private void OnCheckForUpdatesToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.CheckForNewVersion();
        }

        private void OnEditingPaneLoad(Object sender, EventArgs e)
        {
            this.UndoToolStripMenuItem.Enabled = false;
            this.RedoToolStripMenuItem.Enabled = false;
            this.CreateDataGridViewColumns();
            this.RegisterEvents();
        }

        public CastInformation GetCastInformation()
        {
            return (DataGridViewHelper.GetCastInformation(this.CastDataGridView));
        }

        public CrewInformation GetCrewInformation()
        {
            return (DataGridViewHelper.GetCrewInformation(this.CrewDataGridView));
        }

        public Boolean CheckDataValidity()
        {
            if (CheckCastValidity())
            {
                return (CheckCrewValidity());
            }
            else
            {
                return (false);
            }
        }

        private Boolean CheckCrewValidity()
        {
            return (DataGridViewHelper.CheckCrewValidity(this.GetCrewInformation()));
        }

        private Boolean CheckCastValidity()
        {
            return (DataGridViewHelper.CheckCastValidity(this.GetCastInformation()));
        }

        private void OpenReadme()
        {
            String helpFile;

            helpFile = (new FileInfo(this.HostAssembly.Location)).DirectoryName + @"\Readme\readme.html";
            if (File.Exists(helpFile))
            {
                using (HelpForm helpForm = new HelpForm(helpFile))
                {
                    helpForm.Text = "Read Me";
                    helpForm.ShowDialog(this);
                }
            }
        }

        private void OnEditingPaneRedoStateChanged(Object sender, UndoStackEventArgs e)
        {
            this.RedoToolStripMenuItem.Text = String.Format(ActionTexts.Redo, e.Action);
            this.RedoToolStripMenuItem.Enabled = this.CanRedo;
        }

        private void OnEditingPaneUndoStateChanged(Object sender, UndoStackEventArgs e)
        {
            this.UndoToolStripMenuItem.Text = String.Format(ActionTexts.Undo, e.Action);
            this.UndoToolStripMenuItem.Enabled = this.CanUndo;
        }

        private void OnDataGridViewRowPostPaint(Object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dataGridView;

            dataGridView = (DataGridView)sender;
            using (SolidBrush b = new SolidBrush(dataGridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), dataGridView.DefaultCellStyle.Font
                    , b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        public void OnAddEpisodeDividerToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.AddDivider(false, DividerType.Episode);
        }

        public void OnCopyDataToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.CopyDataToClipboard();
        }

        private void OnPasteDataToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.PasteData(true);
        }

        private void OnAppendDataFromClipboardToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.PasteData(false);
        }

        private void OnAboutToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            using (AboutBox aboutBox = new AboutBox(this.HostAssembly))
            {
                aboutBox.ShowDialog();
            }
        }

        private void OnClearGridToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.ClearGrid();
        }

        private void OnClearGridsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.ClearGrids();
        }

        private void OnReadmeToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.OpenReadme();
        }

        private void OnAddGroupStartDividerToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.AddDivider(false, DividerType.Group);
        }

        private void OnAddGroupEndDividerToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.AddDivider(false, DividerType.EndDiv);
        }

        private void OnCopySelectedRowsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.CopySelectedRows();
        }

        private void OnUndoToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.Undo();
        }

        private void OnRedoToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.Redo();
        }

        private void OnInsertEpisodeDividerToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.AddDivider(true, DividerType.Episode);
        }

        private void OnInsertGroupStartDividerToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.AddDivider(true, DividerType.Group);
        }

        private void OnInsertGroupEndDividerToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            this.AddDivider(true, DividerType.EndDiv);
        }

        private void OnCheckDataValidityToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            if (this.CheckDataValidity())
            {
                MessageBox.Show(MessageBoxTexts.CheckOk, MessageBoxTexts.Information
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OnCrewDataGridViewCellEnter(Object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                DataGridViewComboBoxCell cell;

                cell = (DataGridViewComboBoxCell)(this.CrewDataGridView.Rows[e.RowIndex].Cells[5]);
                if (cell.ReadOnly == false)
                {
                    Int32 token;
                    Object currentValue;

                    currentValue = cell.Value;
                    token = this.UndoStack.BeginDo(ActionTexts.SetDefaultValues);
                    cell.Value = String.Empty;
                    CreditTypesDataGridViewHelper.FillCreditSubtypes(this.CrewDataGridView
                        .Rows[e.RowIndex].Cells[4].Value.ToString(), cell.Items);
                    cell.Value = currentValue;
                    this.UndoStack.AbortDo(token);
                }
            }
        }

        private void OnInsertRowToolStripMenuItem1Click(Object sender, EventArgs e)
        {
            this.Validate();
            if (this.CastTabIsActive)
            {
                this.InsertCastRow(true);
            }
            else
            {
                this.InsertCrewRow(true);
            }
        }

        private void OnDataGridViewKeyDown(Object sender, KeyEventArgs e)
        {
            DataGridView dataGridView;

            dataGridView = (DataGridView)sender;
            if ((e.KeyData == Keys.Delete) && (dataGridView.SelectedCells.Count > 0))
            {
                List<Int32> rowList;
                Int32 token;

                rowList = new List<Int32>(dataGridView.SelectedCells.Count);
                foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                {
                    if ((cell.RowIndex != dataGridView.Rows.Count - 1)
                        && (rowList.Contains(cell.RowIndex) == false))
                    {
                        rowList.Add(cell.RowIndex);
                    }
                }
                rowList.Sort();
                token = this.UndoStack.BeginDo(ActionTexts.RemoveRows);
                for (Int32 i = rowList.Count - 1; i >= 0; i--)
                {
                    dataGridView.Rows.RemoveAt(rowList[i]);
                }
                this.UndoStack.CommitDo(token);
            }
        }

        private void CheckForNewVersion()
        {
            OnlineAccess.Init("Doena Soft.", "FreestyleCastCrewEdit");
            OnlineAccess.CheckForNewVersion("http://doena-soft.de/dvdprofiler/3.9.0/versions.xml", this, "FreestyleCastCrewEdit", this.GetType().Assembly);
        }

        private void OnInsertRowsFromClipboardToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            if (this.CastTabIsActive)
            {
                CastInformation castInformation;

                if (Utilities.TryGetCastInformationFromClipboard(out castInformation))
                {
                    Int32 rowIndex;
                    Int32 token;
                    CastInformation ci;
                    List<Object> existingCastList;
                    List<Object> newCastList;

                    rowIndex = this.GetCurrentRowIndex(this.CastDataGridView);
                    token = this.UndoStack.BeginDo(ActionTexts.PasteFromClipboard);
                    ci = this.GetCastInformation();
                    existingCastList = new List<Object>(ci.CastList);
                    if (castInformation.CastList != null)
                    {
                        newCastList = new List<Object>(castInformation.CastList);
                    }
                    else
                    {
                        newCastList = new List<Object>(0);
                    }
                    for (Int32 i = newCastList.Count - 1; i >= 0; i--)
                    {
                        existingCastList.Insert(rowIndex, newCastList[i]);
                    }
                    ci.CastList = existingCastList.ToArray();
                    this.CastDataGridView.Rows.Clear();
                    this.AddCast(ci);
                    this.UndoStack.CommitDo(token);
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.CastDataCantBeRead, MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                CrewInformation crewInformation;

                if (Utilities.TryGetCrewInformationFromClipboard(out crewInformation))
                {
                    Int32 rowIndex;
                    Int32 token;
                    CrewInformation ci;
                    List<Object> existingCrewList;
                    List<Object> newCrewList;

                    rowIndex = this.GetCurrentRowIndex(this.CrewDataGridView);
                    token = this.UndoStack.BeginDo(ActionTexts.PasteFromClipboard);
                    ci = this.GetCrewInformation();
                    existingCrewList = new List<Object>(ci.CrewList);
                    if (crewInformation.CrewList != null)
                    {
                        newCrewList = new List<Object>(crewInformation.CrewList);
                    }
                    else
                    {
                        newCrewList = new List<Object>(0);
                    }
                    for (Int32 i = newCrewList.Count - 1; i >= 0; i--)
                    {
                        existingCrewList.Insert(rowIndex, newCrewList[i]);
                    }
                    ci.CrewList = existingCrewList.ToArray();
                    this.CrewDataGridView.Rows.Clear();
                    this.AddCrew(ci);
                    this.UndoStack.CommitDo(token);
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.CrewDataCantBeRead, MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void OnCutSelectedRowsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            if (this.CastTabIsActive)
            {
                this.CutSelectedRows(this.CastDataGridView);
            }
            else
            {
                this.CutSelectedRows(this.CrewDataGridView);
            }
        }

        private void CutSelectedRows(DataGridView dataGridView)
        {
            this.Validate();
            if (dataGridView.SelectedRows.Count > 0)
            {
                Int32 token;
                List<Int32> indexes;

                token = this.UndoStack.BeginDo(ActionTexts.CutRows);
                if (this.CopySelectedRows())
                {
                    indexes = new List<Int32>(dataGridView.SelectedRows.Count);
                    for (Int32 i = 0; i < dataGridView.SelectedRows.Count; i++)
                    {
                        indexes.Add(dataGridView.SelectedRows[i].Index);
                    }
                    indexes.Sort();
                    for (Int32 i = indexes.Count - 1; i >= 0; i--)
                    {
                        dataGridView.Rows.RemoveAt(indexes[i]);
                    }
                    this.UndoStack.CommitDo(token);
                }
                else
                {
                    this.UndoStack.AbortDo(token);
                }
            }
            else
            {
                MessageBox.Show(MessageBoxTexts.NoRowsSelected, MessageBoxTexts.WarningHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OnShowVerificationCheckboxesToolStripMenuItemClick(Object sender, EventArgs e)
        {
            DataGridViewColumn column;
            Boolean visible;

            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.OpeningCredits];
            visible = (column.Visible == false);
            column.Visible = visible;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.ClosingCredits];
            column.Visible = visible;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.OpeningCredits];
            column.Visible = visible;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.ClosingCredits];
            column.Visible = visible;
        }

        private void OnAllowSortingByClickingOnHeaderRowsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            DataGridViewColumn column;
            DataGridViewColumnSortMode sortMode;

            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.FirstName];
            sortMode = column.SortMode;
            if (sortMode == DataGridViewColumnSortMode.NotSortable)
            {
                sortMode = DataGridViewColumnSortMode.Automatic;
            }
            else
            {
                sortMode = DataGridViewColumnSortMode.NotSortable;
            }
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.FirstName];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.MiddleName];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.LastName];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.BirthYear];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.Role];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.Voice];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.Uncredited];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.CreditedAs];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.OpeningCredits];
            column.SortMode = sortMode;
            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.ClosingCredits];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.FirstName];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.MiddleName];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.LastName];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.BirthYear];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.CreditType];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.CreditSubtype];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.CustomRole];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.CreditedAs];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.OpeningCredits];
            column.SortMode = sortMode;
            column = this.CrewDataGridView.Columns[DataGridViewHelper.ColumnNames.ClosingCredits];
            column.SortMode = sortMode;
        }

        private void OnDataGridViewSorted(Object sender, EventArgs e)
        {
            Int32 token;

            token = this.UndoStack.BeginDo(ActionTexts.Sorted);
            this.UndoStack.CommitDo(token);
        }

        private void OnPutCastRoleAsFirstColumnToolStripMenuItemClick(Object sender, EventArgs e)
        {
            DataGridViewColumn column;
            Int32 displayIndex;

            column = this.CastDataGridView.Columns[DataGridViewHelper.ColumnNames.Role];
            displayIndex = column.DisplayIndex;
            if (displayIndex == 0)
            {
                column.DisplayIndex = 4;
            }
            else
            {
                column.DisplayIndex = 0;
            }
        }

        private void OnFindToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            using (SearchForm searchForm = new SearchForm(this.FindString))
            {
                if (searchForm.ShowDialog() == DialogResult.OK)
                {
                    this.FindString = searchForm.FindString;
                    if (String.IsNullOrEmpty(this.FindString) == false)
                    {
                        this.OnFindNextToolStripMenuItemClick(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void OnFindNextToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            if (String.IsNullOrEmpty(this.FindString) == false)
            {
                if (this.CastTabIsActive)
                {
                    FindNext(this.CastDataGridView, this.FindString);
                }
                else
                {
                    FindNext(this.CrewDataGridView, this.FindString);
                }
            }
            else
            {
                this.OnFindToolStripMenuItemClick(this, EventArgs.Empty);
            }
        }

        private static void FindNext(CustomDataGridView dataGridView, String findString)
        {
            if (dataGridView.RowCount > 0)
            {
                DataGridViewCell cell;
                Boolean multiSelection;

                if (dataGridView.SelectedRows.Count > 0)
                {
                    cell = FindFirstCell(dataGridView.SelectedRows, out multiSelection);
                }
                else if (dataGridView.SelectedCells.Count > 0)
                {
                    cell = FindFirstCell(dataGridView.SelectedCells, out multiSelection);
                }
                else
                {
                    cell = dataGridView.Rows[0].Cells[0];
                    multiSelection = false;
                }
                FindNextFrom(cell, findString, multiSelection);
            }
        }

        private static DataGridViewCell FindFirstCell(DataGridViewSelectedRowCollection rows, out Boolean multiSelection)
        {
            DataGridViewCell cell;

            multiSelection = rows.Count > 1;
            cell = rows[0].Cells[0];
            foreach (DataGridViewRow nextRow in rows)
            {
                if (nextRow.Index < cell.RowIndex)
                {
                    cell = nextRow.Cells[0];
                }
            }
            return (cell);
        }

        private static DataGridViewCell FindFirstCell(DataGridViewSelectedCellCollection cells, out Boolean multiSelection)
        {
            DataGridViewCell cell;

            cell = cells[0];
            multiSelection = cells.Count > 1;
            foreach (DataGridViewCell nextCell in cells)
            {
                if (nextCell.RowIndex < cell.RowIndex)
                {
                    cell = nextCell;
                }
                else if ((nextCell.RowIndex == cell.RowIndex) && (nextCell.ColumnIndex < cell.ColumnIndex))
                {
                    cell = nextCell;
                }
            }
            return (cell);
        }

        private static void FindNextFrom(DataGridViewCell cell, String findString, Boolean multiSelection)
        {
            Int32 columnStartIndex;
            DataGridViewColumn[] orderedColumns;
            Int32 lastRowIndex;
            Int32 lastVisibleColumnIndex;

            lastRowIndex = cell.RowIndex;
            lastVisibleColumnIndex = cell.OwningColumn.DisplayIndex;
            if (multiSelection)
            {
                columnStartIndex = cell.OwningColumn.DisplayIndex;
            }
            else
            {
                columnStartIndex = cell.OwningColumn.DisplayIndex + 1;
            }
            orderedColumns = new DataGridViewColumn[cell.DataGridView.ColumnCount];
            for (Int32 columnIndex = 0; columnIndex < cell.DataGridView.ColumnCount; columnIndex++)
            {
                DataGridViewColumn column;

                column = cell.DataGridView.Columns[columnIndex];
                orderedColumns[column.DisplayIndex] = column;
            }
            for (Int32 rowIndex = cell.RowIndex; rowIndex < cell.DataGridView.RowCount; rowIndex++)
            {
                for (Int32 visibleColumnIndex = columnStartIndex; visibleColumnIndex < cell.DataGridView.ColumnCount; visibleColumnIndex++)
                {
                    if (FindInCell(cell, findString, orderedColumns, rowIndex, visibleColumnIndex))
                    {
                        return;
                    }
                }
                columnStartIndex = 0;
            }
            //if we land here, we've reached the end of the grid
            for (Int32 rowIndex = 0; rowIndex <= lastRowIndex; rowIndex++)
            {
                Int32 maxColumnIndex;

                if (rowIndex == lastRowIndex)
                {
                    maxColumnIndex = lastVisibleColumnIndex + 1;
                }
                else
                {
                    maxColumnIndex = cell.DataGridView.ColumnCount;
                }
                for (Int32 visibleColumnIndex = 0; visibleColumnIndex < maxColumnIndex; visibleColumnIndex++)
                {
                    if (FindInCell(cell, findString, orderedColumns, rowIndex, visibleColumnIndex))
                    {
                        return;
                    }
                }
            }
            //if we land here, we've found nothing
            MessageBox.Show(String.Format(MessageBoxTexts.NoMatchFound, findString)
                , MessageBoxTexts.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static Boolean FindInCell(DataGridViewCell cell, String findString, DataGridViewColumn[] orderedColumns, Int32 rowIndex, Int32 visibleColumnIndex)
        {
            if (orderedColumns[visibleColumnIndex].Visible)
            {
                DataGridViewTextBoxCell textBoxCell;
                Int32 actualColumnIndex;

                actualColumnIndex = orderedColumns[visibleColumnIndex].Index;
                textBoxCell = cell.DataGridView.Rows[rowIndex].Cells[actualColumnIndex] as DataGridViewTextBoxCell;
                if (textBoxCell != null)
                {
                    String value;

                    value = (String)(textBoxCell.Value);
                    if (String.IsNullOrEmpty(value) == false)
                    {
                        if (value.IndexOf(findString, StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            textBoxCell.DataGridView.ClearSelection();
                            textBoxCell.Selected = true;
                            return (true);
                        }
                    }
                }
            }
            return (false);
        }

        private void OnSortCrewToolStripMenuItemClick(Object sender, EventArgs e)
        {
            this.Validate();
            if (this.CastTabIsActive == false)
            {
                CrewInformation crewInformation;

                crewInformation = DataGridViewHelper.GetCrewInformation(this.CrewDataGridView);
                if (DataGridViewHelper.CheckCrewValidity(crewInformation))
                {
                    Int32 token;

                    crewInformation = CrewSorter.GetSortedCrew(crewInformation);
                    token = this.UndoStack.BeginDo(ActionTexts.SortCrew);
                    this.CrewDataGridView.Rows.Clear();
                    this.AddCrew(crewInformation);
                    this.UndoStack.CommitDo(token);
                }
            }
        }

        private void OnEditToolStripMenuItemDropDownOpened(Object sender, EventArgs e)
        {
            this.SortCrewToolStripMenuItem.Enabled = (this.CastTabIsActive == false);
        }

        private void OnCopyNamesToCreditedAsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            if (this.CastTabIsActive)
            {
                if (this.CastDataGridView.SelectedRows.Count > 0)
                {
                    Int32 token;

                    token = this.UndoStack.BeginDo(ActionTexts.CopyNames);
                    this.BuildCreditedAsNames(this.CastDataGridView.SelectedRows);
                    this.UndoStack.CommitDo(token);
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.NoRowsSelected, MessageBoxTexts.WarningHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (this.CrewDataGridView.SelectedRows.Count > 0)
                {
                    Int32 token;

                    token = this.UndoStack.BeginDo(ActionTexts.CopyNames);
                    this.BuildCreditedAsNames(this.CrewDataGridView.SelectedRows);
                    this.UndoStack.CommitDo(token);
                }
                else
                {
                    MessageBox.Show(MessageBoxTexts.NoRowsSelected, MessageBoxTexts.WarningHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void BuildCreditedAsNames(DataGridViewSelectedRowCollection selectedRows)
        {
            foreach (DataGridViewRow row in selectedRows)
            {
                this.BuildCreditedAsName(row);
            }
        }

        private void BuildCreditedAsName(DataGridViewRow row)
        {
            String first;
            String middle;
            String last;
            String creditedAs;

            creditedAs = String.Empty;
            first = row.Cells[0].Value.ToString().Trim();
            middle = row.Cells[1].Value.ToString().Trim();
            last = row.Cells[2].Value.ToString().Trim();
            if (String.IsNullOrEmpty(first) == false)
            {
                creditedAs = first;
            }
            if (String.IsNullOrEmpty(middle) == false)
            {
                if (String.IsNullOrEmpty(creditedAs))
                {
                    creditedAs = middle;
                }
                else
                {
                    creditedAs += " " + middle;
                }
            }
            if (String.IsNullOrEmpty(last) == false)
            {
                if (String.IsNullOrEmpty(creditedAs))
                {
                    creditedAs = last;
                }
                else
                {
                    creditedAs += " " + last;
                }
            }
            row.Cells[7].Value = creditedAs;
        }
    }
}