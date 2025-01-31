using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Integration Scenen
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
   public Button startButton;

    // Start is called before the first frame update
    /*public void Start()
    {
        //neue Scene - Spiel starten 
        //SceneManager.LoadScene(1);

        // Event-Listener hinzufügen
        startButton.onClick.AddListener(OnStartButtonClicked);
    }*/

    public void Start()
    {
        /*if (startButton == null)
        {
            startButton = FindObjectOfType<Button>();  // Sucht nach einem Button in der Szene
        }*/

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            //Debug.LogError("Kein Button gefunden!");
        }
    }

    public void OnStartButtonClicked()
    {
        // Szene wechseln
        SceneManager.LoadScene("Scene_Start"); // Ersetze "NextSceneName" mit dem tatsächlichen Szenennamen
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
