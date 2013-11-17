using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PathRenderAgent
{
    class AgentQueueProcessor
    {

        #region "Private Vars"

        private static AgentQueueProcessor instance;
        private bool RunFlag = true;
        #endregion

        #region "Contructor"

        private AgentQueueProcessor() { }

        #endregion

        #region "Singleton Constructor"

        public static AgentQueueProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AgentQueueProcessor();
                }

                return instance;
            }
        }

        #endregion

        #region "Public Methods"

        public void StartProcess(string[] args)
        {
            while (RunFlag)
            {
                Thread.Sleep(100);
            }
        }

        public void StopProcess()
        {
            RunFlag = false;
        }


        #endregion

        #region "Private Methods"



        #endregion

    }
}
