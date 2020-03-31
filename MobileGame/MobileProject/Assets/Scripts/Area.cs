using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Position CurrentPos=new Position();
    public enum ArealTypes {Healing, weakMonster, Monster, strongMonster, Neutral };

    public ArealTypes AreaType;

    public int HealingPotential, MonsterbasisAtk, Experience;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAreaType(int Setting)
    {
        //Hier SwitchCase 0-3 und Default
    }

    public void SetAreaValues(Hero HeroStats)
    {
        //Hier die Werte, die den Held beeinflussen
    }


}
