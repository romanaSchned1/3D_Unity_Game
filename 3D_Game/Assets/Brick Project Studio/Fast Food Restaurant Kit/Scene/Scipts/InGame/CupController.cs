using System.Collections;
using UnityEngine;

public class CupController : MonoBehaviour
{
    public Transform[] positions;  // Positions-Array für die Becher
    private int currentBallPosition;
    public float swapDuration = 1.0f;  // Dauer des Becherwechsels
    private Transform ballTransform;  // Referenz auf das Ball-Transform
    private bool isAnimating = false; // Verhindert gleichzeitige Animationen

    void Start()
    {
        if (positions == null || positions.Length != 3)
        {
            Debug.LogError("Positions array is not assigned or does not contain exactly 3 positions!");
            return; // Verhindert weitere Fehler
        }

        // Ball-Referenz finden
        ballTransform = GameObject.Find("Ball").transform;

        // Zufällige Position für den Ball auswählen
        currentBallPosition = Random.Range(0, positions.Length);

        // Ball unter den ausgewählten Becher setzen und als Kind hinzufügen
        ballTransform.position = positions[currentBallPosition].position;
        ballTransform.SetParent(positions[currentBallPosition], true);

        // Collider zu jedem Becher hinzufügen
        foreach (Transform cup in positions)
        {
            if (cup.GetComponent<Collider>() == null)
            {
                cup.gameObject.AddComponent<BoxCollider>();
            }
        }
    }

public IEnumerator SwapCupsWithAnimation()
{
    // Zufällige Indizes für den Swap auswählen
    int firstIndex = Random.Range(0, positions.Length);
    int secondIndex = Random.Range(0, positions.Length);

    // Sicherstellen, dass die beiden Indizes unterschiedlich sind
    while (secondIndex == firstIndex)
    {
        secondIndex = Random.Range(0, positions.Length);
    }

    // Starte die Animation des Tauschs
    yield return SwapAnimation(positions[firstIndex], positions[secondIndex]);

    // Aktualisiere die Positionen im Array
    Transform temp = positions[firstIndex];
    positions[firstIndex] = positions[secondIndex];
    positions[secondIndex] = temp;

    // Falls der Ball unter einem der getauschten Becher ist, aktualisiere seine Position
    if (currentBallPosition == firstIndex)
    {
        currentBallPosition = secondIndex;
    }
    else if (currentBallPosition == secondIndex)
    {
        currentBallPosition = firstIndex;
    }
}


    public void SwapCupsMultiple(int swapCount)
    {
        if (isAnimating) return;  // Verhindere parallele Animationen

        StartCoroutine(SwapCupsRoutine(swapCount));
    }

    private IEnumerator SwapCupsRoutine(int swapCount)
    {
        for (int i = 0; i < swapCount; i++)
        {
            // Zufällige Indizes für den Swap auswählen
            int firstIndex = Random.Range(0, positions.Length);
            int secondIndex = Random.Range(0, positions.Length);

            // Sicherstellen, dass die beiden Indizes unterschiedlich sind
            while (secondIndex == firstIndex)
            {
                secondIndex = Random.Range(0, positions.Length);
            }

            // Starte die Animation des Tauschs
            yield return SwapAnimation(positions[firstIndex], positions[secondIndex]);

            // Aktualisiere die Positionen im Array
            Transform temp = positions[firstIndex];
            positions[firstIndex] = positions[secondIndex];
            positions[secondIndex] = temp;

            // Falls der Ball unter einem der getauschten Becher ist, aktualisiere seine Position
            if (currentBallPosition == firstIndex)
            {
                currentBallPosition = secondIndex;
            }
            else if (currentBallPosition == secondIndex)
            {
                currentBallPosition = firstIndex;
            }
        }

        Debug.Log("Becher wurden " + swapCount + " mal vertauscht.");
        isAnimating = false;
    }

    public void SwapCups()
    {
        SwapCupsMultiple(1);  // Falls du nur einen Swap möchtest
    }

    private IEnumerator SwapAnimation(Transform cup1, Transform cup2)
    {
        isAnimating = true;

        // Dauer des Tausches für jede Phase
        float swapDurationPhase = swapDuration;  // Zeit für den horizontalen Tausch

        // Initiale Positionen der Becher
        Vector3 startPos1 = cup1.position;
        Vector3 startPos2 = cup2.position;

        // Phase 1: Becher tauschen (horizontale Bewegung)
        float elapsedTime = 0;
        while (elapsedTime < swapDurationPhase)
        {
            cup1.position = Vector3.Lerp(startPos1, startPos2, elapsedTime / swapDurationPhase);
            cup2.position = Vector3.Lerp(startPos2, startPos1, elapsedTime / swapDurationPhase);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Stelle sicher, dass die Becher ihre endgültigen Positionen erreichen
        cup1.position = startPos2;
        cup2.position = startPos1;

        isAnimating = false;
    }

    public bool CheckBallUnderCup(int cupIndex)
    {
        // Überprüfe, ob der Ball unter dem angeklickten Becher ist
        return cupIndex == currentBallPosition;
    }

    public Vector3 GetCurrentBallPosition()
    {
        // Gibt die aktuelle Position des Balls zurück
        return positions[currentBallPosition].position;
    }

    public void SetBallUnderRandomCup(GameObject ball)
    {
        // Entferne das aktuelle Ball-Objekt, falls vorhanden und nicht gleich dem neuen Ball
        if (ballTransform != null && ballTransform.gameObject != ball)
        {
            Destroy(ballTransform.gameObject);  // Zerstöre das alte Ball-Objekt
            Debug.Log("Vorheriger Ball entfernt.");
        }

        // Wähle eine zufällige Position für den neuen Ball
        int randomIndex = Random.Range(0, positions.Length);
        currentBallPosition = randomIndex;

        // Setze den neuen Ball an die Position und mache ihn zum Kind des Bechers
        ball.transform.position = positions[randomIndex].position;
        ball.transform.SetParent(positions[randomIndex], true);

        // Aktualisiere die Referenz auf das aktuelle Ball-Transform
        ballTransform = ball.transform;

        Debug.Log("Neuer Ball unter Becher " + randomIndex + " gesetzt.");
    }
}
