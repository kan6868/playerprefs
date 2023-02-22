using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //Test data
        string jsonData = @"
        {
            ""name"": ""Jonh"",
            ""age"": 18,
            ""avg"": 8.9999,
            ""some-string"": ""I'm Robot!!""
        }";

        string jsonData2 = @"
        {
            ""name"": ""Steve"",
            ""age"": 21,
            ""avg"": 32190.3219,
            ""some-string"": ""I'm Human!!""
        }";

        string jsonData3 = @"
        {
            ""name"": ""Ken"",
            ""age"": 11,
            ""avg"": 12321.0002,
            ""some-string"": ""I'm Cat!!"",
            ""new"": ""I'm Animal""
        }";

        // Debug.Log(jsonData);
        string profile1 = "profile_1";
        string profile2 = "profile_2";
        string profile3 = "profile_3";

        PlayerPrefsUtility.Clear();

        PlayerPrefsUtility.Save(jsonData, profile1);
        PlayerPrefsUtility.Save(jsonData2, profile2);
        PlayerPrefsUtility.Save(jsonData3, profile3);

        string dataProfile1 = PlayerPrefsUtility.Load(profile1);
        string dataProfile2 = PlayerPrefsUtility.Load(profile2);
        string dataProfile3 = PlayerPrefsUtility.Load(profile3);

        Debug.Log(dataProfile1);
        Debug.Log(dataProfile2);
        Debug.Log(dataProfile3);
    }
}
