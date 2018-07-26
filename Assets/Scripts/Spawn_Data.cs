using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Data : MonoBehaviour
{
    [Header("- The Prefab That gets Spawned In -")]
    public GameObject data_prefab;

    private GameObject find_data;
	// Use this for initialization
	void Start ()
    {
        // Will Only Spawn In The Prefab If One Is Not Detected
        find_data = GameObject.FindGameObjectWithTag("data");

        if (find_data == null) Instantiate(data_prefab, transform.position, transform.rotation);
	}
}
