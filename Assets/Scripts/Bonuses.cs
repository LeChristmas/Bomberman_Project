using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bonus_Type { none, bonus_target, goddess_mask, cola_bottle, famicom, nakamoto_san, dezeniman_san }

public class Bonuses : MonoBehaviour
{
    [Header("- Dropdown Of All Bonuses -")]
    public Bonus_Type bonus_type;

    private int level_number;

    private bool complete;
    public bool started;

    [Header("- Outside Path Used For Goddess Mask -")]
    public Outside_Wall[] outside_path;

    [Header("- Amount Of Destrucible Walls -")]
    public int d_walls;

    private int bombs_chained;
    private int exit_bombed;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Initial_Wait());
	}
	
    IEnumerator Initial_Wait ()
    {
        yield return new WaitForSeconds(0.2f);
        Select_Bonus();
        yield return new WaitForSeconds(2.0f);
        started = true;
    }

    public void Select_Bonus ()
    {
        level_number = Data.game_data.level_number;
        bonus_type = Data.game_data.bonus_types[level_number];

        d_walls = GameObject.Find("Wall_Spawns").GetComponent<Wall_Spawner>().cubes_spawning;
    }

    public void Chain_Bomb ()
    {
        if (GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies == 0)
        {
            bombs_chained++;
        }
    }

    public void Bomb_Exit ()
    {
        if (GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies 
            == GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().total_number_of_enemies
            && d_walls == 0)
        {
            exit_bombed++;
        }
    }

	// Update is called once per frame
	void Update ()
    {
		// Bonus Target
        if (bonus_type == Bonus_Type.bonus_target && !complete && started)
        {
            if (GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies
                == GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().total_number_of_enemies
                && GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().triggered)
            {
                complete = true;
                Data.game_data.score += 10000;
            }
        }

        // Goddess Mask
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

            if (passed == outside_path.Length && GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies == 0)
            {
                Data.game_data.score += 20000;
                complete = true;
            }
        }

        // Cola Bottle
        if (bonus_type == Bonus_Type.cola_bottle && !complete && started)
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().bottle_bonus_complete)
            {
                Data.game_data.score += 30000;
                complete = true;
            }
        }

        // Famicom
        if (bonus_type == Bonus_Type.famicom && !complete && started)
        {
            if(bombs_chained > 248)
            {
                Data.game_data.score += 500000;
                complete = true;
            }
        }

        // Nakamoto-san
        if (bonus_type == Bonus_Type.nakamoto_san && !complete && started)
        {
            if (GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies == 0
                && d_walls == GameObject.Find("Wall_Spawns").GetComponent<Wall_Spawner>().cubes_spawning)
            {
                Data.game_data.score += 10000000;
                complete = true;
            }
        }

        // Dezeniman-san
        if (bonus_type == Bonus_Type.dezeniman_san && !complete)
        {
            if (exit_bombed >= 3)
            {
                Data.game_data.score += 20000000;
                complete = true;
            }
        }
    }
}
