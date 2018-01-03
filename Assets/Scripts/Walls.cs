using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls  : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Shoot")
        {
            DestroyObject(collision.gameObject);
        }
    }
}
