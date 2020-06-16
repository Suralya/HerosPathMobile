using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Position CurrentPos = new Position();

    public enum ArealTypes { Healing, Monster, Neutral, Treasure, Market };

    public ArealTypes AreaType;

    public int ID = 0;
    public float Probability;
    public float Weight;

    //Animationen
    public Animator HeroAnimation;
    public Animator MonsterAnimation;
    public float AnimationSpeed;

    //Area Influence on Hero
    public int Heal, Experience = 0;
    public float MaxHeal,MinHeal;

    public int itemprobability, moneyprobability;
    public int moneyamount;

    //MonsterStats
    public int Minlvl, Maxlvl = 0;

    public int Level, Hp, Str, Def, Dex = 0;
    public float Spe = 0f;

    //Marketplace
    public float Tax;
    public int minStoreItems, maxStoreItems;
    public int StoreInventoryCount;
    public List<Items> MarketStore = new List<Items>();

    // Update is called once per frame
    private void Update()
    {
    }

    // Ausführen der Arealen Eigenschaften
    public void ArealAction(Hero HeroStats)
    {
        HeroAnimation = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().HeroAnimation;
        //Hier die Werte, die den Held beeinflussen
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MarketArea.enabled)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MarketArea.enabled = false;
        }

        switch (AreaType)
        {
            case ArealTypes.Healing:
                Heal = (int)(Random.Range(MinHeal, MaxHeal) * HeroStats.Hp);

                //HealingArealParticles
                foreach (ParticleSystem P in GetComponentsInChildren<ParticleSystem>())
                {
                    P.Play();
                }
                if (HeroStats.CurrentHp + Heal > HeroStats.Hp)
                {
                    HeroStats.CurrentHp = HeroStats.Hp;
                }
                else
                {
                    HeroStats.CurrentHp += Heal;
                }
 
                break;

            case ArealTypes.Neutral:
                HeroStats.AddExp(Experience);
                if (moneyamount!=0&& Random.Range(0, 100) <= moneyprobability)
                {
                    HeroStats.Gold += moneyamount;
                    StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MoneyGained(moneyamount));
                }
                break;

            case ArealTypes.Monster:
                if (GetComponentsInChildren<Animator>().Length > 0)
                    MonsterAnimation = GetComponentsInChildren<Animator>()[0];
                // Set Monster Lvl
                Level = Random.Range(HeroStats.Lvl+Minlvl, HeroStats.Lvl + Maxlvl);
                if (Level < 1)
                { Level = 1; }

                SetMonsterStats();
                StartCoroutine(Fight(HeroStats));

                HeroStats.MonsterCounter++;

                break;

            case ArealTypes.Treasure:
                //set Item
                if (Random.Range(0, 100) <= itemprobability)
                {
                    Items Temp = new Items(WeightedItemPicker.PickItem.Pick().Type);
                    HeroStats.AddItem(Temp, Random.Range(HeroStats.Lvl - 3, HeroStats.Lvl + 5));
                    StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().NewItemGained(Temp));
                }
                //set Money
                if (Random.Range(0, 100) <= moneyprobability)
                {
                    if (moneyamount == 0)
                    {
                        int mon = HeroStats.Lvl * Random.Range(4, 20);
                        HeroStats.Gold += mon;
                        StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MoneyGained(mon));
                    }
                    else
                    {
                        HeroStats.Gold += moneyamount;
                        StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MoneyGained(moneyamount));
                    }
                }
                break;

            case ArealTypes.Market:

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MarketArea.enabled = true;
                Tax = Random.Range(1.03f, 1.35f);
                StoreInventoryCount = Random.Range(minStoreItems, maxStoreItems);

                for (int i = 1; i <= StoreInventoryCount; i++)
                {
                    Items NewStoreItem = new Items(WeightedItemPicker.PickItem.Pick().Type);
                    NewStoreItem.lvl = Random.Range(HeroStats.Lvl - 4, HeroStats.Lvl + 5);
                    if (NewStoreItem.lvl < 1)
                    {
                        NewStoreItem.lvl = 1;
                    }
                    HeroStats.SetupGear(NewStoreItem);
                    MarketStore.Add(NewStoreItem);
                }

                break;
        }

    }

    //Sets Monster Stats
    public void SetMonsterStats()
    {
        for (int i = 0; i <= Level; i++)
        {
            Hp += Random.Range(1, 6);
            Str += Random.Range(1, 6);
            Def += Random.Range(1, 6);
            Dex += Random.Range(1, 6);
            Spe += Random.Range(0f, 0.5f);
            if (i % 5 == 0)
            {
                Hp += Hp * (15 / 100);
                Str += Str * (15 / 100);
                Def += Def * (15 / 100);
                Dex += Dex * (15 / 100);
                Spe += Spe * (15 / 100);
            }
        }

        Experience = (int)(Level * ((Mathf.Pow(Level, 1 / 2) / 4) * 100) * 0.3f);

        //reducing worth of weak monsters on long run
        if (Level < Hero.CurrentHero.Lvl-1 && Hero.CurrentHero.Lvl > 10)
        {
            Experience /= 3;
        }

        if (GetComponentsInChildren<Animator>().Length > 0)
        {
            if (Spe < 7)
            {
                AnimationSpeed = Spe;
                if (Spe < 1.2f)
                { AnimationSpeed = 1.2f; }
            }
            else
            {
                AnimationSpeed = 7;
            }
            MonsterAnimation.speed = AnimationSpeed;
        }

    }

    public IEnumerator Fight(Hero HeroStats)
    {


        if (Hero.CurrentHero.Spe < 10)
        {
            HeroAnimation.speed = Hero.CurrentHero.Spe;
        }
        else
        {
            HeroAnimation.speed = 10;
        }


        int DamageApplyed;
        Hero.CurrentHero.isFighting = true;
        int Damage;
        double CriticalMonster;
        CriticalMonster = (Dex / 8) - (Str / 10) * 0.8;
        if (CriticalMonster > 8.5)
        {
            CriticalMonster = 8.5;
        }

        double CriticalHero;
        CriticalHero = (HeroStats.Dex / 8) - (HeroStats.Str / 10) * 0.8;
        if (CriticalHero > 9.5)
        {
            CriticalHero = 9.5;
        }
        yield return new WaitForSeconds(0.5f);
        //initiative Check
        if (Dex > HeroStats.Dex)
        {
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
                DamageApplyed = HeroStats.CurrentHp - Damage;
                    PlayMonsterAnimation("Attack");
                    yield return new WaitForSeconds(2f / AnimationSpeed);
                
                if (DamageApplyed < 0)
                {
                    DamageApplyed = 0;
                }
                
                while (HeroStats.CurrentHp != DamageApplyed)
                {
                    HeroStats.CurrentHp -= 1;
                    yield return new WaitForEndOfFrame();
                }


                //HeroTurn
                HeroAnimation.SetInteger("FightState", 1);
                Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                if (Damage < 1)
                {
                    Damage = 1;
                }
                if (Random.Range(0f, 10f) < CriticalHero)
                {
                    Damage *= 2;
                    HeroAnimation.SetInteger("FightState", 3);
                }
                HeroAnimation.SetBool("Attacking", true);
                Hp -= Damage;
                yield return new WaitForSeconds(2f / HeroAnimation.speed);
                HeroAnimation.SetBool("Attacking", false);
                HeroAnimation.SetInteger("FightState", 2);
                if (HeroStats.Spe > Spe + (Spe / 3) && Hp>0)
                {
                    Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                    if (Damage < 1)
                    {
                        Damage = 1;
                    }
                    if (Random.Range(0f, 10f) < CriticalHero)
                    {
                        Damage *= 2;
                        HeroAnimation.SetInteger("FightState", 3);
                    }
                    HeroAnimation.SetBool("Attacking", true);
                    Hp -= Damage;
                    yield return new WaitForSeconds(2f / HeroAnimation.speed);
                    HeroAnimation.SetBool("Attacking", false);
                }
                if (Hp <= 0)
                {
                    PlayMonsterAnimation("Die");
                }
            }
        }
        else
        {
            //Hero Starts
            while (Hp > 0 && HeroStats.CurrentHp > 0)
            {
                //HeroTurn
                HeroAnimation.SetInteger("FightState", 1);
                Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                if (Damage < 1)
                {
                    Damage = 1;
                }
                if (Random.Range(0f, 10f) < CriticalHero)
                {
                    Damage *= 2;
                    HeroAnimation.SetInteger("FightState", 3);
                }
                HeroAnimation.SetBool("Attacking", true);
                Hp -= Damage;
                yield return new WaitForSeconds(2f / HeroAnimation.speed);
                HeroAnimation.SetBool("Attacking", false);
                HeroAnimation.SetInteger("FightState", 2);
                if (HeroStats.Spe > Spe + (Spe / 3) && Hp > 0)
                {
                    Damage = (int)(HeroStats.Str - (Def * Random.Range(0.6f, 0.95f)));
                    if (Damage < 1)
                    {
                        Damage = 1;
                    }
                    if (Random.Range(0f, 10f) < CriticalHero)
                    {
                        Damage *= 2;
                        HeroAnimation.SetInteger("FightState", 3);
                    }
                    HeroAnimation.SetBool("Attacking", true);
                    Hp -= Damage;
                    yield return new WaitForSeconds(2f / HeroAnimation.speed);
                    HeroAnimation.SetBool("Attacking", false);
                }
                if (Hp <= 0)
                {
                    PlayMonsterAnimation("Die");
                }

                //MonsterTurn
                if (Hp > 0)
                {
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
                        PlayMonsterAnimation("Attack");
                        yield return new WaitForSeconds(2f / AnimationSpeed);                    

                    
                    while (HeroStats.CurrentHp != DamageApplyed)
                    {
                        HeroStats.CurrentHp -= 1;
                        yield return new WaitForEndOfFrame();
                    }
                    
                }
                //yield return new WaitForSeconds(0.2f);
            }
        }

        Hero.CurrentHero.isFighting = false;
        //Heros Experience gain and Loot
        if (HeroStats.CurrentHp > 0)
        {
            yield return new WaitForSeconds(0.1f);
            HeroStats.AddExp(Experience);

            if (Random.Range(0, 100) <= moneyprobability)
            {
                if (moneyamount == 0)
                {
                    int mon = Level * Random.Range(3, 10);
                    HeroStats.Gold += mon;
                    StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MoneyGained(mon));
                }
                else
                {
                    HeroStats.Gold += moneyamount;
                    StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().MoneyGained(moneyamount));
                }
            }
            if (Random.Range(0, 100) <= itemprobability)
            {
                Items Temp = new Items(WeightedItemPicker.PickItem.Pick().Type);
                HeroStats.AddItem(Temp, Random.Range(Level - 5, Level + 3));
                StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().NewItemGained(Temp));
            }
        }
    }

    public void PlayMonsterAnimation(string Animation)
    {
        StartCoroutine(StartMonsterAnimation(Animation));
    }

    public IEnumerator StartMonsterAnimation(string Animation)
    {
        if (GetComponentsInChildren<Animator>().Length > 0)
        {

                MonsterAnimation.speed = AnimationSpeed;


            switch (Animation)
            {
                case "Attack":
                    MonsterAnimation.SetBool("Attacking", true);
                    yield return new WaitForSeconds(1f / MonsterAnimation.speed);
                    MonsterAnimation.SetBool("Attacking", false);
                    break;

                case "Die":
                    MonsterAnimation.SetBool("Dying", true);
                    break;

            }


        }
    }
}