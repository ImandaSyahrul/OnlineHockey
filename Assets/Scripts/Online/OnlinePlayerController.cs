using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Mirror;

public class OnlinePlayerController : NetworkBehaviour
{
    public float speed = 30;
    private Rigidbody2D rigidbody2d;
    [SyncVar(hook = nameof(SetColor))] 
    Color playerColor = Color.white;
    public Joystick joystick = null;

    // Cache it here and Destroy it in OnDestroy to prevent a memory leak.
    Material cachedMaterial;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Light2D>().color = playerColor;
        rigidbody2d = GetComponent<Rigidbody2D>();
#if UNITY_EDITOR
        joystick = FindObjectOfType<Joystick>();
#endif
#if UNITY_ANDROID
        joystick = FindObjectOfType<Joystick>();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // need to use FixedUpdate for rigidbody
    void FixedUpdate()
    {
        // only let the local player control the racket.
        // don't control other player's rackets
        if (isLocalPlayer)
        {
            rigidbody2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
            if(joystick!= null) rigidbody2d.velocity = new Vector2(joystick.Horizontal, joystick.Vertical) * speed * Time.fixedDeltaTime;
            else rigidbody2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
        }
    }

    

    public override void OnStartServer()
    {
        base.OnStartServer();
        playerColor = Random.ColorHSV(0f, 1f, 1f, 1f);
        GetComponent<Light2D>().color = playerColor;
    }

    void SetColor(Color oldColor, Color newColor)
    {
        if (cachedMaterial == null)
            cachedMaterial = GetComponent<Renderer>().material;

        cachedMaterial.color = newColor;
    }

    void OnDestroy()
    {
        Destroy(cachedMaterial);
    }
}
