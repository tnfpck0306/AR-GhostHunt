using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    public Gun gun;

    public GameObject selectSkillUI;

    public Text skillText;

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
        selectSkillUI.SetActive(false);
    }
}
