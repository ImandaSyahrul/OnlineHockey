using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// Pemain 1
	public PlayerController player1; // script for racket
	private Rigidbody2D player1Rigidbody;
	

	// Pemain 2
	public PlayerController player2; // script for racket
	private Rigidbody2D player2Rigidbody;

	// Bola
	public BallController ball; // script for ball object
	private Rigidbody2D ballRigidbody;
	private CircleCollider2D ballCollider;

	


	//Win condition
	[SerializeField] private AudioClip win;
	[SerializeField] private AudioClip lose;
	public UnityEvent onWin;
	public UnityEvent onLose;
	public UnityEvent onEndMatch;
	private bool onEndGame;



	// Skor maksimal
	[SerializeField] public int maxScore;


	//GUI
	[SerializeField] public Slider bar1;
	[SerializeField] public Slider bar2;
	
	

	// Start is called before the first frame update
	void Start()
    {
		player1Rigidbody = player1.GetComponent<Rigidbody2D>();
		player2Rigidbody = player2.GetComponent<Rigidbody2D>();
		ballRigidbody = ball.GetComponent<Rigidbody2D>();
		ballCollider = ball.GetComponent<CircleCollider2D>();
		bar2.maxValue = player2.MaxBar;
		bar1.maxValue = player1.MaxBar;
		onEndGame = false;
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("Enter")) Application.Quit(); // Quit game
		bar1.value = player1.CurrBar;
		bar2.value = player2.CurrBar;
		if (player1.Score == maxScore)
		{
			if(!onEndGame) Win();
			onEndGame = true;
		}
		if (player2.Score == maxScore)
		{
			if (!onEndGame) Lose();
			onEndGame = true;
		}
	}
	// Untuk menampilkan GUI
	void OnGUI()
	{
		// Tampilkan skor pemain 1 di kiri atas dan pemain 2 di kanan atas
		GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
		GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

		// Tombol restart untuk memulai game dari awal
		if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
		{
			// Ketika tombol restart ditekan, reset skor kedua pemain...
			player1.ResetScore();
			player2.ResetScore();

			// ...dan restart game.
			ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
			
			onEndGame = false;
		}

		// Jika pemain 1 menang (mencapai skor maksimal), ...
		if (player1.Score == maxScore)
		{
			// ...tampilkan teks "PLAYER ONE WINS" di bagian kiri layar...
			GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
			

			// ...dan kembalikan bola ke tengah.
			ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
			//GetComponent<AudioSource>().PlayOneShot(win);

		}
		// Sebaliknya, jika pemain 2 menang (mencapai skor maksimal), ...
		else if (player2.Score == maxScore)
		{
			// ...tampilkan teks "PLAYER TWO WINS" di bagian kanan layar... 
			GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
			

			// ...dan kembalikan bola ke tengah.
			ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
			


		}
	}

	void Lose()
	{
		onLose.Invoke();
	}

	void Win()
	{
		onWin.Invoke();
	}
}
