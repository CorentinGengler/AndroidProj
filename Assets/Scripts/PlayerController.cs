using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{



    #region Public Members

    public Text m_debugText;
    public GameObject m_playerCharacter;
    public GameObject m_tempWallPrefab;
    [Range(1, 10)]
    public float m_Basespeed = 3f;

    [Header("Simulate accelerator in Unity")]
    [Range(-1, 1)]
    public float m_x=0f;
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
        //StartCoroutine(GetAndPrintLocation());
        m_objBody = m_playerCharacter.GetComponent<Rigidbody>();
        //m_timerShoot = m_cooldownShoot;
    }
    
	void Update () 
    {
        /*
        if(m_isGettingLocation)
        {
            m_debugText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
        }
        */
#if UNITY_ANDROID && !UNITY_EDITOR
        
        #region shootingAndroid
        /*
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
                GameObject shoot = Instantiate(m_projectile, m_playerCharacter.transform.position, transform.rotation);
                Rigidbody shootBody = shoot.GetComponent<Rigidbody>();
                Vector3 direction = Camera.main.ScreenToViewportPoint(Input.touches[0].position);
                direction.Set(direction.x - 0.5f, direction.y - 0.5f, 0f);
                direction = direction.normalized;
                Vector3 positionAltered = m_playerCharacter.transform.position;
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
                */
        #endregion

        
        Vector3 temp = Input.acceleration;
        temp.Set(temp.x, 0f, 0f);
        
#else
        //fake accelerator and fake touch on unity editor
        Vector3 temp = new Vector3(m_x, 0, 0);
        
        m_debugText.text = "Score:";

        #region makeWall
        if (Input.GetButtonUp("Fire1")) m_click = false;
        if (Input.GetButtonDown("Fire1")) m_click = true;

        if(m_click)
        {
            Debug.Log("inputpos: " + Input.mousePosition);
            //Debug.Log("newpos: " + Input.mousePosition);
            if (!m_tempWallExists)
            {
                
                m_tempWall = Instantiate(m_tempWallPrefab, Input.mousePosition, transform.rotation);
                if(m_tempWall.transform.position.x > m_playerCharacter.transform.position.x)//so if the wall is to ther player's right
                {
                    m_tempWall.GetComponent<Walls>().m_isLeftWall = false;
                }
                else
                {
                    m_tempWall.GetComponent<Walls>().m_isLeftWall = true;
                }
                m_tempWallExists = true;
            }
        }
        else
        {
            Destroy(m_tempWall);
            m_tempWallExists = false;
        }
        #endregion

        #region shooting

        /*
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
        */
        #endregion

#endif

        


        temp.z = 0;
        if (( m_objBody.velocity.x < 0 && temp.x > 0) || ((m_objBody.velocity.x) > 0 && (temp.x) < 0)) //makes it faster when changing direction
        {
            m_objBody.AddForce(3 * m_Basespeed *  temp);
        }
        else
        {
            m_objBody.AddForce(m_Basespeed * temp);
        }
        
    }

    #endregion

    #region Private Void
    private Vector3 TranslateInputMousePosToGamePos(Vector3 badPosition)
    {
        Vector3 goodPosition = new Vector3(0f,0f,0f);
        return goodPosition;
    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private Touch m_mainTouch;
    private bool m_isPlayerPressing = false;
    private Vector2 m_firstTouchPosition;
    private Rigidbody m_objBody;
    private GameObject m_tempWall;
    private bool m_tempWallExists = false;
    private bool m_isGettingLocation = false;

    private bool m_click;
    private float m_cooldownShoot = 1f;
    private float m_timerShoot;
    #endregion

}
