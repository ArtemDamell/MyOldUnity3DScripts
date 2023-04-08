using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float boundary;

    private Vector3 playerPosition;
    private int playerLives;
    private int playerPoints;

    public AudioClip pointSound;
    public AudioClip lifeSound;

    private void Start()
    {
        playerPosition = gameObject.transform.position;

        playerLives = 3;
        playerPoints = 0;
    }


    private void Update()
    {
        playerPosition.x += Input.GetAxis("Horizontal") * playerSpeed;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        transform.position = playerPosition;

        if (playerPosition.x < -boundary)
            transform.position = new Vector3(-boundary, playerPosition.y, playerPosition.z);

        if (playerPosition.x > boundary)
            transform.position = new Vector3(boundary, playerPosition.y, playerPosition.z);

        WinLose();
    }

    /// <summary>
    /// Adds points to the player's score and plays a sound effect.
    /// </summary>
    private void AddPoints(int points)
    {
        playerPoints += points;
        gameObject.GetComponent<AudioSource>().PlayOneShot(pointSound);
    }

    /// <summary>
    /// Displays the player's lives and score on the GUI.
    /// </summary>
    private void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Live's: " + playerLives + "  Score: " + playerPoints);
    }

    /// <summary>
    /// Decreases the player's lives by one and plays a sound effect. 
    /// </summary>
    private void TakeLife()
    {
        playerLives--;
        gameObject.GetComponent<AudioSource>().PlayOneShot(lifeSound);
    }

    /// <summary>
    /// This method checks if the player has lost or won the game. If the player has lost, the scene is reset to Level_1. 
    /// If the player has won, the scene is either reset to Level_2 or the application is closed.
    /// </summary>
    private void WinLose()
    {
        if (playerLives == 0)
            SceneManager.LoadScene("Level_1");

        if ((GameObject.FindGameObjectsWithTag("Block")).Length == 0)
        {
            if (SceneManager.GetActiveScene().name == "Level_1")
                SceneManager.LoadScene("Level_2");
            else
                Application.Quit();
        }
    }
}