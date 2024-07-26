using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� ĳ������ ����ü�μ��� ���� ���
public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; // ü���� ǥ���� UI �����̴�

    private AudioSource audioSource; // ������ ���� ����
    private GameObject audioManager; // ����� �Ŵ���
    private AudioSource checkvolume; // ����� �Ŵ����� �ҽ�(�ҷ� ���� Ȯ��)

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ����� �Ŵ������� �Ҹ� �ҷ�����
        audioManager = GameObject.Find("Audio Manager");
        checkvolume = audioManager.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // ������ ���� ���� ����(SFX ������ �ι�)
        audioSource.volume = checkvolume.volume * 2;
    }

    protected override void OnEnable()
    {
        // LivingEnitiy�� OnEnable() ����
        base.OnEnable();

        // ü�� �����̴� Ȱ��ȭ
        healthSlider.gameObject.SetActive(true);
        // ü�� �����̴��� �ִ밪�� �⺻ ü�°����� ����
        healthSlider.maxValue = startingHealth;
        // ü�� �����̴��� ���� ���� ü�°����� ����
        healthSlider.value = health;

    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // LivingEntity�� OnDamage() ����
        base.OnDamage(damage, hitPoint, hitDirection);
        // ���ŵ� ü���� ü�� �����̴��� �ݿ�
        healthSlider.value = health;
    }

    // HealPack���� ȸ���� �����̴� ����
    public override void Heal(float heal)
    {
        base.Heal(heal);

        healthSlider.value = health;
    }

    // �����۰� �浹 ��
    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            // �浹�� ������Ʈ�κ��� IItem ������Ʈ ��������
            IItem item = other.GetComponent<IItem>();

            if(item != null)
            {
                audioSource.Play();
                // ������ ���
                item.Use(gameObject);
            }

        }
    }
}
