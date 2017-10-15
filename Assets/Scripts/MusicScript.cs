using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    // attributes
    public AudioSource music;
    private bool started;

	// Use this for initialization
	void Start () {
        // dont destroy the music manager
        DontDestroyOnLoad(gameObject);

        // bools
        started = false;

        // audiosource
        music.volume = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

        // music enabling
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!music.isPlaying && !started)
            {
                music.Play();
                started = true;
            }

            else if (music.isPlaying)
                music.Pause();

            else if (!music.isPlaying && started)
                music.UnPause();
        }

        // turning sound down and up
        if (Input.GetKeyDown(KeyCode.Period))
            music.volume += 0.05f;

        if (Input.GetKeyDown(KeyCode.Comma))
            music.volume -= 0.05f;
    }
}
