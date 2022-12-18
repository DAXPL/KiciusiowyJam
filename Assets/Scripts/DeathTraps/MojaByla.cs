using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MojaByla : MonoBehaviour, IDeathTrap
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject morda;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float time;

    [SerializeField] private AudioSource sound;
    public void ResetTrap()
    {
        holder.SetActive(false);
    }
    public void StartTrap()
    {
        Debug.Log("Deadly laser time!");
        StartCoroutine(Sequence());
    }
    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.GetPlayer().BreakLegs();
        holder.SetActive(true);
        morda.transform.position = startPos.position;
        float timePassed = 0;
        bool k = false;
        while (timePassed <= time)
        {
            morda.transform.position = Vector3.Lerp(startPos.position, endPos.position, timePassed / time);
            timePassed += Time.deltaTime;
            if((timePassed/time)>0.6 && !k)
            {
                k = true;
                sound.Play();
            }
            yield return null;
        }
        yield return new WaitForSeconds(2);
        GameManager.Instance.KillPlayer();
    }
}
