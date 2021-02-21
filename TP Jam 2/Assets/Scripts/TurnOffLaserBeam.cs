using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLaserBeam : MonoBehaviour
{

  float timeTotal = 1f;
  public LineRenderer lineRenderer;
  public ParticleSystem particles;

  public void Off(){
      StartCoroutine(_Off());
  }


    private IEnumerator _Off()
    {
        float time = 0f;
        Vector3 startposition = lineRenderer.GetPosition(1);

        particles.Stop();

        while (time <= timeTotal)
        {
            time += Time.deltaTime;
            lineRenderer.SetPosition(1, Vector3.Lerp(startposition, Vector3.zero, time/timeTotal));
            yield return null;
        }
    }
}
