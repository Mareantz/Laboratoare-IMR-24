using System.Collections;
using UnityEngine;
using TMPro; 

public class Trigger : MonoBehaviour
{
    public GameObject golfBatHandleBlack;
    public GameObject golf;
    public GameObject targetObject;
    public ParticleSystem confettiParticles; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreMessageText; 

    private int score = 6;
    private bool canDecreaseScore = true;
    private float delay = 3.0f; //score dissapear delay
    private bool hasEnteredTargetArea = false;
    private Vector3 initialGolfPosition;
    void Start()
    {
        if (golfBatHandleBlack == null || golf == null || targetObject == null || confettiParticles == null || scoreText == null || scoreMessageText == null)
        {
            Debug.LogError("Please assign all necessary GameObjects in the inspector.");
        }
        initialGolfPosition = golf.transform.position;

        //hide final score at the start
        scoreMessageText.gameObject.SetActive(false);
        UpdateScoreUI();
    }

    void Update()
    {
        // Continuously check if the golf bat is colliding with the golf ball
        if (golfBatHandleBlack != null && golf != null && canDecreaseScore)
        {
            if (golfBatHandleBlack.GetComponent<Collider>().bounds.Intersects(golf.GetComponent<Collider>().bounds) && score > 1)
            {
               

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

            //Confetti animation
            if (confettiParticles != null)
            {
                confettiParticles.Play();
                Debug.Log("Confetti animation triggered!");
            }

            //Show scoring message
            ShowScoreMessage();
            ResetGolfBall();

        }
    }

    void OnTriggerExit(Collider other)
    {
        //Reset the flag when the golf ball leaves the target area
        if (other.gameObject == golf)
        {
            hasEnteredTargetArea = false;
        }
    }
    public void DecreaseScore()
    {
        if (canDecreaseScore && score > 1)
        {
            score--;
            Debug.Log("Score decreased. Current Score: " + score);
            UpdateScoreUI();
        }
    }
        void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    
    void ShowScoreMessage()
    {
        if (scoreMessageText != null)
        {
            scoreMessageText.text = "<align=center><color=#FF0000>YOU SCORED!!!!</color>\n<color=#FFFF00>Final Score: " + score + "</color></align>";
            scoreMessageText.gameObject.SetActive(true);

            StartCoroutine(HideScoreMessageAfterDelay(3.0f));
        }
    }

    IEnumerator HideScoreMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scoreMessageText.gameObject.SetActive(false);
    }


    void ResetGolfBall()
    {
        golf.transform.position = initialGolfPosition;
        Rigidbody golfRb = golf.GetComponent<Rigidbody>();
        if (golfRb != null)
        {
            golfRb.velocity = Vector3.zero; 
            golfRb.angularVelocity = Vector3.zero; 
        }
        score = 6;
        UpdateScoreUI(); 
    }
}
