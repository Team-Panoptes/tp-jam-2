using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToOutro : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    public void Run()
    {
        SceneManager.LoadScene("03_Outro");
    }
}
