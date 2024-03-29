using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

public class VAInline
{
	public void main()
	{
		//*** Initialize
		DateTime localDate = DateTime.Now;
		String message;

		message = localDate.ToString("u");
		VA.WriteToLog(message, "Pink");

		message = "shipNameList: " + VA.GetText(">>shipNameList");
		VA.WriteToLog(message, "Red");

		message = "shipNameListStr: " + VA.GetText(">>shipNameListStr");
		VA.WriteToLog(message, "Red");

		message = "activeShipNameInput: " + VA.GetText(">>activeShipNameInput");
		VA.WriteToLog(message, "Red");

		message = "configShipNameInput: " + VA.GetText(">>configShipNameInput");
		VA.WriteToLog(message, "Red");

		message = "weaponGroupListStr: " + VA.GetText(">>weaponGroupListStr");
		VA.WriteToLog(message, "Blue");

		message = "activeWeaponGroupInput: " + VA.GetText(">>activeWeaponGroupInput");
		VA.WriteToLog(message, "Blue");

		message = "staticGroupList: " + VA.GetText(">>staticGroupList");
		VA.WriteToLog(message, "Green");

		message = "activeStaticGroupList: " + VA.GetText(">>activeStaticGroupList");
		VA.WriteToLog(message, "Green");

		message = "companionNameList: " + VA.GetText(">>companionNameList");
		VA.WriteToLog(message, "Purple");

		message = "Galaxapedia List: " + VA.GetText(">>galaxapediaList");
		VA.WriteToLog(message, "Yellow");

		message = "Constellation List: " + VA.GetText(">>constellationList");
		VA.WriteToLog(message, "Yellow");

		message = "Quantum Theory List: " + VA.GetText(">>quantumTheoryList");
		VA.WriteToLog(message, "Yellow");

		message = "Codex Planet List: " + VA.GetText(">>codexPlanetList");
		VA.WriteToLog(message, "Yellow");

		message = "Codex Object List: " + VA.GetText(">>codexObjectList");
		VA.WriteToLog(message, "Yellow");

		message = "Codex Org List: " + VA.GetText(">>codexOrgList");
		VA.WriteToLog(message, "Yellow");
	}
}
