using System;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Preferences
{
    public string nameTag;
    public string prevFoodTag;
    public FoodType likes;
    public FoodType dislikes;
}

public class PreferenceRegistry : MonoBehaviour
{
    public static PreferenceRegistry Instance { get; private set; }

    public Preferences engiPrefs;
    public Preferences offiPrefs;
    public Preferences naviPrefs;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple PreferenceRegistries in scene!");
            return;
        }

        Instance = this;
    }
}
