using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [Header("Collision Settings")] 
    [SerializeField] private Vector2 size;
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

    private void CheckCollision()
    {
        Vector2 currentPos = transform.position;
        
        //Up
        canMoveUp = !Physics2D.Raycast(currentPos + new Vector2(offset.x, size.y), Vector2.left, distance, collisionLayer);
        
        //Left
        canMoveLeft = !Physics2D.Raycast(currentPos - new Vector2(size.x, size.y - (size.y - offset.y)), Vector2.up, distance, collisionLayer);
        
        //Down
        canMoveDown = !Physics2D.Raycast(currentPos - new Vector2(size.x - (size.x - offset.x), size.y), Vector2.right, distance, collisionLayer);
        
        //Right
        canMoveRight = !Physics2D.Raycast(currentPos + new Vector2(size.x, size.y - (size.y - offset.y)), Vector2.down, distance, collisionLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        //Up
        Gizmos.DrawLine((Vector2) transform.position + new Vector2(offset.x, size.y), (Vector2) transform.position + new Vector2(offset.x, size.y) + Vector2.left * distance);
        
        //Left
        Gizmos.DrawLine((Vector2) transform.position - new Vector2(size.x, size.y - (size.y - offset.y)), (Vector2) transform.position - new Vector2(size.x, size.y - (size.y - offset.y)) + Vector2.up * distance);
        
        //Down
        Gizmos.DrawLine((Vector2) transform.position - new Vector2(size.x - (size.x - offset.x), size.y), (Vector2) transform.position - new Vector2(size.x - (size.x - offset.x), size.y) + Vector2.right * distance);
        
        //Right
        Gizmos.DrawLine((Vector2) transform.position + new Vector2(size.x, size.y - (size.y - offset.y)), (Vector2) transform.position + new Vector2(size.x, size.y - (size.y - offset.y)) + Vector2.down * distance);
    }
}
