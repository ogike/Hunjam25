using System.Collections.Generic;
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
    
    [CreateAssetMenu(menuName = "ScriptableObject/Item")]
    public class FoodItem : ScriptableObject
    {
        public FoodItem cookResult;
        public FoodItem chopResult;
        public FoodItem bakeResult;

        public FoodType baseType;

        public Sprite image;

        public bool isEdible;
        public bool isResultOfMixing;
    }
}
