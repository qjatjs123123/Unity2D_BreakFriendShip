# BreakFriendship - Unity2D를 활용한 4인 멀티게임
----------

프로젝트 소개
----------
3인 팀프로젝트로서, Unity3D의 Photon,PlayFab등을 활용하여 개발한 2D멀티 퍼즐 게임이다. 본인은 포톤 프로그래밍에 관련된 기능, 스테이지 등을 담당하였다.

BreakFriendship은 유니티엔진을 활용한 실시간 협동 2D 플랫포머 멀티게임이다. 최대 4인의 플레이어가 실시간으로 게임에 참여하며, 플랫폼(발판)위를 뛰어다니는 점핑 액션 게임이다. 유사프로그램으로는 "닌텐도"사의 슈퍼마리오 브라더스, "Pixel"사의 "ClaveStory" "TecoPark"사의 "Pico Park"등이 있다.

개발 플랫폼은 PC Window이며 개발환경은 "Unity3d 2020.3.8f1"버전을 사용하였다.

-----------
## 프로젝트 추진 일정
![image](https://user-images.githubusercontent.com/56360477/147561040-f0fcf13f-e60f-40b5-9756-1b7d421d9546.png)

프로젝트 개발의 총 기간은 7주이다.

--------------
## 주요 기술
+ Unity3d
+ C#
+ PlayFab
+ Photon Cloud
+ GitHub

----------------

## 프로그램 동작도
![image](https://user-images.githubusercontent.com/56360477/147561307-b5bf8112-6274-4f18-b473-8ad67ab1c490.png)

1. 회원가입 - 데이터 분석, 스토리지, 처리 및 내보내기를 위한 도구 세트인 PlayFab을 프로젝트에
적용시켜 이메일, 닉네임, 비밀번호를 PlayFab에 저장시킨다.
2. 로그인 - 데이터 분석, 스토리지, 처리 및 내보내기를 위한 도구 세트인 PlayFab을 프로젝트에
적용시켜 저장된 닉네임을 불러온다.
3. 캐릭터 선택 - 4가지 캐릭터가 존재하고 플레이어는 선택한 캐릭터로 게임을 진행하게 된다.
4. 로비접속 - Photon서버에 접속을 하여, 방생성, 또는 생성된 방에 게임참가를 할 수 있다.
5. 방 생성 - 플레이어가 방 생성을 하게 되면, 방 인원수를 2~4명까지 선택 할 수 있고 선택한 인원수에
맞는 방에 적용된다.

6. 방 접속 - 방 접속을 하게 되면 접속한 플레이어의 캐릭터, 닉네임, Ready 텍스트가 플레이어 마다
생성된다. Ready버튼을 누르게 되면 누른 플레이어의 Ready 텍스트가 빨간 색으로 변하게 된다. 접속한
플레이어끼리 실시간 소통할 수 있는 채팅방이 있다.
7. 1~6라운드, 총 6개의 라운드가 존재하며, 각각의 라운드마다 특색있는 퍼즐요소를 추가하여 플레이어들이 목표 위치에 도달하면 클리어하도록 설정하였다.

--------------------
## 프로그램 기능
![image](https://user-images.githubusercontent.com/56360477/147561430-384b0bd8-9512-494e-901a-d6e951438254.png)

----------
## 조작법
+ ← 키보드 방향키 : 좌측이동 
+ → 키보드 방향키 : 우측이동
+ Space : 점프,더블점프

--------------
## 프로그램 실행결과
+ 타이틀 화면  
![image](https://user-images.githubusercontent.com/56360477/147561724-81514e8b-fb72-407f-80e4-7692cce5b921.png)  
  - Start버튼으로 게임을 시작 할 수있고, Exit버튼으로 게임을 종료 할 수 있다.

+ 로그인 화면  
![image](https://user-images.githubusercontent.com/56360477/147562089-6c10a251-7074-4b6f-8c86-3a1023974b4f.png)  
  - 회원가입 버튼 클릭시 가입페이지로 이동한다. PlayFab과 연동되어 유저데이터를 저장 및 불러온다.

+ 캐릭터 선택 화면  
![image](https://user-images.githubusercontent.com/56360477/147562216-1e9a3720-0622-4235-9d27-c13014a3b4ef.png)  
  - 4가지의 캐릭터중 한가지를 선택할 수 있다.  

+ 로비 화면  
 ![image](https://user-images.githubusercontent.com/56360477/147562507-b63d97ea-33ae-400d-a417-a1570f4f82ec.png)  
   -  로비에서는 현재 접속한 유저의 수와, 플레이중인 방을 보여준다. 방이름을 설정하고 Create버튼 클릭시 방을 만들수 있고, QuickStart버튼을 클릭할 시 현재 있는 방들중 하나에 랜덤으로 입장하게 된다.

+ 방 화면  
  ![image](https://user-images.githubusercontent.com/56360477/147562626-6d41bcf3-2e75-4efc-bc7f-796bc72698a2.png)  
  -  현재 접속한 플레이어와, 플레이어의 레디상태를 표시해준다. 플레이어간의 실시간 채팅이 가능한 채팅창이 존재한다.  

+ 1Round - 튜토리얼  
![round1_resize](https://user-images.githubusercontent.com/56360477/147594686-7d607780-341b-45e3-b79d-956f793b3049.gif)  


+ 2Round - 총알피하기   
![round2_resize](https://user-images.githubusercontent.com/56360477/147594693-976fe2ad-3c59-4944-abe6-c414ab936ab5.gif)   

+ 3Round - 무궁화 꽃이 피었습니다.  
  ![round3_resize](https://user-images.githubusercontent.com/56360477/147594801-84bdd18d-ed2c-402f-b3e8-83dac9a1b3fa.gif)  
  
+ 4Round - 일심동체 유령조작  
![round4_resize](https://user-images.githubusercontent.com/56360477/147594869-9a80b8c9-ea34-4f98-8f9e-7fd7c05ec1fb.gif)  

+ 5Round - 장애물 피하기  
 ![round5_resize](https://user-images.githubusercontent.com/56360477/147594931-7f4ef75e-310e-4e32-a221-e6881a4dcd64.gif)  


+ 6Round - 추적 몬스터 피하기  
![round6_resize](https://user-images.githubusercontent.com/56360477/147594937-03454d7a-78a9-4846-a757-e7caa36a1afb.gif)  

-------------------------
## 시연연상
+ https://www.youtube.com/watch?v=91RTeFIy7q4  

