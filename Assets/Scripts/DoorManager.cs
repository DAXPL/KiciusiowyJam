using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Transform corridorStart;
    [SerializeField] private OnTrigger[] triggers;
    public void OnDoorEnter(bool answer)
    {
        
        if (answer == GameManager.Instance.GetCorrectAnswer())
        {
            Debug.Log("Git gut");
            GameManager.Instance.Score();
            GameManager.Instance.TeleportPlayerTo(corridorStart);
        }
        else
        {
            Debug.Log("No i kurwa umrzesz LOL");
            GameManager.Instance.KillPlayer();
        }
        
    }
    public void ResetDoors()
    {
        for (int i=0; i<triggers.Length;i++)
        {
            triggers[i].ResetDoors();
        }
    }
}
