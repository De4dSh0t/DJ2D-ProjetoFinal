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
        int nextState = Random.Range(0, 3);

        switch (nextState)
        {
            case 0: // Idle
            {
                aiSystem.SetState(new IdleState(aiSystem));
                break;
            }
            case 1: // Choose Random Position
            {
                aiSystem.SetState(new RandomPositionState(aiSystem));
                break;
            }
            case 2: // Change Room
            {
                aiSystem.SetState(new ChangeRoomState(aiSystem, aiSystem.rooms[Random.Range(0, aiSystem.rooms.Length)]));
                break;
            }
        }
    }
}
