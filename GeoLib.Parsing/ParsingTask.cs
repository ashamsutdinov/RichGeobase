using System.Linq;

namespace GeoLib.Parsing
{
    public abstract class ParsingTask
    {
        protected string[] Parameters { get; private set; }

        protected string Path { get; set; }

        protected ParsingTask(string[] prms)
        {
            Parameters = prms;
            Path = prms.FirstOrDefault();
        }

        public void Execute()
        {
            ExecuteInternal();
        }

        protected abstract void ExecuteInternal();
    }
}
