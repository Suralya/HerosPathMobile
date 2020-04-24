using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    public Transform SpawnPoint = null;


    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            ScorePanel.enabled = true;
            content.sizeDelta = new Vector2(0, ScoreList.Score.HighScoreList.Count() * 60);

            for (int i = 0; i < ScoreList.Score.HighScoreList.Count(); i++)
            {
                float spawnY = i * 60;
                Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
                GameObject NewEntry = Instantiate(Entry, pos, SpawnPoint.rotation);
                NewEntry.transform.SetParent(SpawnPoint, false);
                NewEntry.GetComponent<ScoreEntrys>().Entrynumber.text = "Nr." + (i+1);
                NewEntry.GetComponent<ScoreEntrys>().HeroName.text = ScoreList.Score.HighScoreList[i].HName;
                NewEntry.GetComponent<ScoreEntrys>().HeroLvl.text = "Lvl." + ScoreList.Score.HighScoreList[i].HLvl;

            }
        }

    }

}
