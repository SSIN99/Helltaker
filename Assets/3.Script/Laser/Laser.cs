using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private bool isOn = true;
    private Animator anim;
    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject CircleB;
    [SerializeField] private GameObject Beam;
    [SerializeField] private GameObject BeamB;

    void Start()
    {
        isOn = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.isWin && isOn)
            TurnOff();
    }

    public void TurnOff()
    {
        isOn = false;
        Beam.SetActive(false);
        BeamB.SetActive(false);
        CircleB.SetActive(false);
        Circle.GetComponent<Animator>().SetTrigger("off");
        anim.SetTrigger("off");
    }


}
