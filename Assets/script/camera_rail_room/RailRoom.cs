using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailRoom : MonoBehaviour {

    public static RailRoom singleton;

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
    void Start () {
        singleton = this;

        NbNode = transform.childCount;
        Nodes = new Vector3[NbNode];

        for (int i = 0; i < NbNode; i++)
        {
            Nodes[i] = transform.GetChild(i).position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < NbNode-1; i++)
        {
            Debug.DrawLine(Nodes[i], Nodes[i + 1], Color.blue);
        }
    }

    public Vector3 ProjetOnSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 segmentDir = (v2 - v1).normalized;

        float distFromV1 = Vector3.Dot(segmentDir, v1ToPos);
        if(distFromV1 < 0.0f)
        {
            return v1;
        }
        else if(distFromV1*distFromV1 > (v2-v1).sqrMagnitude)
        {
            return v2;
        }
        else
        {
            Vector3 fromV1 = segmentDir * distFromV1;
            return v1 + fromV1;
        }
    }
}
