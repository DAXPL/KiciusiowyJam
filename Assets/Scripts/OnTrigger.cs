using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToInvoke;
    [SerializeField] private bool shouldLocking;
    [Space]
    [SerializeField] private UnityEvent enterEvent;
    [SerializeField] private UnityEvent exitEvent;
    private bool locked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterEvent.Invoke();
            if(locked == false)
            {
                if (shouldLocking) locked = true;
                eventToInvoke.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exitEvent.Invoke();
        }
    }

    public void ResetDoors()
    {
        Debug.Log($"Reset {name}");
        locked = false;
    }
    public void Hodor()
    {
        locked = true;
    }
}
