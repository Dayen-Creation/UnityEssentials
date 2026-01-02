/// -----------------------------------------------------------------
/// By DayenCreation
/// License: The Unlicense (public domain)
/// https://www.youtube.com/@dayencreation
/// https://github.com/Dayen-Creation
/// 
/// WARNING: 3-space indentation ahead. Proceed at your own risk! :P
/// -----------------------------------------------------------------

using UnityEditor;
using System.IO;

namespace DayenCreation.ProjectSetup
{
	public static class CreateScriptFromTemplates
	{
		private static void GenerateFile(string path, string fileName)
		{
			path = $"Assets/_{PlayerSettings.productName}/Code/Editor/Templates/{path}";
			string templateContent = File.ReadAllText(path);

			var PLACEHOLDER_CR = "#COPYRIGHT#";
			if (templateContent.Contains(PLACEHOLDER_CR))
			{
				var copyright = $"/// ------------------------------------------------------------------\n/// Copyright (c) 2026 {PlayerSettings.companyName}\n/// All rights reserved.\n///\n/// This file is part of a proprietary project.\n/// Unauthorized copying, modification, or distribution is prohibited.\n/// ------------------------------------------------------------------";
				
				templateContent = templateContent.Replace(PLACEHOLDER_CR, copyright);
				File.WriteAllText(path, templateContent);
			}

			ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, fileName);
		}

		[MenuItem("Assets/Code/MonoBehaviour", priority = 100)]
		public static void CreateMonoBehaviourScript() =>
			GenerateFile("MonoBehaviour.txt", "NewBehaviour.cs");

		[MenuItem("Assets/Code/Class", priority = 101)]
		public static void CreateClassScript() =>
			GenerateFile("Class.txt", "NewClass.cs");

		[MenuItem("Assets/Code/ScriptableObject", priority = 102)]
		public static void CreateScriptableObjectScript() =>
			GenerateFile("ScriptableObject.txt", "NewSO.cs");

		[MenuItem("Assets/Code/Enum", priority = 103)]
		public static void CreateEnumScript() =>
			GenerateFile("Enum.txt", "NewType.cs");

		[MenuItem("Assets/Code/Interface", priority = 104)]
		public static void CreateInterfaceScript() =>
			GenerateFile("Interface.txt", "NewInterface.cs");

		[MenuItem("Assets/Code/Struct", priority = 105)]
		public static void CreateStructScript() =>
			GenerateFile("Struct.txt", "NewStruct.cs");

		[MenuItem("Assets/Code/FiniteState", priority = 106)]
		public static void CreateFiniteStateScript() =>
			GenerateFile("FiniteState.txt", "NewState.cs");
	}
}
