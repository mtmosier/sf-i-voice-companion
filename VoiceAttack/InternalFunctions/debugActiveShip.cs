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
		VA.WriteToLog(message, "Pink");

		string activeShipName = VA.GetText(">>activeShipName");

		string tmpVarName;
		for (short s = 0; s < shipNameList.Length; s++) {
			if (shipNameList[s] != activeShipName)
				continue;

			message = shipNameList[s] + " > isInUse: " + VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse");
			VA.WriteToLog(message, "Orange");

			if (VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse") == true) {
				foreach (string wgName in activeWeaponGroupList) {
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + wgName + "]";
					if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
						int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
						int len = lenN.HasValue ? lenN.Value : 0;

						List<string> keyPressList = new List<string>();
						for (short l = 0; l < len; l++) {
							keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPressFriendly[" + l + "]"));
						}
						message = shipNameList[s] + " >" + wgName + " > weaponKeyPressList:  " + String.Join(", ", keyPressList);
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
						VA.WriteToLog(message, "Orange");
					}
				}
			}
		}
	}
}
