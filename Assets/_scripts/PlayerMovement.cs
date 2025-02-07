using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // index definition for lanes -> 0, 1, 2 -> starts in the middle one
    int currentLane = 0;
    // desired lane position with movement
    Vector3 targetPos = Vector3.zero;
    // control variable of how fast player can change lanes
    [SerializeField] float changeSpeed;

    [SerializeField] LanesManager lanesManager;

    // control variable for jumping state
    bool isJumping = false;
    // control variable for jumping state
    bool isSliding = false;

    // control variables for jumping state
    [Header("Jump attributes")]
    float jumpStartTime;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpDuration = 2f;

    // control variables for sliding state
    [Header("Slider attributes")]
    float sldieStartTime = 2f;
    [SerializeField] float slideLength = 2f;

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        // do jumping movement
        if (isJumping)
        {
            // get time passed from jump start
            float elapsedTime = Time.time - jumpStartTime;
            // clamps 0 to 1
            float ratio = elapsedTime / jumpDuration;

            if (ratio >= 1f)
            {
                isJumping = false;
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z); // Retorna ao chão
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Mathf.Sin(ratio * Mathf.PI) * jumpHeight, transform.position.z);
            }
        }else
        {
            this.transform.position = Vector3.MoveTowards(transform.position, targetPos, changeSpeed * Time.deltaTime);
        }

        // do lane change
        this.transform.position = Vector3.MoveTowards(transform.position, targetPos, changeSpeed * Time.deltaTime);
    }

    void CheckInput()
    {
        // move right
        if (Input.GetKeyDown(KeyCode.D)) ChangeLane(1);
        // move left
        if (Input.GetKeyDown(KeyCode.A)) ChangeLane(-1);
        // jump
        if (Input.GetKeyDown(KeyCode.W)) Jump();
        // slide
        if (Input.GetKeyDown(KeyCode.S)) Slide();
    }

    void ChangeLane(int direction)
    {
        // clamp lane position
        int targetLane = currentLane + direction;
        // return if lane is incorrect criteria
        if (targetLane < -1 || targetLane > 1) return;
        // set current lane
        currentLane = targetLane;
        // set player target position
        targetPos = lanesManager.GetLanePosition(currentLane);
    }

    void Jump()
    {
        if (!isJumping)
        {
            jumpStartTime = Time.time;
            // animate
            isJumping = true;
        }
    }

    void Slide()
    {
        if (!isJumping && !isSliding)
        {
            isSliding = true;
        }
    }
}
