using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public string[] code;

    public string[] availableLetters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y"};

    public int codeLength = 3;

    public TextMeshProUGUI[] codeDisplay;

    private List<string> proposal = new List<string>();

    public UnityEvent onCodeComplete;

    public bool useOverride;
    public string[] overrideCode;

    string[] GenerateCode() {
        System.Random rnd = new System.Random();
        Monument[] monuments = GameObject.FindObjectsOfType<Monument>().OrderBy(x => rnd.Next()).ToArray();

        string[] result = new string[codeLength];
        for (int i = 0; i < codeLength; i++) {
            string letter = availableLetters[UnityEngine.Random.Range(0, availableLetters.Length)];
            while (Array.IndexOf(result, letter) > -1) {
                letter = availableLetters[UnityEngine.Random.Range(0, availableLetters.Length)];
            }
            result[i] = letter;
            if (i < monuments.Length) {
                monuments[i].symbol = letter;
                monuments[i].number = i + 1;
            }
        }

        return result;
    }

    void Awake() {
        code = GenerateCode();
        if (useOverride) {
            code = overrideCode;
        }
        proposal.Clear();
        UpdateCodeDisplay();
    }


    public void EnterCode(string letter) {
        if (proposal.Count < codeLength) {
            proposal.Add(letter);
        } else {
            proposal.Clear();
        }
        UpdateCodeDisplay();
        if (VerifyCode()) {
            onCodeComplete?.Invoke();
        }
    }


    public bool VerifyCode() {
        bool valid = true;
        if (proposal.Count < codeLength) {
            return false;
        }
        for(int i = 0; i < proposal.Count; i++) {
            if (proposal[i] != code[i]) {
                valid = false;
                break;
            }
        }
        Debug.Log(valid);
        return valid;
    }

    public void UpdateCodeDisplay() {
        for(int i = 0; i < codeLength; i++) {
            if (i < proposal.Count) {
                codeDisplay[i].text = proposal[i];
            } else {
                codeDisplay[i].text = "";
            }
        }
    }

}
