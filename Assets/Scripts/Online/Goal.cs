using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goal : NetworkBehaviour
{
    public Network network;
    [SerializeField] int side;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (side == 1) network.SetScoreLeft();
            else network.SetScoreRight();
            collision.gameObject.GetComponent<OnlineBallController>().ResetBall();
            
        }
    }
}
