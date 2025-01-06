using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[7, 7];  // Array für Shop-Artikel (ID, Preis, Menge, etc.)
    public float coins;  // Coins des Spielers
    public Text CoinsTXT;  // Text-Objekt für die Anzeige der Coins

    // Start is called before the first frame update
    void Start()
    {
        CoinsTXT.text = "Coins: " + coins.ToString();

        // Artikel-IDs
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;
        shopItems[1, 6] = 6;

        // Preise der Artikel
        shopItems[2, 1] = 11;
        shopItems[2, 2] = 25;
        shopItems[2, 3] = 32;
        shopItems[2, 4] = 44;
        shopItems[2, 5] = 54;
        shopItems[2, 6] = 60;

        // Initiale Mengen
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;
        shopItems[3, 6] = 0;

        // Lade den Kaufstatus der Artikel
        LoadPurchasedItems();
    }

    // Diese Funktion wird aufgerufen, wenn der "Kaufen"-Button gedrückt wird
    public void Buy()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;
        int itemID = ButtonRef.GetComponent<ButtonInfo>().ItemID;

        // Überprüfen, ob genug Coins vorhanden sind und der Artikel noch nicht gekauft wurde
        if (coins >= shopItems[2, itemID] && shopItems[3, itemID] == 0)
        {
            // Coins abziehen
            coins -= shopItems[2, itemID];
            // Setze die Menge auf 1, da der Artikel nur einmal gekauft werden kann
            shopItems[3, itemID] = 1;

            // Speichern des gekauften Artikels in PlayerPrefs
            PlayerPrefs.SetInt("ItemPurchased_" + itemID, 1);

            // Aktualisiere die Anzeige der Coins
            CoinsTXT.text = "Coins: " + coins.ToString();
            // Aktualisiere die Anzeige der Artikelmenge
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = "1";

            // Deaktiviere den Button, da der Artikel bereits gekauft wurde
            ButtonRef.GetComponent<Button>().interactable = false;
        }
    }

    // Lädt den Kaufstatus der Artikel aus PlayerPrefs
    void LoadPurchasedItems()
    {
        for (int i = 1; i <= 6; i++)  // Hier gehen wir durch alle Artikel
        {
            int purchased = PlayerPrefs.GetInt("ItemPurchased_" + i, 0);  // 0 bedeutet, dass der Artikel nicht gekauft wurde
            shopItems[3, i] = purchased;  // Wenn gekauft, setzen wir die Menge auf 1

            // Falls der Artikel gekauft wurde, deaktiviere den Button
            if (purchased == 1)
            {
                GameObject button = GameObject.Find("Button_" + i);  // Hier musst du sicherstellen, dass die Buttons richtig benannt sind
                if (button != null)
                {
                    button.GetComponent<Button>().interactable = false;  // Button deaktivieren
                    button.GetComponent<ButtonInfo>().QuantityTxt.text = "1";  // Setze den Text
                }
            }
        }
    }

    // Funktion zum Zurücksetzen aller gekauften Artikel auf "nicht gekauft"
    public void ResetAllPurchasedItems()
    {
        // Hardcoded: Alle Artikel auf "nicht gekauft" setzen
        PlayerPrefs.SetInt("ItemPurchased_1", 0);
        PlayerPrefs.SetInt("ItemPurchased_2", 0);
        PlayerPrefs.SetInt("ItemPurchased_3", 0);
        PlayerPrefs.SetInt("ItemPurchased_4", 0);
        PlayerPrefs.SetInt("ItemPurchased_5", 0);
        PlayerPrefs.SetInt("ItemPurchased_6", 0);

        // Alle Artikel im Array auf "nicht gekauft" setzen (Menge = 0)
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;
        shopItems[3, 6] = 0;

        // Finde alle Buttons und stelle sie wieder auf interaktiv
        for (int i = 1; i <= 6; i++)
        {
            GameObject button = GameObject.Find("Button_" + i);
            if (button != null)
            {
                button.GetComponent<Button>().interactable = true;  // Button wieder interaktiv machen
                button.GetComponent<ButtonInfo>().QuantityTxt.text = "0";  // Setze die Menge auf 0
                // Optional: Setze die Hintergrundfarbe zurück
                Image buttonImage = button.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = Color.white;  // Setze die Hintergrundfarbe auf Weiß zurück
                }
            }
        }

        // Speichere die Änderungen
        PlayerPrefs.Save();
    }

    // Optional: Wenn du das Spiel verlässt, speicherst du alle PlayerPrefs
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
