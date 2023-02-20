using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

#if UNITY_EDITOR
using UnityEditor;

public class test
{
    public int keyInt = 15;
    public float keyFloat = 9.3209f;
    public string keyString = "fytguhijokl;56789$%&^*(";
}

public class PlayerPrefsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);
        
        if (GUILayout.Button("Save"))
        {
            string jsonData = JsonConvert.SerializeObject(new test());
            Debug.Log(jsonData);
            PlayerPrefsUtility.Save(jsonData, "");
        }
    }

}
#endif