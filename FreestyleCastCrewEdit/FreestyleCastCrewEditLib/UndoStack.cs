using DoenaSoft.DVDProfiler.DVDProfilerXML.Version390;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public class UndoStack
    {
        private const Int32 StackSize = 25;

        internal event EventHandler<UndoStackEventArgs> UndoStateChanged;

        internal event EventHandler<UndoStackEventArgs> RedoStateChanged;

        private Boolean m_HasChangedPermanently;

        private List<UndoStackItem> m_UndoStackList;

        private List<UndoStackItem> m_RedoStackList;

        private DataGridView m_CastDataGridView;

        private DataGridView m_CrewDataGridView;

        private EditingPane m_EditingPane;

        private Boolean m_Busy;

        private Int32 m_Token;

        private Int32 m_CurrentToken;

        private String m_Action;

        public Boolean HasChanged
        {
            get
            {
                return ((this.m_HasChangedPermanently) || (this.m_UndoStackList.Count > 1));
            }
        }

        public Boolean CanUndo
        {
            get
            {
                return (this.m_UndoStackList.Count > 1);
            }
        }

        public Boolean CanRedo
        {
            get
            {
                return (this.m_RedoStackList.Count > 0);
            }
        }

        internal UndoStack(DataGridView castDataGridView, DataGridView crewDataGridView, EditingPane editingPane)
        {
            CastInformation castInformation;
            CrewInformation crewInformation;
            UndoStackItem usi;

            this.m_UndoStackList = new List<UndoStackItem>(StackSize + 2);
            this.m_RedoStackList = new List<UndoStackItem>(StackSize);
            castInformation = DataGridViewHelper.GetCastInformation(castDataGridView);
            crewInformation = DataGridViewHelper.GetCrewInformation(crewDataGridView);
            usi = new UndoStackItem(castInformation, crewInformation, String.Empty);
            this.m_UndoStackList.Add(usi);
            this.m_CastDataGridView = castDataGridView;
            this.m_CrewDataGridView = crewDataGridView;
            this.m_EditingPane = editingPane;
            this.m_Action = String.Empty;
            this.m_Busy = false;
            this.m_Token = 0;
            this.m_CurrentToken = 0;
        }

        internal UndoStack(DataGridView castDataGridView, DataGridView crewDataGridView, EditingPane editingPane
            , CastInformation castInformation, CrewInformation crewInformation)
        {
            UndoStackItem usi;

            this.m_UndoStackList = new List<UndoStackItem>(StackSize + 2);
            this.m_RedoStackList = new List<UndoStackItem>(StackSize);
            usi = new UndoStackItem(castInformation, crewInformation, String.Empty);
            this.m_UndoStackList.Add(usi);
            this.m_CastDataGridView = castDataGridView;
            this.m_CrewDataGridView = crewDataGridView;
            this.m_EditingPane = editingPane;
            this.m_Action = String.Empty;
            this.m_Busy = false;
            this.m_Token = 0;
            this.m_CurrentToken = 0;
        }

        internal Int32 BeginDo(String action)
        {
            Int32 token;

            this.m_Busy = true;
            token = ++this.m_Token;
            if (this.m_CurrentToken == 0)
            {
                this.m_CurrentToken = token;
                this.m_Action = action;
            }
            return (token);
        }

        internal void CommitDo(Int32 token)
        {
            if (this.m_CurrentToken == token)
            {
                this.m_Busy = false;
                this.m_CurrentToken = 0;
                this.Do();
                this.m_Action = String.Empty;
            }
        }

        internal void AbortDo(Int32 token)
        {
            if (this.m_CurrentToken == token)
            {
                this.m_Busy = false;
                this.m_CurrentToken = 0;
                this.m_Action = String.Empty;
            }
        }

        private void Do()
        {
            CastInformation castInformation;
            CrewInformation crewInformation;
            UndoStackItem usi;

            if (this.m_Busy)
            {
                return;
            }
            castInformation = DataGridViewHelper.GetCastInformation(this.m_CastDataGridView);
            crewInformation = DataGridViewHelper.GetCrewInformation(this.m_CrewDataGridView);
            usi = new UndoStackItem(castInformation, crewInformation, this.m_Action);
            this.m_UndoStackList.Add(usi);
            if (this.m_UndoStackList.Count > StackSize + 1)
            {
                this.m_UndoStackList.RemoveAt(0);
                this.m_HasChangedPermanently = true;
            }
            this.m_RedoStackList.Clear();
            this.FireEvents();
        }

        private void FireEvents()
        {
            if (this.UndoStateChanged != null)
            {
                String action;

                action = String.Empty;
                if (this.m_UndoStackList.Count != 1)
                {
                    action = this.m_UndoStackList[this.m_UndoStackList.Count - 1].Action;
                }
                this.UndoStateChanged(this, new UndoStackEventArgs(action));
            }
            if (this.RedoStateChanged != null)
            {
                String action;

                action = String.Empty;
                if (this.m_RedoStackList.Count > 0)
                {
                    action = this.m_RedoStackList[this.m_RedoStackList.Count - 1].Action;
                }
                this.RedoStateChanged(this, new UndoStackEventArgs(action));
            }
        }

        internal void Undo()
        {
            if (this.m_UndoStackList.Count > 1)
            {
                UndoStackItem usi;
                Int32 token;

                usi = this.m_UndoStackList[this.m_UndoStackList.Count - 1];
                token = this.BeginDo(usi.Action);
                this.m_RedoStackList.Add(usi);
                this.m_UndoStackList.RemoveAt(this.m_UndoStackList.Count - 1);
                this.FillData();
                this.AbortDo(token);
            }
            else
            {
                Debug.Assert(false, "Undo Stack empty!");
            }
        }

        private void FillData()
        {
            UndoStackItem usi;

            usi = this.m_UndoStackList[this.m_UndoStackList.Count - 1];
            this.m_CastDataGridView.Rows.Clear();
            this.m_EditingPane.AddCast(usi.CastInformation);
            this.m_CrewDataGridView.Rows.Clear();
            this.m_EditingPane.AddCrew(usi.CrewInformation);
            this.FireEvents();
        }

        internal void Redo()
        {
            if (this.m_RedoStackList.Count > 0)
            {
                UndoStackItem usi;
                Int32 token;

                usi = this.m_RedoStackList[this.m_RedoStackList.Count - 1];
                token = this.BeginDo(usi.Action);
                this.m_UndoStackList.Add(usi);
                this.m_RedoStackList.RemoveAt(this.m_RedoStackList.Count - 1);
                this.FillData();
                this.AbortDo(token);
            }
            else
            {
                Debug.Assert(false, "Redo Stack empty!");
            }
        }
    }
}
