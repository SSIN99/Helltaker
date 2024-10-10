using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private RaycastHit2D hit;
    private bool isCatch = false;
    private float dis;
    private LineRenderer line;
    [SerializeField] private bool isPre = false;
    [SerializeField] private LayerMask laser;
    [SerializeField] private LayerMask wall;
    [SerializeField] private Vector2 dir;

    void Start()
    {
        isCatch = false;
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isCatch) return;

        RayCast();
        if (!isPre)
        {
            Catch_Player();
        }
        Draw_Line();
    }

    public void RayCast()
    {
        if(isPre)
            hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, wall);
        else
            hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, ~laser);
        dis = Vector2.Distance(transform.position, hit.point);
        Debug.DrawRay(transform.position, dir * dis, Color.red);
    }

    public void Draw_Line()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hit.point);
    }
    public void Catch_Player()
    {
        if (hit && hit.transform.CompareTag("Player"))
        {
            isCatch = true;
            hit.transform.GetComponent<PlayerControl>().Dead();
        }
    }
}
