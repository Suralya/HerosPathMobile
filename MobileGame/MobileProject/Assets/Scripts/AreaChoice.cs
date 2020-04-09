using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChoice : MonoBehaviour
{
    private GameManager GM;

    //Left=0, Mid=1, Right=2
    public int ArrayNumberOfChoice;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
      //  Debug.Log(GM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        if (GM.newStepEnabled == true)
        {
            GM.newStepEnabled = false;
            GM.AddArea(GM.AreasOfChoice[ArrayNumberOfChoice]);
            GM.ClearArrangement();
            GM.MoveAreas();


        }

    }
}
