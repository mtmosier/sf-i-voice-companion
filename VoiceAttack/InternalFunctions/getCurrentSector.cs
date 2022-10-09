using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;



public class VAInline
{
	public void main()
	{
		string pattern = "*_Travel.txt";
		DirectoryInfo dirInfo = new DirectoryInfo(GetGameLogDirectory());
		try {
			//*** Pick out the most recently updated file ending with "_Travel.txt"
			FileInfo file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).First();
			string currentSector = GetLastSectorFromTravelFile(file);
			VA.SetText("~~currentSector", currentSector);
			// VA.WriteToLog("Sector Read: " + currentSector, "Pink");

		} catch (InvalidOperationException) {
			//*** Travel log not found
			VA.WriteToLog("Unable to determine player's current sector.", "Red");
			VA.SetText("~~currentSector", "");
		}
	}



	private string GetLastSectorFromTravelFile(FileInfo fileInfo)
	{
		string rtnVal = null;
		long filesize = fileInfo.Length;
		string travelLogRegexStr = @"\s(\w+-\d+-\d+)\s";
		Regex travelLogRegex = new Regex(travelLogRegexStr, RegexOptions.RightToLeft);

		using (FileStream stream = File.OpenRead(fileInfo.FullName)) {
			if (filesize > 512) {
				stream.Seek(-512, SeekOrigin.End);
			}

			using (StreamReader textReader = new StreamReader(stream)) {
				string fileContents = textReader.ReadToEnd();
				Match match = travelLogRegex.Match(fileContents);
				if (match.Success) {
					rtnVal = match.Groups[1].ToString();
				}
			}
		}

		return rtnVal;
	}



	public static string GetGameLogDirectory() {
		Guid localLowId = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");

		string filepath = GetKnownFolderPath(localLowId);
		filepath = Path.Combine(filepath, "Ben Olding Games");
		filepath = Path.Combine(filepath, "Starfighter_ Infinity");
		filepath = Path.Combine(filepath, "logs");
		return filepath;
	}

	//*** https://stackoverflow.com/a/4495081
	public static string GetKnownFolderPath(Guid knownFolderId)
	{
		IntPtr pszPath = IntPtr.Zero;
		try {
			int hr = SHGetKnownFolderPath(knownFolderId, 0, IntPtr.Zero, out pszPath);
			if (hr >= 0)
				return Marshal.PtrToStringAuto(pszPath);
			throw Marshal.GetExceptionForHR(hr);
		} finally {
			if (pszPath != IntPtr.Zero)
				Marshal.FreeCoTaskMem(pszPath);
		}
	}

	[DllImport("shell32.dll")]
	static extern int SHGetKnownFolderPath( [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);
}
