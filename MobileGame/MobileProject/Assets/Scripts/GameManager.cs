using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool InMenu = false;

    public GameObject StartingArea;
    public List<GameObject> Areas = new List<GameObject>();

    //Areas Currently seen in the Game
    private Queue<GameObject> areasInUse = new Queue<GameObject>();
    public GameObject[] areasInUseArray = new GameObject[3];

    public Position Pos1, Pos2, Pos3, PosEnd;

    //Variables to enable AreaChoice system
    public GameObject[] AreasOfChoice = new GameObject[3];
    private GameObject[] areasShown = new GameObject[3];
    public GameObject[] ChoicePositions = new GameObject[3];

    //Variable to time Area Spawning/ Area Choosing
    public bool newStepEnabled = false;
  //  private bool firstAreas = true;

    //GameUI
    public UIManager GameUI;

    private void Awake()
    {

        Picker.PickItem = new Picker();
        Picker.PickItem.Add(new Items(Items.ItemType.Weapon));
        Picker.PickItem.Add(new Items(Items.ItemType.Armor));
        Picker.PickItem.Add(new Items(Items.ItemType.Gloves));
        Picker.PickItem.Add(new Items(Items.ItemType.Shoes));
        Picker.PickItem.Add(new Items(Items.ItemType.Accessory));

        AreaPicker.PickArea = new AreaPicker();
        foreach (GameObject A in Areas)
        {
            AreaPicker.PickArea.Add(A);
        }


        Hero.CurrentHero = new Hero();
        Game.currentGame = new Game();
        ScoreList.Score = new ScoreList();
        
        for (int i = 1; i <= Areas.Count; i++)
        {
            Areas[i - 1].GetComponent<Area>().ID = i;
        }

        Pos1 = new Position();
        Pos2 = new Position();
        Pos3 = new Position();
        PosEnd = new Position();

        //ShuffleArealist(Areas);

        Pos1.Pos = new Vector3(0, 0, -1);
        Pos1.Following = PosEnd;
        Pos2.Pos = new Vector3(0, 0, 0);
        Pos2.Following = Pos1;
        Pos3.Pos = new Vector3(0, 0, 1);
        Pos3.Following = Pos2;
        PosEnd.Pos = new Vector3(0, 0, -2);
        PosEnd.Following = null;     

    }

    void Start()
    {

        if (File.Exists(Application.persistentDataPath + "/savedGames.HeroPath"))
        {
            SaveLoad.Load();
            ScoreList.Score = Game.currentGame.HighScore;

            if (Game.currentGame.HeroCharacter.Lvl > 0)
            {
                LoadGame();
            }
            else
            {
                StartGame();
            }
        }
        else
        {
            StartGame();
        }

    }

  

    void Update()
    {

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
        Hero.CurrentHero.StageCounter++;
        //  Debug.Log(Temp.GetComponent<Area>().AreaType);
        Temp.GetComponent<Area>().ArealAction(Hero.CurrentHero);
        GameUI.UpdateEnemyUI(Temp.GetComponent<Area>());


    }

    public void ArrangeAreaChoice()
    {
        GameObject Temp;
        for (int i = 0; i < 3; i++)
        {
            AreasOfChoice[i] = AreaPicker.PickArea.Pick();
            Temp = Instantiate(AreasOfChoice[i], ChoicePositions[i].transform.position, Quaternion.identity);
            areasShown[i] = Temp;
            Temp.transform.localScale = ChoicePositions[i].transform.localScale;
        }
        SafeCurrentGame();
        newStepEnabled = true;

    }
    public void ClearArrangement()
    {
        foreach (GameObject Showcase in areasShown)
        {
            Destroy(Showcase);
        }

    }

    public IEnumerator MoveToPosition(GameObject objectToMove, Vector3 end)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, Hero.CurrentHero.Spe * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

    }
    public IEnumerator MoveToLastPosition(GameObject objectToMove, Vector3 end)
    {

        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, Hero.CurrentHero.Spe * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //  Debug.Log("Object Will be Destroyed");

        while (Hero.CurrentHero.isFighting == true)
        {
            yield return new WaitForEndOfFrame();
        }
        ArrangeAreaChoice();
        Destroy(objectToMove);
    }

    public void ShuffleArealist(List<GameObject> List)
    {
        for (int i = List.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            GameObject temp = List[i];
            List[i] = List[j];
            List[j] = temp;
        }

    }

    public void SafeCurrentGame()
    {
        Game.currentGame.HeroCharacter = Hero.CurrentHero;
        Game.currentGame.HighScore = ScoreList.Score;
        for (int i = 0; i <= 2; i++)
        {
           // Debug.Log("SafeInstance " + i);
            if (i!=2)
            {
                Game.currentGame.AreasInUse[i] = areasInUseArray[i].GetComponent<Area>().ID;
            }

            Game.currentGame.AreasforChoice[i]= AreasOfChoice[i].GetComponent<Area>().ID;
        }
        SaveLoad.Save();

    }

    public void StartGame()
    {
        Hero.CurrentHero.HeroSetup();
        Hero.CurrentHero.StageCounter++;

        GameObject Temp = Instantiate(StartingArea, Pos2.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos2;
        areasInUse.Enqueue(Temp);
        Temp = Areas[Random.Range(0, Areas.Count - 1)];
        while (Temp.GetComponent<Area>().AreaType != Area.ArealTypes.Neutral)
        {
            Temp = AreaPicker.PickArea.Pick();
        }
        Temp = Instantiate(Temp, Pos3.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos3;
        areasInUse.Enqueue(Temp);
        areasInUseArray = areasInUse.ToArray();
        MoveAreas();
        Invoke("ArrangeAreaChoice", 1);

    }
    public void LoadGame()
    {
        Hero.CurrentHero = Game.currentGame.HeroCharacter;
        GameObject Temp;
        if (Areas.Find(x => x.GetComponent<Area>().ID == Game.currentGame.AreasInUse[0]) == null)
        {
            Temp = Instantiate(StartingArea, Pos2.Pos, Quaternion.identity);
            Temp.GetComponent<Area>().CurrentPos = Pos2;
            areasInUse.Enqueue(Temp);
        }
        else
        {
            Temp = Instantiate(Areas.Find(x => x.GetComponent<Area>().ID == Game.currentGame.AreasInUse[0]), Pos2.Pos, Quaternion.identity);
            Temp.GetComponent<Area>().CurrentPos = Pos2;
            areasInUse.Enqueue(Temp);
        }

        Temp = Instantiate(Areas.Find(x => x.GetComponent<Area>().ID == Game.currentGame.AreasInUse[1]), Pos3.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos3;
        areasInUse.Enqueue(Temp);
        areasInUseArray = areasInUse.ToArray();
        MoveAreas();
        Invoke("LoadAreaChoice", 1);
    }
    public void LoadAreaChoice()
    {
        GameObject Temp;
        for (int i = 0; i <=2 ; i++)
        {
            AreasOfChoice[i] = Areas.Find(x => x.GetComponent<Area>().ID == Game.currentGame.AreasforChoice[i]);
            Temp = Instantiate(AreasOfChoice[i], ChoicePositions[i].transform.position, Quaternion.identity);
            areasShown[i] = Temp;
            Temp.transform.localScale = ChoicePositions[i].transform.localScale;
        }
        newStepEnabled = true;

    }
}

