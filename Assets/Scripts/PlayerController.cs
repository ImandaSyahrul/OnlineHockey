using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//Ability
	private float minBar;
	private float maxBar;
	private float currBar;
	private bool activated;

	public float CurrBar { get => currBar; set => currBar = value; }
	public bool Activated { get => activated; set => activated = value; }
	public float MaxBar { get => maxBar; set => maxBar = value; }
	public float MinBar { get => minBar; set => minBar = value; }

	// Tombol untuk menggerakkan ke atas
	public KeyCode upButton = KeyCode.W;

	// Tombol untuk menggerakkan ke bawah
	public KeyCode downButton = KeyCode.S;

	// Tombol untuk mengaktifkan ability
	public KeyCode abilityButton = KeyCode.E;

	// Kecepatan gerak
	public float speed = 10.0f;

	// Batas atas dan bawah game scene (Batas bawah menggunakan minus (-))
	public float yBoundary = 9.0f;

	// Rigidbody 2D raket ini
	private Rigidbody2D rigidBody2D;

	// Variabel untuk pilih Bot atau Player lain
	public bool isBot;

	public GameObject ball;

	// Skor pemain
	private int score;

	// Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
	private ContactPoint2D lastContactPoint;

	// Titik asal lintasan bola saat ini
	private Vector2 trajectoryOrigin;

	// Start is called before the first frame update
	void Start()
    {
		rigidBody2D = GetComponent<Rigidbody2D>();
		if(this.gameObject.name == "Player2Racket")
		{
			upButton = KeyCode.UpArrow;
			downButton = KeyCode.DownArrow;
			abilityButton = KeyCode.Space;
		}

		trajectoryOrigin = transform.position;
		MinBar = 0;
		MaxBar = 100;
		CurrBar = MinBar;
		Activated = false;
	}

    // Update is called once per frame
    void Update()
    {
		// Dapatkan kecepatan raket sekarang.
		Vector2 velocity = rigidBody2D.velocity;
		if (CurrBar > MaxBar) CurrBar = MaxBar;
		if (Activated)
		{
			gameObject.transform.localScale = new Vector2( 1f,2f);
			CurrBar -= 10 * Time.deltaTime;
			if (CurrBar < 0)
			{
				Activated = false;
			}
		}
		else
		{
			gameObject.transform.localScale = new Vector2(1f, 1f);
		}

		//Jika bermain dengan player lain
		if (!isBot)
		{
			// Jika pemain menekan tombol ke atas, beri kecepatan positif ke komponen y (ke atas).
			if (Input.GetKey(upButton))
			{
				velocity.y = speed;
			}

			// Jika pemain menekan tombol ke bawah, beri kecepatan negatif ke komponen y (ke bawah).
			else if (Input.GetKey(downButton))
			{
				velocity.y = -speed;
			}

			// Jika pemain tidak menekan tombol apa-apa, kecepatannya nol.
			else
			{
				velocity.y = 0.0f;
			}

			if (Input.GetKeyUp(abilityButton))
			{
				if (CurrBar == 100 & !Activated)
				{
					Activated = true;
				}
			}
		}
		//Jika player main dengan bot(Bot masih sederhana)
		else
		{
			float botSpeed = speed;
			float ballPosX = GameObject.Find("Ball").gameObject.transform.position.x;
			float ballPosY = GameObject.Find("Ball").gameObject.transform.position.y;
			if (ballPosX > -6.75 && ballPosX < transform.position.x)
			{
				if (ball.transform.position.x < -5)
				{
					botSpeed = speed - 7;
				}
				else botSpeed = speed;
				if (ball.transform.position.y > this.gameObject.transform.position.y + 1f)
				{
					velocity.y = botSpeed;
				}
				else if (ball.transform.position.y < this.gameObject.transform.position.y - 1f)
				{
					velocity.y = -botSpeed;
				}
				else
				{
					velocity.y = 0.0f;
				}
			}

			if (currBar == 100)
			{
				if(ball.transform.position.x > 5 && Mathf.Abs(ballPosY-transform.position.y)>9)
				{
					Activated = true;
				}
			}

		}
		

		// Masukkan kembali kecepatannya ke rigidBody2D.
		rigidBody2D.velocity = velocity;

		// Dapatkan posisi raket sekarang.
		Vector3 position = transform.position;

		// Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas tersebut.
		if (position.y > yBoundary)
		{
			position.y = yBoundary;
		}

		// Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.
		if (position.y < -yBoundary)
		{
			position.y = -yBoundary;
		}

		// Masukkan kembali posisinya ke transform.
		transform.position = position;
	}

	// Menaikkan skor sebanyak 1 poin
	public void IncrementScore()
	{
		score++;
	}

	// Mengembalikan skor menjadi 0
	public void ResetScore()
	{
		score = 0;
	}

	// Mendapatkan nilai skor
	public int Score
	{
		get { return score; }
	}


	// Untuk mengakses informasi titik kontak dari kelas lain
	public ContactPoint2D LastContactPoint
	{
		get { return lastContactPoint; }
	}

	// Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name.Equals("Ball"))
		{
			lastContactPoint = collision.GetContact(0);
		}
	}

	// Untuk mengakses informasi titik asal lintasan
	public Vector2 TrajectoryOrigin
	{
		get { return trajectoryOrigin; }
	}


	// Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
	private void OnCollisionExit2D(Collision2D collision)
	{
		trajectoryOrigin = transform.position;
	}
}
