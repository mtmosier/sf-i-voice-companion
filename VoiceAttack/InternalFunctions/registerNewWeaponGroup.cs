// Microsoft.CSharp.dll;System.dll;System.Core.dll;System.Data.dll;System.Data.DataSetExtensions.dll;System.Deployment.dll;System.Drawing.dll;System.Net.Http.dll; System.Windows.Forms.dll;System.Xml.dll;System.Xml.Linq.dll

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class VAInline
{
	private bool allowFreeFormWeaponGroupNames;
	private bool headphonesInUse;
	private string activeShipName;
	private StringComparer comparer;
	private TextInfo ti;

	//*** References to other VA Commands
	private Guid requestVerbalUserInputGuid;
	private Guid writeSettingsToFileGuid;
	private Guid reloadProfileGuid;
	private Guid playRandomSoundGuid;
	private Guid configureWeaponGroupGuid;

	//*** Valid responses
	private string[] cancelList = { "cancel", "nevermind", "never mind", "abort" };
	private string[] restartList = { "restart", "start over", "do over" };
	private string[] agreeList = { "confirm", "positive", "affirmative", "absolutely", "please", "please do", "yeah", "yes", "yessir", "yes sir", "commit" };
	private string[] saveList = { "save", "please save" };



	public void main()
	{
		//*** Initialize
		initialize();

		List<string> weaponGroupList = readWeaponGroupList();
		List<string> activeWeaponGroupList = readWeaponGroupList(true);


		string groupName = null;
		while (true) {
			string response, request;

			if (String.IsNullOrEmpty(groupName)) {
				request = "New weapon group name?";
				response = getUserInput(
					string.Join(";", cancelList) + ";"
					+ string.Join(";", restartList),  //*** inputOptionListStr
					request,  //*** playbackText
					null,  //*** playbackFileGroupName
					(headphonesInUse != true),  //*** pauseForPlayback
					false,  //*** shortPause
					allowFreeFormWeaponGroupNames   //*** returnOnAnyInput
				);

				if (!string.IsNullOrEmpty(response)) {
					if (Array.IndexOf(cancelList, response) != -1) {
						VA.WriteToLog("Cancelled " + response, "Red");
						cancelConfiguration();
						return;
					}

					if (Array.IndexOf(restartList, response) != -1) {
						VA.WriteToLog("Restarting initialization.", "Orange");
						groupName = "";
						continue;
					}

					groupName = response;
				}
			}

			if (!String.IsNullOrEmpty(groupName)) {
				groupName = ti.ToTitleCase(groupName.Trim());

				if (weaponGroupList.Contains(groupName)) {
					//*** Weapon group already exists - pass off control to the configure command
					VA.WriteToLog("Weapon group name already exists. Calling configure.", "Orange");
					configureWeaponGroup(groupName);
					return;

				} else {

					VA.WriteToLog("Registering new weapon group, " + groupName, "Black");

					request = "Initializing " + groupName + " weapon group.";
					response = getUserInput(
						string.Join(";", agreeList)
						+ ";" + string.Join(";", saveList)
						+ ";" + string.Join(";", cancelList)
						+ ";" + string.Join(";", restartList),  //*** inputOptionListStr
						request, //*** playbackText
						null,    //*** playbackFileGroupName
						(headphonesInUse != true),  //*** pauseForPlayback
						true  //*** shortPause
					);

					if (!string.IsNullOrEmpty(response)) {
						if (Array.IndexOf(cancelList, response) != -1) {
							VA.WriteToLog("Cancelled " + response, "Red");
							cancelConfiguration();
							return;
						}

						if (Array.IndexOf(restartList, response) != -1) {
							VA.WriteToLog("Restart new weapon group initialization.", "Orange");
							groupName = "";
							continue;
						}

						if (Array.IndexOf(agreeList, response) != -1 || Array.IndexOf(saveList, response) != -1) {
							VA.WriteToLog("Quick confirm new weapon group initialization.", "Black");
							playRandomSound("Confirmed", "Non-Verbal Confirmation", true);
						}
					}

					weaponGroupList.Add(groupName);
					saveWeaponGroupList(weaponGroupList, false);

					activeWeaponGroupList.Add(groupName);
					saveWeaponGroupList(activeWeaponGroupList, true);

					configureWeaponGroup(groupName);
					return;
				}
			}
		}
	}


	private void initialize()
	{
		comparer = StringComparer.CurrentCultureIgnoreCase;
		ti = CultureInfo.CurrentCulture.TextInfo;

		//*** GET RELEVANT SETTINGS
		activeShipName = VA.GetText(">>activeShipName");

		bool? bVar = VA.GetBoolean(">>allowFreeFormWeaponGroupNames");
		allowFreeFormWeaponGroupNames = bVar.HasValue ? bVar.Value : false;

		bVar = VA.GetBoolean(">>headphonesInUse");
		headphonesInUse = bVar.HasValue ? bVar.Value : false;

		string tmpCmdId = VA.GetText(">requestVerbalUserInputCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  requestVerbalUserInputGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">writeSettingsToFileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  writeSettingsToFileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">reloadProfileCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  reloadProfileGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  playRandomSoundGuid = new Guid(tmpCmdId);

		tmpCmdId = VA.GetText(">configureWeaponGroupCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  configureWeaponGroupGuid = new Guid(tmpCmdId);
	}


	private void configureWeaponGroup(string weaponGroupName)
	{
		if (configureWeaponGroupGuid == null)  return;

		VA.SetText("~~wgGroupNameExtStr", weaponGroupName);
		VA.Command.Execute(configureWeaponGroupGuid, true, true);
	}

	private string getUserInput(string inputOptionListStr, string playbackText, string playbackFileGroupName, bool pauseForPlayback = false, bool shortPause = true, bool returnOnAnyInput = false)
	{
		if (requestVerbalUserInputGuid == null)  return "";

		VA.SetText("~~viInputOptionString", inputOptionListStr);
		VA.SetText("~~viPlaybackText", playbackText);
		VA.SetText("~~viPlaybackFileGroupName", playbackFileGroupName);
		VA.SetBoolean("~~viPauseForPlayback", pauseForPlayback);
		//VA.SetBoolean("~~viVeryShortInputPause", shortPause);
		VA.SetBoolean("~~viShortInputPause", shortPause);
		VA.SetBoolean("~~viReturnOnAnyInput", returnOnAnyInput);
		VA.Command.Execute(requestVerbalUserInputGuid, true, true);

		return VA.GetText("~~viUserResponse");
	}

	private void playRandomSound(string playbackText, string playbackFileGroupName, bool pauseForPlayback = true)
	{
		if (playRandomSoundGuid == null)  return;

		VA.SetText("~~rsText", playbackText);
		VA.SetText("~~rsFileGroupName", playbackFileGroupName);
		VA.Command.Execute(playRandomSoundGuid, pauseForPlayback, true);
	}

	private void reloadProfile()
	{
		if (reloadProfileGuid == null)  return;
		VA.Command.Execute(reloadProfileGuid, false, false);
	}

	private bool writeSettings()
	{
		if (writeSettingsToFileGuid == null)  return false;
		VA.Command.Execute(writeSettingsToFileGuid, true, true);
		return true;
	}

	private void cancelConfiguration()
	{
		playRandomSound("Cancelling Configuration", "Cancel", false);
	}

	private void configurationError()
	{
		cancelConfiguration();
//		playRandomSound("Configuration error. Cancelling", "General Error", false);
	}

	private void timeoutError()
	{
		playRandomSound("Configuration error. Timed out.", "General Error", false);
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
