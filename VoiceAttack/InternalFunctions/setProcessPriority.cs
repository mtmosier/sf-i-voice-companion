// System.dll

using System.Diagnostics;

public class VAInline
{
	public void main()
	{
		using (Process p = Process.GetCurrentProcess())
			p.PriorityClass = ProcessPriorityClass.High;
	}
}
