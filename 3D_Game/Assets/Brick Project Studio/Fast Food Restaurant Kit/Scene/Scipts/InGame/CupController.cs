using System.Collections;
using UnityEngine;

public class CupController : MonoBehaviour
{
    public Transform[] positions;  // Positions-Array für die Becher
    private int currentBallPosition;
    public float swapDuration = 1.0f;  // Dauer des Becherwechsels

    //public Transform[] positions;

    void Start()
    {
        if (positions == null || positions.Length == 0)
        {
            Debug.LogError("Positions array is not assigned or is empty!");
            return; // Verhindert weitere Fehler
        }

        // Beispiel für die Verwendung von positions
        // Zufällige Position für den Ball auswählen
        int currentBallPosition = Random.Range(0, positions.Length);
        GameObject.Find("Ball").transform.position = positions[currentBallPosition].position + Vector3.up * 0.5f;
    }


    public void SwapCups()
    {
        // Beispiel für zufälliges Vertauschen der Becher (einfacher Swap)
        int swapIndex = Random.Range(0, positions.Length);
        
        // Starte die Animation des Tauschs
        StartCoroutine(SwapAnimation(positions[0], positions[swapIndex]));
        
        // Aktualisiere die Positionen im Array
        Transform temp = positions[0];
        positions[0] = positions[swapIndex];
        positions[swapIndex] = temp;
    }

    private IEnumerator SwapAnimation(Transform cup1, Transform cup2)
{
    Vector3 startPos1 = cup1.position;
    Vector3 startPos2 = cup2.position;

    float midHeight = 1.0f; // Höhe, um die die Becher angehoben werden
    Vector3 peakPos1 = startPos1 + Vector3.up * midHeight;
    Vector3 peakPos2 = startPos2 + Vector3.up * midHeight;
    //Vector3 peakPos3 = startPos2 + Vector3.up * midHeight;

    float elapsedTime = 0;

    // Erster Teil: Becher anheben
    while (elapsedTime < swapDuration / 2)
    {
        cup1.position = Vector3.Lerp(startPos1, peakPos1, (elapsedTime / (swapDuration / 2)));
        cup2.position = Vector3.Lerp(startPos2, peakPos2, (elapsedTime / (swapDuration / 2)));
        //cup3.position = Vector3.Lerp(startPos3, peakPos3, (elapsedTime / (swapDuration / 2)));

        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Zurücksetzen der Zeit
    elapsedTime = 0;

    // Zweiter Teil: Becher tauschen und absenken
    while (elapsedTime < swapDuration / 2)
    {
        cup1.position = Vector3.Lerp(peakPos1, startPos2, (elapsedTime / (swapDuration / 2)));
        cup2.position = Vector3.Lerp(peakPos2, startPos1, (elapsedTime / (swapDuration / 2)));
        //cup3.position = Vector3.Lerp(peakPos3, startPos1, (elapsedTime / (swapDuration / 2)));

        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Stelle sicher, dass die Becher ihre endgültigen Positionen erreichen
    cup1.position = startPos2;
    cup2.position = startPos1;
}


    public bool CheckBallUnderCup(int cupIndex)
    {
        // Überprüfe, ob der Ball unter dem angeklickten Becher ist
        return cupIndex == currentBallPosition;
    }
}
