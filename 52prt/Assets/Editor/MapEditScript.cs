using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditScript : EditorWindow
{
    private GameObject parent;
    private GameObject prefab;
    private int numX;
    private int numY;
    private int numZ;
    private Vector3 worldposXYZ;
    private float worldX;
    private float worldY;
    private float worldZ;
    private float intervalX;
    private float intervalY;
    private float intervalZ;

    [UnityEditor.MenuItem("Window/MapEditor")]

    static void Init(){
        EditorWindow.GetWindow<MapEditScript>("MapEdit");
    }

    void OnEnable(){
        if (Selection.gameObjects.Length > 0) parent = Selection.gameObjects[0];
    }
    void OnSelectionChange(){
        if (Selection.gameObjects.Length > 0) parent = Selection.gameObjects[0];
        Repaint();
    }
    void OnGUI()
    {
        try
        {
            GUILayout.Label("プレハブ", EditorStyles.boldLabel);
            GUILayout.Label("上：親になるオブジェクト,下：複製するプレハブ", EditorStyles.largeLabel);
            parent = EditorGUILayout.ObjectField(parent, typeof(UnityEngine.GameObject), true) as GameObject;
            prefab = EditorGUILayout.ObjectField(prefab, typeof(UnityEngine.GameObject), true) as GameObject;

            GUILayout.Space(12);

            worldposXYZ = EditorGUILayout.Vector3Field("ワールド座標", worldposXYZ);

            GUILayout.Space(12);

            GUILayout.Label("オブジェクトの設定(上：個数,下：間隔)",EditorStyles.boldLabel);

            GUILayout.Label("X", EditorStyles.largeLabel);
            numX = int.Parse(EditorGUILayout.TextField(numX.ToString()));
            intervalX = int.Parse(EditorGUILayout.TextField(intervalX.ToString()));

            GUILayout.Label("Y", EditorStyles.largeLabel);
            numY = int.Parse(EditorGUILayout.TextField(numY.ToString()));
            intervalY = int.Parse(EditorGUILayout.TextField(intervalY.ToString()));

            GUILayout.Label("Z", EditorStyles.largeLabel);
            numZ = int.Parse(EditorGUILayout.TextField(numZ.ToString()));
            intervalZ = int.Parse(EditorGUILayout.TextField(intervalZ.ToString()));

            GUILayout.Space(8);
            if (GUILayout.Button("配置")) Create();
        }
        catch (System.FormatException){}
    }

    private void Create()
    {
        if (prefab == null) return;

        int count = 0;
        Vector3 pos;

        pos.x = -(numX - 1) * intervalX / 2;
        for (int x = 0; x < numX; x++){
            pos.y = -(numY - 1) * intervalY / 2;
            for (int y = 0; y < numY; y++){
                pos.z = -(numZ - 1) * intervalZ / 2;
                for (int z = 0; z < numZ; z++){
                    GameObject obj = Instantiate(prefab, worldposXYZ + pos, Quaternion.identity) as GameObject;
                    obj.name = prefab.name + count++;
                    if (parent) obj.transform.parent = parent.transform;
                    Undo.RegisterCreatedObjectUndo(obj,"MapEdit");

                    pos.z += intervalZ;
                }
                pos.y += intervalY;
            }
            pos.x += intervalX;
        }
    }

}
