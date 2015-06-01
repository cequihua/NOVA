using System;
using System.ServiceProcess;

namespace Mega.Synchronizer.Service
{
    static class Program
    {
        static void Main()
        {
            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(new ServiceBase[] { new SynchronizerService() });
            }
            else
            {
               (new SynchronizerService()).StartProcess();
            }
        }
    }
}
