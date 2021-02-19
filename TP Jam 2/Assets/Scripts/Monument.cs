using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : Room
{

    [Header("Size")]
    public Vector3 minSize = new Vector3(10, 10, 10);
    public Vector3 maxSize = new Vector3(30, 30, 30);

    public override void Generate()
    {
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

        int x = Random.Range(0, (int)(size.x/2));
        int y = Random.Range(0, Mathf.Min((int)size.y - 2, 5));

        doorInPlace = new Vector3(x, y ,0);

        base.PlaceEntry();
    }


    protected void AddSubRooms(){
        if(SubRoomsRepository.rooms.Count == 0) {
            Debug.LogError("No sub rooms found", gameObject);
            return;}
        Vector3 sizeMax = size;
        Vector3 cursor = doorInPlace;
        cursor.y = Random.Range(0, doorInPlace.y + 1);

        while(cursor.y < size.y-4) {
            GameObject room = SubRoomsRepository.GiveARoom();
            room.transform.parent = transform;
            room.transform.position = cursor;

            SubRoom subRoom = room.GetComponent<SubRoom>();
            cursor.y += subRoom.size.y;
        }
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Color color = Color.green;
        color.a = 0.25f;
        Gizmos.color = color;
        Gizmos.DrawWireCube(size / 2 + transform.position, size + Vector3.one*2);
    }

    

 
}
