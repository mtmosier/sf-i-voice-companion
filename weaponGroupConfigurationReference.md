# Weapon Group Configuration

### Table of contents:

1. [Introduction](#intro)
2. [Default ships](#definedShips)
3. [Default weapon groups](#definedGroups)
4. [Emergency groups](#emergencyGroups)
5. [List of actions](#actionList)
6. [Confirmation prompt](#confirmation)
7. [Firing multiple groups](#advancedFiring)


<a name="intro"></a>
## Introduction

Weapon groups are broken down by name and number. By default this companion is set up to allow up to 3 of each group. So you can have groups set up for **Missile 1**, **Missile 3**, **Mine 2** and **Counter 3** if you like. You can also have them all firing at the same time. (See the [Firing multiple groups](#advancedFiring) section.)

If you want to reference the first group you can address it by name only. "*Fire Missile*" and "*Fire Missile 1*" are interpreted exactly the same way.

A weapon group can include up to 20 individual [actions](#actionList). (More would be possible, but I can't imagine needing to set more than that for a single group.)

<a name="definedShips"></a>
## Default ships

```
Explorer
Sniper
```

You can add additional ships with any name you want by using the command "*Register new ship*".  Alternatively you can edit the ship list by editing the command labeled "**[Config] General**" under the "*Configuration*" category in the VoiceAttack profile.


<a name="definedGroups"></a>
## Default weapon groups

```
Counter
Missile
Mine
Beam
Mining Laser
Beacon
Highlight
Ambush
Reverse
Large
```

Weapon group names can be changed by editing the command labeled "**[Config] General**" under the "*Configuration*" category in the VoiceAttack profile.


<a name="emergencyGroups"></a>
## Emergency groups

In addition to the weapon groups described, you can also set up a few emergency groups. These differ in that you can only set up one of each, and you activate them simply by saying their name as a full command. (no need to say "Fire Red Alert", instead just say "Red Alert")

```
Evasive Maneuvers
Red Alert
Yellow Alert
```

You would configure and use one like this...

```
> Configure Evasive Maneuvers
  # Which weapon do you want to activate first?
> Hold Left
  # Second?
> Augmentation
  # Third?
> Release Left
  # Fourth?
> Propulsion
  # Fifth?
> Finished
  # Evasive maneuvers will fire hold left turn, augmentation, release turn left, propulsion enhancer.
  # Do you want to save these settings?
> Commit
  # Configuration saved.
> Evasive Maneuvers
  # Executing evasive maneuvers
```

Unlike normal weapon groups, emergency groups do not need the "fire" or "unload" prefix, and they will always fire only once per command.


<a name="actionList"></a>
## List of actions available when configuring weapon groups.

During configuration you will be asked which weapon to fire. The following is a list of allowed inputs. When you're done adding weapons to the list use the "complete" command to move on.

| Input | Description | Alternatives | Hold/Release Allowed |
| ----- | ----------- | ------------ | -------------------- |
| complete | End the input | done, stop here, finished | No |
| restart | Cancel configuration so far and start over | start over, do over | No |
| pause 1..60 | Pause processing this group for N seconds | delay 1..60 | No |
| primary | Fire the primary weapon (once) | primary weapon | Yes |
| 0..9 | Fire the specified secondary weapon | slot 0..9, weapon slot 0..9 | No |
| augmentation | Press the augmentation key | augment | Yes |
| propulsion | Press the propulsion enhancer key | propulsion enhancer | Yes |
| radar | Bring up the ship's radar | activate radar, open radar, display radar | No |
| corkscrew | Perform a corkscrew | None | Yes |
| action | Press the action button | None | Yes |
| fine aiming | Use with hold/release to activate fine aiming | None | Yes |
| left | Press the turn left button | turn left | Yes |
| right | Press the turn right button | turn right | Yes |
| forward | Press the turn accelerate button | None | Yes |
| reverse | Press the turn reverse button | None | Yes |


<a name="confirmation"></a>
## Valid confirmation prompt responses

***"Wouldn't a simple yes or no do?"***

Why yes, normally it would.  But due to the difficulty in VoiceAttack recognizing such a short input properly, I've added a number of alternatives. Hopefully you will never need this, but in case you do here's the list.

| Input | Alternatives | Notes |
| --- | --- | --- |
| complete | done, finished, stop now, stop there | Used when asked for next action in order to stop input loop. |
| yes | absolutely, affirmative, commit, confirm, please, please do, please save, positive, save, yeah, yes sir | Response when asked if you want to save the information. |
| no | abort, cancel, nah, negative, nevermind, nope  | Response when asked if you want to save the information. |
| restart | do over, start over | Cancel configuration so far and start over |


<a name="advancedFiring"></a>
## Firing multiple weapon groups at once

If you fire multiple weapon groups at once all the actions contained within will be added to a weapon firing queue. The individual fire groups will be processed as separate queues, with the system switching between them in priority.

For example, say you had "*Missile 3*" set to up fire slots 1, 2, 3, 4 and "*Counter 2*" set up to fire slots 7, 8, 9.

```
> Unload missile 3
> Unload counter 2
  # Expected result:
  #   Slot 1 fired   (From Missile 3)
  #   Slot 7 fired   (From Counter 2)
  #   Slot 2 fired   (From Missile 3)
  #   Slot 8 fired   (From Counter 2)
  #   Slot 3 fired   (From Missile 3)
  #   Slot 9 fired   (From Counter 2)
  #   Slot 4 fired   (From Missile 3)
  #   Slot 7 fired   (From Counter 2)
  #   Slot 1 fired   (From Missile 3)
  #   Slot 8 fired   (From Counter 2)
> Cancel counter 2
  #   Slot 2 fired   (From Missile 3)
  #   Slot 3 fired   (From Missile 3)
  #   Slot 4 fired   (From Missile 3)
  #   Slot 1 fired   (From Missile 3)
> Cease fire
```

Note that when firing multiple weapon groups you can cancel them individually. The remaining groups will continue to fire.

If you inject a pause in to a weapon group then that entire weapon group will be paused from processing for the number of seconds you specify. Other weapon groups in the queue will not be affected.

Take the above example, and assume "*Counter 2*" has a 3 second pause at the end. The result would look more like this...

```
> Unload missile 3
> Unload counter 2
  # Expected result:
  #   Slot 1 fired   (From Missile 3)
  #   Slot 7 fired   (From Counter 2)
  #   Slot 2 fired   (From Missile 3)
  #   Slot 8 fired   (From Counter 2)
  #   Slot 3 fired   (From Missile 3)
  #   Slot 9 fired   (From Counter 2)
  #   Slot 4 fired   (From Missile 3)
  #   Slot 1 fired   (From Missile 3)
  #   Slot 2 fired   (From Missile 3)
  #   Slot 3 fired   (From Missile 3)
  #   Slot 7 fired   (From Counter 2)
  #   Slot 4 fired   (From Missile 3)
  #   Slot 8 fired   (From Counter 2)
  #   Slot 1 fired   (From Missile 3)
  #   Slot 9 fired   (From Counter 2)
  #   Slot 2 fired   (From Missile 3)
  #   Slot 3 fired   (From Missile 3)
  #   Slot 4 fired   (From Missile 3)
> Cease fire
```
