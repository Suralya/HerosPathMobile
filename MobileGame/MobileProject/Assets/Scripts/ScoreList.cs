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

    public void AddEntry(Entry E)
    {
        HighScoreList.Add(E);
        if (HighScoreList.Count > 1)
        {
            for (int i = HighScoreList.Count - 1; i> 0; i--)
            {
                if (HighScoreList[i].HLvl > HighScoreList[i - 1].HLvl)
                {
                    Entry Temp = HighScoreList[i - 1];
                    HighScoreList[i - 1] = HighScoreList[i];
                    HighScoreList[i] = Temp;
                }
                else if (HighScoreList[i].HLvl == HighScoreList[i - 1].HLvl)
                {
                    if (HighScoreList[i].MSlayn / HighScoreList[i].APassed > HighScoreList[i - 1].MSlayn / HighScoreList[i - 1].APassed)
                    {
                        Entry Temp = HighScoreList[i - 1];
                        HighScoreList[i - 1] = HighScoreList[i];
                        HighScoreList[i] = Temp;
                    }
                }

            }
        }

        if (HighScoreList.Count > 10)
        {
            HighScoreList.RemoveAt(10);
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
