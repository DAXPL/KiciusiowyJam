using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyLaser : MonoBehaviour, IDeathTrap
{
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float time;
    public void ResetTrap() 
    {
        laser.SetActive(false);
    }
    public void StartTrap()
    {
        Debug.Log("Deadly laser time!");
        StartCoroutine(Sequence());
    }
    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2);
        laser.SetActive(true);
        laser.transform.position = startPos.position;
        float timePassed = 0;
        while (timePassed<= time)
        {
            laser.transform.position = Vector3.Lerp(startPos.position, endPos.position, timePassed/time);
            timePassed += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
