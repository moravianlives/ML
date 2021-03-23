using Zen.Web.Host;

namespace edu.bucknell.project.moravianLives.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Builder.Start<Startup>(args);
        }
    }
}