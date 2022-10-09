using System;
using System.Collections.Generic;

public class VAInline
{
	private Guid playRandomSoundGuid;

	public void main()
	{
		//*** Initialize
		string output;
		string tmpVarName;
		int activeWeaponCount;

		//*** Get relevant settings
		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (tmpCmdId != null && tmpCmdId != "")  playRandomSoundGuid = new Guid(tmpCmdId);

		//*** Ship variables
		string variable = VA.GetText(">>shipNameListStr");
		string[] shipNameList = variable.Split(';');
		Dictionary<string, int> shipActiveWeaponCount = new Dictionary<string, int>();

		//*** Get list of Weapon group names
		variable = VA.GetText(">>weaponGroupListStr");
		if (string.IsNullOrEmpty(variable))  variable = "";
		List<string> activeWeaponGroupList = new List<string>(variable.Split(';'));


		//*** Loop through the ships and get a coupon of active weapon groups for each
		for (short s = 0; s < shipNameList.Length; s++) {
			if (VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse") == true) {
				VA.WriteToLog(shipNameList[s] + " > isInUse: " + VA.GetBoolean(">>shipInfo[" + shipNameList[s] + "].isInUse"), "Orange");

				activeWeaponCount = 0;
				foreach (string wgName in activeWeaponGroupList) {
					tmpVarName = ">>shipInfo[" + shipNameList[s] + "].weaponGroup[" + wgName + "]";
					if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
						int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
						if (lenN.HasValue && lenN.Value > 0) {
							activeWeaponCount++;
						}
					}
				}
				if (activeWeaponCount > 0) {
					shipActiveWeaponCount.Add(shipNameList[s], activeWeaponCount);
				}
			}
		}

		if (shipActiveWeaponCount.Count > 0) {
			output = "";
			foreach(KeyValuePair<string, int> shipInfo in shipActiveWeaponCount) {
				output += shipInfo.Key + " ship [with;has] " + shipInfo.Value + " [weapon;] group";
				if (shipInfo.Value != 1)  output += "s";
				output += ", ";
			}
			output = output.Substring(0, output.Length - 2);
		} else {
			output = "No ships were found with active weapon groups.";
		}

		VA.WriteToLog(output, "Black");
		playRandomSound(output, null, true);
	}


	private void playRandomSound(string playbackText, string playbackFileGroupName, bool pauseForPlayback = true) {
		if (playRandomSoundGuid == null)  return;

		VA.SetText("~~rsText", playbackText);
		VA.SetText("~~rsFileGroupName", playbackFileGroupName);
		VA.Command.Execute(playRandomSoundGuid, pauseForPlayback, true);
	}
}
