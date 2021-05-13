using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    GameObject Boxmion;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OntriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon"))SubLife(20);
    }

    public void SubLife(int life)
    {
        life -= 20;
    }
}
