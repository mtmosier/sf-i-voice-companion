using System;
using System.IO;
using System.Collections.Generic;

public class VAInline
{
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
	}


	List<string> getCompanionNameList()
	{
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
}
