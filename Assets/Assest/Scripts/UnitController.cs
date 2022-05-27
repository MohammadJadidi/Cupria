using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    Vector2 startPos, endPos, direction;
    private Vector3 startPosGameObj;
    Rigidbody2D myRigidbody2D;
    public float shootPower = 1.5f;
    private Vector2 lastVelocity;
    private int teamID;
    [SerializeField] private GameController _gameController;

    void Start()
    {
        teamID = GetComponent<Team>().GetTeamID();
        startPosGameObj = gameObject.transform.position;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public Vector3 GetStartPosition()
    {
        return startPosGameObj;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
    }

    private void Update()
    {
        lastVelocity = myRigidbody2D.velocity;
    }

    public Vector2 getLastVelocity()
    {
        return lastVelocity;
    }

    void OnMouseUp()
    {
        if(_gameController.GetTurnID() == teamID)
        {
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            direction = startPos - endPos;
            myRigidbody2D.isKinematic = false;
            myRigidbody2D.AddForce(direction * shootPower);
            _gameController.ChangeTurn();
        }
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(myRigidbody2D.velocity.x) < 0.8f || Mathf.Abs(myRigidbody2D.velocity.y) < 0.8f)
        {
            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.angularVelocity = 0f;
        }
    }
}