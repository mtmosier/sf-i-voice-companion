//***  System.Web.Extensions.dll;System.Linq.dll;System.Core.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;


public class VAInline
{
	public void main() {

		//*** Init
		bool? boolValueN;
		bool boolValue = false;
		int? intValueN;
		int intValue = 0;
		string strValue;
		string keybind;
		string settingName;


		//*** Ship variables
		string variable = VA.GetText(">>shipNameListStr");
		string[] shipNameList = variable.Split(';');

		//*** Weapon group variables
		string wgNameListStr = VA.GetText(">>weaponGroupListStr");
		string[] wgNameList = wgNameListStr.Split(';');

		//*** Static Group List
		variable = VA.GetText(">>staticGroupList");
		string[] staticGroupList = variable.Split(';');

		//*** Prepare settings dict
		Dictionary<string, Object> settings = new Dictionary<string, Object>();

		//*** Prepare global settings

		string[] boolSettingsList = { ">>enableSpeech", ">>gameVoiceEnabled", ">>gameVoiceActionsQuiet", ">>headphonesInUse" };
		foreach (string sn in boolSettingsList) {
			boolValueN = VA.GetBoolean(sn);
			boolValue = false;
			if (boolValueN.HasValue)  boolValue = boolValueN.Value;
			settings.Add(sn, boolValue);
		}

		string[] stringSettingsList = { ">>voiceDir", ">>companionName", ">>title", ">>activeShipName" };
		foreach (string sn in stringSettingsList) {
			strValue = VA.GetText(sn);
			settings.Add(sn, strValue);
		}


		//*** Prepare ship settings
		bool wgIsActive = false;
		int wgLen = 0;
		string tmpVarName;
		string[] settingNameList = new string[2];
		for (short s = 0; s < shipNameList.Length; s++) {
			bool shipInUse = false;
			settingName = ">>shipInfo[" + shipNameList[s] + "].isInUse";
			boolValueN = VA.GetBoolean(settingName);
			if (boolValueN.HasValue)  shipInUse = boolValueN.Value;

			if (shipInUse) {
				settings.Add(settingName, shipInUse);

				for (short w = 0; w < wgNameList.Length; w++) {
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + wgNameList[w] + "]";

					settingName = tmpVarName + ".isActive";
					boolValueN = VA.GetBoolean(settingName);
					wgIsActive = false;
					if (boolValueN.HasValue)  wgIsActive = boolValueN.Value;

					if (wgIsActive) {
						settings.Add(settingName, wgIsActive);

						settingName = tmpVarName + ".weaponKeyPress.len";
						intValueN = VA.GetInt(settingName);
						wgLen = 0;
						if (intValueN.HasValue)  wgLen = intValueN.Value;

						settings.Add(settingName, wgLen);

						for (short l = 0; l < wgLen; l++) {
							settingNameList[0] = tmpVarName + ".weaponKeyPress[" + l + "]";
							settingNameList[1] = tmpVarName + ".weaponKeyPressFriendly[" + l + "]";

							foreach (string sName in settingNameList) {
								keybind = VA.GetText(sName);
								settings.Add(sName, keybind);
							}
						}
					}
				}

				foreach (string gName in staticGroupList) {
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + gName + "]";

					settingName = tmpVarName + ".isActive";
					boolValueN = VA.GetBoolean(settingName);
					wgIsActive = false;
					if (boolValueN.HasValue)  wgIsActive = boolValueN.Value;

					if (wgIsActive) {
						settings.Add(settingName, wgIsActive);

						settingName = tmpVarName + ".weaponKeyPress.len";
						intValueN = VA.GetInt(settingName);
						wgLen = 0;
						if (intValueN.HasValue)  wgLen = intValueN.Value;

						settings.Add(settingName, wgLen);

						for (short l = 0; l < wgLen; l++) {
							settingNameList[0] = tmpVarName + ".weaponKeyPress[" + l + "]";
							settingNameList[1] = tmpVarName + ".weaponKeyPressFriendly[" + l + "]";

							foreach (string sName in settingNameList) {
								keybind = VA.GetText(sName);
								settings.Add(sName, keybind);
							}
						}
					}
				}
			}
		}

		//*** Write settings to a file
		bool success = writeSettingsToFile(settings);
		//VA.WriteToLog("writeSettingsToFile: " + (success ? "success" : "fail"), "Black");
	}

	private string getSettingsPath(bool includeFilename = true) {
		string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		path = Path.Combine(path, "VoiceAttack SF-I Companion");

		string settingsFileName = VA.GetText(">settingsFileName");
		if (String.IsNullOrEmpty(settingsFileName))
			settingsFileName = "settings.json";

		try {
			Directory.CreateDirectory(path);
			if (includeFilename)  path = Path.Combine(path, settingsFileName);
		} catch (Exception e) {
			path = null;
		}

		return path;
	}

	private bool writeSettingsToFile(Dictionary<string,object> settings) {
		bool rtnVal = false;
		string filePath = getSettingsPath(true);

		if (!string.IsNullOrEmpty(filePath)) {
			try {
				string json = new JavaScriptSerializer().Serialize(settings);
				System.IO.File.WriteAllText(filePath, json);
				rtnVal = true;
			} catch (Exception e) {}
		}

		return rtnVal;
	}

	private Dictionary<string,object> readSettingsFromFile() {
		Dictionary<string,object> rtnVal = null;
		string filePath = getSettingsPath(true);

		try {
			string json = System.IO.File.ReadAllText(filePath);
			if (!string.IsNullOrEmpty(json)) {
				rtnVal = new JavaScriptSerializer().Deserialize<Dictionary<string,object>>(json);
			}
		} catch (Exception e) {}

		return rtnVal;
	}
}
