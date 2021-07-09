using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// place the script on a collider trigger
/// the forward of this look into the next room
/// </summary>

public class NextSegRailForCollider : MonoBehaviour {

    //public bool nextRoom = true;
    private Vector3 playerToDoor;
    private bool onEnterIsRight = false;

    public bool isCorridorNext;
    public bool isCorridorPrevious;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        //if (null != other.GetComponent<PlayerController>())
        if (CamOnRailRoom.singleton.target == other.gameObject)
        {
            playerToDoor = (transform.position - CamOnRailRoom.singleton.target.transform.position).normalized;
            //Debug.Log("enter angle : " + Mathf.Abs(Vector3.Angle(playerToDoor, transform.forward)));
            if (Mathf.Abs(Vector3.Angle(playerToDoor, transform.forward)) > 90.0f) // > 90 -> playerToDoor
            {
                onEnterIsRight = true;
            }
            else
            {
                onEnterIsRight = false;
            }
        }
        //Debug.Log("enter from right :" + onEnterIsRight);
    }

    private void OnTriggerExit(Collider other)
    {
        if (CamOnRailRoom.singleton.target == other.gameObject)
        {
            //Debug.Log("try to exit collider");
            Vector3 doorToPlayer = (CamOnRailRoom.singleton.target.transform.position - transform.position).normalized;
            //Debug.Log(Vector3.Dot(doorToPlayer, playerToDoor));
            bool onExitIsRight = false;
            //Debug.Log("exit angle : " + Mathf.Abs(Vector3.Angle(doorToPlayer, transform.forward)));
            if (Mathf.Abs(Vector3.Angle(doorToPlayer, transform.forward)) < 90.0f)
            {
                onExitIsRight = true;
            }
            else
            {
                onExitIsRight = false;
            }
            
            if(onEnterIsRight != onExitIsRight)
            {
                int bfNbNode;

                //check if the whole corridor shit is working well

                if (onExitIsRight)//next room
                {
                    if (isCorridorPrevious || isCorridorNext)
                        bfNbNode = 1;
                    else
                        bfNbNode = 2;

                    RailRoom.singleton.StartingNodeOfCurrentSegment += bfNbNode;

                    CamOnRailRoom.singleton.currentSegment.start = RailRoom.singleton.Nodes[RailRoom.singleton.StartingNodeOfCurrentSegment];
                    CamOnRailRoom.singleton.currentSegment.end = RailRoom.singleton.Nodes[RailRoom.singleton.StartingNodeOfCurrentSegment + 1];
                    //Debug.Log("next segment");
                }
                else//previous room
                {
                    if (isCorridorNext || isCorridorPrevious)
                        bfNbNode = 1;
                    else
                        bfNbNode = 2;

                    RailRoom.singleton.StartingNodeOfCurrentSegment -= bfNbNode;

                    CamOnRailRoom.singleton.currentSegment.start = RailRoom.singleton.Nodes[RailRoom.singleton.StartingNodeOfCurrentSegment];
                    CamOnRailRoom.singleton.currentSegment.end = RailRoom.singleton.Nodes[RailRoom.singleton.StartingNodeOfCurrentSegment + 1];
                    //Debug.Log("previous segment");
                }
            }
        }
    }
}
