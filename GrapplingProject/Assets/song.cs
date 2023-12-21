using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Song : MonoBehaviour
{

    public GameObject player;
    public bool playing = false;
    public float distDivide = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist =  Mathf.Abs((player.transform.position.z - this.transform.position.z)/(this.GetComponent<BoxCollider>().size.z/distDivide));
        this.GetComponent<AudioSource>().volume = Mathf.SmoothStep(75, 0, dist);
        if (dist < 1 && !playing)
        {
            this.GetComponent<AudioSource>().Play();
            playing = true;
        }
    }
}
