// Microsoft.CSharp.dll;System.dll;System.Core.dll;System.Data.dll;System.Data.DataSetExtensions.dll;System.Deployment.dll;System.Drawing.dll;System.Net.Http.dll; System.Windows.Forms.dll;System.Xml.dll;System.Xml.Linq.dll

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class VAInline
{
	private bool headphonesInUse;
	private string activeShipName;
	private StringComparer comparer;
	private TextInfo ti;

	//*** References to other VA Commands
	private Guid requestVerbalUserInputGuid;
	private Guid writeSettingsToFileGuid;
	private Guid reloadProfileGuid;
	private Guid playRandomSoundGuid;

	//*** Valid responses
	private string[] cancelList = { "cancel", "nevermind", "never mind", "abort" };
	private string[] restartList = { "restart", "start over", "do over" };
	private string[] agreeList = { "confirm", "confirmed", "positive", "affirmative", "absolutely", "please", "please do", "yeah", "yes", "yessir", "yes sir", "commit" };
	private string[] saveList = { "save", "please save" };


	public void main()
	{
		//*** Initialize
		initialize();

		string response, request, shipName, last4;
		bool? bVar;

		//*** Get list of ship names
		List<string> activeShipList = new List<string>();

		string shipNameInputStr = VA.GetText(">>shipNameListStr");
		if (!String.IsNullOrEmpty(shipNameInputStr)) {
			string[] shipNameList = shipNameInputStr.Split(';');
			foreach (string sn in shipNameList) {
				if (VA.GetBoolean(">>shipInfo[" + sn + "].isInUse") == true) {
					if (!activeShipList.Contains(sn)) {
						activeShipList.Add(sn);
					}
				}
			}

			shipNameInputStr = String.Format("[{0};active;current] [ship;];", shipNameInputStr);
		}

		//*** Weapon group variables
		string wgNameListStr = VA.GetText(">>weaponGroupListStr");
		List<string> wgNameList = new List<string>(wgNameListStr.Split(';'));

		//*** Get static group list
		string variable = VA.GetText(">>staticGroupList");
		if (string.IsNullOrEmpty(variable))  variable = "";
		List<string> staticGroupList = new List<string>(variable.Split(';'));

		wgNameList.AddRange(staticGroupList);

		//*** Get the ship name given
		shipName = VA.Command.Segment(4);

		while (true) {
			if (String.IsNullOrEmpty(shipName)) {
				request = "Which ship do you want to delete?";
				response = getUserInput(
					shipNameInputStr
					+ string.Join(";", cancelList) + ";"
					+ string.Join(";", restartList),  //*** inputOptionListStr
					request,  //*** playbackText
					null,  //*** playbackFileGroupName
					(headphonesInUse != true),  //*** pauseForPlayback
					false, //*** shortPause
					false   //*** returnOnAnyInput
				);

				if (!string.IsNullOrEmpty(response)) {
					if (Array.IndexOf(cancelList, response) != -1) {
						VA.WriteToLog("Cancelled " + response, "Red");
						cancelConfiguration();
						return;
					}

					if (Array.IndexOf(restartList, response) != -1) {
						VA.WriteToLog("Restart ship deletion.", "Orange");
						shipName = "";
						continue;
					}

					shipName = response;
				}
			}

			if (!String.IsNullOrEmpty(shipName)) {
				shipName = shipName.Trim();

				if (shipName.Length > 6) {
					last4 = shipName.Substring(shipName.Length - 4);
					if (comparer.Compare(last4, "ship") == 0) {
						shipName = shipName.Substring(0, shipName.Length - 4);
						shipName = shipName.Trim();
					}
				}

				if (comparer.Compare(shipName, "current") == 0 || comparer.Compare(shipName, "active") == 0) {
					shipName = activeShipName;
				}

				shipName = ti.ToTitleCase(shipName);
			}

			if (!String.IsNullOrEmpty(shipName)) {

				VA.WriteToLog("Deleting ship, " + shipName, "Black");

				request = "Deleting " + shipName + " ship.";
				response = getUserInput(
					string.Join(";", agreeList)
					+ ";" + string.Join(";", saveList)
					+ ";" + string.Join(";", cancelList)
					+ ";" + string.Join(";", restartList),  //*** inputOptionListStr
					request, //*** playbackText
					null,    //*** playbackFileGroupName
					(headphonesInUse != true)  //*** pauseForPlayback
				);

				if (!string.IsNullOrEmpty(response)) {
					if (Array.IndexOf(cancelList, response) != -1) {
						VA.WriteToLog("Cancelled " + response, "Red");
						cancelConfiguration();
						return;
					}

					if (Array.IndexOf(restartList, response) != -1) {
						VA.WriteToLog("Restart ship deletion.", "Orange");
						shipName = "";
						continue;
					}

					if (Array.IndexOf(agreeList, response) != -1 || Array.IndexOf(saveList, response) != -1) {
						VA.WriteToLog("Quick confirm ship deletion.", "Black");
						playRandomSound("Confirmed", "Non-Verbal Confirmation", true);
					}
				}


				//*** Set the ship to be no longer in use
				VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", false);

				for (short w = 0; w < wgNameList.Count; w++) {
					string tmpVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + wgNameList[w] + "]";
					string settingName = tmpVarName + ".isActive";
					VA.SetBoolean(settingName, false);

					settingName = tmpVarName + ".weaponKeyPress.len";
					VA.SetInt(settingName, 0);
				}


				//*** Remove from the locally stored ship list
				if (activeShipList.Contains(shipName))
					activeShipList.Remove(shipName);

				//*** Update the active ship name if current ship was deleted
				if (comparer.Compare(activeShipName, shipName) == 0) {
					bool found = false;
					string firstShip = "";

					foreach (string sn in activeShipList) {
						if (String.IsNullOrEmpty(firstShip))
							firstShip = sn;

						bVar = VA.GetBoolean(">>shipInfo[" + sn + "].isInUse");
						if (bVar == true) {
							VA.SetText(">>activeShipName", sn);
							VA.WriteToLog("Active ship set to " + sn, "Orange");
							found = true;
							break;
						}
					}

					if (!found) {
						VA.SetText(">>activeShipName", "");
						if (!String.IsNullOrEmpty(firstShip)) {
							VA.SetText(">>activeShipName", firstShip);
							VA.SetBoolean(">>shipInfo[" + firstShip + "].isInUse", true);
							VA.WriteToLog("Active ship set to " + firstShip, "Orange");
							found = true;
						}
					}
				}

				if (!writeSettings()) {
					configurationError();
				}
				reloadProfile();
				return;
			}
		}
	}


	private void initialize()
	{
		comparer = StringComparer.CurrentCultureIgnoreCase;
		ti = CultureInfo.CurrentCulture.TextInfo;

		//*** GET RELEVANT SETTINGS
		activeShipName = VA.GetText(">>activeShipName");

		bool? bVar = VA.GetBoolean(">>headphonesInUse");
		headphonesInUse = bVar.HasValue ? bVar.Value : false;

		string tmpCmdId = VA.GetText(">requestVerbalUserInputCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  requestVerbalUserInputGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">writeSettingsToFileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  writeSettingsToFileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">reloadProfileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  reloadProfileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  playRandomSoundGuid = new Guid(tmpCmdId);
	}

	private string getUserInput(string inputOptionListStr, string playbackText, string playbackFileGroupName, bool pauseForPlayback = false, bool shortPause = true, bool returnOnAnyInput = false) {
		if (requestVerbalUserInputGuid == null)  return "";

		VA.SetText("~~viInputOptionString", inputOptionListStr);
		VA.SetText("~~viPlaybackText", playbackText);
		VA.SetText("~~viPlaybackFileGroupName", playbackFileGroupName);
		VA.SetBoolean("~~viPauseForPlayback", pauseForPlayback);
		VA.SetBoolean("~~viVeryShortInputPause", shortPause);
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
//		playRandomSound("Configuration error. Cancelling", "General Error", false);
	}

	private void timeoutError() {
		playRandomSound("Configuration error. Timed out.", "General Error", false);
	}
}
