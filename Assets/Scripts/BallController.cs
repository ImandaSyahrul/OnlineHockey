using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	private Rigidbody2D rb2d;

	// Timer for powerup
	private float currTime; // waktu saat ini
	[SerializeField] private float strtTime = 10f; // waktu dimana akan muncul power up
	[SerializeField] public GameObject powerUp;
	private GameObject powerObject;
	private bool assigned;

	void GoBall()
	{
		float rand = Random.Range(0, 2);
		if (rand < 1)
		{
			rb2d.AddForce(new Vector2(120, -15));
		}
		else
		{
			rb2d.AddForce(new Vector2(-120, -15));
		}
		currTime = strtTime;
	}

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Invoke("GoBall", 2);
		//powerUp.SetActive(false);
	}

	private void Update()
	{
		if (currTime > 0) currTime -= 1 * Time.deltaTime;
		Debug.Log(currTime);
		if (currTime <= 0 & !assigned)
		{
			powerObject = Instantiate(powerUp, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
			assigned = true;
		}
	}

	void ResetBall()
	{
		rb2d.velocity = new Vector2(0, 0);
		transform.position = Vector2.zero;
		currTime = strtTime;
		Destroy(powerObject.gameObject);
		assigned = false;
	}

	void RestartGame()
	{
		ResetBall();
		Invoke("GoBall", 1);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.CompareTag("Player"))
		{
			if (!coll.gameObject.GetComponent<PlayerController>().Activated)
			{
				coll.gameObject.GetComponent<PlayerController>().CurrBar += 10;
			}
			
			Vector2 vel;
			vel.x = rb2d.velocity.x;
			vel.y = (rb2d.velocity.y / 2.0f) + (coll.collider.attachedRigidbody.velocity.y / 3.0f);
			rb2d.velocity = vel;
			
		}
	}


}
