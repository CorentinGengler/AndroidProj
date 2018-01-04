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
    
    void Awake () 
    {
        m_objBody = m_playerCharacter.GetComponent<Rigidbody>();
    }
    
	void Update () 
    {
        Vector3 position = Vector3.zero;
        Vector3 movement = Vector3.zero;
#if UNITY_ANDROID && !UNITY_EDITOR
        if(Input.touchCount>0)
        {
            m_click = true;
            position = Input.touches[0].position;
        }
        else
        {
            m_click = false;
        }
        movement = Input.acceleration;
        movement.Set(movement.x, movement.y, 0f);
#else
        if (Input.GetButtonUp("Fire1")) m_click = false;
        if (Input.GetButtonDown("Fire1"))
        {
            position = Input.mousePosition;
            m_click = true;
        }
        movement.Set(m_x, 0f, 0f);
#endif

        WallHandler(position);
        Move(movement);
    }
    

#region Private Void
    private void Move(Vector3 vectorMove)
    {
        if ((m_objBody.velocity.x < 0 && vectorMove.x > 0) || ((m_objBody.velocity.x) > 0 && (vectorMove.x) < 0) || (m_objBody.velocity.y < 0 && vectorMove.y > 0) || ((m_objBody.velocity.y) > 0 && (vectorMove.y) < 0)) //makes it faster when changing direction
        {
            m_objBody.AddForce(3 * m_Basespeed * vectorMove);
        }
        else
        {
            m_objBody.AddForce(m_Basespeed * vectorMove);
        }
    }
    
    private Vector3 TranslateInputMousePosToGamePos(Vector3 badPosition)
    {
        float goodX = (badPosition.x / Screen.width) * 4f;
        goodX -= 2f;//because from -2 to 2 and not from 0 to 4
        float goodY = (badPosition.y / Screen.height) * 7.6f;
        goodY -= 3.8f;
        Vector3 goodPosition = new Vector3(goodX, goodY, 0f);
        return goodPosition;
    }

    private void WallHandler(Vector3 position)
    {
        if (m_click)
        {
            if(m_timerShield < m_maxTimeShield && !m_ShieldOverload)
            {
                Vector3 posInWorld = TranslateInputMousePosToGamePos(position);
                m_timerShield += 1 * Time.deltaTime;
                if (!m_tempWallExists)
                {
                    m_tempWall = Instantiate(m_tempWallPrefab, posInWorld, transform.rotation);
                    m_tempWallExists = true;
                }
                else
                {
                    //m_tempWall.GetComponent<Rigidbody>().position=posInWorld;
                }
            }
            else
            {
                m_ShieldOverload = true;
                if (m_timerShield > m_maxTimeShield-10) m_timerShield -= 0.5f * Time.deltaTime;
            }
            
        }
        else
        {
            Destroy(m_tempWall);
            m_tempWallExists = false;
            if(m_timerShield>0) m_timerShield -= 0.5f * Time.deltaTime;
        }
    }
#endregion
    


#region Private And Protected Members
    private Rigidbody m_objBody;
    private GameObject m_tempWall;
    private bool m_tempWallExists = false;
    private bool m_isGettingLocation = false;
    private bool m_click;
    private bool m_ShieldOverload = false;
    private float m_maxTimeShield = 10f;
    private float m_timerShield = 0;
#endregion
    
}
