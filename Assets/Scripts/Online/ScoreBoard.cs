using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ScoreBoard : NetworkBehaviour
{
    [SerializeField] private int side;
    [SyncVar] private int score;
    Text text;
    Network network;
    public Score scoreSystem;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        scoreSystem = GameObject.FindObjectOfType<Score>();
        network = GameObject.FindObjectOfType<Network>();
    }

    // Update is called once per frame
    void Update()
    {
        if (side == 1) 
            score = scoreSystem.score2;
        else score = scoreSystem.score1;
        text.text = score.ToString();
    }
}
