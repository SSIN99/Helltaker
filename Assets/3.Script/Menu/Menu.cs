using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Type
{
    Start,
    Skip,
    BGM,
    SEM,
    Main
}
public class Menu : MonoBehaviour
{
    [SerializeField] GameObject borderL, borderR;
    [SerializeField] public Type type;
    [SerializeField] Text text, state;
    void Awake()
    {
        text = GetComponent<Text>();
    }
    
    public void Seleted()
    {
        borderL.GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, 0);
        borderR.GetComponent<RectTransform>().anchoredPosition = new Vector2(140, 0);
        borderL.GetComponent<Image>().color = new Color(230/255f, 77/255f, 81/255f);
        borderR.GetComponent<Image>().color = new Color(230/255f, 77/255f, 81/255f);
        text.color = Color.white;
        if (type.Equals(Type.BGM) || type.Equals(Type.SEM))
            state.color = Color.white;
    }
    public void NotSelected()
    {
        borderL.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 0);
        borderR.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 0);
        borderL.GetComponent<Image>().color = new Color(101/255f, 61/255f, 72/255f);
        borderR.GetComponent<Image>().color = new Color(101/255f, 61/255f, 72/255f);
        text.color = new Color(159/255f, 140/255f, 151/255f);
        if (type.Equals(Type.BGM) || type.Equals(Type.SEM))
            state.color = new Color(159/255f, 140/255f, 151/255f);
    }
    public void VolumeSet(int volumeState)
    {
        switch (volumeState)
        {
            case 0:
                state.text = "음소거";
                break;
            case 1:
                state.text = "낮음";
                break;
            case 2:
                state.text = "중간";
                break;
            case 3:
                state.text = "높음";
                break;

        }
    }
}
