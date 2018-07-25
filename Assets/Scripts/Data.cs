using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    [Header("- level Spawn Stats")]
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
    // Mystery - grants player temporary immunity to explosions and enemies
    public bool mystery;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (score_text == null) score_text = GameObject.Find("Score_Text").GetComponent<Text>();
        if (lives_text == null) lives_text = GameObject.Find("Lives_Text").GetComponent<Text>();
        if (timer_text == null) timer_text = GameObject.Find("Timer_Text").GetComponent<Text>();

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().bomb_strength = bomb_strength;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().detonator = detonator;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().max_bombs = max_bombs;

        GameObject.Find("Canvas").GetComponent<UI>().time = start_time;
        GameObject.Find("Canvas").GetComponent<UI>().timer_text = timer_text;

        score_text.text = "Score: " + score;
        lives_text.text = "Lives: " + lives;
	}
}
