using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    private AniManager aniManager;

    private void Start()
    {
        aniManager = GameObject.Find("AniManager").GetComponent<AniManager>();
    }
    public void DoThing()
    {
        Debug.Log("did the thing!");
    }

    public void StopAnimating()
    {
        aniManager.SetAnimationStatus(false);
    }

    public void PlaySound(string s)
    {
        FindObjectOfType<AudioManager>().Play(s);

    }

}
