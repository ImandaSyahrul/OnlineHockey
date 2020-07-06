using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Score : NetworkBehaviour
{
    [SyncVar] public int score1;
    [SyncVar] public int score2;
    // Start is called before the first frame update
    void Start()
    {
        score1 = score2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScoreLeft()
    {
        if (score1 < 7)
        {
            score1++;
        }


    }


    public void SetScoreRight()
    {
        if (score1 < 7)
        {
            score2++;
        }

    }
    public void InitScore()
    {
        score1 = 0;
        score2 = 0;
    }

    public void ResetScore()
    {
        score1 = 0;
        score2 = 0;
    }

}
