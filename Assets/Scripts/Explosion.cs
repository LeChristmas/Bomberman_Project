using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public List<Collider> wall_colliders = new List<Collider>();

    private bool disabled;

    void OnTriggerEnter(Collider other)
    {
        if (!wall_colliders.Contains(other) && !disabled)
        {
            if (other.tag == "D_Wall")
            {
                wall_colliders.Add(other);
            }
            else if (other.tag == "Player" && !GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_flamepass)
            {
                wall_colliders.Add(other);
            }
            else if (other.tag == "Bomb" || other.tag == "Enemy")
            {
                wall_colliders.Add(other);
            }
            else if (other.tag == "I_Wall")
            {
                wall_colliders.Clear();
                disabled = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        if(wall_colliders.Contains(other) && !disabled && other.tag == "Player" || other.tag == "Enemy" || other.tag == "Bomb")
        {
            wall_colliders.Remove(other);
        }
    }

    public void Blow_Up_Walls()
    {
        foreach (Collider wall in wall_colliders)
        {
            if (wall.gameObject.tag != "Bomb")
            {
                if (wall.gameObject.tag == "Player")
                {
                    wall.gameObject.GetComponent<Player_Movement>().Die();
                }

                if (wall.gameObject.tag == "Enemy")
                {
                    wall.gameObject.GetComponent<Enemy_AI>().Die();
                }

                if (wall.gameObject.tag == "D_Wall")
                {
                    GameObject.Find("Bonus").GetComponent<Bonuses>().d_walls--;
                }

                Destroy(wall.gameObject);
            }
            else
            {
                wall.gameObject.GetComponent<Bomb>().Chain_Bang();
            }
        }
    }
}
