# Simple PlayerPrefs Editor

## Feature:
- Save PlayerPrefs with multiple slots.
- Read and edit directly in the Unity Editor via the Scriptable Object Inspector.
- Data encrypted with AESEncryption and Base62.

## How to use?

##### Save PlayerPrefs from json string

```csharp
  //Json Format
  string jsonData = @"
  {
    ""name"": ""Steve"",
    ""age"": 30,
    ""avg"": 12321.0002,
    ""some-string"": ""I'm Cat!!"",
    ""new"": ""I'm Animal""
  }";

  PlayerPrefsUtility.Save(string jsonData, string profileName);
```

##### Load PlayerPrefs from profile name

```csharp
  PlayerPrefsUtility.Load(string profileName);
```

##### Clear PlayerPrefs

```csharp
  PlayerPrefsUtility.Clear(); // remove all PlayerPrefs
  PlayerPrefsUtility.Clear(string profileName); // remove PlayerPrefs by profileName
```

## Unity Editor

![image](https://user-images.githubusercontent.com/70838508/220557342-076533fc-5fcd-4dac-984e-1f5204c43a1e.png)

*To view the PlayerPrefs profile you need to create a ScriptableObject User Profile.

#### Credits
- Nguyen Ha Dong (.GEARS) (2012)
- Module Playerprefs made by Kan (2022)
