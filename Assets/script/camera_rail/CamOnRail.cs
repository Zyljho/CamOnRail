using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOnRail : MonoBehaviour
{
    public Transform target;

    private Vector3 objectivePos;
    private Vector3 cameraLastPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Rail.singleton.Nodes[0];
        cameraLastPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        objectivePos = Rail.singleton.ProjectPosOnRail(target.position);
        transform.position = Vector3.Slerp(cameraLastPos, objectivePos, 0.02f);

        transform.LookAt(target.position);

        cameraLastPos = transform.position;

        Debug.DrawLine(transform.position, target.position, Color.magenta);
    }
}
