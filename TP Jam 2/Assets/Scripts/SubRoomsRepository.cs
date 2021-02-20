using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoomsRepository : MonoBehaviour
{

    public List<GameObject> roomsList;
    public List<GameObject> finalRoomsList;
    public static List<GameObject> rooms;
    public static List<GameObject> finalRooms;
    private static SubRoomsRepository instance = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
            rooms = roomsList;
            finalRooms = finalRoomsList;
        }
    }

    public static GameObject GiveARoom(Transform parent, bool randomRotation = true)
    {
        if (randomRotation)
        {
            Quaternion rotation = Orientation.north;
            int rnd = Random.Range(0, 5);

            if (rnd == 0) rotation = Orientation.south;
            else if (rnd == 1) rotation = Orientation.west;
            else if (rnd == 2) rotation = Orientation.east;

            return GiveARoom(rotation, parent);

        }

        GameObject room = Instantiate(rooms[Random.Range(0, rooms.Count)]);
        room.transform.parent = parent;
        room.GetComponent<SubRoom>().Generate();
        return room;
    }

    public static GameObject GiveARoom(Quaternion rotation, Transform parent)
    {
        GameObject room = GiveARoom(parent, false);
        Orientation.ApplyOrientation(room, rotation);
        return room;
    }

    public static GameObject GiveAFinalRoom(Transform parent)
    {

        Quaternion rotation = Orientation.north;
        int rnd = Random.Range(0, 5);

        if (rnd == 0) rotation = Orientation.south;
        else if (rnd == 1) rotation = Orientation.west;
        else if (rnd == 2) rotation = Orientation.east;


        GameObject room = Instantiate(finalRooms[Random.Range(0, finalRooms.Count)]);
        room.transform.parent = parent;
        room.GetComponent<SubRoom>().Generate();
        Orientation.ApplyOrientation(room, rotation);
        return room;
    }
}
