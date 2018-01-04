using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager  : MonoBehaviour
{

    #region Public Members
    public Text m_debugText;
    public GameObject m_playerCharacter;
    public GameObject m_button;
    public GameObject m_meteoriteSpawner;

    #endregion


    #region Public Void
    public void GameOver ()
    {
        GameObject[] objInGame = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in objInGame)
        {
            if(obj.tag=="Asteroid")
            {
                Destroy(obj);
            }
        }
        m_meteoriteSpawner.SetActive(false);
        m_playerCharacter.SetActive(false);
        m_debugText.text = "GAME OVER";
        m_button.SetActive(true);
    }
    public void TryAgain()
    {
        m_meteoriteSpawner.SetActive(true);
        m_playerCharacter.SetActive(true);
        m_playerCharacter.transform.position = new Vector3(0, -3f, 0);
        m_playerCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_debugText.text = "";
        m_button.SetActive(false);
    }
    #endregion


    #region System

    void Start () 
    {
		
	}
    void Awake () 
    {
        

    }
	
	void Update () 
    {
		
	}

    #endregion

    #region Private Void
    
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    
    #endregion

}
