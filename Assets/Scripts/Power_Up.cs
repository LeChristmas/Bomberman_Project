using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Power_Ups { Default, Max_Bombs, Bomb_Range, Speed_Increase, Wallpass, Detonator, Bombpass, Flamepass, Mystery}

public class Power_Up : MonoBehaviour
{
    public Power_Ups power_up_type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (power_up_type == Power_Ups.Max_Bombs)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().max_bombs++;
            }

            if (power_up_type == Power_Ups.Bomb_Range)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bomb_strength++;
            }

            if (power_up_type == Power_Ups.Speed_Increase)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().speed_increase = true;
            }

            if (power_up_type == Power_Ups.Wallpass)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_wallpass = true;
            }

            if (power_up_type == Power_Ups.Detonator)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().detonator = true;
            }

            if (power_up_type == Power_Ups.Bombpass)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_bombpass = true;
            }

            if (power_up_type == Power_Ups.Flamepass)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_flamepass = true;
            }

            if (power_up_type == Power_Ups.Mystery)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().mystery = true;
            }
            Destroy(gameObject);
        }
    }
}
