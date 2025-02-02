using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL2.Neo.Models.Minecraft
{
    public class JavaEntry
    {
    }

    public class JavaEntity
    {
        public required short Is64Bit { set; get; }
        public required string Name { set; get; }
        public required string Path { set; get; }
        public required string Version { set; get; }
    }
}
