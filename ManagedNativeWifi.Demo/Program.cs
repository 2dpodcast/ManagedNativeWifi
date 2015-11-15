﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedNativeWifi.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			if (!Debugger.IsAttached)
				Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

			ShowInformation();

			//PerformUsage().Wait();
		}

		private static void ShowInformation()
		{
			Trace.WriteLine("[Usable Interfaces]");
			foreach (var interfaceInfo in NativeWifi.EnumerateInterfaces())
			{
				Trace.WriteLine($"Interface: {interfaceInfo.Description} ({interfaceInfo.Id})");
			}

			Trace.WriteLine("[Available Network SSIDs]");
			foreach (var ssid in NativeWifi.EnumerateAvailableNetworkSsids())
			{
				Trace.WriteLine($"SSID: {ssid}");
			}

			Trace.WriteLine("[Connected Network SSIDs]");
			foreach (var ssid in NativeWifi.EnumerateConnectedNetworkSsids())
			{
				Trace.WriteLine($"SSID: {ssid}");
			}

			Trace.WriteLine("[Available Networks]");
			foreach (var network in NativeWifi.EnumerateAvailableNetworks())
			{
				Trace.WriteLine($"{{Interface: {network.Interface.Description} ({network.Interface.Id})");
				Trace.WriteLine($" SSID: {network.Ssid}");
				Trace.WriteLine($" BSS: {network.BssType}");
				Trace.WriteLine($" Signal: {network.SignalQuality}");
				Trace.WriteLine($" Security: {network.IsSecurityEnabled}}}");
			}

			Trace.WriteLine("[BSS Networks]");
			foreach (var network in NativeWifi.EnumerateBssNetworks())
			{
				Trace.WriteLine($"{{Interface: {network.Interface.Description} ({network.Interface.Id})");
				Trace.WriteLine($" SSID: {network.Ssid}");
				Trace.WriteLine($" BSS: {network.BssType}");
				Trace.WriteLine($" BSSID: {network.Bssid}");
				Trace.WriteLine($" Signal: {network.SignalStrength}");
				Trace.WriteLine($" Link: {network.LinkQuality}");
				Trace.WriteLine($" Frequency: {network.Frequency}");
				Trace.WriteLine($" Channel: {network.Channel}}}");
			}

			Trace.WriteLine("[Network Profile Names]");
			foreach (var name in NativeWifi.EnumerateProfileNames())
			{
				Trace.WriteLine($"Name: {name}");
			}

			Trace.WriteLine("[Network Profiles]");
			foreach (var profile in NativeWifi.EnumerateProfiles())
			{
				Trace.WriteLine($"{{Name: {profile.Name}");
				Trace.WriteLine($" Interface: {profile.Interface.Description} ({profile.Interface.Id})");
				Trace.WriteLine($" SSID: {profile.Ssid}");
				Trace.WriteLine($" BSS: {profile.BssType}");
				Trace.WriteLine($" Authentication: {profile.Authentication}");
				Trace.WriteLine($" Encryption: {profile.Encryption}");
				Trace.WriteLine($" Signal: {profile.SignalQuality}");
				Trace.WriteLine($" Position: {profile.Position}");
				Trace.WriteLine($" Automatic: {profile.IsAutomatic}");
				Trace.WriteLine($" Connected: {profile.IsConnected}}}");
			}
		}

		//private static async Task PerformUsage()
		//{
		//	foreach (var ssid in Usage.EnumerateNetworkSsids())
		//		Trace.WriteLine($"Ssid: {ssid}");

		//	Trace.WriteLine($"Connect: {await Usage.ConnectAsync()}");

		//	await Usage.RefreshAsync();

		//	Trace.WriteLine($"Delete: {Usage.DeleteProfile("TestProfile")}");

		//	foreach (var channel in Usage.EnumerateNetworkChannels(-60))
		//		Trace.WriteLine($"Channel: {channel}");
		//}
	}
}