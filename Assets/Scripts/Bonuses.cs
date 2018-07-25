using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bonus_Type { none, bonus_target, goddess_mask, cola_bottle, famicom, nakamoto_san, dezeniman_san }

public class Bonuses : MonoBehaviour
{
    public Bonus_Type bonus_type;

    private int level_number;

    private bool complete;

    public Outside_Wall[] outside_path;

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
        yield return new WaitForSeconds(0.1f);
        Select_Bonus();
    }

    void Select_Bonus ()
    {
        level_number = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number;
        bonus_type = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_types[level_number];

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
        if (bonus_type == Bonus_Type.bonus_target && !complete)
        {
            if (GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies
                == GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().total_number_of_enemies
                && GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().triggered)
            {
                complete = true;
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score += 10000;
            }
        }

        // Goddess Mask
        if (bonus_type == Bonus_Type.goddess_mask && !complete)
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
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score += 20000;
                complete = true;
            }
        }

        // Cola Bottle
        if (bonus_type == Bonus_Type.cola_bottle && !complete)
        {

        }

        // Famicom
        if (bonus_type == Bonus_Type.famicom && !complete)
        {
            if(bombs_chained > 248)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score += 500000;
                complete = true;
            }
        }

        // Nakamoto-san
        if (bonus_type == Bonus_Type.nakamoto_san && !complete)
        {
            if (GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies == 0
                && d_walls == GameObject.Find("Wall_Spawns").GetComponent<Wall_Spawner>().cubes_spawning)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score += 10000000;
                complete = true;
            }
        }

        // Dezeniman-san
        if (bonus_type == Bonus_Type.dezeniman_san && !complete)
        {
            if (exit_bombed >= 3)
            {
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score += 20000000;
                complete = true;
            }
        }
    }
}
