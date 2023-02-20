using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class PlayerPrefsUtility
{
    /// <summary>
    /// Save string json to PlayerPrefs
    /// </summary>
    /// <param name="jsonData"></param>
    /// <param name="profileName"></param>
    public static void Save(string jsonData = "", string profileName = "")
    {

        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

        foreach (var key in data.Keys)
        {
            // check if the value is not null or empty.
            if (data[key] != null)
            {
                var value = data[key];
                int iVal;
                float fVal;

                string fullKey = profileName + "_" + key;
                if (int.TryParse(data[key], out iVal))
                {
                    Debug.Log(iVal);
                    PlayerPrefs.SetInt(fullKey, iVal);
                }else if (float.TryParse(data[key], out fVal))
                {
                    Debug.Log(fVal);
                    PlayerPrefs.SetFloat(fullKey, fVal);
                }
                else
                {
                    PlayerPrefs.SetString(fullKey, data[key]);
                }
            }
        }
    }
    /// <summary>
    /// Remove all 
    /// </summary>
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
