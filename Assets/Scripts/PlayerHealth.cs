using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 캐릭터의 생명체로서의 동작 담당
public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더

    private AudioSource audioSource; // 아이템 습득 사운드
    private GameObject audioManager; // 오디오 매니저
    private AudioSource checkvolume; // 오디오 매니저의 소스(불륨 조절 확인)

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // 오디오 매니저에서 소리 불러오기
        audioManager = GameObject.Find("Audio Manager");
        checkvolume = audioManager.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 아이템 습득 사운드 조절(SFX 사운드의 두배)
        audioSource.volume = checkvolume.volume * 2;
    }

    protected override void OnEnable()
    {
        // LivingEnitiy의 OnEnable() 실행
        base.OnEnable();

        // 체력 슬라이더 활성화
        healthSlider.gameObject.SetActive(true);
        // 체력 슬라이더의 최대값을 기본 체력값으로 변경
        healthSlider.maxValue = startingHealth;
        // 체력 슬라이더의 값을 현재 체력값으로 변경
        healthSlider.value = health;

    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // LivingEntity의 OnDamage() 실행
        base.OnDamage(damage, hitPoint, hitDirection);
        // 갱신된 체력을 체력 슬라이더에 반영
        healthSlider.value = health;
    }

    // HealPack으로 회복시 슬라이더 갱신
    public override void Heal(float heal)
    {
        base.Heal(heal);

        healthSlider.value = health;
    }

    // 아이템과 충돌 시
    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            // 충돌한 오브젝트로부터 IItem 컴포넌트 가져오기
            IItem item = other.GetComponent<IItem>();

            if(item != null)
            {
                audioSource.Play();
                // 아이템 사용
                item.Use(gameObject);
            }

        }
    }
}
