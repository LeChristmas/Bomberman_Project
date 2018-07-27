using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("- Array Of Direction Points Movement Uses")]
    public GameObject[] direction_point;

    [Header("- Variables Used For Movement -")]
    public float x_step_distacne = 1.0f;
    public float z_step_distacne = 1.0f;
    public float player_speed = 0.4f;
    public float increased_player_speed = 0.2f;
    private float used_speed;
    public bool walk_delay;
    public int direction;
    public bool blocked;

    [Header("- Local Powerup Stats -")]
    public GameObject bomb_prefab;
    public int max_bombs;
    public int bomb_strength;
    public bool speed_increase;
    public bool detonator;
    public float mystery_timer = 5.0f;
    private bool mystery;

    [Header("- Bomb Variables -")]
    public float bomb_timer;
    public List<Bomb> bombs = new List<Bomb>();

    [Header("Arrow That Shows Player's Direction -")]
    public GameObject arrow;

    [Header("- variables used For Bonuses -")]
    public bool moving;
    public bool on_exit;
    private int move_timer_cycle;
    public bool bottle_bonus_complete;

    private Transform player_transform;

    // Use this for initialization
    void Start ()
    {
        player_transform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (speed_increase)
        {
            used_speed = increased_player_speed;
        }
        else
        {
            used_speed = player_speed;
        }

        // Movement
		//Up Movement
        if (Input.GetKey(KeyCode.W) && Time.timeScale != 0.0f)
        {
            Pre_Move(0, 0, 0, z_step_distacne);
        }
        //Right Movement
        if (Input.GetKey(KeyCode.D) && Time.timeScale != 0.0f)
        {
            Pre_Move(1, -90, x_step_distacne, 0);
        }
        //Down Movement
        if (Input.GetKey(KeyCode.S) && Time.timeScale != 0.0f)
        {
            Pre_Move(2, 180, 0, -z_step_distacne);
        }
        //Left Movement
        if (Input.GetKey(KeyCode.A) && Time.timeScale != 0.0f)
        {
            Pre_Move(3, 90, -x_step_distacne, 0);
        }

        // Detonator
        if (Input.GetKeyDown(KeyCode.E) && detonator && Time.timeScale != 0.0f)
        {
            if(bombs.Count > 0)
            {
                bombs[0].Bang();
            }
        }

        // Called When Movement Stops, For Bonuses
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) && Time.timeScale != 0.0f)
        {
            moving = false;
            on_exit = false;
            move_timer_cycle = 0;
        }

        // Bomb Spawn
        if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0.0f
            && bombs.Count < max_bombs)
        {
            GameObject bomb;
            Bomb bomb_script;

            bomb = Instantiate(bomb_prefab, transform.position, transform.rotation) as GameObject;
            bomb_script = bomb.GetComponent<Bomb>();
            bomb_script.Bomb_Spawned(bomb_strength, bomb_timer, detonator);
            bombs.Add(bomb_script);
        }
    }

    void Pre_Move (int local_direction, float angle, float x_move, float z_move)
    {
        if (!moving && on_exit)
        {
            moving = true;
            StartCoroutine(Bottle_Timer());
        }

        if (!walk_delay)
        {
            walk_delay = true;
            direction = local_direction;
            Move(x_move, z_move);
            StartCoroutine(Walk_Delay());
        }

        arrow.transform.rotation = Quaternion.Euler(90, 0, angle);
    }

    void Move (float x_move, float z_move)
    {
        RaycastHit hit;
        if (Physics.Linecast(gameObject.transform.position, direction_point[direction].transform.position, out hit))
        {
            if (Data.game_data.player_wallpass
                && hit.transform.tag == "D_Wall")
            {
                blocked = false;
            }
            else if (Data.game_data.player_bombpass
                && hit.transform.tag == "Bomb")
            {
                blocked = false;
            }
            else
            {
                blocked = true;
            }
        }
        else
        {
            blocked = false;
        }

        if (!blocked)
        {
            player_transform.position = new Vector3(player_transform.position.x + x_move, player_transform.position.y, player_transform.position.z + z_move);
        }
    }

    IEnumerator Walk_Delay ()
    {
        yield return new WaitForSeconds(used_speed);
        walk_delay = false;
    }

    IEnumerator Bottle_Timer ()
    {
        yield return new WaitForSeconds(0.5f);

        if(moving && move_timer_cycle < 3)
        {
            StartCoroutine(Bottle_Timer());
            move_timer_cycle++;
        }
        else if (move_timer_cycle == 3)
        {
            bottle_bonus_complete = true;
        }
    }

    public void Mystery_Powerup ()
    {
        mystery = true;
        StartCoroutine(Mystery_Delay());
    }

    IEnumerator Mystery_Delay ()
    {
        yield return new WaitForSeconds(mystery_timer);
        mystery = false;
    }

    public void Die()
    {
        if (!mystery)
        {
            GameObject.Find("Canvas").GetComponent<UI>().Death(gameObject);
        }
    }
}
