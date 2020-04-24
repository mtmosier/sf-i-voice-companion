using System;
using System.Globalization;
using System.Text.RegularExpressions;

public class VAInline
{
	private Guid playRandomSoundGuid;
	private Guid requestVerbalUserInputGuid;
	private Guid writeSettingsToFileGuid;

	private void cancelCommand() {
		playRandomSound("Cancelling copy", "Cancel", false);
	}

	private void timeoutError() {
		playRandomSound("Timed out", "General Error", false);
	}

	public void main()
	{
		//*** INITIALIZE
		TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
		string[] agreeList = { "confirm", "positive", "affirmative", "absolutely", "please", "please do", "yeah", "yes", "yessir", "yes sir", "commit" };
		string[] saveList = { "save", "please save" };
		string[] disagreeList = { "no", "nah", "nope", "negative" };
		string[] cancelList = { "cancel", "stop", "nevermind", "abort" };
		string request, response, settingName, keybind;

		string fromShip, fromVarName;
		string toShip, toVarName;
		bool toShipInUse, fromShipInUse;
		int? intValueN;
		bool? boolValueN;

		//*** GET PARAMETERS
		fromShip = VA.Command.Segment(1);
		toShip = VA.Command.Segment(4);

		//*** GET RELEVANT SETTINGS
		string activeShipName = VA.GetText(">>activeShipName");
		bool? headphonesInUse = VA.GetBoolean(">>headphonesInUse");

		//*** Number of weapon groups in use
		int? maxWGNumN = VA.GetInt(">maxWeaponGroupNum");
		int maxWGNum = maxWGNumN.HasValue ? maxWGNumN.Value : 9;

		//*** Ship variables
		string variable = VA.GetText(">>weaponGroupNameList");
		string[] wgNameList = variable.Split(';');

		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  playRandomSoundGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">requestVerbalUserInputCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  requestVerbalUserInputGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">writeSettingsToFileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  writeSettingsToFileGuid = new Guid(tmpCmdId);


		//*** Special scenario for fromShip
		if (
			fromShip.Equals("active", StringComparison.OrdinalIgnoreCase)
			|| fromShip.Equals("current", StringComparison.OrdinalIgnoreCase)
		) {
			fromShip = activeShipName;
		}

		//*** Special scenario for fromShip
		if (
			toShip.Equals("active", StringComparison.OrdinalIgnoreCase)
			|| toShip.Equals("current", StringComparison.OrdinalIgnoreCase)
		) {
			toShip = activeShipName;
		}


		//*** Make sure we can process the command as requested
		if (toShip.Equals(fromShip, StringComparison.OrdinalIgnoreCase)) {
			playRandomSound("Cannot copy " + fromShip + " ship to itself.", null, true);
			cancelCommand();
			return;
		}


		//*** Check whether the souce ship is in use
		boolValueN = VA.GetBoolean(">>shipInfo[" + fromShip + "].isInUse");
		fromShipInUse = boolValueN.HasValue ? boolValueN.Value : false;

		if (!fromShipInUse) {
			playRandomSound(fromShip + " ship is not configured.", null, true);
			cancelCommand();
			return;
		}


		//*** Check whether the destination ship is in use
		boolValueN = VA.GetBoolean(">>shipInfo[" + toShip + "].isInUse");
		toShipInUse = boolValueN.HasValue ? boolValueN.Value : false;


		//*** Confirm the user really wants to perform the copy
		request = "Are you sure you want to copy " + fromShip + " ship data to " + toShip + " ship?";
		if (toShipInUse)  request += " This will replace the configuration currently in " + toShip + " ship";
		response = getUserInput(
			string.Join(";", agreeList)
				+ ";" + string.Join(";", disagreeList)
				+ ";" + string.Join(";", cancelList),
			request,
			null,
			false
		);


		if (string.IsNullOrEmpty(response)) {
			timeoutError();
			return;
		}

		if (Array.IndexOf(cancelList, response) != -1 || Array.IndexOf(disagreeList, response) != -1) {
			cancelCommand();
			return;
		}


		//*** Copy the ship information
		VA.SetBoolean(">>shipInfo[" + toShip + "].isInUse", true);

		for (short w = 0; w < wgNameList.Length; w++) {
			for (short n = 1; n <= maxWGNum; n++) {
				fromVarName = ">>shipInfo[" + fromShip + "].weaponGroup[" + wgNameList[w] + "][" + n + "]";
				toVarName = ">>shipInfo[" + toShip + "].weaponGroup[" + wgNameList[w] + "][" + n + "]";

				bool wgIsActive = false;
				settingName = fromVarName + ".isActive";
				boolValueN = VA.GetBoolean(settingName);
				if (boolValueN.HasValue)  wgIsActive = boolValueN.Value;

				VA.SetBoolean(toVarName + ".isActive", wgIsActive);

				if (wgIsActive) {
					int wgLen = 0;
					settingName = fromVarName + ".weaponKeyPress.len";
					intValueN = VA.GetInt(settingName);
					if (intValueN.HasValue)  wgLen = intValueN.Value;

					VA.SetInt(toVarName + ".weaponKeyPress.len", wgLen);

					for (short l = 0; l < wgLen; l++) {
						settingName = fromVarName + ".weaponKeyPress[" + l + "]";
						keybind = VA.GetText(settingName);

						VA.SetText(toVarName + ".weaponKeyPress[" + l + "]", keybind);
					}
				}
			}
		}

		VA.SetText(">>activeShipName", toShip);
		playRandomSound("Active ship changed to " + toShip, null, true);

		if (writeSettings()) {
			playRandomSound("Copy complete", "Configuration Complete");
		} else {
			playRandomSound("Problem writing ship settings", "Settings Write Error");
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
}
