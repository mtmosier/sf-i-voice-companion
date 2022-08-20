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
		string response, request, shipName, last4;

		string[] cancelList = { "cancel", "nevermind", "never mind", "abort" };
		string[] restartList = { "restart", "start over", "do over" };
		string[] agreeList = { "confirm", "confirmed", "positive", "affirmative", "absolutely", "please", "please do", "yeah", "yes", "yessir", "yes sir", "commit" };
		string[] saveList = { "save", "please save" };

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


		shipName = null;
		while (true) {
			if (String.IsNullOrEmpty(shipName)) {
				request = "New ship name?";
				response = getUserInput(
					string.Join(";", cancelList) + ";"
					+ string.Join(";", restartList),  //*** inputOptionListStr
					request,  //*** playbackText
					null,  //*** playbackFileGroupName
					(headphonesInUse != true),  //*** pauseForPlayback
					false,  //*** shortPause
					allowFreeFormShipNames   //*** returnOnAnyInput
				);

				if (!string.IsNullOrEmpty(response)) {
					if (Array.IndexOf(cancelList, response) != -1) {
						VA.WriteToLog("Cancelled " + response, "Red");
						cancelConfiguration();
						return;
					}

					if (Array.IndexOf(restartList, response) != -1) {
						VA.WriteToLog("Restart new ship initialization.", "Orange");
						shipName = "";
						continue;
					}

					shipName = response;
				}
			}

			if (!String.IsNullOrEmpty(shipName)) {
				shipName = shipName.Trim();

				last4 = shipName.Substring(shipName.Length - 4);
				if (shipName.Length > 6 && comparer.Compare(last4, "ship") == 0) {
					shipName = shipName.Substring(0, shipName.Length - 4);
					shipName = shipName.Trim();
				}
				shipName = ti.ToTitleCase(shipName);
			}


			if (!String.IsNullOrEmpty(shipName)) {

				if (comparer.Compare(activeShipName, shipName) == 0) {
					//*** Make sure the ship is marked active and give feedback to the user
					playRandomSound("Switching to " + shipName + " ship.", null, true);

					VA.WriteToLog("Ship name given, " + shipName + ", is already the active ship.", "Orange");
					VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", true);
					VA.SetText(">>activeShipName", shipName);
					break;

				} else {

					VA.WriteToLog("Registering new ship, " + shipName, "Black");

					request = "Initializing " + shipName + " ship.";
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
							VA.WriteToLog("Restart new ship initialization.", "Orange");
							shipName = "";
							continue;
						}

						if (Array.IndexOf(agreeList, response) != -1 || Array.IndexOf(saveList, response) != -1) {
							VA.WriteToLog("Quick confirm new ship initialization.", "Black");
							playRandomSound("Confirmed", "Non-Verbal Confirmation", true);
						}
					}

					VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", true);
					VA.SetText(">>activeShipName", shipName);

					//*** Set list of ship names
					List<string> fullShipList = new List<string>();
					List<string> activeShipList = new List<string>();

					string variable = VA.GetText(">>shipNameListStr");
					if (!String.IsNullOrEmpty(variable)) {
						string[] shipNameList = variable.Split(';');
						foreach (string sn in shipNameList) {
							if (!fullShipList.Contains(sn))
								fullShipList.Add(sn);
							if (VA.GetBoolean(">>shipInfo[" + sn + "].isInUse") == true) {
								if (!activeShipList.Contains(sn)) {
									activeShipList.Add(sn);
								}
							}
						}
					}
					if (!fullShipList.Contains(shipName))
						fullShipList.Add(shipName);
					if (!activeShipList.Contains(shipName))
						activeShipList.Add(shipName);

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


					if (!writeSettings()) {
						configurationError();
					}
					reloadProfile();
					return;
				}
			}
		}
	}


	private string getUserInput(string inputOptionListStr, string playbackText, string playbackFileGroupName, bool pauseForPlayback = false, bool shortPause = true, bool returnOnAnyInput = false) {
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
//		playRandomSound("Configuration error. Cancelling", "General Error", false);
	}

	private void timeoutError() {
		playRandomSound("Configuration error. Timed out.", "General Error", false);
	}
}
