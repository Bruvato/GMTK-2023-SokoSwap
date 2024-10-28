using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isActivated;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the box enters the pressure plate trigger zone
        if (other.CompareTag("Box"))
        {
            isActivated = true;
            //CheckWinCondition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the box exits the pressure plate trigger zone
        if (other.CompareTag("Box"))
        {
            isActivated = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (gameObject.name == "Goal")
            Gizmos.color = Color.red;
        else if (gameObject.name == "final goal")
            Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.5f);
    }
}
