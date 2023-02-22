using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using static AESEncryption;

public static class PlayerPrefsUtility
{
    private const string SIGNATURE = "-CUTSTRING-";
    private const string IV_SYMBOL = "_IV_";

    /// <summary>
    /// Save data json to PlayerPrefs
    /// </summary>
    /// <param name="jsonData"></param>
    /// <param name="profileName"></param>
    public static void Save(string jsonData, string profileName)
    {
        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        
        string storeAllKey = "";

        foreach (var key in data.Keys)
        {
            // check if the value is not null or empty.
            if (data[key] != null)
            {
                string cipherFullkey = GetDataEncrypt(GetFullKey(profileName, key));

                PlayerPrefs.SetString(cipherFullkey, GetDataEncrypt(data[key]));

                storeAllKey += cipherFullkey + ";";
            }
        }

        PlayerPrefs.SetString(profileName, GetDataEncrypt(storeAllKey)); //Store metadata

        PlayerPrefs.Save();
    }

    /// <summary>
    /// Load user-profile in PlayerPrefs
    /// </summary>
    /// <param name="profileName"></param>
    /// <returns>String Json</returns>
    public static string Load(string profileName)
    {   
        string metadata = PlayerPrefs.GetString(profileName); // Load metadata by profileName
        
        if (string.IsNullOrEmpty(metadata)) return "";
        
        metadata = GetDataDecrypt(metadata);

        string[] keys = metadata.Split(";");//Get all key in metadata

        string output = "{\n";

        //Store data from keys in metadata
        for (int i = 0; i < keys.Length - 1; i++)
        {
            string value = PlayerPrefs.GetString(keys[i], "");

            string symbol = (i == (keys.Length - 2)) ? "" : ",";
            
            output += "\"" + GetTitle(GetDataDecrypt(keys[i])) + "\"" + " : " + "\"" + GetDataDecrypt(value) + "\"" + symbol + "\n";
        }
        output += "}";

        return output;
    }

    //Splite cipher text to get the real title
    //exmaple "profile_1" + SIGNATURE + "key" => return "key"
    public static string GetTitle(string cipher)
    {
        return (cipher.Split(SIGNATURE)[1]);
    }

    // Encrypt data and return cipher text and IV
    public static string GetDataEncrypt(string data)
    {
        AESEncryptedText cipherFullkey = AESEncryption.Encrypt(data, AESEncryption.PASSWORD);

        return cipherFullkey.EncryptedText + IV_SYMBOL + cipherFullkey.IV;
    }

    public static string GetDataDecrypt(string data)
    {
        AESEncryptedText aes = new AESEncryptedText();
        
        string[] decrypted = data.Split(IV_SYMBOL);
        
        aes.EncryptedText = decrypted[0];
        aes.IV = decrypted[1];

        return AESEncryption.Decrypt(aes.EncryptedText, aes.IV, AESEncryption.PASSWORD);
    }

    /// <summary>
    /// Merge full key with signature, signature will  divide between profile name and key.
    /// </summary>
    /// <param name="profileName"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private static string GetFullKey(string profileName, string key)
    {
        return profileName + SIGNATURE + key;
    }

    /// <summary>
    /// Remove all profile in PlayerPrefs
    /// </summary>
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    //Clear by profile name
    public static bool Clear(string profileName)
    {
        string metadata = PlayerPrefs.GetString(profileName); // Load metadata by profileName
        
        if (string.IsNullOrEmpty(metadata)) return false;
        
        metadata = GetDataDecrypt(metadata);

        string[] keys = metadata.Split(";");//Get all key in metadata

        for (int i = 0; i < keys.Length - 1; i++)
        {
            PlayerPrefs.DeleteKey(keys[i]);
        }

        PlayerPrefs.DeleteKey(profileName);
        PlayerPrefs.Save();

        return true;
    }
}
