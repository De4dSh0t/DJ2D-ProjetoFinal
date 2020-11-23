using UnityEngine;

public class MoveRight : ICommand
{
    private readonly Transform pos;
    private readonly float speed;

    public MoveRight(Transform p, float s)
    {
        pos = p;
        speed = s;
    }
    
    public void Execute()
    {
        pos.Translate(Vector2.right * (speed * Time.deltaTime));
    }
}
