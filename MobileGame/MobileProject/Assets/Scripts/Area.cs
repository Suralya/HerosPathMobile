using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    public Position CurrentPos=new Position();
    public enum ArealTypes {Healing, weakMonster, Monster, strongMonster, Neutral };

    public ArealTypes AreaType;

    public int Heal, Experience =0;

    public int Level, Hp, Str, Def, Dex=0;
    public float Spe=0f;

    public int ID;


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

                if (HeroStats.CurrentHp +Heal > HeroStats.Hp)
                {
                    HeroStats.CurrentHp = HeroStats.Hp;
                }
                else
                {
                    HeroStats.CurrentHp += Heal;
                }

                break;
            case ArealTypes.Neutral:
                Experience = 1;
                HeroStats.AddExp(Experience);
                break;
            case ArealTypes.weakMonster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl - 6, HeroStats.Lvl - 3);
                if (Level < 1)
                    Level = 1;

                SetMonsterStats();
                StartCoroutine(Fight(HeroStats));

                HeroStats.MonsterCounter++;

                break;
            case ArealTypes.Monster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl - 2, HeroStats.Lvl + 2);
                if (Level < 1)
                    Level = 1;

                SetMonsterStats();
                StartCoroutine(Fight(HeroStats));

                HeroStats.MonsterCounter++;

                break;
            case ArealTypes.strongMonster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl + 1, HeroStats.Lvl + 3);

                SetMonsterStats();
                StartCoroutine(Fight(HeroStats));

                HeroStats.MonsterCounter++;

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

        Hp += Hp * (Level*3 / 100);
        Str += Str * (Level*3 / 100);
        Def += Def * (Level*3 / 100);
        Dex += Dex * (Level*3 / 100);
        Spe += Spe * (Level*3 / 100);


        Experience = (int) (Level*((Mathf.Pow(Level, 1 / 2)/4) * 100)* 0.6f);

        Debug.Log("Die Werte des Monsters sind:");
        Debug.Log("Level: " + Level);
        Debug.Log("HP: " + Hp);
        Debug.Log("Strength: " + Str);
        Debug.Log("Defense: " + Def);
        Debug.Log("Dexterity: " + Dex);
        Debug.Log("Speed: " + Spe);
  

    }

    public IEnumerator Fight(Hero HeroStats)
    {
        int DamageApplyed;
        HeroStats.isFighting = true;
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
                Damage = (int)(Str - (HeroStats.Def * Random.Range(0.6f, 0.95f)));
                if (Damage < 1)
                {
                    Damage = 1;
                }
                if (Random.Range(0f, 10f) < CriticalMonster)
                {
                    Damage *= 2;
                }
                DamageApplyed = HeroStats.CurrentHp -Damage;
                if (DamageApplyed <0)
                {
                    DamageApplyed = 0;
                }
                while (HeroStats.CurrentHp != DamageApplyed)
                {
                    HeroStats.CurrentHp -= 1;
                    yield return new WaitForEndOfFrame();

                }
               // yield return new WaitForSeconds(0.2f);

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
                yield return new WaitForSeconds(0.2f);

            }
        }
        else
        {
            Debug.Log("Der Held Startet");
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
                yield return new WaitForSeconds(0.2f);

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
                DamageApplyed = HeroStats.CurrentHp - Damage;
                if (DamageApplyed < 0)
                {
                    DamageApplyed = 0;
                }
                while (HeroStats.CurrentHp != DamageApplyed)
                {
                    HeroStats.CurrentHp -= 1;
                    yield return new WaitForEndOfFrame();

                }
                //yield return new WaitForSeconds(0.2f);

            }
        }

        HeroStats.isFighting = false;
        //Heros Experience gain
        if (HeroStats.CurrentHp > 0)
        {
            yield return new WaitForSeconds(0.1f);
            HeroStats.AddExp(Experience);
            Debug.Log("The Hero gained " + Experience + " Experience.");
            Debug.Log("The Hero has " + HeroStats.CurrentExp + " now.");
        }

    }




}





