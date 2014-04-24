using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeoLib.Parsing
{
    public abstract class ParsingTask
    {
        protected object[] Parameters { get; private set; }

        protected string Path { get; set; }

        protected ParsingTask(IEnumerable<object> prmse)
        {
            var prms = prmse.ToArray();
            Parameters = prms.ToArray();
            var firstOrDefault = prms.FirstOrDefault();
            if (firstOrDefault != null) 
                Path = firstOrDefault.ToString();
        }

        public void Execute()
        {
            ExecuteInternal();
        }

        protected abstract void ExecuteInternal();
    }
}
