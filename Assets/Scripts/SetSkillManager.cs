using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

// 스킬 선택창 UI 관리 및 스킬 적용
public class SetSkillManager : MonoBehaviour
{
    public Text[] skillText = new Text[3]; // 스킬 선택창의 스킬 텍스트
    public SkillData skillData; // 스킬 데이터

    public GameObject selectSkillUI; // 스킬 선택창
    public GameObject playerPref; // 플레이어(체력 컴포넌트)
    private LivingEntity player;
    private PlayerHealth playerHealth;
    private PlayerShooter playerShooter;

    public GhostSpawner ghostSpawner;
    public ItemSpawner itemSpawner;

    public AmmoPack ammoPack;
    public HealPack healPack;

    public List<string> playerSkillIndex; // 플레이어가 선택할 수 있는 남은 스킬들
    List<string> uiSkillIndex; // UI에 띄울 스킬들

    public Text healthText; // 체력 시각화 텍스트(테스트용)

    public bool isHealthRegen = false; // 체력 회복 스킬 활성화 여부

    private void Awake()
    {
        playerSkillIndex = new List<string>(skillData.SkillList);
        player = playerPref.GetComponent<LivingEntity>();

        playerHealth = playerPref.GetComponent<PlayerHealth>();
        playerShooter = playerPref.GetComponent<PlayerShooter>();

        // 플레이어 능력치 초기화
        playerShooter.gun.gunData.damage = 50;

        // 아이템 수치 초기화
        ammoPack.ammo = 30;
        healPack.heal = 50;

    }

    private void Update()
    {
        healthText.text = player.health.ToString(); // (테스트용 코드)
        
    }

    // 스킬 적용
    public void SetSkill(string skill)
    {

        switch (skill)
        {
            case "공격력 증가":
                playerShooter.gun.gunData.damage += 20; // 플레이어 총 공격력 20 증가
                playerSkillIndex.Remove("공격력 증가");
                break;

            case "탄알 보급":
                SkillAutoAmmo(playerShooter);
                playerSkillIndex.Remove("탄알 보급");
                break;

            case "체력 증가":
                playerHealth.healthSlider.maxValue += 50; // 체력 실린더의 값(UI) 50 증가
                player.startingHealth += 50; // 플레이어 최대체력 50 증가
                playerSkillIndex.Remove("체력 증가");
                break;

            case "체력 재생":
                isHealthRegen = true;
                playerSkillIndex.Remove("체력 재생");
                break;

            case "아이템 스폰시간 감소":
                itemSpawner.timeBetSpawnMin *= 0.85f; // 아이템 생성 최소 시간 15% 감소
                itemSpawner.timeBetSpawnMax *= 0.85f; // 아이템 생성 최대 시간 15& 감소
                playerSkillIndex.Remove("아이템 스폰시간 감소");
                break;

            case "아이템 효율 증가":
                // 아이템 효율 20% 증가 코드
                ammoPack.ammo = (int)(ammoPack.ammo * 1.2);
                healPack.heal *= 1.2f;
                playerSkillIndex.Remove("아이템 효율 증가");
                break;

            case "유령 속도 감소":

                // 속도 감소 코드
                playerSkillIndex.Remove("유령 속도 감소");
                break;

            case "유령 탐지기":

                // 가장 가까운 유령을 찾는 코드
                playerSkillIndex.Remove("유령 탐지기");
                break;
        }
    }

    // 탄알 보급 스킬
    public IEnumerator SkillAutoAmmo(PlayerShooter playerShooter)
    {
        while (true)
        {
            // 5초에 탄알 5씩 탄창에 추가
            yield return new WaitForSeconds(5f);
            playerShooter.gun.ammoRemain += 5;
        }
    }

    // 체력 회복 스킬
    public void SkillHealthRegen()
    {
        // 체력을 4회복 (유령 킬 당)
        player.Heal(4);
    }

    // 스킬 선택창 활성화
    public void SetActiveSkillUI()
    {
        uiSkillIndex = new List<string>(playerSkillIndex);

        // 스킬 3가지 선택창 UI에 띄우기
        for (int index = 0; index < skillText.Length; index++)
        {
            int rand = Random.Range(0, uiSkillIndex.Count);
            
            // 선택가능한 스킬이 더이상 없다면
            if(uiSkillIndex.Count == 0)
            {
                skillText[index].text = "X";
            }

            else
            {
                skillText[index].text = uiSkillIndex[rand];
                uiSkillIndex.RemoveAt(rand); // 중복 방지
            }
        }
        selectSkillUI.SetActive(true);

    }
}
