# <center>Unity 3주차 - 게임 개발 입문 팀 프로젝트</center>

## 개요

- **프로젝트 명 :**  전튜내베(?)
- **프로젝트 소개**
  - **목적성 :** 내일배움캠프에서 살아 남아 취업을 하기 위해 Basic부터 Challange까지 무시무시한 강의를 마스터하는 Player
  - **대결 구도 :** Player vs 각 단계 별 보스들의 무시무시한 코드 질문들
  - **클리어 :** 모든 보스들의 무시무시한 질문을 클리어하면, 내배캠을 수료하여 새로운 단계로 넘어가게 됩니다.
- **맴버 :** 팀 럭키스타 ( 이동훈, 김한진, 조종완, 김도현 )

## 게임 설명

|![image (1)](https://github.com/kuraqura88/777Project/assets/167050509/2119c2e6-8d20-446c-999b-59d2d0c2d2e7)|![녹음-2024-05-22-102936](https://github.com/kuraqura88/777Project/assets/167050509/0f664bda-9cec-4ecd-9b98-ebae25b0de5b)|![CharacterMoving (1)](https://github.com/kuraqura88/777Project/assets/167050509/9fb03c4b-02f2-4e5e-8412-5b09a264b823)|
|:---:|:---:|:---:|
|**게임 시작 화면**|**랜덤 캐릭터 생성**|**생성된 캐릭터**|

- 게임 시작 버튼을 눌러 실행할 수 있습니다. 바로 다음 화면에서 슬롯을 통해 랜덤한 캐릭터가 생성됩니다.

- 기본적인 조작은 키보드 방향키를 통해 이루어지며, 공격 조작은 따로 없이 일정한 시간마다 투사체가 발사됩니다.

- 나오는 몬스터들을 쓰러트리며, 최종적으로 보스 몬스터를 무찌르면(?) 클리어할 수 있습니다!


## 조작 방식

|이동 방향|위|아래|오른쪽|왼쪽|
|:---:|:---:|:---:|:---:|:---:|
|키보드|↑|↓|→|←|

## 전투 방식

![녹음-2024-05-22-152542](https://github.com/kuraqura88/777Project/assets/167050509/fb1f5a3f-4bc5-43cd-9c6a-a6634dd52192)

- 캐릭터 기준으로 자동적으로 공격이 나갑니다.
- 에너미 또한 에너미 기준으로 자동적으로 공격이 나갑니다.

![2](https://github.com/kuraqura88/777Project/assets/167050509/58a30877-4d14-4ab5-8043-b06cd6b3c7f1)


- 상단 게이지 바가 오른쪽 끝까지 도달하면 보스와 조우하게 됩니다!
- 보스를 무찔러 다음 단계로 넘어가 자신의 실력을 증명해 보세요!

## 파일 디텍토리

─Animation  
│  ├─Boss  
│  ├─Enemy  
│  ├─Handle  
│  ├─Player  
│  └─Projectile  
├─Background  
├─Content  
│  ├─BackgroundsPixelArt  
│  │  ├─city 1  
│  │  ├─city 2  
│  │  ├─city 3  
│  │  ├─city 4  
│  │  ├─city 5  
│  │  ├─city 6  
│  │  ├─city 7  
│  │  └─city 8  
│  ├─Character  
│  │  ├─Character1  
│  │  │  ├─Hit  
│  │  │  ├─Idle  
│  │  │  └─Move  
│  │  ├─Character2  
│  │  │  ├─Hit  
│  │  │  ├─Idle  
│  │  │  └─Move  
│  │  └─Character3  
│  │      ├─Hit  
│  │      ├─Idle  
│  │      └─Move  
│  ├─Enemy&Bullet  
│  ├─Slot  
│  │  └─Animation  
│  ├─Test  
│  └─Tutor  
├─Fonts  
│  ├─ChosunCentennial  
│  └─KCC-Hanbit  
├─Images  
├─Input  
├─Karugamo  
│  ├─BGM  
│  │  └─FREE  
│  ├─Sample  
│  │  ├─Audio  
│  │  ├─Scenes  
│  │  └─Scripts  
│  └─Scripts  
│      └─Audio  
├─Prefabs  
├─Resources  
│  ├─Prefabs  
│  │  ├─Boss  
│  │  ├─Enemy  
│  │  ├─Player  
│  │  └─Projectiles  
│  └─Sounds  
│      └─NormalAttackSound  
├─Scenes  
├─ScriptableObjects  
│  ├─Data  
│  │  ├─Boss  
│  │  ├─Enemy  
│  │  │  ├─CircleShotAttack  
│  │  │  ├─MultiShotAttack  
│  │  │  └─OneShotAttack  
│  │  └─Player  
│  └─Scripts  
├─Scripts  
│  ├─Background  
│  ├─Character  
│  │  └─ItemFunctionScripts  
│  ├─Enemy  
│  │  └─Boss  
│  ├─Interface  
│  ├─Managers  
│  ├─Pool  
│  ├─Projectile  
│  ├─Scene  
│  └─Util  
├─Sounds  
│  ├─190114_AchivementSFX  
│  ├─ArcadeGameBGM#17  
│  ├─ArcadeGameBGM#3  
│  └─CasualGameSounds  
├─TextMesh Pro  
│  ├─Documentation  
│  ├─Fonts  
│  ├─Resources  
│  │  ├─Fonts & Materials  
│  │  ├─Sprite Assets  
│  │  └─Style Sheets  
│  ├─Shaders  
│  └─Sprites  
├─Timeline  
└─UI  
    ├─Animation  
    ├─Dark UI  
    │  └─Free  
    └─StartBackGround  
