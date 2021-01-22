using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TaskRunnerExplorer;

namespace ManifestTaskRunner
{
    [TaskRunnerExport("source.extension.vsixmanifest")]
    public class ManifestTaskRunner : ITaskRunner
    {
        public List<ITaskRunnerOption> Options => null;

        public Task<ITaskRunnerConfig> ParseConfig(ITaskRunnerCommandContext context, string configPath)
        {
            var rootDir = Path.GetDirectoryName(configPath);
            var rootNode = new TaskRunnerNode("VSIX Manifest");

            var ipConfigNode = new TaskRunnerNode("IPConfig", true)
            {
                Command = new TaskRunnerCommand(rootDir, "ipconfig", "all")
            };

            rootNode.Children.Add(ipConfigNode);

            var traceNode = new TaskRunnerNode("Trace Route", true)
            {
                Command = new TaskRunnerCommand(rootDir, "tracert", "visualstudio.com")
            };

            rootNode.Children.Add(traceNode);
            var config = new ManifestTaskRunnerConfig(rootNode);
            return Task.FromResult<ITaskRunnerConfig>(config);
        }
    }
}
