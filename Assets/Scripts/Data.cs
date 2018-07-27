using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public enum Bonus_Stage { Off, A, B, C, D, E, F, G, H, I, J }

public enum Scene { Menu, Game }

public class Data : MonoBehaviour
{
    // Static Reference for other scripts
    public static Data game_data;

    [Header("- Used To Tell Whether Game Is In The Menu Or The Game -")]
    public Scene current_scene;

    [Header("- level Spawn Stats -")]
    // Number Assigned To Each Level
    public int level_number;
    // Powerup Type In Each Level
    public Power_Ups[] powerup_types;
    // Bonus Type In Each Level
    public Bonus_Type[] bonus_types;
    // Balloom Spawn Numbers
    public int[] balloom_spawn_number;
    // Oneal Spawn Numbers
    public int[] oneal_spawn_number;
    // Doll Spawn Numbers
    public int[] doll_spawn_number;
    // Minvo Spawn Numbers
    public int[] minvo_spawn_number;
    // Kondoria Spawn Numbers
    public int[] kondoria_spawn_number;
    // Ovapi Spawn Numbers
    public int[] ovapi_spawn_number;
    // pass Spawn Numbers
    public int[] pass_spawn_number;
    // Pontan Spawn Numbers
    public int[] pontan_spawn_number;



    [Header("- Integers For Game Properties -")]
    public int score;
    public int lives;
    public int start_time;

    [Header("- game UI -")]
    public Text score_text;
    public Text lives_text;
    public Text timer_text;

    [Header("- Bonus Stage Variable -")]
    public Bonus_Stage bonus_stage;
    public int bonus_enemy_index = -1;


    private Player_Movement player_script;

    [Header("- Powerups -")]
    // Bombs - increase max amount of bombs to be dropped at once
    public int max_bombs;
    // Flames - increase explosion range
    public int bomb_strength;
    // Speed - increase player's speed
    public bool speed_increase;
    // Wallpass - allows player to move trhough destructible walls
    public bool player_wallpass;
    // Detonator - remotely detonates oldest bomb and disables bomb timer
    public bool detonator;
    // Bombpass - allows player to pass through bombs
    public bool player_bombpass;
    // Flamepass - grants player immunity to explosions
    public bool player_flamepass;

    [Header("- Saving Variables -")]
    public bool saving_active;

    public int save_pointer;

    public string[] score_name;
    public int[] score_number;

    // Used To Create Data Singleton
    void Awake ()
    {
        if (game_data == null)
        {
            DontDestroyOnLoad(this);
            game_data = this;
        }
        else if (game_data != this)
        {
            Destroy(gameObject);
        }
        Load_Data();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (current_scene == Scene.Game)
        {
            if (player_script != null)
            {
                player_script.max_bombs = max_bombs;
                player_script.bomb_strength = bomb_strength;
                player_script.speed_increase = speed_increase;
                player_script.detonator = detonator;
            }
        }
	}

    public void Delay ()
    {
        if (timer_text == null) timer_text = GameObject.Find("Timer_Text").GetComponent<Text>();

        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();

        if (bonus_stage == Bonus_Stage.Off)
        {
            start_time = 201;
            player_script.mystery_timer = 5.0f;
        }
        else
        {
            start_time = 31;
            player_script.mystery_timer = 33.0f;
            player_script.Mystery_Powerup();
        }

        GameObject.Find("Canvas").GetComponent<UI>().time = start_time;
        GameObject.Find("Canvas").GetComponent<UI>().timer_text = timer_text;
    }



    // Saving Functions

    // Initally Setting The Data Into Place
    public void Load_Data ()
    {
        // Loading From File
        if (File.Exists(Application.persistentDataPath + "/Score_Data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Score_Data.dat", FileMode.Open);
            Score_Data score_data = (Score_Data)bf.Deserialize(file);
            file.Close();

            save_pointer = score_data.save_pointer;

            score_name = score_data.score_name;
            score_number = score_data.score_number;
        }
        else
        {
            save_pointer = 0;
        }
    }

    // Used To Save The Players Score
    public void Save_Data (string name)
    {
        score_name[save_pointer] = name;
        score_number[save_pointer] = score;

        if (save_pointer > 8)
        {
            save_pointer = 0;
        }
        else
        {
            save_pointer++;
        }

        // Saving To File
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Score_Data.dat");
        Score_Data score_data = new Score_Data();

        score_data.save_pointer = save_pointer;

        score_data.score_name = score_name;
        score_data.score_number = score_number;


        bf.Serialize(file, score_data);
        file.Close();
    }

    // Used To Erase All Data
    public void Clear_Data ()
    {
        // Clears All Infomation In File
        if (File.Exists(Application.persistentDataPath + "/Score_Data.dat"))
        {
            File.Delete(Application.persistentDataPath + "/Score_Data.dat");

            save_pointer = 0;

            for (int i = 0; i < 10; i++)
            {
                score_name[i] = "";
                score_number[i] = 0;
            }
        }
    }
}


[Serializable]
class Score_Data
{
    public int save_pointer;

    public string[] score_name;
    public int[] score_number;
}
