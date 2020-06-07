using System;
using System.Collections.Generic;

public class VAInline
{
	public void main()
	{
        //*** Get active ship name
        string activeShipName = VA.GetText(">>activeShipName");

		//*** Get list of ship names
		List<string> fullShipList = new List<string>();
		List<string> activeShipList = new List<string>();

		string shipNameInputStr = VA.GetText(">>shipNameListStr");
		if (!String.IsNullOrEmpty(shipNameInputStr)) {
			string[] shipNameList = shipNameInputStr.Split(';');
			foreach (string sn in shipNameList) {
				if (!fullShipList.Contains(sn))
					fullShipList.Add(sn);
				if (VA.GetBoolean(">>shipInfo[" + sn + "].isInUse") == true) {
					if (!activeShipList.Contains(sn)) {
						activeShipList.Add(sn);
					}
				}
			}
		}

        if (!activeShipList.Contains(activeShipName))
            activeShipList.Add(activeShipName);

		string shipNameInput = "[" + string.Join<string>(";", activeShipList) + ";current;active] [ship;]";
		VA.SetText(">>activeShipNameInput", shipNameInput);
	}
}
