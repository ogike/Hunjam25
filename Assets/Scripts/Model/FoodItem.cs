using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class FoodItem : ScriptableObject
{
    public enum FoodType
    {
        Healthy, 
        Gluten, 
        Lactose,
        Neutral,
        Cigarette
    }
    
    public string name;

    public FoodItem cookResult;

    public FoodType baseType;
    private List<FoodType> contaminations;

    public Sprite image;

    public void AddContamination(FoodType contamination)
    {
        contaminations.Add(contamination);
    }

    public List<FoodType> GetContaminations() => contaminations;

    public FoodItem Cook()
    {
        if (cookResult)
        {
            //Instantiate, add contaminations
            return cookResult;
        }
        else
        {
            return null;
        }
    }
}
