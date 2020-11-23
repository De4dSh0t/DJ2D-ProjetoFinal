using UnityEngine;

public class MoveUp : ICommand
{
    private readonly Transform pos;
    private readonly float speed;

    public MoveUp(Transform p, float s)
    {
        pos = p;
        speed = s;
    }
    
    public void Execute()
    {
        pos.Translate(Vector2.up * (speed * Time.deltaTime));
    }
}
