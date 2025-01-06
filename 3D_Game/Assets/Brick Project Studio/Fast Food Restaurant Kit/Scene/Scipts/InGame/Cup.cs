using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{

    public float downHeight = 1.662f;
    public float upHeight = 2.257f;

    public GameObject ball;

    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //Udes to attach ball to the cup
        if(ball != null){
            ball.transform.position = new Vector3 (
                transform.position.x,
                ball.transform.position.y,
                transform.position.z
            );
        }
    }

    // Bewegt den Becher nach oben
    public void MoveUp()
    {
        // Beispiel: Becher nach oben bewegen
        transform.position += Vector3.up * 2f;  // Hebt den Becher um 2 Einheiten nach oben
    }

}
