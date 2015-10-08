﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ManagedNativeWifi.Win32
{
	internal static class NativeMethod
	{
		[SuppressUnmanagedCodeSecurity]
		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanOpenHandle(
			uint dwClientVersion,
			IntPtr pReserved,
			out uint pdwNegotiatedVersion,
			out SafeClientHandle phClientHandle);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanCloseHandle(
			IntPtr hClientHandle,
			IntPtr pReserved);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern void WlanFreeMemory(IntPtr pMemory);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanEnumInterfaces(
			SafeClientHandle hClientHandle,
			IntPtr pReserved,
			out IntPtr ppInterfaceList);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanScan(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			IntPtr pDot11Ssid,
			IntPtr pIeData,
			IntPtr pReserved);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanGetAvailableNetworkList(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			uint dwFlags,
			IntPtr pReserved,
			out IntPtr ppAvailableNetworkList);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanGetNetworkBssList(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			IntPtr pDot11Ssid,
			DOT11_BSS_TYPE dot11BssType,
			[MarshalAs(UnmanagedType.Bool)] bool bSecurityEnabled,
			IntPtr pReserved,
			out IntPtr ppWlanBssList);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanQueryInterface(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			WLAN_INTF_OPCODE OpCode,
			IntPtr pReserved,
			out uint pdwDataSize,
			ref IntPtr ppData,
			IntPtr pWlanOpcodeValueType);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanGetProfileList(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			IntPtr pReserved,
			out IntPtr ppProfileList);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanGetProfile(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			[MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
			IntPtr pReserved,
			out IntPtr pstrProfileXml,
			ref uint pdwFlags,
			out uint pdwGrantedAccess);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanSetProfile(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			uint dwFlags,
			[MarshalAs(UnmanagedType.LPWStr)] string strProfileXml,
			[MarshalAs(UnmanagedType.LPWStr)] string strAllUserProfileSecurity,
			[MarshalAs(UnmanagedType.Bool)] bool bOverwrite,
			IntPtr pReserved,
			out uint pdwReasonCode);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanSetProfilePosition(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			[MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
			uint dwPosition,
			IntPtr pReserved);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanDeleteProfile(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			[MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
			IntPtr pReserved);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanConnect(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			[In] ref WLAN_CONNECTION_PARAMETERS pConnectionParameters,
			IntPtr pReserved);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanDisconnect(
			SafeClientHandle hClientHandle,
			[MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			IntPtr pReserved);

		[DllImport("Wlanapi.dll", SetLastError = true)]
		public static extern uint WlanRegisterNotification(
			SafeClientHandle hClientHandle,
			uint dwNotifSource,
			[MarshalAs(UnmanagedType.Bool)] bool bIgnoreDuplicate,
			WLAN_NOTIFICATION_CALLBACK funcCallback,
			IntPtr pCallbackContext,
			IntPtr pReserved,
			uint pdwPrevNotifSource);

		public delegate void WLAN_NOTIFICATION_CALLBACK(
			IntPtr data, // Pointer to WLAN_NOTIFICATION_DATA
			IntPtr context);

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern uint FormatMessage(
			uint dwFlags,
			IntPtr lpSource,
			uint dwMessageId,
			uint dwLanguageId,
			StringBuilder lpBuffer,
			int nSize,
			IntPtr Arguments);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WLAN_INTERFACE_INFO
		{
			public Guid InterfaceGuid;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string strInterfaceDescription;

			public WLAN_INTERFACE_STATE isState;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_INTERFACE_INFO_LIST
		{
			public uint dwNumberOfItems;
			public uint dwIndex;
			public WLAN_INTERFACE_INFO[] InterfaceInfo;

			public WLAN_INTERFACE_INFO_LIST(IntPtr ppInterfaceList)
			{
				dwNumberOfItems = (uint)Marshal.ReadInt32(ppInterfaceList, 0);
				dwIndex = (uint)Marshal.ReadInt32(ppInterfaceList, 4);
				InterfaceInfo = new WLAN_INTERFACE_INFO[dwNumberOfItems];

				var offset = Marshal.SizeOf(typeof(uint)) * 2; // Size of dwNumberOfItems and dwIndex

				for (int i = 0; i < dwNumberOfItems; i++)
				{
					var interfaceInfo = new IntPtr(ppInterfaceList.ToInt64() + (Marshal.SizeOf(typeof(WLAN_INTERFACE_INFO)) * i) + offset);
					InterfaceInfo[i] = Marshal.PtrToStructure<WLAN_INTERFACE_INFO>(interfaceInfo);
				}
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WLAN_AVAILABLE_NETWORK
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string strProfileName;

			public DOT11_SSID dot11Ssid;
			public DOT11_BSS_TYPE dot11BssType;
			public uint uNumberOfBssids;
			public bool bNetworkConnectable;
			public uint wlanNotConnectableReason;
			public uint uNumberOfPhyTypes;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public DOT11_PHY_TYPE[] dot11PhyTypes;

			public bool bMorePhyTypes;
			public uint wlanSignalQuality;
			public bool bSecurityEnabled;
			public DOT11_AUTH_ALGORITHM dot11DefaultAuthAlgorithm;
			public DOT11_CIPHER_ALGORITHM dot11DefaultCipherAlgorithm;
			public uint dwFlags;
			public uint dwReserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_AVAILABLE_NETWORK_LIST
		{
			public uint dwNumberOfItems;
			public uint dwIndex;
			public WLAN_AVAILABLE_NETWORK[] Network;

			public WLAN_AVAILABLE_NETWORK_LIST(IntPtr ppAvailableNetworkList)
			{
				dwNumberOfItems = (uint)Marshal.ReadInt32(ppAvailableNetworkList, 0);
				dwIndex = (uint)Marshal.ReadInt32(ppAvailableNetworkList, 4);
				Network = new WLAN_AVAILABLE_NETWORK[dwNumberOfItems];

				var offset = Marshal.SizeOf(typeof(uint)) * 2; // Size of dwNumberOfItems and dwIndex

				for (int i = 0; i < dwNumberOfItems; i++)
				{
					var availableNetwork = new IntPtr(ppAvailableNetworkList.ToInt64() + (Marshal.SizeOf(typeof(WLAN_AVAILABLE_NETWORK)) * i) + offset);
					Network[i] = Marshal.PtrToStructure<WLAN_AVAILABLE_NETWORK>(availableNetwork);
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_BSS_ENTRY
		{
			public DOT11_SSID dot11Ssid;
			public uint uPhyId;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public byte[] dot11Bssid; // DOT11_MAC_ADDRESS

			public DOT11_BSS_TYPE dot11BssType;
			public DOT11_PHY_TYPE dot11BssPhyType;
			public int lRssi;
			public uint uLinkQuality;

			[MarshalAs(UnmanagedType.U1)]
			public bool bInRegDomain;

			public ushort usBeaconPeriod;
			public ulong ullTimestamp;
			public ulong ullHostTimestamp;
			public ushort usCapabilityInformation;
			public uint ulChCenterFrequency;
			public WLAN_RATE_SET wlanRateSet;
			public uint ulIeOffset;
			public uint ulIeSize;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_BSS_LIST
		{
			public uint dwTotalSize;
			public uint dwNumberOfItems;
			public WLAN_BSS_ENTRY[] wlanBssEntries;

			public WLAN_BSS_LIST(IntPtr ppWlanBssList)
			{
				dwTotalSize = (uint)Marshal.ReadInt32(ppWlanBssList, 0);
				dwNumberOfItems = (uint)Marshal.ReadInt32(ppWlanBssList, 4);
				wlanBssEntries = new WLAN_BSS_ENTRY[dwNumberOfItems];

				var offset = Marshal.SizeOf(typeof(uint)) * 2; // Size of dwTotalSize and dwNumberOfItems

				for (int i = 0; i < dwNumberOfItems; i++)
				{
					var wlanBssEntry = new IntPtr(ppWlanBssList.ToInt64() + (Marshal.SizeOf(typeof(WLAN_BSS_ENTRY)) * i) + offset);
					wlanBssEntries[i] = Marshal.PtrToStructure<WLAN_BSS_ENTRY>(wlanBssEntry);
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct DOT11_SSID
		{
			public uint uSSIDLength;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] ucSSID;

			public byte[] ToSsidBytes()
			{
				return (ucSSID != null)
					? ucSSID.Take((int)uSSIDLength).ToArray()
					: null;
			}

			public string ToSsidString()
			{
				return (ucSSID != null)
					? Encoding.UTF8.GetString(ToSsidBytes())
					: null;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_RATE_SET
		{
			public uint uRateSetLength;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 126)]
			public ushort[] usRateSet;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WLAN_CONNECTION_ATTRIBUTES
		{
			public WLAN_INTERFACE_STATE isState;
			public WLAN_CONNECTION_MODE wlanConnectionMode;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string strProfileName;

			public WLAN_ASSOCIATION_ATTRIBUTES wlanAssociationAttributes;
			public WLAN_SECURITY_ATTRIBUTES wlanSecurityAttributes;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_ASSOCIATION_ATTRIBUTES
		{
			public DOT11_SSID dot11Ssid;
			public DOT11_BSS_TYPE dot11BssType;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public byte[] dot11Bssid; // DOT11_MAC_ADDRESS

			public DOT11_PHY_TYPE dot11PhyType;
			public uint uDot11PhyIndex;
			public uint wlanSignalQuality;
			public uint ulRxRate;
			public uint ulTxRate;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_SECURITY_ATTRIBUTES
		{
			[MarshalAs(UnmanagedType.Bool)]
			public bool bSecurityEnabled;

			[MarshalAs(UnmanagedType.Bool)]
			public bool bOneXEnabled;

			public DOT11_AUTH_ALGORITHM dot11AuthAlgorithm;
			public DOT11_CIPHER_ALGORITHM dot11CipherAlgorithm;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WLAN_PROFILE_INFO
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string strProfileName;

			public uint dwFlags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_PROFILE_INFO_LIST
		{
			public uint dwNumberOfItems;
			public uint dwIndex;
			public WLAN_PROFILE_INFO[] ProfileInfo;

			public WLAN_PROFILE_INFO_LIST(IntPtr ppProfileList)
			{
				dwNumberOfItems = (uint)Marshal.ReadInt32(ppProfileList, 0);
				dwIndex = (uint)Marshal.ReadInt32(ppProfileList, 4);
				ProfileInfo = new WLAN_PROFILE_INFO[dwNumberOfItems];

				var offset = Marshal.SizeOf(typeof(uint)) * 2; // Size of dwNumberOfItems and dwIndex

				for (int i = 0; i < dwNumberOfItems; i++)
				{
					var profileInfo = new IntPtr(ppProfileList.ToInt64() + (Marshal.SizeOf(typeof(WLAN_PROFILE_INFO)) * i) + offset);
					ProfileInfo[i] = Marshal.PtrToStructure<WLAN_PROFILE_INFO>(profileInfo);
				}
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WLAN_CONNECTION_PARAMETERS
		{
			public WLAN_CONNECTION_MODE wlanConnectionMode;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string strProfile;
			public IntPtr pDot11Ssid; // DOT11_SSID[]
			public IntPtr pDesiredBssidList; // DOT11_BSSID_LIST[]
			public DOT11_BSS_TYPE dot11BssType;
			public uint dwFlags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct DOT11_BSSID_LIST
		{
			public NDIS_OBJECT_HEADER Header;
			public uint uNumOfEntries;
			public uint uTotalNumOfEntries;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public byte[] BSSIDs; // DOT11_MAC_ADDRESS
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NDIS_OBJECT_HEADER
		{
			public byte Type;
			public byte Revision;
			public ushort Size;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WLAN_NOTIFICATION_DATA
		{
			public uint NotificationSource;
			public uint NotificationCode;
			public Guid InterfaceGuid;
			public uint dwDataSize;
			public IntPtr pData;
		}

		public enum WLAN_INTERFACE_STATE
		{
			wlan_interface_state_not_ready = 0,
			wlan_interface_state_connected = 1,
			wlan_interface_state_ad_hoc_network_formed = 2,
			wlan_interface_state_disconnecting = 3,
			wlan_interface_state_disconnected = 4,
			wlan_interface_state_associating = 5,
			wlan_interface_state_discovering = 6,
			wlan_interface_state_authenticating = 7
		}

		public enum WLAN_CONNECTION_MODE
		{
			wlan_connection_mode_profile,
			wlan_connection_mode_temporary_profile,
			wlan_connection_mode_discovery_secure,
			wlan_connection_mode_discovery_unsecure,
			wlan_connection_mode_auto,
			wlan_connection_mode_invalid
		}

		public enum DOT11_BSS_TYPE
		{
			/// <summary>
			/// Infrastructure BSS network
			/// </summary>
			dot11_BSS_type_infrastructure = 1,

			/// <summary>
			/// Independent BSS (IBSS) network
			/// </summary>
			dot11_BSS_type_independent = 2,

			/// <summary>
			/// Either infrastructure or IBSS network
			/// </summary>
			dot11_BSS_type_any = 3,
		}

		public enum DOT11_PHY_TYPE : uint
		{
			dot11_phy_type_unknown = 0,
			dot11_phy_type_any = 0,
			dot11_phy_type_fhss = 1,
			dot11_phy_type_dsss = 2,
			dot11_phy_type_irbaseband = 3,
			dot11_phy_type_ofdm = 4,
			dot11_phy_type_hrdsss = 5,
			dot11_phy_type_erp = 6,
			dot11_phy_type_ht = 7,
			dot11_phy_type_vht = 8,
			dot11_phy_type_IHV_start = 0x80000000,
			dot11_phy_type_IHV_end = 0xffffffff
		}

		public enum DOT11_AUTH_ALGORITHM : uint
		{
			DOT11_AUTH_ALGO_80211_OPEN = 1,
			DOT11_AUTH_ALGO_80211_SHARED_KEY = 2,
			DOT11_AUTH_ALGO_WPA = 3,
			DOT11_AUTH_ALGO_WPA_PSK = 4,
			DOT11_AUTH_ALGO_WPA_NONE = 5,
			DOT11_AUTH_ALGO_RSNA = 6,
			DOT11_AUTH_ALGO_RSNA_PSK = 7,
			DOT11_AUTH_ALGO_IHV_START = 0x80000000,
			DOT11_AUTH_ALGO_IHV_END = 0xffffffff
		}

		public enum DOT11_CIPHER_ALGORITHM : uint
		{
			DOT11_CIPHER_ALGO_NONE = 0x00,
			DOT11_CIPHER_ALGO_WEP40 = 0x01,
			DOT11_CIPHER_ALGO_TKIP = 0x02,
			DOT11_CIPHER_ALGO_CCMP = 0x04,
			DOT11_CIPHER_ALGO_WEP104 = 0x05,
			DOT11_CIPHER_ALGO_WPA_USE_GROUP = 0x100,
			DOT11_CIPHER_ALGO_RSN_USE_GROUP = 0x100,
			DOT11_CIPHER_ALGO_WEP = 0x101,
			DOT11_CIPHER_ALGO_IHV_START = 0x80000000,
			DOT11_CIPHER_ALGO_IHV_END = 0xffffffff
		}

		public enum WLAN_INTF_OPCODE : uint
		{
			wlan_intf_opcode_autoconf_start = 0x000000000,
			wlan_intf_opcode_autoconf_enabled,
			wlan_intf_opcode_background_scan_enabled,
			wlan_intf_opcode_media_streaming_mode,
			wlan_intf_opcode_radio_state,
			wlan_intf_opcode_bss_type,
			wlan_intf_opcode_interface_state,
			wlan_intf_opcode_current_connection,
			wlan_intf_opcode_channel_number,
			wlan_intf_opcode_supported_infrastructure_auth_cipher_pairs,
			wlan_intf_opcode_supported_adhoc_auth_cipher_pairs,
			wlan_intf_opcode_supported_country_or_region_string_list,
			wlan_intf_opcode_current_operation_mode,
			wlan_intf_opcode_supported_safe_mode,
			wlan_intf_opcode_certified_safe_mode,
			wlan_intf_opcode_hosted_network_capable,
			wlan_intf_opcode_management_frame_protection_capable,
			wlan_intf_opcode_autoconf_end = 0x0fffffff,
			wlan_intf_opcode_msm_start = 0x10000100,
			wlan_intf_opcode_statistics,
			wlan_intf_opcode_rssi,
			wlan_intf_opcode_msm_end = 0x1fffffff,
			wlan_intf_opcode_security_start = 0x20010000,
			wlan_intf_opcode_security_end = 0x2fffffff,
			wlan_intf_opcode_ihv_start = 0x30000000,
			wlan_intf_opcode_ihv_end = 0x3fffffff
		}

		public enum WLAN_NOTIFICATION_ACM : uint
		{
			wlan_notification_acm_start = 0,
			wlan_notification_acm_autoconf_enabled,
			wlan_notification_acm_autoconf_disabled,
			wlan_notification_acm_background_scan_enabled,
			wlan_notification_acm_background_scan_disabled,
			wlan_notification_acm_bss_type_change,
			wlan_notification_acm_power_setting_change,
			wlan_notification_acm_scan_complete,
			wlan_notification_acm_scan_fail,
			wlan_notification_acm_connection_start,
			wlan_notification_acm_connection_complete,
			wlan_notification_acm_connection_attempt_fail,
			wlan_notification_acm_filter_list_change,
			wlan_notification_acm_interface_arrival,
			wlan_notification_acm_interface_removal,
			wlan_notification_acm_profile_change,
			wlan_notification_acm_profile_name_change,
			wlan_notification_acm_profiles_exhausted,
			wlan_notification_acm_network_not_available,
			wlan_notification_acm_network_available,
			wlan_notification_acm_disconnecting,
			wlan_notification_acm_disconnected,
			wlan_notification_acm_adhoc_network_state_change,
			wlan_notification_acm_profile_unblocked,
			wlan_notification_acm_screen_power_change,
			wlan_notification_acm_profile_blocked,
			wlan_notification_acm_scan_list_refresh,
			wlan_notification_acm_end
		}

		public const uint WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_ADHOC_PROFILES = 0x00000001;
		public const uint WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES = 0x00000002;

		public const uint ERROR_SUCCESS = 0;
		public const uint ERROR_INVALID_PARAMETER = 87;
		public const uint ERROR_INVALID_HANDLE = 6;
		public const uint ERROR_INVALID_STATE = 5023;
		public const uint ERROR_NOT_FOUND = 1168;
		public const uint ERROR_NOT_ENOUGH_MEMORY = 8;
		public const uint ERROR_ACCESS_DENIED = 5;
		public const uint ERROR_NOT_SUPPORTED = 50;
		public const uint ERROR_SERVICE_NOT_ACTIVE = 1062;
		public const uint ERROR_NDIS_DOT11_AUTO_CONFIG_ENABLED = 0x80342000;
		public const uint ERROR_NDIS_DOT11_MEDIA_IN_USE = 0x80342001;
		public const uint ERROR_NDIS_DOT11_POWER_STATE_INVALID = 0x80342002;

		public const uint WLAN_NOTIFICATION_SOURCE_NONE = 0;
		public const uint WLAN_NOTIFICATION_SOURCE_ALL = 0x0000FFFF;
		public const uint WLAN_NOTIFICATION_SOURCE_ACM = 0x00000008;
		public const uint WLAN_NOTIFICATION_SOURCE_HNWK = 0x00000080;
		public const uint WLAN_NOTIFICATION_SOURCE_ONEX = 0x00000004;
		public const uint WLAN_NOTIFICATION_SOURCE_MSM = 0x00000010;
		public const uint WLAN_NOTIFICATION_SOURCE_SECURITY = 0x00000020;
		public const uint WLAN_NOTIFICATION_SOURCE_IHV = 0x00000040;

		public const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
	}
}