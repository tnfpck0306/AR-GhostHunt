using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonListener : MonoBehaviour
{
    public Gun gun;
    public SetSkillManager skillManager;

    public GameObject selectSkillUI;
    public GameObject pauseUI;

    public Dropdown settingUIDd;
    public Dropdown pauseUIDd;

    private string prvText;

    // ��� ��ư Ŭ���� �� �߻�
    public void OnButtonClickedShot()
    {
        gun.Fire();
    }

    // ������ ��ư Ŭ���� �� ����
    public void OnButtonClickedReload()
    {
        gun.Reload();
    }

    // ���� ��ư Ŭ���� �� ����
    public void OnButtonClickedPause()
    {
        pauseUI.SetActive(true);
    }

    // ���� ��ư Ŭ���� �� ����
    public void OnButtonClickedReturn()
    {
        pauseUI.SetActive(false);
    }

    // ��ų ���� ��ư Ŭ����
    public void OnButtonClickedSkill()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string clickSkill = clickObject.GetComponentInChildren<Text>().text;

        if (clickSkill == prvText)
        {
            skillManager.SetSkill(clickSkill);
            selectSkillUI.SetActive(false);
        }
        else
        {
            skillManager.SkillExplaneText(clickSkill);
            prvText = clickSkill;
        }
    }

    public void onSettingUILanguage()
    {
        int languageValue = settingUIDd.value;
        pauseUIDd.value = languageValue;
    }

    public void onPauseUILanguage()
    {
        int languageValue = pauseUIDd.value;
        settingUIDd.value = languageValue;
    }
}
