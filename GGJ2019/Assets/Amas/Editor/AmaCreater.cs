using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

public class AmaCreater : EditorWindow
{
	[MenuItem("GGJ2019/AmaCreater", false, 0)]
	static void ShowWindow()
	{
		EditorWindow.GetWindow<AmaCreater>("AmaCreater");
	}

	private void AutoCreatePrefabs()
	{
		string bodyPath = "Assets/Amas/Sources/Bodys";
		string facePath = "Assets/Amas/Sources/Faces";
		string hairPath = "Assets/Amas/Sources/Hairs";
		string skinPath = "Assets/Amas/Sources/Skins";
		string localPath = string.Empty;

		int index = 0;
		var bodyGuids = AssetDatabase.FindAssets("t:Model", new[] { bodyPath });
		var faceGuids = AssetDatabase.FindAssets("t:Model", new[] { facePath });
		var hairGuids = AssetDatabase.FindAssets("t:Model", new[] { hairPath });
		var skinGuids = AssetDatabase.FindAssets("t:Model", new[] { skinPath });
		foreach(var bguid in bodyGuids)
		{
			var bObj = CreateNew(null, index++);
			SetSource(bguid, bObj);
			foreach(var fguid in faceGuids)
			{
				var fObj = CreateNew(bObj, index++);
				SetSource(fguid, fObj);
				foreach(var hguid in hairGuids)
				{
					var hObj = CreateNew(fObj, index++);
					SetSource(hguid, hObj);
					foreach(var sguid in skinGuids)
					{
						var sObj = CreateNew(hObj, index++);
						SetSource(sguid, sObj);

						localPath = "Assets/Amas/Output/" + sObj.name + ".prefab";
						ReplacePrefab(sObj, localPath);

						DestroyImmediate(sObj);
					}
					DestroyImmediate(hObj);
				}
				DestroyImmediate(fObj);
			}
			DestroyImmediate(bObj);
		}



		//if(AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
		//{
		//	Debug.Log("The Prefab already exists: " + localPath);
		//}
		//else
		//{
		//	CreateNew(rootObj, localPath);
		//}
	}

	private void SetSource(string guid, GameObject rootObj)
	{
		var path = AssetDatabase.GUIDToAssetPath(guid);
		var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
		var obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
		obj.transform.SetParent(rootObj.transform, false);
	}

	private GameObject CreateNew(GameObject originObj, int index)
	{
		GameObject obj = null;
		if(originObj == null)
		{
			obj = new GameObject();
		}
		else
		{
			obj = GameObject.Instantiate(originObj);
		}
		var fileName = string.Format("Ama_{0:000}", index);
		obj.name = fileName;

		return obj;
	}

	static void ReplacePrefab(GameObject obj, string localPath)
	{
		UnityEngine.Object prefab = PrefabUtility.CreatePrefab(localPath, obj);
		PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
	}

	void OnGUI()
	{
		if(GUILayout.Button("AutoCreatePrefabs"))
		{
			AutoCreatePrefabs();
		}
	}
}