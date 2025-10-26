using System;
using System.Collections.Generic;
using SaintsField;
using UnityEngine;

namespace Model
{
    [Flags]
    public enum FoodType
    {
        Neutral = 0,
        Healthy = 1,
        Gluten = 2,
        Lactose = 4,
        Cigarette = 8
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
