# Importing HCS Voice Pack Files

### Here there be Dragons
Please note that this project is in no way affiliated with HCS, and is not authorized to use these voice files in any capacity. By using these files with Starfighter: Infinity you may be in violation of your HCS terms and conditions. Due to this I cannot recommended nor officially support usage of these files.

#### Use at your own risk!

### TL;DR

1. Run the "*copyHCSVoiceFiles.cmd*" file
2. Reload the SF:I Companion VoiceAttack profile
3. Say "Promote Jazz"  (substitute "Jazz" with your favorite companion)

### Which voice packs work with this companion?

I have personally tested using Vega [ED], Jazz [ED], and Carina [SC]. In theory any HCS Voice Pack should work, though I can't promise that.

Note that none of the voice packs contain the proper recordings needed for weapon configuration. If you do use a voice pack you should expect to still hear some text-to-speech generated voice being used in these areas.

If you do use end up using a voice pack and you hear some text-to-speech generated voices where you think there shouldn't be (outside of configuration or querying ship/weapon status), [please let me know](mailto:m.t.m.o.s.i.e.r@gmail.com). I may be able to make some adjustments to get your particular voice pack working.

### How do I get my voice pack to work?

Make sure you already have your HCS Voice Pack(s) you want to use fully installed. [Download the "*copyHCSVoiceFiles.cmd*" file from the releases page.](https://github.com/mtmosier/sf-i-voice-companion/releases/latest)

For most people the import process will be as simple as running the batch file then reloading the VoiceAttack SF:I Companion profile.  To reload the profile either switch to a different profile and then back, or close and reopen VoiceAttack.

### It didn't work! Now what?

The import script will search through the most common VoiceAttack installation directories trying to find where the relevant files are stored.  If you've specified a custom install/sounds path, or if you've moved VoiceAttack to a different location after installing, this can cause the import to fail.

If having trouble you should try editing the import script and adding the correct directory to paths being searched. I'd suggest adding your path as the first one in order to avoid old files being loaded by mistake.

![copyHCSVoiceFiles.cmd setup](../images/copyHCSVoiceFiles_config.png?raw=true)

If you're not sure where your VoiceAttack Sounds directory is located you can open up the companion profile in VoiceAttack. As part of the initialization routine I output the Sounds folder path to the VA console.

![copyHCSVoiceFiles.cmd setup](../images/VA_companion_init_output.png?raw=true)

Once you've updated the "*copyHCSVoiceFiles.cmd*" batch file you need to run it. This is as simple as double clicking the file in Explorer. When it completes you should see output similar to the following:

![copyHCSVoiceFiles.cmd setup](../images/copyHCSVoiceFiles_output.png?raw=true)

Next you will need to reload your companion profile. You can do so by switching to a different profile and switching back, or by restarting VoiceAttack. To use the companion of your choice run the "**Promote**" command.

```
> Promote Jazz
  # Jazz online
```
