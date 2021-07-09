using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOnRailRoom : MonoBehaviour {

    public static CamOnRailRoom singleton;

    public GameObject target;//PlayerController
    public float maxAngle;
    private Vector3 cameraLastPos;
    private Vector3 targetLastPos;

    public struct Segment
    {
        public Vector3 start;
        public Vector3 end;
    }
    public Segment currentSegment;
    

	// Use this for initialization
	void Start () {
        singleton = this;

        currentSegment.start = RailRoom.singleton.Nodes[RailRoom.singleton.StartingNodeOfCurrentSegment];
        currentSegment.end = RailRoom.singleton.Nodes[RailRoom.singleton.StartingNodeOfCurrentSegment + 1];

        cameraLastPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate () {

        float coeffTime = Vector3.Distance(transform.position, target.transform.position) / 1000.0f;

        //set the camera pos on the rail
        Vector3 tempCamPos = RailRoom.singleton.ProjetOnSegment(currentSegment.start, currentSegment.end, target.transform.position);
        transform.position = Vector3.Lerp(cameraLastPos, tempCamPos, coeffTime);

        SetCameraAngle();

        cameraLastPos = transform.position;
    }
    
    private void SetCameraAngle()
    {
        Vector3 vectorCamPlayer = target.transform.position - transform.position;
        vectorCamPlayer.y = 0;
        
        //find perpendicular vector of current segment and y-axis
        Vector3 dirCurrentRail = currentSegment.end - currentSegment.start;
        Vector3 perp = Vector3.Cross(dirCurrentRail, Vector3.up).normalized;
        
        //Debug.Log(Mathf.Abs(Vector3.Angle(perp, vectorCamPlayer)));
        float tmpA = Mathf.Abs(Vector3.Angle(perp, vectorCamPlayer));
        if (tmpA < maxAngle)
        {
            //trying to fix the jolt but didn't work
            //Vector3 targetPosTmp = Vector3.Lerp(target.transform.position, targetLastPos, 0.005f);

            transform.LookAt(target.transform.position);
        }
        else // pfiou je l'ai échappé belle
        {
            //c'est à ce demander si j'ai vraiment pas un ange gardien
            //même si parfois iel pourrai me punir un peu pour m'apprendre à me bouger
            //ou peut-être que c'est pour éviter que je me brise encore plus
            //je me demande à quoi iel ressemble
            //est ce que la vois que j'ai entendu ce matin c'était iel ?
            //pourrais-je avoir une réponse à cette simple question ?
        
            float previousY = transform.rotation.eulerAngles.y;
            transform.LookAt(target.transform.position);
            Vector3 currentRot = transform.rotation.eulerAngles;
            currentRot.y = previousY;
            transform.rotation = Quaternion.Euler(currentRot);
        }
        

        targetLastPos = target.transform.position;

        Debug.DrawLine(transform.position, target.transform.position, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 2f, Color.red);
        Debug.DrawLine(currentSegment.start, currentSegment.start + perp, Color.magenta);
    }
}
