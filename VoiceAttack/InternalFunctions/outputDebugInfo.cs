using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

public class VAInline
{
	public void main()
	{
		//*** Initialize
		DateTime localDate = DateTime.Now;
		String debugOutput = "";
		String message;

		//*** Ship variables
		string shipNameListStr = VA.GetText(">>shipNameListStr");
		if (shipNameListStr == null)  shipNameListStr = "";
		string[] shipNameList = shipNameListStr.Split(';');

		//*** Weapon group variables
		string variable = VA.GetText(">>activeStaticGroupList");
		if (variable == null)  variable = "";
		List<string> activeStaticGroupList = new List<string>(variable.Split(';'));

		variable = VA.GetText(">>activeWeaponGroupList");
		if (variable == null)  variable = "";
		List<string> activeWeaponGroupList = new List<string>(variable.Split(';'));

		variable = VA.GetText(">>weaponGroupListStr");
		if (variable == null)  variable = "";
		List<string> fullWeaponGroupList = new List<string>(variable.Split(';'));

		//*** Static Group List
		variable = VA.GetText(">>staticGroupList");
		if (variable == null)  variable = "";
		string[] staticGroupList = variable.Split(';');

		//*** Static Group List
		variable = VA.GetText(">>companionNameList");
		if (variable == null)  variable = "";
		string[] companionNameList = variable.Split(';');

		message = localDate.ToString("u");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		message = "Environment version: " + Environment.Version.ToString();
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		message = VA.ParseTokens("CPU Usage: {STATE_CPU}%");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		string ramUsedStr = VA.ParseTokens("{EXP: {STATE_RAMTOTAL} - {STATE_RAMAVAILABLE}}");
		string ramTotalStr = VA.ParseTokens("{STATE_RAMTOTAL}");
		decimal ramUsed = 0, ramTotal = 0;

		try {
			ramUsed = decimal.Round(decimal.Parse(ramUsedStr) / 1024 / 1024, 0);
			ramTotal = decimal.Round(decimal.Parse(ramTotalStr) / 1024 / 1024, 0);
		} catch (Exception e) {}

		message = String.Format("RAM Usage: {0:n0}mb/ {1:n0}mb", ramUsed, ramTotal);
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		message = VA.ParseTokens("Recording Device: {STATE_DEFAULTRECORDING} || {STATE_DEFAULTRECORDINGCOMMS}");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		string tmpVarName;
		for (short s = 0; s < shipNameList.Length; s++) {

			message = shipNameList[s] + " > isInUse: " + VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse");
			debugOutput += message + "\n";
			VA.WriteToLog(message, "Orange");

			if (VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse") == true) {
				foreach (string wgName in fullWeaponGroupList) {
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + wgName + "]";
					if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
						int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
						int len = lenN.HasValue ? lenN.Value : 0;

						List<string> keyPressList = new List<string>();
						for (short l = 0; l < len; l++) {
							keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPressFriendly[" + l + "]"));
						}
						message = shipNameList[s] + " >" + wgName + " > weaponKeyPressList:  " + String.Join(", ", keyPressList);
						debugOutput += message + "\n";
						VA.WriteToLog(message, "Orange");
					}
				}

				foreach (string groupName in staticGroupList) {
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + groupName + "]";

					if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
						int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
						int len = lenN.HasValue ? lenN.Value : 0;

						List<string> keyPressList = new List<string>();
						for (short l = 0; l < len; l++) {
							keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPressFriendly[" + l + "]"));
						}

						message = shipNameList[s] + " >" + groupName + " > weaponKeyPressList:  " + String.Join(", ", keyPressList);
						debugOutput += message + "\n";
						VA.WriteToLog(message, "Orange");
					}
				}
			}
		}

		message = "Active Ship Name: " + VA.GetText(">>activeShipName");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Orange");

		message = "Headphones in use: " + (VA.GetBoolean(">>headphonesInUse") == true ? "Yes" : "No");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Ship Name List: " + String.Join(", ", shipNameList);
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Active Ship Name Input List: " + VA.GetText(">>activeShipNameInput");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Config Ship Name Input List: " + VA.GetText(">>configShipNameInput");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Weapon Group Name List: " + String.Join(", ", fullWeaponGroupList);
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Active Weapon Group Input: " + VA.GetText(">>activeWeaponGroupInput");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Emergency Group List: " + String.Join(", ", staticGroupList);
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Companion Name List: " + String.Join(", ", companionNameList);
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Selected Weapon Slot: " + VA.GetInt(">>selectedWeaponSlot");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Green");

		message = "Cease Fire Active: " + VA.GetBoolean(">>ceaseFireActive");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Green");

		message = "Game Voice Enabled: " + VA.GetBoolean(">>gameVoiceEnabled");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Green");

		message = "Active Window Title: " + VA.ParseTokens("{ACTIVEWINDOWTITLE}");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Blue");

		message = "Voice Directory: " + VA.GetText(">>voiceDir");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Blue");

		message = "Companion: " + VA.GetText(">>companionName");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Blue");


		debugOutput += "\n\n";
		appendToDebugFile(debugOutput);
	}


	private bool appendToDebugFile(string fileContents) {
		bool rtnVal = false;
		string filePath = getSavePath();

		if (!string.IsNullOrEmpty(filePath)) {
			try {
				System.IO.File.AppendAllText(filePath, fileContents);
				rtnVal = true;
			} catch (Exception e) {}
		}

		return rtnVal;
	}

	private string getSavePath() {
		string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		path = Path.Combine(path, "VoiceAttack SF-I Companion");

		try {
			Directory.CreateDirectory(path);
			path = Path.Combine(path, "debug.log");
		} catch (Exception e) {
			path = null;
		}

		return path;
	}
}
