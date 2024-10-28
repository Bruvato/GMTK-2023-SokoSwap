using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] public float targetAngle = 45f;
    private float currentAngle = 0f;
    [SerializeField] private float rotationAmt = 45f;
    [SerializeField] private float rotationSpeed = 5f;
    private bool axisDown;

    private void Update()
    {
        // float horizontal = Input.GetAxisRaw("Horizontal");
        // if (horizontal != 0)
        // {
        //     if (axisDown == false)
        //     {
        //         targetAngle -= horizontal * 45;
        //         axisDown = true;
        //     }
        // }
        // if (horizontal == 0)
        // {
        //     axisDown = false;
        // }
        bool left = Input.GetKeyDown(KeyCode.Q);
        bool right = Input.GetKeyDown(KeyCode.E);


        if (left && !right)
        {
            if (!axisDown)
            {
                targetAngle += rotationAmt;
                axisDown = true;
            }
        }
        else if (right && !left)
        {
            if (!axisDown)
            {
                targetAngle -= rotationAmt;
                axisDown = true;
            }
        }
        else
        {
            axisDown = false;
        }

        if (targetAngle < 0)
            targetAngle += 360;

        if (targetAngle > 360)
            targetAngle -= 360;

        currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(30, currentAngle, 0);
    }
}
