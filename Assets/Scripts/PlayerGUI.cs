using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    void Awake()
    {
#if UNITY_EDITOR
        gameObject.SetActive(true);
#endif
#if UNITY_ANDROID
        gameObject.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN	
        Destroy(gameObject);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
