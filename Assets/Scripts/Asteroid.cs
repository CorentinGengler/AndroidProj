using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid  : MonoBehaviour
{

    #region Public 

    #endregion


    #region Public Void

    #endregion


    #region System

    void Start () 
    {
        transform.localScale *= Random.Range(0.2f, 1f);
        body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(0f, -Random.Range(0.2f, 1f), 0f);
    }
    void Awake () 
    {
        m_debugText = FindObjectOfType<Canvas>().GetComponent<Text>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Shoot")
        {
            Destroy(transform.gameObject);
        }
        if (collision.transform.tag == "Player")
        {
            Destroy(collision.gameObject);
            m_debugText.text = "GAME OVER";
        }
    }
    void Update () 
    {
        transform.Rotate(Vector3.back * Random.Range(10, 30f) * Time.deltaTime);
    }

    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private Rigidbody body;
    private Text m_debugText;
    #endregion

}
