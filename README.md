# Probe Plugin

Allows GM to request a player's Plugin list. The plugin will collect a list of the player's active plugins and any content mod
packs downloaded from ThunderStore along with their versions and send it to the GM where it gets logged into the GM's log.
While the plugin can be used to potentially spot cheaters using unknown plugins, the intention of the plugin is for the GM to
be able to verify that the players are using the correct plugins necessary for the game. This is a convenient way for the GM
to check if the group is not using a common R2ModMan profile.

This plugin only checks for plugins (added manually or downloaded from ThuderStore) and (asset) mod packs downloaded from
ThunderStore. It does not verify the presence of (assets) content that is added manaully.

## Change Log

1.0.0: Initial release

## Install

Use R2ModMan or similar installer to install this plugin.ntegration functionality.

## Usage

The is no "allow confirmation" implemented because this plugin cannot be forced on a player. By the fact that the player
installs the plugin, they are giving their GM permission to probe their plugin list. The player does not need to do anything
to facilitate a probe.

To initiate a probe, the initiating user must be a GM (players are prevented from using the Probe function) and must have a
mini selected. The results of the probe, for each connected player, will be obtained and written to the log. 
