using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToInvoke;
    public void Interact()
    {
        Debug.Log("Did");
        eventToInvoke.Invoke();
    }
}
