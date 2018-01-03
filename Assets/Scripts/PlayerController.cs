using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{



    #region Public Members

    public Text m_debugText;
    public GameObject m_controlledObject;
    public GameObject m_projectile;

    [Header("Simulate accelerator in Unity")]
    [Range(-1, 1)]
    public float m_x=0f;
    [Range(-1, 1)]
    public float m_y=0f;
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
        m_objBody = m_controlledObject.GetComponent<Rigidbody>();
        m_timerShoot = m_cooldownShoot;
    }
    
	void Update () 
    {
        if(m_isGettingLocation)
        {
            m_debugText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
        }

    #if UNITY_ANDROID && !UNITY_EDITOR
        
        #region shootingAndroid
        m_isPlayerPressing = Input.touchCount > 0;
        if (m_isPlayerPressing)
        {
            m_click = true;
        }
        else
        {
            m_click = false;
        }

        if (m_timerShoot >= m_cooldownShoot)
        {
            if (m_click)
            {
                GameObject shoot = Instantiate(m_projectile, m_controlledObject.transform.position, transform.rotation);
                Rigidbody shootBody = shoot.GetComponent<Rigidbody>();
                Vector3 direction = Camera.main.ScreenToViewportPoint(Input.touches[0].position);
                direction.Set(direction.x - 0.5f, direction.y - 0.5f, 0f);
                direction = direction.normalized;
                Vector3 positionAltered = m_controlledObject.transform.position;
                positionAltered.Set(positionAltered.x / 4.4f, positionAltered.y / 3.9f, 0f);
                Vector3 test = direction - positionAltered;
                shootBody.AddForce(test * 500f);
                m_timerShoot = 0f;
            }
        }
        else
        {
            m_timerShoot += Time.deltaTime;
        }
                
        #endregion

        
        Vector3 temp = Input.acceleration;
        
    #else
        //fake accelerator and fake touch on unity editor
        

        Vector3 temp = new Vector3(m_x, m_y, 0);
        m_debugText.text = "No Text";

        #region shooting


        if (Input.GetButtonUp("Fire1")) m_click = false;
        if (Input.GetButtonDown("Fire1")) m_click = true;

        if (m_timerShoot >= m_cooldownShoot)
        {
            if (m_click)
            {
                GameObject shoot= Instantiate(m_projectile, m_controlledObject.transform.position, transform.rotation);
                Rigidbody shootBody = shoot.GetComponent<Rigidbody>();
                Vector3 direction = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                direction.Set(direction.x - 0.5f, direction.y - 0.5f, 0f);
                direction = direction.normalized;
                Vector3 positionAltered = m_controlledObject.transform.position;
                positionAltered.Set(positionAltered.x / 4.4f, positionAltered.y / 3.9f, 0f);
                Debug.Log("direction normalized: " + direction);
                Debug.Log("positionAltered: " + positionAltered);
                Vector3 test = direction - positionAltered;
                shootBody.AddForce(test * 500f);
                m_timerShoot = 0f;
            }
        }
        else
        {
            m_timerShoot += Time.deltaTime;
        }

        #endregion

    #endif


        temp.z = 0;
        if ((((m_objBody.velocity.x) < 0 && (temp.x) > 0) || ((m_objBody.velocity.x) > 0 && (temp.x) < 0)) || (((m_objBody.velocity.y) < 0 && (temp.y) > 0) || ((m_objBody.velocity.y) > 0 && (temp.y) < 0)))
        {
            m_objBody.AddForce(3 * temp);
        }
        else
        {
            m_objBody.AddForce(temp);
        }
        
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
    private Rigidbody m_objBody;

    private bool m_isGettingLocation = false;

    private bool m_click;
    private float m_cooldownShoot = 1f;
    private float m_timerShoot;
    #endregion

}
