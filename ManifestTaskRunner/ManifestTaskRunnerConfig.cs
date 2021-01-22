using System.IO;
using System.Text;
using System.Windows.Media;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.TaskRunnerExplorer;

namespace ManifestTaskRunner
{
    internal class ManifestTaskRunnerConfig : ITaskRunnerConfig
    {
        public ManifestTaskRunnerConfig(ITaskRunnerNode rootNode)
        {
            TaskHierarchy = rootNode;
        }

        public ImageSource Icon => KnownMonikers.CSExtension.ToBitmap(16);

        public ITaskRunnerNode TaskHierarchy {get;}

        public void Dispose()
        {
        }

        public string LoadBindings(string configPath)
        {
            var lines = File.ReadAllLines(configPath);

            foreach (var line in lines)
            {
                if (line.StartsWith("<!--<binding"))
                {
                    return line.Replace("<!--", "").Replace("-->", "");
                }
            }

            return null;
        }

        public bool SaveBindings(string configPath, string bindingsXml)
        {
            var lines = File.ReadAllLines(configPath);
            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                if (!line.Contains("<binding"))
                {
                    sb.AppendLine(line);
                }
            }

            sb.Append("<!--" + bindingsXml + "-->");
            File.WriteAllText(configPath, sb.ToString());

            return true;
        }
    }
}