using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Position CurrentPos=new Position();
    public enum ArealTypes {Healing, weakMonster, Monster, strongMonster, Neutral };

    public ArealTypes AreaType;

    public int Heal, MonsterbasisAtk, Experience =0;

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
                break;
            case ArealTypes.Monster:
                break;
            case ArealTypes.strongMonster:
                break;

        }


    }



}
