using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcManager : MonoBehaviour
{
    [SerializeField] private Animator[] objs;  
    private  bool isOn = true;
    void Update()
    {
        if (GameManager.Instance.isWin && isOn)
            TurnOff();
    }
    public void TurnOff()
    {
        isOn = false;
        for(int i=0;i< objs.Length; i++)
        {
            objs[i].SetTrigger("off");
        }
    }
}
