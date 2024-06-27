using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Text highScoreText; // �ְ� ���� ǥ�ÿ� �ؽ�Ʈ

    public Text[] skillText = new Text[3]; // ��ų ����â�� ��ų �ؽ�Ʈ

    public GameObject collisonEffect; // �浹�� ȿ�� UI
    public GameObject gameoverUI; // ���ӿ��� �� Ȱ��ȭ�� UI
    public GameObject mainMenuUI; // ���θ޴� UI
    public GameObject selectSkillUI; // ��ų ����â UI

    public SkillData skillData; // ��ų ������
    public List<string> skillIndex;

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

    // ��ų ����â Ȱ��ȭ
    public void SetActiveSkillUI()
    {
        skillIndex = skillData.SkillList;

        for (int index = 0; index < skillText.Length; index++)
        {
            int rand = Random.Range(0, skillIndex.Count);
            skillText[index].text = skillIndex[rand];
            skillIndex.RemoveAt(rand); // �ߺ� ����
        }
        selectSkillUI.SetActive(true);

    }

    // �浹 ȿ���� �ڷ�ƾ
    public void CollisionEffect()
    {
        StartCoroutine(CollisonEffect_Coroutine());
    }

    // �浹�� ª�� �ð� UI Ȱ��ȭ
    public IEnumerator CollisonEffect_Coroutine()
    {
        collisonEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        collisonEffect.SetActive(false);

    }
    
    // ���ӿ��� UI Ȱ��ȭ
    public void SetActiveGameoverUI(bool active)
    {
        int highScore = GameManager.instance.Get_HighScore();
        print(highScore);
        highScoreText.text = "High Score : " + highScore;

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
