using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	public GameObject golfBatHandleBlack;
	public GameObject golf;
	public GameObject targetObject; // The GameObject you want to detect collisions with

	private int score = 6;
	private bool canDecreaseScore = true;
	private float delay = 3.0f; // 3 second delay between score decreases
	private bool hasEnteredTargetArea = false;

	void Start()
	{
		// Make sure all required objects are assigned
		if (golfBatHandleBlack == null || golf == null || targetObject == null)
		{
			Debug.LogError("Please assign all necessary GameObjects in the inspector.");
		}
	}

	void Update()
	{
		// Continuously check if the golf bat is colliding with the golf ball
		if (golfBatHandleBlack != null && golf != null && canDecreaseScore)
		{
			if (golfBatHandleBlack.GetComponent<Collider>().bounds.Intersects(golf.GetComponent<Collider>().bounds) && score > 1)
			{
				score--;
				Debug.Log("Score decreased. Current Score: " + score);
				StartCoroutine(ScoreDecreaseDelay());
			}
		}
	}

	IEnumerator ScoreDecreaseDelay()
	{
		canDecreaseScore = false;
		yield return new WaitForSeconds(delay);
		canDecreaseScore = true;
	}

	void OnTriggerEnter(Collider other)
	{
		// Check if the golf ball hits the target object
		if (other.gameObject == golf && other.bounds.Intersects(targetObject.GetComponent<Collider>().bounds) && !hasEnteredTargetArea)
		{
			hasEnteredTargetArea = true;
			Debug.Log("Golf entered the target area! Current Score: " + score);
			// You can add any additional actions here, such as triggering an event or updating UI
		}
	}

	void OnTriggerExit(Collider other)
	{
		// Reset the flag when the golf ball leaves the target area
		if (other.gameObject == golf)
		{
			hasEnteredTargetArea = false;
		}
	}
}
