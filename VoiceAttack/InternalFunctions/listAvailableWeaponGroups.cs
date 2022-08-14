// Microsoft.CSharp.dll;System.dll;System.Core.dll;System.Data.dll;System.Data.DataSetExtensions.dll;System.Deployment.dll;System.Drawing.dll;System.Net.Http.dll; System.Windows.Forms.dll;System.Xml.dll;System.Xml.Linq.dll

using System;
using System.Linq;

public class VAInline
{
	private Guid playRandomSoundGuid;

	public void main()
	{
		//*** Initialize
		string output;

		//*** Get relevant settings
		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (tmpCmdId != null && tmpCmdId != "")  playRandomSoundGuid = new Guid(tmpCmdId);

		//*** Ship variables
		string variable = VA.GetText(">>weaponGroupListStr");
		if (variable == null)  variable = "";
		string[] wgNameList = variable.Split(';');

		//*** Static Group List
		variable = VA.GetText(">>staticGroupList");
		if (variable == null)  variable = "";
		string[] staticGroupList = variable.Split(';');


		if (wgNameList.Count() > 0) {
			output = "Available weapon groups: ";
			output += String.Join(", ", wgNameList);
			if (staticGroupList.Count() > 0)
				output += ". You can also configure actions for " + String.Join(", ", staticGroupList);
		} else {
			output = "No named weapon groups found.";
			if (staticGroupList.Count() > 0)
				output += " You can configure actions for " + String.Join(", ", staticGroupList);
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
