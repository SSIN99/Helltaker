using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogData : ScriptableObject
{
    [SerializeField] Sprite[] cutseen;
    [SerializeField] Sprite[] charList;
    [SerializeField] Sprite[] subCharList;
    [SerializeField] string[] nameList;
    [SerializeField] string[] scriptList;
    [SerializeField] bool[] isDouble;
    [SerializeField] int dialogCount;
    public Sprite[] CharList => charList;
    public Sprite[] SubCharList => subCharList;
    public string[] NameList => nameList;
    public string[] ScriptList => scriptList;
    public bool[] IsDouble => isDouble;
    public int DialogCount => dialogCount;
    public Sprite[] Cutseen => cutseen;
}
