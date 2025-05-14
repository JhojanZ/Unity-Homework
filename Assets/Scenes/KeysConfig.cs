using System;
using System.Collections.Generic;
using UnityEngine;

public class KeysConfig
{
    public Dictionary<string, KeyCode> AssignedKey { get; private set; }
    public KeysConfig()
    {
        AssignedKey = new Dictionary<string, KeyCode>
        {
            { "Left", KeyCode.LeftArrow },
            { "Right", KeyCode.RightArrow },
            { "Up", KeyCode.UpArrow },
            { "Down", KeyCode.DownArrow },
            { "Jump", KeyCode.Z },
            { "Get", KeyCode.X },
            { "Especial", KeyCode.C }

        };
    }

    public KeyCode GetCodeKey(string action)
    {
        if (AssignedKey.TryGetValue(action, out KeyCode key))
        {
            return key;
        }
        throw new ArgumentException($"No key binding found for action: {action}");
    }

    public float GetHorizontalAxis()
    {
        float axis = 0;
        if (Input.GetKey(GetCodeKey("Left")))
        {
            axis -= 1;
        }
        if (Input.GetKey(GetCodeKey("Right")))
        {
            axis += 1;
        }
        return axis;
    }
}
