using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private GameObject[] goals;
    [SerializeField] private GameObject[] finalGoals;
    private bool firstPhase = true;

    [SerializeField] private GameObject boxPrefab;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AniManager aniManager;

    [SerializeField] private Material blue;
    [SerializeField] private Material red;

    private bool soundPlayed = false;



    private void Start()
    {
        SetListActive(goals, false);
        SetListActive(finalGoals, false);
    }
    private void Update()
    {
        if (AllPressed() && firstPhase && !player.isMoving)
        {
            ReverseRoles();
        }
        else if (FinalPressed() && !firstPhase && !player.isMoving)
        {
            Debug.Log("YOU WIN");
            SetListActive(finalGoals, false);

            if (!soundPlayed) //does this once instead of every frame in update
            {
                FindObjectOfType<AudioManager>().Play("clink");
                soundPlayed = true;

                aniManager.AnimateBoxes("levelComplete");
                aniManager.AnimatePlayers("levelComplete");
                aniManager.Invoke("AnimateLevelComplete", 1.5f);

            }


            // gameManager.LoadNextLevel();
        }
    }

    private bool AllPressed()
    {
        foreach (GameObject goal in goals)
        {
            if (!goal.GetComponent<Goal>().isActivated)
            {
                return false;
            }
        }
        return true;

    }
    private bool FinalPressed()
    {
        foreach (GameObject goal in finalGoals)
        {
            if (!goal.GetComponent<Goal>().isActivated)
            {
                return false;
            }
        }
        return true;
    }

    private void ReverseRoles()
    {
        firstPhase = false;

        SetListActive(goals, false);
        SetListActive(finalGoals, true);
        Debug.Log("REVERSED!");

        FindObjectOfType<AudioManager>().Play("clink");

        aniManager.AnimateBoxes("levelComplete");
        aniManager.AnimatePlayers("levelComplete");

        Invoke("Swap", 1.5f);
    }


    private void Swap()
    {

        // Get all game objects with the tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Get all game objects with the tag "Box"
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        // Iterate over all player objects
        foreach (GameObject player in players)
        {
            // Get the position and rotation of the player
            Vector3 playerPosition = player.transform.position;
            Quaternion playerRotation = player.transform.rotation;

            // Destroy the player object
            Destroy(player);

            // Instantiate a box object at the same position and rotation as the player
            GameObject box = Instantiate(boxPrefab, playerPosition, playerRotation);
            box.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = blue;
        }

        // Iterate over all box objects
        foreach (GameObject box in boxes)
        {
            // Get the position and rotation of the box
            Vector3 boxPosition = box.transform.position;
            Quaternion boxRotation = box.transform.rotation;

            // Destroy the box object
            Destroy(box);

            // Instantiate a player object at the same position and rotation as the box
            GameObject player = Instantiate(playerPrefab, boxPosition, boxRotation);
            player.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = red;

        }
    }

    public void SetListActive(GameObject[] list, bool status)
    {
        foreach (GameObject x in list)
        {
            x.SetActive(status);
        }
    }

    public GameObject[] GetGoals()
    {
        return goals;
    }

}
