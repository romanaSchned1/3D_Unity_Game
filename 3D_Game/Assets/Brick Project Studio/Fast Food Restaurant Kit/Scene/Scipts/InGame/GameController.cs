using UnityEngine;

public class GameController : MonoBehaviour
{
    public CupController becherController;
    public GameObject uiCanvas; // Referenz zu UI, um Buttons zu steuern

    void Start()
    {
        // Starte das Spiel automatisch, wenn die Szene geladen wird
        StartGame();
    }

    public void StartGame()
    {
        // Starte das Spiel, indem wir die Becher vertauschen
        Debug.Log("Spiel startet!");

        // Becher vertauschen und Ball zufällig platzieren
        becherController.SwapCups();
        
        // Optional: Deaktiviere das UI, falls Buttons eingeblendet werden
        uiCanvas.SetActive(false);
    }

    void Update()
    {
        // Überprüfe Mausklicks und prüfe, ob ein Becher angeklickt wurde
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                int cupIndex = GetCupIndex(hit.transform);
                if (cupIndex != -1)
                {
                    bool isBall = becherController.CheckBallUnderCup(cupIndex);
                    Debug.Log(isBall ? "Richtig!" : "Falsch!");
                }
            }
        }
    }

    int GetCupIndex(Transform cupTransform)
    {
        // Überprüfe, welchen Becher der Spieler angeklickt hat
        for (int i = 0; i < becherController.positions.Length; i++)
        {
            if (becherController.positions[i] == cupTransform)
            {
                return i;
            }
        }
        return -1;
    }
}
