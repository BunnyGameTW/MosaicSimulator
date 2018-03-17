using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float speed;
    public AudioClip bii;
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.clip = bii;

    }

    // Update is called once per frame
    void Update () {
        control();

    }
    void control()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3( speed * Time.deltaTime,0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            source.Play();
            
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            source.Stop();
        }
    }
}
