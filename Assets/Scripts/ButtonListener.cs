using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    public Gun gun;

    public GameObject selectSkillUI;

    public Text skillText;

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

    public void OnButtonClickedSkill()
    {
        selectSkillUI.SetActive(false);
    }
}
