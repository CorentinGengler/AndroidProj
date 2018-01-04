using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls  : MonoBehaviour
{

    public float m_rebound = 200;

    void Awake()
    {
        AudioSource[] sounds = FindObjectOfType<Camera>().GetComponents<AudioSource>();
        foreach (AudioSource sound in sounds)
        {
            if(sound.clip.name == "B1")
            {
                m_bouncingSound1 = sound;
            }
            if (sound.clip.name == "B2")
            {
                m_bouncingSound2 = sound;
            }
        }
    }
    void Update()
    {
        if(m_timer!=0)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_timeBeforeNextRebound)
            {
                m_timer = 0;
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Shoot")
        {
            DestroyObject(collision.gameObject);
        }
        if (collision.transform.tag == "Player" )
        {
            if(m_timer==0f)//we don't want the force applied multiple times in case the colision last more than 1 frame so we use a timer;
            {
                
                m_bouncingSound1.Play();
                m_timer += Time.deltaTime;
            }
            
        }
        if(collision.transform.tag == "Asteroid")
        {
            if (m_timer == 0f)//we don't want the force applied multiple times in case the colision last more than 1 frame so we use a timer;
            {

                m_bouncingSound2.Play();
                m_timer += Time.deltaTime;
            }

        }
    }
    private float m_timer = 0f;
    private float m_timeBeforeNextRebound = 0.5f;
    private AudioSource m_bouncingSound1;
    private AudioSource m_bouncingSound2;

}
