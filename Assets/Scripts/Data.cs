using UnityEngine;
using UnityEngine.UI;

public enum Bonus_Stage { Off, A, B, C, D, E, F, G, H, I, J }

public enum Scene { Menu, Game }

public class Data : MonoBehaviour
{
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

    public string[] score_name_key;
    public string[] score_name_value;

    public string[] score_number_key;
    public int[] score_number_value;

    public string current_save_pointer_key;
    public int current_save_pointer_value;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(this);
        Set_Data();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (current_scene == Scene.Menu)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Clear_Data();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                Save_Data("cuckin");
            }
        }

        if (current_scene == Scene.Game)
        {
            if (score_text == null) score_text = GameObject.Find("Score_Text").GetComponent<Text>();
            if (lives_text == null) lives_text = GameObject.Find("Lives_Text").GetComponent<Text>();
            if (timer_text == null) timer_text = GameObject.Find("Timer_Text").GetComponent<Text>();

            if (bonus_stage == Bonus_Stage.Off)
            {
                start_time = 201;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().mystery_timer = 5.0f;
            }
            else
            {
                start_time = 31;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().mystery_timer = 33.0f;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().Mystery_Powerup();
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().max_bombs = max_bombs;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().bomb_strength = bomb_strength;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().speed_increase = speed_increase;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().detonator = detonator;

            GameObject.Find("Canvas").GetComponent<UI>().time = start_time;
            GameObject.Find("Canvas").GetComponent<UI>().timer_text = timer_text;

            score_text.text = "Score: " + score;
            lives_text.text = "Lives: " + lives;
        }
	}

    // Initally Setting The Data Into Place
    void Set_Data ()
    {
        for (int i = 0; i < 10; i++)
        {
            score_name_value[current_save_pointer_value] = PlayerPrefs.GetString(score_name_key[i], "");
            score_number_value[current_save_pointer_value] = PlayerPrefs.GetInt(score_number_key[i], 0);
        }

        current_save_pointer_value = PlayerPrefs.GetInt(current_save_pointer_key, 0);
    }

    // Used To Save The Players Score
    public void Save_Data (string name)
    {
        score_name_value[current_save_pointer_value] = name;
        score_number_value[current_save_pointer_value] = score;

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetString(score_name_key[i], score_name_value[i]);
            PlayerPrefs.SetInt(score_number_key[i], score_number_value[i]);
        }

        if (current_save_pointer_value < 9)
        {
            current_save_pointer_value++;
        }
        else
        {
            current_save_pointer_value = 0;
        }

        PlayerPrefs.SetInt(current_save_pointer_key, current_save_pointer_value);
    }

    // Used To Erase All Data
    void Clear_Data ()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetString(score_name_key[i], "");
            PlayerPrefs.SetInt(score_number_key[i], 0);
        }

        PlayerPrefs.SetInt(current_save_pointer_key, 0);
    }
}
