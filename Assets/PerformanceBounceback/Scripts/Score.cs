using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

   // public GameManager gameManager;
    public Text text;
    private int previousScore;


    // Use this for initialization
    void Start()
    {
        //To set 0
        text.text = "Score: " + GameManager.instance.score.ToString();
    }

    // Update is called once per frame
    void Update () {
        // if (gameManager.score != previousScore)
        if(GameManager.instance.score != previousScore)
        {
             text.text = "Score: " + GameManager.instance.score.ToString();
             previousScore = GameManager.instance.score;

           //  text.text = "Score: " + gameManager.score.ToString();
           //  previousScore = gameManager.score;
        }

    }
}
