using UnityEngine;

public class CupController : MonoBehaviour
{
    public Transform[] positions;  // Positions-Array für die Becher
    private int currentBallPosition;

    void Start()
    {
        // Zufällige Position für den Ball auswählen (zwischen 0 und 2 für 3 Becher)
        currentBallPosition = Random.Range(0, positions.Length);
        // Ball unter dem gewählten Becher platzieren
        GameObject.Find("Ball").transform.position = positions[currentBallPosition].position + Vector3.up * 0.5f;
    }

    public void SwapCups()
    {
        // Beispiel für zufälliges Vertauschen der Becher (einfacher Swap)
        int swapIndex = Random.Range(0, positions.Length);
        Vector3 tempPosition = positions[0].position;
        positions[0].position = positions[swapIndex].position;
        positions[swapIndex].position = tempPosition;

        // Hier kannst du auch Animationen einbauen, um die Becher zu bewegen
    }

    public bool CheckBallUnderCup(int cupIndex)
    {
        // Überprüfe, ob der Ball unter dem angeklickten Becher ist
        return cupIndex == currentBallPosition;
    }
}
