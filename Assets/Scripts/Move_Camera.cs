using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    [Header("- The Transform Of The Camera -")]
    public Transform camera_point;

    [Header("- Positions That The Camera Moves To -")]
    public Transform position_a;
    public Transform position_b;

    [Header("- Which Position The Camera Is Currently In -")]
    public int position = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Physics.Linecast(transform.position, camera_point.position))
        {
            Debug.Log("Change");

            if (position == 0)
            {
                position = 2;
                gameObject.transform.position = new Vector3(position_b.position.x, position_b.position.y, position_b.position.z);
                StartCoroutine(Cam_Wait(1));
            }

            if (position == 1)
            {
                position = 2;
                gameObject.transform.position = new Vector3(position_a.position.x, position_a.position.y, position_a.position.z);
                StartCoroutine(Cam_Wait(0));
            }
        }
	}

    IEnumerator Cam_Wait(int cam_number)
    {
        yield return new WaitForSeconds(0.1f);
        position = cam_number;
    }
}
