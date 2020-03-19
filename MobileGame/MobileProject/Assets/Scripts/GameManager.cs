using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject StartingArea;
    public List<GameObject> Areas = new List<GameObject>();
    private Queue<GameObject> areasInUse=new Queue<GameObject>();
    private GameObject[] areasInUseArray = new GameObject[3];

    private Vector3 position1 = new Vector3(0, 0, -1);
    private Vector3 position2 = new Vector3(0, 0, 0);
    private Vector3 position3 = new Vector3(0, 0, 1);

    public bool newStepEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        areasInUse.Enqueue(StartingArea);
        areasInUse.Enqueue(Areas[Random.Range(0, Areas.Count - 1)]);
        areasInUseArray = areasInUse.ToArray();
        Instantiate(areasInUseArray[0],position2,Quaternion.identity);
        Instantiate(areasInUseArray[1], position3, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
