using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    // Remove the Update() method as the movement check is handled in PlayerMovement script
}
