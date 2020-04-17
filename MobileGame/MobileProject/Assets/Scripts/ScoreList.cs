using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ScoreList 
{
    public static ScoreList Score;
    public List<Entry> HighScoreList;

    public ScoreList()
    {
        HighScoreList = new List<Entry>();
    }

    public void ShowList()
    {
        foreach (Entry e in HighScoreList)
        {
            Debug.Log((HighScoreList.IndexOf(e)+1)+".   Lvl:"+e.HLvl + "    MonsterCount:"+e.MSlayn+"    AreasPassed:"+e.APassed);          
        }
    }

}

[System.Serializable]
public class Entry
{

    public string HName;
    public int HLvl, MSlayn, APassed;

    public Entry(string HeroName, int HeroLvl, int MonsterSlayn, int AreasPassed)
    {
        HName = HeroName;
        HLvl = HeroLvl;
        MSlayn = MonsterSlayn;
        APassed = AreasPassed;
    }


}
