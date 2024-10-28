using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private float delay = 1f;

    private Transform map;
    private GameObject[] players;
    private GameObject[] boxes;

    private GoalManager goalManager;


    private void Start()
    {
        map = GameObject.Find("Map").transform;
        players = GameObject.FindGameObjectsWithTag("Player");
        boxes = GameObject.FindGameObjectsWithTag("Box");
        goalManager = GameObject.Find("GoalManager").GetComponent<GoalManager>();


        HideEntities();
        HideMap();
        StartCoroutine(SpawnMapDelay());

    }
    private IEnumerator SpawnMapDelay()
    {
        foreach (Transform block in map)
        {
            yield return new WaitForSeconds(delay);
            block.gameObject.SetActive(true);

        }
        yield return new WaitForSeconds(delay);

        StartCoroutine(SpawnBoxDelay());

    }
    private IEnumerator SpawnBoxDelay()
    {
        foreach (GameObject x in boxes)
        {
            yield return new WaitForSeconds(delay);
            x.SetActive(true);
        }
        yield return new WaitForSeconds(delay);

        StartCoroutine(SpawnPlayerDelay());

    }
    private IEnumerator SpawnPlayerDelay()
    {
        foreach (GameObject x in players)
        {
            yield return new WaitForSeconds(delay);
            x.SetActive(true);
        }
        yield return new WaitForSeconds(delay);

        //DO a thing
        goalManager.SetListActive(goalManager.GetGoals(), true);
    }

    private void HideMap()
    {
        foreach (Transform block in map)
        {
            block.gameObject.SetActive(false);
        }

    }
    private void HideEntities()
    {

        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }
        foreach (GameObject box in boxes)
        {
            box.SetActive(false);
        }

    }
}
