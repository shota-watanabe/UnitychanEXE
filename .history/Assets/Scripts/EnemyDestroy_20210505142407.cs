using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestroy : MonoBehaviour
{
    public UnityEvent OnDestroyed = new UnityEvent();

    private void OnDestroy(){
        Debug.Log("Destroyed");
        OnDestroyed.Invoke();
    }
}
