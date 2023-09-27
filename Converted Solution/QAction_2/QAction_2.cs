using System;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: After Startup Logic.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol)
	{
		
		Console.WriteLine()
		
		try
		{
			PopulateActivelyPolledIp(protocol);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}

	/// <summary>
	/// Set the Actively Polled IP (Dynamic IP) value to the Element IP Address.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void PopulateActivelyPolledIp(SLProtocol protocol)
	{
		var elementIp = Convert.ToString(protocol.GetParameter(6));
		protocol.SetParameter(Parameter.activelypolledip_207, elementIp);
	}

	// Generate funtion to calculate how many days it has been since the 1st of January 1994 using today's date
	// This is used to calculate the number of days since the last reboot
	public static int DaysSince1994()
	{
        DateTime date1 = new DateTime(1994, 1, 1);
        DateTime date2 = DateTime.Now;
        TimeSpan diff = date2.Subtract(date1);
        return diff.Days;
    }

	
}