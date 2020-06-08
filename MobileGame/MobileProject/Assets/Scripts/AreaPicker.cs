using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPicker
{
    public static AreaPicker PickArea;
    public List<GameObject> Areas;
    public float ProbabilitySum;

    public AreaPicker()
    {
        Areas = new List<GameObject>();
    }

    public void Add(GameObject Area)
    {
        Areas.Add(Area);
        ProbabilitySum += Areas[Areas.Count - 1].GetComponent<Area>().Probability;

        for (var i = Areas.Count - 1; i >= 0; i--)
        {
            Areas[i].GetComponent<Area>().Weight = Areas[i].GetComponent<Area>().Probability / ProbabilitySum;

            
            if (i <= Areas.Count - 2)
            {
                Areas[i].GetComponent<Area>().Weight += Areas[i + 1].GetComponent<Area>().Weight;
            }
            
        }
    }

    public GameObject Pick()
    {
        var picker = Random.Range(0.0f, 1.0f);

        return Areas.FindLast(
            delegate (GameObject current) { return picker <= current.GetComponent<Area>().Weight; }
            );
    }
}
