using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL2.Neo.Models.Minecraft
{
    public class Java
    {
        private static string[] Windows()
        {
            var javaList = new List<string>();

            // find by environment path
            // JAVA_HOME
            var javaPath = Environment.GetEnvironmentVariable("JAVA_HOME");

            if (javaPath == null || !Directory.Exists(javaPath)) return javaList.ToArray();

            var exePath = Path.Combine(javaPath, "bin");
            if (File.Exists(exePath))
            {
                javaList.Add(exePath);
            }

            // PATH
            javaPath = Environment.GetEnvironmentVariable("PATH");
            var pathItemList = javaPath.Split(';');
            javaPath = pathItemList.FirstOrDefault(x => x.Contains("jdk"));

            return javaList.ToArray();
        }

        private static string[] Unix()
        {
            var javaList = new List<string>();


            return javaList.ToArray();
        }

        public static string[] SearchJava()
        {
            var java = new List<string>();

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    java.AddRange(Windows());
                    break;
                case PlatformID.Unix:

                    break;
            }

            return java.ToArray();
        }
    }
}
