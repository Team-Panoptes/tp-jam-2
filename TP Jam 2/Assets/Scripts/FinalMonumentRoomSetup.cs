using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalMonumentRoomSetup : MonoBehaviour
{

    private Monument monument;

    public TextMeshProUGUI number;
    public TextMeshProUGUI letter;
    public TextMeshProUGUI button;


    // Start is called before the first frame update
    void Start()
    {
        monument = GetComponentInParent<Monument>();
        letter.text = monument.symbol;
        button.text = monument.symbol;
        number.text = "";
        for (int i = 0; i < monument.number; i++) {
            number.text += "|";
        }
    }


    public void TeleportPlayer() {
        GameObject.FindWithTag("Player").transform.position = Vector3.zero;
    }

}
