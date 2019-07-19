namespace Mallos.Insight.Nancy
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    class EmbeddedResourceReader
    {
        private readonly Assembly[] assemblies;
        private readonly Dictionary<string, int> assembliesFiles;

        public EmbeddedResourceReader(params Assembly[] assemblies)
        {
            this.assemblies = assemblies;
            this.assembliesFiles = new Dictionary<string, int>();

            for (var i = 0; i < assemblies.Length; i++)
            {
                var files = assemblies[i].GetManifestResourceNames();
                foreach (var file in files)
                {
                    this.assembliesFiles[file] = i;
                }
            }
        }

        public bool Exist(string name)
        {
            return this.assembliesFiles.ContainsKey(name);
        }

        public string ReadFile(string name)
        {
            if (!this.assembliesFiles.ContainsKey(name))
            {
                return null;
            }

            var assemblyIndex = this.assembliesFiles[name];
            var assembly = assemblies[assemblyIndex];

            using (Stream stream = assembly.GetManifestResourceStream(name))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
