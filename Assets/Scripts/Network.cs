using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


[AddComponentMenu("")]
public class Network : NetworkManager
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    public Transform left2RacketSpawn;
    public Transform right2RacketSpawn;
    public GameObject ballPrefab;
    public int score1;
    public int score2;
    GameObject ball;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = null;
        switch (numPlayers)
        {
            case 0: start = leftRacketSpawn; break;
            case 1: start = rightRacketSpawn; break;
            case 2: start = left2RacketSpawn; break;
            case 4: start = right2RacketSpawn; break;
            default: break;
        }
            
            
        
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        // spawn ball if 4 players joined
        if (numPlayers == 2)
        {
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "OnlineBall"));
            NetworkServer.Spawn(ball);
            score1 = 0;
            score2 = 0;
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        if (ball != null)
            NetworkServer.Destroy(ball);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }

    public void SetScoreLeft()
    {
        score2++;
    }

    public void SetScoreRight()
    {
        score1++;
    }

}

