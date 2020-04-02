using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Position CurrentPos=new Position();
    public enum ArealTypes {Healing, weakMonster, Monster, strongMonster, Neutral };

    public ArealTypes AreaType;

    public int Heal, Experience =0;

    public int Level, Hp, Str, Def, Dex;
    public float Spe;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArealAction(Hero HeroStats)
    {
        //Hier die Werte, die den Held beeinflussen

        switch (AreaType)
        {
            case ArealTypes.Healing:
                Heal = (int) (Random.Range(0.3f, 0.8f) * HeroStats.Hp);
                Debug.Log("The Hero has got " + HeroStats.CurrentHp + " HP");
                HeroStats.CurrentHp += Heal;
                if (HeroStats.CurrentHp > HeroStats.Hp)
                {
                    HeroStats.CurrentHp = HeroStats.Hp;
                }
                Debug.Log("The Hero was healed " + Heal + " HP");
                Debug.Log("The Hero has now " + HeroStats.CurrentHp + " HP");
                break;
            case ArealTypes.Neutral:
                Experience = 1;
                HeroStats.CurrentExp += Experience;
                break;
            case ArealTypes.weakMonster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl - 6, HeroStats.Lvl -3);
                if (Level < 1)
                    Level = 1;
                SetMonsterStats();
                Fight(HeroStats);

                break;
            case ArealTypes.Monster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl - 2, HeroStats.Lvl+2);
                if (Level < 1)
                    Level = 1;
                SetMonsterStats();
                Fight(HeroStats);

                break;
            case ArealTypes.strongMonster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl +1, HeroStats.Lvl + 3);
                SetMonsterStats();
                Fight(HeroStats);

                break;

        }


    }

    public void SetMonsterStats()
    {
        for (int i = 0; i <= Level; i++)
        {
            Hp += Random.Range(1, 4);
            Str += Random.Range(1, 4);
            Def += Random.Range(1, 4);
            Dex += Random.Range(1, 4);
            Spe += Random.Range(0f, 0.3f);
        }

        Experience = (int) (Level*((Mathf.Pow(Level, 1 / 2)/4) * 100)* 0.7f);

        Debug.Log("Die Werte des Monsters sind:");
        Debug.Log("Level: " + Level);
        Debug.Log("HP: " + Hp);
        Debug.Log("Strength: " + Str);
        Debug.Log("Defense: " + Def);
        Debug.Log("Dexterity: " + Dex);
        Debug.Log("Speed: " + Spe);
  

    }

    public void Fight(Hero HeroStats)
    {
        int Damage;
        double CriticalMonster;
        CriticalMonster = (Dex / 8) * 0.1 - (Str / 10) * 0.1;
        if (CriticalMonster > 9.5)
        {
            CriticalMonster = 9.5;
        }

        double CriticalHero;
        CriticalHero = (HeroStats.Dex / 8) * 0.1 - (HeroStats.Str / 10) * 0.1;
        if (CriticalHero > 9.5)
        {
            CriticalHero = 9.5;
        }

        //initiative Check
        if (Dex > HeroStats.Dex)
        {
            Debug.Log("Das Monster Startet");
            //Monster Starts
            while (Hp > 0 && HeroStats.CurrentHp > 0)
            {
                //MonsterTurn
                Damage = (int) (Str - (HeroStats.Def * Random.Range(0.6f, 0.95f)));
                if (Damage < 1)
                {
                    Damage = 1;
                }
                if (Random.Range(0f, 10f) < CriticalMonster)
                {
                    Damage *= 2;
                }
                HeroStats.CurrentHp -= Damage;

                //HeroTurn
                Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                if (Damage < 1)
                {
                    Damage = 1;
                }
                if (Random.Range(0f, 10f) < CriticalHero)
                {
                    Damage *= 2;
                }
                Hp -= Damage;
                if (HeroStats.Spe > Spe + (Spe / 3))
                {
                    Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                    if (Damage < 1)
                    {
                        Damage = 1;
                    }
                    if (Random.Range(0f, 10f) < CriticalHero)
                    {
                        Damage *= 2;
                    }
                    Hp -= Damage;
                }

            }
        }
        else
        {
            Debug.Log("Das Held Startet");
            //Hero Starts
            while (Hp > 0 && HeroStats.CurrentHp > 0)
            {
                //HeroTurn
                Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                if (Damage < 1)
                {
                    Damage = 1;
                }
                if (Random.Range(0f, 10f) < CriticalHero)
                {
                    Damage *= 2;
                }
                Hp -= Damage;
                if (HeroStats.Spe > Spe + (Spe / 3))
                {
                    Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                    if (Damage < 1)
                    {
                        Damage = 1;
                    }
                    if (Random.Range(0f, 10f) < CriticalHero)
                    {
                        Damage *= 2;
                    }
                    Hp -= Damage;
                }

                //MonsterTurn
                Damage = (int)(Str - (HeroStats.Def * Random.Range(0.6f, 0.95f)));
                if (Damage < 1)
                {
                    Damage = 1;
                }
                if (Random.Range(0f, 10f) < CriticalMonster)
                {
                    Damage *= 2;
                }
                HeroStats.CurrentHp -= Damage;

            }
        }

        //Heros Experience gain
        if (HeroStats.CurrentHp > 0)
        {
            HeroStats.CurrentExp += Experience;
            Debug.Log("The Hero gained "+Experience+" Experience.");
            Debug.Log("The Hero has " + HeroStats.CurrentExp + " now.");
        }


    }





}





