using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject redPipe;
    [SerializeField] private TextMeshProUGUI TextCost;
    [SerializeField] private TextMeshProUGUI Highscore_Text;
    [SerializeField] private TextMeshProUGUI NoMoneyText;
    [SerializeField] private TextMeshProUGUI OrbsText;
    [SerializeField] private GameObject DefSkin;
    [SerializeField] private Texture[] SkinImages = new Texture[5];
    private int skinpoint = 0;


    private void Awake()
    {
        if (redPipe == null) return;
        
    }   

    void Start()
    {
        // hotfix aby editor nehadzal error v main menu
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            OrbsText.text = "Orbs: "+PlayerPrefs.GetInt("Currency", 0);
            DefSkin.GetComponent<RawImage>().texture = SkinImages[0];
            Highscore_Text.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }
     
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenLeaderboards()
    {
        SceneManager.LoadScene("Leaderboards");
    }

    public void OpenProfile()
    {
        SceneManager.LoadScene("Profile");
    }

    public void DisableSound()
    {
        // ak playerprefs sound neexistuje, nastavi ho defaultne na 0
        int sound = PlayerPrefs.GetInt("Sound", 0);
        if (sound == 0)
        {
            redPipe.SetActive(false);
            PlayerPrefs.SetInt("Sound", 1);
            Debug.Log(PlayerPrefs.GetInt("Sound"));
        }
        else
        {
            redPipe.SetActive(true);
            PlayerPrefs.SetInt("Sound", 0);
            Debug.Log(PlayerPrefs.GetInt("Sound"));
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RightArrow()
    {
        if (skinpoint >= 4) skinpoint = 0;
        else skinpoint++;
        DefSkin.GetComponent<RawImage>().texture = SkinImages[skinpoint];
        ChangeCostOfSkinText();
        if ((PlayerPrefs.GetInt("Skin")) == skinpoint)
        {
            TextCost.text = "SELECTED";
        }
    }

    public void LeftArrow()
    {
        if (skinpoint <= 0) skinpoint = 4;
        else skinpoint--;
        DefSkin.GetComponent<RawImage>().texture = SkinImages[skinpoint];
        ChangeCostOfSkinText();
        if ((PlayerPrefs.GetInt("Skin")) == skinpoint)
        {
            TextCost.text = "SELECTED";
        }
    }

    private void ChangeCostOfSkinText()
    {
        string ownedSkins = PlayerPrefs.GetString("OwnedSkins", "10000");

        switch (skinpoint)
        {
            case 4:
                if (ownedSkins[4] == '1')
                    TextCost.text = "OWNED";
                else TextCost.text = "cost: 20000";
                break;
            case 3:
                if (ownedSkins[3] == '1')
                    TextCost.text = "OWNED";
                else TextCost.text = "cost: 10000";
                break;
            case 2:
                if (ownedSkins[2] == '1')
                    TextCost.text = "OWNED";
                else TextCost.text = "cost: 5000";
                break;
            case 1:
                if (ownedSkins[1] == '1')
                    TextCost.text = "OWNED";
                else TextCost.text = "cost: 1000";
                break;
            case 0:
                TextCost.text = "OWNED";
                break;
        }
    }

    public void SelectBuySkin()
    {
        string ownedSkins = PlayerPrefs.GetString("OwnedSkins", "10000");
        char[] temp = ownedSkins.ToCharArray();
        int currency = PlayerPrefs.GetInt("Currency", 0);

        

        switch (skinpoint)
        {
            case 4:
                if (ownedSkins[4] != '1' && currency >= 20000)
                {
                    currency -= 20000;
                    temp[4] = '1';
                    ownedSkins = new string(temp);
                    PlayerPrefs.SetString("OwnedSkins", ownedSkins);
                    PlayerPrefs.SetInt("Skin", 4);
                    TextCost.text = "OWNED";
                    break;
                }
                else if (ownedSkins[4] == '1')
                {
                    PlayerPrefs.SetInt("Skin", 4);
                    TextCost.text = "SELECTED";
                    break;
                }
                else
                    StartCoroutine(ShowNotificationNoOrbs());
                    break;

            case 3:
                if (ownedSkins[3] != '1' && currency >= 10000)
                {
                    currency -= 10000;
                    temp[3] = '1';
                    ownedSkins = new string(temp);
                    PlayerPrefs.SetString("OwnedSkins", ownedSkins);
                    PlayerPrefs.SetInt("Skin", 3);
                    TextCost.text = "OWNED";
                    break;
                }
                else if (ownedSkins[3] == '1')
                {
                    PlayerPrefs.SetInt("Skin", 3);
                    TextCost.text = "SELECTED";
                    break;
                }
                else
                    StartCoroutine(ShowNotificationNoOrbs());
                    break;

            case 2:
                if (ownedSkins[2] != '1' && currency >= 5000)
                {
                    currency -= 5000;
                    temp[2] = '1';
                    ownedSkins = new string(temp);
                    PlayerPrefs.SetString("OwnedSkins", ownedSkins);
                    PlayerPrefs.SetInt("Skin", 2);
                    TextCost.text = "OWNED";
                    break;
                }
                else if (ownedSkins[2] == '1')
                {
                    PlayerPrefs.SetInt("Skin", 2);
                    TextCost.text = "SELECTED";
                    break;
                }
                else
                    StartCoroutine(ShowNotificationNoOrbs());
                    break;

            case 1:
                if (ownedSkins[1] != '1' && currency >= 1000)
                {
                    currency -= 1000;
                    temp[1] = '1';
                    ownedSkins = new string(temp);
                    PlayerPrefs.SetString("OwnedSkins", ownedSkins);
                    PlayerPrefs.SetInt("Skin", 1);
                    TextCost.text = "OWNED";
                    break;
                }
                else if (ownedSkins[1] == '1')
                {
                    PlayerPrefs.SetInt("Skin", 1);
                    TextCost.text = "SELECTED";
                    break;
                }
                else
                    StartCoroutine(ShowNotificationNoOrbs());
                    break;

            case 0:

                if ((PlayerPrefs.GetInt("Skin")) == skinpoint)
                {
                    TextCost.text = "SELECTED";
                    PlayerPrefs.SetInt("Skin", 0);
                }
                else
                {
                    PlayerPrefs.SetInt("Skin", 0);
                    TextCost.text = "OWNED";
                }
                break;
        }
        OrbsText.text = "Orbs: " + PlayerPrefs.GetInt("Currency", 0);

        
    }

    IEnumerator ShowNotificationNoOrbs()
    {
        NoMoneyText.color = new Color32(255, 0, 0, 200);
        yield return new WaitForSeconds(1f);
        NoMoneyText.color = new Color32(0, 0, 0, 0);

    }
}
