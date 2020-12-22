using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public Coroutine SetState(IState state)
    {
        return StartCoroutine(state.Execute());
    }
}
