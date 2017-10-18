using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    // attributes
    private KeyCode left, right, forward, fire;
    private Vector3 velocity, acceleration, direction, rotateAcceleration, rotateVelocity;
    private float accelerationUnit, maxSpeed, rotateAccelerationUnit, rotateMaxSpeed, camWidth, camHeight;
    public GameObject bullet, spawnLocation;
    public Transform shotLocation;
    public List<GameObject> bullets;
    public int score, lives, shotTime, tempScore;
    private bool enabledBullet;
    public bool invulnerable;
    private int shotClock, invulClock;

    // Use this for initialization
    void Start()
    {
        // keys
        left = KeyCode.A;
        right = KeyCode.D;
        forward = KeyCode.W;
        fire = KeyCode.Space;

        // floats
        accelerationUnit = 0.0005f;
        maxSpeed = .1f;
        rotateAccelerationUnit = .3f;
        rotateMaxSpeed = 1f;
        camWidth = (Camera.main.orthographicSize * Screen.width / Screen.height);
        camHeight = Camera.main.orthographicSize;

        // vector 3s
        direction = gameObject.transform.up;
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);

        // lists
        bullets = new List<GameObject>(5);

        // ints
        score = 0;
        lives = 3;
        shotClock = 0;
        shotTime = 10;
        tempScore = 0;
        invulClock = 0;

        // bools
        enabled = true;
        invulnerable = false;
    }
	
	// Update is called once per frame
	void Update () {
        direction = transform.up;
        gameObject.transform.position += Thruster();
        gameObject.transform.Rotate(Rotator());

        Wrap();
        Fire();

        // invulerability checking
        if (!invulnerable)
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        if (invulnerable)
        {
            if (invulClock % 2 == 0)
            {
                if (gameObject.GetComponent<SpriteRenderer>().color.a == 1f)
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

                else
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }

            if (invulClock == 250)
            {
                invulClock = 0;
                invulnerable = false;
            }

            invulClock++;
        }
    }

    Vector3 Thruster()
    {
        // Increase or decrease the speed
        if (Input.GetKey(forward) || Input.GetKey(KeyCode.UpArrow))
            acceleration = direction * accelerationUnit;

        else
        {
            acceleration = new Vector3(0, 0, 0);
            velocity = velocity / 1.01f;
        }

        // increase the velocity
        velocity += acceleration;

        // clamp the velocity and make sure it cannot go backwards
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        return velocity;
    }
    
    Vector3 Rotator()
    {
        if (Input.GetKey(right) || Input.GetKey(KeyCode.RightArrow))
            rotateAcceleration = new Vector3(0, 0, -rotateAccelerationUnit);

        else if (Input.GetKey(left) || Input.GetKey(KeyCode.LeftArrow))
            rotateAcceleration = new Vector3(0, 0, rotateAccelerationUnit);

        else
        {
            rotateAcceleration = new Vector3(0, 0, 0);
            rotateVelocity /= 1.05f;
        }

        rotateVelocity += rotateAcceleration;

        rotateVelocity = Vector3.ClampMagnitude(rotateVelocity, rotateMaxSpeed);

        return rotateVelocity;
    }

    // to wrap the object back around the screen
    void Wrap()
    {
        // Checks if the object ever goes to one of these locations
        if (gameObject.transform.position.x > camWidth)
            gameObject.transform.position = new Vector3(-camWidth, gameObject.transform.position.y, gameObject.transform.position.z);

        if (gameObject.transform.position.y > camHeight)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -camHeight, gameObject.transform.position.z);

        if (gameObject.transform.position.x < -camWidth)
            gameObject.transform.position = new Vector3(camWidth, gameObject.transform.position.y, gameObject.transform.position.z);

        if (gameObject.transform.position.y < -camHeight)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, camHeight, gameObject.transform.position.z);
    }

    void Fire()
    {
        if (bullets.Count != bullets.Capacity && Input.GetKey(fire) && shotClock == 0)
        {
            bullets.Add(Instantiate(bullet, shotLocation.position, shotLocation.rotation));
            enabledBullet = false;
            // GetComponent<AudioSource>().Play();
        }

        for (int i = bullets.Count - 1; i > -1; i--)
            if (bullets[i] == null)
                bullets.RemoveAt(i);
        if (!enabledBullet)
        {
            shotClock++;
            if(shotClock == shotTime)
            {
                shotClock = 0;
                enabledBullet = true;
                // GetComponent<AudioSource>().Stop();
            }
        }
    }

    // when you hit an asteroid
    public void Reset()
    {
        lives--;
        invulnerable = true;
        velocity = new Vector3(0, 0, 0);

        if (lives == 0)
            GameOver();

        gameObject.transform.position = spawnLocation.transform.position;
    }

    // Restart the game
    void GameOver()
    {
        // write the scores
        gameObject.GetComponent<ScoreScript>().WriteFile(gameObject.GetComponent<ScoreScript>().prevFileLocation, score);

        if (score > gameObject.GetComponent<ScoreScript>().ReadFile(gameObject.GetComponent<ScoreScript>().highFileLocation))
            gameObject.GetComponent<ScoreScript>().WriteFile(gameObject.GetComponent<ScoreScript>().highFileLocation, score);

        SceneManager.LoadScene(0);
    }
}
