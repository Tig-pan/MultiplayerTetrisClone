using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Circuit", menuName = "Circuits")]
public class Circuit : ScriptableObject
{
    public int circuitIndex;
    public AI[] ais = new AI[4];
}
