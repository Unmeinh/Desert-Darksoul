using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu_Setting : MonoBehaviour
{
    public Slider volSlider;
    public GameObject yourSelf;
    AudioSource auSrc;
    Transform camera;

    private float defaultVol;

    void Start()
    {
        auSrc = GameObject.Find("BGM").GetComponent<AudioSource>();
        camera = GameObject.Find("MainCamera").GetComponent<Transform>();
        defaultVol = auSrc.volume;
        transform.position = new Vector3(camera.position.x, camera.position.y, -7);
        // GameObject.Find("ButtonPlay").GetComponent<Image>().color = new Color32(137, 137, 137, 225);
    }

    void Update()
    {
        yourSelf.transform.position = new Vector3(camera.position.x, camera.position.y, -7);
    }

    public void SetVolume()
    {
        auSrc.volume = volSlider.value;
    }

    public void ShowSetting()
    {
        if (auSrc != null) {
            defaultVol = auSrc.volume;
            volSlider.value = defaultVol;
        }
        yourSelf.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideSetting(string action)
    {
        if (action == "save")
        {
            auSrc.volume = volSlider.value;
            yourSelf.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            auSrc.volume = defaultVol;
            volSlider.value = defaultVol;
            yourSelf.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void MoveToScene2()
    {
        SceneManager.LoadScene("Level2Scene");
        PlayerPrefs.SetFloat("SrcVol", auSrc.volume);
        Time.timeScale = 1;
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene("Level1Scene");
        PlayerPrefs.SetFloat("SrcVol", auSrc.volume);
        Time.timeScale = 1;
    }

    public void ReplayLevel2()
    {
        SceneManager.LoadScene("Level2Scene");
        PlayerPrefs.SetFloat("SrcVol", auSrc.volume);
        Time.timeScale = 1;
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("HomeScene");
        Time.timeScale = 1;
    }

    public void SettingLab2()
    {
        if (yourSelf.activeInHierarchy)
        {
            yourSelf.SetActive(false);
        }
        else
        {
            yourSelf.SetActive(true);
        }
    }

    public void PauseLab2()
    {
        Time.timeScale = 0;
        GameObject.Find("ButtonPlay").GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        GameObject.Find("ButtonPause").GetComponent<Image>().color = new Color32(
            137,
            137,
            137,
            225
        );
    }

    public void PlayLab2()
    {
        Time.timeScale = 1;
        GameObject.Find("ButtonPause").GetComponent<Image>().color = new Color32(
            255,
            255,
            225,
            255
        );
        GameObject.Find("ButtonPlay").GetComponent<Image>().color = new Color32(137, 137, 137, 225);
    }

    public void MusicLab2()
    {
        if (auSrc != null)
        {
            if (auSrc.isPlaying)
            {
                auSrc.Pause();
                GameObject.Find("ButtonMusic").GetComponent<Image>().color = new Color32(
                    137,
                    137,
                    137,
                    225
                );
            }
            else
            {
                auSrc.Play();
                GameObject.Find("ButtonMusic").GetComponent<Image>().color = new Color32(
                    255,
                    255,
                    225,
                    255
                );
            }
        }
    }
}
