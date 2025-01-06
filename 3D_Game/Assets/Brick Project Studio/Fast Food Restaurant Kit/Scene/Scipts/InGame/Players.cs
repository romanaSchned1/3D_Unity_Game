using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public bool canPick = false;

    // Update is called once per frame
    void Update()
    {
        // Überprüfen, ob der Spieler Becher auswählen kann
        if (canPick)
        {
            // Überprüfen, ob die linke Maustaste gedrückt wird
            if (Input.GetMouseButtonDown(0))  // 0 steht für die linke Maustaste
            {
                RaycastHit hit;

                // Raycast in die Vorwärtsrichtung des Spielers senden
                Vector3 forward = transform.TransformDirection(Vector3.forward) * 1000;
                Debug.DrawRay(transform.position, forward, Color.green);  // Zum Debuggen, um den Ray sichtbar zu machen

                // Prüfen, ob das Raycast ein Objekt trifft
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    // Überprüfen, ob das getroffene Objekt ein Cup-Skript hat
                    Cup cup = hit.transform.GetComponent<Cup>();
                    if (cup != null)
                    {
                        // Becher auswählen und nach oben bewegen
                        canPick = false;
                        cup.MoveUp();  // Stelle sicher, dass die Methode 'MoveUp' im Cup-Skript existiert
                    }
                }
            }
        }
    }
}
