using System;
using System.Collections.Generic;
using Core;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: Parse HTTP Responses.
/// </summary>
public static class QAction
{
	private delegate void DelegeteParser(SLProtocol protocol, string response);

	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol)
	{
		try
		{
			int iTrigger = protocol.GetTriggerParameter();
			var paramIds = new List<uint> { Parameter.statuscode_20 };
			DelegeteParser parserDelegate = null;

			switch (iTrigger)
			{
				case Parameter.parsestatisticsresponse_10:
					paramIds.Add(Parameter.statisticsresponse_21);
					parserDelegate = Parser.ParseStatistics;

					break;

				case Parameter.parseconfigurationresponse_11:
					paramIds.Add(Parameter.configurationresponse_22);
					parserDelegate = Parser.ParseConfiguration;
					break;

				case Parameter.parseconfigurationstreamresponse_12:
					paramIds.Add(Parameter.configurationstreamresponse_23);
					parserDelegate = Parser.ParseConfigurationStream;
					break;

				default:
					protocol.Log("QA" + protocol.QActionID + "|Run|Case not supported: " + iTrigger, LogType.Error, LogLevel.NoLogging);
					break;
			}

			object[] aoParameter = (object[])protocol.GetParameters(paramIds.ToArray());
			string sStatus = Convert.ToString(aoParameter[0]);
			string sResponse = Convert.ToString(aoParameter[1]);

			if (sStatus.Contains("200 OK"))
			{
				parserDelegate(protocol, sResponse);
			}
			else
			{
				protocol.Log("QA" + protocol.QActionID + "|Run|Error in response with status code: " + sStatus + Environment.NewLine + sResponse, LogType.Error, LogLevel.NoLogging);
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}