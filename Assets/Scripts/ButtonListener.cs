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

    // 사격 버튼 클릭시 총 발사
    public void OnButtonClickedShot()
    {
        gun.Fire();
    }

    // 재장전 버튼 클릭시 총 장전
    public void OnButtonClickedReload()
    {
        gun.Reload();
    }

    // 정지 버튼 클릭시 총 장전
    public void OnButtonClickedPause()
    {
        pauseUI.SetActive(true);
    }

    // 정지 버튼 클릭시 총 장전
    public void OnButtonClickedReturn()
    {
        pauseUI.SetActive(false);
    }

    // 스킬 선택 버튼 클릭시
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
