using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


[AddComponentMenu("")]
public class Network : NetworkManager
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    public Transform left2RacketSpawn;
    public Transform right2RacketSpawn;
    public GameObject ballPrefab;
    public GameObject scorePrefab;
    public Score score;
    GameObject ball;
    GameObject scoreBoard;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = null;
        switch (numPlayers)
        {
            case 0: start = leftRacketSpawn; break;
            case 1: start = rightRacketSpawn; break;
            case 2: start = left2RacketSpawn; break;
            case 3: start = right2RacketSpawn; break;
            default: break;
        }
            
            
        
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        // spawn ball and start game if minimal 2 players joined
        if (numPlayers == 2)
        {
            score.InitScore();
            scoreBoard = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Score Board"));
            NetworkServer.Spawn(scoreBoard);
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "OnlineBall"));
            NetworkServer.Spawn(ball);
        }
    }



    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        if (ball != null)
            NetworkServer.Destroy(ball);
        // destroy score board
        if (scoreBoard != null)
            NetworkServer.Destroy(scoreBoard);
        score.ResetScore();
        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }

    

}

