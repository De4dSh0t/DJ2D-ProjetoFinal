using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public IState currentState;

    public void SetState(IState state)
    {
        currentState = state;
        StartCoroutine(state.Execute());
    }
}
