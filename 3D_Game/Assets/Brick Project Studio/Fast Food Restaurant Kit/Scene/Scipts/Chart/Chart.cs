using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Import für UI-Elemente

public class Chart : MonoBehaviour
{
    public Button startButton;  // Der Start-Button für das Spiel
    public Button[] objectButtons;  // Buttons zum Auswählen der Objekte
    public GameObject ballPrefab;  // Prefab für das Ball-Objekt
    private GameObject currentBall;  // Referenz zum aktuellen Ball-Objekt
    private List<string> purchasedBalls = new List<string>();  // Liste der gekauften Ball-Objekte
    public float moveSpeed = 5f;

    // Start wird aufgerufen, bevor das erste Frame gestartet wird
    public void Start()
    {
        // Event-Listener für den Start-Button
        //startButton.onClick.AddListener(OnStartButtonClicked);

        // Event-Listener für die Objekt-Auswahl-Buttons
        foreach (Button btn in objectButtons)
        {
            btn.onClick.AddListener(() => OnObjectSelected(btn));  // Parameter für die Auswahl
        }
    }

    // Wenn der Start-Button geklickt wird, wechsle die Szene
    public void OnStartButtonClicked()
    {
        // Wechsle zur Startszene des Spiels (Ändere den Szenennamen nach Bedarf)
        SceneManager.LoadScene("Scene_Start");  
    }

    // Wenn ein Objekt-Button geklickt wird, wähle das entsprechende Objekt als Ball
    public void OnObjectSelected(Button selectedButton)
    {
        // Debug-Ausgabe, welches Objekt ausgewählt wurde
        Debug.Log("Objekt ausgewählt: " + selectedButton.name);

        // Überprüfen, ob der Ball bereits gekauft wurde
        if (purchasedBalls.Contains(selectedButton.name))
        {
            // Wenn es schon gekauft wurde, dann einfach den Ball auswählen
            Debug.Log(selectedButton.name + " wurde bereits gekauft und ist nun ausgewählt.");

            // Setze den Ball in der Szene
            ReplaceBallInScene(selectedButton.name);
        }
        else
        {
            // Falls der Ball noch nicht gekauft wurde, kaufen und speichern
            Debug.Log(selectedButton.name + " wurde gekauft!");

            // Füge das gekaufte Objekt zur Liste der gekauften Objekte hinzu
            purchasedBalls.Add(selectedButton.name);

            // Füge dem Objekt ein Label oder eine Markierung hinzu, dass es gekauft wurde
            selectedButton.GetComponentInChildren<Text>().text = selectedButton.name + " (Gekauft)";

            // Optional: Jetzt als Ball auswählen und ersetzen
            ReplaceBallInScene(selectedButton.name);
        }
    }

    // Ersetze den alten Ball im Level mit dem neuen Ball, der ausgewählt wurde
    void ReplaceBallInScene(string ballName)
    {
        // Zerstöre den alten Ball, falls einer existiert
        if (currentBall != null)
        {
            Destroy(currentBall);  // Lösche das alte Ball-Objekt
        }

        // Suche das neue Ball-Objekt in der Szene
        GameObject selectedBall = GameObject.Find(ballName);

        if (selectedBall != null)
        {
            // Setze das ausgewählte Objekt als Ball
            currentBall = selectedBall;

            // Optional: Füge dem Ball-Objekt ein Rigidbody hinzu, um Physik zu ermöglichen
            if (currentBall.GetComponent<Rigidbody>() == null)
            {
                currentBall.AddComponent<Rigidbody>();  // Füge Rigidbody hinzu, falls noch nicht vorhanden
            }

            // Gib eine Nachricht in der Konsole aus, dass der Ball erfolgreich ersetzt wurde
            Debug.Log("Der Ball wurde durch " + ballName + " ersetzt.");
        }
        else
        {
            // Falls das Objekt nicht gefunden wurde, zeige eine Fehlermeldung
            Debug.LogError("Das ausgewählte Ball-Objekt konnte nicht gefunden werden.");
        }
    }

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        // Hier könnten Updates für andere Spielmechaniken sein
    }

    internal void MoveCart()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
