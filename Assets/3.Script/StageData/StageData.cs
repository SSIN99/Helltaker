using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField] private int count;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private int generatorNum;
    [SerializeField] private string nextScene;
    public int Count => count;
    public Vector2 StartPos => startPos;
    public int GeneratorNum => generatorNum;
    public string NextScene => nextScene;
}
