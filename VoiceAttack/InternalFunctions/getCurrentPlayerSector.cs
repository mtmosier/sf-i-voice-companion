using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;



public class VAInline
{
    public void main()
    {
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
