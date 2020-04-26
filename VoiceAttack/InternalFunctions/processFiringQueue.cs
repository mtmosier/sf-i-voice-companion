using System;
using System.Linq;
using System.Collections.Generic;

public class VAInline
{

	private List<WeaponGroup> WeaponGroupList = new List<WeaponGroup>();
	private decimal skippedKeypressPauseTime = 0.3m;
	private decimal defaultPauseTime = 0.2m;
	private decimal defaultKeyHoldTime = 0.1m;
	private bool isInitialRun = false;
	private string secondaryFireKey;
	public string prevKeybindName;


	private class WeaponGroup
	{
		public VAInline Parent;
		public string ActiveShipName;
		public string Name;
		public int GroupNum;
		private List<QueueEntry> KeyPressQueue = new List<QueueEntry>();

		// Constructor
		public WeaponGroup(VAInline parent, string name, int groupNum, string activeShipName, int executeCount = 1)
		{
			Parent = parent;
			ActiveShipName = activeShipName;
			Name = name;
			GroupNum = groupNum;

			if (!string.IsNullOrEmpty(Name) && (Name.Length <= 7 || Name.Substring(0, 7) != "keyBind")) {
				loadKeybindList(executeCount);
			}
		}

		public void addKeybind(string keybindName, int executeCount = 1, decimal requeueTime = 0m, decimal keyHoldTime = 0.1m, decimal pauseTime = 0m) {
			if (pauseTime == 0)  pauseTime = Parent.defaultPauseTime;

			KeyPressQueue.Add(
				new QueueEntry(
					keybindName,
					executeCount,
					requeueTime,
					ToString(),
					keyHoldTime,
					pauseTime
				)
			);
		}

		private void loadKeybindList(int executeCount) {
			string keybindName;

			//*** Load weapon group information
			string wgDefVarPrefix = ">>shipInfo[" + ActiveShipName + "].weaponGroup[" + Name + "][" + GroupNum + "]";
			if (Parent.VA.GetBoolean(wgDefVarPrefix + ".isActive") == true) {
				int? weaponCount = Parent.VA.GetInt(wgDefVarPrefix + ".weaponKeyPress.len");
				if (weaponCount.HasValue) {
					for (short i = 0; i < weaponCount.Value; i++) {
						keybindName = Parent.VA.GetText(wgDefVarPrefix + ".weaponKeyPress[" + i + "]");
						if (!string.IsNullOrEmpty(keybindName)) {
							addKeybind(keybindName, executeCount);
						}
					}
				}
			}

			if (KeyPressQueue.Count == 0)
				Parent.VA.WriteToLog(ToString() + " has no keybinds to load.", "Red");
		}

		public bool exportKeypress()
		{
			//*** Find the next queue entry which is ready to fire
			double timestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
			QueueEntry keybind = KeyPressQueue.Find(
				delegate(QueueEntry entry) {
					return (entry.NextRunTime <= timestamp);
				}
			);

			if (keybind != null) {

				if (!keybind.PauseQueue && !keybind.IsNoop) {
					if (Parent.prevKeybindName != keybind.KeybindName) {
						Parent.VA.SetText("~~keybindName", keybind.KeybindName);
						Parent.VA.SetDecimal("~~pauseTime", keybind.PauseTime);

						if (keybind.HoldKey) {
							Parent.VA.SetText("~~nextAction", "Hold Key");
							if (!String.IsNullOrEmpty(keybind.KeybindName))
								Parent.addKeybindToHeldKeyList(keybind.KeybindName);
						} else if (keybind.ReleaseKey) {
							Parent.VA.SetText("~~nextAction", "Release Key");
							Parent.removeKeybindFromHeldKeyList(keybind.KeybindName);
						} else {
							Parent.VA.SetDecimal("~~keyHoldTime", keybind.KeyHoldTime);
							Parent.VA.SetText("~~nextAction", "Press Key");
						}

						if (keybind.KeybindName.Substring(0, 4) == "Slot")
							Parent.prevKeybindName = keybind.KeybindName;

					} else {

						//*** No weapon groups ready to fire. Pause while we wait.
						Parent.VA.SetText("~~nextAction", "Pause");
						Parent.VA.SetDecimal("~~pauseTime", Parent.skippedKeypressPauseTime + Parent.defaultPauseTime);
					}
				}
				// else Parent.VA.WriteToLog("Time " + timestamp, "Black");

				KeyPressQueue.Remove(keybind);

				if (keybind.ExecuteCount > 0)  keybind.ExecuteCount--;
				if (keybind.ExecuteCount != 0) {
					//*** Requeue the item
					if (!keybind.PauseQueue && keybind.RequeueTime > 0)
						keybind.NextRunTime = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds + (double)(keybind.RequeueTime * 1000);

					KeyPressQueue.Add(keybind);
				}

				if (keybind.PauseQueue) {
					if (KeyPressQueue.Count == 0) {
						addKeybind("Noop");
					}

					// Parent.VA.WriteToLog("Pausing for " + keybind.PauseTime + " seconds", "Black");
					timestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
					for (short i = 0; i < KeyPressQueue.Count; i++)
						KeyPressQueue[i].NextRunTime = timestamp + (double)(keybind.PauseTime * 1000);
				}

				return !keybind.PauseQueue && !keybind.IsNoop;
			}

			return false;
		}

		public bool hasQueueEntries() { return KeyPressQueue.Count > 0; }
		public int getQueueEntryCount() { return KeyPressQueue.Count; }
		public string ToString() { return (!string.IsNullOrEmpty(Name) ? Name : "Keybind") + " " + GroupNum; }
	}





	private class QueueEntry
	{
		public string KeybindName;
		public decimal KeyHoldTime;
		public decimal PauseTime;
		public int ExecuteCount;
		public decimal RequeueTime;
		public string Source;
		public double NextRunTime;
		public bool HoldKey;
		public bool ReleaseKey;
		public bool PauseQueue;
		public bool IsNoop;

		// Constructor
		public QueueEntry(string keybindName, int executeCount = 1, decimal requeueTime = 0m, string source = "", decimal keyHoldTime = 0.1m, decimal pauseTime = 0.1m)
		{
			string[] keybindParts = new string[] {};
			if (!String.IsNullOrEmpty(keybindName))
				keybindParts = keybindName.Split(' ');

			PauseQueue = false;
			HoldKey = false;
			ReleaseKey = false;
			IsNoop = false;

			if (keybindName != "Noop") {
				if (keybindParts.Length > 1) {
					if (keybindParts[0].Equals("pause", StringComparison.OrdinalIgnoreCase)) {
						PauseQueue = true;
						keybindName = "";

						pauseTime = 2m;
						try { pauseTime = Decimal.Parse(keybindParts[1]); }
						catch (Exception e) {}

					} else {
						keybindName = keybindParts[1];
						if (keybindParts[0].Equals("hold", StringComparison.OrdinalIgnoreCase)) {
							HoldKey = true;
						} else if (keybindParts[0].Equals("release", StringComparison.OrdinalIgnoreCase)) {
							ReleaseKey = true;
						}
					}
				}
			} else {
				IsNoop = true;
				pauseTime = 0;
				keybindName = "";
			}

			KeybindName = keybindName;
			Source = source;
			ExecuteCount = executeCount;
			PauseTime = pauseTime;
			RequeueTime = requeueTime;
			KeyHoldTime = keyHoldTime;
			NextRunTime = 0D;
		}

		public string ToString()
		{
			string rtnVal;
			if (!String.IsNullOrEmpty(Source))
				rtnVal = Source;
			else
				rtnVal = "Keystroke";
			rtnVal += " / ";
			if (PauseQueue) {
				rtnVal += "Pause for " + PauseTime + "s";
			} else if (IsNoop) {
				rtnVal += "Pause anchor";
			} else {
				if (HoldKey) rtnVal += "Hold ";
				else if (ReleaseKey) rtnVal += "Release ";
				rtnVal += KeybindName;
			}
			return rtnVal;
		}
	}




	private void addWeaponGroupToQueue(string groupNameToAdd, int groupNumToAdd, string activeShipName, int executeCount = -1)
	{
		groupNameToAdd = uppercaseFirst(groupNameToAdd);

		//*** Create the weapon group object
		WeaponGroup wg = new WeaponGroup(this, groupNameToAdd, groupNumToAdd, activeShipName, executeCount);
		if (wg.hasQueueEntries()) {
			//*** Insert at the top of the queue
			WeaponGroupList.Insert(0, wg);
		}
	}

	private void addKeybindToQueue(string keybindToAdd, string activeShipName, int executeCount = 1)
	{
		keybindToAdd = uppercaseFirst(keybindToAdd);

		//*** Create a dummy weapon group containing only the single keybind
		WeaponGroup wg = new WeaponGroup(this, "keyBind(" + keybindToAdd + ")", 0, activeShipName);
		wg.addKeybind(keybindToAdd, executeCount, 0, 0.1m, 1.2m);

		//*** Insert at the top of the queue
		WeaponGroupList.Insert(0, wg);
	}

	private void purgeWeaponGroup(string groupNameToCancel, int groupNumToCancel)
	{
		groupNameToCancel = uppercaseFirst(groupNameToCancel);
		WeaponGroupList.RemoveAll(
			delegate(WeaponGroup entry) {
				return (
					entry.Name == groupNameToCancel
					&& entry.GroupNum == groupNumToCancel
				);
			}
		);
	}

	private void purgeKeybind(string keybindToCancel)
	{
		keybindToCancel = uppercaseFirst(keybindToCancel);
		WeaponGroupList.RemoveAll(
			delegate(WeaponGroup entry) {
				return (
					entry.Name == "keyBind(" + keybindToCancel + ")"
				);
			}
		);
	}

	private void pruneEmptyWeaponGroups()
	{
		WeaponGroupList.RemoveAll(
			delegate(WeaponGroup entry) {
				return !entry.hasQueueEntries();
			}
		);
	}

	private void exportNextLoopAction()
	{
		//*** Clear any existing return variables
		VA.SetText("~~keybindName", null);
		VA.SetDecimal("~~keyHoldTime", null);
		VA.SetDecimal("~~pauseTime", null);
		VA.SetText("~~nextAction", null);

		//*** Prune any empty weapon groups
		pruneEmptyWeaponGroups();

		//*** Empty queue - Perform needed housekeeping and exit
		if (WeaponGroupList.Count == 0) {

			//*** Make sure the secondary fire key is held down. If it is not
			//*** we can assume a single keystroke queue ran and the dealyed
			//*** secondary fire button has not yet occured. Press the secondary
			//*** fire button before exiting proper next loop.
			if (!isInitialRun && !string.IsNullOrEmpty(secondaryFireKey) && !VA.State.KeyDown(secondaryFireKey)) {
				if (!string.IsNullOrEmpty(prevKeybindName)) {
					VA.SetText("~~keybindName", "SecondaryFire");
					VA.SetDecimal("~~pauseTime", defaultPauseTime);
					VA.SetDecimal("~~keyHoldTime", defaultKeyHoldTime);
					VA.SetText("~~nextAction", "Press Key");
					prevKeybindName = null;
					return;
				}
			}

			//*** Send an Exit command if we have no weapon groups left in the queue
			VA.SetText("~~nextAction", "Exit");
			return;
		}

		//*** Make sure the secondary fire key is held down. If not allow one
		//*** queue keypress (which is presumably a secondary weapon slot)
		//*** first then send the hold secondary fire key next
		if (!isInitialRun) {
			if (!string.IsNullOrEmpty(secondaryFireKey) && !VA.State.KeyDown(secondaryFireKey)) {
				if (!string.IsNullOrEmpty(prevKeybindName)) {
					VA.SetText("~~keybindName", "SecondaryFire");
					VA.SetDecimal("~~pauseTime", 0.05m);
					VA.SetText("~~nextAction", "Hold Key");
					addKeybindToHeldKeyList("SecondaryFire");
					return;
				}
			}
		}

		//*** Export the next keypress in queue
		WeaponGroup executedWeaponGroup = WeaponGroupList.Find(
			delegate(WeaponGroup entry) {
				return entry.exportKeypress();
			}
		);

		if (executedWeaponGroup != null) {

			//*** Put the weapon group which fired at the end of the queue list
			WeaponGroupList.Remove(executedWeaponGroup);
			WeaponGroupList.Add(executedWeaponGroup);
			isInitialRun = false;

		} else {

			if (!string.IsNullOrEmpty(secondaryFireKey) && VA.State.KeyDown(secondaryFireKey)) {

				//*** Nothing to fire. Release the secondary fire key.
				VA.SetText("~~keybindName", secondaryFireKey);
				VA.SetDecimal("~~pauseTime", 0m);
				VA.SetText("~~nextAction", "Release Key");
				removeKeybindFromHeldKeyList(secondaryFireKey);

			} else {

				//*** No weapon groups ready to fire. Pause while we wait.
				VA.SetText("~~nextAction", "Pause");
				VA.SetDecimal("~~pauseTime", skippedKeypressPauseTime + defaultPauseTime);
			}
		}
	}




	public void main()
	{
		//*** Initialize
		int gn;
		int keybindExecuteCount, groupExecuteCount;
		int groupNumToAdd, groupNumToCancel;

		if (VA.GetBoolean("~~reset") == true) {
			WeaponGroupList = new List<WeaponGroup>();
			isInitialRun = true;
			prevKeybindName = null;
			VA.SetBoolean("~~reset", null);
		}

		//*** Get relevant settings
		string activeShipName = VA.GetText(">>activeShipName");


		//*** Get keybind to add parameters
		string keybindToAdd = VA.GetText(">secondaryFireKeybindToAdd");
		int? keybindExecuteNull = VA.GetInt(">secondaryFireKeybindExecuteCount");
		if (!string.IsNullOrEmpty(keybindToAdd)) {
			VA.SetText(">secondaryFireKeybindToAdd", null);
			VA.SetInt(">secondaryFireKeybindExecuteCount", null);
		}

		//*** Get weapon group to add parameters
		string groupNameToAdd = VA.GetText(">secondaryFireGroupNameToAdd");
		int? groupNumToAddNull = VA.GetInt(">secondaryFireGroupNumToAdd");
		int? groupExecuteNull = VA.GetInt(">secondaryFireGroupExecuteCount");
		if (!string.IsNullOrEmpty(groupNameToAdd)) {
			VA.SetText(">secondaryFireGroupNameToAdd", null);
			VA.SetInt(">secondaryFireGroupNumToAdd", null);
			VA.SetInt(">secondaryFireGroupExecuteCount", null);
		}

		//*** Get keybind to cancel parameter
		string keybindToCancel = VA.GetText(">secondaryFireKeybindToCancel");
		VA.SetText(">secondaryFireKeybindToCancel", null);

		//*** Get weapon group to cancel parameters
		string groupNameToCancel = VA.GetText(">secondaryFireGroupNameToCancel");
		int? groupNumToCancelNull = VA.GetInt(">secondaryFireGroupNumToCancel");
		if (!string.IsNullOrEmpty(groupNameToCancel)) {
			VA.SetText(">secondaryFireGroupNameToCancel", null);
			VA.SetInt(">secondaryFireGroupNumToCancel", null);
		}


		//*** For nullable ints set the real variable after checking null status
		keybindExecuteCount = keybindExecuteNull.HasValue ? keybindExecuteNull.Value : 0;
		groupExecuteCount = groupExecuteNull.HasValue ? groupExecuteNull.Value : 0;
		groupNumToAdd = groupNumToAddNull.HasValue ? groupNumToAddNull.Value : 1;
		groupNumToCancel = groupNumToCancelNull.HasValue ? groupNumToCancelNull.Value : 1;

		//*** Debug
		// VA.WriteToLog("keybindToAdd: " + keybindToAdd, "Pink");
		// VA.WriteToLog("keybindExecuteCount: " + keybindExecuteCount, "Pink");
		// VA.WriteToLog("groupNameToAdd: " + groupNameToAdd, "Pink");
		// VA.WriteToLog("groupNumToAdd: " + groupNumToAdd, "Pink");
		// VA.WriteToLog("groupExecuteCount: " + groupExecuteCount, "Pink");
		// VA.WriteToLog("groupNameToCancel: " + groupNameToCancel, "Pink");
		// VA.WriteToLog("groupNumToCancel: " + groupNumToCancel, "Pink");
		// VA.WriteToLog("activeShipName: " + activeShipName, "Pink");

		if (string.IsNullOrEmpty(secondaryFireKey)) {
			secondaryFireKey = VA.GetText(">>keyBind(SecondaryFire)");
		}


		//*** Remove weapon group from the queue
		if (!string.IsNullOrEmpty(keybindToCancel))
			purgeKeybind(keybindToCancel);

		//*** Remove weapon group from the queue
		if (!string.IsNullOrEmpty(groupNameToCancel))
			purgeWeaponGroup(groupNameToCancel, groupNumToCancel);

		//*** Add passed in keybind to the queue
		if (!string.IsNullOrEmpty(keybindToAdd))
			addKeybindToQueue(keybindToAdd, activeShipName, keybindExecuteCount);

		//*** Add weapon group to the queue
		if (!string.IsNullOrEmpty(groupNameToAdd))
			addWeaponGroupToQueue(groupNameToAdd, groupNumToAdd, activeShipName, groupExecuteCount);


		//*** Data management done - Now execute the next loop action
		exportNextLoopAction();
	}

	List<string> getCurrentlyHeldKeybindList() {
		List<string> rtnList = new List<string>();

		int? currentlyHeldKeybindListLen_N = VA.GetInt(">>currentlyHeldKeybindList.len");
		if (currentlyHeldKeybindListLen_N.HasValue && currentlyHeldKeybindListLen_N.Value > 0) {
			for (int i = 0; i < currentlyHeldKeybindListLen_N.Value; i++) {
				string keybindVar = VA.GetText(">>currentlyHeldKeybindList[" + i + "]");
				if (!String.IsNullOrEmpty(keybindVar))
					rtnList.Add(keybindVar);
			}
		}

		return rtnList;
	}

	void setCurrentlyHeldKeybindList(List<string> currentlyHeldKeybindList) {
		for (int i = 0; i < currentlyHeldKeybindList.Count; i++) {
			VA.SetText(">>currentlyHeldKeybindList[" + i + "]", currentlyHeldKeybindList[i]);
		}
		VA.SetInt(">>currentlyHeldKeybindList.len", currentlyHeldKeybindList.Count);
	}

	void addKeybindToHeldKeyList(string keybindName) {
		List<string> currentlyHeldKeybindList = getCurrentlyHeldKeybindList();

		if (!currentlyHeldKeybindList.Contains(keybindName)) {
			currentlyHeldKeybindList.Add(keybindName);
			setCurrentlyHeldKeybindList(currentlyHeldKeybindList);
		}
	}

	void removeKeybindFromHeldKeyList(string keybindName) {
		List<string> currentlyHeldKeybindList = getCurrentlyHeldKeybindList();
		if (currentlyHeldKeybindList.Contains(keybindName)) {
			currentlyHeldKeybindList.RemoveAll(x => x == keybindName);
			setCurrentlyHeldKeybindList(currentlyHeldKeybindList);
		}
	}

	static string uppercaseFirst(string s) {
        if (string.IsNullOrEmpty(s))  return "";
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
