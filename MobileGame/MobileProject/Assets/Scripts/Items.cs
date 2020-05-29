using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Items
{

    // Weapon->Str; Armor->Def, Shoes->Spe, Gloves->Dex, Accessory->HP
    public enum ItemType {Weapon, Armor, Shoes, Gloves, Accessory };
    
    // 1=common, 2=rare, 3=super rare;
    public int rarity;
    public int lvl;
    public ItemType Type;
    public float worth;

    public int price;

    public float Probability;
    public float Weight;


    public Items(ItemType IT)
    {
        Type = IT;

        switch (Type)
        {
            case ItemType.Weapon:
                rarity = 1;
                Probability = 7;

                break;
            case ItemType.Armor:
                rarity = 1;
                Probability = 6;

                break;
            case ItemType.Shoes:
                rarity = 2;
                Probability = 4;

                break;
            case ItemType.Gloves:
                rarity = 2;
                Probability = 3;

                break;
            case ItemType.Accessory:
                rarity = 3;
                Probability = 0.3f;

                break;

        }

    }


}

