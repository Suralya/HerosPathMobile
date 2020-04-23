using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Hero
{
    public static Hero CurrentHero;

    public string Name = "Adberd";
    public int Lvl;

    public int maxExp;
    public int CurrentExp;

    public int Hp, CurrentHp, Str, Def, Dex;
    public int StageCounter, MonsterCounter;
    public float Spe;

    public bool ShowMessage = true;
    public bool isFighting = false;

    public int Gold;
    public List<Items> Wearing;
    public List<Items> Bag;


    public Hero()
    {


    }


    public void AddExp(int expGain)
    {
        CurrentExp += expGain;
        if (CurrentExp >= maxExp)
        {
            LvlUp();
        }

    }

    public void LvlUp()
    {
        Lvl++;
        CurrentExp = CurrentExp-maxExp;
        maxExp += (int)(Mathf.Pow(Lvl, 1/2) * 100);
        Hp += Random.Range(0, 6);
        CurrentHp = Hp;
        Str += Random.Range(0, 6);
        Def += Random.Range(0, 6);
        Dex += Random.Range(0, 6);
        Spe += Random.Range(0f, 0.5f);

    }

    public void HeroSetup()
    {

        Lvl = 1;
        Name = "Adberd";
        maxExp = 100;
        Hp = Random.Range(15, 25);
        CurrentHp = Hp;
        Str = Random.Range(3, 9);
        Def = Random.Range(3, 9);
        Dex = Random.Range(3, 9);
        Spe = Random.Range(1.0f, 1.8f);
        Gold = 5;

        Wearing = new List<Items>();
        Bag = new List<Items>();
        Debug.Log("The Adventure Starts now ");


    }

    public void AddItem(Items NewItem, int ItemLvl)
    {

        if (ItemLvl < 1)
        {
            ItemLvl = 1;
        }
        NewItem.lvl = ItemLvl;

        bool b = Wearing.Any(i => i.Type == NewItem.Type);

        if (!b)
        {
            Equip(NewItem);
            Debug.Log(Name + " is now wearing " + NewItem.Type + " on Lvl " + NewItem.lvl);
        }
        else
        {
            Bag.Add(NewItem);
            Debug.Log(Name + " added " + NewItem.Type + " on Lvl " + NewItem.lvl+" to his bag");
        }
    }

    public void Equip(Items Gear)
    {
        Wearing.Add(Gear);
        switch (Gear.Type)
        {
            case Items.ItemType.Weapon:
                for (int i = 0; i <= Gear.lvl; i++)
                {
                    Gear.strength += Random.Range(0, 3);
                }
                Str += (int)Gear.strength;
                break;
            case Items.ItemType.Armor:
                for (int i = 0; i <= Gear.lvl; i++)
                {
                    Gear.strength += Random.Range(0, 3);
                }
                Def += (int)Gear.strength;
                break;
            case Items.ItemType.Shoes:
                for (int i = 0; i <= Gear.lvl; i++)
                {
                    Gear.strength += Random.Range(0f,0.5f);
                }
                Spe += Gear.strength;
                break;
            case Items.ItemType.Gloves:
                for (int i = 0; i <= Gear.lvl; i++)
                {
                    Gear.strength += Random.Range(0, 3);
                }
                Dex += (int)Gear.strength;
                break;
            case Items.ItemType.Accessory:
                for (int i = 0; i <= Gear.lvl; i++)
                {
                    Gear.strength += Random.Range(0, 2);
                }
                Hp += (int)Gear.strength;
                CurrentHp += (int)Gear.strength;
                break;
        }
    }

    public void Unequip(Items Gear)
    {
        Wearing.Remove(Gear);
        switch (Gear.Type)
        {
            case Items.ItemType.Weapon:
                Str -= (int)Gear.strength;
                break;
            case Items.ItemType.Armor:
                Def -= (int)Gear.strength;
                break;
            case Items.ItemType.Shoes:
                Spe -= Gear.strength;
                break;
            case Items.ItemType.Gloves:
                Dex -= (int)Gear.strength;
                break;
            case Items.ItemType.Accessory:
                Hp -= (int)Gear.strength;
                CurrentHp -= (int)Gear.strength;
                break;
        }
        Bag.Add(Gear);

    }

    public void SwapGear(Items Gear)
    {
        Unequip(Gear);
        Equip(Gear);
    }

}
