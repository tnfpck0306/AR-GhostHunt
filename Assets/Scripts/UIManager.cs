using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // �̱��� ���ٿ� ������Ƽ
    public static UIManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // �̱����� �Ҵ�� ����

    public Text ammoText; // ź�� ǥ�ÿ� �ؽ�Ʈ
    public Text killText; // ų �� ǥ�ÿ� �ؽ�Ʈ
    public GameObject gameoverUI; // ���ӿ��� �� Ȱ��ȭ�� UI

    public GameObject mainMenuUI; // ���θ޴� UI
    public bool startMenu = true;

    // ź�� �ؽ�Ʈ ����
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + " / " + remainAmmo;
    }

    // ų �� �ؽ�Ʈ ����
    public void UpdateKillText(int newKill)
    {
        killText.text = "Kill : " + newKill;
    }
    
    // ���ӿ��� UI Ȱ��ȭ
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    // ���θ޴� UI ��Ȱ��ȭ
    public void GameStart()
    {
        startMenu = false;
        mainMenuUI.SetActive(startMenu);
    }

    // ���� �����
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
