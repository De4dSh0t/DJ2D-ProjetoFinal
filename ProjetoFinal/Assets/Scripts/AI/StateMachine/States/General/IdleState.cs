﻿using System.Collections;
using UnityEngine;

public class IdleState : IState
{
    private readonly AISystem aiSystem;

    public IdleState(AISystem system)
    {
        aiSystem = system;
    }
    
    public IEnumerator Execute()
    {
        Idle();
        yield return new WaitForSecondsRealtime(2);
        aiSystem.SendMessage("DecisionMaking");
    }

    private void Idle()
    {
        // Idle Behaviour
        aiSystem.Animator.SetBool("isWalking", false);
    }
}