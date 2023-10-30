using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Check if the player is moving
        bool isMoving = Mathf.Abs(playerMovement.Horizontal) > 0f;

        // Set the "IsMoving" parameter in the Animator
        animator.SetBool("IsMoving", isMoving);
    }
}
