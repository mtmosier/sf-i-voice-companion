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
		string[] shipNameList = shipNameListStr.Split(';');

		//*** Weapon group variables
		string wgNameListStr = VA.GetText(">>weaponGroupNameList");
		string[] wgNameList = wgNameListStr.Split(';');

		//*** Number of weapon groups in use
		int? maxWGNum_N = VA.GetInt(">maxWeaponGroupNum");
		int maxWGNum = maxWGNum_N.HasValue ? maxWGNum_N.Value : 9;

		//*** Static Group List
		string variable = VA.GetText(">>staticGroupList");
		string[] staticGroupList = variable.Split(';');

		//*** Static Group List
		variable = VA.GetText(">>companionNameList");
		string[] companionNameList = variable.Split(';');

		message = localDate.ToString("u");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		message = typeof(string).Assembly.ImageRuntimeVersion;
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		message = VA.ParseTokens("CPU Usage: {STATE_CPU}%");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Pink");

		message = VA.ParseTokens("RAM Usage: {STATE_RAMAVAILABLE} / {STATE_RAMTOTAL}");
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
				for (short w = 0; w < wgNameList.Length; w++) {
					for (short n = 1; n <= maxWGNum; n++) {
						tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + wgNameList[w] + "][" + n + "]";
						if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
							int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
							int len = lenN.HasValue ? lenN.Value : 0;

							List<string> keyPressList = new List<string>();
							for (short l = 0; l < len; l++) {
								keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPress[" + l + "]"));
							}
							message = shipNameList[s] + " >" + wgNameList[w] + " " + n + " > weaponKeyPressList:  " + String.Join(",", keyPressList);
							debugOutput += message + "\n";
							VA.WriteToLog(message, "Orange");
						}
					}
				}

				foreach (string groupName in staticGroupList) {
					short n = 1;
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + groupName + "][" + n + "]";

					if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
						int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
						int len = lenN.HasValue ? lenN.Value : 0;

						List<string> keyPressList = new List<string>();
						for (short l = 0; l < len; l++) {
							keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPress[" + l + "]"));
						}

						message = shipNameList[s] + " >" + groupName + " > weaponKeyPressList:  " + String.Join(",", keyPressList);
						debugOutput += message + "\n";
						VA.WriteToLog(message, "Orange");
					}
				}
			}
		}

		message = "Active Ship Name: " + VA.GetText(">>activeShipName");
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Orange");

		message = "Ship Name List: " + String.Join(", ", shipNameList);
		debugOutput += message + "\n";
		VA.WriteToLog(message, "Red");

		message = "Weapon Group Name List: " + String.Join(", ", wgNameList);
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
