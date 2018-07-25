using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Spawner : MonoBehaviour
{
    [Header("- List Of All Possible Spawn Positions -")]
    public List<GameObject> all_spots = new List<GameObject>();
    private List<GameObject> temp_all_spots = new List<GameObject>();

    [Header("- Section For Destrucible Wall Spawn Stats -")]
    public GameObject wall_prefab;
    public int cubes_spawning;
    public List<GameObject> cube_spawning_spots = new List<GameObject>();

    [Header("- Section For Powerup Spawn Stats -")]
    public GameObject power_up_prefab;
    public Power_Ups power_up_type;
    private int power_up_int;

    [Header("- Section For Exit Spawn Stats -")]
    public GameObject exit_prefab;
    private int exit_int;

    [Header("- Section For Enemy Spawn Stats -")]
    public GameObject[] enemy_prefabs;
    public int[] enemy_spawing_numbers;
    public List<GameObject> enemy_spawning_spots = new List<GameObject>();

    private int level_number;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Initial_Wait());
    }

    IEnumerator Initial_Wait ()
    {
        yield return new WaitForSeconds(0.1f);

        level_number = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number;
        power_up_type = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().powerup_types[level_number];

        enemy_spawing_numbers[0] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().balloom_spawn_number[level_number];
        enemy_spawing_numbers[1] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().oneal_spawn_number[level_number];
        enemy_spawing_numbers[2] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().doll_spawn_number[level_number];
        enemy_spawing_numbers[3] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().minvo_spawn_number[level_number];
        enemy_spawing_numbers[4] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().kondoria_spawn_number[level_number];
        enemy_spawing_numbers[5] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().ovapi_spawn_number[level_number];
        enemy_spawing_numbers[6] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().pass_spawn_number[level_number];
        enemy_spawing_numbers[7] = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().pontan_spawn_number[level_number];

        Spawn_Level();
    }

    void Spawn_Level ()
    {
        temp_all_spots = all_spots;

        for (int i = 0; i < cubes_spawning; i++)
        {
            int current_number = Random.Range(0, temp_all_spots.Count);

            cube_spawning_spots.Add(temp_all_spots[current_number]);
            temp_all_spots.Remove(temp_all_spots[current_number]);
        }

        power_up_int = Random.Range(0, cube_spawning_spots.Count);
        exit_int = Random.Range(0, cube_spawning_spots.Count);

        for (int i = 0; i < cube_spawning_spots.Count; i++)
        {
            Instantiate(wall_prefab, cube_spawning_spots[i].transform.position, cube_spawning_spots[i].transform.rotation);

            if (power_up_int == i)
            {
                GameObject powerup = Instantiate(power_up_prefab, cube_spawning_spots[i].transform.position, cube_spawning_spots[i].transform.rotation) as GameObject;
                powerup.GetComponent<Power_Up>().power_up_type = power_up_type;
            }

            if (exit_int == i)
            {
                Instantiate(exit_prefab, new Vector3(cube_spawning_spots[i].transform.position.x, 0.001f, cube_spawning_spots[i].transform.position.z), cube_spawning_spots[i].transform.rotation);
            }
        }

        // Enemy Spawning
        for (int i = 0; i < enemy_prefabs.Length; i++)
        {
            for (int j = 0; j < enemy_spawing_numbers[i]; j++)
            {
                int current_number = Random.Range(0, temp_all_spots.Count);

                enemy_spawning_spots.Add(temp_all_spots[current_number]);
                temp_all_spots.Remove(temp_all_spots[current_number]);
            }

            foreach (GameObject enemy in enemy_spawning_spots)
            {
                Instantiate(enemy_prefabs[i], enemy.transform.position, enemy.transform.rotation);
            }

            enemy_spawning_spots.Clear();
        }
    }
}
