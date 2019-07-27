using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class SetGoAssertBundleName : EditorWindow
{
    //文件夹名字
    string DictorName = "";
    //完整文件夹名字
    string AllDictorName = "";
    //ab包名字
    string abName = "sound";

    string[] mFangXiangName = { "DiEast", "DiWest", "DiSouth", "DiNorth", "East", "West", "South", "North" };
    string[] mNumberName = { "One", "Two", "Three", "Four" };
    //后缀名
    string VariantName = "unity3d";
    List<string> files = new List<string>();
    //打开窗口
    [MenuItem("Tools/SetAssertBundleName")]
    public static void GetWidow()
    {
        EditorWindow.GetWindow<SetGoAssertBundleName>(false, "批量设置AB包名字", true);
    }
    private void OnGUI()
    {
        EditorGUILayout.LabelField("批量修改AB包名");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("选择文件夹");
        //得到文件夹下的名字
        if (GUILayout.Button("选择文件夹"))
        {
            AllDictorName = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, "");
            if (!string.IsNullOrEmpty(AllDictorName))
            {
                DictorName = AllDictorName.Substring(AllDictorName.IndexOf("Assets"));
            }
        }
        //输入包名
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("输入包名");
        abName = EditorGUILayout.TextField(abName);
        EditorGUILayout.EndHorizontal();

        //输入后缀名
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("输入后缀名");
        VariantName = EditorGUILayout.TextField(VariantName);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("设置ab包名"))
        {
            if (string.IsNullOrEmpty(DictorName)) return;
            files.Clear();
            GetAllFiles(files, DictorName);
            foreach (string fl in files)
            {
                Debug.Log(fl);
                var import = AssetImporter.GetAtPath(fl);
                if (import != null)
                {
                    import.assetBundleName = abName;
                    import.assetBundleVariant = VariantName;
                }
                else
                    Debug.Log("import=null");
            }
        }

        EditorGUILayout.Space();
        //if (GUILayout.Button("修改_MySound文件名字,声音不能重复"))
        //{
        //    ChangeAllFilesName(AllDictorName);
        //}

        EditorGUILayout.Space();

        //if (GUILayout.Button("修改3D记牌器文件中的名字"))
        //{
        //    ChangeAll3dFilesName(AllDictorName);
        //}


    }
    //获取文件夹下所有的文件 .meta文件除外
    static void GetAllFiles(List<string> files, string dir)
    {
        string[] fls = Directory.GetFiles(dir);
        foreach (string fl in fls)
        {
            string extension = Path.GetExtension(fl);
            if (extension != ".meta")
            {
                files.Add(fl);
            }
        }
        string[] subDirs = Directory.GetDirectories(dir);
        foreach (string subDir in subDirs)
        {
            GetAllFiles(files, subDir);
        }
    }

    //修改文件名字
    static void ChangeAllFilesName(string dir)
    {
        string[] fls = Directory.GetFiles(dir);
        foreach (string fl in fls)
        {
            string extension = Path.GetExtension(fl);
            if (extension != ".meta")
            {
                string s = fl.Replace(@"\", "/");
                string d = dir.Replace(@"\", "/");
                string newfl = s.Substring(0, s.LastIndexOf("/") + 1) + d.Substring(d.LastIndexOf("/") + 1) + s.Substring(s.LastIndexOf("/") + 1);
                if (!File.Exists(newfl))
                {
                    File.Create(newfl).Close();
                }
                File.WriteAllBytes(newfl, File.ReadAllBytes(s));
                File.Delete(s);
            }
        }
        string[] subDirs = Directory.GetDirectories(dir);
        foreach (string subDir in subDirs)
        {
            ChangeAllFilesName(subDir);
        }
    }

    void ChangeAll3dFilesName(string dir)
    {
        string[] subDirs = Directory.GetDirectories(dir);
        for (int i = 0; i < subDirs.Length; i++)
        {
            string[] fls = Directory.GetFiles(subDirs[i], "*.png");
            for (int j = 0; j < fls.Length; j++)
            {
                string extension = Path.GetExtension(fls[j]);
                if (extension != ".meta")
                {
                    string s = fls[j].Replace(@"\", "/");
                    //string d = subDirs[i].Replace(@"\", "/");
                    string newfl = s.Substring(0, s.LastIndexOf("/") + 1) + mNumberName[i] + mFangXiangName[j]+".png";
                    if (!File.Exists(newfl))
                    {
                        File.Create(newfl).Close();
                    }
                    File.WriteAllBytes(newfl, File.ReadAllBytes(s));
                    File.Delete(s);
                }

            }
        }
    }
}
