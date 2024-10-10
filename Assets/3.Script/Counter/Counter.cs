using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Counter : MonoBehaviour
{

    [SerializeField] private TextMeshPro counter;
    [SerializeField] private int count;
    [SerializeField] private StageData stage;
    public int Count => count;
    private void Awake()
    {
        TryGetComponent<TextMeshPro>(out counter);
    }
    void Start()
    {
        Reset();
    }
    private void Update()
    {
        if (GameManager.Instance.isWin)
            gameObject.SetActive(false);
    }
    public void Down_Count()
    {
        count--;
        if (count <= 0)
            counter.text = $"--";
        else if (count < 10)
            counter.text = $"0{count}";
        else
            counter.text = $"{count}";
    }
    public void Reset()
    {
        count = stage.Count;
        if(count <= 0)
            counter.text = $"--";
        else
            counter.text = $"{count}";
    }
   
}
