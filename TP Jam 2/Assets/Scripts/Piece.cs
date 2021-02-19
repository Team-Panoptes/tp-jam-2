using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation
{
    public static Quaternion north = Quaternion.Euler(0, 0, 0);
    public static Quaternion south = Quaternion.Euler(0, 180, 0);
    public static Quaternion east = Quaternion.Euler(0, -90, 0);
    public static Quaternion west = Quaternion.Euler(0, 90, 0);

    public static void ApplyOrientation(GameObject item, Quaternion rotation)
    {
        Piece piece = item.GetComponent<Piece>();
        if (piece == null)
        {
            Debug.LogError("No Piece Data found in \"" + item.name + "\"", item);
        }
        if (rotation == east)
        {
            item.transform.rotation = rotation;
            item.transform.position += Vector3.right * piece.size.x;
            float tmp = piece.size.y;
            piece.size.y = piece.size.x;
            piece.size.x = tmp;


        }
        else if (rotation == west)
        {
            item.transform.rotation = rotation;
            item.transform.position += Vector3.forward * piece.size.y;
            float tmp = piece.size.y;
            piece.size.y = piece.size.x;
            piece.size.x = tmp;
        }
        else if (rotation == south)
        {
            item.transform.rotation = rotation;
            item.transform.position += Vector3.forward * piece.size.y + Vector3.right * piece.size.x;
        }
    }

}

public class Piece : MonoBehaviour
{
    [Header("Size")]
    public Vector3 size = Vector3.one;
}
