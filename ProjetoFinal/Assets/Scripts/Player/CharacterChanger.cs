using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private PlayerMovement[] characters;
    private int index;

    [Header("Camera Settings")]
    [SerializeField] private CameraBehaviour camera;
    
    //Components
    private ChangeCharacter change;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCharacter();
        }
    }

    private void ChangeCharacter()
    {
        //Enables the target controller
        change = new ChangeCharacter(characters[index], characters[NextIndex()]);
        change.Execute();
        
        //Updates the camera target
        camera.UpdateTarget(characters[index].transform);
    }

    private int NextIndex()
    {
        index++;
        
        if (index > characters.Length - 1)
        {
            index = 0;
        }
        
        return index;
    }
}
