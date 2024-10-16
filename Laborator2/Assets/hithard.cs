using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHit : MonoBehaviour
{
    public GameObject golfBall;
    public float hitForce = 200f;
    public float verticalForce = 100f;

    private Rigidbody golfBallRb;
    private Trigger triggerScript;

    void Start()
    {
        if (golfBall != null)
        {
            golfBallRb = golfBall.GetComponent<Rigidbody>();
        }

        triggerScript = FindObjectOfType<Trigger>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject == golfBall)
        {
            Vector3 hitDirection = (golfBall.transform.position - transform.position).normalized;

            golfBallRb.AddForce(hitDirection * hitForce + Vector3.up * verticalForce);
            Debug.Log("Golf ball hit with force! Direction: " + hitDirection);

            if (triggerScript != null)
            {
                triggerScript.DecreaseScore();
            }
        }
    }
}
