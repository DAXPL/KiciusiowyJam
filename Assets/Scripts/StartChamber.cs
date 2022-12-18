using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChamber : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource doorAudio;
    public void Open(bool open)
    {
        animator.SetBool("Open", open);
        doorAudio.Play();
    }
}
