using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public GameObject redMarker, yellowMarker, greenMarker;
    public GameObject redWin, yellowWin, greenWin;
    public AudioClip victoryClip, ambientClip;
    public float winAudioMultiplier = 2.0f;
    private GameObject[] markers, wins;
    private AudioSource audi;
    private float high = 1.0f;
    private float low;

    // Start is called before the first frame update
    void Start()
    {
        markers = new GameObject[] { redMarker, yellowMarker, greenMarker };
        wins = new GameObject[] { redWin, yellowWin, greenWin };
        audi = GetComponent<AudioSource>();
        low = 1.0f / winAudioMultiplier;
        audi.volume = low;
        audi.Play();
    }

    public void TurnChange(int playerID) {
        markers[playerID-1].SetActive(true);
        markers[(playerID ) % 3].SetActive(false);
        markers[(playerID + 1) % 3].SetActive(false);
    }

    public void WinSequence(int playerID) {
        wins[playerID-1].SetActive(true);
        audi.clip = victoryClip;
        audi.loop = false;
        audi.volume = high;
        audi.Play();
        TurnChange(playerID);
    }

    public void UndoWin() {
        foreach (GameObject gb in wins) {
            gb.SetActive(false);
            audi.clip = ambientClip;
            audi.loop = true;
            //Debug.Log("Volume was " + audi.volume + " now it will become " + audi.volume / winAudioMultiplier);
            audi.volume = low;
            audi.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
