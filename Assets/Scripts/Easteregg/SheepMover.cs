using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMover : MonoBehaviour
{
    public Vector3 valueToMove = Vector3.zero;
    private Vector3 desiredPosition;

    public float durationUntilStart = 1f;
    public float movementDuration = 2f;

    public float waited;

    private BloekiState state = BloekiState.WAIT;

    private float timeWaited;

    private void Start()
    {
        desiredPosition = transform.position + valueToMove;

        Debug.Log("desired pos: " + desiredPosition);
        Debug.Log("pos: " + transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == BloekiState.DONE || state == BloekiState.MOVING)
        {
            DoPulse();
            return;
        }

        switch(state)
        {
            case BloekiState.WAIT:
                timeWaited += Time.deltaTime;
                if (timeWaited >= durationUntilStart)
                {
                    state = BloekiState.START_MOVEMENT;
                }
                break;
            case BloekiState.START_MOVEMENT:
                state = BloekiState.MOVING;
                StartCoroutine("move");
                break;


        }
    }

    IEnumerator move()
    {
        while(state == BloekiState.MOVING)
        {
            if(transform.position.Equals(desiredPosition))
            {
                state = BloekiState.DONE;
            }
            iTween.MoveTo(gameObject, desiredPosition, movementDuration);
            yield return null;
        }
    }

    enum BloekiState
    {
        WAIT, START_MOVEMENT, MOVING, DONE
    }

    public void DoPulse()
    {
        iTween.PunchRotation(gameObject, new Vector3(0f, 0f, 2f), 1f);
    }
}
