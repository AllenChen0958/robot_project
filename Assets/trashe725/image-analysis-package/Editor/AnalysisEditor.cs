using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnalysisEditor{



	[MenuItem("Analysis/Segmentation/Color Settings", false, 0)]
	private static void ColorSettingsWindow()
	{
		EditorWindow window = EditorWindow.GetWindow<AnalysisSettingsWindow> ("Analysis", true);
		window.Show ();
	}

	[MenuItem("Analysis/Segmentation/Import Settings", false, 50)]
	private static void ColorSettingsImport()
	{
		string path = EditorUtility.OpenFilePanel ("Import", "", "bytes");
		if (path == null || path == "")
			return;
		TagsManager.Load (path);
		EditorWindow.GetWindow<AnalysisSettingsWindow> ("Settings", true).Repaint ();

		Debug.Log ("Import successfully");
	}

	[MenuItem("Analysis/Segmentation/Export Settings", false, 51)]
	private static void ColorSettingsExport()
	{
		string path = EditorUtility.SaveFilePanel ("Export", "", "tagssettings.bytes", "bytes");
		if (path == null || path == "")
			return;
		TagsManager.Save (path);
		Debug.Log ("Export successfully");
	}
}
