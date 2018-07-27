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
    public Level_Exit exit;
    private int exit_int;

    [Header("- Section For Bonus Item Spawn Stats -")]
    public GameObject bonus_item_prefab;

    [Header("- Section For Enemy Spawn Stats -")]
    public GameObject[] enemy_prefabs;
    public int[] enemy_spawing_numbers;
    public List<GameObject> enemy_spawning_spots = new List<GameObject>();

    private int level_number;

    // A Delay To Ensure Everything It Needs Is Spawned In
    public void Delay ()
    {
        // Gets The Stage Number And Powerup Type For That Stage
        level_number = Data.game_data.level_number;
        power_up_type = Data.game_data.powerup_types[level_number];

        // Gets The Amount Of Each Enemy Type that Spwans In For The Particular Stage
        enemy_spawing_numbers[0] = Data.game_data.balloom_spawn_number[level_number];
        enemy_spawing_numbers[1] = Data.game_data.oneal_spawn_number[level_number];
        enemy_spawing_numbers[2] = Data.game_data.doll_spawn_number[level_number];
        enemy_spawing_numbers[3] = Data.game_data.minvo_spawn_number[level_number];
        enemy_spawing_numbers[4] = Data.game_data.kondoria_spawn_number[level_number];
        enemy_spawing_numbers[5] = Data.game_data.ovapi_spawn_number[level_number];
        enemy_spawing_numbers[6] = Data.game_data.pass_spawn_number[level_number];
        enemy_spawing_numbers[7] = Data.game_data.pontan_spawn_number[level_number];

        // Assigns The Spawn Locations To A Temporary List For Use
        temp_all_spots = all_spots;

        // Normal Spawning For Stages
        if (Data.game_data.bonus_stage == Bonus_Stage.Off)
        {
            Spawn_Cubes();
            Spawn_Enemies();
        }
        // Spawning For Bonus Stages
        else
        {
            Instantiate(exit_prefab, new Vector3(1000, 0, 0), transform.rotation);
            StartCoroutine(Bonus_Stage_Spawn_Enemies());
        }
    }

    // Cube Spawning
    void Spawn_Cubes ()
    {
        for (int i = 0; i < cubes_spawning; i++)
        {
            int current_number = Random.Range(0, temp_all_spots.Count);

            cube_spawning_spots.Add(temp_all_spots[current_number]);
            temp_all_spots.Remove(temp_all_spots[current_number]);
        }

        // Selects Random Ints Within the Cube Spawn List To Spawn Items Under Cubes
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
                GameObject exit_go = Instantiate(exit_prefab, new Vector3(cube_spawning_spots[i].transform.position.x, 0.001f, cube_spawning_spots[i].transform.position.z), cube_spawning_spots[i].transform.rotation) as GameObject;
                exit = exit_go.GetComponent<Level_Exit>();
            }
        }
    }

    // Enemy Spawning
    void Spawn_Enemies ()
    {
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

    // Spawning The Bonus Items
    public void Spawn_Bonus_Item (int points, int sprite_index)
    {
        int random_number = Random.Range(0, all_spots.Count);

        for (int i = 0; i < all_spots.Count; i++)
        {
            if (random_number == i)
            {
                GameObject bonus_item_go = Instantiate(bonus_item_prefab, all_spots[i].transform.position, all_spots[i].transform.rotation) as GameObject;
                bonus_item_go.GetComponent<Bonus_Pickup>().points = points;
                bonus_item_go.GetComponent<Bonus_Pickup>().sprite_index = sprite_index;
            }
        }
    }

    // Used To Spawn Enemies Every Second
    IEnumerator Bonus_Stage_Spawn_Enemies ()
    {
        for (int i = 0; i < 31; i++)
        {
            int random_position = Random.Range(0, temp_all_spots.Count);

            Instantiate(enemy_prefabs[Data.game_data.bonus_enemy_index],
                temp_all_spots[random_position].transform.position, temp_all_spots[random_position].transform.rotation);

            yield return new WaitForSeconds(1.0f);
        }
    }

    // Spawns Five Pontan Enemies When Timer Reaches Zero
    public void Time_Up ()
    {
        for (int i = 0; i < 5; i++)
        {
            int current_number = Random.Range(0, temp_all_spots.Count);

            enemy_spawning_spots.Add(temp_all_spots[current_number]);
            temp_all_spots.Remove(temp_all_spots[current_number]);
        }

        foreach (GameObject enemy in enemy_spawning_spots)
        {
            Instantiate(enemy_prefabs[7], enemy.transform.position, enemy.transform.rotation);
        }

        enemy_spawning_spots.Clear();
    }
}
