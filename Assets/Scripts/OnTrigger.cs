using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToInvoke;
    [SerializeField] private UnityEvent eventToReset;
    [SerializeField] private bool shouldLocking;
    private bool locked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && locked == false)
        {
            if(shouldLocking) locked = true;
            eventToInvoke.Invoke();
        }
    }

    public void ResetDoors()
    {
        locked = false;
        eventToReset.Invoke();
    }
    public void Hodor()
    {
        locked = true;
    }
}
