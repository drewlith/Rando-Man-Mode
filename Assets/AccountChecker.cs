using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AccountChecker : MonoBehaviour
{
    public GameObject editWindow;
    public Text[] uiText;
    public Text accountName;
    public InputField newAccountName;
    public Button[] loadButtons;
    public Button[] editButtons;
    Randomizer r;

    void Start() {
        r = FindObjectOfType<Randomizer>();
        CheckForAccountData();
    }
    void CheckForAccountData()
    {
        accountName.text = r.accountName;
        string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/", "*.txt");
        for (int i = 0; i < files.Length; i++) {
            files[i] = files[i].Replace(Application.dataPath + "/", "");
            files[i] = files[i].Replace(".txt", "");
            loadButtons[i].gameObject.SetActive(true);
            editButtons[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < uiText.Length; i++) {
            if (i >= files.Length) {
                break;
            }
            uiText[i].text = files[i];
        }
    }

    public void Save() {
        r.accountName = newAccountName.text;
        r.Save(r.accountName);
        CheckForAccountData();
        accountName.text = r.accountName;
    }

    public void Load(int id) {
        r.Load(uiText[id].text);
        r.accountName = uiText[id].text;
        accountName.text = r.accountName;
        newAccountName.text = r.accountName;
    }

    public void Edit(int id) {
        Load(id);
        editWindow.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
