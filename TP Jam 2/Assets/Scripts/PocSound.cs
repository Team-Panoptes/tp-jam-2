using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocSound : MonoBehaviour
{
    // Start is called before the first frame update
public List<AudioSource> songs;
int index = 0;

/// <summary>
/// OnCollisionEnter is called when this collider/rigidbody has begun
/// touching another rigidbody/collider.
/// </summary>
/// <param name="other">The Collision data associated with this collision.</param>
void OnCollisionEnter(Collision other)
{
    if(!songs[index].isPlaying){
        index = Random.Range(0, songs.Count);
        songs[index].Play();
    }
}
}
