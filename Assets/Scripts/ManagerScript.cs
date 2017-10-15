using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{

    // attributes
    public List<GameObject> asteroidPrefabs;
    public GameObject player, livesLocation;
    public List<GameObject> bigAsteroids;
    private float x, y;
    public Texture playerSprite;
    private int score, lives, tempCount;
    private GameObject asteroid;
    private List<Sprite> playerLives;

    // Use this for initialization
    void Start()
    {
        // lists
        bigAsteroids = new List<GameObject>(10);
        for (int i = 0; i < 5; i++)
        {
            GenAsteroid();
            GenNum();
            GameObject temp = Instantiate(asteroid, new Vector2(x, y), asteroid.transform.rotation);
            temp.GetComponent<AsteroidScript>().player = player;
            bigAsteroids.Add(temp);
        }

        // ints
        score = player.GetComponent<PlayerScript>().score;
        lives = player.GetComponent<PlayerScript>().lives;
    }

    // Update is called once per frame
    void Update()
    {
        // set up the asteroids when they get removed
        for (int i = bigAsteroids.Count - 1; i > -1; i--)
        {
            if (bigAsteroids[i] == null)
            {
                bigAsteroids.RemoveAt(i);
                GenAsteroid();
                GenNum();
                GameObject temp = Instantiate(asteroid, new Vector2(x, y), asteroid.transform.rotation);
                temp.GetComponent<AsteroidScript>().player = player;
                bigAsteroids.Add(temp);
            }
        }

        // ints
        score = player.GetComponent<PlayerScript>().score;
        lives = player.GetComponent<PlayerScript>().lives;

        // increment the number of asteroids based on the score
        if (player.GetComponent<PlayerScript>().tempScore % 100 == 10)
        {
            player.GetComponent<PlayerScript>().tempScore = 0;

            // if their is still room that is
            if (bigAsteroids.Count != bigAsteroids.Capacity)
            {
                GenAsteroid();
                GenNum();
                GameObject temp = Instantiate(asteroid, new Vector2(x, y), asteroid.transform.rotation);
                temp.GetComponent<AsteroidScript>().player = player;
                bigAsteroids.Add(temp);
            }
        }
    }

    void GenNum()
    {
        // make the random positon
        int rng = Random.Range(1, 3);
        if (rng == 1)
            x = Random.Range(-20f, 0f);
        if (rng == 2)
            x = Random.Range(0f, 20f);

        rng = Random.Range(1, 3);
        if (rng == 1)
            y = Random.Range(-20f, 0f);
        if (rng == 2)
            y = Random.Range(0f, 20f);
    }

    void OnGUI()
    {
        // make the font bigger and set the color
        var style = new GUIStyle("label");
        style.fontSize = 25;

        // draw the score and lives
        GUI.Label(new Rect(10, 10, 200, 50), "Score: " + score, style);

        // lives
        for (int i = 0; i < lives; i++)
        {
            GUI.Label(new Rect((25 * i) + 10, 50, 25, 25), playerSprite);
        }
    }

    void GenAsteroid()
    {
        // choose the asteroid
        int x = Random.Range(0, 3);
        asteroid = asteroidPrefabs[x];
    }
}
