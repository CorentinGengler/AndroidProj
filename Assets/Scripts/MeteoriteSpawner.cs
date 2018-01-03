using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner  : MonoBehaviour
{

    #region Public Members
    public List<GameObject> m_asteroidSpawners = new List<GameObject>() ;
    public List<GameObject> m_asteroidModels = new List<GameObject>();
    public float m_cooldownMeteorite;
    #endregion


    #region Public Void

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
        if (m_timerMeteoriteSpawn >= m_cooldownMeteorite)
        {
            int randomSpawningPoint = Random.Range(0, m_asteroidSpawners.Count-1);
            while(randomSpawningPoint == m_previousRandom)
            {
                randomSpawningPoint = Random.Range(0, m_asteroidSpawners.Count - 1);
            }
            int randomAsteroidModel = Random.Range(0, m_asteroidModels.Count-1);
            Instantiate(m_asteroidModels[randomAsteroidModel], 
                m_asteroidSpawners[randomSpawningPoint].transform.position,
                m_asteroidSpawners[randomSpawningPoint].transform.rotation);

            m_timerMeteoriteSpawn = 0f;
        }
        else
        {
            m_timerMeteoriteSpawn += Time.deltaTime;
        }
    }

    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private float m_timerMeteoriteSpawn=0f;
    private int m_previousRandom = 0;
    #endregion

}
