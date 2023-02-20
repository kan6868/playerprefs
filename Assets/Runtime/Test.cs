using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //Test data
        string jsonData = @"
        {
            ""name"": ""Jonh"",
            ""age"": 18,
            ""test"": 8.9999,
            ""testAnyString"": ""dhisakdkjas$465&*&(3""
        }"
        ;
        string profile1 = "profile_1";
        PlayerPrefsUtility.Save(jsonData, profile1);
    }
}
