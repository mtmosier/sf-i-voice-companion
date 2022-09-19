//***  System.Web.Extensions.dll;System.Linq.dll;System.Core.dll;System.Text.RegularExpressions.dll;System.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

public class VAInline
{
	private List<string> staticGroupList;


	public void main() {
		restoreSettings();
	}

	public void restoreSettings() {

		//*** Init
		bool? boolValueN;
		int? intValueN;
		string strValue, variable;
		string keybind;
		string settingName;
		string shipNameInput;
		string activeShipName = "";
		StringComparer comparer = StringComparer.CurrentCultureIgnoreCase;

		//*** Static Group List
		variable = VA.GetText(">>staticGroupList");
		if (string.IsNullOrEmpty(variable)) variable = "";
		staticGroupList = new List<string>(variable.Split(';'));


		//*** Set list of ship / weapon group names
		List<string> activeShipList = new List<string>();
		List<string> activeWeaponGroupList = new List<string>();
		List<string> activeStaticGroupList = new List<string>();

		List<string> fullShipList;
		List<string> fullWeaponGroupList;

		variable = VA.GetText(">>shipNameList");
		if (!string.IsNullOrEmpty(variable)) {
			fullShipList = new List<string>(variable.Split(';'));
		} else {
			fullShipList = new List<string>();
		}

		variable = VA.GetText(">>shipNameListStr");
		if (!String.IsNullOrEmpty(variable)) {
			string[] shipNameList = variable.Split(';');
			foreach (string sn in shipNameList)
				if (!fullShipList.Contains(sn))
					fullShipList.Add(sn);
		}

		variable = VA.GetText(">>weaponGroupNameList");
		if (!string.IsNullOrEmpty(variable)) {
			fullWeaponGroupList = new List<string>(variable.Split(';'));
		} else {
			fullWeaponGroupList = new List<string>();
		}


		//*** Load the settings from file
		Dictionary<string, Object> settings = readSettingsFromFile();
		if (settings != null) {

			//*** Add additional ship information found in the settings

			//*** Instantiate the regular expression objects.
			string shipPattern;
			shipPattern = "^" + Regex.Escape(@">>shipInfo[");
			shipPattern += @"(.*?)";
			shipPattern += Regex.Escape(@"].isInUse") + "$";

			string wgPattern;
			wgPattern = Regex.Escape(@"].weaponGroup[");
			wgPattern += @"(.*?)";
			wgPattern += Regex.Escape(@"].isActive") + "$";

			Regex shipRegex = new Regex(shipPattern, RegexOptions.IgnoreCase);
			Regex wgRegex = new Regex(wgPattern, RegexOptions.IgnoreCase);

			string sn = "";
			foreach (string loopSn in settings.Keys) {
		  		Match match = shipRegex.Match(loopSn);
				if (match.Success) {
					sn = match.Groups[1].ToString();

					boolValueN = (Boolean) settings[loopSn];
					if (boolValueN.HasValue && boolValueN.Value) {
						if (!fullShipList.Contains(sn))
							fullShipList.Add(sn);
						if (!activeShipList.Contains(sn))
							activeShipList.Add(sn);
					}
				}

				match = wgRegex.Match(loopSn);
				if (match.Success) {
					string wg = match.Groups[1].ToString();

					boolValueN = (Boolean) settings[loopSn];
					if (boolValueN.HasValue && boolValueN.Value) {
						if (!fullWeaponGroupList.Contains(wg) && !staticGroupList.Contains(wg))
							fullWeaponGroupList.Add(wg);
					}
				}
	  		}
//VA.WriteToLog("ship count: " + fullShipList.Count.ToString(), "Orange");

			//*** Prepare global settings
			string[] boolSettingsList = { ">>enableSpeech", ">>gameVoiceEnabled",
				">>gameVoiceActionsQuiet", ">>headphonesInUse",
				">>galaxapediaEnabled", ">>constellationsEnabled",
				">>quantumTheoryEnabled"
			};
			foreach (string sname in boolSettingsList) {
				if (settings.ContainsKey(sname)) {
					try {
						boolValueN = (Boolean) settings[sname];
						if (boolValueN.HasValue) {
							VA.SetBoolean(sname, boolValueN.Value);
						}
					} catch (Exception e) {
						VA.WriteToLog("Unabled to read bool " + sname + " from json.", "Red");
					}
				}
			}

			string[] stringSettingsList = { ">>voiceDir", ">>companionName", ">>title", ">>activeShipName" };
			foreach (string sname in stringSettingsList) {
				if (settings.ContainsKey(sname)) {
					try {
						strValue = settings[sname].ToString();
						if (strValue != null) {
							VA.SetText(sname, strValue);

							if (sname == ">>activeShipName")
								activeShipName = strValue;
						}
					} catch (Exception e) {
						VA.WriteToLog("Unabled to read str " + sname + " from json.", "Red");
					}
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
					for (short w = 0; w < fullWeaponGroupList.Count; w++) {
						tmpVarName = ">>shipInfo[" + fullShipList[s] + "].weaponGroup[" + fullWeaponGroupList[w] + "]";

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

							if (comparer.Compare(fullShipList[s], activeShipName) == 0) {
								string activeWGName = fullWeaponGroupList[w];

								if (!activeWeaponGroupList.Contains(activeWGName))
									activeWeaponGroupList.Add(activeWGName);
							}

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



					foreach (string gName in staticGroupList) {
						tmpVarName = ">>shipInfo[" + fullShipList[s] + "].weaponGroup[" + gName + "]";

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
							if (comparer.Compare(fullShipList[s], activeShipName) == 0) {
								if (!activeStaticGroupList.Contains(gName))
									activeStaticGroupList.Add(gName);
							}

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
		}

		if (fullShipList.Count == 0)
			fullShipList.Add("Fighter");

		//*** Make sure an active ship name is selected
		if (string.IsNullOrEmpty(activeShipName)) {
			activeShipName = fullShipList[0];
			VA.SetText(">>activeShipName", activeShipName);

			if (!activeShipList.Contains(activeShipName))
				activeShipList.Add(activeShipName);
		}

		//*** Ensure the active ship is marked as in use
		if (!string.IsNullOrEmpty(activeShipName))
			VA.SetBoolean(">>shipInfo[" + activeShipName + "].isInUse", true);


		//*** Export ship list settings
		fullShipList.Sort();
		shipNameInput = "[" + string.Join<string>(";", fullShipList) + "] [ship;]";
		VA.SetText(">>configShipNameInput", shipNameInput);

		if (fullShipList.Count > 0)		shipNameInput = "[" + string.Join<string>(";", fullShipList) + ";current;active] [ship;]";
		else							shipNameInput = "[current;active] [ship;]";
		VA.SetText(">>shipNameInput", shipNameInput);

		activeShipList.Sort();
		if (activeShipList.Count > 0)	shipNameInput = "[" + string.Join<string>(";", activeShipList) + ";current;active] [ship;]";
		else							shipNameInput = "[current;active] [ship;]";
		VA.SetText(">>activeShipNameInput", shipNameInput);

		VA.SetText(">>shipNameListStr", string.Join<string>(";", fullShipList));
		//VA.WriteToLog("ship count: " + fullShipList.Count.ToString(), "Red");

		//*** Export active weapon group list
		activeWeaponGroupList.Sort();
		string weaponGroupInput = string.Join<string>(";", activeWeaponGroupList);
		VA.SetText(">>activeWeaponGroupList", weaponGroupInput);

		if (!string.IsNullOrEmpty(weaponGroupInput))
			weaponGroupInput = "[" + weaponGroupInput + "]";
		VA.SetText(">>activeWeaponGroupInput", weaponGroupInput);

		fullWeaponGroupList.Sort();
		VA.SetText(">>weaponGroupListStr", string.Join<string>(";", fullWeaponGroupList));

		//*** Export active static group list
		activeStaticGroupList.Sort();
		string staticGroupInput = string.Join<string>(";", activeStaticGroupList);
		VA.SetText(">>activeStaticGroupList", staticGroupInput);


		//*** Done
		//VA.WriteToLog("restoreSavedSettings done", "Black");
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

	private string updateSettingsJson(string jsonStr) {
		string pattern = @"weaponGroup\[([^\]]+)\]\[(\d+)\]";
		string replacement = "weaponGroup[$1 $2]";
		Regex rgx = new Regex(pattern);
		jsonStr = rgx.Replace(jsonStr, replacement);

		foreach (string staticGroup in staticGroupList) {
			pattern = Regex.Escape(@"weaponGroup[" + staticGroup  + " 1]");
			replacement = "weaponGroup[" + staticGroup + "]";
			rgx = new Regex(pattern);
			jsonStr = rgx.Replace(jsonStr, replacement);
		}

		return jsonStr;
	}

	private Dictionary<string,object> readSettingsFromFile() {
		Dictionary<string,object> rtnVal = null;
		string filePath = getSettingsPath(true);

		try {
			string json = System.IO.File.ReadAllText(filePath);
			json = updateSettingsJson(json);
			if (!string.IsNullOrEmpty(json)) {
				rtnVal = new JavaScriptSerializer().Deserialize<Dictionary<string,object>>(json);
			}
		} catch (Exception e) {}

		return rtnVal;
	}
}
