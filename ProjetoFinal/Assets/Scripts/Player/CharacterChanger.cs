using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private PlayerMovement[] characters;
    private int index;
    
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
        change = new ChangeCharacter(characters[index], characters[NextIndex()]);
        change.Execute();
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
