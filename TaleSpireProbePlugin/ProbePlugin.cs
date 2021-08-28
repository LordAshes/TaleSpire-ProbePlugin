using BepInEx;
using BepInEx.Configuration;

using UnityEngine;

namespace LordAshes
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(LordAshes.StatMessaging.Guid)]
    public partial class ProbePlugin : BaseUnityPlugin
    {
        // Plugin info
        public const string Name = "Probe Plug-In";             
        public const string Guid = "org.lordashes.plugins.probe";
        public const string Version = "1.0.0.0";                 

        // Configuration
        private ConfigEntry<KeyboardShortcut> triggerKey { get; set; }      // Sample configuration for triggering a plugin via keyboard

        /// <summary>
        /// Function for initializing plugin
        /// This function is called once by TaleSpire
        /// </summary>
        void Awake()
        {
            // Not required but good idea to log this state for troubleshooting purpose
            UnityEngine.Debug.Log("Probe Plugin: Active.");

            // The Config.Bind() format is category name, setting text, default
            triggerKey = Config.Bind("Hotkeys", "States Activation", new KeyboardShortcut(KeyCode.P, KeyCode.RightControl));

            

            Utility.PostOnMainPage(this.GetType());
        }

        /// <summary>
        /// Function for determining if view mode has been toggled and, if so, activating or deactivating Character View mode.
        /// This function is called periodically by TaleSpire.
        /// </summary>
        void Update()
        {
            if (Utility.StrictKeyCheck(triggerKey.Value))
            {
                if (LocalClient.IsInGmMode)
                {
                    if (Utility.isBoardLoaded())
                    {
                        CreatureBoardAsset asset;
                        CreaturePresenter.TryGetAsset(LocalClient.SelectedCreatureId, out asset);
                        if (asset != null)
                        {
                            foreach (PlayerInfo player in CampaignSessionManager.PlayersInfo.Values)
                            {
                                UnityEngine.Debug.Log("Probe Plugin: Sending Probe Result To " + player.Name);
                                StatMessaging.Subscribe(ProbePlugin.Guid + "." + player.Name, HandleRequest);
                                StatMessaging.SetInfo(LocalClient.SelectedCreatureId, ProbePlugin.Guid + "." + player.Name, "[Probe]");
                            }
                        }
                        else
                        {
                            SystemMessage.DisplayInfoText("Probe Requires Selected Mini");
                        }
                    }
                }
                else
                {
                    SystemMessage.DisplayInfoText("Only GM Can Use Probe Function");
                }
            }
        }
    }
}
