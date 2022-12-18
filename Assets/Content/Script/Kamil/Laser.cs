using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform Start;
    [SerializeField] private Transform Koniec;

    [SerializeField] private LineRenderer LineRenderer;

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        LineRenderer.SetPosition(0, Start.transform.position);
        LineRenderer.SetPosition(1, Koniec.transform.position);


    }
}
