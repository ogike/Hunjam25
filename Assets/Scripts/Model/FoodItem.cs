using System;
using System.Collections.Generic;
using SaintsField;
using UnityEngine;

namespace Model
{
    public enum FoodType
    {
        Healthy,
        Gluten,
        Lactose,
        Neutral,
        Cigarette
    }
    
    [Serializable]
    [CreateAssetMenu(menuName = "ScriptableObject/Item")]
    public class FoodItem : ScriptableObject
    {
        [Expandable] public FoodItem cookResult;
        [Expandable] public FoodItem chopResult;
        [Expandable] public FoodItem bakeResult;

        public FoodType baseType;

        public Sprite image;

        public bool isEdible;
        public bool isResultOfMixing;
    }
}
