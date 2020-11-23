using UnityEngine;

public class MoveDown : ICommand
{
    private readonly Transform pos;
    private readonly float speed;

    public MoveDown(Transform p, float s)
    {
        pos = p;
        speed = s;
    }
    
    public void Execute()
    {
        pos.Translate(Vector2.down * (speed * Time.deltaTime));
    }
}
