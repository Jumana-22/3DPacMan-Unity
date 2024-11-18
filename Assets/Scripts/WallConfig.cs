using UnityEngine;

[System.Serializable]
public class Wall
{
    public float x;
    public float y;
    public float z;
    public bool rotateLeft; // Determines if the wall should be rotated 90 degrees left
}

[System.Serializable]
public class WallConfig
{
    public Wall[] walls;
}
