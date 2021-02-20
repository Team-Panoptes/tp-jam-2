using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation
{
    public static Quaternion north = Quaternion.Euler(0, 0, 0);
    public static Quaternion south = Quaternion.Euler(0, 180, 0);
    public static Quaternion east = Quaternion.Euler(0, 90, 0);
    public static Quaternion west = Quaternion.Euler(0, -90, 0);

    public static Vector3 Decal (GameObject item, Quaternion rotation){
        Piece piece = item.GetComponent<Piece>();
        if (piece == null)
        {
            Debug.LogError("No Piece Data found in \"" + item.name + "\"", item);
            return Vector3.zero;
        }

        if (rotation == east) return Vector3.forward * piece.size.z;
        if (rotation == west) return Vector3.right * piece.size.x;
        if(rotation == south) return Vector3.forward * piece.size.z + Vector3.right * piece.size.x;
        return Vector3.zero;
    }

    public static void ApplyOrientation(GameObject item, Quaternion rotation)
    {
        Piece piece = item.GetComponent<Piece>();
        if (piece == null)
        {
            Debug.LogError("No Piece Data found in \"" + item.name + "\"", item);
            return;
        }
        if (rotation == east || rotation == west)
        {
            float tmp = piece.size.z;
            piece.size.z = piece.size.x;
            piece.size.x = tmp;
        }

        item.transform.rotation = rotation;
        item.transform.position += Decal(item, rotation);
    }
}

public class Piece : MonoBehaviour
{
    [Header("Size")]
    public Vector3 size = Vector3.one;
    protected bool isGenerated = false;
    public virtual void Generate()
    {if(isGenerated)return;}
}
