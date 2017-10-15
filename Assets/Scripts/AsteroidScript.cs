using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    // attributes
    private float camWidth, camHeight, speed, extraDistance;
    public GameObject player;
    public List<GameObject> bullets;
    public Transform spawn1, spawn2;
    private Vector2 direction;
    public float scaleSize;

    // Use this for initialization
    void Start()
    {
        // floats
        camWidth = (Camera.main.orthographicSize * Screen.width / Screen.height);
        camHeight = Camera.main.orthographicSize;
        speed = 0.02f;
        extraDistance = 1.5f;

        // vector 2s
        direction = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    // Update is called once per frame
    void Update()
    {
        bullets = player.GetComponent<PlayerScript>().bullets;

        // check the collisions
        CollisionCheck(player);

        foreach (GameObject g in bullets)
            CollisionCheck(g);

        Move();
        Wrap();
    }

    // random movement
    void Move()
    {
        transform.position += (Vector3)direction;
    }

    // to wrap the object back around the screen
    void Wrap()
    {
        // Checks if the object ever goes to one of these locations
        if (gameObject.transform.position.x > camWidth + extraDistance)
            gameObject.transform.position = new Vector3(-camWidth, gameObject.transform.position.y, gameObject.transform.position.z);

        if (gameObject.transform.position.y > camHeight + extraDistance)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -camHeight, gameObject.transform.position.z);

        if (gameObject.transform.position.x < -camWidth - extraDistance)
            gameObject.transform.position = new Vector3(camWidth, gameObject.transform.position.y, gameObject.transform.position.z);

        if (gameObject.transform.position.y < -camHeight - extraDistance)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, camHeight, gameObject.transform.position.z);
    }

    void CollisionCheck(GameObject obj)
    {
        if (Vector2.Distance(transform.position, obj.transform.position) < scaleSize * transform.localScale.x && obj.tag != "Player")
        {
            // destroy the bullet
            if (obj.tag != "Player")
                Destroy(obj);

            // if still not puny
            if (transform.localScale.x > 0.5f && scaleSize == 1.8f || transform.localScale.x > 0.25f && scaleSize == 3.4f)
            {
                // make the new asteroid
                GameObject newAsteroid = gameObject;

                // set scale
                newAsteroid.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2);
                newAsteroid.GetComponent<AsteroidScript>().scaleSize = scaleSize;

                // create
                Instantiate(newAsteroid, spawn1.position, spawn1.rotation);
                Instantiate(newAsteroid, spawn2.position, spawn2.rotation);
            }

            // boom
            Destroy(gameObject);

            // player score increase
            player.GetComponent<PlayerScript>().score += 10;
            player.GetComponent<PlayerScript>().tempScore += 10;
        }

        else
        {
            if (Vector2.Distance(transform.position, obj.transform.position) < scaleSize * transform.localScale.x && !player.GetComponent<PlayerScript>().invulnerable)
                player.GetComponent<PlayerScript>().Reset();
        }
    }
}
