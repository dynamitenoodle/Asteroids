using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    // attributes
    private float speed, camWidth, camHeight, distance;

    // Use this for initialization
    void Start () {
        // floats
        speed = .5f;
        camWidth = (Camera.main.orthographicSize * Screen.width / Screen.height);
        camHeight = Camera.main.orthographicSize;
        distance = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        // update position
        gameObject.transform.position += transform.up * speed;

        // too far go away
        if (gameObject.transform.position.x > camWidth + distance || gameObject.transform.position.x < -camWidth - distance || gameObject.transform.position.y > camHeight + distance || gameObject.transform.position.y < -camHeight - distance)
            Destroy(gameObject);
	}
}
