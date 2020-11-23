using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [Header("Collision Settings")] 
    [SerializeField] private Vector2 offset;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask collisionLayer;
    
    public bool canMoveUp;
    public bool canMoveLeft;
    public bool canMoveDown;
    public bool canMoveRight;

    void Update()
    {
        CheckCollision();
    }

    private void CheckCollision() // TODO: Fix clipping
    {
        Vector2 currentPos = transform.position;
        
        //Up
        canMoveUp = !Physics2D.Raycast(currentPos - new Vector2(offset.x, 0), Vector2.up, distance, collisionLayer) &&
                    !Physics2D.Raycast(currentPos + new Vector2(offset.x, 0), Vector2.up, distance, collisionLayer);
        
        //Left
        canMoveLeft = !Physics2D.Raycast(currentPos - new Vector2(0, offset.y), Vector2.left, distance, collisionLayer) &&
                      !Physics2D.Raycast(currentPos + new Vector2(0, offset.y), Vector2.left, distance, collisionLayer);
        
        //Down
        canMoveDown = !Physics2D.Raycast(currentPos - new Vector2(offset.x, 0), Vector2.down, distance, collisionLayer) &&
                      !Physics2D.Raycast(currentPos + new Vector2(offset.x, 0), Vector2.down, distance, collisionLayer);
        
        //Right
        canMoveRight = !Physics2D.Raycast(currentPos - new Vector2(0, offset.y), Vector2.right, distance, collisionLayer) &&
                       !Physics2D.Raycast(currentPos + new Vector2(0, offset.y), Vector2.right, distance, collisionLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine((Vector2) transform.position - new Vector2(0, offset.y), (Vector2) transform.position - new Vector2(0, offset.y) + Vector2.left * distance);
        Gizmos.DrawLine((Vector2) transform.position + new Vector2(0, offset.y), (Vector2) transform.position + new Vector2(0, offset.y) + Vector2.left * distance);
    }
}
