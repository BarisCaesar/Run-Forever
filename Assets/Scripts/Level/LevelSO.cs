using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects", order = 0)]
public class LevelSO : ScriptableObject
{
    public List<RoadChunk> chunks;
}
