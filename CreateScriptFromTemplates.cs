///https://www.youtube.com/@dayencreation

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
				var copyright = $"///\n/// Copyright (c) 2026 {PlayerSettings.companyName}\n/// All rights reserved.\n///\n/// This file is part of a proprietary project.\n/// Unauthorized copying, modification, or distribution is prohibited.\n///";
				
				templateContent = templateContent.Replace(PLACEHOLDER_CR, copyright);
				File.WriteAllText(path, templateContent);
			}

			ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, fileName);
		}

		[MenuItem("Assets/Create/Code/MonoBehaviour", priority = 1)]
		public static void CreateMonoBehaviourScript() =>
			GenerateFile("MonoBehaviour.txt", "NewBehaviour.cs");

		[MenuItem("Assets/Create/Code/Class", priority = 2)]
		public static void CreateClassScript() =>
			GenerateFile("Class.txt", "NewClass.cs");

		[MenuItem("Assets/Create/Code/Enum", priority = 3)]
		public static void CreateEnumScript() =>
			GenerateFile("Enum.txt", "NewType.cs");

		[MenuItem("Assets/Create/Code/ScriptableObject", priority = 4)]
		public static void CreateScriptableObjectScript() =>
			GenerateFile("ScriptableObject.txt", "NewSO.cs");

		[MenuItem("Assets/Create/Code/Interface", priority = 5)]
		public static void CreateInterfaceScript() =>
			GenerateFile("Interface.txt", "NewInterface.cs");

		[MenuItem("Assets/Create/Code/Struct", priority = 6)]
		public static void CreateStructScript() =>
			GenerateFile("Struct.txt", "NewStruct.cs");

		[MenuItem("Assets/Create/Code/FiniteState", priority = 7)]
		public static void CreateFiniteStateScript() =>
			GenerateFile("FiniteState.txt", "NewState.cs");
	}
}
