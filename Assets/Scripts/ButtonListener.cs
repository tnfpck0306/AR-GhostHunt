using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListener : MonoBehaviour
{
    public Gun gun;

    // 사격 버튼 클릭시 총 발사
    public void OnButtonClicked()
    {
        gun.Fire();
    }
}
