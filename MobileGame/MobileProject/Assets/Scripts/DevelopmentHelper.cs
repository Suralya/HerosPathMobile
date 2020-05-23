using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentHelper : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            print("HeroLvlUp");
            HeroLvlUp();
        }

        if (Input.GetKeyDown("w"))
        {
            print("HeroSpeedUp");
            HeroSpeedUp();
        }

        if (Input.GetKeyDown("s"))
        {
            print("HeroLowerSpeed");
            HeroLowerSpeed();
        }

        if (Input.GetKeyDown("m"))
        {
            print("MoreMony");
            AddHeroMony();
        }

        if (Input.GetKeyDown("o"))
        {
            print("AddItem");
            AddHeroItem();
        }
    }

    public void HeroLvlUp()
    {
        Hero.CurrentHero.AddExp(Hero.CurrentHero.maxExp);
    }

    public void HeroSpeedUp()
    {
        Hero.CurrentHero.Spe += 1;
    }

    public void HeroLowerSpeed()
    {
        Hero.CurrentHero.Spe -= 1;
    }

    public void AddHeroMony()
    {
        Hero.CurrentHero.Gold += 10000;
    }

    public void AddHeroItem()
    {
        Items Temp = new Items(Picker.PickItem.Pick().Type);
        Hero.CurrentHero.AddItem(Temp, Random.Range(Hero.CurrentHero.Lvl, Hero.CurrentHero.Lvl + 5));
        StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().NewItemGained(Temp));
    }


}
