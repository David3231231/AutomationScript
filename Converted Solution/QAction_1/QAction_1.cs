
//---------------------------------
// QAction_1.cs
//---------------------------------
namespace Skyline.Protocol
{
	using System;
    using Skyline.DataMiner.Library.Common;
	using Skyline.DataMiner.Scripting;

	namespace MyExtension
	{
		#region Classes

		public class Helper
		{
			#region Methods

			public static string ConvertToBinary(int val, int length = 32)
			{
				return Convert.ToString(val, 2).PadLeft(length, '0');
			}

			public static int ConvertToDecimal(string binaryVal)
			{
				return Convert.ToInt32(binaryVal, 2);
			}

			#endregion Methods
		}

		#endregion Classes
	}

	namespace DynamicPolling
	{
		public class DynamicPollingController
		{
			private SLProtocol protocol;

			public DynamicPollingController(SLProtocol protocol)
			{
				this.protocol = protocol;

				var ipValues = GetElementIpInfo();

				ElementIp = ipValues[0];
				ElementPort = Convert.ToInt32(ElementIp.Split(':')[1]);
				ActivelyPolledIp = ipValues[1];
				Port1Ip = string.IsNullOrEmpty(ipValues[2]) ? null : CombineIpPort(ipValues[2], ElementPort);
				Port2Ip = string.IsNullOrEmpty(ipValues[3]) ? null : CombineIpPort(ipValues[3], ElementPort);
			}

			public string ElementIp { get; set; }

			public int ElementPort { get; set; }

			public string ActivelyPolledIp { get; set; }

			public string Port1Ip { get; set; }

			public string Port2Ip { get; set; }

			public static AlarmLevel GetElementAlarmStatus(SLProtocol protocol)
			{
				var alarmStatusParameterValue = protocol.GetParameter(65008);
				return (AlarmLevel)Convert.ToInt32(alarmStatusParameterValue);
			}

			public void TogglePollingIp()
			{
				var dynamicIpToSet = (ActivelyPolledIp == Port1Ip) ? Port2Ip : Port1Ip;
				protocol.Log("Setting Active IP to: " + dynamicIpToSet, LogType.Information, LogLevel.NoLogging);
				protocol.SetParameter(Parameter.activelypolledip_207, dynamicIpToSet);
			}

			private static string CombineIpPort(string ip, int port)
			{
				var formattedIp = $"{ip}:{port}";

				return formattedIp;
			}

			private string[] GetElementIpInfo()
			{
				var parameterIds = new uint[]
				{
					6, // Element IP
					Parameter.activelypolledip_207,
					Parameter.configurationport1ipaddress_202,
					Parameter.configurationport2ipaddress_204,
				};

				var paramValues = (object[])protocol.GetParameters(parameterIds);

				return Array.ConvertAll(paramValues, x => x.ToString());
			}
		}
	}
}
//---------------------------------
// API\Configuration.cs
//---------------------------------
namespace API
{
	using System;
	using System.Collections.Generic;
	using Skyline.DataMiner.Scripting;
   

    public class Configuration
	{
		private Dictionary<int, object> paramValues = new Dictionary<int, object>();

        public object CONFIG_ID { get; set; }

		public object CONFIG_PARAM_DEVICE_DEVICE_NAME { get; set; }

		public object CONFIG_PARAM_DEVICE_SOFTWARE_VERSION { get; set; }

		public object CONFIG_PARAM_DEVICE_HARDWARE_VERSION { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_CONFIG_MODE { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_REDUNDANCY { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_PROFILE { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_CLOCK_TYP { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_CLOCK_CLASS { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_ACCURACY { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_DOMAIN_PHY1 { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_DOMAIN_PHY2 { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_PRIORITY1 { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_PRIORITY2 { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_ANNOUNCE { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_SYNC { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_MIN_DELAY_REQ_INTERVAL { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_MIN_PDELAY_REQ_INTERVAL { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_ANNOUNCE_RECEIPT_TIMEOUT { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_ONE_STEP_CLOCK { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_SLAVE_ONLY { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_DELAY_MECHANISM { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_DSCP { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_NO_PTP_SWITCH_1000 { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_NO_PTP_SWITCH_100 { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_IP_MODE { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_UNICAST_NEGOTIATION { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_UNICAST_NEGOTIATION_BROADCAST { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_UNICAST_DESTINATION { get; set; }

		public object CONFIG_PARAM_PTP_CLOCK_UNICAST_GRAND_DURATION { get; set; }

		public object CONFIG_PARAM_DEVICE_LANGUAGE { get; set; }

		public object CONFIG_PARAM_DEVICE_TYPE { get; set; }

		public object CONFIG_PARAM_DEVICE_IS_LOCKED { get; set; }

		public object CONFIG_PARAM_CONFIG_FPGA_TYPE { get; set; }

		public object CONFIG_PARAM_NETWORK_MAC_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_MAC_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_DYNAMIC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_DYNAMIC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_DYNAMIC_IP_ADDRESS_PROTOCOL_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_DYNAMIC_IP_ADDRESS_PROTOCOL_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_STATIC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_STATIC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_SUBNET_MASK_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_SUBNET_MASK_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_GATEWAY_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_GATEWAY_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_IP_DNS_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_IP_DNS_PHY2 { get; set; }

		public object CONFIG_PARAM_NETWORK_TCP_HTTP { get; set; }

		public object CONFIG_PARAM_NETWORK_TCP_RTSP { get; set; }

		public object CONFIG_PARAM_NETWORK_MULTI_STREAM { get; set; }

		public object CONFIG_PARAM_NETWORK_MDNS_ANNOUNCE { get; set; }

		public object CONFIG_PARAM_NETWORK_SAP_ANNOUNCE { get; set; }

		public object CONFIG_PARAM_NETWORK_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_NETWORK_IGMP_VERSION_PHY1 { get; set; }

		public object CONFIG_PARAM_NETWORK_IGMP_VERSION_PHY2 { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE_MAX_CH_RX_1FS { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE_MAX_CH_RX_2FS { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE_MAX_CH_RX_4FS { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE_MAX_CH_TX_1FS { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE_MAX_CH_TX_2FS { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE_MAX_CH_TX_4FS { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE_SIZE { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_AUDIO_INTERFACE { get; set; }

		public object CONFIG_SHARED_AUDIO_CONTROL_EXTERN_PTP_SYNC { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_SAMPLE_FREQ { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_TTL { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_DSCP { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTCP_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTCP_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_IP_ADDRESS_DST_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_IP_ADDRESS_DST_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_START_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_NUM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_FRAME_SIZE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SIP_ROUTE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SIP_ROUTE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_ENABLE_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_ENABLE_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_BACKUP_STREAM { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_BACKUP_STREAM_TIMEOUT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_ADVANCE_MODE { get; set; }

		public object CONFIG_PARAM_CONFIG_SAVE_MODE { get; set; }

		public object CONFIG_PARAM_CONFIG_SAVE_BOOT_MODE { get; set; }

		public object CONFIG_PARAM_LAST_ERROR { get; set; }

		public object CONFIG_PARAM_NETWORK_SIP_PORT { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_ARP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_BASE_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_DHCP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_DNS_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_FLASH_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_IGMP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_MDNS_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_PTP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_RS232_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_RTCP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_SIP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_SAP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_TCP_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_ZEROCONF_LEVEL { get; set; }

		public object CONFIG_PARAM_LOG_PROTOCOL_LEVEL { get; set; }

		public object CONFIG_PARAM_RS232_DEVICE_ID { get; set; }

		public object MONTONE_CONFIG_PARAM_SETTING { get; set; }

		public object MONTONE_CONFIG_PARAM_HW_VERSION { get; set; }

		public object MONTONE_CONFIG_PARAM_SW_VERSION { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_0 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_1 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_2 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_3 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_4 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_5 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_6 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_7 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_8 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_9 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_10 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_11 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_12 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_13 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_14 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_15 { get; set; }

		public object MONTONE_CONFIG_PARAM_ROUTING_MATRIX_16 { get; set; }

		public Dictionary<int, object> ParamValues
		{
			get
			{
				return paramValues;
			}

			set
			{
				paramValues = value;
			}
		}

		public enum PtpProfile
		{
			DefaultE2E = 0,
			DefaultP2P = 1,
			MediaE2E = 2,
			MediaP2P = 3,
			Customized = 4,
		}

		public List<object[]> OutputStreamsRows
		{
			get
			{
				List<object[]> lOutputStreamsRows = new List<object[]>
			{
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "1",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_1_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "2",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_2_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "3",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_3_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "4",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_4_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "5",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_5_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "6",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_6_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "7",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_7_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "8",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_8_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "9",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_9_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "10",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_10_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "11",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_11_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "12",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_12_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "13",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_13_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "14",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_14_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "15",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_15_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "16",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_16_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "17",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_17_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "18",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_18_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "19",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_19_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "20",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_20_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "21",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_21_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "22",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_22_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "23",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_23_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "24",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_24_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "25",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_25_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "26",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_26_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "27",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_27_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "28",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_28_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "29",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_29_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "30",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_30_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "31",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_31_SIP_SERVER_PHY2),
				},
				new OutputstreamsQActionRow
				{
					Outputstreamsid_1201 = "32",
					Outputstreamsname_1202 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_STREAM_NAME),
					Outputstreamsstate_1203 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_ENABLE),
					Outputstreamsstreamoutput_1204 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_PHY_SELECTOR),
					Outputstreamsunicast_1205 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_UNICAST),
					Outputstreamsrtpdstport1_1206 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTP_DST_PHY1),
					Outputstreamsrtpdstport2_1207 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTP_DST_PHY2),
					Outputstreamsrtcpdstport1_1208 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTCP_DST_PHY1),
					Outputstreamsrtcpdstport2_1209 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTCP_DST_PHY2),
					Outputstreamsdstipaddrport1_1210 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_IP_ADDRESS_DST_PHY1),
					Outputstreamsdstipaddrport2_1211 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_IP_ADDRESS_DST_PHY2),
					Outputstreamsframesize_1212 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_FRAME_SIZE),
					Outputstreamsformat_1213 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_FORMAT),
					Outputstreamsstartch_1214 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_START_CH),
					Outputstreamsnumch_1215 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_NUM_CH),
					Outputstreamssipserverenableport1_1216 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_ENABLE_PHY1),
					Outputstreamssipserverenableport2_1217 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_ENABLE_PHY2),
					Outputstreamsrtppayloadid_1218 = Convert.ToInt32(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_RTP_PAYLOAD_ID),
					Outputstreamssipserverurlport1_1219 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_PHY1),
					Outputstreamssipserverurlport2_1220 = Convert.ToString(CONFIG_PARAM_AUDIO_SERVER_STREAM_32_SIP_SERVER_PHY2),
				},
			};

				return lOutputStreamsRows;
			}
		}

		public List<object[]> InputStreamsRows
		{
			get
			{
				List<object[]> lInputStreamRows = new List<object[]>
			{
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "1",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "2",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "3",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "4",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "5",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "6",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "7",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "8",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "9",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "10",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "11",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "12",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "13",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "14",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "15",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "16",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "17",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "18",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "19",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "20",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "21",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "22",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "23",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "24",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "25",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "26",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "27",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "28",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "29",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "30",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "31",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET),
				},
				new InputstreamsQActionRow
				{
					Inputstreamsid_1001 = "32",
					Inputstreamsstatus_1002 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_ENABLE),
					Inputstreamsstreaminput_1003 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_PHY_SELECTOR),
					Inputstreamsautomaticconfig_1004 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_AUTOMATIC_CONFIG),
					Inputstreamsunicast_1005 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_UNICAST),
					Inputstreamstransfer_1006 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_TRANSFER),
					Inputstreamsdstipaddrphy1_1007 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_DST_IP_ADDRESS_PHY1),
					Inputstreamsdstipaddrphy2_1008 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_DST_IP_ADDRESS_PHY2),
					Inputstreamschannels_1009 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_CHANNELS),
					Inputstreamsrtppayloadid_1010 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_PAYLOAD_ID),
					Inputstreamsname_1011 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_STREAM_NAME),
					Inputstreamsrtpdstport1_1012 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_DST_PORT_PHY1),
					Inputstreamsrtpdstport2_1013 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_DST_PORT_PHY2),
					Inputstreamsrtcpdstportphy1_1014 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTCP_DST_PORT_PHY1),
					Inputstreamsrtcpdstportphy2_1015 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTCP_DST_PORT_PHY2),
					Inputstreamsaudioformat_1016 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_FORMAT),
					Inputstreamsstartstreamch_1017 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_START_STREAM_CH),
					Inputstreamschunicast_1018 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_CH_UNICAST),
					Inputstreamsmediaoffset_1019 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET_MEDIA),
					Inputstreamsoffsetfine_1020 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET_FINE),
					Inputstreamssrcipaddressphy1_1021 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SRC_IP_ADDRESS_PHY1),
					Inputstreamssrcipaddressphy2_1022 = Convert.ToString(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SRC_IP_ADDRESS_PHY2),
					Inputstreamsssmphy1_1023 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SSM_PHY1),
					Inputstreamsssmphy2_1024 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SSM_PHY2),
					Inputstreamsbackupstream_1025 = SetBackupStreamValue(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_BACKUP_STREAM),
					Inputstreamsbackupstreamtimeout_1026 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_BACKUP_STREAM_TIMEOUT),
					Inputstreamsoffset_1027 = Convert.ToInt32(CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET),
				},
			};

				return lInputStreamRows;
			}
		}

		public void ParseGeneralParameters()
		{
			ParamValues[Parameter.configurationsamplerate_205] = Convert.ToInt32(this.CONFIG_PARAM_AUDIO_SERVER_SAMPLE_FREQ);
		}

		public void ParseNetworkParameters()
		{
			ParamValues[Parameter.configurationdevicename_200] = Convert.ToString(this.CONFIG_PARAM_DEVICE_DEVICE_NAME);
			ParamValues[Parameter.configurationport1macaddress_201] = Convert.ToString(this.CONFIG_PARAM_NETWORK_MAC_ADDRESS_PHY1);
			ParamValues[Parameter.configurationport1ipaddress_202] = Convert.ToString(this.CONFIG_PARAM_NETWORK_IP_ADDRESS_PHY1).TrimEnd('\r', '\n').Trim();
			ParamValues[Parameter.configurationport2macaddress_203] = Convert.ToString(this.CONFIG_PARAM_NETWORK_MAC_ADDRESS_PHY2);
			ParamValues[Parameter.configurationport2ipaddress_204] = Convert.ToString(this.CONFIG_PARAM_NETWORK_IP_ADDRESS_PHY2).TrimEnd('\r', '\n').Trim();
		}

		public void ParseAdvancedParameters()
		{
			ParamValues[Parameter.ptpclockclass_300] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_CLOCK_CLASS);
			ParamValues[Parameter.ptpaccuracy_301] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_ACCURACY);
			ParamValues[Parameter.ptpclockdomainport1_302] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_DOMAIN_PHY1);
			ParamValues[Parameter.ptpclockdomainport2_303] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_DOMAIN_PHY2);
			ParamValues[Parameter.ptppriority1_304] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_PRIORITY1);
			ParamValues[Parameter.ptppriority2_305] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_PRIORITY2);
			ParamValues[Parameter.ptpannounce_306] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_ANNOUNCE);
			ParamValues[Parameter.ptpsync_307] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_SYNC);
			ParamValues[Parameter.ptpmindelayreq_308] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_MIN_DELAY_REQ_INTERVAL);
			ParamValues[Parameter.ptpminpdelayreq_309] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_MIN_PDELAY_REQ_INTERVAL);
			ParamValues[Parameter.ptpannouncereceipttimeout_310] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_ANNOUNCE_RECEIPT_TIMEOUT);
			ParamValues[Parameter.ptponestepclock_311] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_ONE_STEP_CLOCK);
			ParamValues[Parameter.ptpslaveonly_312] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_SLAVE_ONLY);
			ParamValues[Parameter.ptpdelaymechanism_313] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_DELAY_MECHANISM);
			ParamValues[Parameter.ptpswitch1gbps_314] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_NO_PTP_SWITCH_1000);
			ParamValues[Parameter.ptpswitch100mbps_315] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_NO_PTP_SWITCH_100);
			ParamValues[Parameter.ptpprofile_316] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_PROFILE);
			ParamValues[Parameter.ptpmode_317] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_CONFIG_MODE);
			ParamValues[Parameter.ptpinput_318] = Convert.ToInt32(this.CONFIG_PARAM_PTP_CLOCK_REDUNDANCY);
			ParamValues[Parameter.networktcpporthttp_400] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_TCP_HTTP);
			ParamValues[Parameter.networktcpportrtsp_401] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_TCP_RTSP);
			ParamValues[Parameter.networkudpportsip_402] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_SIP_PORT);
			ParamValues[Parameter.networkmultistreamrx_403] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_MULTI_STREAM);
			ParamValues[Parameter.networkmdnsannouncement_404] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_MDNS_ANNOUNCE);
			ParamValues[Parameter.networksapannouncement_405] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_SAP_ANNOUNCE);
			ParamValues[Parameter.networkigmpversionport1_406] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_IGMP_VERSION_PHY1);
			ParamValues[Parameter.networkigmpversionport2_407] = Convert.ToInt32(this.CONFIG_PARAM_NETWORK_IGMP_VERSION_PHY2);
		}

		public void HandlePtpParameters()
		{
			PtpProfile ptpProfile = (PtpProfile)Convert.ToInt32(ParamValues[Parameter.ptpprofile_316]);
			switch (ptpProfile)
			{
				case PtpProfile.DefaultE2E:
					HandleMutualPtpParameters();
					ParamValues[Parameter.ptpsync_307] = 5;
					ParamValues[Parameter.ptpdelaymechanism_313] = 1;
					break;

				case PtpProfile.DefaultP2P:
					HandleMutualPtpParameters();
					ParamValues[Parameter.ptpsync_307] = 5;
					ParamValues[Parameter.ptpdelaymechanism_313] = 0;
					break;

				case PtpProfile.MediaE2E:
					HandleMutualPtpParameters();
					ParamValues[Parameter.ptpsync_307] = 2;
					ParamValues[Parameter.ptpdelaymechanism_313] = 1;
					break;

				case PtpProfile.MediaP2P:
					HandleMutualPtpParameters();
					ParamValues[Parameter.ptpsync_307] = 2;
					ParamValues[Parameter.ptpdelaymechanism_313] = 0;
					break;

				default:
					// no custom settings
					break;
			}
		}

		private void HandleMutualPtpParameters()
		{
			ParamValues[Parameter.ptpannounce_306] = 1;
			ParamValues[Parameter.ptpmindelayreq_308] = 3;
			ParamValues[Parameter.ptpminpdelayreq_309] = 0;
			ParamValues[Parameter.ptpannouncereceipttimeout_310] = 1;
			ParamValues[Parameter.ptppriority1_304] = 128;
			ParamValues[Parameter.ptppriority2_305] = 128;
			ParamValues[Parameter.ptpclockclass_300] = 248;
			ParamValues[Parameter.ptpaccuracy_301] = 254;
			ParamValues[Parameter.ptponestepclock_311] = 1;
		}

		private static int SetBackupStreamValue(object apiValue)
		{
			return Convert.ToInt32(Convert.ToString(apiValue).Trim());
		}
	}
}
//---------------------------------
// API\ConfigurationStream.cs
//---------------------------------
namespace API
{
	public class ConfigurationStream
	{
		public object CONFIG_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_1_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_2_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_3_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_4_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_5_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_6_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_7_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_8_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_9_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_10_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_11_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_12_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_13_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_14_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_15_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_16_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_17_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_18_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_19_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_20_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_21_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_22_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_23_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_24_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_25_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_26_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_27_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_28_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_29_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_30_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_31_SSM_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_ENABLE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_PHY_SELECTOR { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_AUTOMATIC_CONFIG { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_TRANSFER { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_URL_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_URL_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_DST_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_DST_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_CHANNELS { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_PAYLOAD_ID { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_STREAM_NAME { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTCP_DST_PORT_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_RTCP_DST_PORT_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_FORMAT { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_START_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_NUM_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_LEN_AUDIO_CH_UNICAST { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET_MEDIA { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_OFFSET_FINE { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_LEN_AUDIO_STREAM_CH { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SSM_PHY1 { get; set; }

		public object CONFIG_PARAM_AUDIO_CLIENT_STREAM_32_SSM_PHY2 { get; set; }
	}
}
//---------------------------------
// API\Statistics.cs
//---------------------------------
namespace API
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Skyline.DataMiner.Scripting;

	public enum LockStatus
	{
		Locked = 6,
		Unlocked = 1,
	}

	public enum ClockStatus
	{
		Slave = 8,
	}

	public class Statistics
	{
		private Dictionary<int, object> paramValues = new Dictionary<int, object>();

		public object STATISTIC_CONFIG_ID { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_STATUS { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_DRIFT { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_OFFSET { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_OFFSET_MS_SEC { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_OFFSET_MS_NSEC { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_OFFSET_SM_SEC { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_OFFSET_SM_NSEC { get; set; }

		public object STATISTIC_PARAM_PTP_NO_PTP_DELAY_RESP_RECV { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_STATUS_PHY1 { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_STATUS_PHY2 { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_ERROR_STATUS_PHY1 { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_ERROR_STATUS_PHY2 { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_CH_UNICAST { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_NUM_CH_UNICAST { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_STREAM_NAME { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_LEN_AUDIO_CH_UNICAST { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_TIMESTAMP_MIN { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_TIMESTAMP_MAX { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_SRC_IP_ADDRESS_PHY1 { get; set; }

		public object STATISTIC_PARAM_AUDIO_CLIENT_STREAM_SRC_IP_ADDRESS_PHY2 { get; set; }

		public object STATISTIC_PARAM_STATUS7 { get; set; }

		public object STATISTIC_PARAM_STATUS8 { get; set; }

		public object STATISTIC_PARAM_STATUS { get; set; }

		public object STATISTIC_PARAM_STATUS2 { get; set; }

		public object STATISTIC_PARAM_STATUS3 { get; set; }

		public object STATISTIC_PARAM_STATUS4 { get; set; }

		public object STATISTIC_PARAM_STATUS5_PHY1 { get; set; }

		public object STATISTIC_PARAM_STATUS6_PHY1 { get; set; }

		public object STATISTIC_PARAM_STATUS5_PHY2 { get; set; }

		public object STATISTIC_PARAM_STATUS6_PHY2 { get; set; }

		public object STATISTIC_PARAM_CURRENT_IP_ADDRESS_PHY1 { get; set; }

		public object STATISTIC_PARAM_CURRENT_IP_ADDRESS_PHY2 { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_OFFSET_ARRAY_POS { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_OFFSET_ARRAY { get; set; }

		public object STATISTIC_PARAM_MONTONE { get; set; }

		public object STATISTIC_PARAM_CURRENT_IP_ADDRESS_DHCP_PHY1 { get; set; }

		public object STATISTIC_PARAM_CURRENT_SUBNET_MASK_DHCP_PHY1 { get; set; }

		public object STATISTIC_PARAM_CURRENT_GATEWAY_DHCP_PHY1 { get; set; }

		public object STATISTIC_PARAM_CURRENT_DNS_SERVER_DHCP_PHY1 { get; set; }

		public object STATISTIC_PARAM_CURRENT_IP_ADDRESS_DHCP_PHY2 { get; set; }

		public object STATISTIC_PARAM_CURRENT_SUBNET_MASK_DHCP_PHY2 { get; set; }

		public object STATISTIC_PARAM_CURRENT_GATEWAY_DHCP_PHY2 { get; set; }

		public object STATISTIC_PARAM_CURRENT_DNS_SERVER_DHCP_PHY2 { get; set; }

		public object STATISTIC_PARAM_TEMPERATURE_CPU { get; set; }

		public object STATISTIC_PARAM_TEMPERATURE_SWITCH { get; set; }

		public object STATISTIC_PARAM_PTP_SECONDS { get; set; }

		public object STATISTIC_PARAM_PTP_NANOSECONDS { get; set; }

		public object STATISTIC_PARAM_KERNEL { get; set; }

		public object STATISTIC_PARAM_MEMORY_USAGE { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_PRIORITY1 { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_PRIORITY2 { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_IP_ADDRESS { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_SYNC_PORT { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_DOMAIN_NUMBER { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_TIME_SOURCE { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_SYNC_INTERVAL { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_CLOCK_ID { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_CLOCK_CLASS { get; set; }

		public object STATISTIC_PARAM_PTP_CLOCK_MASTER_CLOCK_ACCURACY { get; set; }

		public object STATISTIC_PARAM_NETWORK_NIC1 { get; set; }

		public object STATISTIC_PARAM_NETWORK_NIC2 { get; set; }

		public object STATISTIC_PARAM_NETWORK_PACKETS { get; set; }

		public object STATISTIC_PARAM_USED_PHY_FOR_STREAMING { get; set; }

		public object STATISTIC_PARAM_STREAM_OUTPUT_WARNING_PHY1 { get; set; }

		public object STATISTIC_PARAM_STREAM_OUTPUT_WARNING_PHY2 { get; set; }

		public object STATISTIC_PARAM_STREAM_OUTPUT_ERROR_PHY1 { get; set; }

		public object STATISTIC_PARAM_STREAM_OUTPUT_ERROR_PHY2 { get; set; }

		public object STATISTIC_PARAM_TRANSFER_RX_ARRAY_NIC1 { get; set; }

		public object STATISTIC_PARAM_TRANSFER_RX_ARRAY_NIC2 { get; set; }

		public object STATISTIC_PARAM_TRANSFER_TX_ARRAY_NIC1 { get; set; }

		public object STATISTIC_PARAM_TRANSFER_TX_ARRAY_NIC2 { get; set; }

		public object STATISTIC_PARAM_TRANSFER_RX_ARRAY_NIC12 { get; set; }

		public object STATISTIC_PARAM_TRANSFER_TX_ARRAY_NIC12 { get; set; }

		public Dictionary<int, object> ParamValues
		{
			get
			{
				return paramValues;
			}

			set
			{
				paramValues = value;
			}
		}

		public int Temperature { get; set; }

		public int TemperatureCpu { get; set; }

		public int TemperatureSwitch { get; set; }

		public int FanLevel_1 { get; set; }

		public int FanLevel_2 { get; set; }

		public long MonotoneStatus { get; set; }

		public int Madi_1_Status { get; set; }

		public int Madi_2_Status { get; set; }

		public int Madi_3_Status { get; set; }

		public int Madi_4_Status { get; set; }

		public int Ptp_Status { get; set; }

		public int Wck_Status { get; set; }

		public int Video_Status { get; set; }

		public int Int_Status { get; set; }

		public int I44_1_K_Status { get; set; }

		public int I48_K_Status { get; set; }

		public int I2_Fs_Status { get; set; }

		public int I56_Ch_Status { get; set; }

		public int I96_K_Status { get; set; }

		public int I75_Ohm_Status { get; set; }

		public int PSU_1_Status { get; set; }

		public int PSU_2_Status { get; set; }

		public double VoltajePsu_1 { get; set; }

		public double VoltajePsu_2 { get; set; }

		public double Voltaje5P { get; set; }

		public double Voltage3P3 { get; set; }

		public double Voltage1P2 { get; set; }

		public double Voltage1P2Mgt { get; set; }

		public double Cpu_ticks { get; set; }

		public double Cpu_idle_ticks { get; set; }

		public double Idle_ticks { get; set; }

		public double Arp_ticks { get; set; }

		public double Base_ticks { get; set; }

		public double Base_connection_ticks { get; set; }

		public double Dhcp_ticks { get; set; }

		public double Dns_ticks { get; set; }

		public double Flash_ticks { get; set; }

		public double Igmp_ticks { get; set; }

		public double Mdns_ticks { get; set; }

		public double Ptp_ticks { get; set; }

		public double Rs232_ticks { get; set; }

		public double Rtcp_ticks { get; set; }

		public double Sip_ticks { get; set; }

		public double Sap_ticks { get; set; }

		public double Tcp_ticks { get; set; }

		public double Zeroconf_ticks { get; set; }

		public double Icmp_ticks { get; set; }

		public double Nmos_ticks { get; set; }

		public static void CheckGrandMasterID(string grandmasterID, out bool regExOK)
		{
			Regex regExpress = new Regex(@"^([0-9A-F]{2}[--]){2}([0-9A-F]{2})(-FF-FE)([--][0-9A-F]{2}){3}$");

			// The Grandmaster Clock ID needs to be of the following format :
			// aa-bb-cc-FF-FE-dd-ee, whereby aa, bb, etc. are hexadecimal numbers, and FF-FE is fixed.
			regExOK = regExpress.IsMatch(grandmasterID);
		}

		public void ParseGeneralParameters()
		{
			ParamValues[Parameter.statisticsptpjitter_76] = Convert.ToDouble(this.STATISTIC_PARAM_PTP_CLOCK_DRIFT) / 1000;
			ParamValues[Parameter.statisticsptpoffset_77] = Convert.ToDouble((int)Convert.ToUInt32(this.STATISTIC_PARAM_PTP_CLOCK_OFFSET)) / 1000;

			// ParamValues[Parameter.statisticsptpoffset_77] = Convert.ToDouble((int)Convert.ToUInt32(Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_OFFSET))) / 1000;
			// Casting to int is necessary, because this is a parameter sent as an unsigned integer, although the true value is a signed integer.
			ParamValues[Parameter.statisticsptpclockstate_78] = this.STATISTIC_PARAM_PTP_CLOCK_STATUS;
		}

		public void ParseNetworkParameters()
		{
			string grandmasterID = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_CLOCK_ID).Trim();
			CheckGrandMasterID(grandmasterID, out bool regExOK);
			ParamValues[Parameter.statisticssyncport_83] = Convert.ToInt32(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_SYNC_PORT);
			ParamValues[Parameter.statisticsgmid_84] = regExOK ? grandmasterID : "N/A";
			ParamValues[Parameter.statisticsptpmasterslave_85] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_OFFSET_MS_NSEC);
			ParamValues[Parameter.statisticsptpslavemaster_86] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_OFFSET_SM_NSEC);
			ParamValues[Parameter.statisticsptpmasterpriority1_87] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_PRIORITY1);
			ParamValues[Parameter.statisticsptpmasterpriority2_88] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_PRIORITY2);
			ParamValues[89] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_IP_ADDRESS);
			ParamValues[Parameter.statisticsptpclass_90] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_CLOCK_CLASS);
			ParamValues[Parameter.statisticsptpaccuracy_91] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_CLOCK_ACCURACY);
			ParamValues[Parameter.statisticsptpdomain_92] = Convert.ToString(this.STATISTIC_PARAM_PTP_CLOCK_MASTER_DOMAIN_NUMBER);
			ParamValues[Parameter.statisticsptplockstate_93] = (regExOK && Convert.ToInt32(this.STATISTIC_PARAM_PTP_CLOCK_STATUS) == (Int32)ClockStatus.Slave) ? LockStatus.Locked : LockStatus.Unlocked;
		}

		public void ParseStatisticsParamKernel()
		{
			string[] splits = GetSplits(this.STATISTIC_PARAM_KERNEL);
			this.Idle_ticks = ParamMontoneValue<Double>(splits, 0);
			this.Arp_ticks = ParamMontoneValue<Double>(splits, 1);
			this.Base_ticks = ParamMontoneValue<Double>(splits, 2);
			this.Base_connection_ticks = ParamMontoneValue<Double>(splits, 3);
			this.Dhcp_ticks = ParamMontoneValue<Double>(splits, 4);
			this.Dns_ticks = ParamMontoneValue<Double>(splits, 5);
			this.Flash_ticks = ParamMontoneValue<Double>(splits, 6);
			this.Igmp_ticks = ParamMontoneValue<Double>(splits, 7);
			this.Mdns_ticks = ParamMontoneValue<Double>(splits, 8);
			this.Ptp_ticks = ParamMontoneValue<Double>(splits, 9);
			this.Rs232_ticks = ParamMontoneValue<Double>(splits, 10);
			this.Rtcp_ticks = ParamMontoneValue<Double>(splits, 11);
			this.Sip_ticks = ParamMontoneValue<Double>(splits, 12);
			this.Sap_ticks = ParamMontoneValue<Double>(splits, 13);
			this.Tcp_ticks = ParamMontoneValue<Double>(splits, 14);
			this.Zeroconf_ticks = ParamMontoneValue<Double>(splits, 15);
			this.Icmp_ticks = ParamMontoneValue<Double>(splits, 16);
			Cpu_ticks = -1;
			Cpu_idle_ticks = -1;
			Nmos_ticks = -1;
			double dTotal = GetTotalTicks();
			FillDictionaryPercentageParameters(dTotal, GetPercentage(dTotal - Idle_ticks, dTotal));
		}

		public void ParseStatisticsParamKernelRavio()
		{
			string[] splits = GetSplits(this.STATISTIC_PARAM_KERNEL);
			this.Cpu_ticks = ParamMontoneValue<Double>(splits, 0);
			this.Cpu_idle_ticks = ParamMontoneValue<Double>(splits, 1);
			this.Base_ticks = ParamMontoneValue<Double>(splits, 2);
			this.Base_connection_ticks = ParamMontoneValue<Double>(splits, 3);
			this.Dns_ticks = ParamMontoneValue<Double>(splits, 4);
			this.Flash_ticks = ParamMontoneValue<Double>(splits, 5);
			this.Mdns_ticks = ParamMontoneValue<Double>(splits, 6);
			this.Ptp_ticks = ParamMontoneValue<Double>(splits, 7);
			this.Rs232_ticks = ParamMontoneValue<Double>(splits, 8);
			this.Rtcp_ticks = ParamMontoneValue<Double>(splits, 9);
			this.Sip_ticks = ParamMontoneValue<Double>(splits, 10);
			this.Sap_ticks = ParamMontoneValue<Double>(splits, 11);
			this.Tcp_ticks = ParamMontoneValue<Double>(splits, 12);
			this.Nmos_ticks = ParamMontoneValue<Double>(splits, 13);
			Dhcp_ticks = -1;
			Igmp_ticks = -1;
			Arp_ticks = -1;
			Zeroconf_ticks = -1;
			Icmp_ticks = -1;
			double dTotal = Array.ConvertAll(splits, Convert.ToInt32).Sum();
			FillDictionaryPercentageParameters(dTotal, GetPercentage(Cpu_ticks - Cpu_idle_ticks, dTotal - Cpu_idle_ticks));
		}

		public void ParseStatisticMemory()
		{
			string[] splits = GetSplits(this.STATISTIC_PARAM_MEMORY_USAGE);
			var total = ParamMontoneValue<Double>(splits, 0);
			var free = ParamMontoneValue<Double>(splits, 1);
			ParamValues[Parameter.statisticsramusage_102] = (total - free) / total * 100;
		}

		public void ParseStatisticNetwork()
		{
			string[] splits = GetSplits(this.STATISTIC_PARAM_NETWORK_NIC1);
			ParamValues[Parameter.statisticspacketlossphy1_110] = ParamMontoneValue<Double>(splits, 0);
			ParamValues[Parameter.statisticspacketlosscpu1_111] = ParamMontoneValue<Double>(splits, 1);
			ParamValues[Parameter.statisticspacketlossrtprx1_112] = ParamMontoneValue<Double>(splits, 2);
			ParamValues[Parameter.statisticspacketlossrtptx1_113] = ParamMontoneValue<Double>(splits, 3);
			splits = GetSplits(this.STATISTIC_PARAM_NETWORK_NIC2);
			ParamValues[Parameter.statisticspacketlossphy2_114] = ParamMontoneValue<Double>(splits, 0);
			ParamValues[Parameter.statisticspacketlosscpu2_115] = ParamMontoneValue<Double>(splits, 1);
			ParamValues[Parameter.statisticspacketlossrtprx2_116] = ParamMontoneValue<Double>(splits, 2);
			ParamValues[Parameter.statisticspacketlossrtptx2_117] = ParamMontoneValue<Double>(splits, 3);

			double bytesPerSecond;

			splits = GetSplits(this.STATISTIC_PARAM_TRANSFER_RX_ARRAY_NIC1);
			bytesPerSecond = GetBytesPerSecond(splits);

			if (splits.Length >= 2)
			{
				ParamValues[Parameter.statisticstrafficrxnic1_118] = ToBitPerSecond(bytesPerSecond, 1000000);
			}

			splits = GetSplits(this.STATISTIC_PARAM_TRANSFER_RX_ARRAY_NIC2);
			bytesPerSecond = GetBytesPerSecond(splits);
			if (splits.Length >= 2)
			{
				ParamValues[Parameter.statisticstrafficrxnic2_119] = ToBitPerSecond(bytesPerSecond, 1000000);
			}

			splits = GetSplits(this.STATISTIC_PARAM_TRANSFER_RX_ARRAY_NIC12);
			bytesPerSecond = GetBytesPerSecond(splits);
			if (splits.Length >= 2)
			{
				ParamValues[Parameter.statisticstrafficrxnic12_120] = ToBitPerSecond(bytesPerSecond, 1000000);
			}

			splits = GetSplits(this.STATISTIC_PARAM_TRANSFER_TX_ARRAY_NIC1);
			bytesPerSecond = GetBytesPerSecond(splits);
			if (splits.Length >= 2)
			{
				ParamValues[Parameter.statisticstraffictxnic1_121] = ToBitPerSecond(bytesPerSecond, 1000000);
			}

			splits = GetSplits(this.STATISTIC_PARAM_TRANSFER_TX_ARRAY_NIC2);
			bytesPerSecond = GetBytesPerSecond(splits);
			if (splits.Length >= 2)
			{
				ParamValues[Parameter.statisticstraffictxnic2_122] = ToBitPerSecond(bytesPerSecond, 1000000);
			}

			splits = GetSplits(this.STATISTIC_PARAM_TRANSFER_TX_ARRAY_NIC12);
			bytesPerSecond = GetBytesPerSecond(splits);
			if (splits.Length >= 2)
			{
				ParamValues[Parameter.statisticstraffictxnic12_123] = ToBitPerSecond(bytesPerSecond, 1000000);
			}
		}

		public void ParseStatisticParamMontone()
		{
			string[] splits = Convert.ToString(this.STATISTIC_PARAM_MONTONE).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			this.Temperature = ParamMontoneValue<Int32>(splits, 0);
			this.FanLevel_1 = ParamMontoneValue<Int32>(splits, 1);
			this.FanLevel_2 = ParamMontoneValue<Int32>(splits, 2);
			this.MonotoneStatus = ParamMontoneValue<Int64>(splits, 3);
			var auxVoltajePsu_1 = ParamMontoneValue<Double>(splits, 4);
			var auxVoltajePsu_2 = ParamMontoneValue<Double>(splits, 5);
			var auxVoltaje5P = ParamMontoneValue<Double>(splits, 6);
			var auxVoltage3P3 = ParamMontoneValue<Double>(splits, 7);
			var auxVoltage1P2 = ParamMontoneValue<Double>(splits, 8);
			var auxVoltage1P2MGT = ParamMontoneValue<Double>(splits, 9);

			this.VoltajePsu_1 = auxVoltajePsu_1.Equals(-1) ? auxVoltajePsu_1 * 3.0 * 18700 / 1024 / 3300 : auxVoltajePsu_1;
			this.VoltajePsu_2 = auxVoltajePsu_2.Equals(-1) ? auxVoltajePsu_2 * 3.0 * 18700 / 1024 / 3300 : auxVoltajePsu_2;
			this.Voltaje5P = auxVoltaje5P.Equals(-1) ? auxVoltaje5P * 3.0 * 25000 / 1024 / 15000 : auxVoltaje5P;
			this.Voltage3P3 = auxVoltage3P3.Equals(-1) ? auxVoltage3P3 * 3.0 * 5170 / 1024 / 470 : auxVoltage3P3;
			this.Voltage1P2 = auxVoltage1P2.Equals(-1) ? auxVoltage1P2 * 3.0 / 1024 : auxVoltage1P2;
			this.Voltage1P2Mgt = auxVoltage1P2MGT.Equals(-1) ? auxVoltage1P2MGT * 3.0 / 1024 : auxVoltage1P2MGT;

			if (this.MonotoneStatus > 0)
			{
				ParseMonotoneStatus(Convert.ToInt32(this.MonotoneStatus));
			}

			this.TemperatureCpu = -1;
			this.TemperatureSwitch = -1;

			FillDictionaryStatusParameters();
		}

		public void ParseStatisticParamRavio()
		{
			this.Temperature = -1;
			this.FanLevel_1 = -1;
			this.FanLevel_2 = -1;
			this.MonotoneStatus = -1;
            
            this.VoltajePsu_1 = -1;
			this.VoltajePsu_2 = -1;
			this.Voltaje5P = -1;
			this.Voltage3P3 = -1;
			this.Voltage1P2 = -1;
			this.Voltage1P2Mgt = -1;

			this.Madi_1_Status = -1;
			this.Madi_2_Status = -1;
			this.Madi_3_Status = -1;
			this.Madi_4_Status = -1;
			this.Ptp_Status = -1;
			this.Wck_Status = -1;
			this.Video_Status = -1;
			this.Int_Status = -1;
			this.I44_1_K_Status = -1;
			this.I48_K_Status = -1;
			this.I2_Fs_Status = -1;
			this.I56_Ch_Status = -1;
			this.I96_K_Status = -1;
			this.I75_Ohm_Status = -1;
			this.PSU_1_Status = -1;
			this.PSU_2_Status = -1;

			this.TemperatureCpu = Convert.ToInt32(this.STATISTIC_PARAM_TEMPERATURE_CPU);
			this.TemperatureSwitch = Convert.ToInt32(this.STATISTIC_PARAM_TEMPERATURE_SWITCH);

			FillDictionaryStatusParameters();
		}

		private static string[] GetSplits(object param)
		{
			return Convert.ToString(param).Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
		}

		private static double GetBytesPerSecond(string[] splits)
		{
			double totalValue = splits.Where(x => Double.TryParse(x, out double y)).Select(x => Convert.ToDouble(x)).Sum();
			double averageValue = totalValue / (splits.Length - 1);
			return averageValue;
		}

		private static object ToBitPerSecond(double bytesPerSecond, int factor)
		{
			return bytesPerSecond * 8 / factor;
		}

		private static double GetPercentage(double dValue, double dTotal)
		{
			return dValue < 0 || dTotal <= 0 ? -1 : dValue / dTotal * 100;
		}

		private static int GetStatus(string sStatus, int idx)
		{
			if (idx > sStatus.Length) return -1;

			return Skyline.Protocol.MyExtension.Helper.ConvertToDecimal(sStatus.Substring(idx, 2));
		}

		private static T ParamMontoneValue<T>(string[] splits, int idx)
		{
			switch (typeof(T).ToString())
			{
				case "System.String":
					return splits.Length <= idx || String.IsNullOrEmpty(splits[idx]) ? (T)(object)Convert.ToString(-1) : (T)(object)Convert.ToString(splits[idx]);

				case "System.Double":
					return splits.Length <= idx || String.IsNullOrEmpty(splits[idx]) ? (T)(object)Convert.ToDouble(-1) : (T)(object)Convert.ToDouble(splits[idx]);

				case "System.Int32":
					return splits.Length <= idx || String.IsNullOrEmpty(splits[idx]) ? (T)(object)Convert.ToInt32(-1) : (T)(object)Convert.ToInt32(splits[idx]);

				case "System.Int64":
					return splits.Length <= idx || String.IsNullOrEmpty(splits[idx]) ? (T)(object)Convert.ToInt64(-1) : (T)(object)Convert.ToInt64(splits[idx]);

				default:
					return (T)(object)Convert.ToString(-1);
			}
		}

		private double GetTotalTicks()
		{
			List<double> ldTicks = new List<double> { this.Idle_ticks + this.Arp_ticks + this.Base_ticks + this.Base_connection_ticks + this.Dhcp_ticks + this.Dns_ticks + this.Flash_ticks + this.Igmp_ticks + this.Mdns_ticks + this.Ptp_ticks + this.Rs232_ticks + this.Rtcp_ticks + this.Sip_ticks + this.Sap_ticks + this.Tcp_ticks + this.Zeroconf_ticks + this.Icmp_ticks };
			double dTotal = 0;

			for (int i = 0; i < ldTicks.Count; i++)
			{
				if (ldTicks[i].Equals(-1)) continue;
				dTotal += ldTicks[i];
			}

			return dTotal;
		}

		private void FillDictionaryStatusParameters()
		{
			ParamValues[Parameter.statisticstemperature_50] = this.Temperature;
			ParamValues[Parameter.statisticsmadi1status_51] = this.Madi_1_Status;
			ParamValues[Parameter.statisticsmadi2status_52] = this.Madi_2_Status;
			ParamValues[Parameter.statisticsmadi3status_53] = this.Madi_3_Status;
			ParamValues[Parameter.statisticsmadi4status_54] = this.Madi_4_Status;
			ParamValues[Parameter.statisticsptpstatus_55] = this.Ptp_Status;
			ParamValues[Parameter.statisticswckstatus_56] = this.Wck_Status;
			ParamValues[Parameter.statisticsvideostatus_57] = this.Video_Status;
			ParamValues[Parameter.statisticsintstatus_58] = this.Int_Status;
			ParamValues[Parameter.statisticsframeformat_79] = this.I48_K_Status;
			ParamValues[Parameter.statisticschannelmode_80] = this.I56_Ch_Status;
			ParamValues[Parameter.statisticspsu1_81] = this.PSU_1_Status;
			ParamValues[Parameter.statisticspsu2_82] = this.PSU_2_Status;
			ParamValues[Parameter.statisticstemperaturecpu_100] = this.TemperatureCpu;
			ParamValues[Parameter.statisticstemperatureswitch_101] = this.TemperatureSwitch;
		}

		private void ParseMonotoneStatus(int monotoneStatus)
		{
			string sStatus = Skyline.Protocol.MyExtension.Helper.ConvertToBinary(monotoneStatus);
			this.Madi_1_Status = GetStatus(sStatus, 0);
			this.Madi_2_Status = GetStatus(sStatus, 2);
			this.Madi_3_Status = GetStatus(sStatus, 4);
			this.Madi_4_Status = GetStatus(sStatus, 6);
			this.Ptp_Status = GetStatus(sStatus, 8);
			this.Wck_Status = GetStatus(sStatus, 10);
			this.Video_Status = GetStatus(sStatus, 12);
			this.Int_Status = GetStatus(sStatus, 14);
			this.I44_1_K_Status = GetStatus(sStatus, 16);
			this.I48_K_Status = GetStatus(sStatus, 18);
			this.I2_Fs_Status = GetStatus(sStatus, 20);
			this.I56_Ch_Status = GetStatus(sStatus, 22);
			this.I96_K_Status = GetStatus(sStatus, 24);
			this.I75_Ohm_Status = GetStatus(sStatus, 26);
			this.PSU_1_Status = GetStatus(sStatus, 28);
			this.PSU_2_Status = GetStatus(sStatus, 30);
		}

		private void FillDictionaryPercentageParameters(double dTotal, double cpuPercent)
		{
			ParamValues[Parameter.statisticscpuusage_59] = cpuPercent;
			ParamValues[Parameter.statisticsarp_60] = GetPercentage(this.Arp_ticks, dTotal);
			ParamValues[Parameter.statisticsbase_75] = GetPercentage(this.Base_ticks, dTotal);
			ParamValues[Parameter.statisticsbaseconnections_61] = GetPercentage(this.Base_connection_ticks, dTotal);
			ParamValues[Parameter.statisticsdhcp_62] = GetPercentage(this.Dhcp_ticks, dTotal);
			ParamValues[Parameter.statisticsdns_63] = GetPercentage(this.Dns_ticks, dTotal);
			ParamValues[Parameter.statisticsflash_64] = GetPercentage(this.Flash_ticks, dTotal);
			ParamValues[Parameter.statisticsigmp_65] = GetPercentage(this.Igmp_ticks, dTotal);
			ParamValues[Parameter.statisticsmdns_66] = GetPercentage(this.Mdns_ticks, dTotal);
			ParamValues[Parameter.statisticsptp_67] = GetPercentage(this.Ptp_ticks, dTotal);
			ParamValues[Parameter.statisticsrs232_68] = GetPercentage(this.Rs232_ticks, dTotal);
			ParamValues[Parameter.statisticsrtcp_69] = GetPercentage(this.Rtcp_ticks, dTotal);
			ParamValues[Parameter.statisticssip_70] = GetPercentage(this.Sip_ticks, dTotal);
			ParamValues[Parameter.statisticssap_71] = GetPercentage(this.Sap_ticks, dTotal);
			ParamValues[Parameter.statisticstcp_72] = GetPercentage(this.Tcp_ticks, dTotal);
			ParamValues[Parameter.statisticszeroconf_73] = GetPercentage(this.Zeroconf_ticks, dTotal);
			ParamValues[Parameter.statisticsicmp_74] = GetPercentage(this.Icmp_ticks, dTotal);
			ParamValues[Parameter.statisticsnmos_103] = GetPercentage(this.Nmos_ticks, dTotal);
		}
	}
}
//---------------------------------
// Core\Parser.cs
//---------------------------------
namespace Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using API;
	using Newtonsoft.Json;
	using Skyline.DataMiner.Scripting;

	public static class Parser
	{
		public enum Product
		{
			NA = -1,
			MONTONE42 = 1,
			RAVIO = 2,
		}

		public static T Deserialize<T>(SLProtocol protocol, string response)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(response);
			}
			catch (Exception e)
			{
				protocol.Log("QA" + protocol.QActionID + "|Deserialize|Error trying to parse Json with exception: " + e, LogType.Error, LogLevel.NoLogging);
				return default(T);
			}
		}

		public static string Serialize<T>(T jsonObject)
		{
			return JsonConvert.SerializeObject(
									jsonObject,
									Formatting.None,
									new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
		}

		public static IDictionary<string, object> ToDiccionary(string response)
		{
			IDictionary<string, object> keyValues = new Dictionary<string, object>();

			string[] lsSplits = response.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			string sKey = String.Empty;
			string sVal = String.Empty;

			foreach (var line in lsSplits)
			{
				sKey = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
				sVal = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1];

				keyValues[sKey] = sVal;
			}

			return keyValues;
		}


		//what does this code mean? 
		public static T ToObject<T>(this IDictionary<string, object> source)
		where T : class, new()
		{
			var someObject = new T();
			var someObjectType = someObject.GetType();

			foreach (var item in source)
			{
				PropertyInfo prop = someObjectType.GetProperty(item.Key, BindingFlags.Public | BindingFlags.Instance);

				if (prop != null && prop.CanWrite)
				{
					prop.SetValue(someObject, item.Value, null);
				}
			}

			return someObject;
		}

		

		public static void ParseStatistics(SLProtocol protocol, string response)
		{
			var product = (Product)Convert.ToInt32(protocol.GetParameter(Parameter.configurationravennaproduct_206));
			IDictionary<string, object> keyValues = ToDiccionary(response);
			Statistics statistics = ToObject<Statistics>(keyValues);
			if (product == Product.MONTONE42)
			{
				statistics.ParseStatisticParamMontone();
				statistics.ParseStatisticsParamKernel();
			}
			else
			{
				statistics.ParseStatisticParamRavio();
				statistics.ParseStatisticsParamKernelRavio();
				statistics.ParseStatisticNetwork();
				statistics.ParseStatisticMemory();
			}

			statistics.ParseGeneralParameters();
			statistics.ParseNetworkParameters();
			protocol.SetParameters(statistics.ParamValues.Keys.ToArray(), statistics.ParamValues.Values.ToArray());
		}

		public static void ParseConfiguration(SLProtocol protocol, string response)
		{
			var product = (Product)Convert.ToInt32(protocol.GetParameter(Parameter.configurationravennaproduct_206));
			IDictionary<string, object> keyValues = ToDiccionary(response);
			Configuration configuration = ToObject<Configuration>(keyValues);
			configuration.ParseGeneralParameters();
			configuration.ParseNetworkParameters();
			configuration.ParseAdvancedParameters();
			configuration.HandlePtpParameters();
			SetDynamicPollingDiscreteDependency(protocol, configuration.ParamValues);

			List<object[]> lOutputStreams = configuration.OutputStreamsRows;
			List<object[]> lInputStreams = configuration.InputStreamsRows;
			protocol.SetParameters(configuration.ParamValues.Keys.ToArray(), configuration.ParamValues.Values.ToArray());
			protocol.FillArray(Parameter.Outputstreams.tablePid, lOutputStreams, NotifyProtocol.SaveOption.Full);
			protocol.FillArray(Parameter.Inputstreams.tablePid, lInputStreams, NotifyProtocol.SaveOption.Full);
		}

		public static void ParseConfigurationStream(SLProtocol protocol, string response)
		{
			IDictionary<string, object> keyValues = ToDiccionary(response);
			ConfigurationStream configurationStream = ToObject<ConfigurationStream>(keyValues);
		}

		public static void SetDynamicPollingDiscreteDependency(SLProtocol protocol, Dictionary<int, object> paramValues)
		{
			// Get element IP to retrieve port value
			var elementIp = Convert.ToString(protocol.GetParameter(6));
			var port1Ip = Convert.ToString(paramValues[Parameter.configurationport1ipaddress_202]);
			var port2Ip = Convert.ToString(paramValues[Parameter.configurationport2ipaddress_204]);

			if (!string.IsNullOrEmpty(elementIp) && !string.IsNullOrEmpty(port1Ip) && !string.IsNullOrEmpty(port2Ip))
			{
				var elementPort = elementIp.Split(':')[1];
				var discreteDependency = string.Format(
					"{0};{1}",
					string.Format(
						"{0}:{1}",
						port1Ip,
						elementPort),
					string.Format(
						"{0}:{1}",
						port2Ip,
						elementPort));

				protocol.SetParameter(Parameter.activelypolledip_discretedependency_208, discreteDependency);
			}
		}
	}
}