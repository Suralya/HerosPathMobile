using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntrys : MonoBehaviour
{
   public Text ItemType;
   public Text ItemLvl;
   public Text ItemStrength;

   public Items LinkedItem;

    //ifBagentry
    public void SwapGear()
    {
        Hero.CurrentHero.SwapGear(LinkedItem);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().UpdateInventoryTables();
    }
    //ifEquipEntry
    public void UnequipGear()
    {
        Hero.CurrentHero.Unequip(LinkedItem);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().UpdateInventoryTables();
    }

}
