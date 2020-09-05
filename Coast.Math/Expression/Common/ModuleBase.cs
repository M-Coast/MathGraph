using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math.Expression
{
    public class ModuleBase
    {
        private string _moduleName = null;
        private uint _moduleId = 0;
        //private StatusLog _log = null;
        private static uint _gidCounter = 0;


        //To Set or Reset error states
        //Only can be visited in this Class or in it's inherited Class
        protected bool _errored = false;

        //To get Error state
        //Get Only
        public bool Errored { get { return _errored; } }

        
        public ModuleBase()
        {
            _moduleId = _gidCounter++;
        }
        public ModuleBase(string name)
        {
            _moduleName = name;
            _moduleId = _gidCounter++;
        }

        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        public uint ModuleId
        {
            get { return _moduleId; }
        }

        public virtual void Reset()
        {
            _errored = false;
        }

        //public virtual void LogStatus(StatusLog.StateType state, string description)
        //{
        //    _log.Log(_moduleName, state, description);
        //}

        //public virtual void LogStatus(StatusLog.StateType state, string description, bool forceDisplay)
        //{
        //    _log.Log(_moduleName, state, description, forceDisplay);
        //}

        //public virtual void LogError(string description)
        //{
        //    StatusLog.StateType state = StatusLog.StateType.Error;
        //    _log.Log(_moduleName, state, description);
        //}
        //public virtual void LogWarning(string description)
        //{
        //    StatusLog.StateType state = StatusLog.StateType.Warning;
        //    _log.Log(_moduleName, state, description);
        //}

    }
}
