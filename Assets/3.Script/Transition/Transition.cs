using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Transition : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private void Start()
    {
        StopCoroutine(Load_Co());
        StartCoroutine(Load_Co());
    }

    public IEnumerator Load_Co()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
    }
}
