using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Area))]
public class AreaEditor : Editor
{
    Area Activ;
    void OnEnable()
    {
        Activ = (Area)target;
    }

    public override void OnInspectorGUI()
    {
        Activ.AreaType = (Area.ArealTypes)EditorGUILayout.EnumPopup("Area Type", Activ.AreaType);
        switch (Activ.AreaType)
        {
            case Area.ArealTypes.Healing:
                {
                    Activ.Probability = EditorGUILayout.FloatField("Area Probability", Activ.Probability);
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Healing percentage of Hero HP");
                    EditorGUILayout.LabelField("Min Healing:", Activ.MinHeal.ToString());
                    EditorGUILayout.LabelField("Max Healing:", Activ.MaxHeal.ToString());
                    EditorGUILayout.MinMaxSlider(ref Activ.MinHeal, ref Activ.MaxHeal, 0f, 1f);
                    EditorGUILayout.Space();
                    break;
                }
            case Area.ArealTypes.Neutral:
                {
                    Activ.Probability = EditorGUILayout.FloatField("Area Probability", Activ.Probability);
                    EditorGUILayout.Space();
                    Activ.Experience = EditorGUILayout.IntField("Gained Experience by passing", Activ.Experience);
                    EditorGUILayout.Space();
                    Activ.moneyprobability = EditorGUILayout.IntField("Probabillity Money can be gained", Activ.moneyprobability);
                    Activ.moneyamount = EditorGUILayout.IntField("Money Amount", Activ.moneyamount);
                    break;
                }
            case Area.ArealTypes.Monster:
                {
                    Activ.Probability = EditorGUILayout.FloatField("Area Probability", Activ.Probability);
                    EditorGUILayout.Space();
                    GUILayout.Label(new GUIContent("Monster Level", "Level Number added to current HeroLvl to define Monster Level"));
                    Activ.Minlvl = EditorGUILayout.IntField("Minimum", Activ.Minlvl);
                    Activ.Maxlvl = EditorGUILayout.IntField("Maximum", Activ.Maxlvl);
                    EditorGUILayout.Space();
                    Activ.itemprobability = EditorGUILayout.IntSlider(new GUIContent("Item Probability", "Percentage to gain an Item when passing"), Activ.itemprobability, 0, 100);
                    EditorGUILayout.Space();
                    Activ.moneyprobability = EditorGUILayout.IntSlider(new GUIContent("Money Probability", "Percentage to gain Money when passing"), Activ.moneyprobability,0,100);
                    Activ.moneyamount = EditorGUILayout.IntField(new GUIContent("Money Amount", "If '0' Amount will be Set according to MonsterLvl"), Activ.moneyamount);
                    break;
                }
            case Area.ArealTypes.Treasure:
                {
                    Activ.Probability = EditorGUILayout.FloatField("Area Probability", Activ.Probability);
                    EditorGUILayout.Space();
                    Activ.itemprobability = EditorGUILayout.IntSlider(new GUIContent("Item Probability", "Percentage to gain an Item when passing"), Activ.itemprobability, 0, 100);
                    EditorGUILayout.Space();
                    Activ.moneyprobability = EditorGUILayout.IntSlider(new GUIContent("Money Probability", "Percentage to gain Money when passing"), Activ.moneyprobability, 0, 100);
                    Activ.moneyamount = EditorGUILayout.IntField(new GUIContent("Money Amount", "If '0' Amount will be Set according to HeroLvl"), Activ.moneyamount);
                    break;
                }
            case Area.ArealTypes.Market:
                {
                    Activ.Probability = EditorGUILayout.FloatField("Area Probability", Activ.Probability);
                    EditorGUILayout.Space();
                    Activ.minStoreItems = EditorGUILayout.IntField("Minimum StoreItems", Activ.minStoreItems);
                    Activ.maxStoreItems = EditorGUILayout.IntField("Maximum StoreItems", Activ.maxStoreItems);
                    break;
                }
        }
    }

}
