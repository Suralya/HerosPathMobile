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
    public Canvas DeathScreen;


    // Start is called before the first frame update
    void Start()
    {
        DeathScreen.enabled = false;
        UpdateHeroUI();

    }

    // Update is called once per frame
    void Update()
    {
     UpdateHeroUI();
        if (GM.HeroStats.CurrentHp <= 0)
        {
            DeathScreen.enabled = true;
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
        HPText.text = "HP: " + GM.HeroStats.CurrentHp + " / " + GM.HeroStats.Hp;

        if (HPIndicator.fillAmount < (float)GM.HeroStats.CurrentHp / GM.HeroStats.Hp)
        {
          //  GM.Hero.isFighting = true;

            HPIndicator.fillAmount += IndicationSpeed * Time.deltaTime;
            if (HPIndicator.fillAmount > (float)GM.HeroStats.CurrentHp / GM.HeroStats.Hp)
            {
                HPIndicator.fillAmount = (float)GM.HeroStats.CurrentHp / GM.HeroStats.Hp;
            }



        }
        else if (HPIndicator.fillAmount > (float)GM.HeroStats.CurrentHp / GM.HeroStats.Hp)
        {
          //  GM.Hero.isFighting = true;

            HPIndicator.fillAmount -= IndicationSpeed * Time.deltaTime;
            if (HPIndicator.fillAmount < (float)GM.HeroStats.CurrentHp / GM.HeroStats.Hp)
            {
                HPIndicator.fillAmount = (float)GM.HeroStats.CurrentHp / GM.HeroStats.Hp;
            }

        }
        else if (HPIndicator.fillAmount == (float)GM.HeroStats.CurrentHp / GM.HeroStats.Hp)
        {
            GM.HeroStats.isFighting = false;
        }

        if (ExpIndicator.fillAmount < (float)GM.HeroStats.CurrentExp / GM.HeroStats.maxExp)
        {
            ExpIndicator.fillAmount += IndicationSpeed * Time.deltaTime;
            if (ExpIndicator.fillAmount > (float)GM.HeroStats.CurrentExp / GM.HeroStats.maxExp)
            {
                ExpIndicator.fillAmount = (float)GM.HeroStats.CurrentExp / GM.HeroStats.maxExp;
            }

        }
        else
        {
            ExpIndicator.fillAmount = (float)GM.HeroStats.CurrentExp / GM.HeroStats.maxExp;
        }


        //Stats public Text LevelText,StrText,DefText,DexText,SpeText;
        LevelText.text = "Level: " + GM.HeroStats.Lvl;
        StrText.text = "Strength: " + GM.HeroStats.Str;
        DefText.text = "Defense: " + GM.HeroStats.Def;
        DexText.text = "Dexterity: " + GM.HeroStats.Dex;
        SpeText.text = "Speed: " + GM.HeroStats.Spe;

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
