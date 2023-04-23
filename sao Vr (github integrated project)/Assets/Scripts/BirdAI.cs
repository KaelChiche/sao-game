using System.Collections;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    public float turnSpeed = 10f;
    public float moveSpeed = 30f;

    public float range = 1300f;

    void Start()
    {
        StartCoroutine(RandomTurnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * moveSpeed * Time.deltaTime;
    
        float distance = Vector3.Distance(transform.position, Vector3.zero);
        if (distance >= range)
        {
            // Object is outside range
            Debug.Log("Bird is outside range!");
            transform.Rotate(180, 0, 0);
        }
    }

    IEnumerator RandomTurnCoroutine()
    {
        while (true)
        {
            // Choose a random direction to turn (left or right)
            int direction = Random.Range(0, 2) * 2 - 1; // either -1 or 1

            // Choose a random amount of time to turn for
            float turnTime = Random.Range(1.0f, 10.0f);

            // Turn for the chosen amount of time
            float startTime = Time.time;
            while (Time.time - startTime < turnTime)
            {
                transform.Rotate(Vector3.forward, turnSpeed * direction * Time.deltaTime);
                yield return null;

            }
        }
    }
}
