// System.Core.dll

using System.Linq;

public class VAInline
{

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
		string[] soundGroupList = getSoundFileGroupList();

		foreach (string groupName in soundGroupList.Reverse()) {
			VA.WriteToLog(VA.ParseTokens(">soundGroupDirList[" + groupName + "].fileCount = {INT:>soundGroupDirList[" + groupName + "].fileCount:0}"), "Black");
			VA.WriteToLog(VA.ParseTokens(">soundGroupDirList[" + groupName + "] = {TXT:>soundGroupDirList[" + groupName + "]:}"), "Black");
		}
	}
}
