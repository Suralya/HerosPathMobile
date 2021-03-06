﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntrys : MonoBehaviour
{
    public Text ItemType;
    public Text ItemLvl;
    public Text ItemStrength;

    public Items LinkedItem;
    public Text Price;

    //ifBagentry
    public void SwapGear()
    {
        Hero.CurrentHero.SwapGear(LinkedItem);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().UpdateInventoryTables();
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().Marketplace.enabled)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().UpdateMarketplace();
        }
    }
    //ifEquipEntry
    public void UnequipGear()
    {
        Hero.CurrentHero.Unequip(LinkedItem);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().UpdateInventoryTables();
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().Marketplace.enabled)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().UpdateMarketplace();
        }
    }

    public void BuyItem()
    {
        if (Hero.CurrentHero.Gold - LinkedItem.price >= 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().SafeShopping(LinkedItem);
        }
        else
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().ShowShopErrorMessage());
        }
    }

    public void SellItem()
    {
        Hero.CurrentHero.Bag.Remove(LinkedItem);
        Hero.CurrentHero.Gold += LinkedItem.price;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().GM.areasInUseArray[1].GetComponent<Area>().MarketStore.Add(LinkedItem);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().UpdateMarketplace();
    }

}
