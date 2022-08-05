using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clean Up", menuName = "Clean Up")]
public class CleanUp : ScriptableObject
{
    public int level;
    public int[] garbageTiles;
    public int aiTiles;
    public float timeLimit;
}
