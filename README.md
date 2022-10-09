# Starfighter: Infinity Voice Companion

### Table of contents:

1. [What Is This?](#intro)
2. [Prerequisites](#prereq)
3. [Installation](#install)
4. [Basic usage](#howToUse)
5. [Configuration](#config)
6. [Command list](#commandList)
7. [Troubleshooting](#issues)
8. [Frequently Asked Questions](#faq)
9. [Credits](#credits)


<a name="intro"></a>
## What Is This?

**This project is a simple voice companion which will help you on your journeys while playing [Starfighter: Infinity](http://www.starfighterinfinity.com/).** *Starfighter: Infinity* is a space based MMORPG with a focus on ‘dogfighting’ style action and exploration. It is in currently in early access on [Steam](https://store.steampowered.com/app/967330/Starfighter_Infinity/).

My primary focus has been on streamlining weapon usage during battle. Weapons can be set up in groups, and groups can be fired as a one-off or in a continuous barrage. Beyond that most ship functions can be accessed verbally using the companion. The companion also has some limited chatting options, though I plan to flesh them out more in the future.

This application requires [VoiceAttack](https://voiceattack.com/) to function.  Please note that I am not currently distributing any prerecorded voice files for this companion. As such all interactions are currently done via text-to-speech (unless you [Import](import/) your own voice files).


<a name="prereq"></a>
## Prerequisites

In order to make use of this ship's companion you must have the full version of [VoiceAttack](https://voiceattack.com/) in addition to [Starfighter: Infinity](http://www.starfighterinfinity.com/). Note - the demo version of VoiceAttack **will not work** with this project.


<a name="install"></a>
## Installation

Currently installation is as simple as [downloading the "*sf-i_companion.vap*" file from the releases page](https://github.com/mtmosier/sf-i-voice-companion/releases/latest) and importing it in to VoiceAttack.

In the future I plan to add some basic voice mp3s and a VA plug-in, but for now the profile alone is all that's required. If you own an HCS Voice Pack you can also import voice files from your existing pack. See the [Import](import/) section for instructions.


<a name="howToUse"></a>
## Basic usage

Here's a very basic, no config needed, I-just-want-to-kill-stuff example of usage.

```
> Unload weapon slot 1    # continuous fire weapon slot 1
> Unload slot 2    # continuous fire weapon slot 2 (will alternate with slot 1)
> Fire weapon slot 6    # single fire weapon slot 6 (after which slots 1 and 2 will continue to alternate)
> Fire primary    # continuous fire primary
[...]
> Cease fire    # stop firing both secondary and primary weapons
> Engines to max    # holds accelerate and propulsion keys
[...]
> All stop    # releases accelerate and propulsion keys
> Show system map    # display the system map dialog
> Scan object    # scan an object if in range. also read the codex entry if found
```

If you are [importing a voice pack](import/) there will be a few additional commands available to you.
```
> Promote Jazz    # Jazz Online  (only usable if you have imported voicepacks
> Hello    # Greetings
> Who are you?    # I am your ship's companion...
> Tell me a random constellation fact    # Pisces is a constellation. Its name is the latin..
> Give me a random quantum theory fact    # Hawking Radiation. In 1975 Steven Hawking showed...
> Give me a random galaxy fact    # Neutrinos. A particle produced by the nuclear reactions...
> Enable galaxy    # Interactive mode on
> What is a black hole    # A black hole is a region of spacetime...
> Enable constellations    # Interactive mode on
> Tell me about constellation Ares    # Ares. Representing a ram's horns. It is one of...
```


<a name="config"></a>
## Configuration

There is settings dialog which you can open using the command **"Show configuration dialog"** or keyboard shortcut [Left Ctrl + Left Alt + S].

You can also do your configuration using voice commands, including setting up weapon groups.  All requests and responses below are verbal.

```
> What is my current ship?
  # Currently using explorer ship
> Switch to battle ship
  # Configuration loaded
> List active weapon groups
  # No active weapon groups found
> Configure missile 3
  # Which weapon do you want to activate first?
> Slot 3
  # Second?
> Slot 4
  # Third?
> Pause 2
  # Fourth?
> Finished
  # Weapon group missile 3 will fire slot 3, slot 4, pause for 2 seconds.
  # Do you want to save these settings?
> Commit
  # Configuration saved.
> Engines to max
  # Engaging full burn
> Unload missile 3
  # Launching missiles
[...]
> Cease fire
  # Holding fire
> All stop
  # Cutting engines
```

[Read a more detailed description of what you can do with weapon groups.](weaponGroupConfigurationReference.md)


## Command list
<a name="commandList"></a>

This command list may be incomplete. To see a full list of voice commands available please check the [voice command reference guide](https://htmlpreview.github.io/?https://github.com/mtmosier/sf-i-voice-companion/blob/master/reference/Starfighter%20Infinity%20Companion%20Reference.html).

| Command | Description |
|:------- |:----------- |
| **Stop listening** | Set VoiceAttack to not listen. |
| **Start listening** | Set VoiceAttack to begin listening again. |
| **List available ships** | Responds with a list of valid ship names. |
| **List active ships** | Responds with a list of ships which have previously been used. |
| **Register new ship** | You will be asked for a new ship name. The ship will be added and made active. |
| **Switch to sniper ship** | Loads the configuration for **Sniper** ship. |
| **List available weapon groups** | Responds with a list of valid weapon group names. |
| **List active weapon groups** | Responds with a list of weapon groups which have been configured for your current ship. |
| **Configure counter 1** | Initiate weapon configuration for group **Counter 1**. |
| **Delete mining laser** | Deletes any configuration saved for **Mining Laser 1**. |
| **Equip slot 6** | Activates (but does not fire) **Slot 6**. Will also return to this weapon slot when finished performing firing actions. |
| **Fire weapon slot 3** | Single fire **Slot 3**. |
| **Unload slot 1** | Continuous fire **Slot 1**. |
| **Cancel slot 1** | Stop firing **Slot 1**. |
| **Fire missile** | Single fire group **missile**. |
| **Unload mine 2** | Continuous fire group **mine 2**. |
| **Unload beacon** | Continuous fire group **beacon**. |
| **Cancel beam 3** | Stop firing group **beam 3**. |
| **Unload primary** | Continuous fire primary weapon. |
| **Cancel primary** | Stop firing primary weapon. |
| **Cease fire** | Stops firing any active weapon groups as well as the primary weapon. |
| **Configure *&lt;emergency group&gt;*** | Initiate configuration for the emergency group. |
| **Red Alert** | Activate **Red Alert** emergency group. If configured with actions they will be carried out. |
| **Yellow Alert** | Activate **Yellow Alert** emergency group. If configured with actions they will be carried out. |
| **Evasive Maneuvers** | Activate **Evasive Maneuvers** emergency group. If configured with actions they will be carried out. |
| **Next Target** | Switch between locked targets. |
| **Engines to full** | Holds down the accelerate key. |
| **Engines to max** | Holds down the accelerate and propulsion keys. |
| **All stop** | Releases the accelerate and propulsion keys. |
| **Cruise control** | Toggles cruise control. |
| **Engage autopilot** | Engages autopilot. |
| **Engage hyperspace** | Engages hyperspace. |
| **Show system map** | Activates the system map dialog. |
| **Show ship info** | Activates the ship information dialog. |
| **Show mission log** | Activates the objectives dialog. |
| **Cargo hold** | Activates the inventory dialog. |
| **Expand chat** | Expand in game chat. Add 2 or 3 to expand the chat multiple times. |
| **Take a screenshot** | Takes a screenshot. |
| **Scan object** | Scan an object if in range. Also read the codex entry if found. |
| **Tell me a codex fact** | Reads a random codex fact. |
| **Tell me a planet fact** | Reads a random codex fact limited to planet descriptions. |
| **Tell me a object fact** | Reads a random codex fact limited to object descriptions. |
| **Tell me a race fact** | Reads a random codex fact limited to race/org descriptions. |
| **Tell me a ship fact** | Reads a random codex fact limited to ship descriptions. |
| **Enable codex** | Turns on individual codex fact look-ups. |
| **Disable codex** | Turns off individual codex fact look-ups. |
| **Tell me about planet *&lt;planet name&gt;*** | Reads the planet description from the codex. |
| **Tell me about the *&lt;object name&gt;*** | Reads the object description from the codex. |
| **Tell me about the *&lt;race name&gt; race*** | Reads the race/org description from the codex. |
| **Tell me about the *&lt;ship name&gt;*** | Reads the ship description from the codex. (Player ships only) |
| **What time is it** | Responds with the current time. |
| **Hello *&lt;companion name&gt;*** | Voice response depending on companion used. |
| **Who are you** | Voice response depending on companion used. |
| **Why are you on my ship** | Voice response depending on companion used. |
| **Thank you** | Voice response depending on companion used. |
| **That was sarcasm** | Voice response depending on companion used. |
| **Please stop talking** | Voice companion will stop playing sounds or reading text. |

[Read a more detailed description of what you can do with weapon groups.](weaponGroupConfigurationReference.md)

Additional commands relevant if you are [importing a voice pack](import/).

| Command | Description |
|:------- |:----------- |
| **Promote *&lt;companion name&gt;*** | Will switch to a new ship's companion. |
| **Tell me a constellation fact** | Plays a random constellation fact* |
| **Tell me a quantum theory fact** | Plays a random quantum theory fact* |
| **Tell me a galaxy fact** | Plays a random galaxapedia fact* |
| **Enable constellations** | Turns on individual constellation fact look-ups* |
| **Disable constellations** | Turns off individual constellation fact look-ups* |
| **Tell me about constellation *&lt;constellation name&gt;*** | Plays information about a specific constellation* |
| **Enable quantum theory** | Turns on individual quantum theory fact look-ups* |
| **Disable quantum theory** | Turns off individual quantum theory fact look-ups* |
| **What is a *&lt;quantum theory entry&gt;*** | Plays a specific quantum theory fact* |
| **Enable galaxy** | Turns on individual galaxapedia fact look-ups* |
| **Disable galaxy** | Turns off individual galaxapedia fact look-ups* |
| **What is a *&lt;galaxapedia entry&gt;*** | Plays a specific galaxapedia fact* |

* Not all voice packs include constellations/quantum theory/galaxapedia facts


<a name="issues"></a>
## Troubleshooting

#### The companion doesn't respond consistently (or ever).

This is a common problem with speech recognition, especially when using the built-in speech engine provided with windows. The first thing to try is to train your speech engine. [Here are the instructions provided by Microsoft for doing so.](https://support.microsoft.com/en-us/help/4027176/windows-10-use-voice-recognition)

I have some issues with failed recognition myself due to living in a noisy apartment. That's the reason why many voice commands in this profile have multiple methods of calling them. For example, you can say "Switch to battle ship" or "Swap to battle" or "Change to battle ship", all of which do the same thing. [View the voice command reference guide](https://htmlpreview.github.io/?https://github.com/mtmosier/sf-i-voice-companion/blob/master/reference/Starfighter%20Infinity%20Companion%20Reference.html) to see all commands and command variations available.

If you're still having trouble with your voice recognition I suggest reading over this [post on the VoiceAttack forums](https://forum.voiceattack.com/smf/index.php?topic=1635.0) which covers more advanced troubleshooting methods better than I ever could.


#### Sometimes the companion responds several seconds late, or not at all.

I have found that on occasion the companion responses may get quite laggy. I'm continuing to look in to the issue. So far it seems that the speech engine gets less priority than the running game, and if the game is taxing your system the speech engine is high on the list of processes which get delayed or dropped.

If you do experience this issue I'd appreciate it if you would report [your issue on GitHub](https://github.com/mtmosier/sf-i-voice-companion/issues), or [email me](mailto:m.t.m.o.s.i.e.r@gmail.com) letting me know what you were doing at the time of the lag, your system specs, version of the profile you are using, and whether any voice packs are in use.


#### The companion says it engaged hyperspace (or other such command) even though it really didn't.

There currently is no direct tie-in with Starfighter: Infinity. This companion doesn't actually know what's going on in game. It is simply taking your commands and attempting to perform them in game, with no idea of whether they succeeded or failed. So if you ask to engage hyperspace with no course plotted, or without auto-pilot active, it will fail with no verbal warning from the companion. The same goes for trying to fire a weapon with no ammo or not enough energy.

I hope to add some game log parsing to get feedback directly from the companion at some point, but for the moment all I can offer is this somewhat-blind companion.


<a name="faq"></a>
## Frequently Asked Questions

1. **I use custom keybinds in SF:I. Do I really need to configure my kebinds manually in the VoiceAttack profile?**
  * There should be no keybind configuration required. Keybinds are automatically read from the game configuration data upon profile initialization.
  * The keybind configuration included in the profile are the defaults. These will be used if the profile is unable to load some (or all) keybinds from the game configuration data.
  * ***If you change your keybinds while running VoiceAttack you may need to reload the profile before the changes will take affect. You can do this by switching to another profile and switching back, or by restarting VoiceAttack.***
2. **Can you make this work with [some other game]?**
  * I don't have time to port to other projects at the moment. However, you are more than welcome to clone the project and update it for use with any game you want.
  * Depending on your desired game you might be able to change the key bind configuration to make the profile work with it with minimal changes.
3. **Why are there no voice files included?**
  * I plan to record the default companion voice, Null, myself when I have time. I don't know when I'll have that done, but it's safe to assume it'll take me a while.
4. **I already own an HCS Voice Pack. Can I use my existing sound files with this project?**
  * Yes, you can. Check the [Import](import/) section for instructions.
  * Please note that this project is in no way affiliated with HCS, and is not authorized to use these voice files in any capacity. Use at your own risk!
5. **Can I use this voice companion in VR?**
  * Yes, this project was designed with the idea that the user would be in VR. To that end most weapon and ship configuration is done via voice interface. You can also ask for details about existing configurations to get a verbal description.
  * Note that VR is in no way necessary to make use of this companion. A headset with a decent quality mic is recommended though.
6. **Can I use the VoiceAttack demo with this companion?**
  * No, you cannot. The VA demo only allows up to 20 voice commands. At the time of this writing this profile is using over 1,300 (mostly derived) commands. It would be very difficult to compress it down to only 20.
7. **I don't like the weapon group names or ship names you selected. Can I change them?**
  * Yes, absolutely. The easiest way to do this is to open the ship configuration window using the command "Show configuration dialog" or the keyboard shortcut [Left Ctrl + Left Alt + S].  From here you can add or delete ships and weapon groups to better suit you.
8. **My question wasn't answered here. How do I get further help?**
  * Either report [your issue on GitHub](https://github.com/mtmosier/sf-i-voice-companion/issues), or [just email me](mailto:m.t.m.o.s.i.e.r@gmail.com). If this project becomes at all popular I'll set up some forums or similar.



<a name="credits"></a>
## Credits

I've made use of a number of sound files from [Freesound.org](https://freesound.org/) as permitted by their licenses.

* "UI Confirmation Alert, C1.wav" by InspectorJ (www.jshaw.co.uk) of [Freesound.org](https://freesound.org/)
* "UI Confirmation Alert, C2.wav" by InspectorJ (www.jshaw.co.uk) of [Freesound.org](https://freesound.org/)
* "UI Confirmation Alert, C3.wav" by InspectorJ (www.jshaw.co.uk) of [Freesound.org](https://freesound.org/)
* "UI Confirmation Alert, C4.wav" by InspectorJ (www.jshaw.co.uk) of [Freesound.org](https://freesound.org/)
* "UI Confirmation Alert, C5.wav" by InspectorJ (www.jshaw.co.uk) of [Freesound.org](https://freesound.org/)

<!--
* "Heartbeat, Regular, Single, 01-01, LOOP.wav" by InspectorJ (www.jshaw.co.uk) of [Freesound.org](https://freesound.org/)
-->
