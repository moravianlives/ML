using edu.bucknell.project.moravianLives.model.Common;
using System;
using System.Linq;
using Zen.Base;
using Zen.Base.Extension;
using Zen.Base.Module.Service;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub
{
    class Program
    {
        static void Main(string[] args)
        {
            var importProcesses = IoC.
                GetClassesByInterface<IMoravianLivesDataOnboarding>()
                .CreateInstances<IMoravianLivesDataOnboarding>()
                .ToList();

            foreach (var process in importProcesses)
            {
                try
                {
                    process.Run();
                }
                catch (Exception e)
                {
                    Current.Log.Add(e);
                }
            }
        }
    }
}
