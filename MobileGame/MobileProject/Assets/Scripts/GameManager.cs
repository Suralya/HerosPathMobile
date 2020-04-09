using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject HeroObject;
    public Hero Hero;
    public GameObject StartingArea;
    public List<GameObject> Areas = new List<GameObject>();

    //Areas Currently seen in the Game
    private Queue<GameObject> areasInUse = new Queue<GameObject>();
    private GameObject[] areasInUseArray = new GameObject[3];

    public Position Pos1, Pos2, Pos3, PosEnd;

    //Variables to enable AreaChoice system
    public GameObject[] AreasOfChoice = new GameObject[3];
    private GameObject[] areasShown = new GameObject[3];
    public GameObject[] ChoicePositions = new GameObject[3];

    //Variable to time Area Spawning/ Area Choosing
    public bool newStepEnabled = false;
    private bool firstAreas = true;

    //UI HeroStatus
    public Text HPText;
    public Image HPIndicator;
    public Image ExpIndicator;
    public Text LevelText,StrText,DefText,DexText,SpeText;

    public Text EnmyLevelText, EnmyStrText, EnmyDefText, EnmyDexText, EnmySpeText;

    //GameUI
    public Canvas DeathScreen;


    void Start()
    {
        DeathScreen.enabled = false;
        Pos1 = new Position();
        Pos2 = new Position();
        Pos3 = new Position();
        PosEnd = new Position();

        ShuffleArealist(Areas);

        Hero = HeroObject.GetComponent<Hero>();
        Hero.StageCounter ++;

        Pos1.Pos = new Vector3(0, 0, -1);
        Pos1.Following = PosEnd;
        Pos2.Pos = new Vector3(0, 0, 0);
        Pos2.Following = Pos1;
        Pos3.Pos = new Vector3(0, 0, 1);
        Pos3.Following = Pos2;
        PosEnd.Pos = new Vector3(0, 0, -2);
        PosEnd.Following = null;

        UpdateHeroUI();

        GameObject Temp= Instantiate(StartingArea, Pos2.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos2;
        areasInUse.Enqueue(Temp);
        Temp = Areas[Random.Range(0, Areas.Count - 1)];
        while (Temp.GetComponent<Area>().AreaType != Area.ArealTypes.Neutral)
        {
            Temp = Areas[Random.Range(0, Areas.Count - 1)];
        }
        Temp = Instantiate(Temp, Pos3.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos3;
        areasInUse.Enqueue(Temp);
        areasInUseArray = areasInUse.ToArray();
        MoveAreas();
        Invoke("ArrangeAreaChoice", 1);


    }

  

    void Update()
    {
        UpdateHeroUI();
        if (Hero.CurrentHp <= 0)
        {
            DeathScreen.enabled = true;
        } 
    }


    public void MoveAreas()
    {
        foreach (GameObject Area in areasInUseArray)
        {
            if (Area != null)
            {

                if (Area.GetComponent<Area>().CurrentPos.Following == PosEnd)
                {
                    areasInUse.Dequeue();
                    areasInUseArray = areasInUse.ToArray();
                   // Debug.Log("DestroyingFlagEntered");
                    StartCoroutine(MoveToLastPosition(Area, PosEnd.Pos));
                }
                else
                {
                    StartCoroutine(MoveToPosition(Area, Area.GetComponent<Area>().CurrentPos.Following.Pos));
                    Area.GetComponent<Area>().CurrentPos = Area.GetComponent<Area>().CurrentPos.Following;
                }
            }

        }
    }

    //Random Area will be replaced with Chosen Area later
    public void AddArea(GameObject Area)
    {

        GameObject Temp = Instantiate(Area, Pos3.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos3;
        areasInUse.Enqueue(Temp);
        areasInUseArray = areasInUse.ToArray();
        Hero.StageCounter++;
        //  Debug.Log(Temp.GetComponent<Area>().AreaType);
        Temp.GetComponent<Area>().ArealAction(Hero);
        UpdateEnemyUI(Temp.GetComponent<Area>());


    }

    public void ArrangeAreaChoice()
    {
        GameObject Temp;
        for (int i = 0; i < 3; i++)
        {
            AreasOfChoice[i] = Areas[Random.Range(0, Areas.Count - 1)];
            Temp = Instantiate(AreasOfChoice[i], ChoicePositions[i].transform.position, Quaternion.identity);
            areasShown[i] = Temp;
            Temp.transform.localScale = ChoicePositions[i].transform.localScale;
        }
        newStepEnabled = true;
    }

    public void ClearArrangement()
    {
        foreach (GameObject Showcase in areasShown)
        {
            Destroy(Showcase);
        }

    }

    public void UpdateHeroUI()
    {
        //Bars
        HPText.text = "HP: " + Hero.CurrentHp + " / " + Hero.Hp;
        HPIndicator.fillAmount = (float) Hero.CurrentHp / Hero.Hp;
        ExpIndicator.fillAmount = (float)Hero.CurrentExp / Hero.maxExp;

        //Stats public Text LevelText,StrText,DefText,DexText,SpeText;
        LevelText.text = "Level: " + Hero.Lvl;
        StrText.text = "Strength: " + Hero.Str;
        DefText.text = "Defense: " + Hero.Def;
        DexText.text = "Dexterity: " + Hero.Dex;
        SpeText.text = "Speed: " + Hero.Spe;

    }

    public void UpdateEnemyUI(Area Enemy)
    {
        switch (Enemy.AreaType)
        {
            case Area.ArealTypes.Healing:
                EnmyLevelText.text = "Healing Areal";
                EnmyStrText.text = "Healing: "+ Enemy.Heal ;
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


    public IEnumerator MoveToPosition(GameObject objectToMove, Vector3 end)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, Hero.Spe * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

    }

    public IEnumerator MoveToLastPosition(GameObject objectToMove, Vector3 end)
    {

        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, Hero.Spe * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //  Debug.Log("Object Will be Destroyed");

        ArrangeAreaChoice();
        Destroy(objectToMove);
    }

    public static void ShuffleArealist(List<GameObject> List)
    {
        for (int i = List.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            GameObject temp = List[i];
            List[i] = List[j];
            List[j] = temp;
        }

    }



}

