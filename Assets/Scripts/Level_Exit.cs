using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Exit : MonoBehaviour
{
    public int number_of_enemies = 0;

    public GameObject[] enemies;

    private bool started = false;

    public int total_number_of_enemies;

    public bool triggered;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Initial_Wait());
	}

    IEnumerator Initial_Wait ()
    {
        yield return new WaitForSeconds(0.5f);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            number_of_enemies++;
        }

        total_number_of_enemies = number_of_enemies;
        started = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (started && number_of_enemies == 0)
            {
                Debug.Log("Level Complete");
                GameObject.Find("Canvas").GetComponent<UI>().Win();
            }
            if(!triggered)
            {
                triggered = true;
            }
        }

        if (other.tag == "Bomb")
        {
            GameObject.Find("Bonus").GetComponent<Bonuses>().Bomb_Exit();
        }
    }
}
