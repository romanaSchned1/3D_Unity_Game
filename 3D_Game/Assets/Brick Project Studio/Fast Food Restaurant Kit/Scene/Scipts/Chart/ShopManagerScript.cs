using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{

    public int[,] shopItems = new int[7,7];
    public float coins;
    public Text CoinsTXT;

    // Start is called before the first frame update
    void Start()
    {
        CoinsTXT.text = "Coins: " + coins.ToString();

        //ID's
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        shopItems[1,5] = 5;
        shopItems[1,6] = 6;

        //Price
        shopItems[2,1] = 11;
        shopItems[2,2] = 25;
        shopItems[2,3] = 32;
        shopItems[2,4] = 44;
        shopItems[2,5] = 54;
        shopItems[2,6] = 60;

        //Quantity
        shopItems[3,1] = 0;
        shopItems[3,2] = 0;
        shopItems[3,3] = 0;
        shopItems[3,4] = 0;
        shopItems[3,5] = 0;
        shopItems[3,6] = 0;

    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;

        if (coins >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID]){

            coins -= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3,ButtonRef.GetComponent<ButtonInfo>().ItemID]++;

            CoinsTXT.text = "Coins: " + coins.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3,ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

        }

        
    }


}
