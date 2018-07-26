using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemy_speed { One, Two, Three, Four }
public enum enemy_intelligence { One, Two, Three }

public class Enemy_AI : MonoBehaviour
{
    [Header("- Enemy Stats -")]
    public enemy_speed speed;
    public enemy_intelligence smarts;

    [Header("- How Many Points The Enemy Is Worth -")]
    public int points;

    [Header("- Whether The Enemy Can PassThrough Walls -")]
    public bool wall_pass;

    private float move_speed;

    public List<GameObject> directions = new List<GameObject>();
    public List<GameObject> move_direction = new List<GameObject>();

    private GameObject player;
    private int player_direction;

	// Use this for initialization
	void Start ()
    {
        // Player
        player = GameObject.FindGameObjectWithTag("Player");

        // Setting Speed
	    if (speed == enemy_speed.One)
        {
            move_speed = 2;
        }
        if (speed == enemy_speed.Two)
        {
            move_speed = 1;
        }
        if (speed == enemy_speed.Three)
        {
            move_speed = 0.5f;
        }
        if (speed == enemy_speed.Four)
        {
            move_speed = 0.2f;
        }
        Move();
    }

    // Enemy Movement
    void Move ()
    {
        if (smarts == enemy_intelligence.One)
        {
            Random_Movement();
        }

        if (smarts == enemy_intelligence.Two)
        {
            int move_mode = Random.Range(0, 2);

            if (move_mode == 0)
            {
                Random_Movement();
            }

            if (move_mode == 1)
            {
                Follow_Player();
            }
        }

        if (smarts == enemy_intelligence.Three)
        {
            Follow_Player();
        }

        StartCoroutine(Wait_Timer());
    }

    void Random_Movement ()
    {
        foreach (GameObject direction in directions)
        {
            RaycastHit hit;
            if (!Physics.Linecast(gameObject.transform.position, direction.transform.position, out hit))
            {
                move_direction.Add(direction);
            }
            else
            {
                if(hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(direction);
                }
            }
        }

        if (move_direction.Count > 0)
        {
            int use_direction = Random.Range(0, move_direction.Count);

            if (move_direction[use_direction].name == "Up_Point")
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
            }

            if (move_direction[use_direction].name == "Right_Point")
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            if (move_direction[use_direction].name == "Down_Point")
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
            }

            if (move_direction[use_direction].name == "Left_Point")
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }

    // Used To Find The Player In Relation To The Enemy
    void Player_Finder()
    {
        float up_distance = Vector3.Distance(directions[0].transform.position, player.transform.position);
        float right_distance = Vector3.Distance(directions[1].transform.position, player.transform.position);
        float down_distance = Vector3.Distance(directions[2].transform.position, player.transform.position);
        float left_distance = Vector3.Distance(directions[3].transform.position, player.transform.position);

        // Striaght Directions
        // Up
        if (up_distance < down_distance && right_distance == left_distance)
        {
            player_direction = 0;
        }
        // Right
        if (up_distance == down_distance && right_distance < left_distance)
        {
            player_direction = 2;
        }
        // Down
        if (up_distance > down_distance && right_distance == left_distance)
        {
            player_direction = 4;
        }
        // left
        if (up_distance == down_distance && right_distance > left_distance)
        {
            player_direction = 6;
        }

        //Diagonal Directions
        // Up Right
        if (up_distance < down_distance && right_distance < left_distance)
        {
            player_direction = 1;
        }
        // Down Right
        if (up_distance > down_distance && right_distance < left_distance)
        {
            player_direction = 3;
        }
        // Down left
        if (up_distance > down_distance && right_distance > left_distance)
        {
            player_direction = 5;
        }
        // Up left
        if (up_distance < down_distance && right_distance > left_distance)
        {
            player_direction = 7;
        }
    }

    void Follow_Player ()
    {
        Player_Finder();

        // Straight
        if (player_direction == 0)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[0].transform.position, out hit))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
            }
            else if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
            }
            else
            {
                if (!Physics.Linecast(gameObject.transform.position, directions[1].transform.position, out hit))
                {
                    move_direction.Add(directions[1]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[1]);
                    }
                }
                if (!Physics.Linecast(gameObject.transform.position, directions[3].transform.position, out hit))
                {
                    move_direction.Add(directions[3]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[3]);
                    }
                }

                if (move_direction.Count > 0)
                {
                    int use_direction = Random.Range(0, move_direction.Count);

                    if (move_direction[use_direction].name == "Right_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
                    }

                    if (move_direction[use_direction].name == "Left_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
                    }
                }
            }
        }

        if (player_direction == 2)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[1].transform.position, out hit))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                if (!Physics.Linecast(gameObject.transform.position, directions[0].transform.position, out hit))
                {
                    move_direction.Add(directions[0]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[0]);
                    }
                }
                if (!Physics.Linecast(gameObject.transform.position, directions[2].transform.position, out hit))
                {
                    move_direction.Add(directions[2]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[2]);
                    }
                }

                if (move_direction.Count > 0)
                {
                    int use_direction = Random.Range(0, move_direction.Count);

                    if (move_direction[use_direction].name == "Up_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
                    }

                    if (move_direction[use_direction].name == "Down_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
                    }
                }
            }
        }

        if (player_direction == 4)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[2].transform.position, out hit))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
            }
            else if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
            }
            else
            {
                if (!Physics.Linecast(gameObject.transform.position, directions[1].transform.position, out hit))
                {
                    move_direction.Add(directions[1]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[1]);
                    }
                }
                if (!Physics.Linecast(gameObject.transform.position, directions[3].transform.position, out hit))
                {
                    move_direction.Add(directions[3]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[3]);
                    }
                }

                if (move_direction.Count > 0)
                {
                    int use_direction = Random.Range(0, move_direction.Count);

                    if (move_direction[use_direction].name == "Right_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
                    }

                    if (move_direction[use_direction].name == "Left_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
                    }
                }
            }
        }

        if (player_direction == 6)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[3].transform.position, out hit))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                if (!Physics.Linecast(gameObject.transform.position, directions[0].transform.position, out hit))
                {
                    move_direction.Add(directions[0]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[0]);
                    }
                }
                if (!Physics.Linecast(gameObject.transform.position, directions[2].transform.position, out hit))
                {
                    move_direction.Add(directions[2]);
                }
                else
                {
                    if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                    {
                        move_direction.Add(directions[2]);
                    }
                }

                if (move_direction.Count > 0)
                {
                    int use_direction = Random.Range(0, move_direction.Count);

                    if (move_direction[use_direction].name == "Up_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
                    }

                    if (move_direction[use_direction].name == "Down_Point")
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
                    }
                }
            }
        }

        // Diagonal
        if (player_direction == 1)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[0].transform.position, out hit))
            {
                move_direction.Add(directions[0]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[0]);
                }
            }
            if (!Physics.Linecast(gameObject.transform.position, directions[1].transform.position, out hit))
            {
                move_direction.Add(directions[1]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[1]);
                }
            }

            if (move_direction.Count > 0)
            {
                int use_direction = Random.Range(0, move_direction.Count);

                if (move_direction[use_direction].name == "Up_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
                }

                if (move_direction[use_direction].name == "Right_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
                }
            }
        }

        if (player_direction == 3)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[1].transform.position, out hit))
            {
                move_direction.Add(directions[1]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[1]);
                }
            }
            if (!Physics.Linecast(gameObject.transform.position, directions[2].transform.position, out hit))
            {
                move_direction.Add(directions[2]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[2]);
                }
            }

            if (move_direction.Count > 0)
            {
                int use_direction = Random.Range(0, move_direction.Count);

                if (move_direction[use_direction].name == "Right_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
                }

                if (move_direction[use_direction].name == "Down_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
                }
            }
        }

        if (player_direction == 5)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[2].transform.position, out hit))
            {
                move_direction.Add(directions[2]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[2]);
                }
            }
            if (!Physics.Linecast(gameObject.transform.position, directions[3].transform.position, out hit))
            {
                move_direction.Add(directions[3]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[3]);
                }
            }

            if (move_direction.Count > 0)
            {
                int use_direction = Random.Range(0, move_direction.Count);

                if (move_direction[use_direction].name == "Down_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
                }

                if (move_direction[use_direction].name == "left_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
                }
            }
        }

        if (player_direction == 7)
        {
            RaycastHit hit;

            if (!Physics.Linecast(gameObject.transform.position, directions[3].transform.position, out hit))
            {
                move_direction.Add(directions[3]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[3]);
                }
            }
            if (!Physics.Linecast(gameObject.transform.position, directions[0].transform.position, out hit))
            {
                move_direction.Add(directions[0]);
            }
            else
            {
                if (hit.transform.gameObject.tag == "D_Wall" && wall_pass)
                {
                    move_direction.Add(directions[0]);
                }
            }

            if (move_direction.Count > 0)
            {
                int use_direction = Random.Range(0, move_direction.Count);

                if (move_direction[use_direction].name == "Left_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
                }

                if (move_direction[use_direction].name == "Up_Point")
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
                }
            }
        }
    }

    // Called When The Enemy Dies
    public void Die ()
    {
        Level_Exit lvl_exit = GameObject.FindGameObjectWithTag("Exit").GetComponent<Level_Exit>();
        lvl_exit.number_of_enemies--;

        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score += points;

        Destroy(gameObject);
    }

    // Called To Kill the Player
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator Wait_Timer ()
    {
        yield return new WaitForSeconds(move_speed);
        move_direction.Clear();
        Move();
    }
}
