﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameManager GM;

    //Decrease/Increase Speed
    public float IndicationSpeed = 1f;

    //UI HeroStatus
    public Text HPText;
    public Image HPIndicator;
    public Image ExpIndicator;
    public Text LevelText, StrText, DefText, DexText, SpeText;

    public Text EnmyLevelText, EnmyStrText, EnmyDefText, EnmyDexText, EnmySpeText;

    //GameUI
    public Text HeroName, HeroMony;


    public Canvas DeathScreen;

    //ScoreListUI
    public Canvas ScorePanel;
    public RectTransform content;
    public GameObject Entry;
    public List<GameObject> TableContent;

    [SerializeField]
    public Transform SpawnPoint = null;

    //Stats And Inventory UI
    public Text HeroStats;

    public Canvas InventoryAndStatsPanel;
    public RectTransform EquipmentContent, BagContent;
    public GameObject EquipedItem,BagItem;
    public List<GameObject> TableEquipment, TableBag;

    public Transform EquipedSpawn,BagSpawn;




    // Start is called before the first frame update
    void Start()
    {
        InventoryAndStatsPanel.enabled = false;
        ScorePanel.enabled = false;
        DeathScreen.enabled = false;
        HeroName.text = Hero.CurrentHero.Name;
        UpdateHeroUI();

    }

    // Update is called once per frame
    void Update()
    {
     UpdateHeroUI();
        if (Hero.CurrentHero.CurrentHp <= 0)
        {
            if (!DeathScreen.enabled)
            {
                ScoreList.Score.AddEntry(new Entry(Hero.CurrentHero.Name, Hero.CurrentHero.Lvl, Hero.CurrentHero.MonsterCounter, Hero.CurrentHero.StageCounter));
                Hero.CurrentHero.Lvl = 0;
                GM.SafeCurrentGame();
                ScoreList.Score.ShowList();
                DeathScreen.enabled = true;
            }


        }

    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void UpdateHeroUI()
    {
        //float currentfill;
        //Bars
        HPText.text = "HP: " + Hero.CurrentHero.CurrentHp + " / " + Hero.CurrentHero.Hp;

        if (HPIndicator.fillAmount < (float)Hero.CurrentHero.CurrentHp / Hero.CurrentHero.Hp)
        {
          //  GM.Hero.isFighting = true;

            HPIndicator.fillAmount += IndicationSpeed * Time.deltaTime;
            if (HPIndicator.fillAmount > (float)Hero.CurrentHero.CurrentHp / Hero.CurrentHero.Hp)
            {
                HPIndicator.fillAmount = (float)Hero.CurrentHero.CurrentHp / Hero.CurrentHero.Hp;
            }



        }
        else if (HPIndicator.fillAmount > (float)Hero.CurrentHero.CurrentHp / Hero.CurrentHero.Hp)
        {
          //  GM.Hero.isFighting = true;

            HPIndicator.fillAmount -= IndicationSpeed * Time.deltaTime;
            if (HPIndicator.fillAmount < (float)Hero.CurrentHero.CurrentHp / Hero.CurrentHero.Hp)
            {
                HPIndicator.fillAmount = (float)Hero.CurrentHero.CurrentHp / Hero.CurrentHero.Hp;
            }

        }
        else if (HPIndicator.fillAmount == (float)Hero.CurrentHero.CurrentHp / Hero.CurrentHero.Hp)
        {
            Hero.CurrentHero.isFighting = false;
        }

        if (ExpIndicator.fillAmount < (float)Hero.CurrentHero.CurrentExp / Hero.CurrentHero.maxExp)
        {
            ExpIndicator.fillAmount += IndicationSpeed * Time.deltaTime;
            if (ExpIndicator.fillAmount > (float)Hero.CurrentHero.CurrentExp / Hero.CurrentHero.maxExp)
            {
                ExpIndicator.fillAmount = (float)Hero.CurrentHero.CurrentExp / Hero.CurrentHero.maxExp;
            }

        }
        else
        {
            ExpIndicator.fillAmount = (float)Hero.CurrentHero.CurrentExp / Hero.CurrentHero.maxExp;
        }

        HeroMony.text = Hero.CurrentHero.Gold.ToString()+ " Gold";

        //Stats public Text LevelText,StrText,DefText,DexText,SpeText;
        LevelText.text = "Level: " + Hero.CurrentHero.Lvl;
        StrText.text = "Strength: " + Hero.CurrentHero.Str;
        DefText.text = "Defense: " + Hero.CurrentHero.Def;
        DexText.text = "Dexterity: " + Hero.CurrentHero.Dex;
        SpeText.text = "Speed: " + Hero.CurrentHero.Spe;


    }

    public void UpdateEnemyUI(Area Enemy)
    {
        switch (Enemy.AreaType)
        {
            case Area.ArealTypes.Healing:
                EnmyLevelText.text = "Healing Areal";
                EnmyStrText.text = "Healing: " + Enemy.Heal;
                EnmyDefText.text = "";
                EnmyDexText.text = "";
                EnmySpeText.text = "";
                break;
            case Area.ArealTypes.Neutral:
                EnmyLevelText.text = "Neutral Areal";
                EnmyStrText.text = "";
                EnmyDefText.text = "";
                EnmyDexText.text = "";
                EnmySpeText.text = "";
                break;
            case Area.ArealTypes.weakMonster:
                EnmyLevelText.text = "Level: " + Enemy.Level;
                EnmyStrText.text = "Strength: " + Enemy.Str;
                EnmyDefText.text = "Defense: " + Enemy.Def;
                EnmyDexText.text = "Dexterity: " + Enemy.Dex;
                EnmySpeText.text = "Speed: " + Enemy.Spe;
                break;
            case Area.ArealTypes.Monster:
                EnmyLevelText.text = "Level: " + Enemy.Level;
                EnmyStrText.text = "Strength: " + Enemy.Str;
                EnmyDefText.text = "Defense: " + Enemy.Def;
                EnmyDexText.text = "Dexterity: " + Enemy.Dex;
                EnmySpeText.text = "Speed: " + Enemy.Spe;
                break;
            case Area.ArealTypes.strongMonster:
                EnmyLevelText.text = "Level: " + Enemy.Level;
                EnmyStrText.text = "Strength: " + Enemy.Str;
                EnmyDefText.text = "Defense: " + Enemy.Def;
                EnmyDexText.text = "Dexterity: " + Enemy.Dex;
                EnmySpeText.text = "Speed: " + Enemy.Spe;
                break;

        }
    }

    public void ShowScorelist()
    {
        if (ScorePanel.enabled)
        {
            ScorePanel.enabled = false;
            foreach (GameObject Entry in TableContent)
            {
                Destroy(Entry);  
            }
            TableContent.Clear();
        }
        else
        {
            ScorePanel.enabled = true;
            content.sizeDelta = new Vector2(0, ScoreList.Score.HighScoreList.Count() * 60);

            for (int i = 0; i < ScoreList.Score.HighScoreList.Count(); i++)
            {
                float spawnY = i * 60;
                Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
                TableContent.Add( Instantiate(Entry, pos, SpawnPoint.rotation));
                TableContent[i].transform.SetParent(SpawnPoint, false);
                TableContent[i].GetComponent<ScoreEntrys>().Entrynumber.text = "Nr." + (i+1);
                TableContent[i].GetComponent<ScoreEntrys>().HeroName.text = ScoreList.Score.HighScoreList[i].HName;
                TableContent[i].GetComponent<ScoreEntrys>().HeroLvl.text = "Lvl." + ScoreList.Score.HighScoreList[i].HLvl;

            }
        }

    }

    public void ShowStatsAndInventory()
    {
        if (InventoryAndStatsPanel.enabled)
        {
            InventoryAndStatsPanel.enabled = false;
            foreach (GameObject Item in TableEquipment)
            {
                Destroy(Item);
            }
            TableEquipment.Clear();

            foreach (GameObject Item in TableBag)
            {
                Destroy(Item);
            }
            TableBag.Clear();

        }
        else
        {
            InventoryAndStatsPanel.enabled = true;

            //HeroStatusPanel
            HeroStats.text = "Level "+ Hero.CurrentHero.Lvl+
                "\nHP: "+ Hero.CurrentHero.CurrentHp+ "/" + Hero.CurrentHero.Hp+
                "\nStr: " + Hero.CurrentHero.Str +
                "\nDef: "+ Hero.CurrentHero.Def +
                "\nDex: "+ Hero.CurrentHero.Dex +
                "\nSpe: "+ (Math.Truncate(100*Hero.CurrentHero.Spe)/100);

            //Hero Equpped List

            EquipmentContent.sizeDelta = new Vector2(0, ScoreList.Score.HighScoreList.Count() * 60);

            for (int i = 0; i < Hero.CurrentHero.Wearing.Count(); i++)
            {
                float spawnY = i * 45;
                Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
                TableEquipment.Add(Instantiate(EquipedItem, pos, EquipedSpawn.rotation));
                TableEquipment[i].transform.SetParent(EquipedSpawn, false);
                TableEquipment[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + Hero.CurrentHero.Wearing[i].lvl;
                    switch (Hero.CurrentHero.Wearing[i].Type)
                    {
                        case Items.ItemType.Weapon:
                        TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                        break;
                        case Items.ItemType.Armor:
                        TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                        break;
                        case Items.ItemType.Shoes:
                        TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                        break;
                        case Items.ItemType.Gloves:
                        TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                        break;
                        case Items.ItemType.Accessory:
                        TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                        break;
                    }
                TableEquipment[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Wearing[i].strength) / 100);
                TableEquipment[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Wearing[i];

            }

            //Hero Bag List

            BagContent.sizeDelta = new Vector2(0, ScoreList.Score.HighScoreList.Count() * 60);

            for (int i = 0; i < Hero.CurrentHero.Bag.Count(); i++)
            {
                float spawnY = i * 45;
                Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
                TableBag.Add(Instantiate(BagItem, pos, BagSpawn.rotation));
                TableBag[i].transform.SetParent(BagSpawn, false);
                TableBag[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + Hero.CurrentHero.Bag[i].lvl;
                switch (Hero.CurrentHero.Bag[i].Type)
                {
                    case Items.ItemType.Weapon:
                        TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                        break;
                    case Items.ItemType.Armor:
                        TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                        break;
                    case Items.ItemType.Shoes:
                        TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                        break;
                    case Items.ItemType.Gloves:
                        TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                        break;
                    case Items.ItemType.Accessory:
                        TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                        break;
                }
                TableBag[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Bag[i].strength) / 100);
                TableBag[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Bag[i];

            }


        }

    }

    //Enables or Disables Scorlist/InvoentoryandStats Menu
    public void EnableDisableSideMenu()
    {
        if (InventoryAndStatsPanel.enabled || ScorePanel.enabled)
        {
            InventoryAndStatsPanel.enabled = true;
            ScorePanel.enabled = true;
            ShowStatsAndInventory();
            ShowScorelist();
            GM.InMenu = false;
        }
        else
        {
            GM.InMenu = true;
            ShowStatsAndInventory();
        }
    }


    public void UpdateInventoryTables()
    {

        foreach (GameObject Item in TableEquipment)
        {
            Destroy(Item);
        }
        TableEquipment.Clear();

        foreach (GameObject Item in TableBag)
        {
            Destroy(Item);
        }
        TableBag.Clear();
        //Hero Equpped List

        EquipmentContent.sizeDelta = new Vector2(0, ScoreList.Score.HighScoreList.Count() * 60);

        for (int i = 0; i < Hero.CurrentHero.Wearing.Count(); i++)
        {
            float spawnY = i * 45;
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            TableEquipment.Add(Instantiate(EquipedItem, pos, EquipedSpawn.rotation));
            TableEquipment[i].transform.SetParent(EquipedSpawn, false);
            TableEquipment[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + Hero.CurrentHero.Wearing[i].lvl;
            switch (Hero.CurrentHero.Wearing[i].Type)
            {
                case Items.ItemType.Weapon:
                    TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                    break;
                case Items.ItemType.Armor:
                    TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                    break;
                case Items.ItemType.Shoes:
                    TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                    break;
                case Items.ItemType.Gloves:
                    TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                    break;
                case Items.ItemType.Accessory:
                    TableEquipment[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                    break;
            }
            TableEquipment[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Wearing[i].strength) / 100);
            TableEquipment[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Wearing[i];

        }

        //Hero Bag List

        BagContent.sizeDelta = new Vector2(0, ScoreList.Score.HighScoreList.Count() * 60);

        for (int i = 0; i < Hero.CurrentHero.Bag.Count(); i++)
        {
            float spawnY = i * 45;
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            TableBag.Add(Instantiate(BagItem, pos, BagSpawn.rotation));
            TableBag[i].transform.SetParent(BagSpawn, false);
            TableBag[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + Hero.CurrentHero.Bag[i].lvl;
            switch (Hero.CurrentHero.Bag[i].Type)
            {
                case Items.ItemType.Weapon:
                    TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                    break;
                case Items.ItemType.Armor:
                    TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                    break;
                case Items.ItemType.Shoes:
                    TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                    break;
                case Items.ItemType.Gloves:
                    TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                    break;
                case Items.ItemType.Accessory:
                    TableBag[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                    break;
            }
            TableBag[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Bag[i].strength) / 100);
            TableBag[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Bag[i];

        }



    }

}
