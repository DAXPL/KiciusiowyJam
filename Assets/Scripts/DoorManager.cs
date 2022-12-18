using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Transform corridorStart;
    [SerializeField] private OnTrigger[] triggers;
    [SerializeField] private OnTrigger endTrigger;
    [SerializeField] private Transform killingMachineRoot;
    private List<IDeathTrap> deathTraps = new List<IDeathTrap>();
    private void Awake()
    {
        for(int i = 0; i < killingMachineRoot.childCount; i++)
        {
            if (killingMachineRoot.GetChild(i).TryGetComponent(out IDeathTrap trap))
            {
                deathTraps.Add(trap);
                trap.ResetTrap();
            }
        }
    }
    public void OnDoorEnter(bool answer)
    {
        
        if (answer == GameManager.Instance.GetCorrectAnswer())
        {
            Debug.Log("Git gut");
            GameManager.Instance.Score();  
        }
        else
        {
            Debug.Log("No i kurwa umrzesz LOL");
            endTrigger.Hodor();
            ActivateRandomDeathMachine();
        }
        GameManager.Instance.ZaWardo();
        GameManager.Instance.TeleportPlayerTo(corridorStart);

    }
    private void ActivateRandomDeathMachine()
    {
        int id = Random.Range(0, deathTraps.Count);
        deathTraps[id].StartTrap();
    }
    public void ResetDoors()
    {
        for (int i=0; i<triggers.Length;i++)
        {
            triggers[i].ResetDoors();
        }
    }
}
