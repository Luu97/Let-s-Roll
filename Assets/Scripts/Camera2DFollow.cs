using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    //float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadPos;
    Vector3 aheadTargetPos;

    float nextTimeToSearch = 0;

    // Use this for initialization
    void Start() {
        lastTargetPosition = target.position;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update() {

        if (target == null) {
            FindPlayer();
            return;
        }

        // only update lookahead pos if accelerating or changed direction
        float yMoveDelta = (target.position - lastTargetPosition).y;

        bool updateLookAheadTarget = Mathf.Abs(yMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget) {
            lookAheadPos = lookAheadFactor * Vector3.down * Mathf.Sign(yMoveDelta);
        }
        else {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        //Only update the target position if the player is not too high up
        if (target.position.y - PlatformHandler.PH.getFirstPlatform().transform.position.y < 2) {
            aheadTargetPos = target.position + lookAheadPos;
            lastTargetPosition = target.position;
        }
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);
        newPos = new Vector3(0, newPos.y, -10);

        transform.position = newPos;

    }

    void FindPlayer() {
        if (nextTimeToSearch <= Time.time) {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                target = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
