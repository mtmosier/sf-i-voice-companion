using System;
using System.Globalization;

public class VAInline
{
	private Guid playRandomSoundGuid;

	public void main()
	{
		//*** INITIALIZE
		TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
		string groupName, groupNum;
		string keybind;
		bool isStaticGroup = false;

		//*** GET RELEVANT SETTINGS
		string activeShipName = VA.GetText(">>activeShipName");

		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (tmpCmdId != null && tmpCmdId != "")  playRandomSoundGuid = new Guid(tmpCmdId);

		//*** Static Group List
		string variable = VA.GetText(">>staticGroupList");
		string[] staticGroupList = variable.Split(';');

		//*** GET PARAMETERS
		groupName = VA.GetText("~~wgGroupNameExtStr");
		groupNum = VA.GetText("~~wgGroupNumExtStr");
		if (String.IsNullOrEmpty(groupName)) {
			groupName = VA.Command.Segment(1);
			groupNum = VA.Command.Segment(2);
		}

		if (String.IsNullOrEmpty(groupName) || groupName.Equals("Group", StringComparison.OrdinalIgnoreCase) || groupName.Equals("Weapon Group", StringComparison.OrdinalIgnoreCase))
			groupName = "Default";
		groupName = ti.ToTitleCase(groupName);

		if (Array.IndexOf(staticGroupList, groupName) >= 0) {
			isStaticGroup = true;
			groupNum = "1";
		} else if (String.IsNullOrEmpty(groupNum)) {
			groupNum = "1";
		} else if (groupName.Equals("eighths", StringComparison.OrdinalIgnoreCase) || groupName.Equals("ate", StringComparison.OrdinalIgnoreCase)) {
			groupNum = "8";
		}

		string output = "";
		if (groupName != "Default") {
			output += groupName + " ";
		} else {
			output += "Group ";
		}
		if (groupNum != "1")  output += groupNum + " ";

		string tmpVarName = ">>shipInfo[" + activeShipName + "].weaponGroup[" + groupName + " " + groupNum + "]";
		int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
		if (VA.GetBoolean(tmpVarName + ".isActive") == true && lenN.HasValue && lenN.Value > 0) {
			output += "will fire ";
			for (short i = 0; i < lenN.Value; i++) {

				keybind = VA.GetText(tmpVarName + ".weaponKeyPressFriendly[" + i + "]");
				if (String.IsNullOrEmpty(keybind)) {
					keybind = VA.GetText(tmpVarName + ".weaponKeyPress[" + i + "]");

					switch (keybind) {
			            case "UseAugmentation":
							output += "augmentation" + ", ";
			                break;
			            case "UsePropulsionEnhancer":
							output += "propulsion enhancer" + ", ";
			                break;
						case "PrimaryFire":
							output += "primary weapon" + ", ";
			                break;
			            default:
							output += keybind.Replace("Slot", "Slot ") + ", ";
			                break;
			        }
				} else {
					output += keybind + ", ";
				}
			}
			output = output.Substring(0, output.Length - 2);
		} else {
			output += "is not active.";
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
