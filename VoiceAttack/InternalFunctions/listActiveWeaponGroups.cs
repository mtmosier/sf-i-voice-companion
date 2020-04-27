using System;

public class VAInline
{
	private Guid playRandomSoundGuid;

	public void main()
	{
		//*** GET RELEVANT SETTINGS
		string activeShipName = VA.GetText(">>activeShipName");

		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (tmpCmdId != null && tmpCmdId != "")  playRandomSoundGuid = new Guid(tmpCmdId);

		//*** Weapon group variables
		string wgNameListStr = VA.GetText(">>weaponGroupNameList");
		string[] wgNameList = wgNameListStr.Split(';');

		//*** Number of weapon groups in use
		int? maxWGNum_N = VA.GetInt(">maxWeaponGroupNum");
		int maxWGNum = maxWGNum_N.HasValue ? maxWGNum_N.Value : 9;

		//*** Static Group List
		string variable = VA.GetText(">>staticGroupList");
		string[] staticGroupList = variable.Split(';');

		//*** CHECK COMMAND PARAMETERS
		bool verbose = false;
		string testStr = VA.Command.Segment(3);
		if (!String.IsNullOrEmpty(testStr)) {
			verbose = true;
		}

		string output = getWeaponGroupListDescription(activeShipName, wgNameList, maxWGNum, verbose);
		output += getWeaponGroupListDescription(activeShipName, staticGroupList, 1, verbose, true);

		if (String.IsNullOrEmpty(output)) {
			output = "No active weapon groups found.";
		} else {
			output = output.Substring(0, output.Length - 2);
		}

		VA.WriteToLog(output, "Black");
		playRandomSound(output, null, true);
	}


	private string getWeaponGroupListDescription(string activeShipName, string[] wgNameList, int maxWGNum = 10, bool verbose = false, bool isStaticGroupList = false) {

		string output = "";
		foreach (string wgName in wgNameList) {
			int groupCount = 0;

			for (short n = 1; n <= maxWGNum; n++) {
				string tmpVarName = ">>shipInfo[" + activeShipName + "].weaponGroup[" + wgName + "][" + n + "]";

				if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
					int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
					if (lenN.HasValue && lenN.Value > 0) {
						if (verbose) {
							if (wgName != "Default") {
								output += wgName + " ";
							} else {
								output += "Group ";
							}
							if (!isStaticGroupList)  output += n + " ";
							output += "has " + lenN.Value;
							if (!isStaticGroupList)  output += " weapon";
							else output += " action";
							output += (lenN.Value == 1 ? "" : "s") + ", ";
						} else {
							groupCount++;
						}
					}
				}
			}

			if (!verbose && groupCount > 0) {
				if (!isStaticGroupList) {
					output += wgName + " has " + groupCount + " group";
					if (groupCount != 1)  output += "s";
					output += ", ";
				} else {
					output += wgName + " has been configured, ";
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
