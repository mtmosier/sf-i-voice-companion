using System;
using System.Collections.Generic;

public class VAInline
{
	public void main()
	{
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


		VA.WriteToLog(typeof(string).Assembly.ImageRuntimeVersion, "Pink");

		string tmpVarName;
		for (short s = 0; s < shipNameList.Length; s++) {
			VA.WriteToLog(shipNameList[s] + " > isInUse: " + VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse"), "Orange");
			if (VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse") == true) {
				for (short w = 0; w < wgNameList.Length; w++) {
					for (short n = 1; n <= maxWGNum; n++) {
						tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + wgNameList[w] + "][" + n + "]";
						if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
							int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
							int len = lenN.HasValue ? lenN.Value : 0;

							// VA.WriteToLog(shipNameList[s] + " >" + wgNameList[w] + " " + n + " > isActive:  " + VA.GetBoolean(tmpVarName + ".isActive"), "Orange");
							// VA.WriteToLog(shipNameList[s] + " >" + wgNameList[w] + " " + n + " > weaponKeyPress.len:  " + VA.GetInt(tmpVarName + ".weaponKeyPress.len"), "Orange");
							List<string> keyPressList = new List<string>();
							for (short l = 0; l < len; l++) {
								keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPress[" + l + "]"));
							}
							VA.WriteToLog(shipNameList[s] + " >" + wgNameList[w] + " " + n + " > weaponKeyPressList:  " + String.Join(",", keyPressList), "Orange");
						}
					}
				}

				foreach (string groupName in staticGroupList) {
					short n = 1;
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + groupName + "][" + n + "]";

					if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
						int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
						int len = lenN.HasValue ? lenN.Value : 0;

						// VA.WriteToLog(shipNameList[s] + " >" + groupName + " > isActive:  " + VA.GetBoolean(tmpVarName + ".isActive"), "Orange");
						// VA.WriteToLog(shipNameList[s] + " >" + groupName + " > weaponKeyPress.len:  " + VA.GetInt(tmpVarName + ".weaponKeyPress.len"), "Orange");
						List<string> keyPressList = new List<string>();
						for (short l = 0; l < len; l++) {
							keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPress[" + l + "]"));
						}
						VA.WriteToLog(shipNameList[s] + " >" + groupName + " > weaponKeyPressList:  " + String.Join(",", keyPressList), "Orange");
					}
				}
			}
		}

		VA.WriteToLog("Active Ship Name: " + VA.GetText(">>activeShipName"), "Orange");
		VA.WriteToLog("Ship Name List: " + String.Join(", ", shipNameList), "Red");
		VA.WriteToLog("Weapon Group Name List: " + String.Join(", ", wgNameList), "Red");
		VA.WriteToLog("Emergency Group List: " + String.Join(", ", staticGroupList), "Red");
		VA.WriteToLog("Companion Name List: " + String.Join(", ", companionNameList), "Red");
		VA.WriteToLog("Selected Weapon Slot: " + VA.GetInt(">>selectedWeaponSlot"), "Green");
		VA.WriteToLog("Cease Fire Active: " + VA.GetBoolean(">>ceaseFireActive"), "Green");
		VA.WriteToLog("Game Voice Enabled: " + VA.GetBoolean(">>gameVoiceEnabled"), "Green");
		VA.WriteToLog("Active Window Title: " + VA.ParseTokens("{ACTIVEWINDOWTITLE}"), "Blue");
		VA.WriteToLog("Voice Directory: " + VA.GetText(">>voiceDir"), "Blue");
		VA.WriteToLog("Companion: " + VA.GetText(">>companionName"), "Blue");
	}
}
