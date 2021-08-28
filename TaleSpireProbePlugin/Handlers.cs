using BepInEx;
using System.Diagnostics;
using UnityEngine;

namespace LordAshes
{
    public partial class ProbePlugin : BaseUnityPlugin
    {
        /// <summary>
        /// Handler for Stat Messaging subscribed messages.
        /// </summary>
        /// <param name="changes"></param>
        public void HandleRequest(StatMessaging.Change[] changes)
        {
            foreach (StatMessaging.Change change in changes)
            {
                if(change.action!=StatMessaging.ChangeType.removed)
                {
                    if (change.value == "[Probe]")
                    {
                        // Get results
                        UnityEngine.Debug.Log("Probe Plugin: Sending Probe Results");
                        StatMessaging.SetInfo(change.cid, change.key, GetPluginList());
                    }
                    else
                    {
                        // Record results
                        if (LocalClient.IsInGmMode)
                        {
                            UnityEngine.Debug.Log("[" + change.key.Substring(ProbePlugin.Guid.Length + 1) + "]\r\n" + change.value);
                            StatMessaging.ClearInfo(change.cid, change.key);
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Probe Plugin: Ignoring Probe Result - Not GM");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handler for Radial Menu selections
        /// </summary>
        /// <param name="cid"></param>
        private string GetPluginList()
        {
            string result = "";
            foreach(string pluginDir in System.IO.Directory.EnumerateDirectories(BepInEx.Paths.PluginPath))
            {
                bool dllFound = false;
                string name = pluginDir.Substring(pluginDir.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                foreach (string plugin in System.IO.Directory.EnumerateFiles(pluginDir,"*.DLL*"))
                {
                    if (plugin.ToUpper().EndsWith(".DLL"))
                    {
                        // Active plugin
                        result = result + name + " (" + FileVersionInfo.GetVersionInfo(plugin).FileVersion + ")\r\n";
                    }
                    else
                    {
                        // Deactivated plugin
                    }
                    dllFound = true;
                    break;
                }
                if (!dllFound)
                {
                    // Plugin with content only
                    result = result + name + " (No Plugin, Content Only)\r\n";
                }
            }
            return result;
        }
    }
}
