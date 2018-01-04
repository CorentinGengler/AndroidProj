using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls  : MonoBehaviour
{
    public bool m_isLeftWall =false;
    public float m_rebound = 200;
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
        if (collision.transform.tag == "Player")
        {
            if(m_timer==0f)//we don't want the force applied multiple times in case the colision last more than 1 frame so we use a timer;
            {
                m_timer += Time.deltaTime;
                if (m_isLeftWall)
                {
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(m_rebound, 0f, 0f));
                }
                else
                {
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-m_rebound, 0f, 0f));
                }
            }
            
        }
    }
    private float m_timer = 0f;
    private float m_timeBeforeNextRebound = 0.5f;
}
