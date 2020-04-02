using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public string Name = "HERO";
    public int Lvl = 1;

    private int maxExp = 100;
    public int CurrentExp = 0;

    public int Hp, CurrentHp, Str, Def, Dex = 0;
    public int StageCounter, MonsterCounter = 0;
    public float Spe= 0f;

    private bool ShowMessage=true;
    

    // Start is called before the first frame update
    void Start()
    {
        Lvl = 1;
        Hp =  Random.Range(15, 25);
        CurrentHp = Hp;
        Str = Random.Range(3, 9);
        Def = Random.Range(3, 9);
        Dex = Random.Range(3, 9);
        Spe = Random.Range(1.0f, 1.8f);

        Debug.Log("The Adventure Starts now - This are your Starting Stats:");
        Debug.Log("HP: " +Hp);
        Debug.Log("Strength: " +Str);
        Debug.Log("Defense: " + Def);
        Debug.Log("Dexterity: " + Dex);
        Debug.Log("Speed: " + Spe);


    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentExp >= maxExp)
        {
            LvlUp();
            Debug.Log("You Had a LvlUp, your new Lvl is "+Lvl+"!!");
            Debug.Log("Your new Stats are:");
            Debug.Log("HP: " + Hp);
            Debug.Log("Strength: " + Str);
            Debug.Log("Defense: " + Def);
            Debug.Log("Dexterity: " + Dex);
            Debug.Log("Speed: " + Spe);
            Debug.Log("You passed " + StageCounter + " Stages for this Lvl");
        }

        if (Hp <= 0 && ShowMessage==true)
        {
            Debug.Log("Hero is Dead");
            ShowMessage = false;
        }
        
    }

    public void LvlUp()
    {
        Lvl++;
        CurrentExp = CurrentExp-maxExp;
        maxExp += (int)(Mathf.Pow(Lvl, 1/2) * 100);
        Hp += Random.Range(0, 6);
        CurrentHp = Hp;
        Str += Random.Range(0, 6);
        Def += Random.Range(0, 6);
        Dex += Random.Range(0, 6);
        Spe += Random.Range(0f, 0.5f);

    }

}
