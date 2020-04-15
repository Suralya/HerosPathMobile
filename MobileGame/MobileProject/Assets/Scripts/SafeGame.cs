using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{

    public static Game currentGame;

  //  public Hero HeroCharacter;
    public int[] AreasInUse;
    public int[] AreasforChoice;

    public Game()
    {
      //  HeroCharacter = null;
        AreasInUse = new int[3];
        AreasforChoice = new int[3];
    }


}
