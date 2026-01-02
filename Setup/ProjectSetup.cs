/// -----------------------------------------------------------------
/// By DayenCreation
/// License: The Unlicense (public domain)
/// https://www.youtube.com/@dayencreation
/// https://github.com/Dayen-Creation
/// 
/// WARNING: 3-space indentation ahead. Proceed at your own risk! :P
/// -----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DayenCreation.ProjectSetup
{
	/// <summary>
	/// Automated project setup targeted for URP 2D.
	/// Running this in other SRPs may produce harmless errors.
	/// Configures company and product names, generates the folder structure
	/// and script templates, adjusts the GameObject naming scheme,
	/// and imports utility files from GitHub. 
	/// Manually refresh using ctrl+r afterwards.
	/// Modify the setup to suit project requirements 
	/// [video link]
	/// </summary>

	public class ProjectSetup : EditorWindow
	{
		private static string companyName = "DayenCreation";
		private static string productName = "";
		private static bool isDragging = false;
		private static Vector2 dragStart;
		[MenuItem("Assets/Setup Project")]
		private static void InitiateSetup()
		{
			productName = PlayerSettings.productName;

			int width = 300, height = 150;
			ProjectSetup window = ScriptableObject.CreateInstance<ProjectSetup>();
			Rect mainPos = EditorGUIUtility.GetMainWindowPosition();
			Rect myPos = new(mainPos.x + (mainPos.width - width) / 2f, mainPos.y + (mainPos.height - height) / 2f, width, height);
			window.position = myPos;
			window.ShowPopup();
		}

		void OnGUI()
		{
			// Drag handle
			GUILayout.Label("≡ Drag to move", EditorStyles.centeredGreyMiniLabel, GUILayout.Height(20));
			Rect dragRect = GUILayoutUtility.GetLastRect();
			EditorGUIUtility.AddCursorRect(dragRect, MouseCursor.MoveArrow);
			Event e = Event.current;

			if (e == null)
				return; // defensive, though Event.current is normally available inside OnGUI

			if (e.type == EventType.MouseDown && dragRect.Contains(e.mousePosition))
			{
				isDragging = true;
				dragStart = e.mousePosition;
				e.Use();
			}
			else if (e.type == EventType.MouseDrag && isDragging)
			{
				Vector2 diff = e.mousePosition - dragStart;
				position = new Rect(position.x + diff.x, position.y + diff.y, position.width, position.height);
				this.Repaint();
				e.Use();
			}
			else if (e.type == EventType.MouseUp)
			{
				isDragging = false;
			}

			companyName = EditorGUILayout.TextField("Company Name: ", companyName);
			productName = EditorGUILayout.TextField("Product Name: ", productName);
			this.Repaint();
			var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
			EditorGUILayout.LabelField($"Root Directory: _{productName}", style);
			GUILayout.Space(20);
			if (GUILayout.Button("BOOM!"))
			{
				SetUpProject();
				this.Close();
			}
			if (GUILayout.Button("Close"))
			{
				this.Close();
			}
		}

		private static void SetUpProject()
		{
			companyName = companyName == "" || companyName == null ? "DefaultCompany" : companyName;
			PlayerSettings.companyName = companyName;

			if (productName != "") PlayerSettings.productName = productName;

			EditorSettings.projectGenerationRootNamespace = $"{companyName}.{productName}";
			EditorSettings.gameObjectNamingScheme = EditorSettings.NamingScheme.Underscore;
			EditorSettings.gameObjectNamingDigits = 2;

			productName = $"_{productName}";
			CreateAllFolders(productName);
			ImportFromGit();
			CleanUpAssets(productName);

			AssetDatabase.Refresh();
		}

		private static void CreateAllFolders(string rootName)
		{
			List<string> folders = new()
			{
				"Animations",
				"Code/Editor",
				"Code/Scripts/Dependency/Input",
				"Code/Scripts/Entity/Core",
				"Code/Scripts/Entity/Enemy",
				"Code/Scripts/Entity/Player",
				"Code/Scripts/Helper/Singleton",
				"Code/Scripts/Helper/Extensions",
				"Code/Scripts/Helper/Utility",
				"Code/Scripts/Manager",
				"Code/Shaders",
				"Data/Materials",
				"Data/Prefabs",
				"Data/SO",
				"Media/Audio/Music",
				"Media/Audio/SFX",
				"Media/Video",
				"Art/Sprites",
				"Art/Textures",
				"Art/UI",
			};

			foreach (string folder in folders)
			{
				Directory.CreateDirectory($"Assets/{rootName}/{folder}");
			}
			AssetDatabase.Refresh();
		}

		private static void CleanUpAssets(string rootName)
		{
			string[,] paths = {
				{ "Assets/Scenes", $"Assets/{rootName}/Scenes" },
				{ "Assets/InputSystem_Actions.inputactions", $"Assets/{rootName}/Code/Scripts/Dependency/Input/InputActions.inputactions" },
				{ "Assets/DefaultVolumeProfile.asset", "Assets/Settings/DefaultVolumeProfile.asset" },
				{ "Assets/UniversalRenderPipelineGlobalSettings.asset", "Assets/Settings/UniversalRenderPipelineGlobalSettings.asset" },
			};

			for (int i = 0; i < paths.GetLength(0); i++)
			{
				try
				{
					MoveAsset(paths[i, 0], paths[i, 1]);
				}
				catch (Exception e)
				{
					Debug.LogError(e);
				}
			}
		}

		private static void MoveAsset(string sourcePath, string targetPath)
		{
			string error = AssetDatabase.MoveAsset(sourcePath, targetPath);
			if (!string.IsNullOrEmpty(error))
				Debug.LogError(error);
		}

		private static void ImportFromGit()
		{
			var url = "https://raw.githubusercontent.com/Dayen-Creation/UnityProjectSetup/refs/heads/main/";
			string targetPath = $"Assets/_{PlayerSettings.productName}/Code/";

			string[] editorItems =
			{
				"CreateScriptFromTemplates.cs",
				"Templates/MonoBehaviour.txt",
				"Templates/Class.txt",
				"Templates/Enum.txt",
				"Templates/ScriptableObject.txt",
				"Templates/Interface.txt",
				"Templates/Struct.txt",
				"Templates/FiniteState.txt"
			};

			foreach (var eItem in editorItems)
				_ = DownloadAndImportFile($"{url}Setup/{eItem}", $"{targetPath}Editor/{eItem}");

			string[] helperItems =
			{
				"Singleton/Singleton.cs",
				"Singleton/PersistentSingleton.cs",
				"Extensions/TransformExtensions.cs",
				"Extensions/Vector2Extensions.cs",
				"Extensions/Vector2IntExtensions.cs",
				"Extensions/Vector3Extensions.cs",
				"Extensions/Vector3IntExtensions.cs",
			};

			foreach (var hItem in helperItems)
				_ = DownloadAndImportFile(url + hItem, $"{targetPath}/Scripts/Helper/{hItem}");

		}

		private static async Task DownloadAndImportFile(string url, string targetPath)
		{
			// Ensure the path exists
			Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

			using var request = UnityWebRequest.Get(url);
			var operation = request.SendWebRequest();

			// Wait asynchronously until download is done
			while (!operation.isDone)
			{
				await Task.Yield();
			}

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError($"Failed to download {url}: {request.error}");
				return;
			}

			File.WriteAllBytes(targetPath, request.downloadHandler.data);
			AssetDatabase.Refresh();
		}

	}
}
