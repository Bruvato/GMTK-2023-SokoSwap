using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of movement
    [SerializeField] private float gridSize = 1f; // Size of each grid cell

    private Vector3 targetPosition; // Target position to move towards

    private CameraPivot cameraPivot;

    public bool isMoving;

    private AniManager aniManager;

    private bool soundPlayed;

    private void Start()
    {
        targetPosition = transform.position; // Start at current position
        cameraPivot = GameObject.Find("Cam Pivot").GetComponent<CameraPivot>();
        aniManager = GameObject.Find("AniManager").GetComponent<AniManager>();
    }

    private void Update()
    {
        // Check if movement input is received
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Move(Quaternion.Euler(0, cameraPivot.targetAngle - 45, 0) * Vector3.right);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            Move(Quaternion.Euler(0, cameraPivot.targetAngle - 45, 0) * Vector3.left);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Quaternion.Euler(0, cameraPivot.targetAngle - 45, 0) * Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            Move(Quaternion.Euler(0, cameraPivot.targetAngle - 45, 0) * Vector3.back);

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
            // Apply Gravity
            Move(Vector3.down);
        }
    }

    private void Move(Vector3 direction)
    {
        // Calculate the new target position based on the grid size
        Vector3 newTargetPosition = targetPosition + (direction * gridSize);

        // Check if the new target position is valid
        if (IsValidGridPosition(newTargetPosition) && !isMoving && !aniManager.isAnimating)
        {
            targetPosition = newTargetPosition;
            isMoving = true;

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



            // Check if there's a block next to position
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, gridSize))
            {
                // Check if the block is tagged as "Block"
                if (hit.collider.CompareTag("Box"))
                {
                    Box box = hit.collider.GetComponent<Box>();
                    if (box != null)
                        box.Move(direction);
                }
            }
        }
    }

    private bool IsValidGridPosition(Vector3 position)
    {
        // Perform raycast to check for obstacles or other conditions
        RaycastHit hit;

        // Perform the raycast from current position to the new position
        if (Physics.Raycast(transform.position, position - transform.position, out hit, gridSize))
        {
            // Check if the raycast hits an obstacle
            if (hit.collider.CompareTag("Obstacle"))
            {
                if (position - targetPosition != Vector3.down)
                    FindObjectOfType<AudioManager>().Play("collide");

                return false;
            }

            // Check if raycast hits a box
            else if (hit.collider.CompareTag("Box"))
            {
                Box box = hit.collider.GetComponent<Box>();
                if (box != null)
                {
                    //check if the box can move
                    Vector3 blockTargetPosition = hit.collider.transform.position + ((position - transform.position) * gridSize);
                    return box.IsValidGridPosition(blockTargetPosition);
                }
            }
            // Check if raycast hits another player
            else if (hit.collider.CompareTag("Player"))
            {
                PlayerController player = hit.collider.GetComponent<PlayerController>();
                Vector3 newTargetPosition = hit.collider.transform.position + ((position - transform.position) * gridSize);
                // Check if other player can move
                if (player.IsValidGridPosition(newTargetPosition))
                {
                    player.Move((position - transform.position));
                    return true;
                }

                if (position - targetPosition != Vector3.down)
                    FindObjectOfType<AudioManager>().Play("collide");

                return false;

            }
        }

        return true;
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f))
        {
            return true;
        }
        return false;
    }


}


