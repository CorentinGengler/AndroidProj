using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidExplorer  : MonoBehaviour
{



    #region Public Members
    public Text m_debugText;
    public GameObject m_controlledObject;
    public GameObject m_projectile;

    #endregion


    #region Public Void

    #endregion

    IEnumerator GetAndPrintLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            m_debugText.text ="Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            m_debugText.text="Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            m_isGettingLocation = true;
            m_debugText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
        }

        // Stop service if there is no need to query location updates continuously
        // Input.location.Stop();
    }
    #region System

    void Awake () 
    {
        StartCoroutine(GetAndPrintLocation());
	}
    
	void Update () 
    {
        if(m_isGettingLocation)
        {
            m_debugText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        m_isPlayerPressing = Input.touchCount > 0;
        if(m_isPlayerPressing)
        {
            //m_mainTouch = Input.touches[0];
            //m_firstTouchPosition = m_mainTouch.position;

        }


        Vector3 temp = Input.acceleration;
        temp.z = 0;
        m_objbody = m_controlledObject.GetComponent<Rigidbody>();
        if ((((m_objbody.velocity.x) < 0 && (temp.x) > 0)  || ((m_objbody.velocity.x) > 0 && (temp.x) < 0)) || (((m_objbody.velocity.y) < 0 && (temp.y) > 0) || ((m_objbody.velocity.y) > 0 && (temp.y) < 0)))
        {
            m_objbody.AddForce(3*temp);
        }
        else
        {
            m_objbody.AddForce(temp);
        }
#else

#endif

        /*
        m_debugText.text += Input.touchCount.ToString(); // Number of finger on screne
        if (Input.touchCount > 0)
            m_debugText.text += Input.touches[0].position.ToString(); // De 0,0 à 1,1 en pixel

        m_debugText.text += Input.acceleration.ToString(); // Accélration
        m_debugText.text += Input.compass.ToString(); // direction magnétique
        m_debugText.text += Input.gyro.ToString(); // direction magnétique
        m_debugText.text += Input.location.lastData.latitude.ToString();
        */
        /*
        if (m_isPlayerPressing)
        {
            m_firstTouchPosition = Input.mousePosition;
            //m_firstTouchPosition = m_mainTouch.position;
            //m_controlledObject.SetActive(true);
        }
        */
        /*
        if (m_firstTouchPosition.x!=0 && m_firstTouchPosition.y != 0)
        {
            m_debugText.text = m_firstTouchPosition.ToString();
        }
        */

    }

    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private Touch m_mainTouch;
    private bool m_isPlayerPressing = false;
    private Vector2 m_firstTouchPosition;
    private Rigidbody m_objbody;

    private bool m_isGettingLocation = false;

    #endregion

}
