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

    public static GameObject GiveARoom(){
        GameObject room = rooms[Random.Range(0, rooms.Count)];

        return Instantiate(room);
    }
}
