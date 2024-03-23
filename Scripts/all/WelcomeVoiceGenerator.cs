using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WelcomeVoiceGenerator : MonoBehaviour
{
    AudioSource audio;
    Animator animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audio.isPlaying)
        {
            animator.SetBool("Talk", false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            audio.Play();
            animator.SetBool("Talk",true);
        }
    }
}
