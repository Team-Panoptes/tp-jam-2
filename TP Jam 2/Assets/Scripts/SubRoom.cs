using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoom : Room
{

    public enum RoomType {line, corner};
    RoomType roomType;
    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
