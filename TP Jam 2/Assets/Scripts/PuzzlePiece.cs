using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : Piece
{
    public List<GameObject> items;
    public float coverage = 1f;
    public bool randomRotation = true;
    public bool applyRotation = false;
    public Color color = Color.green;
    public bool isFilled = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Fill()
    {
        if(isFilled)return;
        if (items.Count == 0 || coverage <= 0) return;
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    if (Random.Range(0f, 1f) <= coverage)
                    {
                        Quaternion rotation = Orientation.north;
                        if (randomRotation) rotation = Orientation.RandomOrientation();
                        Vector3 position = new Vector3(x, y, z) + transform.position;
                        GameObject item = ChooseOne(items, position, rotation);
                    }
                }
            }
        }
        isFilled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = color;
        if(applyRotation){
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = matrix;
        Gizmos.DrawWireCube(size / 2, size);}
        else{
        Vector3 decal = Orientation.Decal(gameObject, transform.rotation) * -1;
        Gizmos.DrawWireCube(size / 2 + transform.position + decal, size);}

    }
}
