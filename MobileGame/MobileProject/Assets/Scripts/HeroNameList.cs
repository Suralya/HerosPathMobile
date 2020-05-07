using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroNameList<HeroNames>
{
    public HeroNames[] NameList;
}

[System.Serializable]
public class HeroNames
{
    public string Name="";
}