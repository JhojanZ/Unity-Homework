using System;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
public class GrapplingHook : MonoBehaviour
{
    public Vector3 player;
    public Vector3 direction;
    public DistanceJoint2D joint;   

    public Vector3 best;
    public GameObject target;
    public GameObject obj;
    private int mapLayer;
    //static readonly

    public void OnEnable()
    {
        SetInitValues();

    }

    private void SetInitValues()
    {
        mapLayer = (1 << LayerMask.NameToLayer("Grap"));
        Debug.Log("GrapplingHook is enable at: " + LayerMask.NameToLayer("Grap"));
        target = GameObject.FindWithTag("target");
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }
    public void Main(Vector3 pos, Vector3 dir)
    {
        best = pos;
        player = pos;
        direction = dir;
        Grappling();
        Debug.Log("Grappling Hooks is activate!, Direction:" + direction + "| Best: " + best);
    }
    public void Destroy()
    {
        if (obj != null)
        {
            Destroy(obj);
            joint.enabled = false;
        }
        else
        {
            Debug.Log("Object not found in destroy.");
        }
    }

    public void Grappling()
    {
         
        for (int i = -10; i <= 10; i+=10)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, i);
            Vector2 dir = rotation * direction;
            RaycastHit2D hit = Physics2D.Raycast(player, dir, 10f, mapLayer);
            Debug.DrawRay(player, dir * 10f, Color.red);
            shoot(hit);
        }
        if (best != player)
        {
            obj = Instantiate(target, best, transform.rotation);
            joint.connectedAnchor = best;
            joint.enabled = true;
            joint.distance = Vector3.Distance(player, best);
        }
    }


    void shoot(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.Log("position: " + hit.point);
            GameObject objectToThrow = GameObject.FindWithTag("Grappling");
            if (objectToThrow != null)
            {
                if(best == null)
                {
                    best = hit.point;
                }
                else
                {
                    if (Vector3.Distance(player, best) < Vector3.Distance(player, hit.point))
                    {
                        best = hit.point;
                    }
                }

                GameObject thrownObject = Instantiate(objectToThrow, hit.point, transform.rotation);
                Debug.Log("Object thrown: " + thrownObject);
                Destroy(thrownObject, 2f);
            }
            else
            {
                Debug.LogWarning("No object with tag 'Grappling' found.");
            }
        }
    }
}
