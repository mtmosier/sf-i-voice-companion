using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;



public class VAInline
{
    public void main()
    {
    	//*** Determine whether loading custom keybinds is enabled
    	bool? loadKeybinds_N = VA.GetBoolean(">loadCustomKeybindsFromStarfighterSaveData");

        if (loadKeybinds_N.HasValue && loadKeybinds_N.Value == true) {
            int keybindsLoadedCount = 0;
            Dictionary<string,int> keybindList = ReadKeybindsFromFile();

            foreach (KeyValuePair<string,int> kvp in keybindList) {
                string keybind = kvp.Key;
                int key = kvp.Value;
                string keyStr;
                bool unknownKey = false;

                if (key > 31 && key < 127) {
                    keyStr = ((char)key).ToString();
                } else {
                    switch (key) {
                        case 8:
                            keyStr = "[BACKSPACE]";
                            break;
                        case 9:
                            keyStr = "[TAB]";
                            break;
                        case 10:
                            keyStr = "[ENTER]";
                            break;
                        case 19:
                            keyStr = "[BREAK]";
                            break;
                        case 27:
                            keyStr = "[ESC]";
                            break;
                        case 127:
                            keyStr = "[DEL]";
                            break;
                        case 256:
                            keyStr = "[NUM0]";
                            break;
                        case 257:
                            keyStr = "[NUM1]";
                            break;
                        case 258:
                            keyStr = "[NUM2]";
                            break;
                        case 259:
                            keyStr = "[NUM3]";
                            break;
                        case 260:
                            keyStr = "[NUM4]";
                            break;
                        case 261:
                            keyStr = "[NUM5]";
                            break;
                        case 262:
                            keyStr = "[NUM6]";
                            break;
                        case 263:
                            keyStr = "[NUM7]";
                            break;
                        case 264:
                            keyStr = "[NUM8]";
                            break;
                        case 265:
                            keyStr = "[NUM9]";
                            break;
                        case 266:
                            keyStr = "[NUM.]";
                            break;
                        case 267:
                            keyStr = "[NUM/]";
                            break;
                        case 268:
                            keyStr = "[NUM*]";
                            break;
                        case 269:
                            keyStr = "[Num-]";
                            break;
                        case 270:
                            keyStr = "[NUM+]";
                            break;
                        case 271:
                            keyStr = "[NUMENTER]";
                            break;
                        // case 272:
                        //     keyStr = "[NUM??]";  //*** Either num insert or delete
                        //     break;
                        case 273:
                            keyStr = "[ARROWU]";
                            break;
                        case 274:
                            keyStr = "[ARROWD]";
                            break;
                        case 275:
                            keyStr = "[ARROWR]";
                            break;
                        case 276:
                            keyStr = "[ARROWL]";
                            break;
                        case 277:
                            keyStr = "[INSERT]";
                            break;
                        case 278:
                            keyStr = "[HOME]";
                            break;
                        case 280:
                            keyStr = "[PAGEUP]";
                            break;
                        case 281:
                            keyStr = "[PAGEDOWN]";
                            break;
                        case 282:
                            keyStr = "[F1]";
                            break;
                        case 283:
                            keyStr = "[F2]";
                            break;
                        case 284:
                            keyStr = "[F3]";
                            break;
                        case 285:
                            keyStr = "[F4]";
                            break;
                        case 286:
                            keyStr = "[F5]";
                            break;
                        case 287:
                            keyStr = "[F6]";
                            break;
                        case 288:
                            keyStr = "[F7]";
                            break;
                        case 289:
                            keyStr = "[F8]";
                            break;
                        case 290:
                            keyStr = "[F9]";
                            break;
                        case 291:
                            keyStr = "[F10]";
                            break;
                        case 292:
                            keyStr = "[F11]";
                            break;
                        case 293:
                            keyStr = "[F12]";
                            break;
                        case 300:
                            keyStr = "[NUMLOCK]";
                            break;
                        case 301:
                            keyStr = "[CAPSLOCK]";
                            break;
                        case 302:
                            keyStr = "[SCRLOCK]";
                            break;
                        case 303:
                            keyStr = "[RSHIFT]";
                            break;
                        case 304:
                            keyStr = "[LSHIFT]";
                            break;
                        case 305:
                            keyStr = "[RCTRL]";
                            break;
                        case 306:
                            keyStr = "[LCTRL]";
                            break;
                        case 307:
                            keyStr = "[RALT]";
                            break;
                        case 308:
                            keyStr = "[LALT]";
                            break;
                        case 309:
                            keyStr = "[RWIN]";
                            break;
                        case 310:
                            keyStr = "[LWIN]";
                            break;
                        // case 317:
                        //     keyStr = "[SYSREQ]";  //*** I don't believe this is present in VA keypress input keys
                        //     break;
                        // case 319:
                        //     keyStr = "[MENU]";  //*** I don't believe this is present in VA keypress input keys
                        //     break;
                        default:
                            if (key > 127)  unknownKey = true;
                            keyStr = "[" + key + "]";
                            break;
                    }
                }

                if (unknownKey) {
                    VA.WriteToLog(String.Format("{0}: key {1} is unknown. Using the default value instead.", keybind, keyStr), "Red");
                } else {
                    keybindsLoadedCount++;
                    VA.SetText(">>keyBind(" + keybind + ")", keyStr);
                }
            }

            if (keybindsLoadedCount > 0) {
                VA.WriteToLog(String.Format("{0} Keybinds were loaded from {1}", keybindsLoadedCount, GetFilepathToRead()), "Green");
            }
        } else {
            VA.WriteToLog("Loading keybinds from SF:I saved data file is disabled. Using default values.", "Orange");
        }
    }



    public Dictionary<string,int> ReadKeybindsFromFile() {
        Dictionary<string,int> rtnList = new Dictionary<string,int>();
        string filepath = GetFilepathToRead();
        byte[] bytesFromFile = File.ReadAllBytes(filepath);

        string[] keybindNameList = GetKeybindNameList();
        Dictionary<string,string> keybindNameMap = GetKeybindNameMappingList();

        int offset = 0;
        foreach (string keybind in keybindNameList) {
            if (keybindNameMap.ContainsKey(keybind)) {
                string internalKB = keybindNameMap[keybind];
                if (!String.IsNullOrEmpty(internalKB)) {
                    int kbLen = keybind.Length;
                    string search = (char)kbLen + keybind;
                    byte[] searchBytes = Encoding.ASCII.GetBytes(search);

                    int idx = FindBytes(bytesFromFile, offset, searchBytes);
                    if (idx >= 0) {
                        if (keybind == "Accel" && offset == 0)
                            offset = idx;

                        byte[] kbBytes = new byte[4];
                        Buffer.BlockCopy(bytesFromFile, idx + kbLen + 1, kbBytes, 0, 4);

                        int key = BitConverter.ToInt32(kbBytes, 0);
                        rtnList.Add(internalKB, key);
                    } else {
                        VA.WriteToLog(String.Format("{0} not found", keybind), "Red");
                    }
                }
            }
        }

        return rtnList;
    }

    public static string[] GetKeybindNameList() {
       return new string[] {
           "Accel",
           "Brake",
           "Left",
           "Right",
           "Fire1",
           "Fire2",
           "SpeedBoost",
           "Aug",
           "Corkscrew",
           "Action",
           "Cruise Control",
           "Fine Aiming",
           "Autopilot",
           "Hyperspace",
           "Radar",
           "Switch Target",
           "Camera Mode",
           "Keys",
           "Options",
           "Objectives",
           "Map",
           "Inventory",
           "Console",
           "Ship Info",
           "Next Secondary",
           "Prev Secondary",
           "Screenshot",
           "Chat Mode",
           "Chat Channel",
           "Zoom In",
           "Zoom Out",
           "Look Left",
           "Look Right",
           "VR Options"
       };
    }

    public static Dictionary<string,string> GetKeybindNameMappingList() {
        return new Dictionary<string,string>
        {
            { "Fine Aiming", "FineAiming" },
            { "Console", "Console" },
            { "VR Options", "VROptions" },
        	{ "Accel", "Accelerate" },
        	{ "Brake", "Reverse" },
        	{ "Left", "TurnLeft" },
        	{ "Right", "TurnRight" },
        	{ "Fire1", "PrimaryFire" },
        	{ "Fire2", "SecondaryFire" },
        	{ "SpeedBoost", "UsePropulsionEnhancer" },
        	{ "Aug", "UseAugmentation" },
        	{ "Corkscrew", "Corkscrew" },
        	{ "Action", "Action" },
        	{ "Cruise Control", "CruiseControl" },
        	{ "Autopilot", "Autopilot" },
        	{ "Hyperspace", "Hyperspace" },
        	{ "Radar", "Radar" },
        	{ "Switch Target", "NextTarget" },
        	{ "Camera Mode", "CameraMode" },
        	{ "Keys", "ShowKeys" },
        	{ "Options", "Options" },
        	{ "Objectives", "Objectives" },
        	{ "Map", "Map" },
        	{ "Inventory", "Inventory" },
        	{ "Ship Info", "ShipInformation" },
        	{ "Next Secondary", "NextSecondary" },
        	{ "Prev Secondary", "PreviousSecondary" },
        	{ "Screenshot", "Screenshot" },
        	{ "Chat Mode", "ExpandChat" },
        	{ "Chat Channel", "CycleChatWindows" },
        	{ "Zoom In", "ZoomIn" },
        	{ "Zoom Out", "ZoomOut" },
        	{ "Look Left", "PanCameraLeft" },
        	{ "Look Right", "PanCameraRight" }
        };
    }

    //*** https://stackoverflow.com/questions/5132890/c-sharp-replace-bytes-in-byte
    public static int FindBytes(byte[] src, int srcOffset, byte[] find)
    {
        int index = -1;
        int matchIndex = 0;
        // handle the complete source array
        for (int i = srcOffset; i < src.Length; i++) {
            if (src[i] == find[matchIndex]) {
                if (matchIndex == (find.Length - 1)) {
                    index = i - matchIndex;
                    break;
                }
                matchIndex++;
            } else if (src[i] == find[0]) {
                matchIndex = 1;
            } else {
                matchIndex = 0;
            }
        }
        return index;
    }



    public static string GetFilepathToRead() {
        Guid localLowId = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");

        string filepath = GetKnownFolderPath(localLowId);
        filepath = Path.Combine(filepath, "Ben Olding Games");
        filepath = Path.Combine(filepath, "Starfighter_ Infinity");
        filepath = Path.Combine(filepath, "steam_global.dat");
        return filepath;
    }

    //*** https://stackoverflow.com/a/4495081
    public static string GetKnownFolderPath(Guid knownFolderId)
    {
        IntPtr pszPath = IntPtr.Zero;
        try
        {
            int hr = SHGetKnownFolderPath(knownFolderId, 0, IntPtr.Zero, out pszPath);
            if (hr >= 0)
                return Marshal.PtrToStringAuto(pszPath);
            throw Marshal.GetExceptionForHR(hr);
        }
        finally
        {
            if (pszPath != IntPtr.Zero)
                Marshal.FreeCoTaskMem(pszPath);
        }
    }

    [DllImport("shell32.dll")]
    static extern int SHGetKnownFolderPath( [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);
}
