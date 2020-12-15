using System.Collections;
using UnityEngine;

public class ChooseState : IState
{
    private readonly AISystem aiSystem;

    public ChooseState(AISystem system)
    {
        aiSystem = system;
    }
    
    public IEnumerator Execute()
    {
        ChooseNextState();
        yield break;
    }

    private void ChooseNextState()
    {
        int nextState = Random.Range(0, 2);

        switch (nextState)
        {
            case 0: // Idle
            {
                aiSystem.SetState(new IdleState(aiSystem));
                break;
            }
            case 1: // Find
            {
                aiSystem.SetState(new FindState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, aiSystem.WaypointInt));
                break;
            }
        }
    }
}
