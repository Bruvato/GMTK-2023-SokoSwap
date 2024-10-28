using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of movement
    [SerializeField] private float gridSize = 1;

    private Vector3 targetPosition; // Target position to move towards

    public bool isMoving;
    private bool soundPlayed;


    private void Start()
    {
        targetPosition = transform.position; // Start at current position
    }
    private void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
            // Apply Gravity
            Move(Vector3.down);
        }

    }

    public void Move(Vector3 direction)
    {
        // Calculate the new position for the block
        Vector3 blockTargetPosition = transform.position + (direction * gridSize);

        // Move the block if the new position is valid
        if (IsValidGridPosition(blockTargetPosition))
        {
            targetPosition = blockTargetPosition;

            if (direction != Vector3.down)
                FindObjectOfType<AudioManager>().Play("player move");
            else
            {
                if (!soundPlayed)
                {
                    FindObjectOfType<AudioManager>().Play("fall");
                    soundPlayed = true;
                }
            }

        }


    }

    public bool IsValidGridPosition(Vector3 position)
    {
        // Perform raycast to check for obstacles or other conditions
        RaycastHit hit;

        // Perform the raycast from current position to the new position
        if (Physics.Raycast(transform.position, position - transform.position, out hit, gridSize))
        {
            // check if collides with anything

            if (position - targetPosition != Vector3.down)
                FindObjectOfType<AudioManager>().Play("collide");

            return false;
        }

        return true;
    }


}
