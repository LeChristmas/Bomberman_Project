using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bonus_Type { none, bonus_target, goddess_mask, cola_bottle, famicom, nakamoto_san, dezeniman_san }

public class Bonuses : MonoBehaviour
{
    [Header("- Dropdown Of All Bonuses -")]
    public Bonus_Type bonus_type;

    private int level_number;

    [Header("- Whether The Bonuses Ar Obtainible -")]
    public bool started;
    private bool complete;


    [Header("- Outside Path Used For Goddess Mask -")]
    public Outside_Wall[] outside_path;

    [Header("- Amount Of Destrucible Walls -")]
    public int d_walls;

    public Level_Exit level_exit;
    public Wall_Spawner wall_spawner;

    // Bomb Realted variables
    private int bombs_chained;
    private int exit_bombed;

    // Selects The Bonus For The Stage Dependant On What Data Says It Is Meant To Be
    public void Select_Bonus (Level_Exit exit, Wall_Spawner w_spawner)
    {
        level_exit = GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>();
        wall_spawner = w_spawner;

        level_number = Data.game_data.level_number;
        bonus_type = Data.game_data.bonus_types[level_number];

        d_walls = wall_spawner.cubes_spawning;
    }

    // Called When A Bomb Is Chained/ For Famicom Bonus
    public void Chain_Bomb ()
    {
        if (level_exit.number_of_enemies == 0)
        {
            bombs_chained++;
        }
    }

    // Called When The Exit Is Bombed
    public void Bomb_Exit ()
    {
        if (level_exit.number_of_enemies == level_exit.total_number_of_enemies
            && d_walls == 0)
        {
            exit_bombed++;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        // Bonus Target - Reveal the exit and pass over it without killing a single enemy
        if (bonus_type == Bonus_Type.bonus_target && !complete && started)
        {
            if (level_exit.number_of_enemies == level_exit.total_number_of_enemies && level_exit.triggered)
            {
                wall_spawner.Spawn_Bonus_Item(10000, 0);
                complete = true;
            }
        }

        // Goddess Mask - After killing every single enemy, walk all the way around the outer circle of the stage
        if (bonus_type == Bonus_Type.goddess_mask && !complete && started)
        {
            int passed = 0;

            foreach (Outside_Wall edge in outside_path)
            {
                if (edge.triggered == true)
                {
                    passed++;
                }
            }

            if (passed >= outside_path.Length && level_exit.number_of_enemies == 0)
            {
                wall_spawner.Spawn_Bonus_Item(20000, 1);
                complete = true;
            }
        }

        // Cola Bottle - Before killing all the enemies, reveal and walk over the exit, and continue to press the movement
        // in that same direction for a short period of time
        if (bonus_type == Bonus_Type.cola_bottle && !complete && started)
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().bottle_bonus_complete)
            {
                wall_spawner.Spawn_Bonus_Item(30000, 2);
                complete = true;
            }
        }

        // Famicom - After destroying every enemy, detonate over 248 bombs with chain reactions
        if (bonus_type == Bonus_Type.famicom && !complete && started)
        {
            if(bombs_chained > 248)
            {
                wall_spawner.Spawn_Bonus_Item(500000, 3);
                complete = true;
            }
        }

        // Nakamoto-san - Destroy all enemies without breaking a single wall, a nearly impossible task
        if (bonus_type == Bonus_Type.nakamoto_san && !complete && started)
        {
            if (level_exit.number_of_enemies == 0
                && d_walls == wall_spawner.cubes_spawning)
            {
                wall_spawner.Spawn_Bonus_Item(1000000, 4);
                complete = true;
            }
        }

        // Dezeniman-san - Without destroying a single enemy, destroy every single wall on the stage and 
        // detonate a bomb on the exit three times while not killing the spawned enemies from the exit
        if (bonus_type == Bonus_Type.dezeniman_san && !complete)
        {
            if (exit_bombed >= 3)
            {
                wall_spawner.Spawn_Bonus_Item(20000000, 5);
                complete = true;
            }
        }
    }
}
