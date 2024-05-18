using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPBEMAssistant.Core
{

    [Serializable]
    public class KnownDataSource
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Password { get; set; }
        //public string Name { get; set; }
    }
}
