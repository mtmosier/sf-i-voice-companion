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

This is a simple voice assistant which will help you on your journeys while playing [Starfighter: Infinity](http://www.starfighterinfinity.com/). My primary focus has been on streamlining weapon usage. Most ship functions can be accessed verbally using the assistant. The assistant also has some limited chatting options, though I plan to flesh them out much more in the future.

<a name="prereq"></a>
## Prerequisites

In order to make use of this voice assistant you must have a full version of [VoiceAttack](https://voiceattack.com/) in addition to [Starfighter: Infinity](http://www.starfighterinfinity.com/).  Note - the demo version of VoiceAttack **will not work** with this project.

<a name="install"></a>
## Installation

***Coming Soon***

<a name="howToUse"></a>
## Basic usage

Here's an example of a typical interaction.  Assume all requests and responses are verbal.

```
> Greetings
  # Hello
> Who are you?
  # I'm your ship's companion.  My name is Null.
> What is my current ship?
  # Currently using explorer ship
> Switch to battle ships
  # Configuration updated
> List active weapon Groups
  # No groups are currently in use
> Configure missile 1
  # Which weapon would you like to fire first?
> Slot 1
  # second?
> Slot 2
  # third?
> Stop there
  # Group missile 1 will fire slot 1, slot 2.  Do you want to save this weapon group?
> Commit
  # Group saved. Configuration Complete.
> Configure evasive maneuvers
  #
```

<a name="issues"></a>
## Troubleshooting

***Coming Soon***

<a name="faq"></a>
## Frequently Asked Questions

1. Can you make this work with [some other game]?
  * I don't have time to port to other projects at the moment.  However, you are more than welcome to clone the project and update it for use with any game you want.
  * Depending on your desired game you might be able to change the keybind configuration to make the profile work with it with essentially no changes.
2. Why are there no voice files included?
  * I plan to record the default companion voice, Null, myself when I have time. I don't know when I'll have that done.
3. I already own an HCS Voice Pack.  Can I use my existing sound files with this project?
  * Yes, you can.  Check the [Import](import/) scripts for instructions.
  * Please note that this project is in no way affiliated with HCS, and is not authorized to use these voice files in any capacity. Use at your own risk.
4. Can I use this voice assistant in VR?
  * Yes, this project was designed with the idea that the user would be in VR. To that end most weapon and ship configuration is done via voice interface. You can also ask for details about existing configuration to get a verbal description.
  * Note that VR is in no way necessary to make use of this assistant.
5. Can I use the VoiceAttack demo with this assistant?
  * No, you cannot. The VA demo only allows up to 20 voice commands. At the time of this writing this profile is using over 1,300 commands. It would be very difficult to compress it down to only 20.
6. I don't like the weapon group names or ship names you selected.  Can I change them?
  * Yes, absolutely. Open the profile in VoiceAttack and edit the command labeled "[Config] General" under the "Configuration" category. Edit the variables ">>weaponGroupNameList" and/or ">>shipNameListStr" to better reflect your play style.  These must be formatted as a semi-colon (;) separated list.
