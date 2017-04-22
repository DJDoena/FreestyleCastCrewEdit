using DoenaSoft.DVDProfiler.DVDProfilerXML.Version390;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    internal static class DataGridViewHelper
    {
        #region Nested Classes
        internal static class ColumnNames
        {
            public const String FirstName = "First Name";
            public const String MiddleName = "Middle Name";
            public const String LastName = "Last Name";
            public const String BirthYear = "Birth Year";
            public const String Role = "Role";
            public const String Voice = "Voice";
            public const String Uncredited = "Uncredited";
            public const String CreditedAs = "Credited As";
            public const String CreditType = "Credit Type";
            public const String CreditSubtype = "Credit Subtype";
            public const String CustomRole = "Custom Role";
            public const String OpeningCredits = "Opening Credits";
            public const String ClosingCredits = "Closing Credits";
        }

        internal static class FirstNames
        {
            public const String Divider = "<divider>";
        }

        private class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
        {
            public DataGridViewDisableButtonColumn()
            {
                this.CellTemplate = new DataGridViewDisableButtonCell();
            }
        }

        internal class DataGridViewDisableButtonCell : DataGridViewButtonCell
        {
            private Boolean EnabledValue;

            public Boolean Enabled
            {
                get
                {
                    return (this.EnabledValue);
                }
                set
                {
                    this.EnabledValue = value;
                }
            }

            // By default, enable the button cell.
            public DataGridViewDisableButtonCell()
            {
                this.EnabledValue = true;
            }

            // Override the Clone method so that the Enabled property is copied.
            public override Object Clone()
            {
                DataGridViewDisableButtonCell cell;

                cell = (DataGridViewDisableButtonCell)(base.Clone());
                cell.Enabled = this.Enabled;
                return (cell);
            }

            protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, Int32 rowIndex
                , DataGridViewElementStates elementState, Object value, Object formattedValue, String errorText
                , DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle
                , DataGridViewPaintParts paintParts)
            {
                // The button cell is disabled, so paint the border,  
                // background, and disabled button for the cell.
                if (this.EnabledValue == false)
                {
                    Rectangle buttonArea;
                    Rectangle buttonAdjustment;

                    // Draw the cell background, if specified.
                    if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                    {
                        SolidBrush cellBackground;

                        cellBackground = new SolidBrush(cellStyle.BackColor);
                        graphics.FillRectangle(cellBackground, cellBounds);
                        cellBackground.Dispose();
                    }

                    // Draw the cell borders, if specified.
                    if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                    {
                        this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                    }

                    // Calculate the area in which to draw the button.
                    buttonArea = cellBounds;
                    buttonAdjustment = this.BorderWidths(advancedBorderStyle);
                    buttonArea.X += buttonAdjustment.X;
                    buttonArea.Y += buttonAdjustment.Y;
                    buttonArea.Height -= buttonAdjustment.Height;
                    buttonArea.Width -= buttonAdjustment.Width;

                    // Draw the disabled button.                
                    ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);

                    // Draw the disabled button text. 
                    if (this.FormattedValue is String)
                    {
                        TextRenderer.DrawText(graphics, (String)(this.FormattedValue), this.DataGridView.Font
                            , buttonArea, SystemColors.GrayText);
                    }
                }
                else
                {
                    // The button cell is enabled, so let the base class 
                    // handle the painting.
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue
                        , errorText, cellStyle, advancedBorderStyle, paintParts);
                }
            }
        }
        #endregion

        public static void CreateCastColumns(DataGridView dataGridView)
        {
            DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn middlenameDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn birthyearDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn roleDataGridViewTextBoxColumn;
            DataGridViewCheckBoxColumn voiceDataGridViewTextBoxColumn;
            DataGridViewCheckBoxColumn uncreditedDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn creditedasDataGridViewTextBoxColumn;
            DataGridViewCheckBoxColumn openingCreditsGridViewTextBoxColumn;
            DataGridViewCheckBoxColumn closingCreditsGridViewTextBoxColumn;

            dataGridView.SuspendLayout();

            firstnameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            firstnameDataGridViewTextBoxColumn.Name = ColumnNames.FirstName;
            firstnameDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.FirstName;
            firstnameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            firstnameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            firstnameDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(firstnameDataGridViewTextBoxColumn);

            middlenameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            middlenameDataGridViewTextBoxColumn.Name = ColumnNames.MiddleName;
            middlenameDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.MiddleName;
            middlenameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            middlenameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            middlenameDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(middlenameDataGridViewTextBoxColumn);

            lastnameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            lastnameDataGridViewTextBoxColumn.Name = ColumnNames.LastName;
            lastnameDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.LastName;
            lastnameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            lastnameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            lastnameDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(lastnameDataGridViewTextBoxColumn);

            birthyearDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            birthyearDataGridViewTextBoxColumn.Name = ColumnNames.BirthYear;
            birthyearDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.BirthYear;
            birthyearDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            birthyearDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            birthyearDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(birthyearDataGridViewTextBoxColumn);

            roleDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            roleDataGridViewTextBoxColumn.Name = ColumnNames.Role;
            roleDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.Role;
            roleDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            roleDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            roleDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(roleDataGridViewTextBoxColumn);

            voiceDataGridViewTextBoxColumn = new DataGridViewCheckBoxColumn();
            voiceDataGridViewTextBoxColumn.Name = ColumnNames.Voice;
            voiceDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.Voice;
            voiceDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            voiceDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            voiceDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(voiceDataGridViewTextBoxColumn);

            uncreditedDataGridViewTextBoxColumn = new DataGridViewCheckBoxColumn();
            uncreditedDataGridViewTextBoxColumn.Name = ColumnNames.Uncredited;
            uncreditedDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.Uncredited;
            uncreditedDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            uncreditedDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            uncreditedDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(uncreditedDataGridViewTextBoxColumn);

            creditedasDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            creditedasDataGridViewTextBoxColumn.Name = ColumnNames.CreditedAs;
            creditedasDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.CreditedAs;
            creditedasDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            creditedasDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            creditedasDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(creditedasDataGridViewTextBoxColumn);

            openingCreditsGridViewTextBoxColumn = new DataGridViewCheckBoxColumn();
            openingCreditsGridViewTextBoxColumn.Name = ColumnNames.OpeningCredits;
            openingCreditsGridViewTextBoxColumn.HeaderText = DataGridViewTexts.OpeningCredits;
            openingCreditsGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            openingCreditsGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            openingCreditsGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            openingCreditsGridViewTextBoxColumn.Visible = false;
            dataGridView.Columns.Add(openingCreditsGridViewTextBoxColumn);

            closingCreditsGridViewTextBoxColumn = new DataGridViewCheckBoxColumn();
            closingCreditsGridViewTextBoxColumn.Name = ColumnNames.ClosingCredits;
            closingCreditsGridViewTextBoxColumn.HeaderText = DataGridViewTexts.ClosingCredits;
            closingCreditsGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            closingCreditsGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            closingCreditsGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            closingCreditsGridViewTextBoxColumn.Visible = false;
            dataGridView.Columns.Add(closingCreditsGridViewTextBoxColumn);

            dataGridView.ResumeLayout();
        }

        public static void CreateCrewColumns(DataGridView dataGridView)
        {
            DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn middlenameDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn birthyearDataGridViewTextBoxColumn;
            DataGridViewComboBoxColumn creditTypeDataGridViewComboBoxBoxColumn;
            DataGridViewComboBoxColumn creditSubtypeDataGridViewComboBoxColumn;
            DataGridViewTextBoxColumn customRoleDataGridViewTextBoxColumn;
            DataGridViewTextBoxColumn creditedasDataGridViewTextBoxColumn;
            DataGridViewCheckBoxColumn openingCreditsGridViewTextBoxColumn;
            DataGridViewCheckBoxColumn closingCreditsGridViewTextBoxColumn;

            dataGridView.SuspendLayout();

            firstnameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            firstnameDataGridViewTextBoxColumn.Name = ColumnNames.FirstName;
            firstnameDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.FirstName;
            firstnameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            firstnameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            firstnameDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(firstnameDataGridViewTextBoxColumn);

            middlenameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            middlenameDataGridViewTextBoxColumn.Name = ColumnNames.MiddleName;
            middlenameDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.MiddleName;
            middlenameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            middlenameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            middlenameDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(middlenameDataGridViewTextBoxColumn);

            lastnameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            lastnameDataGridViewTextBoxColumn.Name = ColumnNames.LastName;
            lastnameDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.LastName;
            lastnameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            lastnameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            lastnameDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(lastnameDataGridViewTextBoxColumn);

            birthyearDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            birthyearDataGridViewTextBoxColumn.Name = ColumnNames.BirthYear;
            birthyearDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.BirthYear;
            birthyearDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            birthyearDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            birthyearDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(birthyearDataGridViewTextBoxColumn);

            creditTypeDataGridViewComboBoxBoxColumn = new DataGridViewComboBoxColumn();
            creditTypeDataGridViewComboBoxBoxColumn.Name = ColumnNames.CreditType;
            creditTypeDataGridViewComboBoxBoxColumn.HeaderText = DataGridViewTexts.CreditType;
            creditTypeDataGridViewComboBoxBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            creditTypeDataGridViewComboBoxBoxColumn.Resizable = DataGridViewTriState.True;
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Direction);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Writing);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Production);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Cinematography);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.FilmEditing);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Music);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Sound);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Art);
            creditTypeDataGridViewComboBoxBoxColumn.Items.Add(CreditTypesDataGridViewHelper.CreditTypes.Other);
            creditTypeDataGridViewComboBoxBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(creditTypeDataGridViewComboBoxBoxColumn);

            creditSubtypeDataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
            creditSubtypeDataGridViewComboBoxColumn.Name = ColumnNames.CreditSubtype;
            creditSubtypeDataGridViewComboBoxColumn.HeaderText = DataGridViewTexts.CreditSubtype;
            creditSubtypeDataGridViewComboBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            creditSubtypeDataGridViewComboBoxColumn.Resizable = DataGridViewTriState.True;
            creditSubtypeDataGridViewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(creditSubtypeDataGridViewComboBoxColumn);

            customRoleDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            customRoleDataGridViewTextBoxColumn.Name = ColumnNames.CustomRole;
            customRoleDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.CustomRole;
            customRoleDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            customRoleDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            customRoleDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(customRoleDataGridViewTextBoxColumn);

            creditedasDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            creditedasDataGridViewTextBoxColumn.Name = ColumnNames.CreditedAs;
            creditedasDataGridViewTextBoxColumn.HeaderText = DataGridViewTexts.CreditedAs;
            creditedasDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            creditedasDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            creditedasDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(creditedasDataGridViewTextBoxColumn);

            openingCreditsGridViewTextBoxColumn = new DataGridViewCheckBoxColumn();
            openingCreditsGridViewTextBoxColumn.Name = ColumnNames.OpeningCredits;
            openingCreditsGridViewTextBoxColumn.HeaderText = DataGridViewTexts.OpeningCredits;
            openingCreditsGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            openingCreditsGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            openingCreditsGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            openingCreditsGridViewTextBoxColumn.Visible = false;
            dataGridView.Columns.Add(openingCreditsGridViewTextBoxColumn);

            closingCreditsGridViewTextBoxColumn = new DataGridViewCheckBoxColumn();
            closingCreditsGridViewTextBoxColumn.Name = ColumnNames.ClosingCredits;
            closingCreditsGridViewTextBoxColumn.HeaderText = DataGridViewTexts.ClosingCredits;
            closingCreditsGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            closingCreditsGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            closingCreditsGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            closingCreditsGridViewTextBoxColumn.Visible = false;
            dataGridView.Columns.Add(closingCreditsGridViewTextBoxColumn);

            dataGridView.ResumeLayout();
        }

        public static void CopyCastToClipboard(DataGridView dataGridView)
        {
            CastInformation ci;

            ci = GetCastInformation(dataGridView);
            if (CheckCastValidity(ci))
            {
                try
                {
                    Utilities.CopyCastInformationToClipboard(ci);
                }
                catch (Exception)
                {
                    MessageBox.Show(MessageBoxTexts.ClipboardOperationFailed, MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal static CastInformation GetCastInformation(DataGridView dataGridView)
        {
            CastInformation ci;

            ci = new CastInformation();
            ci.Title = "Freestyle Cast/Crew Edit";
            ci.CastList = new Object[dataGridView.Rows.Count - 1];
            for (Int32 i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                ci.CastList[i] = CreateCastMember(dataGridView.Rows[i]);
            }
            return (ci);
        }

        internal static Boolean CheckCrewValidity(CrewInformation crewInformation)
        {
            if ((crewInformation.CrewList != null) && (crewInformation.CrewList.Length > 0))
            {
                Boolean groupDividerPresent;
                String groupDividerCreditType;
                String crewMemberCreditType;
                Int32 mismatchRowNumber;
                String groupDividerName;

                mismatchRowNumber = -1;
                groupDividerPresent = false;
                groupDividerCreditType = String.Empty;
                crewMemberCreditType = String.Empty;
                groupDividerName = String.Empty;
                for (Int32 i = 0; i < crewInformation.CrewList.Length; i++)
                {
                    CrewDivider divider;

                    divider = crewInformation.CrewList[i] as CrewDivider;
                    if (divider != null)
                    {
                        if (divider.Type == DividerType.EndDiv)
                        {
                            if (groupDividerPresent == false)
                            {
                                //Group End Divider without Group Start divider
                                MessageBox.Show(String.Format(MessageBoxTexts.GroupEndDivider
                                    , "Crew", i + 1), MessageBoxTexts.ErrorHeader
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return (false);
                            }
                            if (String.IsNullOrEmpty(crewMemberCreditType) == false)
                            {
                                if (groupDividerCreditType != crewMemberCreditType)
                                {
                                    //Group section contains a differen CreditType than the Group Start divider
                                    MessageBox.Show(String.Format(MessageBoxTexts.DifferentCreditTypesCrewMember
                                        , groupDividerName, crewMemberCreditType, groupDividerCreditType, mismatchRowNumber + 1)
                                        , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return (false);
                                }
                            }
                            if (divider.CreditType != groupDividerCreditType)
                            {
                                //Group section contains a different CreditType than the Group Start divider
                                MessageBox.Show(String.Format(MessageBoxTexts.DifferentCreditTypesEndDivider
                                    , groupDividerName, divider.CreditType, groupDividerCreditType, i + 1)
                                    , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return (false);
                            }
                            groupDividerPresent = false;
                            groupDividerCreditType = String.Empty;
                            crewMemberCreditType = String.Empty;
                            groupDividerName = String.Empty;
                        }
                        else if (divider.Type == DividerType.Group)
                        {
                            groupDividerPresent = true;
                            groupDividerCreditType = divider.CreditType;
                            crewMemberCreditType = String.Empty;
                            groupDividerName = divider.Caption;
                        }
                        else if (divider.Type == DividerType.Episode)
                        {
                            groupDividerPresent = false;
                            groupDividerCreditType = String.Empty;
                            crewMemberCreditType = String.Empty;
                            groupDividerName = String.Empty;
                        }
                    }
                    else
                    {
                        CrewMember crewMember;

                        crewMember = (CrewMember)(crewInformation.CrewList[i]);
                        if (String.IsNullOrEmpty(groupDividerCreditType) == false)
                        {
                            if (groupDividerCreditType != crewMember.CreditType)
                            {
                                crewMemberCreditType = crewMember.CreditType;
                                mismatchRowNumber = i;
                            }
                        }
                        if (String.IsNullOrEmpty(crewMember.FirstName))
                        {
                            MessageBox.Show(String.Format(MessageBoxTexts.ColumnMustNotBeEmpty
                                , MessageBoxTexts.FirstNameColumn, i + 1), MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                            return (false);
                        }
                        else if (String.IsNullOrEmpty(crewMember.CreditType))
                        {
                            MessageBox.Show(String.Format(MessageBoxTexts.ColumnMustNotBeEmpty
                            , DataGridViewTexts.CreditType, i + 1), MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK
                            , MessageBoxIcon.Error);
                            return (false);
                        }
                        else if (String.IsNullOrEmpty(crewMember.CreditSubtype))
                        {
                            MessageBox.Show(String.Format(MessageBoxTexts.ColumnMustNotBeEmpty
                            , DataGridViewTexts.CreditSubtype, i + 1), MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK
                            , MessageBoxIcon.Error);
                            return (false);
                        }
                    }
                }
            }
            return (true);
        }

        internal static Boolean CheckCastValidity(CastInformation castInformation)
        {
            if ((castInformation.CastList != null) && (castInformation.CastList.Length > 0))
            {
                Boolean groupDividerPresent;

                groupDividerPresent = false;
                for (Int32 i = 0; i < castInformation.CastList.Length; i++)
                {
                    Divider divider;

                    divider = castInformation.CastList[i] as Divider;
                    if (divider != null)
                    {
                        if (divider.Type == DividerType.EndDiv)
                        {
                            if (groupDividerPresent == false)
                            {
                                //Group End Divider without Group Start divider
                                MessageBox.Show(String.Format(MessageBoxTexts.GroupEndDivider
                                    , "Cast", i + 1), MessageBoxTexts.ErrorHeader
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return (false);
                            }
                            groupDividerPresent = false;
                        }
                        else if (divider.Type == DividerType.Group)
                        {
                            groupDividerPresent = true;
                        }
                        else if (divider.Type == DividerType.Episode)
                        {
                            groupDividerPresent = false;
                        }
                    }
                    else
                    {
                        CastMember castMember;

                        castMember = (CastMember)(castInformation.CastList[i]);
                        if (String.IsNullOrEmpty(castMember.FirstName))
                        {
                            MessageBox.Show(String.Format(MessageBoxTexts.ColumnMustNotBeEmpty
                                , MessageBoxTexts.FirstNameColumn, i + 1), MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                            return (false);
                        }
                    }
                }
            }
            return (true);
        }

        public static Boolean CopyCastToClipboard(DataGridViewSelectedRowCollection dataGridViewRows)
        {
            CastInformation ci;
            List<DataGridViewRow> list;

            list = new List<DataGridViewRow>(dataGridViewRows.Count);
            for (Int32 i = 0; i < dataGridViewRows.Count; i++)
            {
                list.Add(dataGridViewRows[i]);
            }
            list.Sort(new Comparison<DataGridViewRow>(delegate(DataGridViewRow left, DataGridViewRow right)
                { return (left.Index.CompareTo(right.Index)); }
                ));
            ci = new CastInformation();
            ci.Title = "Freestyle Cast/Crew Edit";
            ci.CastList = new Object[list.Count];
            for (Int32 i = 0; i < list.Count; i++)
            {
                ci.CastList[i] = CreateCastMember(list[i]);
            }
            if (CheckCastValidity(ci))
            {
                try
                {
                    Utilities.CopyCastInformationToClipboard(ci);
                }
                catch (Exception)
                {
                    MessageBox.Show(MessageBoxTexts.ClipboardOperationFailed, MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (false);
                }
            }
            return (true);
        }

        private static Object CreateCastMember(DataGridViewRow row)
        {
            Object value;

            value = row.Cells[ColumnNames.FirstName].Value;
            if ((value != null) && (value.ToString() == FirstNames.Divider))
            {
                Divider divider;
                String name;

                divider = new Divider();

                name = String.Empty;
                value = row.Cells[ColumnNames.MiddleName].Value;
                if (value != null)
                {
                    name += value.ToString().Trim();
                }
                divider.Caption = name.Trim();
                divider.Type = (DividerType)(Enum.Parse(typeof(DividerType)
                    , row.Cells[ColumnNames.CreditedAs].Value.ToString()));
                return (divider);
            }
            else
            {
                CastMember castMember;

                castMember = new CastMember();
                if (value != null)
                {
                    castMember.FirstName = value.ToString();
                }
                value = row.Cells[ColumnNames.MiddleName].Value;
                if (value != null)
                {
                    castMember.MiddleName = value.ToString();
                }
                value = row.Cells[ColumnNames.LastName].Value;
                if (value != null)
                {
                    castMember.LastName = value.ToString();
                }
                value = row.Cells[ColumnNames.BirthYear].Value;
                if (value != null)
                {
                    Int32 intValue;

                    if (Int32.TryParse(value.ToString(), out intValue))
                    {
                        castMember.BirthYear = intValue;
                    }
                }
                value = row.Cells[ColumnNames.Role].Value;
                if (value != null)
                {
                    castMember.Role = value.ToString();
                }
                value = row.Cells[ColumnNames.Voice].Value;
                if (value != null)
                {
                    castMember.Voice = Boolean.Parse(value.ToString());
                }
                value = row.Cells[ColumnNames.Uncredited].Value;
                if (value != null)
                {
                    castMember.Uncredited = Boolean.Parse(value.ToString());
                }
                value = row.Cells[ColumnNames.CreditedAs].Value;
                if (value != null)
                {
                    castMember.CreditedAs = value.ToString();
                }
                return (castMember);
            }
        }

        public static void CopyCrewToClipboard(DataGridView dataGridView)
        {
            CrewInformation ci;

            ci = GetCrewInformation(dataGridView);
            if (CheckCrewValidity(ci))
            {
                try
                {
                    Utilities.CopyCrewInformationToClipboard(ci);
                }
                catch (Exception)
                {
                    MessageBox.Show(MessageBoxTexts.ClipboardOperationFailed, MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal static CrewInformation GetCrewInformation(DataGridView dataGridView)
        {
            CrewInformation ci;

            ci = new CrewInformation();
            ci.Title = "Freestyle Cast/Crew Edit";
            ci.CrewList = new Object[dataGridView.Rows.Count - 1];
            for (Int32 i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                ci.CrewList[i] = CreateCrewMember(dataGridView.Rows[i]);
            }
            return ci;
        }

        public static Boolean CopyCrewToClipboard(DataGridViewSelectedRowCollection dataGridViewRows)
        {
            CrewInformation ci;
            List<DataGridViewRow> list;

            list = new List<DataGridViewRow>(dataGridViewRows.Count);
            for (Int32 i = 0; i < dataGridViewRows.Count; i++)
            {
                list.Add(dataGridViewRows[i]);
            }
            list.Sort(new Comparison<DataGridViewRow>(delegate(DataGridViewRow left, DataGridViewRow right)
            { return (left.Index.CompareTo(right.Index)); }
                ));
            ci = new CrewInformation();
            ci.Title = "Freestyle Cast/Crew Edit";
            ci.CrewList = new Object[list.Count];
            for (Int32 i = 0; i < list.Count; i++)
            {
                ci.CrewList[i] = CreateCrewMember(list[i]);
            }
            if (CheckCrewValidity(ci))
            {
                try
                {
                    Utilities.CopyCrewInformationToClipboard(ci);
                }
                catch (Exception)
                {
                    MessageBox.Show(MessageBoxTexts.ClipboardOperationFailed, MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (false);
                }
            }
            return (true);
        }

        private static Object CreateCrewMember(DataGridViewRow row)
        {
            Object value;

            value = row.Cells[ColumnNames.FirstName].Value;
            if ((value != null) && (value.ToString() == FirstNames.Divider))
            {
                CrewDivider divider;
                String name;

                divider = new CrewDivider();
                name = String.Empty;
                value = row.Cells[ColumnNames.MiddleName].Value;
                if (value != null)
                {
                    name += value.ToString().Trim();
                }
                divider.Caption = name.Trim();
                divider.Type = (DividerType)(Enum.Parse(typeof(DividerType)
                    , row.Cells[ColumnNames.CreditedAs].Value.ToString()));
                if ((divider.Type == DividerType.Group) || (divider.Type == DividerType.EndDiv))
                {
                    divider.CreditType = row.Cells[ColumnNames.CreditType].Value.ToString();
                }
                return (divider);
            }
            else
            {
                CrewMember crewMember;

                crewMember = new CrewMember();
                if (value != null)
                {
                    crewMember.FirstName = value.ToString();
                }
                value = row.Cells[ColumnNames.MiddleName].Value;
                if (value != null)
                {
                    crewMember.MiddleName = value.ToString();
                }
                value = row.Cells[ColumnNames.LastName].Value;
                if (value != null)
                {
                    crewMember.LastName = value.ToString();
                }
                value = row.Cells[ColumnNames.BirthYear].Value;
                if (value != null)
                {
                    Int32 intValue;

                    if (Int32.TryParse(value.ToString(), out intValue))
                    {
                        crewMember.BirthYear = intValue;
                    }
                }
                value = row.Cells[ColumnNames.CreditType].Value;
                if (value != null)
                {
                    crewMember.CreditType = value.ToString();
                }
                value = row.Cells[ColumnNames.CreditSubtype].Value;
                if (value != null)
                {
                    crewMember.CreditSubtype = value.ToString();
                }
                value = row.Cells[ColumnNames.CustomRole].Value;
                if (value != null)
                {
                    crewMember.CustomRole = value.ToString();
                    if (String.IsNullOrEmpty(crewMember.CustomRole) == false)
                    {
                        crewMember.CustomRoleSpecified = true;
                    }
                }
                value = row.Cells[ColumnNames.CreditedAs].Value;
                if (value != null)
                {
                    crewMember.CreditedAs = value.ToString();
                }
                return (crewMember);
            }
        }

        public static void OnCrewDataGridViewCellValueChanged(Object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                DataGridViewRow row;
                DataGridViewComboBoxCell creditTypeCell;
                DataGridViewComboBoxCell creditSubtypeCell;

                row = ((DataGridView)sender).Rows[e.RowIndex];
                if (row.Cells[ColumnNames.FirstName].Value.ToString() != DataGridViewHelper.FirstNames.Divider)
                {
                    creditTypeCell = (DataGridViewComboBoxCell)(row.Cells[DataGridViewHelper.ColumnNames.CreditType]);
                    creditSubtypeCell = (DataGridViewComboBoxCell)(row.Cells[DataGridViewHelper.ColumnNames.CreditSubtype]);
                    creditSubtypeCell.Value = null;
                    CreditTypesDataGridViewHelper.FillCreditSubtypes(creditTypeCell.Value.ToString(), creditSubtypeCell.Items);
                    creditSubtypeCell.Value = CreditTypesDataGridViewHelper.CreditSubtypes.Custom;
                }
            }
        }
    }
}