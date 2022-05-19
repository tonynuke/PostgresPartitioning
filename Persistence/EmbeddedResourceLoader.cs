using System.Reflection;

namespace Persistence
{
    public static class EmbeddedResourceLoader
    {
        public static string ReadResourceFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(fileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
