using System;
using System.IO;
using System.Collections.Generic;

public class VAInline
{
	List<string> getCompanionNameList() {
		List<string> rtnList = new List<string>();
		rtnList.Add("Null");

		string realFilePath = VA.ParseTokens(@"{VA_SOUNDS}");
		try {
            string[] directories = Directory.GetDirectories(realFilePath, "sf-i_*");
			foreach (string dir in directories) {
				string companion = dir.Replace(realFilePath + @"\sf-i_", "");
				if (!rtnList.Contains(companion))
					rtnList.Add(companion);
			}
        } catch (Exception e) {}

		return rtnList;
	}

	public void main()
	{
		string variable;

		//*** Ordinal variables
		string ordinalListStr = VA.GetText("~~ordinalList");
		string[] ordinalList = ordinalListStr.Split(';');

		for (short i = 0; i < ordinalList.Length; i++) {
			VA.SetText(">ordList(" + (i+1) + ")", ordinalList[i]);
		}

		//*** Set the companionNameList variable
		List<String> companionNameList = getCompanionNameList();
		VA.SetText(">>companionNameList", String.Join(";", companionNameList));

		//*** Ship variables
		variable = VA.GetText(">>shipNameListStr");
		string[] shipNameList = variable.Split(';');

		for (short i = 0; i < shipNameList.Length; i++) {
			VA.SetText(">shipName[" + i + "]", shipNameList[i]);
		}
		VA.SetInt(">shipName.len", shipNameList.Length);

		//*** Weapon group variables
		int? wgNumMax = VA.GetInt(">maxWeaponGroupNum");
		string wgNumRangeStr = "";
		string wgNumOptRangeStr = "";
		if (wgNumMax.HasValue) {
			wgNumRangeStr = "1.." + wgNumMax.Value;
			if (wgNumMax.Value >= 8)  wgNumRangeStr += ";ate;eighths";
		} else {
			wgNumRangeStr = "1..6";
		}
		wgNumOptRangeStr = wgNumRangeStr + ";";

		VA.SetText(">>wgNumRangeCmd", wgNumRangeStr);
		VA.SetText(">>wgNumOptRangeCmd", wgNumOptRangeStr);

		string wgNameListStr = VA.GetText(">>weaponGroupNameList");
		string[] wgNameList = wgNameListStr.Split(';');
		string[] wgNamePluralList = new string[wgNameList.Length];

		for (short i = 0; i < wgNameList.Length; i++) {
			VA.SetText(">>wgNameList[" + i + "]", wgNameList[i]);
			if (wgNameList[i][wgNameList[i].Length - 1] == 'o')
				wgNamePluralList[i] = wgNameList[i] + "es";
			else
				wgNamePluralList[i] = wgNameList[i] + "s";
		}
		VA.SetInt(">>wgNameList.len", wgNameList.Length);
	}
}
