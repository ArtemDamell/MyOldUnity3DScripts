using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private int numberOfHits = 0;

    [SerializeField] private int hitsToKill;
    [SerializeField] private int points;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            numberOfHits++;

            if(numberOfHits == hitsToKill)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.SendMessage("AddPoints", points);
                Destroy(this.gameObject);
            }
        }
    }
}
