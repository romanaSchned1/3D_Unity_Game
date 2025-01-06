using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // WICHTIG: Import für UI-Elemente

public class Chart : MonoBehaviour
{
    public Button startButton;

    // Start is called before the first frame update
    public void Start()
    {
        // Event-Listener hinzufügen
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void OnStartButtonClicked()
    {
        // Szene wechseln
        SceneManager.LoadScene("Scene_Start");  // Ersetze "Scene_Start" mit dem tatsächlichen Szenennamen
    }

    // Beispiel für eine Funktion zum Löschen von Objekten
    public void DeleteObjects()
    {
        // Beispiel: Alle Objekte mit einem bestimmten Tag "Deletable" löschen
        GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("delete");

        foreach (GameObject obj in objectsToDelete)
        {
            Destroy(obj);  // Lösche das Objekt aus der Szene
        }

        Debug.Log(objectsToDelete.Length + " objects deleted.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
