using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����۵��� ���������� ������ �ϴ� �������̽�
public interface IItem
{
    // ���������� ���Ǵ� Ÿ�Ե��� Iitem�� ����ϰ� Use �޼��带 �ݵ�� �����ؾ� �Ѵ�
    void Use(GameObject target);
}