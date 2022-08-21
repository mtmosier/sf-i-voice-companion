// Microsoft.CSharp.dll;System.dll;System.Core.dll;System.Data.dll;System.Data.DataSetExtensions.dll;System.Deployment.dll;System.Drawing.dll;System.Net.Http.dll; System.Windows.Forms.dll;System.Xml.dll;System.Xml.Linq.dll

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class VAInline
{
	private Guid requestVerbalUserInputGuid;
	private Guid writeSettingsToFileGuid;
	private Guid reloadProfileGuid;
	private Guid playRandomSoundGuid;

	public void main()
	{
		//*** INITIALIZE
		StringComparer comparer = StringComparer.CurrentCultureIgnoreCase;
		TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
		string response, request, last4, variable;
		string fromShip, toShip = "";
		bool? boolValueN;

		string[] cancelList = { "cancel", "nevermind", "never mind", "abort" };
		string[] restartList = { "restart", "start over", "do over" };
		string[] agreeList = { "confirm", "confirmed", "positive", "affirmative", "absolutely", "please", "please do", "yeah", "yes", "yessir", "yes sir", "commit" };
		string[] saveList = { "save", "please save" };
		string[] disagreeList = { "no", "nah", "nope", "negative" };

		//*** GET RELEVANT SETTINGS
		string activeShipName = VA.GetText(">>activeShipName");

		bool? bVar = VA.GetBoolean(">>allowFreeFormShipNames");
		bool allowFreeFormShipNames = bVar.HasValue ? bVar.Value : false;

		bVar = VA.GetBoolean(">>headphonesInUse");
		bool headphonesInUse = bVar.HasValue ? bVar.Value : false;

		string tmpCmdId = VA.GetText(">requestVerbalUserInputCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  requestVerbalUserInputGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">writeSettingsToFileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  writeSettingsToFileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">reloadProfileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  reloadProfileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  playRandomSoundGuid = new Guid(tmpCmdId);


		//*** Get list of Weapon group names
		variable = VA.GetText(">>weaponGroupListStr");
		if (string.IsNullOrEmpty(variable))  variable = "";
		List<string> weaponGroupList = new List<string>(variable.Split(';'));

		//*** Get static group list
		variable = VA.GetText(">>staticGroupList");
		if (string.IsNullOrEmpty(variable))  variable = "";
		List<string> staticGroupList = new List<string>(variable.Split(';'));

		weaponGroupList.AddRange(staticGroupList);



		//*** Get list of ship names
		List<string> fullShipList = new List<string>();
		List<string> activeShipList = new List<string>();

		string shipNameInputStr = VA.GetText(">>shipNameListStr");
		if (!String.IsNullOrEmpty(shipNameInputStr)) {
			string[] shipNameList = shipNameInputStr.Split(';');
			foreach (string sn in shipNameList) {
				if (!fullShipList.Contains(sn))
					fullShipList.Add(sn);
				if (VA.GetBoolean(">>shipInfo[" + sn + "].isInUse") == true) {
					if (!activeShipList.Contains(sn)) {
						activeShipList.Add(sn);
					}
				}
			}

			shipNameInputStr = String.Format("[{0};active;current] [ship;];", shipNameInputStr);
		}


		//*** Get source ship name
		fromShip = VA.Command.Segment(2);
		fromShip = fromShip.Trim();

		if (fromShip.Length > 6) {
			last4 = fromShip.Substring(fromShip.Length - 4);
			if (comparer.Compare(last4, "ship") == 0) {
				fromShip = fromShip.Substring(0, fromShip.Length - 4);
				fromShip = fromShip.Trim();
			}
		}

		if (comparer.Compare(fromShip, "current") == 0 || comparer.Compare(fromShip, "active") == 0) {
			fromShip = activeShipName;
		}

		fromShip = ti.ToTitleCase(fromShip);

		//*** Check whether the source ship is in use
		boolValueN = VA.GetBoolean(">>shipInfo[" + fromShip + "].isInUse");
		bool fromShipInUse = boolValueN.HasValue ? boolValueN.Value : false;

		if (!fromShipInUse) {
			playRandomSound(fromShip + " ship is not configured.", null, true);
			cancelConfiguration();
			return;
		}


		while (true) {
			toShip = "";

			request = "New ship name?";
			response = getUserInput(
				shipNameInputStr
				+ string.Join(";", cancelList) + ";"
				+ string.Join(";", restartList),  //*** inputOptionListStr
				request,  //*** playbackText
				null,  //*** playbackFileGroupName
				(headphonesInUse != true), //*** pauseForPlayback
				false, //*** shortPause
				allowFreeFormShipNames   //*** returnOnAnyInput
			);

			if (!string.IsNullOrEmpty(response)) {
				if (Array.IndexOf(cancelList, response) != -1) {
					cancelConfiguration();
					return;
				}

				if (Array.IndexOf(restartList, response) != -1) {
					VA.WriteToLog("Restart rename ship.", "Orange");
					continue;
				}

				toShip = response;
			}


			if (!String.IsNullOrEmpty(toShip)) {
				toShip = toShip.Trim();

				if (toShip.Length > 6) {
					last4 = toShip.Substring(toShip.Length - 4);
					if (comparer.Compare(last4, "ship") == 0) {
						toShip = toShip.Substring(0, toShip.Length - 4);
						toShip = toShip.Trim();
					}
				}

				if (comparer.Compare(toShip, "current") == 0 || comparer.Compare(toShip, "active") == 0) {
					toShip = activeShipName;
				}

				toShip = ti.ToTitleCase(toShip);
			}


			if (!String.IsNullOrEmpty(toShip)) {

				if (comparer.Compare(fromShip, toShip) == 0) {
					playRandomSound("Cannot copy " + toShip + " to itself.", null, true);
					continue;

				} else {

					//*** Check whether the destination ship is in use
					boolValueN = VA.GetBoolean(">>shipInfo[" + toShip + "].isInUse");
					bool toShipInUse = boolValueN.HasValue ? boolValueN.Value : false;


					//*** Confirm the user really wants to perform the copy
					request = "Are you sure you want to rename " + fromShip + " ship to " + toShip + " ship?";
					if (toShipInUse)  request += " This will replace the configuration currently in " + toShip + " ship";
					response = getUserInput(
						string.Join(";", agreeList)
							+ ";" + string.Join(";", disagreeList)
							+ ";" + string.Join(";", restartList)
							+ ";" + string.Join(";", cancelList),  //*** inputOptionListStr
						request,  //*** playbackText
						null,  //*** playbackFileGroupName
						(headphonesInUse != true)  //*** pauseForPlayback
					);


					if (string.IsNullOrEmpty(response)) {
						continue;
					}

					if (Array.IndexOf(cancelList, response) != -1 || Array.IndexOf(disagreeList, response) != -1) {
						cancelConfiguration();
						return;
					}

					if (Array.IndexOf(restartList, response) != -1) {
						VA.WriteToLog("Restart rename ship.", "Orange");
						continue;
					}


					//*** Perform ship rename
					VA.SetBoolean(">>shipInfo[" + fromShip + "].isInUse", false);
					VA.SetBoolean(">>shipInfo[" + toShip + "].isInUse", true);

					foreach (string wgName in weaponGroupList) {
						string fromVarName = ">>shipInfo[" + fromShip + "].weaponGroup[" + wgName + "]";
						string toVarName = ">>shipInfo[" + toShip + "].weaponGroup[" + wgName + "]";

						bool wgIsActive = false;
						string settingName = fromVarName + ".isActive";
						boolValueN = VA.GetBoolean(settingName);
						if (boolValueN.HasValue)  wgIsActive = boolValueN.Value;

						VA.SetBoolean(fromVarName + ".isActive", false);
						VA.SetBoolean(toVarName + ".isActive", wgIsActive);

						if (wgIsActive) {
							int wgLen = 0;
							settingName = fromVarName + ".weaponKeyPress.len";
							int? intValueN = VA.GetInt(settingName);
							if (intValueN.HasValue)  wgLen = intValueN.Value;

							VA.SetInt(toVarName + ".weaponKeyPress.len", wgLen);

							for (short l = 0; l < wgLen; l++) {
								settingName = fromVarName + ".weaponKeyPress[" + l + "]";
								string keybind = VA.GetText(settingName);
								VA.SetText(toVarName + ".weaponKeyPress[" + l + "]", keybind);

								settingName = fromVarName + ".weaponKeyPressFriendly[" + l + "]";
								keybind = VA.GetText(settingName);
								VA.SetText(toVarName + ".weaponKeyPressFriendly[" + l + "]", keybind);
							}
						}
					}

					VA.SetText(">>activeShipName", toShip);

					if (!fullShipList.Contains(toShip))
						fullShipList.Add(toShip);
					if (fullShipList.Contains(fromShip))
						fullShipList.Remove(fromShip);

					if (!activeShipList.Contains(toShip))
						activeShipList.Add(toShip);
					if (activeShipList.Contains(fromShip))
						activeShipList.Remove(fromShip);

					//*** Export ship list settings
					string shipNameInput = "[" + string.Join<string>(";", fullShipList) + "] [ship;]";
					VA.SetText(">>configShipNameInput", shipNameInput);

					if (fullShipList.Count > 0)		shipNameInput = "[" + string.Join<string>(";", fullShipList) + ";current;active] [ship;]";
					else							shipNameInput = "[current;active] [ship;]";
					VA.SetText(">>shipNameInput", shipNameInput);

					if (activeShipList.Count > 0)	shipNameInput = "[" + string.Join<string>(";", activeShipList) + ";current;active] [ship;]";
					else							shipNameInput = "[current;active] [ship;]";
					VA.SetText(">>activeShipNameInput", shipNameInput);

					VA.SetText(">>shipNameListStr", string.Join<string>(";", fullShipList));


					if (writeSettings()) {
						playRandomSound("Rename complete", "Configuration Complete");
						reloadProfile();
					} else {
						playRandomSound("Problem writing ship settings", "Settings Write Error");
					}

					break;
				}
			}
		}
	}


	private string getUserInput(string inputOptionListStr, string playbackText, string playbackFileGroupName, bool pauseForPlayback = false, bool shortPause = false, bool returnOnAnyInput = false) {
		if (requestVerbalUserInputGuid == null)  return "";

		VA.SetText("~~viInputOptionString", inputOptionListStr);
		VA.SetText("~~viPlaybackText", playbackText);
		VA.SetText("~~viPlaybackFileGroupName", playbackFileGroupName);
		VA.SetBoolean("~~viPauseForPlayback", pauseForPlayback);
		VA.SetBoolean("~~viShortInputPause", shortPause);
		VA.SetBoolean("~~viReturnOnAnyInput", returnOnAnyInput);
		VA.Command.Execute(requestVerbalUserInputGuid, true, true);

		return VA.GetText("~~viUserResponse");
	}

	private void playRandomSound(string playbackText, string playbackFileGroupName, bool pauseForPlayback = true) {
		if (playRandomSoundGuid == null)  return;

		VA.SetText("~~rsText", playbackText);
		VA.SetText("~~rsFileGroupName", playbackFileGroupName);
		VA.Command.Execute(playRandomSoundGuid, pauseForPlayback, true);
	}

	private void reloadProfile() {
		if (reloadProfileGuid == null)  return;
		VA.Command.Execute(reloadProfileGuid, false, false);
	}

	private bool writeSettings() {
		if (writeSettingsToFileGuid == null)  return false;
		VA.Command.Execute(writeSettingsToFileGuid, true, true);
		return true;
	}

	private void cancelConfiguration() {
		playRandomSound("Cancelling Configuration", "Cancel", false);
	}

	private void configurationError() {
		cancelConfiguration();
	}

	private void timeoutError() {
		playRandomSound("Configuration error. Timed out.", "General Error", false);
	}
}
