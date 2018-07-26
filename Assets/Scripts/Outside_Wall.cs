using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside_Wall : MonoBehaviour
{
    [Header("- Used For The Goddess Mask Bonus -")]
    public bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }
}
