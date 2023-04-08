using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector2 ballInitialForce;
    private AudioSource audioSource;

    [SerializeField] private float force_x = 100.0f;
    [SerializeField] private float force_y = 300.0f;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private AudioClip hitSound;
    	
	void Start ()
    {
        ballInitialForce = new Vector2(force_x, force_y);
        ballIsActive = false;
        ballPosition = transform.position;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!ballIsActive)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(ballInitialForce);
                ballIsActive = !ballIsActive;
            }
        }

        if (!ballIsActive && playerObject != null)
        {
            ballPosition.x = playerObject.transform.position.x;
            transform.position = ballPosition;
        }

        if (ballIsActive && transform.position.y < -6)
        {
            ballIsActive = !ballIsActive;
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = -4.38f;
            transform.position = ballPosition;

            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerObject.SendMessage("TakeLife");
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ballIsActive)
            audioSource.PlayOneShot(hitSound);
    }
}
