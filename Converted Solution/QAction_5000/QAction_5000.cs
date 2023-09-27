using System;
using Skyline.DataMiner.Library.Common;
using Skyline.DataMiner.Scripting;
using Skyline.Protocol.DynamicPolling;

/// <summary>
/// DataMiner QAction Class: Dynamic Polling Logic.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol)
	{
		try
		{
			var controller = new DynamicPollingController(protocol);

			var elementStatus = DynamicPollingController.GetElementAlarmStatus(protocol);

			// Only if the element is in timeout and the previous state was not in timeout.
			// If device is not responding on startup, no IPs will be provided
			if (elementStatus == AlarmLevel.Timeout && controller.Port1Ip != null && controller.Port2Ip != null)
			{
				protocol.Log("Element timeout detected. Toggling polling IP address.", LogType.Information, LogLevel.NoLogging);
				controller.TogglePollingIp();
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}