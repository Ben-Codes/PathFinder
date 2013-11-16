using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathRenderAgent
{
    class AgentQueueProcessor
    {

        #region "Private Vars"

        private static AgentQueueProcessor instance;

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

        }

        public void StopProcess()
        {

        }


        #endregion

        #region "Private Methods"



        #endregion

    }
}
