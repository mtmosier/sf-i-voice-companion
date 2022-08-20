using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class VAInline
{
	private bool headphonesInUse;

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

		string response, request;

		//*** Get list of ship names
		string[] shipNameList = {};
		string shipNameListStr = VA.GetText(">>shipNameListStr");
		if (!String.IsNullOrEmpty(shipNameListStr)) {
			shipNameList = shipNameListStr.Split(';');
		} else {
			configurationError();
			return;
		}

		//*** Confirm the action
		request = "Deleting all ship data.";
		response = getUserInput(
			string.Join(";", agreeList)
			+ ";" + string.Join(";", saveList)
			+ ";" + string.Join(";", cancelList),  //*** inputOptionListStr
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

			if (Array.IndexOf(agreeList, response) != -1 || Array.IndexOf(saveList, response) != -1) {
				VA.WriteToLog("Quick confirm all ship data deletion.", "Black");
				playRandomSound("Confirmed", "Non-Verbal Confirmation", true);
			}
		}


		//*** Set all ships to be no longer in use
		foreach (string shipName in shipNameList) {
			VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", false);
		}

		VA.SetText(">>activeShipName", "");

		if (!writeSettings()) {
			configurationError();
		}
		reloadProfile();
		return;
	}


	private void initialize()
	{
		//*** GET RELEVANT SETTINGS
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
