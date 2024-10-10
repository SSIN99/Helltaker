using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum MenuType
{
    Start,
    Select,
    Exit,
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] public MenuType type;
    [SerializeField] Image border;
    [SerializeField] Text text;
    [SerializeField] Sprite SelectedMenuBorder;
    [SerializeField] Sprite DefaultMenuBorder;
    public void Seleted()
    {
        border.sprite = SelectedMenuBorder;
        border.color = new Color(230 / 255f, 77 / 255f, 81 / 255f);
        text.color = Color.white;
    }
    public void NotSelected()
    {
        border.sprite = DefaultMenuBorder;
        border.color = new Color(101 / 255f, 61 / 255f, 72 / 255f);
        text.color = new Color(159 / 255f, 140 / 255f, 151 / 255f);
    }
}
