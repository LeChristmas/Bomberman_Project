using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("- Bomb Boby -")]
    public Transform bomb_body;

    [Header("- Array Of All Colliders In Explosion Range -")]
    public List<Collider> wall_colliders = new List<Collider>();
    public List<Collider> other_colliders = new List<Collider>();
    public List<float> distance = new List<float>();

    private bool disabled;

    private float min_wall_distacne = float.MaxValue;
    private int minimum_distance_index = -1;

    void OnTriggerEnter(Collider other)
    {
        if (!wall_colliders.Contains(other) && !disabled)
        {
            if (other.tag == "D_Wall")
            {
                float local_distance = Vector3.Distance(bomb_body.position, other.transform.position);

                wall_colliders.Add(other);
                distance.Add(local_distance);
            }
            else if (other.tag == "I_Wall")
            {
                other_colliders.Clear();
                wall_colliders.Clear();
                disabled = true;
            }
        }

        if (!other_colliders.Contains(other) && !disabled)
        {
            if (other.tag == "Player" && !GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_flamepass)
            {
                other_colliders.Add(other);
            }
            else if (other.tag == "Bomb" || other.tag == "Enemy" || other.tag == "Exit")
            {
                other_colliders.Add(other);
            }
            else if (other.tag == "I_Wall")
            {
                other_colliders.Clear();
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
        if(other_colliders.Contains(other) && !disabled && other.tag == "Player" || other.tag == "Enemy" || other.tag == "Bomb")
        {
            other_colliders.Remove(other);
        }
    }

    public void Blow_Up_Walls()
    {
        for (int i = 0; i < distance.Count; i++)
        {
            if (distance[i] < min_wall_distacne)
            {
                min_wall_distacne = distance[i];
                minimum_distance_index = i;
            }
        }

        for (int i = 0; i < wall_colliders.Count; i++)
        {
            if (i == minimum_distance_index)
            {
                if (wall_colliders[i].gameObject.tag == "D_Wall")
                {
                    GameObject.Find("Bonus").GetComponent<Bonuses>().d_walls--;
                }

                Destroy(wall_colliders[i].gameObject);
            }
        }

        foreach (Collider other in other_colliders)
        {
            if (other.gameObject.tag != "Bomb")
            {
                if (other.gameObject.tag == "Player")
                {
                    other.gameObject.GetComponent<Player_Movement>().Die();
                }

                if (other.gameObject.tag == "Enemy")
                {
                    other.gameObject.GetComponent<Enemy_AI>().Die();
                }

                if (other.gameObject.tag == "Exit")
                {
                    other.gameObject.GetComponent<Level_Exit>().Exit_Bombed();
                }
            }
            else
            {
                other.gameObject.GetComponent<Bomb>().Chain_Bang();
            }
        }
    }
}
