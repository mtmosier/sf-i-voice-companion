using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class VAInline
{
	private Guid playRandomSoundGuid;
	private string notFoundText = "No information available";
	private Random rand = new Random();

	public void main()
	{
		string tmpCmdId = VA.GetText(">playRandomSoundCommandId");
		if (!string.IsNullOrEmpty(tmpCmdId))  playRandomSoundGuid = new Guid(tmpCmdId);

		string scanType = VA.Command.Segment(3);
		string codexLookupVar = "";
		int codexLookupIndex = -1;
		int? scanCount;

		if (scanType == "planet") {
			scanCount = VA.GetInt(">>codexPlanetDescription.len");
			if ((scanCount ?? 0) == 0)  return;
			codexLookupVar = ">>codexPlanetDescription";
			codexLookupIndex = rand.Next(0, scanCount.Value);

		} else if (scanType == "object") {
			scanCount = VA.GetInt(">>codexObjectDescription.len");
			if ((scanCount ?? 0) == 0)  return;
			codexLookupVar = ">>codexObjectDescription";
			codexLookupIndex = rand.Next(0, scanCount.Value);

		} else if (scanType == "race" || scanType == "org" || scanType == "organization") {
			scanCount = VA.GetInt(">>codexOrgDescription.len");
			if ((scanCount ?? 0) == 0)  return;
			codexLookupVar = ">>codexOrgDescription";
			codexLookupIndex = rand.Next(0, scanCount.Value);

		} else if (scanType == "ship") {
			scanCount = VA.GetInt(">>codexShipDescription.len");
			if ((scanCount ?? 0) == 0)  return;
			codexLookupVar = ">>codexShipDescription";
			codexLookupIndex = rand.Next(0, scanCount.Value);

		} else {
			scanCount = VA.GetInt(">>codexDescriptionsCombined.len");
			if ((scanCount ?? 0) == 0)  return;

			codexLookupIndex = rand.Next(0, scanCount.Value);
			// VA.WriteToLog("Full codex search selected index " + codexLookupIndex.ToString());

			int? planetCount = VA.GetInt(">>codexPlanetDescription.len");
			int? objectCount = VA.GetInt(">>codexObjectDescription.len");
			int? orgCount = VA.GetInt(">>codexOrgDescription.len");
			int? shipCount = VA.GetInt(">>codexShipDescription.len");

			if (string.IsNullOrEmpty(codexLookupVar) && (planetCount ?? 0) > 0) {
				if (codexLookupIndex < planetCount) {
					codexLookupVar = ">>codexPlanetDescription";
				} else {
					codexLookupIndex -= planetCount.Value;
				}
			}
			if (string.IsNullOrEmpty(codexLookupVar) && (objectCount ?? 0) > 0) {
				if (codexLookupIndex < objectCount) {
					codexLookupVar = ">>codexObjectDescription";
				} else {
					codexLookupIndex -= objectCount.Value;
				}
			}
			if (string.IsNullOrEmpty(codexLookupVar) && (orgCount ?? 0) > 0) {
				if (codexLookupIndex < orgCount) {
					codexLookupVar = ">>codexObjectDescription";
				} else {
					codexLookupIndex -= orgCount.Value;
				}
			}
			if (string.IsNullOrEmpty(codexLookupVar) && (shipCount ?? 0) > 0) {
				if (codexLookupIndex < shipCount) {
					codexLookupVar = ">>codexShipDescription";
				} else {
					codexLookupIndex -= shipCount.Value;
				}
			}
		}

		if (!string.IsNullOrEmpty(codexLookupVar)) {
			string description = VA.GetText(codexLookupVar + "[" + codexLookupIndex.ToString() + "]");
			if (description.IndexOf(notFoundText) > 0) {
				// VA.WriteToLog("Empty codex entry found.  Trying another.  " + description);
				main();
				return;
			}

			if (!string.IsNullOrEmpty(description)) {
				playRandomSound("", "Handover", true);
				playRandomSound(description, "", false);
			}
		}
	}


	private void playRandomSound(string playbackText, string playbackFileGroupName, bool pauseForPlayback = true) {
		if (playRandomSoundGuid == null)  return;

		VA.SetText("~~rsText", playbackText);
		VA.SetText("~~rsFileGroupName", playbackFileGroupName);
		VA.Command.Execute(playRandomSoundGuid, pauseForPlayback, true);
	}
}
