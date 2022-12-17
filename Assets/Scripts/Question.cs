using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Question")]
public class Question : ScriptableObject
{
    public string desc;
    public bool correctAnswer;
}
