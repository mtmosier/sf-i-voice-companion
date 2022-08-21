using System;
using System.Collections.Generic;

public class VAInline
{
	private Guid playRandomSoundGuid;

	public void main()
	{
		//*** GET RELEVANT SETTINGS
		string activeShipName = VA.GetText(">>activeShipName");

		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (tmpCmdId != null && tmpCmdId != "")  playRandomSoundGuid = new Guid(tmpCmdId);

		string variable = VA.GetText(">>activeStaticGroupList");
		if (string.IsNullOrEmpty(variable))  variable = "";
		List<string> activeStaticGroupList = new List<string>(variable.Split(';'));

		variable = VA.GetText(">>activeWeaponGroupList");
		if (string.IsNullOrEmpty(variable))  variable = "";
		List<string> activeWeaponGroupList = new List<string>(variable.Split(';'));


		//*** CHECK COMMAND PARAMETERS
		bool verbose = false;
		string testStr = VA.Command.Segment(3);
		if (!string.IsNullOrEmpty(testStr)) {
			verbose = true;
		}

		string output = "";
		if (verbose) {
			output += getWeaponGroupListDescription(activeShipName, activeWeaponGroupList);
			output += getWeaponGroupListDescription(activeShipName, activeStaticGroupList, true);
		} else {
			if (activeWeaponGroupList.Count > 0)
				output += string.Join<string>(", ", activeWeaponGroupList) + ", ";
			if (activeStaticGroupList.Count > 0)
				output += string.Join<string>(", ", activeStaticGroupList) + ", ";
		}

		if (string.IsNullOrEmpty(output)) {
			output = "No active weapon groups found.";
		} else {
			output = output.Substring(0, output.Length - 2);
		}

		VA.WriteToLog(output, "Black");
		playRandomSound(output, null, false);
	}


	private string getWeaponGroupListDescription(string activeShipName, List<string> weaponGroupList, bool isStaticGroupList = false)
	{

		string output = "";
		foreach (string wgName in weaponGroupList) {
			string tmpVarName = ">>shipInfo[" + activeShipName + "].weaponGroup[" + wgName + "]";

			if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
				int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
				if (lenN.HasValue && lenN.Value > 0) {
					output += wgName + " has " + lenN.Value + " action";
					output += (lenN.Value == 1 ? "" : "s") + ", ";
				}
			}
		}

		return output;
	}


	private void playRandomSound(string playbackText, string playbackFileGroupName, bool pauseForPlayback = true) {
		if (playRandomSoundGuid == null)  return;

		VA.SetText("~~rsText", playbackText);
		VA.SetText("~~rsFileGroupName", playbackFileGroupName);
		VA.Command.Execute(playRandomSoundGuid, pauseForPlayback, true);
	}
}
