using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PathRenderAgent
{
    public partial class MainAgent : ServiceBase
    {
        public MainAgent()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            AgentQueueProcessor.Instance.StartProcess(args);
        }

        protected override void OnStop()
        {
            AgentQueueProcessor.Instance.StopProcess();
        }
    }
}
