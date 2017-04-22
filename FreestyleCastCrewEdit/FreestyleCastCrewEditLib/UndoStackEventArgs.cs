using System;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public class UndoStackEventArgs : EventArgs
        {
            private String m_Action;

            public String Action
            {
                get
                {
                    return (this.m_Action);
                }
            }

            internal UndoStackEventArgs(String action)
            {
                this.m_Action = action;
            }
        }
}
