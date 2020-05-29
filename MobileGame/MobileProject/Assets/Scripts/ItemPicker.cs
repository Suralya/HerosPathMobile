using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedItemPicker
{
    public static WeightedItemPicker PickItem;
    public List<Items> Items;
    public float ProbabilitySum;

    public WeightedItemPicker()
    {
        Items = new List<Items>();
    }

    public void Add(Items Item)
    {
        Items.Add(Item);
        ProbabilitySum += Items[Items.Count - 1].Probability;

        for (var i = Items.Count - 1; i >= 0; i--)
        {
            Items[i].Weight = Items[i].Probability / ProbabilitySum;

            if (i <= Items.Count - 2)
            {
                Items[i].Weight += Items[i + 1].Weight;
            }
        }
    }

    public Items Pick()
    {
        var picker = Random.Range(0.0f, 1.0f);

        return Items.FindLast(
            delegate (Items current) { return picker <= current.Weight; }
            );
    }
}
