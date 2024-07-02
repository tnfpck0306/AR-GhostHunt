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

    public ItemSpawner itemSpawner;

    List<string> playerSkillIndex; // 플레이어가 선택할 수 있는 남은 스킬들
    List<string> uiSkillIndex; // UI에 띄울 스킬들

    private void Awake()
    {
        playerSkillIndex = new List<string>(skillData.SkillList);
    }

    // 스킬 적용
    public void SetSkill(string skill)
    {
        LivingEntity player = playerPref.GetComponent<LivingEntity>();
        PlayerHealth playerHealth = playerPref.GetComponent<PlayerHealth>();

        switch (skill)
        {
            case "체력증가":
                playerHealth.healthSlider.maxValue += 50; // 체력 실린더의 값(UI) 50 증가
                player.startingHealth += 50; // 플레이어 최대체력 50 증가
                playerSkillIndex.Remove("체력증가");
                break;

            case "1":
                print("1");
                break;

            case "아이템 스폰시간 감소":
                itemSpawner.timeBetSpawnMin *= 0.85f; // 아이템 생성 최소 시간 15% 감소
                itemSpawner.timeBetSpawnMax *= 0.85f; // 아이템 생성 최대 시간 15& 감소
                playerSkillIndex.Remove("아이템 스폰시간 감소");
                break;

            case "3":
                print("3");
                break;
        }
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
