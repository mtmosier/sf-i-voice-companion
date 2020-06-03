//***  System.Web.Extensions.dll;System.Linq.dll;System.Core.dll;System.Text.RegularExpressions.dll;System.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

public class VAInline
{
	public void main() {

		//*** Init
		bool? boolValueN;
		int? intValueN;
		string strValue, variable;
		string keybind;
		string settingName;


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
		if (settings == null) {
			return;
		}


		//*** Set list of ship names
		List<string> fullShipList = new List<string>();
		variable = VA.GetText(">>shipNameListStr");
		if (!String.IsNullOrEmpty(variable)) {
			string[] shipNameList = variable.Split(';');
			foreach (string sn in shipNameList)
				fullShipList.Add(sn);
		}

		string pattern;
		pattern = "^" + Regex.Escape(@">>shipInfo[");
		pattern += @"(.*?)";
		pattern += Regex.Escape(@"].isInUse") + "$";

		//*** Instantiate the regular expression object.
		Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

		foreach (string loopSn in settings.Keys) {
	  		Match match = regex.Match(loopSn);
			if (match.Success) {
				string sn = match.Groups[1].ToString();

				boolValueN = (Boolean) settings[loopSn];
				if (boolValueN.HasValue && boolValueN.Value)
					if (!fullShipList.Contains(sn))
						fullShipList.Add(sn);
			}
  		}

		string shipNameInput = "[" + string.Join<string>(";", fullShipList) + "] [ship;]";
		VA.SetText(">>configShipNameInput", shipNameInput);

		shipNameInput = "[" + string.Join<string>(";", fullShipList) + ";current;active] [ship;]";
		VA.SetText(">>shipNameInput", shipNameInput);

		VA.SetText(">>shipNameListStr", string.Join<string>(";", fullShipList));

		//*** Prepare global settings
		string[] boolSettingsList = { ">>enableSpeech", ">>gameVoiceEnabled", ">>gameVoiceActionsQuiet", ">>headphonesInUse" };
		foreach (string sn in boolSettingsList) {
			if (settings.ContainsKey(sn)) {
				try {
					boolValueN = (Boolean) settings[sn];
					if (boolValueN.HasValue) {
						VA.SetBoolean(sn, boolValueN.Value);
					}
				} catch (Exception e) {
					VA.WriteToLog("Unabled to read bool " + sn + " from json.", "Red");
				}
			}
		}

		string[] stringSettingsList = { ">>voiceDir", ">>companionName", ">>title", ">>activeShipName" };
		string activeShipName = "";
		foreach (string sn in stringSettingsList) {
			if (settings.ContainsKey(sn)) {
				try {
					strValue = settings[sn].ToString();
					if (strValue != null) {
						VA.SetText(sn, strValue);

						if (sn == ">>activeShipName")
							activeShipName = strValue;
					}
				} catch (Exception e) {
					VA.WriteToLog("Unabled to read str " + sn + " from json.", "Red");
				}
			}
		}


		//*** Make sure an active ship name is selected
		if (string.IsNullOrEmpty(activeShipName)) {
			if (fullShipList.Count > 0) {
				activeShipName = fullShipList[0];
				VA.SetText(">>activeShipName", activeShipName);
			}
		}


		//*** Prepare ship settings
		bool wgIsActive = false;
		int wgLen = 0;
		string tmpVarName;
		string[] settingNameList = new string[2];
		for (short s = 0; s < fullShipList.Count; s++) {
			bool shipInUse = false;
			settingName = ">>shipInfo[" + fullShipList[s] + "].isInUse";

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
						tmpVarName = ">>shipInfo[" + fullShipList[s] + "].weaponGroup[" + wgNameList[w] + "][" + n + "]";

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
					tmpVarName = ">>shipInfo[" + fullShipList[s] + "].weaponGroup[" + gName + "][1]";

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

		//*** Ensure the active ship is marked as in use
		if (!string.IsNullOrEmpty(activeShipName))
			VA.SetBoolean(">>shipInfo[" + activeShipName + "].isInUse", true);

		//*** Done
		//VA.WriteToLog("restoreSavedSettings done", "Black");
	}



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

	private string updateSettingsJson(string jsonStr) {
		string pattern = @"weaponGroup\[([^\]]+)\]\[(\d+)\]";
		string replacement = "weaponGroup[$1 $2]";
		Regex rgx = new Regex(pattern);

		return rgx.Replace(jsonStr, replacement);
	}

	private Dictionary<string,object> readSettingsFromFile() {
		Dictionary<string,object> rtnVal = null;
		string filePath = getSettingsPath(true);

		try {
			string json = System.IO.File.ReadAllText(filePath);
			// json = updateSettingsJson(json);
			if (!string.IsNullOrEmpty(json)) {
				rtnVal = new JavaScriptSerializer().Deserialize<Dictionary<string,object>>(json);
			}
		} catch (Exception e) {}

		return rtnVal;
	}
}
