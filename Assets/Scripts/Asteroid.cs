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
        body.velocity = new Vector3(0f, -Random.Range(2f, 5f), 0f);
    }
    void Awake () 
    {
        m_canvas = FindObjectOfType<Canvas>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            m_canvas.GetComponent<Manager>().GameOver();
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
    private Canvas m_canvas;
    #endregion

}
