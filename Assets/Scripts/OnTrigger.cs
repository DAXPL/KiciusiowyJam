using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToInvoke;
    [SerializeField] private UnityEvent eventToReset;
    private bool locked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && locked == false)
        {
            locked = true;
            eventToInvoke.Invoke();
        }
    }

    public void ResetDoors()
    {
        locked = false;
        eventToReset.Invoke();
    }
}
