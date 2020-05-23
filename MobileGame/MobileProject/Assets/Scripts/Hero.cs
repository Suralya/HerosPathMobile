using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Hero
{
    public static Hero CurrentHero;

    public string Name = "NoName";
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

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().OnLvlUp.Play();

    }

    public void HeroSetup()
    {

        Lvl = 1;


        Name = GetName();
        maxExp = 100;
        Hp = Random.Range(15, 25);
        CurrentHp = Hp;
        Str = Random.Range(3, 9);
        Def = Random.Range(3, 9);
        Dex = Random.Range(3, 9);
        Spe = Random.Range(1.0f, 1.8f);
        Gold = 12;

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
        SetupGear(NewItem);

        bool b = Wearing.Any(i => i.Type == NewItem.Type);

        Debug.Log("Is Adberd wearing something like this?" + b);

        if (!b)
        {
            Equip(NewItem);
        }
        else
        {
            Bag.Add(NewItem);
        }
    }

    public void SetupGear(Items Gear)
    {
        switch (Gear.Type)
        {
            case Items.ItemType.Weapon:
                for (int i = 1; i <= Gear.lvl; i++)
                {
                    Gear.worth += Random.Range(0, 3);
                }
                break;
            case Items.ItemType.Armor:
                for (int i = 1; i <= Gear.lvl; i++)
                {
                    Gear.worth += Random.Range(0, 3);
                }
                break;
            case Items.ItemType.Shoes:
                for (int i = 1; i <= Gear.lvl; i++)
                {
                    Gear.worth += Random.Range(0f, 0.5f);
                }
                break;
            case Items.ItemType.Gloves:
                for (int i = 1; i <= Gear.lvl; i++)
                {
                    Gear.worth += Random.Range(0, 3);
                }
                break;
            case Items.ItemType.Accessory:
                for (int i = 1; i <= Gear.lvl; i++)
                {
                    Gear.worth += Random.Range(0, 5);
                }
                break;
        }
    }

    public void Equip(Items Gear)
    {

        Wearing.Add(Gear);
        switch (Gear.Type)
        {
            case Items.ItemType.Weapon:
                Str += (int)Gear.worth;
                break;
            case Items.ItemType.Armor:
                Def += (int)Gear.worth;
                break;
            case Items.ItemType.Shoes:
                Spe += Gear.worth;
                break;
            case Items.ItemType.Gloves:
                Dex += (int)Gear.worth;
                break;
            case Items.ItemType.Accessory:
                Hp += (int)Gear.worth;
                CurrentHp += (int)Gear.worth;
                break;
        }
        SortWearingGear();
    }

    public void Unequip(Items Gear)
    {
        Wearing.Remove(Gear);
        switch (Gear.Type)
        {
            case Items.ItemType.Weapon:
                Str -= (int)Gear.worth;
                break;
            case Items.ItemType.Armor:
                Def -= (int)Gear.worth;
                break;
            case Items.ItemType.Shoes:
                Spe -= Gear.worth;
                break;
            case Items.ItemType.Gloves:
                Dex -= (int)Gear.worth;
                break;
            case Items.ItemType.Accessory:
                Hp -= (int)Gear.worth;
                CurrentHp -= (int)Gear.worth;
                break;
        }
        Bag.Add(Gear);

    }

    public void SwapGear(Items Gear)
    {

        bool b = Wearing.Any(i => i.Type == Gear.Type);

        if (!b)
        {
            Equip(Gear);
        }
        else
        {
            Unequip(Wearing.Find(i=> i.Type == Gear.Type));
            Equip(Gear);
        }
        Bag.Remove(Gear);

    }

    public void SortWearingGear()
    {
        Debug.Log("Liste wird Sortiert");
        var sortedEquipment= Wearing.OrderBy(i =>
                    i.Type == Items.ItemType.Weapon ? 1
                  : i.Type == Items.ItemType.Armor ? 2
                  : i.Type == Items.ItemType.Accessory ? 3
                  : i.Type == Items.ItemType.Gloves ? 4
                  : 5).ToList();

        Wearing = sortedEquipment; 


    }

    public string GetName()
    {

        TextAsset file = Resources.Load("Hero_Names") as TextAsset;
        string jsonString = file.ToString();

        HeroNameList<HeroNames> HeroNamesList = JsonUtility.FromJson<HeroNameList<HeroNames>>(jsonString);
        List<string> HeroNames= new List<string>();
        //put everything into a list
        for (int i = 0; i < HeroNamesList.NameList.Length; i++)
        {
            HeroNames.Add(HeroNamesList.NameList[i].Name);
        }

        //Randomize Namelist
        for (int i = HeroNames.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            string temp = HeroNames[i];
            HeroNames[i] = HeroNames[j];
            HeroNames[j] = temp;
        }

        return HeroNames[Random.Range(0, HeroNames.Count)];

    }

    public void WearBestEquipment()
    {
        Debug.Log("Optimizing");
        foreach (Items Gear in Bag.ToList<Items>())
        {
            bool b = Wearing.Any(i => i.Type == Gear.Type);
            if (!b)
            {
                Equip(Gear);
                Bag.Remove(Gear);
            }
            else
            {
                if (Wearing.Find(i => i.Type == Gear.Type).worth < Gear.worth)
                {
                    SwapGear(Gear);
                }
            }

        }
        Debug.Log("finished");


    }


}
