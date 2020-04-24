//***  System.Web.Extensions.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

public class VAInline
{

	private string getSettingsPath(bool includeFilename = true) {
		string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		path = Path.Combine(path, "VoiceAttack SF-I Companion");

		try {
			Directory.CreateDirectory(path);
			if (includeFilename)  path = Path.Combine(path, "settings.json");
		} catch (Exception e) {
			path = null;
		}

		return path;
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

	public void main() {

		//*** Init
		bool? boolValueN;
		int? intValueN;
		string strValue;
		string keybind;
		string settingName;


		//*** Ship variables
		string variable = VA.GetText(">>shipNameListStr");
		string[] shipNameList = variable.Split(';');

		//*** Weapon group variables
		string wgNameListStr = VA.GetText(">>weaponGroupNameList");
		string[] wgNameList = wgNameListStr.Split(';');

		//*** Max weapon group num
		int wgNumMax = 9;
		intValueN = VA.GetInt(">maxWeaponGroupNum");
		if (intValueN.HasValue)  wgNumMax = intValueN.Value;

		//*** Static Group List
		variable = VA.GetText(">>staticGroupList");
		string[] staticGroupList = variable.Split(';');

		//*** Load the settings from file
		Dictionary<string, Object> settings = readSettingsFromFile();

		//*** Prepare global settings
		string[] boolSettingsList = { ">>enableSpeech", ">>gameVoiceEnabled", ">>gameVoiceActionsQuiet", ">>headphonesInUse" };
		foreach (string sn in boolSettingsList) {
			if (settings.ContainsKey(sn)) {
				try {
					boolValueN = (Boolean) settings[sn];
					if (boolValueN.HasValue) {
						VA.SetBoolean(sn, boolValueN.Value);
						//VA.WriteToLog("Loaded bool " + sn + " from json [" + boolValueN.Value + "]", "Green");
					}
				} catch (Exception e) {
					VA.WriteToLog("Unabled to read bool " + sn + " from json.", "Red");
				}
			}
		}

		string[] stringSettingsList = { ">>voiceDir", ">>companionName", ">>title", ">>activeShipName" };
		foreach (string sn in stringSettingsList) {
			if (settings.ContainsKey(sn)) {
				try {
					strValue = settings[sn].ToString();
					if (strValue != null) {
						VA.SetText(sn, strValue);
						//VA.WriteToLog("Loaded str " + sn + " from json [" + strValue + "]", "Green");
					}
				} catch (Exception e) {
					VA.WriteToLog("Unabled to read str " + sn + " from json.", "Red");
				}
			}
		}



		//*** Prepare ship settings
		bool wgIsActive = false;
		int wgLen = 0;
		string tmpVarName;
		string[] settingNameList = new string[2];
		for (short s = 0; s < shipNameList.Length; s++) {
			bool shipInUse = false;
			settingName = ">>shipInfo[" + shipNameList[s] + "].isInUse";

			if (settings.ContainsKey(settingName)) {
				try {
					boolValueN = (Boolean) settings[settingName];
					if (boolValueN.HasValue) {
						shipInUse = boolValueN.Value;
						//VA.WriteToLog("Loaded bool " + settingName + " from json [" + shipInUse + "]", "Green");
					}
				} catch (Exception e) {
					VA.WriteToLog("Unabled to read bool " + settingName + " from json.", "Red");
				}
			}

			VA.SetBoolean(settingName, shipInUse);

			if (shipInUse) {
				for (short w = 0; w < wgNameList.Length; w++) {
					for (short n = 1; n <= wgNumMax; n++) {
						tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + wgNameList[w] + "][" + n + "]";

						wgIsActive = false;
						settingName = tmpVarName + ".isActive";

						if (settings.ContainsKey(settingName)) {
							try {
								boolValueN = (Boolean) settings[settingName];
								if (boolValueN.HasValue) {
									wgIsActive = boolValueN.Value;
									//VA.WriteToLog("Loaded bool " + settingName + " from json [" + wgIsActive + "]", "Green");
								}
							} catch (Exception e) {
								VA.WriteToLog("Unabled to read bool " + settingName + " from json.", "Red");
							}
						}

						VA.SetBoolean(settingName, wgIsActive);

						if (wgIsActive) {
							wgLen = 0;
							settingName = tmpVarName + ".weaponKeyPress.len";

							if (settings.ContainsKey(settingName)) {
								try {
									intValueN = (int) settings[settingName];
									if (intValueN.HasValue) {
										wgLen = intValueN.Value;
										//VA.WriteToLog("Loaded int " + settingName + " from json [" + wgLen + "]", "Green");
									}
								} catch (Exception e) {
									VA.WriteToLog("Unabled to read int " + settingName + " from json.", "Red");
								}
							}

							VA.SetInt(settingName, wgLen);

							for (short l = 0; l < wgLen; l++) {
								settingNameList[0] = tmpVarName + ".weaponKeyPress[" + l + "]";
								settingNameList[1] = tmpVarName + ".weaponKeyPressFriendly[" + l + "]";

								foreach (string sName in settingNameList) {
									keybind = "";
									if (settings.ContainsKey(sName)) {
										try {
											keybind = settings[sName].ToString();
											//VA.WriteToLog("Loaded str " + sName + " from json [" + keybind + "]", "Green");
										} catch (Exception e) {
											if (sName.IndexOf("Friendly") == -1) {
												VA.WriteToLog("Unabled to read str " + sName + " from json.", "Red");
											}
										}
									}

									if (!string.IsNullOrEmpty(keybind)) {
										VA.SetText(sName, keybind);
									}
								}
							}
						}
					}
				}



				foreach (string gName in staticGroupList) {
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + gName + "][1]";

					wgIsActive = false;
					settingName = tmpVarName + ".isActive";

					if (settings.ContainsKey(settingName)) {
						try {
							boolValueN = (Boolean) settings[settingName];
							if (boolValueN.HasValue)  wgIsActive = boolValueN.Value;
						} catch (Exception e) {
							VA.WriteToLog("Unabled to read bool " + settingName + " from json.", "Red");
						}
					}

					VA.SetBoolean(settingName, wgIsActive);

					if (wgIsActive) {
						settingName = tmpVarName + ".weaponKeyPress.len";

						wgLen = 0;
						if (settings.ContainsKey(settingName)) {
							try {
								intValueN = (int) settings[settingName];
								if (intValueN.HasValue)  wgLen = intValueN.Value;
							} catch (Exception e) {
								VA.WriteToLog("Unabled to read int " + settingName + " from json.", "Red");
							}
						}

						VA.SetInt(settingName, wgLen);

						for (short l = 0; l < wgLen; l++) {
							settingNameList[0] = tmpVarName + ".weaponKeyPress[" + l + "]";
							settingNameList[1] = tmpVarName + ".weaponKeyPressFriendly[" + l + "]";

							foreach (string sName in settingNameList) {
								keybind = "";
								if (settings.ContainsKey(sName)) {
									try {
										keybind = settings[sName].ToString();
										//VA.WriteToLog("Loaded str " + sName + " from json [" + keybind + "]", "Green");
									} catch (Exception e) {
										if (sName.IndexOf("Friendly") == -1) {
											VA.WriteToLog("Unabled to read str " + sName + " from json.", "Red");
										}
									}
								}

								if (!string.IsNullOrEmpty(keybind)) {
									VA.SetText(sName, keybind);
								}
							}
						}
					}
				}
			}
		}

		//*** Done
		//VA.WriteToLog("restoreSavedSettings done", "Black");
	}
}
