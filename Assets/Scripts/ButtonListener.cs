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

    public void OnButtonClickedSkill()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        skillManager.SetSkill(clickObject.GetComponentInChildren<Text>().text);
        selectSkillUI.SetActive(false);
    }
}
