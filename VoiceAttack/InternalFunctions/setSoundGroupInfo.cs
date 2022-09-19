using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public class VAInline
{
	private Dictionary<string, string> getSynonymList() {
		Dictionary<string, string> soundGroupSynonymList = new Dictionary<string, string>();
		soundGroupSynonymList.Add("Firing Countermeasure", "Firing Counter");
		soundGroupSynonymList.Add("Firing Flare", "Firing Highlight");
		soundGroupSynonymList.Add("Firing Hack", "Firing Hacking");
		return soundGroupSynonymList;
	}

	private string[] getSoundFileGroupList() {
		return new string[] {
			"Non-Verbal Error",
			"Non-Verbal Confirmation",
			"Switch Companion Target",
			"Switch Companion Source",
			"Listening Enabled",
			"Listening Disabled",
			"Annoyed Response",
			"Hello",
			"My name is",
			"I am",
			"You're welcome",
			"Handover",
			"Constellations",
			"Planets",
			"Stars",
			"Quantum Theory",
			"Galaxapedia",
			"Enable Constellations",
			"Enable Quantum Theory",
			"Enable Galaxapedia",
			"Disable Constellations",
			"Disable Quantum Theory",
			"Disable Galaxapedia",
			"Acknowledged",
			"Red Alert",
			"Yellow Alert",
			"Evasive Maneuvers",
			"Display Cargo",
			"Display Ship Info",
			"Display Objectives",
			"Loading Configuration",
			"Next Target",
			"Cancel",
			"Sending Message",
			"Typing",
			"Screenshot",
			"Show Map",
			"Autopilot Disengaged",
			"Autopilot",
			"Hyperspace",
			"Cruise Control",
			"Propulsion Enhancer",
			"Engines Max",
			"Engines Full Power",
			"Engines Full Stop",
			"General Error",
			"Equip Weapon",
			"Configure Weapon Group",
			"Configuration Complete",
			"Firing Error",
			"Cease Fire",
			"Firing Group Generic",
			"Firing Group 1",
			"Firing Group 2",
			"Firing Group 3",
			"Firing Ambush",
			"Firing Counter",
			"Firing Missile",
			"Firing Torpedo",
			"Firing Mine",
			"Firing Mining Laser",
			"Firing Beacon",
			"Firing Escape",
			"Firing Highlight",
			"Firing Hacking"
		};
	}




	public void main()
	{
		TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

		int fileCount = 0;
		string realFilePath, groupName;
		List<string> constellationList = new List<string>();
		List<string> planetList = new List<string>();
		List<string> starList = new List<string>();
		List<string> quantumTheoryList = new List<string>();
		List<string> galaxapediaList = new List<string>();
		string[] soundGroupList = getSoundFileGroupList();
		Dictionary<string, string> soundGroupSynonymList = getSynonymList();

		//*** Weapon group variables
		string wgNameListStr = VA.GetText(">>activeWeaponGroupList");
		if (wgNameListStr == null)  wgNameListStr = "";
		string[] wgNameList = wgNameListStr.Split(';');


		//*** Loop through weapon groups and add synonyms to the generic
		//*** firing group for weapons which do not exists explicitely
		foreach (string wgName in wgNameList) {
			groupName = "Firing " + ti.ToTitleCase(wgName);
			if (!Array.Exists(soundGroupList, element => element == groupName) && !soundGroupSynonymList.ContainsKey(groupName)) {
				soundGroupSynonymList.Add(groupName, "Firing Group Generic");
			}
		}

		//*** Set the sound group synonym variables
		foreach (KeyValuePair<string, string> synonymPair in soundGroupSynonymList) {
			VA.SetText(">soundGroupSynonymList[" + synonymPair.Key + "]", synonymPair.Value);
		}


		realFilePath = VA.ParseTokens(@"{VA_SOUNDS}\{TXT:>>voiceDir}\");
		foreach (string soundGroup in soundGroupList) {
			fileCount = 0;
			try {
	            string[] files = Directory.GetFiles(realFilePath + soundGroup, "*.mp3");
				fileCount = files.Length;
				if (soundGroup == "Constellations") {
					foreach (string filePath in files) {
						constellationList.Add(Path.GetFileNameWithoutExtension(filePath));
					}
				} else if (soundGroup == "Quantum Theory") {
					foreach (string filePath in files) {
						quantumTheoryList.Add(Path.GetFileNameWithoutExtension(filePath));
					}
				} else if (soundGroup == "Galaxapedia") {
					foreach (string filePath in files) {
						galaxapediaList.Add(Path.GetFileNameWithoutExtension(filePath));
					}
				}
				files = Directory.GetFiles(realFilePath + soundGroup, "*.wav");
				fileCount += files.Length;
	        } catch (Exception e) {
//				VA.WriteToLog("Failed getting file list from directory " + realFilePath + soundGroup, "Red");
			}

			VA.SetText(">soundGroupDirList[" + soundGroup + "]", @"{VA_SOUNDS}\{TXT:>>voiceDir}\" + soundGroup);
			VA.SetInt(">soundGroupDirList[" + soundGroup + "].fileCount", fileCount);

			if (constellationList.Count > 0 && VA.GetBoolean(">>constellationsEnabled") == true) {
				VA.SetText(">>constellationList", string.Join<string>(";", constellationList));
			} else {
				VA.SetText(">>constellationList", "the constellation list is disabled");
			}
			if (quantumTheoryList.Count > 0 && VA.GetBoolean(">>quantumTheoryEnabled") == true) {
				VA.SetText(">>quantumTheoryList", string.Join<string>(";", quantumTheoryList));
			} else {
				VA.SetText(">>quantumTheoryList", "the quantum theory list is disabled");
			}
			if (galaxapediaList.Count > 0 && VA.GetBoolean(">>galaxapediaEnabled") == true) {
				VA.SetText(">>galaxapediaList", string.Join<string>(";", galaxapediaList));
			} else {
				VA.SetText(">>galaxapediaList", "the galaxapedia list is disabled");
			}

			if (fileCount == 0) {
				if (!String.IsNullOrEmpty(realFilePath) && realFilePath.IndexOf("sf-i_Null") == 0)
					VA.WriteToLog("Playback identifier " + soundGroup + " contains no sound files.", "Red");
			}
		}
	}
}
