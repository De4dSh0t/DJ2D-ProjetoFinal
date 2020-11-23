using UnityEngine;

public class MoveLeft : ICommand
{
    private readonly Transform pos;
    private readonly float speed;

    public MoveLeft(Transform p, float s)
    {
        pos = p;
        speed = s;
    }
    
    public void Execute()
    {
        pos.Translate(Vector2.left * (speed * Time.deltaTime));
    }
}
