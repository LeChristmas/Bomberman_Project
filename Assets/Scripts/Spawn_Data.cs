using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Data : MonoBehaviour
{
    public GameObject data;

    private GameObject find_data;
	// Use this for initialization
	void Start ()
    {
        find_data = GameObject.FindGameObjectWithTag("data");

        if (find_data == null) Instantiate(data, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
