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
		string variable = VA.GetText(">>shipNameListStr");
		string[] shipNameList = variable.Split(';');

		if (shipNameList.Count() > 0) {
			output = "Available ships: ";
			output += String.Join(", ", shipNameList);
		} else {
			output = "No ships found";
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
