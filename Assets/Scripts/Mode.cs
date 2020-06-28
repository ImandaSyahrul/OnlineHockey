using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mode : MonoBehaviour
{
	public GameObject player2;
	public GameObject ball;
	public GameObject screenOpponent;
	//public GameObject powerUp;
	public GameObject gameManager;
	PlayerController playerControl;
    // Start is called before the first frame update
    void Start()
    {
		ball.SetActive(false);
		Time.timeScale = 1.0f;
		playerControl = player2.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SelectMode(bool bot)
	{
		playerControl.isBot = bot;
		InitiateGame();
	}

	void InitiateGame()
	{
		ball.SetActive(true);
		screenOpponent.SetActive(false);
		gameManager.SetActive(true);
		//Destroy(powerUp);
	}

}
