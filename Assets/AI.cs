using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AI", menuName = "AIs")]
public class AI : ScriptableObject
{
    public new string name = "N0obMaster69";
    public float randomAdditiveMax = 4f;
    public float screwChance = 0.04f;
    public int timesToScrew = 5;
    public int initialThinkMid = 24;
    public int framesToMove = 8;
    public int aiIndex = 0;
    public bool usesPreview = false;
    public bool usesHold = true;
    public bool isCleanup = false;

    public const float bumpinessCare = 0.6f;
    public const float maxHeightCare = 1.5f;
    public const float tetrisCare = 10;
    public const float threeHoleHate = 7;
    public const float holeCare = 50;
    public const float hangCare = 50;
    public const float hangStackCare = 6;
    public const float oneLineHate = 13;
    public const float twoLineHate = 8;
    public const float threeLineHate = 5;
    public const float oneHoleCare = 12;
    public const float clearCare = 15;
}
