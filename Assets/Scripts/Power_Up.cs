﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Power_Ups { Default, Max_Bombs, Bomb_Range, Speed_Increase, Wallpass, Detonator, Bombpass, Flamepass, Mystery}

public class Power_Up : MonoBehaviour
{
    [Header("- Dropdown Of All Powerup Types -")]
    public Power_Ups power_up_type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Increases Amount of Deployable Bombs By One
            if (power_up_type == Power_Ups.Max_Bombs)
            {
                Data.game_data.max_bombs++;
            }

            // Increases The Range Of Bomb Blasts By One Square
            if (power_up_type == Power_Ups.Bomb_Range)
            {
                Data.game_data.bomb_strength++;
            }

            // Activates The Player Speed Increase
            if (power_up_type == Power_Ups.Speed_Increase)
            {
                Data.game_data.speed_increase = true;
            }

            // Allows The Player To Pass Through Destrucible Walls
            if (power_up_type == Power_Ups.Wallpass)
            {
                Data.game_data.player_wallpass = true;
            }

            // Allows The Player To Remotely Detonate Bombs
            if (power_up_type == Power_Ups.Detonator)
            {
                Data.game_data.detonator = true;
            }

            // Allows The Player To Pass Through Bombs
            if (power_up_type == Power_Ups.Bombpass)
            {
                Data.game_data.player_bombpass = true;
            }

            // Makes Player Immune to Explosions From Bomb
            if (power_up_type == Power_Ups.Flamepass)
            {
                Data.game_data.player_flamepass = true;
            }

            // Grants tempory Immunity To Explosions And Enemies
            if (power_up_type == Power_Ups.Mystery)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().Mystery_Powerup();
            }
            Destroy(gameObject);
        }
    }
}
