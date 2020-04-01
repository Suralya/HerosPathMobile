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

                HeroStats.CurrentHp += Heal;
                if (HeroStats.CurrentHp > HeroStats.Hp)
                {
                    HeroStats.CurrentHp = HeroStats.Hp;
                }

                break;
            case ArealTypes.Neutral:
                Experience = 1;
                break;
            case ArealTypes.weakMonster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl - 6, HeroStats.Lvl -3);
                if (Level < 1)
                    Level = 1;
                SetMonsterStats();


                break;
            case ArealTypes.Monster:
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl - 2, HeroStats.Lvl+1);
                if (Level < 1)
                    Level = 1;
                SetMonsterStats();



                break;
            case ArealTypes.strongMonster:

                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl +1, HeroStats.Lvl + 3);
                SetMonsterStats();



                break;

        }


    }

    public void SetMonsterStats()
    {
        // Set Monster Stats
        for (int i = 0; i <= Level; i++)
        {
            Hp += Random.Range(1, 4);
            Str += Random.Range(1, 4);
            Def += Random.Range(1, 4);
            Dex += Random.Range(1, 4);
            Spe += Random.Range(0f, 0.3f);
        }
        Debug.Log("Die Werte des Monsters sind:");
        Debug.Log("HP: " + Hp);
        Debug.Log("Strength: " + Str);
        Debug.Log("Defense: " + Def);
        Debug.Log("Dexterity: " + Dex);
        Debug.Log("Speed: " + Spe);

    }



}





