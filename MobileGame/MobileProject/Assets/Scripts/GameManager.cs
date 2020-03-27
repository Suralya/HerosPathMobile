using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject HeroObject;
    public Hero Hero;
    public GameObject StartingArea;
    public List<GameObject> Areas = new List<GameObject>();
    private Queue<GameObject> areasInUse = new Queue<GameObject>();
    private GameObject[] areasInUseArray = new GameObject[3];

    public Position Pos1, Pos2, Pos3, Pos4;



    public bool newStepEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        Pos1 = new Position();
        Pos2 = new Position();
        Pos3 = new Position();
        Pos4 = new Position();

        Hero = HeroObject.GetComponent<Hero>();
        Hero.StageCounter ++;

        Pos1.Pos = new Vector3(0, 0, -1);
        Pos1.Following = Pos4;
        Pos2.Pos = new Vector3(0, 0, 0);
        Pos2.Following = Pos1;
        Pos3.Pos = new Vector3(0, 0, 1);
        Pos3.Following = Pos2;
        Pos4.Pos = new Vector3(0, 0, -2);
        Pos4.Following = null;




        GameObject Temp= Instantiate(StartingArea, Pos2.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos2;
        areasInUse.Enqueue(Temp);
        Temp = Instantiate(Areas[Random.Range(0, Areas.Count - 1)], Pos3.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos3;
        areasInUse.Enqueue(Temp);
        areasInUseArray = areasInUse.ToArray();
        MoveAreas();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (areasInUseArray[0].transform.position == Pos1.Pos)
            {
                AddArea();
                MoveAreas();
            }
        }

    }


    public void MoveAreas()
    {
        foreach (GameObject Area in areasInUseArray)
        {
            if (Area != null)
            {

                if (Area.GetComponent<Area>().CurrentPos.Following == Pos4)
                {
                    areasInUse.Dequeue();
                    areasInUseArray = areasInUse.ToArray();
                    Debug.Log("DestroyingFlagEntered");
                    StartCoroutine(MoveToLastPosition(Area, Pos4.Pos));
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
    public void AddArea()
    {

        GameObject Temp = Instantiate(Areas[Random.Range(0, Areas.Count - 1)], Pos3.Pos, Quaternion.identity);
        Temp.GetComponent<Area>().CurrentPos = Pos3;
        areasInUse.Enqueue(Temp);
        areasInUseArray = areasInUse.ToArray();

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
            Debug.Log("Object Will be Destroyed");
            Destroy(objectToMove);
    }




}

