using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(UserProfile))]
public class PlayerPrefsEditor : Editor
{
    Dictionary<string, string> profile;
    private UserProfile userProfile;

    void OnEnable()
    {
        userProfile = (UserProfile)target;
        string profileData = PlayerPrefsUtility.Load(userProfile.name);

        profile = JsonConvert.DeserializeObject<Dictionary<string, string>>(profileData);
    }
    public override void OnInspectorGUI()
    {
        if (profile != null)
        {

            Dictionary<string, string> localStoreProfile = new Dictionary<string, string>();
            foreach (var data in profile)
            {
                localStoreProfile.Add(data.Key, EditorGUILayout.TextField(data.Key, data.Value));
            }

            //local to profile to update in next call
            foreach (var data in localStoreProfile)
            {
                profile[data.Key] = data.Value;
            }

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Delete"))
            {
                bool isClear = PlayerPrefsUtility.Clear(userProfile.name);
                if (isClear) profile = null;
            }

            if (GUILayout.Button("Update"))
            {
                string output = "{\n";

                foreach (var data in profile)
                {
                    output += "\"" + data.Key + "\"" + " : " + "\"" + data.Value + "\"" + ",\n";
                }
                output += "}";

                PlayerPrefsUtility.Save(output, userProfile.name);
            }

            if (GUILayout.Button("Print String Json"))
            {
                string output = "{\n";

                foreach (var data in profile)
                {
                    output += "\"" + data.Key + "\"" + " : " + "\"" + data.Value + "\"" + ",\n";
                }
                output += "}";

                Debug.Log(output);
            }

            GUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.LabelField("Don't found user-profile in PlayerPrefs.");
        }
    }

}
#endif