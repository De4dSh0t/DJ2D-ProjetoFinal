using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected IState currentState;

    public void SetState(IState state)
    {
        currentState = state;
        state.Execute();
    }
}
