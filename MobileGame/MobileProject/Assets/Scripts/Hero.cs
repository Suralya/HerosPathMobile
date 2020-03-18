using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public string Name = "HERO";
    public int Lvl = 1;

    private int maxExp = 100;
    public int CurrentExp = 0;

    public int Hp, Str, Def, Dex, Spe = 1;
    public int StageCounter, MonsterCounter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
