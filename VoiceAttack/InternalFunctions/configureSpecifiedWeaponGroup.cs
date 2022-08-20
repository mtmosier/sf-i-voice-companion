using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class VAInline
{
	private Guid playRandomSoundGuid;
	private Guid requestVerbalUserInputGuid;
	private Guid writeSettingsToFileGuid;
	private Guid reloadProfileGuid;
	private Guid registerNewWeaponGroupGuid;
	private bool restart = false;
	private TextInfo ti;

	public void main()
	{
		//*** INITIALIZE
		ti = CultureInfo.CurrentCulture.TextInfo;
		string groupName;
		string response, logMessage;
		string[] inputKeywordsToIgnore = new string[] { "turn", "activate", "weapon", "slot", "enhancer", "open", "display" };

		string[] agreeList = { "confirm", "confirmed", "positive", "affirmative", "absolutely", "please", "please do", "yeah", "yes", "yessir", "yes sir", "commit" };
		string[] saveList = { "save", "please save" };
		string[] disagreeList = { "no", "nah", "nope", "negative" };
		string[] cancelList = { "cancel", "nevermind", "never mind", "abort" };
		string[] restartList = { "restart", "start over", "do over" };
		string[] inputCompleteList = { "stop here", "stop there", "done", "finished", "complete", "completed" };


		//*** GET RELEVANT SETTINGS
		string activeShipName = VA.GetText(">>activeShipName");
		bool? headphonesInUse = VA.GetBoolean(">>headphonesInUse");

		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  playRandomSoundGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">requestVerbalUserInputCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  requestVerbalUserInputGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">writeSettingsToFileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  writeSettingsToFileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">reloadProfileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  reloadProfileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">registerNewWeagonGroupCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  registerNewWeaponGroupGuid = new Guid(tmpCmdId);

		//*** Static Group List
		string variable = VA.GetText(">>staticGroupList");
		List<string> staticGroupList = new List<string>(variable.Split(';'));


		//*** Get parameters
		bool? quickConfig = VA.GetBoolean("~~quickConfig");
		bool? newGroupConfig = VA.GetBoolean("~~newGroupConfig");

		groupName = VA.GetText("~~wgGroupNameExtStr");
		if (String.IsNullOrEmpty(groupName)) {
			groupName = VA.Command.Segment(1);
		}

		if (String.IsNullOrEmpty(groupName)) {
			playRandomSound(null, "Non-Verbal Error", true);
			//runRegisterWeaponGroup();  //*** Errors due to multiple commands running at once
			return;
		}

		groupName = ti.ToTitleCase(groupName);


		VA.WriteToLog("Configuring group: " + groupName, "Green");


		//*** START PLAYER INTERACTION
		string tmpVarName = ">>shipInfo[" + activeShipName + "].weaponGroup[" + groupName + "]";
		int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");

		if (!restart && VA.GetBoolean(tmpVarName + ".isActive") == true && lenN.HasValue && lenN.Value > 0) {
			bool? extendedWeaponConfigPlayed = VA.GetBoolean(">>extendedWeaponConfigPlayed");
			string voiceGroupName = "Weapon Group Already Configured";
			string msg = groupName + " ";
			msg += "is already configured.";

			if (!extendedWeaponConfigPlayed.HasValue || extendedWeaponConfigPlayed.Value == false) {
				msg += " Say abort to cancel configuration at any prompt.";
				VA.SetBoolean(">>extendedWeaponConfigPlayed", true);
			}
			playRandomSound(msg, voiceGroupName, true);
		} else if (quickConfig != true && newGroupConfig != true) {
			playRandomSound(null, "Configure Weapon Group", true);
		}


		string request = "";

		List<string> keyPressList = new List<string>();
		List<string> weaponList = new List<string>();
		bool exitLoop = false;
		int i = 0;

		while (!exitLoop) {
			string keyPress, weapon;
			string[] responseList;
			bool hold = false, release = false, pause = false;
			i++;

			if (i == 1) {
				if (quickConfig != true) {
					request += "Which weapon do you want to activate first?";
				} else {
					request += VA.GetText(">ordList(" + i + ")") + " weapon?";
				}
			} else {
				request = VA.GetText(">ordList(" + i + ")") + "?";
			}

			response = getUserInput(
				"[weapon;] [slot;] [0..10;ate;eighths];"
				+ "[hold;release;] [augmentation;augment];"
				+ "[hold;release;] propulsion [enhancer;];"
				+ "[hold;release;] primary [weapon;];"
				+ "[hold;release;] [left;turn left;left turn;right;turn right;right turn;forward;reverse];"
				+ "[hold;release;] fine aiming;"
				+ "[hold;release;] corkscrew;"
				+ "[hold;release;] action;"
				+ "[activate;open;display;] radar;"
				+ "[pause;delay] [1..60;ate;eighths];"
				+ string.Join(";", restartList) + ";"
				+ string.Join(";", cancelList) + ";"
				+ string.Join(";", inputCompleteList),
				request,
				null,
				(headphonesInUse != true)
			);


			if (string.IsNullOrEmpty(response)) {
				timeoutError();
				return;
			}
			// VA.WriteToLog("Heard " + response, "Red");

			if (Array.IndexOf(cancelList, response) != -1) {
				VA.WriteToLog("Cancelled " + response, "Red");
				cancelConfiguration();
				return;
			}

			if (Array.IndexOf(restartList, response) != -1) {
				VA.WriteToLog("Restart weapon group configuration.", "Orange");
				playRandomSound("Restarting configuration", "Restart Configuration", true);
				restart = true;
				main();
				return;
			}

			responseList = response.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			response = "";
			foreach (string loopVal in responseList) {
				if (loopVal == "pause" || loopVal == "delay") {
					pause = true;
				}

				if (loopVal == "hold") {
					hold = true;
				} else if (loopVal == "release") {
					release = true;
				} else if (Array.IndexOf(inputKeywordsToIgnore, response) == -1) {
					response += loopVal + " ";
				}
			}
			response = response.Trim();

			if (Array.IndexOf(inputCompleteList, response) == -1) {
				if (pause) {

					response = response.Replace("eighths", "8");
					response = response.Replace("ate", "8");
					response = Regex.Replace(response, "[^0-9]", "");

					keyPress = "Pause " + response;
					weapon = "Pause for " + response + " second";
					if (response != "1") weapon += "s";

				} else {

					switch (response) {
			            case "augmentation":
			            case "augment":
							keyPress = "UseAugmentation";
							weapon = "augmentation";
			                break;
			            case "action":
							keyPress = "Action";
							weapon = "perform action";
			                break;
						case "corkscrew":
							keyPress = "Corkscrew";
							weapon = "corkscrew";
			                break;
			            case "radar":
							keyPress = "Radar";
							weapon = "open radar";
			                break;
						case "forward":
							keyPress = "Accelerate";
							weapon = "forward";
			                break;
						case "reverse":
							keyPress = "Reverse";
							weapon = "reverse";
			                break;
						case "left":
							keyPress = "TurnLeft";
							weapon = "turn left";
			                break;
			            case "right":
							keyPress = "TurnRight";
							weapon = "turn right";
			                break;
			            case "propulsion":
							keyPress = "UsePropulsionEnhancer";
							weapon = "propulsion enhancer";
			                break;
						case "primary":
							keyPress = "PrimaryFire";
							weapon = "primary weapon";
			                break;
						case "fine aiming":
							keyPress = "FineAiming";
							weapon = "fine aiming";
			                break;
			            default:
							response = response.Replace("eighths", "8");
							response = response.Replace("ate", "8");
							response = response.Replace("10", "0");

							keyPress = "Slot" + Regex.Replace(response, "[^0-9]", "");
							weapon = keyPress.Replace("Slot", "Slot ");
			                break;
			       	}

				}

				if (!String.IsNullOrEmpty(keyPress)) {
					if (hold) {
						keyPress = "Hold " + keyPress;
						weapon = "hold " + weapon;
					}
					if (release) {
						keyPress = "Release " + keyPress;
						weapon = "release " + weapon;
					}

					keyPressList.Add(keyPress);
					weaponList.Add(weapon);
				}

			} else {
				exitLoop = true;
			}
		}

		if (keyPressList.Count == 0) {
			cancelConfiguration();
			return;
		}

		if (quickConfig != true) {
			if (staticGroupList.Contains(groupName)) {
				request = groupName + " will fire ";
			} else {
				request = "Group " + groupName + " will fire ";
			}
			request += string.Join(", ", weaponList);
			request += ". Do you want to save these settings?";
		} else {
			if (staticGroupList.Contains(groupName)) {
				request = "Save " + groupName + "?";
			} else {
				request = "Save group " + groupName + "?";
			}
 		}

		response = getUserInput(
			string.Join(";", agreeList)
				+ ";" + string.Join(";", saveList)
				+ ";" + string.Join(";", restartList)
				+ ";" + string.Join(";", disagreeList)
				+ ";" + string.Join(";", cancelList),
			request,
			null,
			(headphonesInUse != true)
		);

		if (string.IsNullOrEmpty(response)) {
			timeoutError();
			return;
		}
		if (Array.IndexOf(cancelList, response) != -1 || Array.IndexOf(disagreeList, response) != -1) {
			cancelConfiguration();
			return;
		}
		if (Array.IndexOf(restartList, response) != -1) {
			VA.WriteToLog("Restart weapon group configuration.", "Orange");
			playRandomSound("Restarting configuration", "Restart Configuration", true);
			restart = true;
			main();
			return;
		}

		if (staticGroupList.Contains(groupName))  logMessage = "Saving " + groupName;
		else  logMessage = "Saving group " + groupName;
		VA.WriteToLog(logMessage, "Blue");

		string wgDefVarName = ">>shipInfo["+activeShipName+"].weaponGroup["+groupName+"]";

		VA.SetBoolean(">>shipInfo[" + activeShipName + "].isInUse", true);
		VA.SetBoolean(wgDefVarName+".isActive", true);
		VA.SetInt(wgDefVarName+".weaponKeyPress.len", keyPressList.Count);

		i = 0;
		foreach (string keyPress in keyPressList) {
			VA.SetText(wgDefVarName+".weaponKeyPress[" + (i) + "]", keyPress);
			VA.SetText(wgDefVarName+".weaponKeyPressFriendly[" + (i) + "]", weaponList[i]);
			i++;
		}

		List<string> activeWeaponGroupList = readWeaponGroupList(true);
		if (!activeWeaponGroupList.Contains(groupName)) {
			activeWeaponGroupList.Add(groupName);
			saveWeaponGroupList(activeWeaponGroupList, true);
		}

		List<string> fullWeaponGroupList = readWeaponGroupList(false);
		if (!fullWeaponGroupList.Contains(groupName) && !staticGroupList.Contains(groupName)) {
			fullWeaponGroupList.Add(groupName);
			saveWeaponGroupList(fullWeaponGroupList, false);
		}

		if (writeSettings()) {
			playRandomSound("Configuration [saved;complete]", "Configuration Complete");
			reloadProfile();
		} else {
			configurationError();
		}
	}




	private int getIntFromStr(string input, int defaultValue = -1)
	{
		if (input == null || input == "") {
			return defaultValue;
		} else {
			try { return Convert.ToInt32(input); }
			catch (FormatException) { return defaultValue; }
			catch (OverflowException) { return defaultValue; }
		}
	}

	private string getUserInput(string inputOptionListStr, string playbackText, string playbackFileGroupName, bool pauseForPlayback = false) {
		if (requestVerbalUserInputGuid == null)  return "";

		VA.SetText("~~viInputOptionString", inputOptionListStr);
		VA.SetText("~~viPlaybackText", playbackText);
		VA.SetText("~~viPlaybackFileGroupName", playbackFileGroupName);
		VA.SetBoolean("~~viPauseForPlayback", pauseForPlayback);
		VA.Command.Execute(requestVerbalUserInputGuid, true, true);

		return VA.GetText("~~viUserResponse");
	}

	private void playRandomSound(string playbackText, string playbackFileGroupName, bool pauseForPlayback = true) {
		if (playRandomSoundGuid == null)  return;

		VA.SetText("~~rsText", playbackText);
		VA.SetText("~~rsFileGroupName", playbackFileGroupName);
		VA.Command.Execute(playRandomSoundGuid, pauseForPlayback, true);
	}

	private bool writeSettings() {
		if (writeSettingsToFileGuid == null)  return false;
		VA.Command.Execute(writeSettingsToFileGuid, true, true);
		return true;
	}

	private void cancelConfiguration() {
		playRandomSound("Cancelling Configuration", "Cancel", true);
	}

	private void configurationError() {
		cancelConfiguration();
//		playRandomSound("Configuration error. Cancelling", "General Error", true);
	}

	private void runRegisterWeaponGroup() {
		if (registerNewWeaponGroupGuid == null)  return;
		VA.Command.Execute(registerNewWeaponGroupGuid, false, false);
	}

	private void reloadProfile() {
		if (reloadProfileGuid == null)  return;
		VA.Command.Execute(reloadProfileGuid, false, false);
	}

	private void timeoutError() {
		playRandomSound("Configuration error. Timed out.", "General Error", true);
	}

	private List<string> readWeaponGroupList(bool activeGroups = false)
	{
		string variable;
		if (activeGroups)	variable = VA.GetText(">>activeWeaponGroupList");
		else				variable = VA.GetText(">>weaponGroupListStr");

		List<string> weaponGroupList = new List<string>();

		if (!String.IsNullOrEmpty(variable)) {
			string[] wgList = variable.Split(';');
			foreach (string wg in wgList) {
				string group = ti.ToTitleCase(wg.Trim());
				if (!weaponGroupList.Contains(group))
					weaponGroupList.Add(group);
			}
		}

		return weaponGroupList;
	}

	private void saveWeaponGroupList(List<string> weaponGroupList, bool activeGroups = false)
	{
		if (activeGroups) {
			VA.SetText(">>activeWeaponGroupList", String.Join(";", weaponGroupList));
			VA.SetText(">>activeWeaponGroupInput", "[" + String.Join(";", weaponGroupList) + "]");
		} else {
			VA.SetText(">>weaponGroupListStr", String.Join(";", weaponGroupList));
		}
	}
}
