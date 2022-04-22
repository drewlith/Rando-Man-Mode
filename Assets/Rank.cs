using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{   
    static bool setRankDisplay;
    static string rankString;
    public Text rankDisplay;
    public Text rankShadow;
    public Color[] rankColors;
    public Image[] imageDisplay;

    void Update() {
        if (setRankDisplay) {
            setRankDisplay = false;
            rankDisplay.text = rankString;
            rankShadow.text = rankString;
            rankDisplay.color = rankColors[Randomizer.difficulty];
            DetermineImageToDisplay();
        }
    }

    public static void CheckRank() {
        if (Randomizer.difficulty == 0) {
            rankString = "Bronze";
        }
        if (Randomizer.difficulty == 1) {
            rankString = "Iron";
        }
        if (Randomizer.difficulty == 2) {
            rankString = "Steel";
        }
        if (Randomizer.difficulty == 3) {
            rankString = "Mithril";
        }
        if (Randomizer.difficulty == 4) {
            rankString = "Adamant";
        }
        if (Randomizer.difficulty == 5) {
            rankString = "Rune";
        }
        if (Randomizer.difficulty == 6) {
            rankString = "Dragon";
        }
        if (Randomizer.difficulty == 7) {
            rankString = "Spectral";
        }
        if (Randomizer.difficulty == 8) {
            rankString = "Arcane";
        }
        if (Randomizer.difficulty == 9) {
            rankString = "Elysian";
        }
        if (Randomizer.difficulty == 10) {
            rankString = "Divine";
        }
        setRankDisplay = true;
    }

    void DetermineImageToDisplay() {
        for (int i = 0; i < imageDisplay.Length; i++) {
            imageDisplay[i].gameObject.SetActive(false);
        }
        imageDisplay[Randomizer.difficulty].gameObject.SetActive(true);
    }
}
