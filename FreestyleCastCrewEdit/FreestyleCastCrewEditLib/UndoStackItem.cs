using DoenaSoft.DVDProfiler.DVDProfilerXML.Version390;
using System;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    internal class UndoStackItem
    {
        private CastInformation m_CastInformation;

        internal CastInformation CastInformation
        {
            get
            {
                return (this.m_CastInformation);
            }
        }

        private CrewInformation m_CrewInformation;

        internal CrewInformation CrewInformation
        {
            get
            {
                return (this.m_CrewInformation);
            }
        }

        private String m_Action;

        internal String Action
        {
            get
            {
                return (this.m_Action);
            }
        }

        internal UndoStackItem(CastInformation castInformation, CrewInformation crewInformation, String action)
        {
            this.m_CastInformation = castInformation;
            this.m_CrewInformation = crewInformation;
            this.m_Action = action;
        }
    }
}
