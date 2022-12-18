using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private OnTrigger[] triggers;
    //[SerializeField] private OnTrigger endTrigger;
    [SerializeField] private Transform killingMachineRoot;
    private List<IDeathTrap> deathTraps = new List<IDeathTrap>();
    [SerializeField] private Animator[] doorsAnimators;
    [SerializeField] private GameObject mainDoors;
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
        mainDoors.SetActive(false);
    }
    public void OnDoorEnter(bool answer)
    {
        StartCoroutine(OnDoorEnterCorountine(answer));
    }
    IEnumerator OnDoorEnterCorountine(bool answer)
    {
        OpenDoor(-1);
        yield return new WaitForSeconds(3);

        if (answer == GameManager.Instance.GetCorrectAnswer())
        {
            Debug.Log("Git gut");
            GameManager.Instance.Score();
        }
        else
        {
            Debug.Log("No i umrzesz");
            mainDoors.SetActive(true);
            //endTrigger.Hodor();
            ActivateRandomDeathMachine();
        }
        GameManager.Instance.ZaWardo();
        GameManager.Instance.TeleportPlayerTo();
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

    public void OpenDoor(int id)
    {
        for (int i=0;i<doorsAnimators.Length;i++)
        {
            bool open = doorsAnimators[i].GetBool("Open");
            doorsAnimators[i].SetBool("Open", i == id);

           if ((open != (i == id)) && doorsAnimators[i].TryGetComponent(out AudioSource audio))
           {
               audio.Play();
           }
        }
        
    }
}
