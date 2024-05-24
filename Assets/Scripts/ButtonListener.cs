using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListener : MonoBehaviour
{
    public Gun gun;

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
}
