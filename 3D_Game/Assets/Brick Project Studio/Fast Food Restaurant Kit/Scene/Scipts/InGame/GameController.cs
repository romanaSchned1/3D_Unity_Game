using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public CupController becherController; // Referenz zu Becher-Controller
    public GameObject uiCanvas; // Referenz zu UI, um Buttons zu steuern
    public Camera mainCamera;  // Referenz zur Hauptkamera
    public GameObject currentBall; // Referenz zum aktuellen Ball-Objekt
    public GameObject newBallPrefab; // Referenz für das neue Ball-Prefab

    public int shuffleCount = 10; // Anzahl der Vertauschungen zu Beginn (Erhöht für mehr Vertauschungen)
    public float shuffleDelay = 0.5f; // Verzögerung zwischen den Vertauschungen

    void Start()
    {
        if (becherController == null)
        {
            Debug.LogError("CupController is not assigned!");
            return;
        }

        StartGame();
    }

    public void StartGame()
    {
        Debug.Log("Spiel startet!");

        // Alle Becher gleichzeitig anheben und dann den Tausch starten
        StartCoroutine(RaiseAllCupsAndShuffle());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Mausklick (0 für links)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Wenn der Ray ein Objekt trifft
            {
                Debug.Log("Raycast hit: " + hit.transform.name);

                int cupIndex = GetCupIndex(hit.transform);
                if (cupIndex != -1)
                {
                    StartCoroutine(HandleCupClick(cupIndex, hit.transform));
                }
                else
                {
                    Debug.Log("Kein Becher angeklickt.");
                }
            }
        }
    }

    int GetCupIndex(Transform cupTransform)
    {
        for (int i = 0; i < becherController.positions.Length; i++)
        {
            if (becherController.positions[i] == cupTransform)
            {
                return i;
            }
        }
        return -1;
    }

    IEnumerator HandleCupClick(int cupIndex, Transform cupTransform)
    {
        Vector3 originalPosition = cupTransform.position;
        Vector3 raisedPosition = originalPosition + new Vector3(0, 0.5f, 0); // Becher 0.5 Einheiten nach oben bewegen

        float elapsedTime = 0f;
        float moveDuration = 0.5f;

        while (elapsedTime < moveDuration)
        {
            cupTransform.position = Vector3.Lerp(originalPosition, raisedPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cupTransform.position = raisedPosition;

        bool isBallUnderCup = becherController.CheckBallUnderCup(cupIndex);
        Debug.Log(isBallUnderCup ? "Richtig! Der Ball ist unter diesem Becher!" : "Falsch! Der Ball ist nicht unter diesem Becher.");

        if (!isBallUnderCup)
        {
            elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                cupTransform.position = Vector3.Lerp(raisedPosition, originalPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            cupTransform.position = originalPosition;
        }
    }

    // Diese Methode wird alle Becher gleichzeitig anheben, dann nach unten bewegen und danach den Tausch starten
    IEnumerator RaiseAllCupsAndShuffle()
    {
        Vector3[] originalPositions = new Vector3[becherController.positions.Length];
        Vector3[] raisedPositions = new Vector3[becherController.positions.Length];

        // Speichere die Originalpositionen und berechne die angehobenen Positionen
        for (int i = 0; i < becherController.positions.Length; i++)
        {
            originalPositions[i] = becherController.positions[i].position;
            raisedPositions[i] = originalPositions[i] + new Vector3(0, 0.5f, 0); // 0.5 Einheiten nach oben
        }

        // Alle Becher gleichzeitig nach oben bewegen
        float elapsedTime = 0f;
        float moveDuration = 0.5f;
        while (elapsedTime < moveDuration)
        {
            for (int i = 0; i < becherController.positions.Length; i++)
            {
                becherController.positions[i].position = Vector3.Lerp(originalPositions[i], raisedPositions[i], elapsedTime / moveDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Setze die endgültige Position auf die angehobene Position
        for (int i = 0; i < becherController.positions.Length; i++)
        {
            becherController.positions[i].position = raisedPositions[i];
        }

        // Kurz warten, bevor die Becher wieder nach unten gehen
        yield return new WaitForSeconds(0.5f);

        // Becher wieder nach unten bewegen
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            for (int i = 0; i < becherController.positions.Length; i++)
            {
                becherController.positions[i].position = Vector3.Lerp(raisedPositions[i], originalPositions[i], elapsedTime / moveDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Setze die endgültige Position auf die Originalposition
        for (int i = 0; i < becherController.positions.Length; i++)
        {
            becherController.positions[i].position = originalPositions[i];
        }

        // Nun können die Becher vertauscht werden
        yield return StartCoroutine(ShuffleCups(shuffleCount, shuffleDelay));
    }

    IEnumerator ShuffleCups(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            Debug.Log($"Vertausche Becher {i + 1}/{count}...");
            // Mehrfaches Vertauschen der Becher mit Animation
            yield return StartCoroutine(becherController.SwapCupsWithAnimation());
            yield return new WaitForSeconds(delay); // Warte, bis der Tausch sichtbar wird
        }
        Debug.Log("Becher wurden " + count + " mal vertauscht.");
    }

    public void ReplaceBall()
    {
        if (newBallPrefab == null)
        {
            Debug.LogError("Kein Ball-Prefab zugewiesen!");
            return;
        }

        if (currentBall != null)
        {
            Debug.Log("Zerstöre aktuellen Ball: " + currentBall.name);
            Destroy(currentBall);
        }

        currentBall = Instantiate(newBallPrefab, becherController.GetCurrentBallPosition(), Quaternion.identity);
        Debug.Log("Neuer Ball erstellt an Position: " + currentBall.transform.position);

        becherController.SetBallUnderRandomCup(currentBall);
    }

    public void SelectNewBall(GameObject newBall)
    {
        if (becherController != null)
        {
            becherController.SetBallUnderRandomCup(newBall);
        }
    }
}
