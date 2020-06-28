using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private bool goDown;
    private void Awake()
    {
       // gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        goDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -8.2)
        {
            goDown = false;
        }
        if (transform.position.y > 8.2)
        {
            goDown = true;
        }
        if (goDown)
        {
            transform.Translate(new Vector3(0.0f, -1f * Time.deltaTime, 0.0f));
        }
        else
        {
            transform.Translate( new Vector3(0.0f, 1f * Time.deltaTime, 0.0f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 2f;
            Destroy(gameObject);
        }
        
    }
}
