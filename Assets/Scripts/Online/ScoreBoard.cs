using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ScoreBoard : NetworkBehaviour
{
    [SyncVar] int score1;
    [SyncVar] int score2;
    public Text board1;
    public Text board2;
    Network network;
    public Score scoreSystem;
    // Start is called before the first frame update
    void Start()
    {
        scoreSystem = GameObject.FindObjectOfType<Score>();
        network = GameObject.FindObjectOfType<Network>();
    }

    // Update is called once per frame
    void Update()
    {
        score1 = scoreSystem.score1;
        score2 = scoreSystem.score2;
        board1.text = score2.ToString();
        board2.text = score1.ToString();
    }
}
