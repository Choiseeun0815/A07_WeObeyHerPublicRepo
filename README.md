# WE OBEY HER (A 07조)

<br>

<div align="center">

![StartSceneImage](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/4cfada63-ab35-41f6-84f8-082a114a02ae)

# 목차

| [🙇‍♀️ 프로젝트 소개(개발환경) ](#bowing_woman-프로젝트-소개) |
| :---: |
| [✋ 팀 소개 ](#hand-팀-소개) |
| [💭 기획의도 ](#thought_balloon-기획의도) |
| [🌟 주요기능 ](#star2-주요기능) |
| [⏲️ 프로젝트 수행 절차 ](#timer_clock-프로젝트-수행-절차) |

</div>

<br><br>

## :bowing_woman: 프로젝트 소개

#### Team Notion을 클릭하시면, 프로젝트의 컨셉과 진행 과정을 확인하실 수 있습니다!
### [🤝Team Notion](https://www.notion.so/teamsparta/WE-OBEY-b3bac74d07dc41269dcfbab7a8efbbc2)

<br>

<div align="center">

#### ${\textsf{\color{purple}매력적인 그녀를 만날 수 있는 클리커 게임}}$
### 많은 사람들이 그녀를 알 수 있도록 클릭! 클릭!
![Preview](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/4c8bf6fd-00b6-4ca0-ad6d-00062b055877)

<br>

| 게임명 | **${\textsf{\color{purple}WE OBEY HER}}$** |
| :---: | :---: |
| 장르 | 클리커 게임 |
| 개발 언어 | C# |
| 프레임워크 | .Net 4.8.04084 |
| 개발 환경 | Unity 2022.3.17f1 <br/> Visual Studio Community 2022 |
| 타겟 플랫폼 | Android |
| 개발 기간 | 2024.06.19 ~ 2024.06.26 |

</div>

<br>

[:crown: 목차로 돌아가기](#목차)

<br><br>

## :hand: 팀 소개

<div align="center">

![Members](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/d5f2882b-a2eb-492a-b437-ab4649432d09)

| 직급 | 이름 | 기능 구현 | 깃허브 주소 | 
| :---: | :---: | :---: | :---: |
| 팀장 | 최세은 | 신도, 대화시스템 | https://github.com/Choiseeun0815 |
| 팀원 | 권유리 | 적, 사운드 | https://github.com/Kyr001?tab=projects |
| 팀원 | 안보연 | 애니메이션, 맵 | https://github.com/BY0808?tab=repositories |
| 팀원 | 윤세나 | 게임매니저, UI | https://github.com/mwomwo1 |
| 팀원 | 이유신 | 플레이어, 재화시스템 | https://github.com/shinmegan |

</div>

<br>

[:crown: 목차로 돌아가기](#목차)

<br><br>

## :thought_balloon: 기획의도

### 1. 주제 선정 배경
팀 컨셉에 맞게 어떤 대상을 숭배하는 것을 목표로 하는 게임으로 기획, 신도들을 포교하는 주제로 결정  

### 2. 게임 목표
플레이어가 포교하여 신도를 모으는 게임으로 적과의 미니 게임도 함께 하며, 특별한 신도를 모두 모아 게임을 완료 하는 것이 목표


<br>

[:crown: 목차로 돌아가기](#목차)

<br><br>

## :star2: 주요기능

![playerImage](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/92630eb2-8ea5-4b40-9989-5fbf8ba080f8)

### 1. 플레이어 및 스탯 정보
   - 마우스 왼쪽 버튼을 누른 채, 마우스 위치를 이동시키면 플레이어가 마우스 위치에 따라 움직입니다.
   - 플레이어는 상하좌우 및 대각선으로 움직일 수 있습니다.

<div align="center">
  
![Stats](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/f0a0c8ba-34a6-4ef3-9512-82efa6ca7726)

| 스탯 | 초기값 | 내용 | 비고 | 
| :---: | :---: | :---: | :---: |
| 생명 | 하트 3개 | 적의 공격을 받으면 1개 소모 | 모두 소모 시, 게임 오버 |
| 지배력 | 일반인 | 신도 수에 따라 변경 | 일반인 -> 광신도 -> 사이비 -> 주교 |
| 신도 | 0명 | 일반신도 -> 클릭으로 포교<br>네임드신도 -> 올바른 질문지를 선택시 포교 | 수금 가능,<br> 신도유비지 지불 필요 |
| 골드 | 100G | 증가: 수금<br> 감소: 신도유지비, 강화 | 신도유지비를 10초 이내에 지불하지 못하면,<br>하트 1개 소모 |

![StatChange](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/8aa8fa26-885d-4e96-a713-5bd40d7b8228)

</div>

<br>

***


     
### 2. 다수의 적(4~5명)  
   - 적은 상하좌우 랜덤한 방향으로 움직입니다.
   - 플레이어가 적과 일정거리 이내로 가까워지면, 적의 추격이 시작됩니다.
   - 적은 "TIL 쓰셨죠?" 질문으로 공격합니다.

<div align="center">

![EnemyImage](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/a798424e-0adc-4dd7-8157-b5179d6cc74a)

</div>

<br>

***
  
### 3. 강화 기능
   - 골드를 소모하면 3가지 스탯을 강화할 수 있습니다.
   - 매력 강화(강화시, 일반 신도를 전도하기 위해 필요한 클릭수가 0.2배 감소)
   - 수금력 강화(강화시, 수금량 2배 증가)
   - 수명 연장(강화시, 하트 1개 획득)

<div align="center">

![Reinforce](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/b0f02951-343a-4293-b721-ae75e0ef5870)

</div>

<br>

***
        
### 4. 업적 기능
   - 총 9개의 업적이 존재하며, 마지막 업적(WE OBEY U)을 달성하면 게임 클리어가 가능합니다.
   - 업적 달성시, 해당 업적이 회색에서 컬러로 바뀝니다.
   - 플러팅의 신(네임드신도가 제시하는 선택지를 단 한 번도 틀리지 않고, 모든 네임드신도를 포교하면 성공)
   - 부자 꿈나무(골드를 3000G 이상 모으면 성공)
   - 불사신(하트를 3개 이상 소모시 성공 - 플레이어 생존 필수)
   - 미꾸라지(적의 추격 상태로부터 5회 이상 도망치면 성공)
   - 시간은 금(10초 이내에 신도유지비를 연달아 5회 지불시 성공)
   - 동아리 회장(광신도 단계의 지배력을 달성하면 성공)
   - 왕관의 무게(사이비 단계의 지배력을 달성하면 성공)
   - 세계 지배자(주교 단계의 지배력을 달성하면 성공)
   - WE OBEY U(모든 네임드 신도들을 포교시 성공)

<div align="center">

![Goal](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/6abacc24-20af-4146-93a0-1605529f2dce)

</div>

<br>

***
    
### 5. 애니메이션 및 이펙트 효과
   - 플레이어와 적, 신도의 움직임에 반응하여, 좌우앞뒤 애니메이션이 실행됩니다.
   - 플레이어 주위로 이펙트 효과가 실행됩니다.
   - 일반 신도를 포교하기 위해 클릭 시, 이펙트 효과가 실행됩니다.

<br>

***
    
### 6. 사운드
   - 게임 진행 내내 BGM이 흘러나와, 게임의 페이스와 리듬을 유지할 수 있습니다.  
   - 포교를 위해 일반신도를 클릭시, 효과음을 추가하여, 게임의 몰입감과 즐거움을 높였습니다.
   - 사운드 포함 리스트(골드 사용, 포교 성공, 적 공격, 플레이어 걸을 때, 신도유지비 지불 카운트 다운, 올바른 질문 선택 성공 및 실패)
   - 특별 사운드 포함 리스트(네임드 신도에 해당하는 팀원들의 목소리로 "복종" 사운드 생성)

<br>

***
        
### 7. UI 
   - Start scene에서 JOIN 버튼을 누르면 Game Scene로 넘어가 게임을 플레이 할 수 있습니다.
   - Start scene에서 OUT 버튼을 누르면 게임이 종료됩니다.
   - Start Scene와 Game Scene, End Scene을 생성하여 게임의 완성도를 높였습니다.

<div align="center">

![endSceneImage](https://github.com/Choiseeun0815/A07WeObeyCSE/assets/108499207/9be5fd21-891d-4c43-a402-a6e95ea7b6bf)

</div>

<br>

[:crown: 목차로 돌아가기](#목차)

<br><br>

## :timer_clock: 프로젝트 수행 절차

| 구분 | 기간 | 활동 |
| :---: | :---: | :---: |
| 사전 기획 | 06.19(수) | 프로젝트 기획 및 주제 선정, 기획안(S.A.) 작성, 깃허브 생성 |
| 1차 구현 | 06.19(수) ~ 06.20(목) | 기본 게임 사이클 완성 |
| 2차 구현 | 06.21(금) | 필수사항 및 선택사항 구현 |
| 3차 구현 | 06.22(토) ~ 06.23(일) | 추가 기능 구현, 에셋 추가 <br> 최종 버그 수정 |
| 게임 완성 | 06.24(월) ~ 06.26(수) | 와이어프레임 수정, ReadMe 수정, <br> PPT 제작 | UML 추가 |

<br>

[:crown: 목차로 돌아가기](#목차)

<br><br>

