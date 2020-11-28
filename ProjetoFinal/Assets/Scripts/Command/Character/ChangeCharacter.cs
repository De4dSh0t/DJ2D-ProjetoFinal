public class ChangeCharacter : ICommand
{
    private readonly PlayerMovement current;
    private readonly PlayerMovement target;

    public ChangeCharacter(PlayerMovement current, PlayerMovement target)
    {
        this.current = current;
        this.target = target;
    }

    public void Execute()
    {
        current.enabled = false;
        target.enabled = true;
    }
}
