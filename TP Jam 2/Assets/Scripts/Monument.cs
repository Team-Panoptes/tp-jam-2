using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : Room
{

    [Header("Size")]
    public Vector3 minSize = new Vector3(10, 10, 10);
    public Vector3 maxSize = new Vector3(30, 30, 30);
    [Header("Code")]
    public string symbol;
    public override void Generate()
    {
        if(isGenerated)return;
        DefineSize();
        AddSubRooms();
        base.Generate();
    }

    protected void DefineSize()
    {
        int x = Random.Range((int)minSize.x, (int)maxSize.x + 1);
        int y = Random.Range((int)minSize.y, (int)maxSize.y + 1);
        int z = Random.Range((int)minSize.z, (int)maxSize.z + 1);
        size = new Vector3(x, y, z);
    }
    protected override void PlaceEntry()
    {

        //test which edges entry will touches

        int x = Random.Range(0, (int)(size.x / 2));
        int y = Random.Range(0, Mathf.Min((int)size.y - 2, 5));

        doorInPlace = new Vector3(x, y, 0);

        base.PlaceEntry();
    }


    protected void AddSubRooms()
    {
        if (SubRoomsRepository.rooms.Count == 0)
        {
            Debug.LogError("No sub rooms found", gameObject);
            return;
        }
        Vector3 newSize = Vector3.zero;
        newSize.y = size.y;

        Vector3 cursor = doorInPlace;
        cursor.y = Random.Range(0, doorInPlace.y + 1);

        int y = 0;
        GameObject room;
        SubRoom subRoom;
        while (cursor.y < size.y - 4)
        {
            room = SubRoomsRepository.GiveARoom(transform);
            subRoom = room.GetComponent<SubRoom>();
            room.transform.localPosition = cursor + Orientation.Decal(room, room.transform.rotation);            


            y = Mathf.Max((int)subRoom.size.y, y);
            
            if(Random.Range(0,2)==0){
            cursor.x += subRoom.size.x;
            newSize.x = Mathf.Max(newSize.x, cursor.x);
            newSize.z = Mathf.Max(newSize.z, cursor.z + subRoom.size.z);
            }
            else {    
            cursor.z += subRoom.size.z;
            newSize.z = Mathf.Max(newSize.z, cursor.z);
            newSize.x = Mathf.Max(newSize.x, cursor.x + subRoom.size.x);
            }

            if((cursor.x > size.x || cursor.z > size.z)){

                if(cursor.y + y < size.y - 4)
                    {
                        int x = Mathf.Max((int)doorInPlace.x + (int)Random.Range(-2, 3), 0);
                        cursor = new Vector3(x, cursor.y + y, 0);}
                else{
                    cursor.y += y;
                }

                y = 0;
            }
        }
        room = SubRoomsRepository.GiveAFinalRoom(transform);
        subRoom = room.GetComponent<SubRoom>();
        room.transform.localPosition = cursor + Orientation.Decal(room, room.transform.rotation);            

        cursor += subRoom.size;
        newSize.x = Mathf.Max(newSize.x, cursor.x);
        newSize.z = Mathf.Max(newSize.z, cursor.z);
        newSize.y = cursor.y;
        size = newSize;
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Color color = this.color;
        color.a = 0.25f;
        Gizmos.color = color;
        Vector3 decal = Orientation.Decal(gameObject, transform.rotation) * -1;
        Gizmos.DrawWireCube(size / 2 + transform.position + decal, size + Vector3.one * 2);
    }

}
