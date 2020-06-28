using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Wall : MonoBehaviour
{
	private Light2D light2d;
	// Start is called before the first frame update
	void Start()
    {
		light2d = GetComponent<Light2D>();
		light2d.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Jika wall tersentuh oleh "Ball":
		if (collision.gameObject.CompareTag("Ball")) light2d.enabled = true;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ball"))
		{
			WaitForSeconds wait = new WaitForSeconds(1f);
			light2d.enabled = false;
		}
	}


}
