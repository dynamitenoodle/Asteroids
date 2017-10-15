using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ScoreScript : MonoBehaviour {
    // I followed https://www.youtube.com/watch?v=9H7aa3g3TEI to create this

    // attributes
    public string highFileLocation, prevFileLocation;
    public int highScore, lastScore;

	// Use this for initialization
	void Start () {
        highFileLocation = Application.dataPath + "/highScore.txt";
        prevFileLocation = Application.dataPath + "/prevScore.txt";

        lastScore = ReadFile(prevFileLocation);
        highScore = ReadFile(highFileLocation);
    }	

    public void WriteFile(string fileName, int score)
    {
        FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

        if (fs.CanWrite)
        {
            byte[] buffer = Encoding.ASCII.GetBytes("" + score);
            fs.Write(buffer, 0, buffer.Length);
        }

        fs.Flush();
        fs.Close();
    }

    public int ReadFile(string fileName)
    {
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        int score = 0;

        if (fs.CanRead)
        {
            byte[] buffer = new byte[fs.Length];
            int bytesread = fs.Read(buffer, 0, buffer.Length);

            int.TryParse((Encoding.ASCII.GetString(buffer, 0, bytesread)), out score);
        }

        fs.Close();

        return score;
    }
}
