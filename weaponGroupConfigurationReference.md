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
Dog Fighter
Mining
Minelayer
Battle
War
```

When selecting your current ship you can use just the ship name, or add ship to it.  "*Swap to battle*" and "*Swap to battle ship*" will both function the same.

<a name="definedGroups"></a>
## Default weapon groups

```
Counter
Missile
Reverse
Mine
Beam
Mining Laser
Beacon
Capitol
Highlight
```

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

Unlike normal weapon groups, emergency groups do not need the "fire" or "unlock" prefix, and they will always fire only once per command.


<a name="actionList"></a>
## List of actions available when configuring weapon groups.

During configuration you will be asked which weapon to fire. The following is a list of allowed inputs. When you're done adding weapons to the list use the "complete" command to move on.

| Input | Description | Alternatives |
| ----- | ----------- | ------------ |
| complete | End the input | done, stop here, finished |
| pause 1..10 | Pause processing this group | delay 1..10 |
| primary | Fire the primary weapon (once) | primary weapon |
| 1..10 | Fire the specified secondary weapon | slot 1..10, weapon slot 1..10 |
| augmentation | Press the augmentation key | augment |
| propulsion | Press the propulsion enhancer key | propulsion enhancer |
| radar | Bring up the ship's radar | activate radar, open radar, display radar |
| corkscrew | Perform a corkscrew | None |
| action | Press the action button | None |
| left | Press the turn left button | None |
| right | Press the turn left button | None |
| forward | Press the turn accelerate button | None |
| reverse | Press the turn reverse button | None |
| hold left | Press and hold the turn left button | None |
| hold right | Press and hold the turn left button | None |
| hold forward | Press and hold the turn accelerate button | None |
| hold reverse | Press and hold the turn reverse button | None |
| release left | Release the turn left button | None |
| release right | Release the turn left button | None |
| release forward | Release the turn accelerate button | None |
| release reverse | Release the turn reverse button | None |


<a name="confirmation"></a>
## Valid confirmation prompt responses

***"Wouldn't a simple yes or no do?"***

Why yes, it would.  But due to the difficulty in VoiceAttack recognizing such a short input properly, I've added a number of alternative inputs. Hopefully you will never need this, but in case you do here's the list.

| Input | Alternatives |
| --- | ---|
| yes | confirm, positive, affirmative, absolutely, please, please do, yeah, yes sir, commit, save, please save |
| no | nah, nope, negative, cancel, nevermind, abort |


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
