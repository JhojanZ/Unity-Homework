using System;
using UnityEngine;
public class ThrowHook : MonoBehaviour
{
    public float throwForce = 30f;
    private const float CharacterHeight = 1.5f;

    public void OnEnable()
    {
        
    }
    public void Main()
    {
        Throw();
        Debug.Log("Throw Hooks is activate!");
    }

    public void Throw()
    {
        GameObject objectToThrow = GameObject.FindWithTag("Rock");
        if (objectToThrow != null)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= CharacterHeight;
            GameObject thrownObject = Instantiate(objectToThrow, spawnPosition, transform.rotation);
        }
        else
        {
            Debug.LogWarning("No object with tag 'Rock' found.");
        }
    }
}
