using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Pickup : MonoBehaviour
{
    [Header("- How Many Points This Is Worth -")]
    public int points;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Data.game_data.score += points;
            Destroy(gameObject);
        }
    }
}
