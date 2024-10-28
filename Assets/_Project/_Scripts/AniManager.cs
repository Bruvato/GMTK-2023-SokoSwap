using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniManager : MonoBehaviour
{
    [SerializeField] private float delay = 1f;

    [SerializeField] private GameManager gameManager;

    private Transform map;
    private GameObject[] players;
    private GameObject[] boxes;

    public bool isAnimating;
    private void Start()
    {
        map = GameObject.Find("Map").transform;
        players = GameObject.FindGameObjectsWithTag("Player");
        boxes = GameObject.FindGameObjectsWithTag("Box");

        isAnimating = true;


    }

    private void AnimateLevelStart()
    {

        foreach (GameObject p in players)
        {
            p.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("goalReached");
        }

    }

    public void AnimateLevelComplete()
    {
        Start();
        StartCoroutine(AnimateMap());
    }

    public void AnimatePlayers(string ani)
    {
        Start();
        foreach (GameObject p in players)
        {
            p.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger(ani);
        }
    }

    public void AnimateBoxes(string ani)
    {
        Start();
        foreach (GameObject b in boxes)
        {
            b.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger(ani);
        }
    }

    private IEnumerator AnimateMap()
    {
        foreach (Transform block in map)
        {
            yield return new WaitForSeconds(delay);
            block.GetChild(0).gameObject.GetComponent<Animator>().SetBool("levelComplete", true);
        }
        yield return new WaitForSeconds(1);
        gameManager.LoadNextLevel();
    }

    public void SetAnimationStatus(bool status)
    {
        isAnimating = status;

    }
}
