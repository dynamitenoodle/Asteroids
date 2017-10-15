using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    void OnGUI()
    {
        // make the font bigger and set the color
        var style = new GUIStyle("label");
        style.fontSize = 80;
        GUI.color = Color.black;

        // make the screen a certain height
        GUI.Label(new Rect(10, 25, Screen.width, Screen.height), "Coehl Gleckner's Asteroids", style);

        GUI.color = Color.blue;

        GUI.Label(new Rect(10, 150, Screen.width, Screen.height), "To start, Click anywhere", style);

        GUI.color = Color.black;

        if (gameObject.GetComponent<ScoreScript>().highScore != 0)
        {
            GUI.Label(new Rect(10, 280, Screen.width, Screen.height), "HighScore: " + gameObject.GetComponent<ScoreScript>().highScore, style);
        }

        if (gameObject.GetComponent<ScoreScript>().lastScore != 0)
        {
            GUI.Label(new Rect(10, 400, Screen.width, Screen.height), "Previous Score: " + gameObject.GetComponent<ScoreScript>().lastScore, style);
        }
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene("Asteroids");
    }
}
