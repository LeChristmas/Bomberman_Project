using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bonus_Type { none, bonus_target, goddess_mask, cola_bottle, famicom, nakamoto_san, dezeniman_san }

public class Bonuses : MonoBehaviour
{
    public Bonus_Type bonus_type;

    private int level_number;

    private bool complete;

    public Collider[] outside_path;

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
    }

	// Update is called once per frame
	void Update ()
    {
		// Bonus Target
        if (bonus_type == Bonus_Type.bonus_target)
        {
            if (GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().number_of_enemies
                == GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().total_number_of_enemies
                && GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>().triggered && !complete)
            {
                complete = true;
                GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score += 10000;
            }
        }

        // Goddess Mask
        if (bonus_type == Bonus_Type.goddess_mask)
        {

        }
	}
}
