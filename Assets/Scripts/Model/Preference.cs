using System;
using Model;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/Preference")]
public class Preference : ScriptableObject
{
    public string nameTag;
    public string prevFoodTag;
    public FoodType likes;
    public FoodType dislikes;
}
