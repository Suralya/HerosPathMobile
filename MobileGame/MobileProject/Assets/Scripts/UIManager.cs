using System.Collections;
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

    private float height = 0f;

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

    //Marketplace UI
    public Canvas MarketArea, Marketplace;
    public RectTransform BagList, MarketInventoryList;
    public GameObject ShopItem,MarketBagItem;
    public List<GameObject> BagItems, ShopItems;
    public Transform ShopBagSpawn, ShopInventorySpawn;

    public Canvas ShoppingError;

    public Canvas SafetyQuestionShopping;
    public Text EquiToBuy, EquiWorn, Price;
    public Items ItemOfChoice;


    void Start()
    {
        InventoryAndStatsPanel.enabled = false;
        ScorePanel.enabled = false;
        DeathScreen.enabled = false;
        MarketArea.enabled = false;
        Marketplace.enabled = false;
        ShoppingError.enabled = false;
        SafetyQuestionShopping.enabled = false;

        HeroName.text = Hero.CurrentHero.Name;
        UpdateHeroUI();

    }

    void FixedUpdate()
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
            default:
                EnmyLevelText.text = "Special Area";
                EnmyStrText.text = "";
                EnmyDefText.text = "";
                EnmyDexText.text = "";
                EnmySpeText.text = "";
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
            height = content.GetComponent<RectTransform>().rect.width;
            SpawnPoint.localPosition = new Vector3(0,0-( height / Entry.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
            content.sizeDelta = new Vector2(0,3+ Hero.CurrentHero.Wearing.Count() * height / Entry.GetComponent<AspectRatioFitter>().aspectRatio);

            for (int i = 0; i < ScoreList.Score.HighScoreList.Count(); i++)
            {
                float spawnY = (i * height + 3) - SpawnPoint.localPosition.y;
                Vector3 pos = new Vector3(SpawnPoint.localPosition.x, -spawnY, SpawnPoint.position.z);
                TableContent.Add( Instantiate(Entry, pos, SpawnPoint.rotation));
                TableContent[i].transform.SetParent(content, false);
                TableContent[i].GetComponent<ScoreEntrys>().Entrynumber.text = "Nr." + (i+1);
                TableContent[i].GetComponent<ScoreEntrys>().HeroName.text = ScoreList.Score.HighScoreList[i].HName;
                TableContent[i].GetComponent<ScoreEntrys>().HeroLvl.text = "Lvl." + ScoreList.Score.HighScoreList[i].HLvl;
                height = TableContent[0].GetComponent<RectTransform>().rect.height;

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
            height = EquipmentContent.GetComponent<RectTransform>().rect.width;
            EquipedSpawn.localPosition = new Vector3(0, 0 - (height / EquipedItem.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
            EquipmentContent.sizeDelta = new Vector2(0,3+ Hero.CurrentHero.Wearing.Count() * height / EquipedItem.GetComponent<AspectRatioFitter>().aspectRatio);
            

            for (int i = 0; i < Hero.CurrentHero.Wearing.Count(); i++)
            {
                float spawnY = (i * height +3) - EquipedSpawn.localPosition.y;
                Vector3 pos = new Vector3(EquipedSpawn.localPosition.x, -spawnY, EquipedSpawn.position.z);
                TableEquipment.Add(Instantiate(EquipedItem, pos, EquipedSpawn.rotation));
                TableEquipment[i].transform.SetParent(EquipmentContent, false);
                TableEquipment[i].GetComponent<ItemEntrys>().ItemLvl.text = "Lvl." + Hero.CurrentHero.Wearing[i].lvl;
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
                TableEquipment[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Wearing[i].worth) / 100);
                TableEquipment[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Wearing[i];
                height = TableEquipment[0].GetComponent<RectTransform>().rect.height;

            }


            //Hero Bag List
            height = BagContent.GetComponent<RectTransform>().rect.width;
            BagSpawn.localPosition = new Vector3(0, 0 - (height / BagItem.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
            BagContent.sizeDelta = new Vector2(0, Hero.CurrentHero.Bag.Count() * height / BagItem.GetComponent<AspectRatioFitter>().aspectRatio);

            for (int i = 0; i < Hero.CurrentHero.Bag.Count(); i++)
            {
                float spawnY = (i * height) - BagSpawn.localPosition.y;
                Vector3 pos = new Vector3(BagSpawn.localPosition.x, -spawnY, BagSpawn.position.z);
                TableBag.Add(Instantiate(BagItem, pos, BagSpawn.rotation));
                TableBag[i].transform.SetParent(BagContent, false);
                TableBag[i].GetComponent<ItemEntrys>().ItemLvl.text = "Lvl." + Hero.CurrentHero.Bag[i].lvl;
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
                TableBag[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Bag[i].worth) / 100);
                TableBag[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Bag[i];
                height = TableBag[0].GetComponent<RectTransform>().rect.height ;

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
            if (!Marketplace.enabled)
            {
                GM.InMenu = false;
            }

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
        height = EquipmentContent.GetComponent<RectTransform>().rect.width;
        EquipedSpawn.localPosition = new Vector3(0, 0 - (height / EquipedItem.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
        EquipmentContent.sizeDelta = new Vector2(0,3+ Hero.CurrentHero.Wearing.Count() * height / EquipedItem.GetComponent<AspectRatioFitter>().aspectRatio);

        for (int i = 0; i < Hero.CurrentHero.Wearing.Count(); i++)
        {
            float spawnY = (i * height+3) - EquipedSpawn.localPosition.y;
            Vector3 pos = new Vector3(EquipedSpawn.localPosition.x, -spawnY, EquipedSpawn.position.z);
            TableEquipment.Add(Instantiate(EquipedItem, pos, EquipedSpawn.rotation));
            TableEquipment[i].transform.SetParent(EquipmentContent, false);
            TableEquipment[i].GetComponent<ItemEntrys>().ItemLvl.text = "Lvl." + Hero.CurrentHero.Wearing[i].lvl;
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
            TableEquipment[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Wearing[i].worth) / 100);
            TableEquipment[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Wearing[i];
            height = TableEquipment[0].GetComponent<RectTransform>().rect.height;
        }

        //Hero Bag List
        height = BagContent.GetComponent<RectTransform>().rect.width;
        BagSpawn.localPosition = new Vector3(0, 0 - (height / BagItem.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
        BagContent.sizeDelta = new Vector2(0,3+ Hero.CurrentHero.Bag.Count() * height / BagItem.GetComponent<AspectRatioFitter>().aspectRatio);

        for (int i = 0; i < Hero.CurrentHero.Bag.Count(); i++)
        {
            float spawnY = (i * height) - BagSpawn.localPosition.y;
            Vector3 pos = new Vector3(BagSpawn.localPosition.x, -spawnY, BagSpawn.position.z);
            TableBag.Add(Instantiate(BagItem, pos, BagSpawn.rotation));
            TableBag[i].transform.SetParent(BagContent, false);
            TableBag[i].GetComponent<ItemEntrys>().ItemLvl.text = "Lvl." + Hero.CurrentHero.Bag[i].lvl;
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
            TableBag[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Bag[i].worth) / 100);
            TableBag[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Bag[i];
            height = TableBag[0].GetComponent<RectTransform>().rect.height;

        }

        HeroStats.text = "Level " + Hero.CurrentHero.Lvl +
    "\nHP: " + Hero.CurrentHero.CurrentHp + "/" + Hero.CurrentHero.Hp +
    "\nStr: " + Hero.CurrentHero.Str +
    "\nDef: " + Hero.CurrentHero.Def +
    "\nDex: " + Hero.CurrentHero.Dex +
    "\nSpe: " + (Math.Truncate(100 * Hero.CurrentHero.Spe) / 100);

    }


    //Marketplace
    public void OpenMarketplace()
    {
        foreach (GameObject Item in BagItems)
        {
            Destroy(Item);
        }

        foreach (GameObject Item in ShopItems)
        {
            Destroy(Item);
        }
        BagItems.Clear();
        ShopItems.Clear();

        if (Marketplace.enabled)
        {
            GM.InMenu = false;
            Marketplace.enabled = false;
            BagItems.Clear();
            ShopItems.Clear();
        }
        else
        {

            GM.InMenu = true;
            Marketplace.enabled = true;

            //BagItemsforShop
            height = BagList.GetComponent<RectTransform>().rect.width;
            ShopBagSpawn.localPosition = new Vector3(0, 0 - (height / MarketBagItem.GetComponent<AspectRatioFitter>().aspectRatio / 2 ), 0);
            BagList.sizeDelta = new Vector2(0, Hero.CurrentHero.Bag.Count() * height / MarketBagItem.GetComponent<AspectRatioFitter>().aspectRatio);

            for (int i = 0; i < Hero.CurrentHero.Bag.Count(); i++)
            {
            float spawnY = (i * height) - ShopBagSpawn.localPosition.y;
            Vector3 pos = new Vector3(ShopBagSpawn.localPosition.x, -spawnY, ShopBagSpawn.position.z);
            BagItems.Add(Instantiate(MarketBagItem, pos, ShopBagSpawn.rotation));
            BagItems[i].transform.SetParent(BagList, false);
            BagItems[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + Hero.CurrentHero.Bag[i].lvl;
            switch (Hero.CurrentHero.Bag[i].Type)
            {
                case Items.ItemType.Weapon:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                    break;
                case Items.ItemType.Armor:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                    break;
                case Items.ItemType.Shoes:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                    break;
                case Items.ItemType.Gloves:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                    break;
                case Items.ItemType.Accessory:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                    break;
            }
            
            BagItems[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Bag[i].worth) / 100);
            Hero.CurrentHero.Bag[i].price = Hero.CurrentHero.Bag[i].lvl * Hero.CurrentHero.Bag[i].rarity * 6;
            BagItems[i].GetComponent<ItemEntrys>().Price.text = "$: " + Hero.CurrentHero.Bag[i].price;
            BagItems[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Bag[i];
            height = BagItems[0].GetComponent<RectTransform>().rect.height;

            }

            //ShopInventory
            height = MarketInventoryList.GetComponent<RectTransform>().rect.width;
            ShopInventorySpawn.localPosition = new Vector3(0, 0 - (height / ShopItem.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
            MarketInventoryList.sizeDelta = new Vector2(0, Hero.CurrentHero.Bag.Count() * height / ShopItem.GetComponent<AspectRatioFitter>().aspectRatio);

            for (int i = 0; i < GM.areasInUseArray[1].GetComponent<Area>().MarketStore.Count(); i++)
            {
                float spawnY = (i * height) - ShopInventorySpawn.localPosition.y;
                Vector3 pos = new Vector3(ShopInventorySpawn.localPosition.x, -spawnY, ShopInventorySpawn.position.z);
                ShopItems.Add(Instantiate(ShopItem, pos, ShopInventorySpawn.rotation));
                ShopItems[i].transform.SetParent(MarketInventoryList, false);
                ShopItems[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl;
                switch (GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].Type)
                {
                    case Items.ItemType.Weapon:
                        ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                        break;
                    case Items.ItemType.Armor:
                        ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                        break;
                    case Items.ItemType.Shoes:
                        ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                        break;
                    case Items.ItemType.Gloves:
                        ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                        break;
                    case Items.ItemType.Accessory:
                        ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                        break;
                }

                ShopItems[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].worth) / 100);
                GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].price = (int) (GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl*(GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl /2)* GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].rarity * 10* GM.areasInUseArray[1].GetComponent<Area>().Tax);
                if (GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl %2 != 0)
                {
                    GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].price = (int)(GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl * ((GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl / 2)+0.5) * GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].rarity * 10 * GM.areasInUseArray[1].GetComponent<Area>().Tax);
                }
                ShopItems[i].GetComponent<ItemEntrys>().Price.text = "$: " + GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].price;
                ShopItems[i].GetComponent<ItemEntrys>().LinkedItem = GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i];
                height = ShopItems[0].GetComponent<RectTransform>().rect.height;


            }

        }
    }

    public void UpdateMarketplace()
    {
        GM.SafeCurrentGame();
        foreach (GameObject Item in BagItems)
        {
            Destroy(Item);
        }
        foreach (GameObject Item in ShopItems)
        {
            Destroy(Item);
        }
        BagItems.Clear();
        ShopItems.Clear();

        //BagItemsforShop
        height = BagList.GetComponent<RectTransform>().rect.width;
        ShopBagSpawn.localPosition = new Vector3(0, 0 - (height / MarketBagItem.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
        BagList.sizeDelta = new Vector2(0, Hero.CurrentHero.Bag.Count() * height / MarketBagItem.GetComponent<AspectRatioFitter>().aspectRatio);

        for (int i = 0; i < Hero.CurrentHero.Bag.Count(); i++)
        {
            float spawnY = (i * height) - ShopBagSpawn.localPosition.y;
            Vector3 pos = new Vector3(ShopBagSpawn.localPosition.x, -spawnY, ShopBagSpawn.position.z);
            BagItems.Add(Instantiate(MarketBagItem, pos, ShopBagSpawn.rotation));
            BagItems[i].transform.SetParent(BagList, false);
            BagItems[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + Hero.CurrentHero.Bag[i].lvl;
            switch (Hero.CurrentHero.Bag[i].Type)
            {
                case Items.ItemType.Weapon:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                    break;
                case Items.ItemType.Armor:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                    break;
                case Items.ItemType.Shoes:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                    break;
                case Items.ItemType.Gloves:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                    break;
                case Items.ItemType.Accessory:
                    BagItems[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                    break;
            }

            BagItems[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Bag[i].worth) / 100);
            Hero.CurrentHero.Bag[i].price = Hero.CurrentHero.Bag[i].lvl * Hero.CurrentHero.Bag[i].rarity * 6;
            BagItems[i].GetComponent<ItemEntrys>().Price.text = "$: " + Hero.CurrentHero.Bag[i].price;
            BagItems[i].GetComponent<ItemEntrys>().LinkedItem = Hero.CurrentHero.Bag[i];
            height = BagItems[0].GetComponent<RectTransform>().rect.height;

        }

        //ShopInventory
        height = MarketInventoryList.GetComponent<RectTransform>().rect.width;
        ShopInventorySpawn.localPosition = new Vector3(0, 0 - (height / ShopItem.GetComponent<AspectRatioFitter>().aspectRatio / 2), 0);
        MarketInventoryList.sizeDelta = new Vector2(0, Hero.CurrentHero.Bag.Count() * height / ShopItem.GetComponent<AspectRatioFitter>().aspectRatio);

        for (int i = 0; i < GM.areasInUseArray[1].GetComponent<Area>().MarketStore.Count(); i++)
        {
            float spawnY = (i * height) - ShopInventorySpawn.localPosition.y;
            Vector3 pos = new Vector3(ShopInventorySpawn.localPosition.x, -spawnY, ShopInventorySpawn.position.z);
            ShopItems.Add(Instantiate(ShopItem, pos, ShopInventorySpawn.rotation));
            ShopItems[i].transform.SetParent(MarketInventoryList, false);
            ShopItems[i].GetComponent<ItemEntrys>().ItemLvl.text = "Level." + GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl;
            switch (GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].Type)
            {
                case Items.ItemType.Weapon:
                    ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Weapon";
                    break;
                case Items.ItemType.Armor:
                    ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Armor";
                    break;
                case Items.ItemType.Shoes:
                    ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Shoes";
                    break;
                case Items.ItemType.Gloves:
                    ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Gloves";
                    break;
                case Items.ItemType.Accessory:
                    ShopItems[i].GetComponent<ItemEntrys>().ItemType.text = "Accessory";
                    break;
            }

            ShopItems[i].GetComponent<ItemEntrys>().ItemStrength.text = "Worth: " + (Math.Truncate(100 * GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].worth) / 100);
            GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].price = (int)(GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl * (GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl / 2) * GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].rarity * 10 * GM.areasInUseArray[1].GetComponent<Area>().Tax);
            if (GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl % 2 != 0)
            {
                GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].price = (int)(GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl * ((GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].lvl / 2) + 0.5) * GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].rarity * 10 * GM.areasInUseArray[1].GetComponent<Area>().Tax);
            }
            ShopItems[i].GetComponent<ItemEntrys>().Price.text = "$: " + GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i].price;
            ShopItems[i].GetComponent<ItemEntrys>().LinkedItem = GM.areasInUseArray[1].GetComponent<Area>().MarketStore[i];
            height = ShopItems[0].GetComponent<RectTransform>().rect.height;


        }


    }

    public void SafeShopping(Items ChosenItem)
    {
        SafetyQuestionShopping.enabled = true;
        ItemOfChoice = ChosenItem;
        
        switch (ChosenItem.Type)
        {
            case Items.ItemType.Weapon:
                EquiToBuy.text ="Weapon, Worth: " + ChosenItem.worth;
                break;
            case Items.ItemType.Armor:
                EquiToBuy.text = "Armor, Worth: " + ChosenItem.worth;
                break;
            case Items.ItemType.Shoes:
                EquiToBuy.text = "Shoes, Worth: " + (Math.Truncate(100 *ChosenItem.worth) / 100);
                break;
            case Items.ItemType.Gloves:
                EquiToBuy.text = "Gloves, Worth: " + ChosenItem.worth;
                break;
            case Items.ItemType.Accessory:
                EquiToBuy.text = "Accessory, Worth: " + ChosenItem.worth;
                break;
        }
        Price.text = "for " + ChosenItem.price + " G?";
        bool b = Hero.CurrentHero.Wearing.Any(i => i.Type == ChosenItem.Type);

        if (b)
        {
            switch (ChosenItem.Type)
            {
                case Items.ItemType.Weapon:
                    EquiWorn.text = "Currently equipped: Weapon, Worth: " + Hero.CurrentHero.Wearing.Find(i => i.Type == ChosenItem.Type).worth;
                    break;
                case Items.ItemType.Armor:
                    EquiWorn.text = "Currently equipped: Armor, Worth: " + Hero.CurrentHero.Wearing.Find(i => i.Type == ChosenItem.Type).worth;
                    break;
                case Items.ItemType.Shoes:
                    EquiWorn.text = "Currently equipped: Shoes, Worth: " + (Math.Truncate(100 * Hero.CurrentHero.Wearing.Find(i => i.Type == ChosenItem.Type).worth) / 100);
                    break;
                case Items.ItemType.Gloves:
                    EquiWorn.text = "Currently equipped: Gloves, Worth: " + Hero.CurrentHero.Wearing.Find(i => i.Type == ChosenItem.Type).worth;
                    break;
                case Items.ItemType.Accessory:
                    EquiWorn.text = "Currently equipped: Accessory, Worth: " + Hero.CurrentHero.Wearing.Find(i => i.Type == ChosenItem.Type).worth;
                    break;
            }
        }
        else
        {
            EquiWorn.text = "";
        }


    }
    public void SafeShoppingNo()
    {
        SafetyQuestionShopping.enabled = false;
    }

    public void SafeShoppingYes()
    {
        SafetyQuestionShopping.enabled = false;

        Hero.CurrentHero.Bag.Add(ItemOfChoice);
        Hero.CurrentHero.Gold -= ItemOfChoice.price;
        GM.areasInUseArray[1].GetComponent<Area>().MarketStore.Remove(ItemOfChoice);
        UpdateMarketplace();
    }

    public IEnumerator ShowShopErrorMessage()
    {

        ShoppingError.enabled = true;
            yield return new WaitForSeconds(1);
        ShoppingError.enabled = false;

    }

}
