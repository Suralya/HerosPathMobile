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


    // Start is called before the first frame update
    void Start()
    {
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
            DeathScreen.enabled = true;
        }

    }

    public void RestartGame()
    {
        ScoreList.Score.AddEntry(new Entry(Hero.CurrentHero.Name, Hero.CurrentHero.Lvl, Hero.CurrentHero.MonsterCounter, Hero.CurrentHero.StageCounter));
        Hero.CurrentHero.Lvl=0;
        GM.SafeCurrentGame();
        ScoreList.Score.ShowList();
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

        HeroMony.text = Hero.CurrentHero.Gold.ToString();

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

}
