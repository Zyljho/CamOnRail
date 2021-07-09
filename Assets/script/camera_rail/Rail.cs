using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public static Rail singleton;

    private Vector3[] nodes;
    private int nbNode;

    [Tooltip("start with 0")]
    public int StartingNodeOfCurrentSegment;

    public Vector3[] Nodes
    {
        get
        {
            return nodes;
        }

        set
        {
            nodes = value;
        }
    }

    public int NbNode
    {
        get
        {
            return nbNode;
        }

        set
        {
            nbNode = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        singleton = this;

        NbNode = transform.childCount;
        Nodes = new Vector3[NbNode];

        for (int i = 0; i < NbNode; i++)
        {
            Nodes[i] = transform.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(NbNode > 1)
        {
            for(int i =0; i < NbNode -1; i++)
            {
                Debug.DrawLine(nodes[i], nodes[i + 1], Color.blue);
            }
        }
    }

    public Vector3 ProjetOnSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 segmentDir = (v2 - v1).normalized;

        float distFromV1 = Vector3.Dot(segmentDir, v1ToPos);
        if (distFromV1 < 0.0f)
        {
            return v1;
        }
        else if (distFromV1 * distFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        }
        else
        {
            Vector3 fromV1 = segmentDir * distFromV1;
            return v1 + fromV1;
        }
    }

    public Vector3 ProjectPosOnRail(Vector3 pos)
    {
        int closestNode = GetClosestNode(pos);

        Debug.DrawLine(Nodes[closestNode], pos, Color.green);

        if(closestNode == 0)
        {
            return ProjetOnSegment(Nodes[0], nodes[1], pos);
        }
        else if (closestNode == nbNode -1)
        {
            return ProjetOnSegment(Nodes[nbNode - 1], Nodes[nbNode - 2], pos);
        }
        else
        {
            Vector3 leftSegment = ProjetOnSegment(Nodes[closestNode - 1], Nodes[closestNode], pos);
            Vector3 rightSegment = ProjetOnSegment(Nodes[closestNode + 1], Nodes[closestNode], pos);
            
            if((pos - leftSegment).sqrMagnitude <= (pos - rightSegment).sqrMagnitude)
            {
                return leftSegment;
            }
            else
            {
                return rightSegment;
            }
        }
    }

    private int GetClosestNode(Vector3 pos)
    {
        int closestNode = -1;
        float shortestDist = 0f;

        for(int i =0; i < NbNode; i++)
        {
            float sqrDist = (Nodes[i] - pos).sqrMagnitude;
            if(shortestDist == 0f || sqrDist < shortestDist)
            {
                shortestDist = sqrDist;
                closestNode = i;
            }
        }

        return closestNode;
    }
}
