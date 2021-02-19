using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoomsRepository : MonoBehaviour
{

    public List<GameObject> roomsList;
    public static List<GameObject> rooms;
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
        }
    }

    public static GameObject GiveARoom(bool randomRotation=true){
        if(randomRotation){
            Quaternion rotation = Orientation.north;
            int rnd = Random.Range(0,5);
            
            if(rnd==0) rotation = Orientation.south;
            else if(rnd==1) rotation = Orientation.west;
            else if(rnd==2) rotation = Orientation.east;

            return GiveARoom(rotation);
            
        }

        GameObject room = rooms[Random.Range(0, rooms.Count)];
        return Instantiate(room);
    }

    public static GameObject GiveARoom(Quaternion rotation){
        GameObject room = GiveARoom(false);
        Orientation.ApplyOrientation(room, rotation);
        return room;
    }
}
