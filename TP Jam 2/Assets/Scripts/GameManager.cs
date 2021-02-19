using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static string[] code;

    public string[] availableLetters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y"};

    public static int codeLength = 3;

    public TextMeshProUGUI codeDisplay;

    private List<string> proposal = new List<string>();

    string[] GenerateCode() {
        string[] result = new string[codeLength];
        for (int i = 0; i < codeLength; i++) {
            string letter = availableLetters[UnityEngine.Random.Range(0, availableLetters.Length)];
            while (Array.IndexOf(result, letter) > -1) {
                letter = availableLetters[UnityEngine.Random.Range(0, availableLetters.Length)];
            }
            result[i] = letter;
            Debug.Log(letter);
        }

        return result;
    }

    void Awake() {
        code = GenerateCode();
        proposal.Clear();
    }


    public void EnterCode(string letter) {
        if (proposal.Count < codeLength) {
            proposal.Add(letter);
        } else {
            proposal.Clear();
        }
        UpdateCodeDisplay();
    }


    public void UpdateCodeDisplay() {
        codeDisplay.text = String.Join("", proposal);
    }

}
