using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyFirstWindow : EditorWindow {
    private  string mBugName = "";
    private  string mDes = "";

    GameObject bugGo;

    MyFirstWindow()
    {
        this.titleContent = new GUIContent("错误报告");
    }

    [MenuItem("Tools/BugReport")]
    static void GetWindow()
    {
        EditorWindow.GetWindow(typeof(MyFirstWindow));
    }

    private void OnGUI()
    {

        GUILayout.BeginVertical();
        GUILayout.Space(10);
        bugGo = (GameObject)EditorGUILayout.ObjectField("Buggy Game Object", bugGo, typeof(GameObject), true);

        GUILayout.Space(10);
        mBugName = EditorGUILayout.TextField("Bug Name", mBugName, GUILayout.Width(200),GUILayout.Height(200));

    }

}
