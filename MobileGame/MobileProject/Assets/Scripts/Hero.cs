using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero
{
    public static Hero CurrentHero;

    public string Name = "HERO";
    public int Lvl;

    public int maxExp;
    public int CurrentExp;

    public int Hp, CurrentHp, Str, Def, Dex;
    public int StageCounter, MonsterCounter;
    public float Spe;

    public bool ShowMessage=true;
    public bool isFighting = false;


    public Hero()
    {


    }


    public void AddExp(int expGain)
    {
        CurrentExp += expGain;
        if (CurrentExp >= maxExp)
        {
            LvlUp();
            Debug.Log("You Had a LvlUp, your new Lvl is " + Lvl + "!!");
            Debug.Log("Your new Stats are:");
            Debug.Log("HP: " + Hp);
            Debug.Log("Strength: " + Str);
            Debug.Log("Defense: " + Def);
            Debug.Log("Dexterity: " + Dex);
            Debug.Log("Speed: " + Spe);
            Debug.Log("You passed " + StageCounter + " Stages for this Lvl");
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
        maxExp = 100;
        Hp = Random.Range(15, 25);
        CurrentHp = Hp;
        Str = Random.Range(3, 9);
        Def = Random.Range(3, 9);
        Dex = Random.Range(3, 9);
        Spe = Random.Range(1.0f, 1.8f);

        Debug.Log("The Adventure Starts now ");

    }

}
