using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeam : MonoBehaviour
{
    private RaycastHit2D hit;
    private LineRenderer line;
    [SerializeField] private LayerMask laser;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float hitTime;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Draw_Line();
    }

    public void HitTime()
    {
        StartCoroutine(HitTime_Co());
    }
    public IEnumerator HitTime_Co()
    {
        float elapsedTime = 0f;
        while(elapsedTime < hitTime)
        {
            RayCast();
            Catch_Player();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void RayCast()
    {
        hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, ~laser);
        Debug.DrawRay(transform.position, dir * 10, Color.red);
    }

    public void Draw_Line()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, new Vector2(-transform.position.x,transform.position.y));
    }
    public void Catch_Player()
    {
        if (hit && hit.transform.CompareTag("Player"))
        {
            hit.transform.GetComponent<PlayerControl>().Dead();
        }
    }
}
