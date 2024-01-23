#region
using System;
using System.IO;
using System.Linq;

using UnityEditor;

using UnityEngine;
#endregion

#pragma warning disable CS0618

namespace OriGames.Editor
{
	[InitializeOnLoad]
	public class FolderMoveValidator : AssetModificationProcessor
	{
		private const string MillisecondsThresholdKey = "MillisecondsThresholdKey";
		private const string FileThresholdKey = "FileThresholdKey";

		private static int _millisecondsThreshold;
		private static int _fileThreshold;

		private static DateTime _lastClickTime;

		static FolderMoveValidator()
		{
			EditorApplication.projectWindowItemOnGUI += HandleProjectItemRedraw;
			
			_millisecondsThreshold = EditorPrefs.GetInt(MillisecondsThresholdKey, 500);
			_fileThreshold = EditorPrefs.GetInt(FileThresholdKey, 5);
		}
		
		private static void HandleProjectItemRedraw(string _, Rect __)
		{
			if (Event.current != null && Event.current.type == EventType.MouseDown)
			{
				_lastClickTime = DateTime.Now;
			}
		}

		[PreferenceItem("Folder Move Validator Settings")]
		private static void CustomPreferencesGUI()
		{
			_millisecondsThreshold = EditorGUILayout.IntField("Milliseconds Threshold: ", _millisecondsThreshold);
			_fileThreshold = EditorGUILayout.IntField("File Threshold: ", _fileThreshold);

			if (GUI.changed)
			{
				EditorPrefs.SetInt(MillisecondsThresholdKey, _millisecondsThreshold);
				EditorPrefs.SetInt(FileThresholdKey, _fileThreshold);
			}
		}
		
		public static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
		{
			bool isFolder = AssetDatabase.IsValidFolder(sourcePath);

			if (!isFolder)
			{
				return AssetMoveResult.DidNotMove;
			}

			double millisecondsPassed = DateTime.Now.Subtract(_lastClickTime).TotalMilliseconds;
			
			if (millisecondsPassed > _millisecondsThreshold)
			{
				return AssetMoveResult.DidNotMove;
			}
			
			int fileCount = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).Count(file => !file.EndsWith(".meta"));

			if (fileCount < _fileThreshold)
			{
				return AssetMoveResult.DidNotMove;
			}
			
			bool moveConfirmed = EditorUtility.DisplayDialog(
				"Confirm Folder Move", 
				$"Are you sure you want to move the folder from \n'{sourcePath}' to \n'{destinationPath}'?",
				"Move",
				"Cancel"
			);

			return moveConfirmed ? AssetMoveResult.DidNotMove : AssetMoveResult.FailedMove;
		}
	}
}