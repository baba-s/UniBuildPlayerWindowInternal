using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditorInternal;
using UnityEngine;

namespace Kogane
{
	// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/BuildPlayerWindow.cs
	public static class BuildPlayerWindowInternal
	{
		private static Regex s_VersionPattern = new Regex
		(
			@"(?<shortVersion>\d+\.\d+\.\d+(?<suffix>((?<alphabeta>[abx])|[fp])[^\s]*))( \((?<revision>[a-fA-F\d]+)\))?",
			RegexOptions.Compiled
		);

		private static Dictionary<string, string> s_ModuleNames = new Dictionary<string, string>()
		{
			{ "tvOS", "AppleTV" },
			{ "OSXStandalone", "Mac" },
			{ "WindowsStandalone", "Windows" },
			{ "LinuxStandalone", "Linux" },
			{ "UWP", "Universal-Windows-Platform" }
		};

		public static string GetUnityHubModuleDownloadURL( string moduleName )
		{
			var fullVersion  = InternalEditorUtility.GetFullUnityVersion();
			var revision     = "";
			var shortVersion = "";
			var versionMatch = s_VersionPattern.Match( fullVersion );

			if ( !versionMatch.Success || !versionMatch.Groups[ "shortVersion" ].Success || !versionMatch.Groups[ "suffix" ].Success )
			{
				Debug.LogWarningFormat( "Error parsing version '{0}'", fullVersion );
			}

			if ( versionMatch.Groups[ "shortVersion" ].Success )
			{
				shortVersion = versionMatch.Groups[ "shortVersion" ].Value;
			}

			if ( versionMatch.Groups[ "revision" ].Success )
			{
				revision = versionMatch.Groups[ "revision" ].Value;
			}

			if ( s_ModuleNames.ContainsKey( moduleName ) )
			{
				moduleName = s_ModuleNames[ moduleName ];
			}

			return $"unityhub://{shortVersion}/{revision}/module={moduleName.ToLower()}";
		}
	}
}