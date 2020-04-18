# Starfighter: Infinity Voice Companion

### Table of contents:

1. [What Is This?](#intro)
2. [Prerequisites](#prereq)
3. [Installation](#install)
4. [Basic usage](#howToUse)
5. [Troubleshooting](#issues)
6. [Frequently Asked Questions](#faq)



<a name="intro"></a>
## What Is This?

**This project is a simple voice companion which will help you on your journeys while playing [Starfighter: Infinity](http://www.starfighterinfinity.com/).** *Starfighter: Infinity* is a space based MMORPG with a focus on ‘dogfighting’ style action and exploration.  It is in currently in early access on [Steam](https://store.steampowered.com/app/967330/Starfighter_Infinity/).

My primary focus has been on streamlining weapon usage during battle. Weapons can be set up in groups, and groups can be fired as a one-off or in a continuous barrage. Beyond that most ship functions can be accessed verbally using the companion. The companion also has some limited chatting options, though I plan to flesh them out more in the future.

Please note that I am not currently distributing any pre-recorded voice files for this companion. As such all interactions are currently done via text-to-speech.

<a name="prereq"></a>
## Prerequisites

In order to make use of this ship's companion you must have the full version of [VoiceAttack](https://voiceattack.com/) in addition to [Starfighter: Infinity](http://www.starfighterinfinity.com/). Note - the demo version of VoiceAttack **will not work** with this project.

<a name="install"></a>
## Installation

Currently installation is as simple as downloading a zip from [Releases page](https://github.com/mtmosier/sf-i-voice-companion/releases), extracting the sf-i_companion.vap file (located under sf-i-voice-companion\VoiceAttack\Profile) and importing that profile in to VoiceAttack.

In the future I plan to add some basic voice mp3s and a VA plugin, but for now the profile alone is all that's required.

If you own an HCS Voice Pack you can also import voice files from your existing pack. See the [Import](import/) section for instructions.

<a name="howToUse"></a>
## Basic usage

Here's an example of a typical interaction.  Assume all requests and responses are verbal.

```
> What is my current ship?
  # Currently using explorer ship
> Switch to battle ship
  # Configuration updated
> List active weapon Groups
  # No groups are currently in use
> Configure missile 1
  # Which weapon would you like to fire first?
> Slot 1
  # second?
> Slot 2
  # third?
> Complete
  # Group missile 1 will fire slot 1, slot 2.  Do you want to save this weapon group?
> Commit
  # Group saved. Configuration Complete.
> Configure evasive maneuvers
  #
```

To see a full list of voice commands available please check the [voice command reference guide](https://htmlpreview.github.io/?https://github.com/mtmosier/sf-i-voice-companion/blob/master/VoiceAttack/Reference/Starfighter%20Infinity%20Companion%20Reference.html).

The weapon group firing system is somewhat robust. [Read a more detailed description of what you can do with weapon groups.](weaponGroupConfigurationReference.md)



<a name="issues"></a>
## Troubleshooting

##### The companion doesn't respond correctly more often than not.

This is a common problem with speech recognition, especially when using the built-in speech engine from windows. The first thing to try is to train your speech engine. [Here are the instructions provided by Microsoft for doing so.](https://support.microsoft.com/en-us/help/4027176/windows-10-use-voice-recognition)

I have some issues with failed recognition myself, due to living in a noisy apartment. That's the reason why many voice commands in this profile have multiple methods of calling them. For example, you can say "Switch to battle ship" or "Swap to battle" or "Change to battle ship", all of which do the same thing. [View the voice command reference guide](https://htmlpreview.github.io/?https://github.com/mtmosier/sf-i-voice-companion/blob/master/VoiceAttack/Reference/Starfighter%20Infinity%20Companion%20Reference.html) to see all commands, and command variations available.

If you're still having trouble with your voice recognition I suggest reading over this [post on the VoiceAttack forums](https://forum.voiceattack.com/smf/index.php?topic=1635.0), which covers more advanced troubleshooting methods better than I ever could.

##### Sometimes the companion responds several seconds late, or not at all.

I have found that on occasion companion responses may get quite laggy. I'm continuing to look in to the issue, though I'm honestly not sure how much control I have over this. So far it seems that the speech engine gets less priority than the running game, and if the game is taxing your system the speech engine is high up on the list of processes to be delayed.

One thing I would suggest is to open the "[Config] General" profile command under Configuration and reduce the number of weapon group names (>>weaponGroupNameList) and ship names (>>shipNameListStr) included in the profile. Each weapon group added to the list increases the total number of commands the speech engine has to search for by over a dozen per, which can adversely affect performance. Ship names have less of an impact, but can help nonetheless. Also consider changing the number of weapon groups available under each group name (>>maxWeaponGroupNum). I've set this value to 3 by default, which I find to be a good compromise between performance and usability. Setting it to 2, or even 1 would be less taxing on your system.

*After changing the above configuration settings you will have to reload your profile before using it further. Do so by switching to another profile and back, or closing and re-starting voice attack.*

##### The companion says it engaged hyperspace (or other such command) even though it really didn't.

There currently is no direct tie-in with Starfighter: Infinity. This companion doesn't actually know what's going on in game. It is simply taking your commands and attempting to perform them in game, with no idea of whether they succeeded or failed. So if you ask to engage hyperspace with no course plotted, or without auto-pilot active, it will fail with no verbal warning from the companion. The same goes for trying to fire a weapon with no ammo or not enough energy.

I hope to add some game log parsing to get feedback directly from the companion at some point, but for the moment all I can offer is this somewhat-blind companion.


<a name="faq"></a>
## Frequently Asked Questions

1. Can you make this work with [some other game]?
  * I don't have time to port to other projects at the moment.  However, you are more than welcome to clone the project and update it for use with any game you want.
  * Depending on your desired game you might be able to change the keybind configuration to make the profile work with it with minimal changes.
2. Why are there no voice files included?
  * I plan to record the default companion voice, Null, myself when I have time. I don't know when I'll have that done, but assume it'll be a while.
3. I already own an HCS Voice Pack.  Can I use my existing sound files with this project?
  * Yes, you can.  Check the [Import](import/) section for instructions.
  * Please note that this project is in no way affiliated with HCS, and is not authorized to use these voice files in any capacity. Use at your own risk!
4. Can I use this voice companion in VR?
  * Yes, this project was designed with the idea that the user would be in VR. To that end most weapon and ship configuration is done via voice interface. You can also ask for details about existing configurations to get a verbal description.
  * Note that VR is in no way necessary to make use of this companion.
5. Can I use the VoiceAttack demo with this companion?
  * No, you cannot. The VA demo only allows up to 20 voice commands. At the time of this writing this profile is using over 1,300 (mostly derived) commands. It would be very difficult to compress it down to only 20.
6. I don't like the weapon group names or ship names you selected.  Can I change them?
  * Yes, absolutely. Open the profile in VoiceAttack and edit the command labeled "[Config] General" under the "Configuration" category. Edit the variables ">>weaponGroupNameList" and/or ">>shipNameListStr" to better reflect your play style.  These must be formatted as a semi-colon (;) separated list.
  * **After making changes to these variables you must reload the profile before they will take affect. You can do this by switching to another profile and switching back, or by restarting VoiceAttack.**
