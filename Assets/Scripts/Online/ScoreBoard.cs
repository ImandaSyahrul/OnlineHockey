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
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        network = GameObject.FindObjectOfType<Network>();
    }

    // Update is called once per frame
    void Update()
    {
        if (side == 1) 
            score = network.score2;
        else score = network.score1;
        text.text = score.ToString();
    }
}
