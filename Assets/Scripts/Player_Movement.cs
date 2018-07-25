using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public GameObject[] direction_point;

    public int direction;
    public bool blocked;

    public GameObject bomb_prefab;
    public int max_bombs;
    public int bomb_strength;
    public bool detonator;
    public float bomb_timer;
    public List<Bomb> bombs = new List<Bomb>();

    public GameObject arrow;

    private Transform player_transform;

    // Use this for initialization
    void Start ()
    {
        player_transform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Movement
		//Up Movement
        if (Input.GetKeyDown(KeyCode.W) && Time.timeScale != 0.0f)
        {
            if(direction == 0)
            {
                Move();
            }

            direction = 0;
            arrow.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        //Right Movement
        if (Input.GetKeyDown(KeyCode.D) && Time.timeScale != 0.0f)
        {
            if (direction == 1)
            {
                Move();
            }

            direction = 1;
            arrow.transform.rotation = Quaternion.Euler(90, 0, -90);
        }
        //Down Movement
        if (Input.GetKeyDown(KeyCode.S) && Time.timeScale != 0.0f)
        {
            if (direction == 2)
            {
                Move();
            }

            direction = 2;
            arrow.transform.rotation = Quaternion.Euler(90, 0, 180);
        }
        //Left Movement
        if (Input.GetKeyDown(KeyCode.A) && Time.timeScale != 0.0f)
        {
            if (direction == 3)
            {
                Move();
            }

            direction = 3;
            arrow.transform.rotation = Quaternion.Euler(90, 0, 90);
        }

        // Detonator
        if (Input.GetKeyDown(KeyCode.E) && detonator && Time.timeScale != 0.0f)
        {
            if(bombs.Count > 0)
            {
                bombs[0].Bang();
            }
        }

        // Bomb Spawn
        if (Input.GetKeyDown(KeyCode.Q) && !blocked && Time.timeScale != 0.0f
            && bombs.Count < max_bombs)
        {
            GameObject bomb;
            Bomb bomb_script;

            RaycastHit hit;
            if (Physics.Linecast(gameObject.transform.position, direction_point[direction].transform.position, out hit))
            {
                blocked = true;
            }
            else
            {
                switch (direction)
                {
                    case 0:
                        bomb = Instantiate(bomb_prefab, direction_point[direction].transform.position, direction_point[direction].transform.rotation) as GameObject;
                        bomb_script = bomb.GetComponent<Bomb>();
                        bomb_script.Bomb_Spawned(bomb_strength, bomb_timer, detonator);
                        bombs.Add(bomb_script);
                        break;

                    case 1:
                        bomb = Instantiate(bomb_prefab, direction_point[direction].transform.position, direction_point[direction].transform.rotation) as GameObject;
                        bomb_script = bomb.GetComponent<Bomb>();
                        bomb_script.Bomb_Spawned(bomb_strength, bomb_timer, detonator);
                        bombs.Add(bomb_script);
                        break;

                    case 2:
                        bomb = Instantiate(bomb_prefab, direction_point[direction].transform.position, direction_point[direction].transform.rotation) as GameObject;
                        bomb_script = bomb.GetComponent<Bomb>();
                        bomb_script.Bomb_Spawned(bomb_strength, bomb_timer, detonator);
                        bombs.Add(bomb_script);
                        break;

                    case 3:
                        bomb = Instantiate(bomb_prefab, direction_point[direction].transform.position, direction_point[direction].transform.rotation) as GameObject;
                        bomb_script = bomb.GetComponent<Bomb>();
                        bomb_script.Bomb_Spawned(bomb_strength, bomb_timer, detonator);
                        bombs.Add(bomb_script);
                        break;
                }
            }
        }
    }

    void Move ()
    {
        RaycastHit hit;
        if (Physics.Linecast(gameObject.transform.position, direction_point[direction].transform.position, out hit))
        {
            if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_wallpass
                && hit.transform.tag == "D_Wall")
            {
                blocked = false;
            }
            else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_bombpass
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
            // Moves player One place in specified direction
            switch (direction)
            {
                case 0:
                    player_transform.position = new Vector3(player_transform.position.x, player_transform.position.y, player_transform.position.z + 1);
                    break;

                case 1:
                    player_transform.position = new Vector3(player_transform.position.x + 1, player_transform.position.y, player_transform.position.z);
                    break;

                case 2:
                    player_transform.position = new Vector3(player_transform.position.x, player_transform.position.y, player_transform.position.z - 1);
                    break;

                case 3:
                    player_transform.position = new Vector3(player_transform.position.x - 1, player_transform.position.y, player_transform.position.z);
                    break;
            }
        }
    }

    public void Die()
    {
        Debug.Log("Player Dead");
        GameObject.Find("Canvas").GetComponent<UI>().Death();
    }
}
