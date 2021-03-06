using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : Piece
{
    [Header("Prefabs")]
    public List<GameObject> entryPrefabs;
    public List<GameObject> groundPrefabs;
    public List<GameObject> wallPrefabs;

    public List<GameObject> roofPrefabs;

    [Header("Coverage")]
    public bool groundInside = false;
    public bool roofInside = false;
    public float groundCoverage = 1f;
    public float roofCoverage = 1f;
    public float northCorerage = 1f;
    public float eastCorerage = 1f;
    public float southCorerage = 1f;
    public float westCorerage = 1f;

    [Header("Others")]
    public Vector3 doorInPlace = Vector3.zero;
    public Vector3 doorOutPlace = Vector3.one;
    [Header("color")]
    public Color color = Color.green;
    protected virtual void Start(){
        Generate();
    }

    public override void Generate()
    {
        if(isGenerated) return;
        foreach(PuzzlePiece piece in GetComponentsInChildren<PuzzlePiece>()){
            piece.Fill();
        }
        PlaceEntry();

        PlaceGround();
        PlaceRoof();
        PlaceWallX(-1, westCorerage, Orientation.east);
        PlaceWallX((int)size.x, eastCorerage, Orientation.west);
        PlaceWallZ(-1, southCorerage, Orientation.north);
        PlaceWallZ((int)size.z, northCorerage, Orientation.south);

        isGenerated = true;
    }

    protected Quaternion StickToTheAWall(Vector3 position){
        int x = (int)position.x;
        int z = (int)position.z;

        bool stickX = false;
        bool stickZ = false;
        if(x == 0 || x == (int)size.x - 1) stickX = true;
        if(z == 0 || z == (int)size.z - 1) stickZ = true;

        if(stickX && stickZ){
            if(Random.Range(0,2) == 0) stickX = false;
            else stickZ = false;
        }

        if(stickX){
            if(x == 0){
                return Orientation.east;
            }
            return Orientation.west;
        }

        if(stickZ){
            if(z == 0){
                return Orientation.south;
            }
        }

        return Orientation.north;

    }

    protected virtual void PlaceEntry()
    {
        if(entryPrefabs.Count == 0)return;
        Quaternion rotation = StickToTheAWall(doorInPlace);
        ChooseOne(entryPrefabs, doorInPlace, rotation);
    }

    protected virtual void PlaceGround()
    {
        if (groundPrefabs.Count == 0 || groundCoverage <= 0f) return;

        int y = -1;
        int startX = -1;
        int startZ = -1;
        int endX = (int)size.x + 1;
        int endZ = (int)size.z + 1;

        if(groundInside) {
            y = 0;
            startX = 0;
            startZ = 0;
            endX -= 1;
            endZ -= 1;}

        for (int x = startX; x < endX; x++)
        {
            for (int z = startZ; z < endZ; z++)
            {
                if(Random.Range(0f, 1f) <= groundCoverage){
                    Vector3 position = new Vector3(x, y , z)  + transform.position;
                    ChooseOne(groundPrefabs, position, Orientation.north);
                }
            }
        }
    }

    protected virtual void PlaceRoof()
    {
        if (roofPrefabs.Count == 0 || roofCoverage <= 0f) return;

        int y = (int)size.y;
        int startX = -1;
        int startZ = -1;
        int endX = (int)size.x + 1;
        int endZ = (int)size.z + 1;

        if(roofInside) {
            y -= 1;
            startX = 0;
            startZ = 0;
            endX -= 1;
            endZ -= 1;}

        for (int x = startX; x < endX; x++)
        {
            for (int z = startZ; z < endZ; z++)
            {
                if(Random.Range(0f, 1f) <= roofCoverage){
                    Vector3 position = new Vector3(x, y, z) + transform.position;
                    ChooseOne(roofPrefabs, position, Orientation.north);
                }
            }
        }
    }

    protected virtual void PlaceWallX(int x, float coverage, Quaternion rotation)
    {
        if (wallPrefabs.Count == 0 ) return;
        if(coverage <= 0f) return;

        for (int y = 0; y < size.y; y++)
        {
            for (int z = 0; z < size.z + 1; z++)
            {
                if (!IsDoorPlace(x, y, z, doorInPlace) && !IsDoorPlace(x, y, z, doorOutPlace))
                {
                    if (Random.Range(0f, 1f) <= coverage)
                    {
                        Vector3 position = new Vector3(x, y, z)  + transform.position;
                        ChooseOne(wallPrefabs, position, rotation);
                    }
                }
            }
        }
    }

    protected virtual void PlaceWallZ(int z, float coverage, Quaternion rotation)
    {
        if (wallPrefabs.Count == 0) return;
        if (coverage <= 0f) return;

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x + 1; x++)
            {
                if (!IsDoorPlace(x, y, z, doorInPlace) && !IsDoorPlace(x, y, z, doorOutPlace))
                {
                    if (Random.Range(0f, 1f) <= coverage)
                    {
                        Vector3 position = new Vector3(x, y, z)  + transform.position;
                        ChooseOne(wallPrefabs, position, rotation);
                    }
                }
            }
        }
    }

    private bool IsDoorPlace(int x, int y, int z, Vector3 doorPlace)
    {
        if (y == doorPlace.y || y == doorPlace.y + 1)
        {
            if (doorPlace.x == 0 && x == -1 && z == doorPlace.z) return true;
            if (doorPlace.x == size.x - 1 && x == size.x && z == doorPlace.z) return true;
            if (doorPlace.z == 0 && z == -1 && x == doorPlace.x) return true;
            if (doorPlace.z == size.z - 1 && z == size.z && x == doorPlace.x) return true;
        }

        return false;
    }

    protected virtual void OnDrawGizmosSelected()
    {

        Color color = this.color;
        Gizmos.color = color;
        Vector3 decal = Orientation.Decal(gameObject, transform.rotation) * -1;
        Gizmos.DrawWireCube(size / 2 + transform.position + decal, size);

        if(groundInside || roofInside)
            Gizmos.DrawWireCube(size / 2 + transform.position + decal, size - Vector3.up * 2);

    }
}
