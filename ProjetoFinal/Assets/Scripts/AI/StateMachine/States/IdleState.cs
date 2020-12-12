using System.Collections;
using UnityEngine;

public class IdleState : IState
{
    private AISystem aiSystem;

    public IdleState(AISystem system)
    {
        aiSystem = system;
    }
    
    public IEnumerator Execute()
    {
        Idle();
        yield return new WaitForSeconds(1);
    }

    private void Idle()
    {
        Debug.Log("Idle");
    }
}
