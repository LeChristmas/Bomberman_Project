using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Exit : MonoBehaviour
{
    [Header("- The Number Of Enemies Currently In The Level -")]
    public int number_of_enemies = 0;

    [Header("- Enemy Arrays -")]
    private GameObject[] temp_enemies;
    public List<GameObject> enemies = new List<GameObject>();

    private bool started = false;

    [Header("- How Many Enemies There Have Been In The Stage In Total -")]
    public int total_number_of_enemies;

    [Header("- Bool For If The Player Has Mover Over Exit -")]
    public bool triggered;

    [Header("- The Prefab For The Enemy That Spawns When Exit Is Bombed -")]
    public GameObject bombed_enemy_prefab;

    // Used To Make Sure Enemies Don't Sapwn As Soon As The Exit Is Revealed
    public Transform linecast_target;
    public bool linecast_lock = false;
    public bool bomb_delay_bool = false;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Initial_Wait());
	}

    private void Update()
    {
        if (!Physics.Linecast(transform.position, linecast_target.position) && !linecast_lock)
        {
            linecast_lock = true;
            StartCoroutine(Bomb_Exit_Delay());
        }
    }

    IEnumerator Initial_Wait ()
    {
        yield return new WaitForSeconds(0.5f);

        temp_enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in temp_enemies)
        {
            enemies.Add(enemy);
            number_of_enemies++;
        }

        total_number_of_enemies = number_of_enemies;
        started = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player_Movement>().on_exit = true;

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
    }

    IEnumerator Bomb_Exit_Delay ()
    {
        yield return new WaitForSeconds(1.0f);

        bomb_delay_bool = true;
    }

    // Called When The Exit Is Hit By A Bomb
    public void Exit_Bombed()
    {
        if (bomb_delay_bool)
        {

            GameObject.Find("Bonus").GetComponent<Bonuses>().Bomb_Exit();

            for (int i = 0; i < 3; i++)
            {
                GameObject spwaned_enemy = Instantiate(bombed_enemy_prefab, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
                enemies.Add(spwaned_enemy);
                total_number_of_enemies++;
                number_of_enemies++;
            }
        }
    }
}
