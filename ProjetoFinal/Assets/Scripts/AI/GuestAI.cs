using UnityEngine;

public class GuestAI : AISystem
{
    void Start()
    {
        DecisionMaking();
    }
    
    public override void DecisionMaking()
    {
        int rI = Random.Range(0, 2);

        switch (rI)
        {
            case 0:
            {
                SetState(new RandomPositionState(this, CurrentRoom));
                break;
            }
            case 1:
            {
                SetState(new ChangeRoomState(this, rooms[Random.Range(0, rooms.Length)]));
                break;
            }
        }
    }
}
