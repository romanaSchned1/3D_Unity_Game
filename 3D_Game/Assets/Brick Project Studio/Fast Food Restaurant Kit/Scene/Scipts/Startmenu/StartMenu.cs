using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Integration Scenen
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button startButton;

    // Start is called before the first frame update
    public void Start()
    {
        //neue Scene - Spiel starten 
        //SceneManager.LoadScene(1);

        // Event-Listener hinzufügen
        startButton.onClick.AddListener(OnStartButtonClickedCart);
    }

    public void OnStartButtonClickedLevel()
    {
        // Szene wechseln
        SceneManager.LoadScene("Scene_Level"); 
    }

    public void OnStartButtonClickedCart()
    {
        // Szene wechseln
        SceneManager.LoadScene("Scene_Chart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
