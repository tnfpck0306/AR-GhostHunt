using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ����� �¾� ������
[CreateAssetMenu(menuName = "Scriptable/GhostData", fileName = "Ghost Data")]
public class GhostData : MonoBehaviour
{
    public float health = 100f; // ü��
    public float damge = 20f; // ���ݷ�
    public float speed = 0.2f; // �̵��ӵ�
}
