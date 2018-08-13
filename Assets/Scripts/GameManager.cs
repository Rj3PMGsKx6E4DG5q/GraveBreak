using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Hakaima;


public class GameManager : MonoBehaviour
{

	private enum State
	{
		Ready,
		ReadyContinue,
		Play,
		BossDefeat,
		Clear,
		Continue,
		TutorialEnd,
		TutorialHelp,
	}
	

	private class Cell
	{
		public int point;
		public int pointX;
		public int pointY;
		public Hakaima.Terrain.Type terrainType;
		public Hakaima.Obstacle.Type obstacleType;
		public Hakaima.Enemy.Type enemyType;
		public Hakaima.Item.Type itemType;
		public bool isHoleOpen;
	}


	private class Score
	{
		private const int MAX = 9999999;

		private int recordScore;
		private int recordScoreHigh;
		public int enemy						{ get; set; }
		public Dictionary<int, int> bonusList	{ get; set; }
		public int timebonus					{ get; set; }

		public int high {
			get {
				return Mathf.Max (now, recordScoreHigh);
			}
		}

		public int now {
			get {
				int s = recordScore;
				s += enemy;
				s += bonusList.Sum (obj => obj.Value);
				s += timebonus;
				s = Mathf.Max (0, s);
				s = Mathf.Min (s, MAX);
				return s;
			}
		}
		
		public int stage {
			get {
				return now - recordScore;
			}
		}

		public int pre		{ get; set; }
		public int preHigh	{ get; set; }

		public Score (int recordScore, int recordScoreHigh)
		{
			this.recordScore = recordScore;
			this.recordScoreHigh = recordScoreHigh;
			this.enemy = 0;
			this.bonusList = new Dictionary<int, int> ();
			this.bonusList [(int)Bonus.Type.Bonus0] = 0;
			this.bonusList [(int)Bonus.Type.Bonus1] = 0;
			this.bonusList [(int)Bonus.Type.Bonus2] = 0;
			this.bonusList [(int)Bonus.Type.Bonus3] = 0;
			this.bonusList [(int)Bonus.Type.Bonus4] = 0;
			this.bonusList [(int)Bonus.Type.Bonus5] = 0;
			this.bonusList [(int)Bonus.Type.Bonus6] = 0;
			this.timebonus = 0;

			this.pre = -1;
			this.preHigh = -1;
		}
	}
	
	
	private class Life
	{
		private const int MAX = 99;
		private int _pre;
		
		public int now {
			get {
				return MainManager.Instance.life;
			}
			set {
				if (value < 0)
					value = 0;
				else if (value > MAX)
					value = MAX;
				MainManager.Instance.life = value;
			}
		}
		
		public int pre {
			get {
				return this._pre;
			}
			set {
				this._pre = value;
			}
		}
		
		public Life (int value)
		{
			this.now = value;
			this.pre = -1;
		}
	}

	private class MyWeapon
	{
		private const int MAX = 99;
		private int _pre;

		public int now {
			get {
				return MainManager.Instance.weapon;
			}
			set {
				if (value < 0)
					value = 0;
				else if (value > MAX)
					value = MAX;
				MainManager.Instance.weapon = value;
			}
		}

		public int pre {
			get {
				return this._pre;
			}
			set {
				this._pre = value;
			}
		}

		public MyWeapon (int value)
		{
			this.now = value;
			this.pre = -1;
		}
	}

	
	private class RemainingTime
	{
		private float _now;
		private float _pre;
		
		public float now {
			get {
				return this._now;
			}
			set {
				this._now = value;
				if (this._now < 0)
					this._now = 0;
			}
		}
		
		public float pre {
			get {
				return this._pre;
			}
			set {
				this._pre = value;
			}
		}
		
		public RemainingTime (float value)
		{
			this.now = value;
			this.pre = -1;
		}
	}


	private class OwnerItem
	{
		public enum State
		{
			NoHave,
			Have,
			Use,
		}

		public Item.Type type	{ get; set; }
		public State state		{ get; set; }
	}


	private class Number
	{
		public enum State
		{
			None,
			Action,
		}

		public State state				{ get; private set; }
		public float time				{ get; private set; }
		public float positionX			{ get; private set; }
		public float positionY			{ get; private set; }
		public bool visible				{ get; private set; }
		public Color color				{ get; private set; }
		public int value				{ get; private set; }
		private float aimDistance;
		private float aimSpeed;
		private float startPositionY;

		public Number ()
		{
			this.None ();
		}

		public void Init (Color color, float aimDistance, float aimSpeed)
		{
			this.color = color;
			this.aimDistance = aimDistance;
			this.aimSpeed = aimSpeed;
		}

		public void Move (float deltaTime, int frameRate)
		{
			if (this.state == State.Action) {
				this.positionY += this.aimSpeed * deltaTime * frameRate;
				if (this.positionY >= this.startPositionY + this.aimDistance) {
					this.None ();
				}
			}
		}

		private void None ()
		{
			this.state = State.None;
			this.time = 0;
			this.visible = false;
		}

		public void Action (float positionX, float positionY, int value)
		{
			if (this.state == State.None) {
				this.state = State.Action;
				this.time = 0;
				this.positionX = positionX;
				this.positionY = positionY;
				this.visible = true;
				this.value = value;
				this.startPositionY = positionY;
			}
		}
	}


	public class ClearPlayer
	{
		public Player.Compass compass	{ get; private set; }
		public float positionX			{ get; private set; }
		public float positionY			{ get; private set; }
		public int imageIndex			{ get; private set; }
		public float speed				{ get; private set; }
		private float imageTime;

		public void Init (float positionX, float positionY, float speed)
		{
			this.compass = Player.Compass.Right;
			this.positionX = positionX;
			this.positionY = positionY;
			this.speed = speed;
			this.imageIndex = Player.IMAGE_0;
		}

		public void Move (float deltaTime, int frameRate)
		{
			this.positionX += this.speed;
			int index = (int)this.imageTime % 4;
			switch (index) {
			case 0:
				this.imageIndex = Player.IMAGE_0;
				break;
			case 1:
				this.imageIndex = Player.IMAGE_1;
				break;
			case 2:
				this.imageIndex = Player.IMAGE_0;
				break;
			case 3:
				this.imageIndex = Player.IMAGE_2;
				break;
			}
			this.imageTime += this.speed * 0.02f;
		}
	}
	
	
	
	private class GroupPlayer
	{
		public GameObject gameObject;
		public GameObject gameObjectSurprise;
		public GameObject gameObjectLifeup;
		public GameObject gameObjectParasol;
		public GameObject gameObjectSweat;
		public int waitCount;
		public bool canHoleCycle;
		public float fallTime;
		public float invincibleTime;
		public Chip bathChip;
		public float bathTime;
		public Chip hideChip;
		public float hideTime;
		public float hoePer = 1;
		public float lifeTime;
		public bool isNonStop;
		public float surpriseTime;
		public Number lifeup;
		public float wellEffectTime;
		public bool isNonStopSound;
		public bool isParasol;
		public bool isSweat;
	}



	private class GroupEnemy
	{
		public enum DieType
		{
			None,
			Tomb,
			Hole,
		}

		public GameObject gameObject;
		public GameObject gameObjectNotice;
		public GameObject gameObjectLost;
		public int waitCount;
		public float waitTime;
		public float fallTime;
		public float angryTime;
		public float invincibleTime;
		public Chip bathChip;
		public float bathTime;
		public bool isFollow;
		public float noticeTime;
		public float lostTime;
		public DieType dieType;
		public float dieTime;
		public bool entrance;
		public bool isNonStop;
		public bool isBoss;
		public int lifeCount;
		public float wellEffectTime;
		public int weaponCount;
		public float weaponStopTime;
	}



	private class GroupChip
	{
		public GameObject gameObjectTerrain;
		public GameObject gameObjectHole;
		public List<GameObject> gameObjectObstacleList;
		public float holeTime;
	}



	private class GroupWeapon
	{
		public GameObject gameObject;
		public GameObject gameObjectImage;
	}
	
	
	
	private class GroupItem
	{
		public GameObject gameObject;
	}
	
	

	private class GroupBonus
	{
		public GameObject gameObject;
		public float remainingTime;
	}

	
	
	private class GroupLight
	{
		public GameObject gameObject;
	}
	
	
	
	private class GroupNumber
	{
		public GameObject gameObject;
		public GameObject gameObjectText;
	}
	
	
	
	private class CollectReady
	{
		public GameObject go;
		public GameObject goStage;
		public GameObject goDescription;
		public GameObject goClear;
		public GameObject goReady;
		public GameObject goGo;
		public Color color;
		public float colorAlphaMax = 200f / 256;
	}



	private class CollectPause
	{
		public GameObject go;
		public GameObject goDescription;
		public GameObject goClear;
		public GameObject goEncourage;
		public GameObject goButtonHowTo;
		public GameObject goButtonBack;
		public GameObject goVolumeOn;
		public GameObject goVolumeOff;
		public GameObject goHowTo;
		public GameObject goHowToImageJp;
		public GameObject goHowToImageEn;
		public GameObject goHowToButtonClose;
	}



	private class CollectClear
	{
		public GameObject go;
		public GameObject goTitle;
		public GameObject goEnemy;
		public GameObject goEnemyScore;
		public GameObject goTime;
		public GameObject goTimeScore;
		public GameObject goBonus;
		public Dictionary<int, GameObject> goBonusScoreList;
		public GameObject goRule;
		public GameObject goTotal;
		public GameObject goTotalScore;
		public GameObject goNextStage;
		public GameObject goPlayer;
		public Vector3 positionTitle;
		public Color color;
		public Color colorEnemy;
		public Color colorEnemyScore;
		public Color colorTime;
		public Color colorTimeScore;
		public Color colorBonus;
		public Dictionary<int, Color> colorBonusScoreList;
		public Color colorRule;
		public Color colorTotal;
		public Color colorTotalScore;
		public Color colorNextStage;
		public ClearPlayer player;
		public int bonusIndex;
		public float colorAlphaMax = 200f / 256;
		public bool isAllEnemyDie;
	}
	
	
	
	private class CollectContinue
	{
		public GameObject go;
		public GameObject goStage;
		public GameObject goScore;
		public GameObject goDescription;
		public GameObject goButtonContinue;
		public GameObject goButtonEnd;
	}
	
	
	
	private State state;
	private float time;
	private int pattern;

	private GameObject goCover;
	private GameObject goTouch;
	private GameObject goMist;
	private GameObject goTrail;
	private List<GameObject> goLayerList;

	private GameObject goOriginEnemy;
	private GameObject goOriginWeapon;
	private GameObject goOriginTerrain;
	private GameObject goOriginHole;
	private GameObject goOriginObstacle;
	private GameObject goOriginItem;
	private GameObject goOriginBonus;
	private GameObject goOriginLight;

	private List<Chip> chipList;
	private Player player;
	private List<Weapon> playerWeaponList;
	private List<Enemy> enemyList;
	private List<Weapon> enemyWeaponList;
	private List<Item> itemList;
	private List<Bonus> bonusList;
	private List<Hakaima.Light> lightList;
	private List<Item> specialItemList;

	private List<GroupChip> groupChipList;
	private GroupPlayer groupPlayer;
	private List<GroupWeapon> groupPlayerWeaponList;
	private List<GroupEnemy> groupEnemyList;
	private List<GroupWeapon> groupEnemyWeaponList;
	private List<GroupItem> groupItemList;
	private List<GroupBonus> groupBonusList;
	private List<GroupLight> groupLightList;
	private List<GroupItem> groupSpecialItemList;
	private List<GroupNumber> groupNumberList;

	private bool isPause;
	private Score score;
	private Life life;
	private MyWeapon myWeapon;
	private RemainingTime remainingTime;
	private List<OwnerItem> ownerItemList;
	private List<Number> numberList;
	private int numberIndex;

	private GameObject goPause;
	private GameObject goStage;
	private GameObject goScore;
	private GameObject goScoreHigh;
	private GameObject goPlayer1;
	private GameObject goLife;
	private GameObject goMyWeapon;
	private GameObject goRemainingTime;
	private GameObject goCompletion;
	private GameObject goCompletionText;
	private GameObject goPuaseTwitterButton;
	private GameObject goContinueTwitterButton;
	private GameObject goStageClearTwitterButton;
	private List<GameObject> goOwnerItemList;
	private List<GameObject> goOwnerItemFrameList;
	private List<GameObject> goNumberList;

	private CollectReady collectReady;
	private CollectPause collectPause;
	private CollectClear collectClear;
	private CollectContinue collectContinue;

	private TitleManager.Catalog help;
	private GameObject goHelp;
	private GameObject goHelpPage;
	private GameObject goHelpPoint;
	private GameObject goHelpArrowRight;
	private GameObject goHelpArrowLeft;

	private bool isTouch;
	private bool isCommandNoneClick;
	private Player.Compass playerNextCompass;

	private int playerNextCommand;
	public const int PLAYER_NEXT_COMMAND_NONE			= 0;
	public const int PLAYER_NEXT_COMMAND_COMPASS		= 1;
	public const int PLAYER_NEXT_COMMAND_COMPASS_WALK	= 2;

	private int continueCommand;
	private const int CONTINUE_COMMAND_NONE				= 0;
	private const int CONTINUE_COMMAND_YES				= 2;
	private const int CONTINUE_COMMAND_NO				= 3;

	private List<Bonus.Type> temporaryBonusTypeList;
	private Dictionary<Bonus.Type, bool> appearBonusTypeList;
	private bool isReachBonus6;

	private int tutorialPlayerIndex;
	private List<int> tutorialEnemyIndexList;
	
	private int lifeupIndex;

	private const float APPEAR_ENEMY_PERSON_TIME_COEFFICIENT = 3;
	private float appearEnemyPersonTime;

	private static List<Cell> keepCellList;

	private GameObject goDebug;
	private bool isDebugDamage;



	private void Awake ()
	{
		Init ();
		Create ();
	}



	private void Update ()
	{
		Run ();
		Draw ();
	}



	private void Init ()
	{
		groupChipList	= new List<GroupChip> ();
		groupPlayer		= new GroupPlayer ();
		groupEnemyList	= new List<GroupEnemy> ();
		groupPlayerWeaponList = new List<GroupWeapon> ();
		groupEnemyWeaponList = new List<GroupWeapon> ();
		groupItemList	= new List<GroupItem> ();
		groupBonusList	= new List<GroupBonus> ();
		groupLightList	= new List<GroupLight> ();
		groupSpecialItemList = new List<GroupItem> ();


		goCover			= transform.Find ("UI/Cover").gameObject;
		goTouch			= transform.Find ("Game/Touch").gameObject;
		goMist			= transform.Find ("Game/Mist").gameObject;
		goTrail			= transform.Find ("Game/Trail").gameObject;

		goLayerList		= new List<GameObject> ();
		goLayerList		.Add (transform.Find ("Game/Map/Layer0").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer1").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer2").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer3").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer4").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer5").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer6").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer7").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer8").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer9").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer10").gameObject);
		goLayerList		.Add (transform.Find ("Game/Map/Layer11").gameObject);

		goPause					= transform.Find ("UI/Information/Pause").gameObject;
		goStage					= transform.Find ("UI/Information/Stage").gameObject;
		goScore					= transform.Find ("UI/Information/Score/PlayerScore(Value)").gameObject;
		goScoreHigh				= transform.Find ("UI/Information/Score/HiScore(Value)").gameObject;
		goPlayer1				= transform.Find ("UI/Information/Score/PlayerScore(Title)").gameObject;
		goLife					= transform.Find ("UI/Information/Life").gameObject;
		goMyWeapon				= transform.Find ("UI/Information/ThrowWepon/Remaining").gameObject;
		goRemainingTime			= transform.Find ("UI/Information/Time").gameObject;
		goCompletion			= transform.Find ("UI/Information/Completion").gameObject;
		goCompletionText		= transform.Find ("UI/Information/Completion/Text").gameObject;

		goOriginEnemy			= transform.Find ("Game/Map/Origin/Enemy").gameObject;
		goOriginWeapon			= transform.Find ("Game/Map/Origin/Weapon").gameObject;
		goOriginTerrain			= transform.Find ("Game/Map/Origin/Terrain").gameObject;
		goOriginHole			= transform.Find ("Game/Map/Origin/Hole").gameObject;
		goOriginObstacle		= transform.Find ("Game/Map/Origin/Obstacle").gameObject;
		goOriginItem			= transform.Find ("Game/Map/Origin/Item").gameObject;
		goOriginBonus			= transform.Find ("Game/Map/Origin/Bonus").gameObject;
		goOriginLight			= transform.Find ("Game/Map/Origin/Light").gameObject;

		goOwnerItemList			= new List<GameObject> ();
		goOwnerItemList			.Add (transform.Find ("UI/Information/OwnerItem/Item0").gameObject);
		goOwnerItemList			.Add (transform.Find ("UI/Information/OwnerItem/Item1").gameObject);
		goOwnerItemList			.Add (transform.Find ("UI/Information/OwnerItem/Item2").gameObject);
		goOwnerItemList			.Add (transform.Find ("UI/Information/OwnerItem/Item3").gameObject);
		goOwnerItemList			.Add (transform.Find ("UI/Information/OwnerItem/Item4").gameObject);

		goOwnerItemFrameList	= new List<GameObject> ();
		goOwnerItemFrameList	.Add (transform.Find ("UI/Information/OwnerItem/Frame0").gameObject);
		goOwnerItemFrameList	.Add (transform.Find ("UI/Information/OwnerItem/Frame1").gameObject);
		goOwnerItemFrameList	.Add (transform.Find ("UI/Information/OwnerItem/Frame2").gameObject);
		goOwnerItemFrameList	.Add (transform.Find ("UI/Information/OwnerItem/Frame3").gameObject);
		goOwnerItemFrameList	.Add (transform.Find ("UI/Information/OwnerItem/Frame4").gameObject);

		groupPlayer.gameObject			= transform.Find ("Game/Map/Origin/Player").gameObject;
		groupPlayer.gameObjectSurprise	= transform.Find ("Game/Map/Num/Surprise").gameObject;
		groupPlayer.gameObjectLifeup	= transform.Find ("Game/Map/Num/Lifeup").gameObject;
		groupPlayer.gameObjectParasol	= transform.Find ("Game/Map/Origin/Player/Parasol").gameObject;
		groupPlayer.gameObjectSweat		= transform.Find ("Game/Map/Origin/Player/Sweat").gameObject;

		groupNumberList			= new List<GroupNumber> ();
		groupNumberList			.Add (new GroupNumber ());
		groupNumberList			.Add (new GroupNumber ());
		groupNumberList			.Add (new GroupNumber ());
		for (int i = 0; i < groupNumberList.Count; i++) {
			groupNumberList [i].gameObject		= transform.Find ("Game/Map/Num/Num" + i).gameObject;
			groupNumberList [i].gameObjectText	= transform.Find ("Game/Map/Num/Num" + i + "/Text").gameObject;
		}

		collectReady					= new CollectReady ();
		collectReady.go					= transform.Find ("UI/Logo/Ready").gameObject;
		collectReady.goStage			= collectReady.go.transform.Find ("Stage").gameObject;
		collectReady.goDescription		= collectReady.go.transform.Find ("Description").gameObject;
		collectReady.goClear			= collectReady.go.transform.Find ("Clear").gameObject;
		collectReady.goReady			= collectReady.go.transform.Find ("Ready").gameObject;
		collectReady.goGo				= collectReady.go.transform.Find ("Go").gameObject;

		collectPause					= new CollectPause ();
		collectPause.go					= transform.Find ("UI/Logo/Pause").gameObject;
		collectPause.goDescription		= collectPause.go.transform.Find ("Description").gameObject;
		collectPause.goClear			= collectPause.go.transform.Find ("Clear").gameObject;
		collectPause.goEncourage		= collectPause.go.transform.Find ("Encourage").gameObject;
		collectPause.goButtonHowTo		= collectPause.go.transform.Find ("ButtonHowTo").gameObject;
		collectPause.goButtonBack		= collectPause.go.transform.Find ("ButtonBack").gameObject;
		collectPause.goVolumeOn			= collectPause.go.transform.Find ("Volume/On").gameObject;
		collectPause.goVolumeOff		= collectPause.go.transform.Find ("Volume/Off").gameObject;
		collectPause.goHowTo			= collectPause.go.transform.Find ("HowTo").gameObject;
		collectPause.goHowToImageJp		= collectPause.go.transform.Find ("HowTo/Image").gameObject;
		collectPause.goHowToImageEn		= collectPause.go.transform.Find ("HowTo/Image_en").gameObject;
		collectPause.goHowToButtonClose	= collectPause.go.transform.Find ("HowTo/ButtonClose").gameObject;

		collectClear					= new CollectClear ();
		collectClear.go					= transform.Find ("UI/Logo/Clear").gameObject;
		collectClear.goTitle			= collectClear.go.transform.Find ("Title").gameObject;
		collectClear.goEnemy			= collectClear.go.transform.Find ("Enemy").gameObject;
		collectClear.goEnemyScore		= collectClear.go.transform.Find ("EnemyScore").gameObject;
		collectClear.goTime				= collectClear.go.transform.Find ("Time").gameObject;
		collectClear.goTimeScore		= collectClear.go.transform.Find ("TimeScore").gameObject;
		collectClear.goBonus			= collectClear.go.transform.Find ("Bonus").gameObject;
		collectClear.goRule				= collectClear.go.transform.Find ("Rule").gameObject;
		collectClear.goTotal			= collectClear.go.transform.Find ("Total").gameObject;
		collectClear.goTotalScore		= collectClear.go.transform.Find ("TotalScore").gameObject;
		collectClear.goNextStage		= collectClear.go.transform.Find ("NextStage").gameObject;
		collectClear.goPlayer			= collectClear.go.transform.Find ("Player").gameObject;
		collectClear.goBonusScoreList	= new Dictionary<int, GameObject> ();
		collectClear.goBonusScoreList [(int)Bonus.Type.Bonus0] = collectClear.go.transform.Find ("Bonus0Score").gameObject;
		collectClear.goBonusScoreList [(int)Bonus.Type.Bonus1] = collectClear.go.transform.Find ("Bonus1Score").gameObject;
		collectClear.goBonusScoreList [(int)Bonus.Type.Bonus2] = collectClear.go.transform.Find ("Bonus2Score").gameObject;
		collectClear.goBonusScoreList [(int)Bonus.Type.Bonus3] = collectClear.go.transform.Find ("Bonus3Score").gameObject;
		collectClear.goBonusScoreList [(int)Bonus.Type.Bonus4] = collectClear.go.transform.Find ("Bonus4Score").gameObject;
		collectClear.goBonusScoreList [(int)Bonus.Type.Bonus5] = collectClear.go.transform.Find ("Bonus5Score").gameObject;
		collectClear.goBonusScoreList [(int)Bonus.Type.Bonus6] = collectClear.go.transform.Find ("Bonus6Score").gameObject;
		collectClear.colorBonusScoreList = new Dictionary<int, Color> ();
		collectClear.player				= new ClearPlayer ();

		collectContinue						= new CollectContinue ();
		collectContinue.go					= transform.Find ("UI/Logo/Continue").gameObject;
		collectContinue.goStage				= collectContinue.go.transform.Find ("Stage(Value)").gameObject;
		collectContinue.goScore				= collectContinue.go.transform.Find ("Score(Value)").gameObject;
		collectContinue.goDescription		= collectContinue.go.transform.Find ("Description").gameObject;
		collectContinue.goButtonContinue	= collectContinue.go.transform.Find ("ButtonContinue").gameObject;
		collectContinue.goButtonEnd			= collectContinue.go.transform.Find ("ButtonEnd").gameObject;

		goPuaseTwitterButton				= collectPause.go.transform.Find ("Twitter").gameObject;
		goContinueTwitterButton				= collectContinue.go.transform.Find ("Twitter").gameObject;
		goStageClearTwitterButton			= collectClear.go.transform.Find ("Twitter").gameObject;
		goPuaseTwitterButton				.GetComponent<Button> ().onClick.AddListener (() => OnTwitter ());
		goContinueTwitterButton				.GetComponent<Button> ().onClick.AddListener (() => OnTwitter ());
		goStageClearTwitterButton			.GetComponent<Button> ().onClick.AddListener (() => OnTwitter ());
		goStageClearTwitterButton.SetActive (false);

		goCover.SetActive (true);

		goOriginEnemy.SetActive (false);
		goOriginWeapon.SetActive (false);
		goOriginTerrain.SetActive (false);
		goOriginHole.SetActive (false);
		goOriginObstacle.SetActive (false);
		goOriginItem.SetActive (false);
		goOriginBonus.SetActive (false);
		goOriginLight.SetActive (false);

		collectReady.go.SetActive (false);
		collectPause.go.SetActive (false);
		collectClear.go.SetActive (false);
		collectContinue.go.SetActive (false);
		goCompletion.SetActive (false);


		goTrail.GetComponent<TrailRenderer> ().sortingOrder = 1;

		groupPlayer.lifeup = new Number ();
		groupPlayer.lifeup.Init (Color.yellow, 100, 3);

		ownerItemList = new List<OwnerItem> ();
		ownerItemList.Add (new OwnerItem ());
		ownerItemList.Add (new OwnerItem ());
		ownerItemList.Add (new OwnerItem ());
		ownerItemList.Add (new OwnerItem ());
		ownerItemList.Add (new OwnerItem ());
		ownerItemList [0].type = Item.Type.Sandal;
		ownerItemList [1].type = Item.Type.Hoe;
		ownerItemList [2].type = Item.Type.Stone;
		ownerItemList [3].type = Item.Type.Amulet;
		ownerItemList [4].type = Item.Type.Parasol;

		numberList = new List<Number> ();
		numberList.Add (new Number ());
		numberList.Add (new Number ());
		numberList.Add (new Number ());
		for (int i = 0; i < numberList.Count; i++) {
			numberList [i].Init (Color.white, 50, 3);
		}


		goTouch.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.PointerDown)	.callback.AddListener (eventData => OnTouchPointerDown ((PointerEventData)eventData));
		goTouch.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.PointerUp)		.callback.AddListener (eventData => OnTouchPointerUp ((PointerEventData)eventData));
		goTouch.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.Drag)			.callback.AddListener (eventData => OnTouchDrag ((PointerEventData)eventData));

		goPause.GetComponent<Button> ().onClick.AddListener (() => OnPause (!isPause));

		goOwnerItemList [0].GetComponent<Button> ().onClick.AddListener (() => OnItem (ownerItemList [0]));
		goOwnerItemList [1].GetComponent<Button> ().onClick.AddListener (() => OnItem (ownerItemList [1]));
		goOwnerItemList [2].GetComponent<Button> ().onClick.AddListener (() => OnItem (ownerItemList [2]));
		goOwnerItemList [3].GetComponent<Button> ().onClick.AddListener (() => OnItem (ownerItemList [3]));

		collectPause.goButtonHowTo			.GetComponent<Button> ().onClick.AddListener (() => OnHowTo (true));
		collectPause.goButtonBack			.GetComponent<Button> ().onClick.AddListener (() => OnPause (false));
		collectPause.goVolumeOn				.GetComponent<Button> ().onClick.AddListener (() => OnVolume (true));
		collectPause.goVolumeOff			.GetComponent<Button> ().onClick.AddListener (() => OnVolume (false));
		collectPause.goHowToButtonClose		.GetComponent<Button> ().onClick.AddListener (() => OnHowTo (false));
		collectContinue.goButtonContinue	.GetComponent<Button> ().onClick.AddListener (() => OnContinue (CONTINUE_COMMAND_YES));
		collectContinue.goButtonEnd			.GetComponent<Button> ().onClick.AddListener (() => OnContinue (CONTINUE_COMMAND_NO));


		OnVolume (SoundManager.Instance.GetMute ());



		goDebug = transform.Find ("UI/Debug").gameObject;
		goDebug.transform.Find ("Damage").GetComponent<Button> ().onClick.AddListener (() => OnDebugDamageClick ());
		goDebug.transform.Find ("PreStage").GetComponent<Button> ().onClick.AddListener (() => {
			keepCellList = null;
			MainManager.Instance.PreStage ();
		});
		goDebug.transform.Find ("NextStage").GetComponent<Button> ().onClick.AddListener (() => {
			keepCellList = null;
			MainManager.Instance.NextStage (life.now, myWeapon.now);
		});

		OnDebugDamageClick ();
		if (!MainManager.Instance.isDebug) {
			goDebug.SetActive (false);
		}
	}



	private void Create ()
	{
		score = new Score (MainManager.Instance.score, MainManager.Instance.scoreHigh);
		life = new Life (MainManager.Instance.life);
		myWeapon = new MyWeapon (MainManager.Instance.weapon);
		remainingTime = new RemainingTime (MainManager.Instance.isTutorial ? 0 : Data.GetStageData (MainManager.Instance.stage).limitTime);

		if (MainManager.Instance.isTutorial)
			keepCellList = null;
		keepCellList = keepCellList ?? GetCellList ();
		List<Cell> cellList = keepCellList;


		chipList = new List<Chip> ();
		for (int i = 0; i < cellList.Count; i++) {
			Cell cell = cellList [i];
			Chip chip = new Chip ();
			chip.Init (cell.pointX, cell.pointY);
			chipList.Add (chip);
			GroupChip groupChip = new GroupChip ();
			groupChip.gameObjectObstacleList = new List<GameObject> ();
			groupChipList.Add (groupChip);

			chip.terrain.Set (cell.terrainType);
			groupChip.gameObjectTerrain = Instantiate (goOriginTerrain) as GameObject;
			groupChip.gameObjectTerrain.transform.SetParent (goOriginTerrain.transform.parent);

			if (cell.isHoleOpen)
				chip.hole.Open ();
			groupChip.gameObjectHole = Instantiate (goOriginHole) as GameObject;
			groupChip.gameObjectHole.transform.SetParent (goOriginHole.transform.parent);

			if (cell.obstacleType != Obstacle.Type.None) {
				Obstacle obstacle = new Obstacle ();
				obstacle.Set (cell.obstacleType);
				chip.obstacleList.Add (obstacle);
				GameObject goObstacle = Instantiate (goOriginObstacle) as GameObject;
				goObstacle.transform.SetParent (goOriginObstacle.transform.parent);
				groupChip.gameObjectObstacleList.Add (goObstacle);
				if (cell.obstacleType == Obstacle.Type.Tomb) {
					if (UnityEngine.Random.Range (0, 2) == 0) {
						obstacle = new Obstacle ();
						obstacle.Set (Obstacle.Type.Stupa);
						chip.obstacleList.Insert (chip.obstacleList.Count - 1, obstacle);
						goObstacle = Instantiate (goOriginObstacle) as GameObject;
						goObstacle.transform.SetParent (goOriginObstacle.transform.parent);
						groupChip.gameObjectObstacleList.Insert (groupChip.gameObjectObstacleList.Count - 1, goObstacle);
					}
				} else if (cell.obstacleType == Obstacle.Type.CartRight) {
					if (UnityEngine.Random.Range (0, 2) == 0) {
						chip.obstacleList.Find (obj => obj.type == Obstacle.Type.CartRight).Set (Obstacle.Type.CartLeft);
					}
				}
			}
		}

		player = new Player ();
		player.Init (Data.PLAYER_START_POINT_X, Data.PLAYER_START_POINT_Y - 1);
		
		enemyList = new List<Enemy> ();
		for (int i = 0; i < cellList.Count; i++) {
			Cell cell = cellList [i];
			if (cell.enemyType != Enemy.Type.None) {
				Enemy enemy = new Enemy ();
				enemy.Init (cell.enemyType, cell.pointX, cell.pointY, Data.GetEnemyData (cell.enemyType).isFly);
				enemyList.Add (enemy);
				GroupEnemy groupEnemy = new GroupEnemy ();
				groupEnemy.gameObject = Instantiate (goOriginEnemy) as GameObject;
				groupEnemy.gameObject.transform.SetParent (goOriginEnemy.transform.parent);
				groupEnemyList.Add (groupEnemy);
				groupEnemy.gameObjectNotice = groupEnemy.gameObject.transform.Find ("Notice").gameObject;
				groupEnemy.gameObjectLost = groupEnemy.gameObject.transform.Find ("Lost").gameObject;
				groupEnemy.isBoss = enemy.type == Enemy.Type.Tengu;
				groupEnemy.lifeCount = Data.GetEnemyData (enemy.type).lifeCount;
			}
		}

		playerWeaponList = new List<Weapon> ();
		enemyWeaponList = new List<Weapon> ();

		itemList = new List<Item> ();
		for (int i = 0; i < cellList.Count; i++) {
			Cell cell = cellList [i];
			if (cell.itemType != Item.Type.None) {
				Item item = new Item ();
				item.Init (cell.itemType, cell.pointX, cell.pointY);
				itemList.Add (item);
				GroupItem groupItem = new GroupItem ();
				groupItem.gameObject = Instantiate (goOriginItem) as GameObject;
				groupItem.gameObject.transform.SetParent (goOriginItem.transform.parent);
				groupItemList.Add (groupItem);
			}
		}
		
		bonusList = new List<Bonus> ();

		lightList = new List<Hakaima.Light> ();
		for (int i = 0; i < cellList.Count; i++) {
			Cell cell = cellList [i];
			if (cell.obstacleType == Obstacle.Type.Tower) {
				Hakaima.Light light = new Hakaima.Light ();
				light.Init (cell.pointX, cell.pointY, Data.LAYER_9);
				lightList.Add (light);
				GroupLight groupLight = new GroupLight ();
				groupLight.gameObject = Instantiate (goOriginLight) as GameObject;
				groupLight.gameObject.transform.SetParent (goOriginLight.transform.parent);
				groupLightList.Add (groupLight);
			}
		}

		specialItemList = new List<Item> ();

		goStage.GetComponent<Text> ().text = string.Format ("STAGE {0}", MainManager.Instance.stage + 1);

		float darkness = Data.GetStageData (MainManager.Instance.stage).darkness;
		goTouch.GetComponent<Image> ().color = Color.black * darkness;
		
		float mist = Data.GetStageData (MainManager.Instance.stage).mist * 0.2f;
		goMist.GetComponent<ParticleSystem> ().startColor = new Color (1, 1, 1, mist);
		if (mist == 0)
			goMist.SetActive (false);

		string clearText;
		if (enemyList.Exists (obj => obj.type == Enemy.Type.Tengu)) {
			clearText = Language.sentence [Language.GAME_READY_APPEAR_BOSS];
		} else if (enemyList.Exists (obj => obj.type == Enemy.Type.Person)) {
			clearText = Language.sentence [Language.GAME_READY_APPEAR_SAMURAI];
		} else {
			clearText = Language.sentence [Language.GAME_READY_APPEAR_NONE];
		}
		collectReady.goStage.GetComponent<Text> ().text = string.Format ("STAGE {0}", MainManager.Instance.stage + 1);
		collectReady.goDescription.GetComponent<Text> ().text = Language.sentence [Language.GAME_READY_CONDITION];
		collectPause.goDescription.GetComponent<Text> ().text = Language.sentence [Language.GAME_READY_CONDITION];
		collectReady.goClear.GetComponent<Text> ().text = clearText;
		collectPause.goClear.GetComponent<Text> ().text = clearText;
		collectPause.goEncourage.GetComponent<Text> ().text = Language.sentence [Language.GAME_PAUSE];
		collectContinue.goDescription.GetComponent<Text> ().text = Language.sentence [Language.GAME_CONTINUE];
		if (Language.sentence == Language.sentenceEn) {
			collectReady.goClear.GetComponent<Text> ().alignment = TextAnchor.UpperCenter;
			collectPause.goClear.GetComponent<Text> ().alignment = TextAnchor.UpperCenter;
		}

		groupPlayer.lifeTime = 180;
		temporaryBonusTypeList = new List<Bonus.Type> ();
		appearBonusTypeList = new Dictionary<Bonus.Type, bool> ();
		appearBonusTypeList [Bonus.Type.Bonus0] = false;
		appearBonusTypeList [Bonus.Type.Bonus1] = false;
		appearBonusTypeList [Bonus.Type.Bonus2] = false;
		appearBonusTypeList [Bonus.Type.Bonus3] = false;
		appearBonusTypeList [Bonus.Type.Bonus4] = false;
		appearBonusTypeList [Bonus.Type.Bonus5] = false;
		appearBonusTypeList [Bonus.Type.Bonus6] = false;

		lifeupIndex = Data.GetLifeupIndex (score.now);

		tutorialEnemyIndexList = new List<int> ();
		for (int i = 0; i < Data.tutorialStageData.enemyDataListList.Count; i++) {
			tutorialEnemyIndexList.Add (0);
		}

		if (MainManager.Instance.isTutorial) {
			goPause.SetActive (false);
			goStage.GetComponent<Text> ().text = "TUTORIAL";
		}

		ResourceManager.Instance.SetTerrian ();
		ResourceManager.Instance.SetObstacle ();
		ResourceManager.Instance.SetHole ();
		ResourceManager.Instance.SetItem ();
		ResourceManager.Instance.SetBonus ();
		ResourceManager.Instance.SetAllEnemy ();
		ResourceManager.Instance.SetPlayer ();
		transform.Find ("UI/Information/Face").GetComponent<Image>().sprite = ResourceManager.Instance.spriteUpperPlayer;
		SetWeapon ();

		goPlayer1.GetComponent<Text> ().text = Language.sentenceEn [Language.CHARANAME_SAMURAI + MainManager.Instance.selectCharacter];
	}



	private void Run ()
	{
		bool loop;
		do {
			loop = false;

			switch (state) {
			case State.Ready:
				{
					if (MainManager.Instance.isTutorial) {
						if (pattern == 0) {
							if (time == 0) {
								goCover.SetActive (false);
								player.Walk (Player.Compass.Top, true);
								player.SetSpeed (Data.SPEED_3);
								player.SetImageCoefficient (Data.GetPlayerImageCoefficient (Hakaima.Terrain.Type.Soil));
								player.SetBlind (true, Color.cyan);
								enemyList.ForEach (obj => obj.SetBlind (true, Color.white));
								goStageClearTwitterButton.SetActive (false);
							} else if (time >= 2) {
								player.SetBlind (false);
								enemyList.ForEach (obj => obj.SetBlind (false));
								state = State.Play;
								time = 0;
								loop = true;
							}
							if (player.state == Player.State.Wait)
							if (player.compass == Player.Compass.Top)
								player.Walk (Player.Compass.Bottom, false);
						}
					} else {
						if (pattern == 0) {
							if (time == 0) {
								goCover.SetActive (false);
								collectReady.go.SetActive (true);
								collectReady.goReady.SetActive (true);
								collectReady.goGo.SetActive (false);
								collectReady.color = Color.black;
								collectReady.color.a = collectReady.colorAlphaMax;
								enemyList.ForEach (obj => obj.SetBlind (true, Color.white));
								SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_START);
							} else if (time >= 4) {
								pattern = 1;
								time = 0;
							}
						}
						if (pattern == 1) {
							if (time == 0) {
								collectReady.goReady.SetActive (false);
								collectReady.goGo.SetActive (true);
								player.Walk (Player.Compass.Top, true);
								player.SetSpeed (Data.SPEED_3);
								player.SetImageCoefficient (Data.GetPlayerImageCoefficient (Hakaima.Terrain.Type.Soil));
								player.SetBlind (true, Color.cyan);
							} else if (time >= 1) {
								if (player.state == Player.State.Wait) {
									collectReady.go.SetActive (false);
									player.SetBlind (false);
									enemyList.ForEach (obj => obj.SetBlind (false));
									state = State.Play;
									time = 0;
									loop = true;
								}
							}
							if (player.state == Player.State.Wait)
							if (player.compass == Player.Compass.Top)
								player.Walk (Player.Compass.Bottom, false);

							collectReady.color = Color.black;
							collectReady.color.a = Mathf.Lerp (collectReady.colorAlphaMax, 0, 2 * time - 1);
						}
					}

					player.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
					enemyList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
					chipList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
				}
				break;
			case State.ReadyContinue:
				{
					if (pattern == 0) {
						if (time == 0) {
							collectReady.go.SetActive (true);
							collectReady.goReady.SetActive (true);
							collectReady.goGo.SetActive (false);
							collectReady.color = Color.black;
							collectReady.color.a = collectReady.colorAlphaMax;
							SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_START);
						} else if (time >= 3) {
							pattern = 1;
							time = 0;
						}
					}
					if (pattern == 1) {
						if (time == 0) {
							collectReady.goReady.SetActive (false);
							collectReady.goGo.SetActive (true);
						} else if (time >= 1) {
							collectReady.go.SetActive (false);
							state = State.Play;
							time = 0;
							loop = true;
						}
						collectReady.color = Color.black;
						collectReady.color.a = Mathf.Lerp (collectReady.colorAlphaMax, 0, 2 * time - 1);
					}
				}
				break;
			case State.Play:
				{
					CheckBackKey ();

					if (isPause) {
						return;
					}

					if (time == 0) {
						playerNextCompass = player.compass;
						playerNextCommand = PLAYER_NEXT_COMMAND_NONE;
						SoundManager.Instance.StopSe ();

						if (!MainManager.Instance.isTutorial) {
							bool isBoss = groupEnemyList.Exists (obj => obj.isBoss);
							if (isBoss) {
								SoundManager.Instance.PlayBgm (SoundManager.BgmName.BGM_BOSS);
							} else {
								switch (Data.GetStageData (MainManager.Instance.stage).sound) {
								case 0:
									SoundManager.Instance.PlayBgm (SoundManager.BgmName.BGM_GAME_0);
									break;
								case 1:
									SoundManager.Instance.PlayBgm (SoundManager.BgmName.BGM_GAME_1);
									break;
								case 2:
									SoundManager.Instance.PlayBgm (SoundManager.BgmName.BGM_GAME_2);
									break;
								}
							}
						}
					}


					switch (player.state) {
					case Player.State.Wait:
						{
							if (MainManager.Instance.isTutorial) {
								if (tutorialPlayerIndex < Data.tutorialStageData.playerDataList.Count) {
									if (Data.tutorialStageData.playerDataList [tutorialPlayerIndex].waitTime > 0) {
										Data.tutorialStageData.playerDataList [tutorialPlayerIndex].waitTime -= Time.deltaTime;
										goCompletion.SetActive (true);
										goCompletionText.GetComponent<Text> ().text = Data.tutorialStageData.playerDataList [tutorialPlayerIndex].waitText;
										return;
									}
									playerNextCompass = Data.tutorialStageData.playerDataList [tutorialPlayerIndex].compass;
									playerNextCommand = Data.tutorialStageData.playerDataList [tutorialPlayerIndex].command;
									isCommandNoneClick = Data.tutorialStageData.playerDataList [tutorialPlayerIndex].isCommandNoneClick;
									groupPlayer.canHoleCycle = Data.tutorialStageData.playerDataList [tutorialPlayerIndex].canHoleCycle;
									tutorialPlayerIndex++;
									goCompletion.SetActive (false);
								}
							} else {
								if (!isTouch) {
									groupPlayer.canHoleCycle = false;
									playerNextCompass = player.compass;
									playerNextCommand = PLAYER_NEXT_COMMAND_NONE;
								}
								if (groupPlayer.isNonStop) {
									groupPlayer.isNonStop = false;
									playerNextCompass = player.compass;
									playerNextCommand = PLAYER_NEXT_COMMAND_COMPASS_WALK;
								}
							}
					
							Chip chip = chipList [player.pointX + player.pointY * Data.LENGTH_X];
							Chip chip1 = null;

							switch (playerNextCompass) {
							case Player.Compass.Right:
								if (player.pointX + 1 < Data.LENGTH_X)
									chip1 = chipList [(player.pointX + 1) + player.pointY * Data.LENGTH_X];
								break;
							case Player.Compass.Left:
								if (player.pointX - 1 >= 0)
									chip1 = chipList [(player.pointX - 1) + player.pointY * Data.LENGTH_X];
								break;
							case Player.Compass.Top:
								if (player.pointY + 1 < Data.LENGTH_Y)
									chip1 = chipList [player.pointX + (player.pointY + 1) * Data.LENGTH_X];
								break;
							case Player.Compass.Bottom:
								if (player.pointY - 1 >= 0)
									chip1 = chipList [player.pointX + (player.pointY - 1) * Data.LENGTH_X];
								break;
							}
							
							GroupChip groupChip = groupChipList [chip.pointX + chip.pointY * Data.LENGTH_X];
							GroupChip groupChip1 = chip1 != null ? groupChipList [chip1.pointX + chip1.pointY * Data.LENGTH_X] : null;

							switch (playerNextCommand) {
							case PLAYER_NEXT_COMMAND_NONE:
								{
									OwnerItem useOwnerItem = ownerItemList.Find (obj => obj.type == Item.Type.Stone);
									if (useOwnerItem.state == OwnerItem.State.Use) {
										if (chip1 != null)
										if (Data.GetTerrainData (chip1.terrain.type).isThrough)
										if (chip1.hole.state == Hole.State.Close)
										if (chip1.obstacleList.Count == 0) {
											useOwnerItem.state = OwnerItem.State.NoHave;
											Obstacle obstacle = new Obstacle ();
											obstacle.Set (Obstacle.Type.Stone);
											chip1.obstacleList.Add (obstacle);
											GameObject goObstacle = Instantiate (goOriginObstacle) as GameObject;
											goObstacle.transform.SetParent (goOriginObstacle.transform.parent);
											groupChip1.gameObjectObstacleList.Add (goObstacle);
										}
									}
									if (groupPlayer.canHoleCycle) {
										if (chip1 != null)
										if (Data.GetTerrainData (chip1.terrain.type).isDigging)
										if (playerWeaponList.Count == 0)
										if (chip1.obstacleList.Count == 0) {
											int point = chip1.pointX + chip1.pointY * Data.LENGTH_X;
											groupChipList [point].holeTime += Data.DELTA_TIME * groupPlayer.hoePer;
											if (groupChipList [point].holeTime >= 0.3f) {
												groupChipList [point].holeTime = 0;
												chip1.hole.Cycle ();
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_HOLE);
												if (chip1.hole.state == Hole.State.Open || chip1.hole.state == Hole.State.Close) {
													groupPlayer.canHoleCycle = false;
													switch (chip1.hole.state) {
													case Hole.State.Open:
														Item item = itemList.Find (obj => obj.pointX == chip1.pointX && obj.pointY == chip1.pointY);
														if (item != null)
															item.Appear ();
														else {
															Item.Type itemType = Item.Type.None;
															float ran = UnityEngine.Random.value * 100;
															if (ran < MainManager.Instance.GetWeaponItemPercent () && MainManager.Instance.IsWeaponCharacter (MainManager.Instance.selectCharacter))
																itemType = Item.Type.Weapon;
															if (itemType != Item.Type.None) {
																Item specialItem = new Item ();
																specialItem.Init (itemType, chip1.pointX, chip1.pointY);
																specialItem.Appear ();
																specialItemList.Add (specialItem);
																GroupItem groupSpecialItem = new GroupItem ();
																groupSpecialItem.gameObject = Instantiate (goOriginItem) as GameObject;
																groupSpecialItem.gameObject.transform.SetParent (goOriginItem.transform.parent);
																groupSpecialItemList.Add (groupSpecialItem);
															}
														}
														PlayerPrefs.SetInt (Data.RECORD_HOLE_OPEN, PlayerPrefs.GetInt (Data.RECORD_HOLE_OPEN) + 1);
														break;
													case Hole.State.Close:
														switch (chip1.terrain.type) {
														case Hakaima.Terrain.Type.Grass:
														case Hakaima.Terrain.Type.Ice:
															chip1.terrain.Set (Hakaima.Terrain.Type.Soil);
															break;
														}
														PlayerPrefs.SetInt (Data.RECORD_HOLE_CLOSE, PlayerPrefs.GetInt (Data.RECORD_HOLE_CLOSE) + 1);
														break;
													}
												}
											}
										}
									}
									if (isCommandNoneClick) {
										if (chip1 != null) {
											if (chip1.obstacleList.Exists (obj => obj.type == Obstacle.Type.Stone)) {
												chip1.obstacleList.Find (obj => obj.type == Obstacle.Type.Stone).Set (Obstacle.Type.Tomb);
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_MAKE);
											} else if (chip1.obstacleList.Exists (obj => obj.type == Obstacle.Type.Bale)) {
												bool isBale = chip.obstacleList.Count == 0;
												if (!isBale) {
													List<Obstacle.Type> list = new List<Obstacle.Type> ();
													list.Add (Obstacle.Type.TombCollapseEnd);
													list.Add (Obstacle.Type.TombPieceEnd);
													list.Add (Obstacle.Type.Bucket);
													list.Add (Obstacle.Type.FallTombPieceEnd);
													isBale = chip.obstacleList.TrueForAll (obj => list.Exists (type => type == obj.type));
												}
												if (isBale) {
													player.SetPoint (chip1.pointX, chip1.pointY);
													Obstacle obstacle = chip1.obstacleList.Find (obj => obj.type == Obstacle.Type.Bale);
													chip.obstacleList.Add (obstacle);
													chip1.obstacleList.Remove (obstacle);
													GameObject goObstacle = groupChip1.gameObjectObstacleList [0];
													groupChip.gameObjectObstacleList.Add (goObstacle);
													groupChip1.gameObjectObstacleList.Remove (goObstacle);
													SoundManager.Instance.PlaySe (SoundManager.SeName.SE_CHANGE);
												}
											} else if (chip1.obstacleList.Exists (obj => obj.type == Obstacle.Type.Tomb)) {
												Func<Chip, List<Chip>, List<Chip>> getTombChipList = null;
												getTombChipList = (Chip tombChip, List<Chip> tombChipList) => {
													tombChipList.Add (tombChip);
													Chip nextChip = null;
													switch (playerNextCompass) {
													case Player.Compass.Right:
														if (tombChip.pointX + 1 < Data.LENGTH_X)
															nextChip = chipList [(tombChip.pointX + 1) + tombChip.pointY * Data.LENGTH_X];
														break;
													case Player.Compass.Left:
														if (tombChip.pointX - 1 >= 0)
															nextChip = chipList [(tombChip.pointX - 1) + tombChip.pointY * Data.LENGTH_X];
														break;
													case Player.Compass.Top:
														if (tombChip.pointY + 1 < Data.LENGTH_Y)
															nextChip = chipList [tombChip.pointX + (tombChip.pointY + 1) * Data.LENGTH_X];
														break;
													case Player.Compass.Bottom:
														if (tombChip.pointY - 1 >= 0)
															nextChip = chipList [tombChip.pointX + (tombChip.pointY - 1) * Data.LENGTH_X];
														break;
													}
													if (nextChip == null) {
														tombChipList.Add (tombChipList [tombChipList.Count - 1]);
													} else {
														if (nextChip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Tomb)) {
															return getTombChipList (nextChip, tombChipList);
														}
														tombChipList.Add (nextChip);
													}
													return tombChipList;
												};
												
												List<Chip> list = new List<Chip> ();
												list = getTombChipList (chip1, list);
												for (int i = 0; i < list.Count; i++) {
													int point = list [i].pointX + list [i].pointY * Data.LENGTH_X;
													if (i == list.Count - 1) {
														bool isPiece = true;
														if (chipList [point].terrain.type == Hakaima.Terrain.Type.River)
															isPiece = false;
														if (chipList [point].obstacleList.Exists (obj => obj.type == Obstacle.Type.Well))
															isPiece = false;
														if (chipList [point].obstacleList.Exists (obj => obj.type == Obstacle.Type.Bathtub))
															isPiece = false;
														if (chipList [point].obstacleList.Exists (obj => obj.type == Obstacle.Type.Stockpile))
															isPiece = false;
														Obstacle obstacle = new Obstacle ();
														obstacle.Set (isPiece ? Obstacle.Type.TombPiece : Obstacle.Type.FallTombPiece);
														chipList [point].obstacleList.Add (obstacle);
														GameObject goObstacle = Instantiate (goOriginObstacle) as GameObject;
														goObstacle.transform.SetParent (goOriginObstacle.transform.parent);
														groupChipList [point].gameObjectObstacleList.Add (goObstacle);
														if (chipList [point].hole.state != Hole.State.Close)
															chipList [point].hole.Close ();
													} else {
														chipList [point].obstacleList.Find (obj => obj.type == Obstacle.Type.Tomb).Set (Obstacle.Type.TombCollapse);
														if (chipList [point].obstacleList.Exists (obj => obj.type == Obstacle.Type.Stupa)) {
															int index = chipList [point].obstacleList.FindIndex (obj => obj.type == Obstacle.Type.Stupa);
															Destroy (groupChipList [point].gameObjectObstacleList [index]);
															chipList [point].obstacleList.RemoveAt (index);
															groupChipList [point].gameObjectObstacleList.RemoveAt (index);
														}
														PlayerPrefs.SetInt (Data.RECORD_TOMB_COLLAPSE, PlayerPrefs.GetInt (Data.RECORD_TOMB_COLLAPSE) + 1);
													}
												}
												if (!appearBonusTypeList [Bonus.Type.Bonus4]) {
													int remain = (int)(Data.GetStageData (MainManager.Instance.stage).obstacleTypeList [Obstacle.Type.Tomb] * 0.2f);
													if (chipList.Sum (obj => obj.obstacleList.Count (obj2 => obj2.type == Obstacle.Type.Tomb)) < remain) {
														appearBonusTypeList [Bonus.Type.Bonus4] = true;
														temporaryBonusTypeList.Add (Bonus.Type.Bonus4);
													}
												}
												if (list.Count - 1 >= 5) {
													isReachBonus6 = true;
												}
												if (list.Count > 0)
													SoundManager.Instance.PlaySe (SoundManager.SeName.SE_KNOCK);
												if (PlayerPrefs.GetInt (Data.RECORD_MAX_TOMB_COLLAPSE) < list.Count - 1) {
													PlayerPrefs.SetInt (Data.RECORD_MAX_TOMB_COLLAPSE, list.Count - 1);
												}
											} else {
												if (canThrough)
												if (MainManager.Instance.IsWeaponCharacter (MainManager.Instance.selectCharacter) && myWeapon.now > 0) {
													Weapon weapon = new Weapon ();
													weapon.Init ((Weapon.Compass)player.compass, player.positionX, player.positionY);
													playerWeaponList.Add (weapon);
													GroupWeapon groupWeapon = new GroupWeapon ();
													groupWeapon.gameObject = Instantiate (goOriginWeapon) as GameObject;
													groupWeapon.gameObjectImage = groupWeapon.gameObject.transform.Find ("Image").gameObject;
													groupPlayerWeaponList.Add (groupWeapon);
													SoundManager.Instance.PlaySe (SoundManager.SeName.SE_TOUCH);
													myWeapon.now--;
													goMyWeapon.GetComponent<Text> ().text = string.Format ("{0:00}", myWeapon.now).ToString ();
												}
											}
										}
									}
								}
								break;
							case PLAYER_NEXT_COMMAND_COMPASS:
								{
									player.Walk (playerNextCompass, false);
								}
								break;
							case PLAYER_NEXT_COMMAND_COMPASS_WALK:
								{
									bool isWalk = false;
									if (chip1 != null) {
										if (Data.GetTerrainData (chip1.terrain.type).isThrough && chip1.obstacleList.TrueForAll (obj => Data.GetObstacleData (obj.type).isThrough))
											isWalk = true;
										if (chip1.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartRight) && playerNextCompass != Player.Compass.Right)
											isWalk = false;
										if (chip1.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartLeft) && playerNextCompass != Player.Compass.Left)
											isWalk = false;
									}
									player.Walk (playerNextCompass, isWalk);
								}
								break;
							}
						}
						break;
					}

					for (int i = 0; i < enemyList.Count; i++) {
						Enemy enemy = enemyList [i];
						GroupEnemy groupEnemy = groupEnemyList [i];
						switch (enemy.state) {
						case Enemy.State.Wait:
							{
								if (groupEnemy.waitTime == 0) {
									if (groupEnemy.bathTime == 0) {

										bool isNext = false;
										if (MainManager.Instance.isTutorial) {
											isNext = true;
										} else {
											if (IsEnemyNextCommand (enemy))
												isNext = true;
											if (groupEnemy.isNonStop)
												isNext = true;
										}

										if (isNext) {
											Enemy.Compass enemyNextCompass = GetEnemyNextCompass (chipList, player, enemy, groupEnemy);

											if (MainManager.Instance.isTutorial) {
												if (tutorialEnemyIndexList [i] < Data.tutorialStageData.enemyDataListList [i].Count) {
													enemyNextCompass = Data.tutorialStageData.enemyDataListList [i] [tutorialEnemyIndexList [i]].compass;
													tutorialEnemyIndexList [i]++;
												}
											} else {
												if (groupEnemy.isNonStop) {
													groupEnemy.isNonStop = false;
													enemyNextCompass = enemy.compass;
												}
											}

											Chip chip1 = null;
											switch (enemyNextCompass) {
											case Enemy.Compass.Right:
												if (enemy.pointX + 1 < Data.LENGTH_X)
													chip1 = chipList [(enemy.pointX + 1) + enemy.pointY * Data.LENGTH_X];
												break;
											case Enemy.Compass.Left:
												if (enemy.pointX - 1 >= 0)
													chip1 = chipList [(enemy.pointX - 1) + enemy.pointY * Data.LENGTH_X];
												break;
											case Enemy.Compass.Top:
												if (enemy.pointY + 1 < Data.LENGTH_Y)
													chip1 = chipList [enemy.pointX + (enemy.pointY + 1) * Data.LENGTH_X];
												break;
											case Enemy.Compass.Bottom:
												if (enemy.pointY - 1 >= 0)
													chip1 = chipList [enemy.pointX + (enemy.pointY - 1) * Data.LENGTH_X];
												break;
											}
											bool isWalk = false;
											if (chip1 != null) {
												if (Data.GetTerrainData (chip1.terrain.type).isThrough && chip1.obstacleList.TrueForAll (obj => Data.GetObstacleData (obj.type).isThrough))
													isWalk = true;
												if (chip1.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartRight) && enemyNextCompass != Enemy.Compass.Right)
													isWalk = false;
												if (chip1.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartLeft) && enemyNextCompass != Enemy.Compass.Left)
													isWalk = false;
												if (chip1.hole.state == Hole.State.Open && Data.GetEnemyData (enemy.type).isAvoidHole)
													isWalk = false;
												if (Data.GetEnemyData (enemy.type).isFly)
													isWalk = true;
											}
											enemy.Walk (enemyNextCompass, isWalk);

											if (groupEnemy.isBoss) {
												if (groupEnemy.weaponStopTime == 0) {
													bool isWeapon = false;
													if (remainingTime.now == 0) {
														isWeapon = true;
													} else {
														if (groupEnemy.lifeCount == 1)
															isWeapon = groupEnemy.weaponCount % 3 != 0;
														else
															isWeapon = groupEnemy.weaponCount % 2 == 0;
													}
													if (isWeapon) {
														Weapon weapon = new Weapon ();
														weapon.Init ((Weapon.Compass)enemy.compass, enemy.positionX, enemy.positionY);
														enemyWeaponList.Add (weapon);
														GroupWeapon groupWeapon = new GroupWeapon ();
														groupWeapon.gameObject = Instantiate (goOriginWeapon) as GameObject;
														groupWeapon.gameObjectImage = groupWeapon.gameObject.transform.Find ("Image").gameObject;
														groupEnemyWeaponList.Add (groupWeapon);
														SoundManager.Instance.PlaySe (SoundManager.SeName.SE_TOUCH);
													}
													groupEnemy.weaponCount++;
													if (groupEnemy.weaponCount >= 20) {
														groupEnemy.weaponCount = 0;
														groupEnemy.weaponStopTime = 2;
													}
												}
											}
										} else {
											groupEnemy.waitTime = 1;
										}
										if (groupEnemy.entrance) {
											groupEnemy.entrance = false;
											enemy.SetBlind (false);
										}
									}
								}
								break;
							}
						}
					}

					for (int i = 0; i < ownerItemList.Count; i++) {
						OwnerItem ownerItem = ownerItemList [i];
						switch (ownerItem.type) {
						case Item.Type.Sandal:
							if (ownerItem.state == OwnerItem.State.Have) {
								ownerItem.state = OwnerItem.State.Use;
							}
							break;
						case Item.Type.Hoe:
							if (ownerItem.state == OwnerItem.State.Have) {
								ownerItem.state = OwnerItem.State.Use;
								groupPlayer.hoePer = 1.5f;
							}
							break;
						case Item.Type.Stone:
							if (ownerItem.state == OwnerItem.State.Use)
								ownerItem.state = OwnerItem.State.Have;
							break;
						case Item.Type.Amulet:
							if (ownerItem.state == OwnerItem.State.Have) {
								ownerItem.state = OwnerItem.State.Use;
								player.SetBlind (true, Color.white);
								groupPlayer.invincibleTime = 10f;
							}
							break;
						case Item.Type.Parasol:
							if (ownerItem.state == OwnerItem.State.Have) {
								ownerItem.state = OwnerItem.State.Use;
								groupPlayer.isParasol = true;
							}
							break;
						}
					}

					isCommandNoneClick = false;

				
					chipList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
					player.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
					enemyList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
					playerWeaponList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
					enemyWeaponList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
					bonusList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
					numberList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
					groupPlayer.lifeup.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
				
			
					groupPlayer.isSweat = false;
					canThrough = true;
					for (int i = 0; i < enemyList.Count; i++) {
						Enemy enemy = enemyList [i];
						GroupEnemy groupEnemy = groupEnemyList [i];
						if (groupEnemy.entrance)
							continue;
						switch (enemy.state) {
						case Enemy.State.Wait:
						case Enemy.State.Walk:
						case Enemy.State.Fall:
							{
								bool isDie = false;
								bool isDamage = false;
								switch (enemy.state) {
								case Enemy.State.Wait:
									{
										Chip chip = chipList [enemy.pointX + enemy.pointY * Data.LENGTH_X];
										if (groupEnemy.waitCount == 0) {
											if (!Data.GetEnemyData (enemy.type).isFly) {
												if (chip.terrain.type == Hakaima.Terrain.Type.Ice) {
													groupEnemy.isNonStop = true;
												}
												if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartRight)) {
													chip.obstacleList.Find (obj => obj.type == Obstacle.Type.CartRight).Set (Obstacle.Type.CartLeft);
												} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartLeft)) {
													chip.obstacleList.Find (obj => obj.type == Obstacle.Type.CartLeft).Set (Obstacle.Type.CartRight);
												} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Bathtub)) {
													groupEnemy.isNonStop = false;
													groupEnemy.bathChip = chip;
													groupEnemy.bathTime = 10;
													enemy.InSide ();
												} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Well)) {
													groupEnemy.isNonStop = false;
													List<Chip> chipAimList = chipList.FindAll (obj => obj.obstacleList.Exists (obj2 => obj2.type == Obstacle.Type.Well) && obj != chip);
													if (chipAimList.Count > 0) {
														int index = UnityEngine.Random.Range (0, chipAimList.Count);
														enemy.SetPoint (chipAimList [index].pointX, chipAimList [index].pointY);
														enemy.SetBlind (true, Color.cyan);
														groupEnemy.wellEffectTime = 1f;
													}
												}
											}
										}
										if (chip.hole.state == Hole.State.Open) {
											if (!Data.GetEnemyData (enemy.type).isFly) {
												groupEnemy.fallTime = Data.GetEnemyData (enemy.type).fallTime;
												enemy.Fall ();
												enemy.SetBlind (false);
											}
										}
										groupEnemy.waitCount++;
									}
									break;
								case Enemy.State.Walk:
									{
										groupEnemy.waitCount = 0;
									}
									break;
								case Enemy.State.Fall:
									{
										Chip chip = chipList [enemy.pointX + enemy.pointY * Data.LENGTH_X];
										switch (chip.hole.state) {
										case Hole.State.Close:
											{
												isDie = true;
												groupEnemy.lifeCount = 0;
												groupEnemy.dieType = GroupEnemy.DieType.Hole;
												groupEnemy.dieTime = 0;
												PlayerPrefs.SetInt (Data.RECORD_ENEMY_DIE_TO_HOLE, PlayerPrefs.GetInt (Data.RECORD_ENEMY_DIE_TO_HOLE) + 1);
											}
											break;
										}
									}
									break;
								}

								if (groupEnemy.waitTime > 0) {
									groupEnemy.waitTime -= Data.DELTA_TIME;
									if (groupEnemy.waitTime <= 0) {
										groupEnemy.waitTime = 0;
									}
								}
								if (groupEnemy.fallTime > 0) {
									groupEnemy.fallTime -= Data.DELTA_TIME;
									if (groupEnemy.fallTime <= 0) {
										groupEnemy.fallTime = 0;
										groupEnemy.angryTime = Data.GetEnemyData (enemy.type).angryTime;
										enemy.Climb ();
										enemy.SetBlind (true, Color.red);
									}
								}
								if (groupEnemy.angryTime > 0) {
									groupEnemy.angryTime -= Data.DELTA_TIME;
									if (groupEnemy.angryTime <= 0) {
										groupEnemy.angryTime = 0;
										enemy.SetBlind (false);
									}
								}
								if (groupEnemy.invincibleTime > 0) {
									groupEnemy.invincibleTime -= Data.DELTA_TIME;
									if (groupEnemy.invincibleTime <= 0) {
										groupEnemy.invincibleTime = 0;
										enemy.SetBlind (groupEnemy.angryTime > 0, Color.red);
									}
								}
								if (groupEnemy.bathTime > 0) {
									groupEnemy.bathTime -= Data.DELTA_TIME;
									if (groupEnemy.bathTime <= 0 || groupEnemy.bathChip.obstacleList.Exists (obj => obj.type != Obstacle.Type.Bathtub)) {
										groupEnemy.bathTime = 0;
										groupEnemy.bathChip = null;
										enemy.OutSide ();
									}
								}
								if (groupEnemy.wellEffectTime > 0) {
									groupEnemy.wellEffectTime -= Data.DELTA_TIME;
									if (groupEnemy.wellEffectTime <= 0) {
										groupEnemy.wellEffectTime = 0;
										enemy.SetBlind (groupEnemy.angryTime > 0, Color.red);
										enemy.SetBlind (groupEnemy.invincibleTime > 0, Color.white);
									}
								}
								if (groupEnemy.noticeTime > 0) {
									groupEnemy.noticeTime -= Data.DELTA_TIME;
									if (groupEnemy.noticeTime <= 0) {
										groupEnemy.noticeTime = 0;
									}
								}
								if (groupEnemy.lostTime > 0) {
									groupEnemy.lostTime -= Data.DELTA_TIME;
									if (groupEnemy.lostTime <= 0) {
										groupEnemy.lostTime = 0;
									}
								}
								if (groupEnemy.weaponStopTime > 0) {
									groupEnemy.weaponStopTime -= Data.DELTA_TIME;
									if (groupEnemy.weaponStopTime <= 0) {
										groupEnemy.weaponStopTime = 0;
									}
								}
								if (groupEnemy.isFollow) {
									if (!MainManager.Instance.isTutorial)
										groupPlayer.isSweat = true;
								}
								{
									int pointX = Mathf.FloorToInt ((enemy.positionX + enemy.size / 2) / enemy.size);
									int pointY = Mathf.FloorToInt ((enemy.positionY + enemy.size / 2) / enemy.size);
									Chip chip = chipList [pointX + pointY * Data.LENGTH_X];

									enemy.SetSpeed (Data.GetEnemySpeed (enemy.type, chip.terrain.type, groupEnemy.angryTime > 0, groupEnemy.isFollow, !MainManager.Instance.isTutorial && remainingTime.now == 0));
									enemy.SetImageCoefficient (Data.GetEnemyImageCoefficient (enemy.type, chip.terrain.type));

									if (groupEnemy.invincibleTime == 0) {
										if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.TombPiece || obj.type == Obstacle.Type.TombCollapse || obj.type == Obstacle.Type.FallTombPiece)) {
											groupEnemy.lifeCount--;
											if (groupEnemy.lifeCount == 0) {
												isDie = true;
												groupEnemy.dieType = GroupEnemy.DieType.Tomb;
												groupEnemy.dieTime = 0;
												PlayerPrefs.SetInt (Data.RECORD_ENEMY_DIE_TO_TOMB, PlayerPrefs.GetInt (Data.RECORD_ENEMY_DIE_TO_TOMB) + 1);
											} else {
												isDamage = true;
											}
										}
									}
								}
								for (int j = 0; j < playerWeaponList.Count;) {
									Weapon weapon = playerWeaponList [j];
									if (Math.Abs (weapon.positionX - enemy.positionX) < weapon.size * weapon.scaleX && Math.Abs (weapon.positionY - enemy.positionY) < weapon.size * weapon.scaleY) {
										groupEnemy.lifeCount--;
										if (groupEnemy.lifeCount == 0) {
											isDie = true;
											groupEnemy.dieType = GroupEnemy.DieType.Tomb;
											groupEnemy.dieTime = 0;
										} else {
											isDamage = true;
										}
										Destroy (groupPlayerWeaponList [j].gameObject);
										playerWeaponList.RemoveAt (j);
										groupPlayerWeaponList.RemoveAt (j);
										continue;
									}
									j++;
								}
								if (isDie) {
									if (groupEnemy.isBoss) {
										state = State.BossDefeat;
										time = 0;
										pattern = 0;
										loop = true;
									} else {
										enemy.Die ();
									}

									if (!MainManager.Instance.isTutorial) {
										int s = Data.GetEnemyScore (enemy.type, groupEnemy.angryTime > 0);
										if (s > 0) {
											score.enemy += s;
											numberList [numberIndex].Action (enemy.positionX, enemy.positionY, s);
											numberIndex = (numberIndex + 1) % numberList.Count;
										}
										int index = Data.GetLifeupIndex (score.now);
										if (lifeupIndex < index) {
											lifeupIndex++;
											life.now++;
											groupPlayer.lifeup.Action (player.positionX, player.positionY, 1);
											SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_1UP);
										}
									}
									
									if (enemy.type == Enemy.Type.Person) {
										if (MainManager.Instance.stage < 5) {
											appearEnemyPersonTime = APPEAR_ENEMY_PERSON_TIME_COEFFICIENT * 1;
										} else if (MainManager.Instance.stage < 10) {
											appearEnemyPersonTime = APPEAR_ENEMY_PERSON_TIME_COEFFICIENT * (1 + (int)(UnityEngine.Random.value * 2));
										} else {
											appearEnemyPersonTime = APPEAR_ENEMY_PERSON_TIME_COEFFICIENT * (2 + (int)(UnityEngine.Random.value * 2));
										}
									}
								} else if (isDamage) {
									groupEnemy.invincibleTime = 5;
									enemy.SetBlind (true, Color.white);
									if (groupEnemy.isBoss)
									if (groupEnemy.lifeCount == 1)
										groupEnemy.angryTime = Data.GetStageData (MainManager.Instance.stage).limitTime;
								}
							}
							break;
						case Enemy.State.Damage:
							{
								if (enemy.isEnd) {
									enemy.Die ();
								}
							}
							break;
						case Enemy.State.Die:
							groupEnemy.dieTime += Data.DELTA_TIME;
							break;
						}
					}
				
					switch (player.state) {
					case Player.State.Wait:
					case Player.State.Walk:
					case Player.State.Fall:
						{
							switch (player.state) {
							case Player.State.Wait:
								{
									Chip chip = chipList [player.pointX + player.pointY * Data.LENGTH_X];
									if (groupPlayer.waitCount == 0) {
										if (chip.terrain.type == Hakaima.Terrain.Type.Ice) {
											groupPlayer.isNonStop = true;
										}
										if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartRight)) {
											chip.obstacleList.Find (obj => obj.type == Obstacle.Type.CartRight).Set (Obstacle.Type.CartLeft);
											SoundManager.Instance.PlaySe (SoundManager.SeName.SE_GIMMICK);
										} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.CartLeft)) {
											chip.obstacleList.Find (obj => obj.type == Obstacle.Type.CartLeft).Set (Obstacle.Type.CartRight);
											SoundManager.Instance.PlaySe (SoundManager.SeName.SE_GIMMICK);
										} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Well)) {
											//	groupPlayer.isNonStop = true;
											List<Chip> chipAimList = chipList.FindAll (obj => obj.obstacleList.Exists (obj2 => obj2.type == Obstacle.Type.Well) && obj != chip);
											if (chipAimList.Count > 0) {
												int index = UnityEngine.Random.Range (0, chipAimList.Count);
												player.SetPoint (chipAimList [index].pointX, chipAimList [index].pointY);
												player.SetBlind (true, Color.cyan);
												player.Walk (Player.Compass.Bottom, false);
												groupPlayer.wellEffectTime = 1f;
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_WARP);
											}
										} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Bathtub)) {
											if (groupPlayer.isNonStop) {
												groupPlayer.isNonStop = false;
												groupPlayer.bathChip = chip;
												groupPlayer.bathTime = Data.GetPlayerData ().bathTime;
												player.InSide ();
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_GIMMICK);
											}
										} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Stockpile)) {
											if (groupPlayer.isNonStop) {
												groupPlayer.isNonStop = false;
												groupPlayer.hideChip = chip;
												groupPlayer.hideTime = Data.GetPlayerData ().hideTime;
												player.InSide ();
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_GIMMICK);
											}
										}
										if (groupPlayer.isNonStop) {
											if (!groupPlayer.isNonStopSound) {
												groupPlayer.isNonStopSound = true;
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_SLIP);
											}
										} else {
											groupPlayer.isNonStopSound = false;
										}
									} else if (groupPlayer.waitCount == 1) {
										if (groupPlayer.invincibleTime == 0) {
											if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Bathtub)) {
												groupPlayer.bathChip = chip;
												groupPlayer.bathTime = Data.GetPlayerData ().bathTime;
												player.InSide ();
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_GIMMICK);
											} else if (chip.obstacleList.Exists (obj => obj.type == Obstacle.Type.Stockpile)) {
												groupPlayer.hideChip = chip;
												groupPlayer.hideTime = Data.GetPlayerData ().hideTime;
												player.InSide ();
												SoundManager.Instance.PlaySe (SoundManager.SeName.SE_GIMMICK);
											}
											if (chip.hole.state == Hole.State.Open) {
												groupPlayer.fallTime = Data.GetPlayerData ().fallTime;
												player.Fall ();
												PlayerPrefs.SetInt (Data.RECORD_HOLE_FALL, PlayerPrefs.GetInt (Data.RECORD_HOLE_FALL) + 1);
											}
										}
										groupPlayer.isNonStopSound = false;
									}
									groupPlayer.waitCount++;
								}
								break;
							case Player.State.Walk:
								{
									groupPlayer.waitCount = 0;
								}
								break;
							}

							if (groupPlayer.invincibleTime == 0) {
								if (groupPlayer.bathTime == 0) {
									if (groupPlayer.hideTime == 0) {
										if (isDebugDamage) {
											for (int i = 0; i < enemyList.Count; i++) {
												Enemy enemy = enemyList [i];
												GroupEnemy groupEnemy = groupEnemyList [i];
												if (enemy.state == Enemy.State.Wait || enemy.state == Enemy.State.Walk)
												if (groupEnemy.bathTime == 0)
												if (Math.Abs (enemy.positionX - player.positionX) < player.size * 0.7f && Math.Abs (enemy.positionY - player.positionY) < player.size * 0.7f) {
													player.Damage ();
													ownerItemList.Find (obj => obj.type == Item.Type.Sandal).state = OwnerItem.State.NoHave;
													ownerItemList.Find (obj => obj.type == Item.Type.Hoe).state = OwnerItem.State.NoHave;
													ownerItemList.Find (obj => obj.type == Item.Type.Parasol).state = OwnerItem.State.NoHave;
													groupPlayer.hoePer = 1;
													groupPlayer.isParasol = false;
													SoundManager.Instance.PlaySe (SoundManager.SeName.SE_BUMP);
												}
											}
											for (int i = 0; i < enemyWeaponList.Count; i++) {
												Weapon weapon = enemyWeaponList [i];
												if (Math.Abs (weapon.positionX - player.positionX) < weapon.size * weapon.scaleX && Math.Abs (weapon.positionY - player.positionY) < weapon.size * weapon.scaleY) {
													player.Damage ();
													ownerItemList.Find (obj => obj.type == Item.Type.Sandal).state = OwnerItem.State.NoHave;
													ownerItemList.Find (obj => obj.type == Item.Type.Hoe).state = OwnerItem.State.NoHave;
													ownerItemList.Find (obj => obj.type == Item.Type.Parasol).state = OwnerItem.State.NoHave;
													groupPlayer.hoePer = 1;
													groupPlayer.isParasol = false;
													SoundManager.Instance.PlaySe (SoundManager.SeName.SE_BUMP);
												}
											}
										}
									}
								}
							}
							if (groupPlayer.fallTime > 0) {
								groupPlayer.fallTime -= Data.DELTA_TIME;
								if (groupPlayer.fallTime <= 0) {
									groupPlayer.fallTime = 0;
									player.Climb ();
								}
							}
							if (groupPlayer.invincibleTime > 0) {
								groupPlayer.invincibleTime -= Data.DELTA_TIME;
								if (groupPlayer.invincibleTime <= 0) {
									groupPlayer.invincibleTime = 0;
									player.SetBlind (false);
									ownerItemList.Find (obj => obj.type == Item.Type.Amulet).state = OwnerItem.State.NoHave;
								}
							}
							if (groupPlayer.bathTime > 0) {
								groupPlayer.bathTime -= Data.DELTA_TIME;
								if (groupPlayer.bathTime <= 0 || player.state != Player.State.Wait) {
									groupPlayer.bathTime = 0;
									groupPlayer.bathChip.obstacleList.Find (obj => obj.type == Obstacle.Type.Bathtub).Set (Obstacle.Type.BathtubCollapse);
									groupPlayer.bathChip = null;
									groupPlayer.invincibleTime = 10f;
									player.SetBlind (true, Color.white);
									player.OutSide ();
									SoundManager.Instance.PlaySe (SoundManager.SeName.SE_BREAK);
								}
							}
							if (groupPlayer.hideTime > 0) {
								groupPlayer.hideTime -= Data.DELTA_TIME;
								if (groupPlayer.hideTime <= 0 || player.state != Player.State.Wait) {
									groupPlayer.hideTime = 0;
									groupPlayer.hideChip.obstacleList.Find (obj => obj.type == Obstacle.Type.Stockpile).Set (Obstacle.Type.StockpileCollapse);
									groupPlayer.hideChip = null;
									player.OutSide ();
									SoundManager.Instance.PlaySe (SoundManager.SeName.SE_BREAK);
								}
							}
							if (groupPlayer.wellEffectTime > 0) {
								groupPlayer.wellEffectTime -= Data.DELTA_TIME;
								if (groupPlayer.wellEffectTime <= 0) {
									groupPlayer.wellEffectTime = 0;
									player.SetBlind (groupPlayer.invincibleTime > 0, Color.white);
								}
							}
							if (groupPlayer.lifeTime > 0) {
								groupPlayer.lifeTime -= Data.DELTA_TIME;
								if (groupPlayer.lifeTime <= 0) {
									groupPlayer.lifeTime = 0;
									if (!appearBonusTypeList [Bonus.Type.Bonus5]) {
										appearBonusTypeList [Bonus.Type.Bonus5] = true;
										temporaryBonusTypeList.Add (Bonus.Type.Bonus5);
									}
								}
							}
							if (groupPlayer.surpriseTime > 0) {
								groupPlayer.surpriseTime -= Data.DELTA_TIME;
								if (groupPlayer.surpriseTime <= 0) {
									groupPlayer.surpriseTime = 0;
								}
							}

							for (int i = 0; i < itemList.Count;) {
								Item item = itemList [i];
								if (item.visible) {
									OwnerItem ownerItem = ownerItemList.Find (obj => obj.type == item.type);
									if (ownerItem.state == OwnerItem.State.NoHave) {
										if (Math.Abs (item.positionX - player.positionX) < player.size && Math.Abs (item.positionY - player.positionY) < player.size) {
											Destroy (groupItemList [i].gameObject);
											ownerItem.state = OwnerItem.State.Have;
											itemList.RemoveAt (i);
											groupItemList.RemoveAt (i);
											SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_GOT);
											PlayerPrefs.SetInt (Data.RECORD_ITEM_GET, PlayerPrefs.GetInt (Data.RECORD_ITEM_GET) + 1);
											continue;
										}
									}
								}
								i++;
							}
							for (int i = 0; i < specialItemList.Count;) {
								Item specialItem = specialItemList [i];
								if (Math.Abs (specialItem.positionX - player.positionX) < player.size && Math.Abs (specialItem.positionY - player.positionY) < player.size) {
									Destroy (groupSpecialItemList [i].gameObject);
									specialItemList.RemoveAt (i);
									groupSpecialItemList.RemoveAt (i);
									SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_GOT);
									MainManager.Instance.AddWeapon ();
									continue;
								}
								i++;
							}
							for (int i = 0; i < bonusList.Count;) {
								Bonus bonus = bonusList [i];
								if (Math.Abs (bonus.positionX - player.positionX) < player.size && Math.Abs (bonus.positionY - player.positionY) < player.size) {
									Destroy (groupBonusList [i].gameObject);
									bonusList.RemoveAt (i);
									groupBonusList.RemoveAt (i);
									int s = Data.GetBonusData (bonus.type).score;
									score.bonusList [(int)bonus.type] += s;
									numberList [numberIndex].Action (bonus.positionX, bonus.positionY, s);
									numberIndex = (numberIndex + 1) % numberList.Count;
									int index = Data.GetLifeupIndex (score.now);
									if (lifeupIndex < index) {
										lifeupIndex++;
										life.now++;
										groupPlayer.lifeup.Action (player.positionX, player.positionY, 1);
										SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_1UP);
									}
									SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_GOT);
									PlayerPrefs.SetInt (Data.RECORD_BONUS_GET, PlayerPrefs.GetInt (Data.RECORD_BONUS_GET) + 1);
									continue;
								}
								i++;
							}
							{
								int pointX = Mathf.FloorToInt ((player.positionX + player.size / 2) / player.size);
								int pointY = Mathf.FloorToInt ((player.positionY + player.size / 2) / player.size);
								Chip chip = chipList [pointX + pointY * Data.LENGTH_X];

								bool isSandal = ownerItemList.Find (obj => obj.type == Item.Type.Sandal).state == OwnerItem.State.Use;
								player.SetSpeed (Data.GetPlayerSpeed (chip.terrain.type, isSandal));
								player.SetImageCoefficient (Data.GetPlayerImageCoefficient (chip.terrain.type));
							}

							if (MainManager.Instance.isTutorial) {
								if (tutorialPlayerIndex == Data.tutorialStageData.playerDataList.Count) {
									state = State.TutorialEnd;
									time = 0;
									pattern = 0;
									loop = true;
								}
							} else {
								bool isBoss = groupEnemyList.Exists (obj => obj.isBoss);
								if (isBoss) {
									remainingTime.now -= Data.DELTA_TIME;
									if (remainingTime.now == 0) {
										goRemainingTime.GetComponent<Text> ().color = Color.red;
									}
								} else {
									if (enemyList.FindAll (obj => obj.type != Enemy.Type.Person).TrueForAll (obj => obj.state == Enemy.State.Die)) {
										if (numberList.TrueForAll (obj => obj.state == Number.State.None)) {
											collectClear.goTitle.GetComponent<Text> ().text = "STAGE CLEAR !";
											collectClear.isAllEnemyDie = true;
											state = State.Clear;
											time = 0;
											pattern = 0;
											loop = true;
											SoundManager.Instance.StopBgm ();
											SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_CLEAR);
										}
									} else {
										remainingTime.now -= Data.DELTA_TIME;
										if (remainingTime.now == 0) {
											collectClear.goTitle.GetComponent<Text> ().text = "GOT CLEAR AWAY!";
											state = State.Clear;
											time = 0;
											pattern = 0;
											loop = true;
											SoundManager.Instance.StopBgm ();
											SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_CLEAR_TIME0);
										}
									}
								}
							}
						}
						break;
					case Player.State.Damage:
						{
							if (player.isEnd) {
								if (life.now == 0) {
									state = State.Continue;
									time = 0;
									loop = true;
									MainManager.Instance.ShowInterstitialNoMovie();
								} else {
									life.now--;
									player.Rise ();
									player.SetBlind (true, Color.white);
									groupPlayer.invincibleTime = 5f;
									groupPlayer.lifeTime = 0;
								}
								PlayerPrefs.SetInt (Data.RECORD_DAMAGE, PlayerPrefs.GetInt (Data.RECORD_DAMAGE) + 1);
							}
							groupPlayer.surpriseTime = 0;
						}
						break;
					}

					for (int i = 0; i < playerWeaponList.Count;) {
						Weapon weapon = playerWeaponList [i];
						bool isEnd = false;
						if (weapon.isEnd) {
							isEnd = true;
						}
						{
							int pointX = Mathf.FloorToInt ((weapon.positionX + weapon.size / 2) / weapon.size);
							int pointY = Mathf.FloorToInt ((weapon.positionY + weapon.size / 2) / weapon.size);
							if (pointX < 0 || pointX == Data.LENGTH_X) {
								isEnd = true;
							} else if (pointY < 0 || pointY == Data.LENGTH_Y) {
								isEnd = true;
							} else {
								Chip chip = chipList [pointX + pointY * Data.LENGTH_X];
								if (chip.obstacleList.Exists (obj => !Data.GetObstacleData (obj.type).isThrough))
									isEnd = true;
							}
						}
						if (isEnd) {
							Destroy (groupPlayerWeaponList [i].gameObject);
							playerWeaponList.RemoveAt (i);
							groupPlayerWeaponList.RemoveAt (i);
							continue;
						}
						i++;
					}
					for (int i = 0; i < enemyWeaponList.Count;) {
						Weapon weapon = enemyWeaponList [i];
						bool isEnd = false;
						if (weapon.isEnd) {
							isEnd = true;
						}
						{
							int pointX = Mathf.FloorToInt ((weapon.positionX + weapon.size / 2) / weapon.size);
							int pointY = Mathf.FloorToInt ((weapon.positionY + weapon.size / 2) / weapon.size);
							//ここでバグる
							Chip chip = chipList [pointX + pointY * Data.LENGTH_X];
							if (chip.obstacleList.Exists (obj => !Data.GetObstacleData (obj.type).isThrough))
								isEnd = true;
						}
						if (isEnd) {
							Destroy (groupEnemyWeaponList [i].gameObject);
							enemyWeaponList.RemoveAt (i);
							groupEnemyWeaponList.RemoveAt (i);
							continue;
						}
						i++;
					}

					if (groupEnemyList.Exists (obj => obj.dieType != GroupEnemy.DieType.None && obj.dieTime == 0)) {
						if (groupEnemyList.Count (obj => obj.dieType != GroupEnemy.DieType.None && obj.dieTime <= 30) >= 5) {
							if (!appearBonusTypeList [Bonus.Type.Bonus0]) {
								appearBonusTypeList [Bonus.Type.Bonus0] = true;
								temporaryBonusTypeList.Add (Bonus.Type.Bonus0);
							}
						}
						if (groupEnemyList.Count (obj => obj.dieType == GroupEnemy.DieType.Tomb && obj.dieTime <= 15) >= 3) {
							if (!appearBonusTypeList [Bonus.Type.Bonus1]) {
								appearBonusTypeList [Bonus.Type.Bonus1] = true;
								temporaryBonusTypeList.Add (Bonus.Type.Bonus1);
							}
						}
						if (groupEnemyList.Count (obj => obj.dieType == GroupEnemy.DieType.Hole && obj.dieTime <= 30) >= 3) {
							if (!appearBonusTypeList [Bonus.Type.Bonus2]) {
								appearBonusTypeList [Bonus.Type.Bonus2] = true;
								temporaryBonusTypeList.Add (Bonus.Type.Bonus2);
							}
						}
						if (groupEnemyList.Count (obj => obj.dieType != GroupEnemy.DieType.None && obj.dieTime == 0) >= 2) {
							if (!appearBonusTypeList [Bonus.Type.Bonus3]) {
								appearBonusTypeList [Bonus.Type.Bonus3] = true;
								temporaryBonusTypeList.Add (Bonus.Type.Bonus3);
							}
						}
						if (groupEnemyList.Count (obj => obj.dieType == GroupEnemy.DieType.Tomb && obj.dieTime == 0) >= 1) {
							if (!appearBonusTypeList [Bonus.Type.Bonus6]) {
								appearBonusTypeList [Bonus.Type.Bonus6] = true;
								if (isReachBonus6)
									temporaryBonusTypeList.Add (Bonus.Type.Bonus6);
							}
						}
					}
					for (int i = 0; i < temporaryBonusTypeList.Count; i++) {
						List<Chip> bonusChipList = chipList.FindAll (obj => Data.GetTerrainData (obj.terrain.type).isThrough && obj.obstacleList.Count == 0);
						for (int j = 0; j < bonusList.Count; j++) {
							bonusChipList = bonusChipList.FindAll (obj => !(obj.pointX == bonusList [j].pointX && obj.pointY == bonusList [j].pointY));
						}
						if (bonusChipList.Count > 0) {
							Chip chip = bonusChipList [UnityEngine.Random.Range (0, bonusChipList.Count)];
							Bonus bonus = new Bonus ();
							bonus.Init (temporaryBonusTypeList [i], chip.pointX, chip.pointY);
							bonusList.Add (bonus);
							GroupBonus groupBonus = new GroupBonus ();
							groupBonus.gameObject = Instantiate (goOriginBonus) as GameObject;
							groupBonus.remainingTime = 20;
							groupBonusList.Add (groupBonus);
							PlayerPrefs.SetInt (Data.RECORD_BONUS_APPEAR, PlayerPrefs.GetInt (Data.RECORD_BONUS_APPEAR) + 1);
						}
					}
					temporaryBonusTypeList.Clear ();
					isReachBonus6 = false;

					for (int i = 0; i < bonusList.Count;) {
						groupBonusList [i].remainingTime -= Data.DELTA_TIME;
						if (groupBonusList [i].remainingTime <= 0) {
							Destroy (groupBonusList [i].gameObject);
							bonusList.RemoveAt (i);
							groupBonusList.RemoveAt (i);
							continue;
						} else if (groupBonusList [i].remainingTime <= 5) {
							if (!bonusList [i].blind)
								bonusList [i].SetBlind (true);
						}
						i++;
					}

					if (appearEnemyPersonTime > 0) {
						int appearIndex = (int)(appearEnemyPersonTime / APPEAR_ENEMY_PERSON_TIME_COEFFICIENT);
						appearEnemyPersonTime -= Data.DELTA_TIME;
						if (appearEnemyPersonTime < 0)
							appearEnemyPersonTime = 0;
						if (appearIndex != (int)(appearEnemyPersonTime / APPEAR_ENEMY_PERSON_TIME_COEFFICIENT)) {
							Enemy enemy = new Enemy ();
							int startX = Data.PLAYER_START_POINT_X;
							if (chipList [Data.PLAYER_START_POINT_X].obstacleList.Count > 0) {
								if (chipList [Data.PLAYER_START_POINT_X + 1].obstacleList.Count == 0)
									startX = Data.PLAYER_START_POINT_X + 1;
								else if (chipList [Data.PLAYER_START_POINT_X - 1].obstacleList.Count == 0)
									startX = Data.PLAYER_START_POINT_X - 1;
							}
							enemy.Init (Enemy.Type.Person, startX, Data.PLAYER_START_POINT_Y - 1, Data.GetEnemyData (Enemy.Type.Person).isFly);
							enemy.Walk (Enemy.Compass.Top, true);
							enemy.SetSpeed (Data.GetEnemySpeed (enemy.type, Hakaima.Terrain.Type.Soil, false, false, false));
							enemy.SetImageCoefficient (Data.GetEnemyImageCoefficient (enemy.type, Hakaima.Terrain.Type.Soil));
							enemy.SetBlind (true, Color.white);
							enemyList.Add (enemy);
							GroupEnemy groupEnemy = new GroupEnemy ();
							groupEnemy.gameObject = Instantiate (goOriginEnemy) as GameObject;
							groupEnemy.gameObject.transform.SetParent (goOriginEnemy.transform.parent);
							groupEnemyList.Add (groupEnemy);
							groupEnemy.gameObjectNotice = groupEnemy.gameObject.transform.Find ("Notice").gameObject;
							groupEnemy.gameObjectLost = groupEnemy.gameObject.transform.Find ("Lost").gameObject;
							groupEnemy.lifeCount = Data.GetEnemyData (enemy.type).lifeCount;
							groupEnemy.entrance = true;
							groupPlayer.surpriseTime = 1.5f;
						}
					}
				}
				break;
			case State.BossDefeat:
				{
					Enemy enemy = null;
					for (int i = 0; i < enemyList.Count; i++) {
						if (groupEnemyList [i].isBoss) {
							enemy = enemyList [i];
						}
					}
					if (time == 0) {
						enemy.Damage ();
					}

					enemy.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);

					if (enemy.isEnd) {
						enemy.Die ();
						collectClear.goTitle.GetComponent<Text> ().text = "STAGE CLEAR !";
						state = State.Clear;
						time = 0;
						pattern = 0;
						loop = true;
						SoundManager.Instance.StopBgm ();
						SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_CLEAR);
					}
				}
				break;
			case State.Clear:
				{
					bool loop2;
					do {
						loop2 = false;

						if (pattern == 0) {
							if (time == 0) {
								collectClear.go.SetActive (true);
								collectClear.goBonus.SetActive (!score.bonusList.ToList ().TrueForAll (obj => obj.Value == 0));
								int pos = 0;
								foreach (Bonus.Type type in Enum.GetValues (typeof(Bonus.Type))) {
									if (type == Bonus.Type.None)
										continue;
									if (score.bonusList [(int)type] == 0) {
										collectClear.goBonusScoreList [(int)type].SetActive (false);
										continue;
									}
									collectClear.goBonusScoreList [(int)type].transform.localPosition = new Vector3 (0, -20 - 80 * pos);
									pos++;
								}
								collectClear.positionTitle = Vector3.zero;
								collectClear.goRule.transform.localPosition = new Vector3 (0, -40 - 80 * pos);
								collectClear.goTotal.transform.localPosition = collectClear.goRule.transform.localPosition + new Vector3 (0, -100);
								collectClear.goTotalScore.transform.localPosition = collectClear.goTotal.transform.localPosition;
								collectClear.goNextStage.transform.localPosition = collectClear.goTotal.transform.localPosition + new Vector3 (0, -180);
								collectClear.goNextStage.GetComponent<Text> ().text = groupEnemyList.Exists (obj => obj.isBoss) ? "GO TO THE PRINCESS" : "NEXT STAGE";

								Color color = Color.white;
								color.a = 0;
								collectClear.colorEnemy = color;
								collectClear.colorEnemyScore = color;
								collectClear.colorTime = color;
								collectClear.colorTimeScore = color;
								collectClear.colorBonus = color;
								collectClear.colorRule = color;
								collectClear.colorTotal = color;
								collectClear.colorTotalScore = color;
								collectClear.colorNextStage = color;
								foreach (Bonus.Type type in Enum.GetValues (typeof(Bonus.Type))) {
									if (type == Bonus.Type.None)
										continue;
									collectClear.colorBonusScoreList [(int)type] = color;
								}
								collectClear.player.Init (-700, -700, Data.SPEED_16);

								PlayerPrefs.SetInt (Data.RECORD_CLEAR + MainManager.Instance.stage, 1);
								PlayerPrefs.SetFloat (Data.RECORD_CLEAR_TIME + MainManager.Instance.stage, Mathf.Max (remainingTime.now, PlayerPrefs.GetInt (Data.RECORD_CLEAR_TIME + MainManager.Instance.stage)));
							} else if (time <= 1) {
							} else {
								float aimTime = 2;
								float value = 1 / (aimTime - 1) * time - 1;
								value = Mathf.Min (value, 1);
								collectClear.positionTitle = new Vector3 (0, -500 * value * (value - 2));
								collectClear.color = Color.black;
								collectClear.color.a = Mathf.Lerp (0, collectClear.colorAlphaMax, value);
								if (time >= aimTime) {
									pattern = 1;
									time = 0;
									goStageClearTwitterButton.SetActive (true);
								}
							}
						}
						if (pattern == 1) {
							Color color = Color.white;
							color.a = Mathf.Lerp (0, 1, 2 * time);
							collectClear.colorEnemy = color;
							collectClear.colorEnemyScore = color;
							if (time >= 0.5f) {
								pattern = 2;
								time = 0;
							}
						}
						if (pattern == 2) {
							Color color = Color.white;
							color.a = Mathf.Lerp (0, 1, 2 * time);
							collectClear.colorTime = color;
							collectClear.colorTimeScore = color;
							if (time >= 0.5f) {
								pattern = 3;
								time = 0;
							}
						}
						if (pattern == 3) {
							if (remainingTime.now == 0) {
								pattern = 4;
								time = 0;
								collectClear.bonusIndex = (int)Bonus.Type.Bonus0;
							} else {
								remainingTime.now--;
								score.timebonus += (int)(Data.FROM_TIME_TO_SCORE_COEFFICIENT * (0.5f * (MainManager.Instance.stage + 1) + 1));
								int index = Data.GetLifeupIndex (score.now);
								if (lifeupIndex < index) {
									lifeupIndex++;
									life.now++;
									SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_1UP);
								}
							}
						}
						if (pattern == 4) {
							if (time == 0) {
								while (true) {
									if (collectClear.bonusIndex > (int)Bonus.Type.Bonus6) {
										pattern = 5;
										break;
									}
									if (score.bonusList [collectClear.bonusIndex] == 0) {
										collectClear.bonusIndex++;
										continue;
									}
									break;
								}
							}
							if (collectClear.bonusIndex <= (int)Bonus.Type.Bonus6) {
								Color color = Color.white;
								color.a = Mathf.Lerp (0, 1, 2 * time);
								if (collectClear.colorBonus.a < color.a)
									collectClear.colorBonus = color;
								collectClear.colorBonusScoreList [collectClear.bonusIndex] = color;
								if (time >= 0.5f) {
									time = 0;
									collectClear.bonusIndex++;
									loop2 = true;
								}
							}
						}
						if (pattern == 5) {
							Color color = Color.white;
							color.a = Mathf.Lerp (0, 1, 2 * time);
							collectClear.colorRule = color;
							collectClear.colorTotal = color;
							collectClear.colorTotalScore = color;
							if (time >= 0.5f) {
								pattern = 10;
								time = 0;
							}
						}
						if (pattern == 10) {
							if (time == 0) {
								while (remainingTime.now > 0) {
									remainingTime.now--;
									score.timebonus += (int)(Data.FROM_TIME_TO_SCORE_COEFFICIENT * (0.5f * (MainManager.Instance.stage + 1) + 1));
									int index = Data.GetLifeupIndex (score.now);
									if (lifeupIndex < index) {
										lifeupIndex++;
										life.now++;
										SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_1UP);
									}
								}

								Color color = Color.white;
								collectClear.colorEnemy = color;
								collectClear.colorEnemyScore = color;
								collectClear.colorTime = color;
								collectClear.colorTimeScore = color;
								collectClear.colorBonus = color;
								collectClear.colorRule = color;
								collectClear.colorTotal = color;
								collectClear.colorTotalScore = color;
								foreach (Bonus.Type type in Enum.GetValues (typeof(Bonus.Type))) {
									if (type == Bonus.Type.None)
										continue;
									collectClear.colorBonusScoreList [(int)type] = color;
								}
								goStageClearTwitterButton.SetActive (true);
							}
							if (time >= 1) {
								pattern = 11;
								time = 0;
							}
						}
						if (pattern == 11) {
							if (time == 0) {
								player.Vanish ();
							}
							Color color = Color.white;
							color.a = ((int)(time * 2) % 5 == 4) ? 0 : 1;
							collectClear.colorNextStage = color;

							if (collectClear.player.positionX < 700)
								collectClear.player.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
						}
					} while (loop2);
				}
				break;
			case State.Continue:
				{
					if (time == 0) {
						collectContinue.go.SetActive (true);
						collectContinue.goStage.GetComponent<Text> ().text = string.Format ("{0}", MainManager.Instance.stage + 1);
						collectContinue.goScore.GetComponent<Text> ().text = score.now.ToString ();
						continueCommand = CONTINUE_COMMAND_NONE;
						SoundManager.Instance.StopBgm ();
						SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_GAMEOVER);
					}

					switch (continueCommand) {
					case CONTINUE_COMMAND_YES:
						{
							MainManager.Instance.CurrentStage (MainManager.CONTINUE_LIFE, 0);

							/*
							collectContinue.go.SetActive (false);
							state = State.ReadyContinue;
							time = 0;
							pattern = 0;
							loop = true;

							player.Rise ();
							player.SetBlind (true, Color.white);
							groupPlayer.invincibleTime = 5f;
							remainingTime.now = Data.GetStageData (MainManager.Instance.stage).limitTime;
							*/
						}
						break;
					case CONTINUE_COMMAND_NO:
						{
							MainManager.Instance.Title ();
						}
						break;
					}
				}
				break;
			case State.TutorialEnd:
				{
					if (pattern == 0) {
						if (time == 0) {
							player.Walk (Player.Compass.Top, true);
							player.SetSpeed (Data.SPEED_3);
							player.SetImageCoefficient (Data.GetPlayerImageCoefficient (Hakaima.Terrain.Type.Soil));
						}
						if (player.state == Player.State.Wait) {
							pattern = 1;
							time = 0;
						}
					}
					if (pattern == 1) {
						if (time == 0) {
							goCover.SetActive (true);
						}
						Color color = Color.black;
						color.a = Mathf.Lerp (0, 0.9f, 1f / 0.6f * time);
						goCover.GetComponent<Image> ().color = color;
						if (color.a >= 0.9f) {
							state = State.TutorialHelp;
							time = 0;
							pattern = 0;
							loop = true;
						}
					}

					player.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
					chipList.ForEach (obj => obj.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE));
				}
				break;
			case State.TutorialHelp:
				{
					if (pattern == 0) {
						if (time == 0) {
							string path = Application.systemLanguage == SystemLanguage.Japanese ? Data.HELP_PATH_JAPANESE : Data.HELP_PATH_ENGLISH;

							help = new TitleManager.Catalog ();
							help.Init (TitleManager.HELP_PAGE_NUM);
							goHelp = Instantiate (Resources.Load<GameObject> (path));
							goHelp.transform.SetParent (transform.Find ("UI"));
							goHelp.transform.Find ("Attention").GetComponent<Text> ().text = Language.sentence [Language.GAME_HELP];
							goHelp.transform.Find ("ButtonBack").GetComponent<Button> ().onClick.AddListener (() => {
								pattern = 1;
								time = 0;
								SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
							});
							goHelp.transform.Find ("ButtonBack/Image/Text").GetComponent<Text> ().text = "PLAY START";
							goHelp.transform.Find ("ButtonBack/Image/Text").GetComponent<Text> ().fontSize = 45;
							goHelp.transform.Find ("ButtonBack/Image/Text").GetComponent<Text> ().color = Color.black;
							goHelpPage = goHelp.transform.Find ("Page").gameObject;
							goHelpPoint = goHelp.transform.Find ("Point").gameObject;
							goHelpArrowRight = goHelp.transform.Find ("ArrowRight").gameObject;
							goHelpArrowLeft = goHelp.transform.Find ("ArrowLeft").gameObject;
							goHelpArrowRight.GetComponent<Button> ().onClick.AddListener (() => OnHelpNextPage ());
							goHelpArrowLeft.GetComponent<Button> ().onClick.AddListener (() => OnHelpPrevPage ());

							Destroy (goHelp.transform.Find ("Com").gameObject);
							Destroy (goHelp.transform.Find ("Logo").gameObject);
							Destroy (goHelp.transform.Find ("Swipe").gameObject);

							if (PlayerPrefs.GetInt (Data.RECORD_IS_TUTORIAL_FIRST_HELP) == 1) {
								pattern = 1;
								time = 0;
							}

						}
						help.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
					}
					if (pattern == 1) {
						if (time == 0) {
							goHelp.SetActive (false);
						}
						Color color = Color.black;
						color.a = Mathf.Lerp (0.9f, 1, 1f / 0.4f * time);
						goCover.GetComponent<Image> ().color = color;
						if (color.a >= 1f) {
							PlayerPrefs.SetInt (Data.RECORD_IS_TUTORIAL_FIRST_HELP, 1);
							MainManager.Instance.NextStage (life.now, myWeapon.now);
							SetClearNextStage ();
						}
					}
				}
				break;
			}
		} while (loop);

		time += Data.DELTA_TIME;
	}



	private void Draw ()
	{
		{
			if (groupPlayer.gameObject.activeSelf != player.visible) {
				groupPlayer.gameObject.SetActive (player.visible);
			}
			if (player.visible) {
				if (groupPlayer.gameObject.transform.parent != goLayerList [player.layer].transform) {
					groupPlayer.gameObject.transform.SetParent (goLayerList [player.layer].transform);
				}
				if (groupPlayer.gameObject.transform.localPosition.x != player.positionX || groupPlayer.gameObject.transform.localPosition.y != player.positionY) {
					groupPlayer.gameObject.transform.localPosition = new Vector3 (player.positionX, player.positionY);
				}
				if (groupPlayer.gameObject.GetComponent<Image> ().color != player.color) {
					groupPlayer.gameObject.GetComponent<Image> ().color = player.color;
				}
				if (groupPlayer.gameObject.GetComponent<Image> ().sprite != ResourceManager.Instance.spritePlayerList [Convert.ToInt32 (player.spin) * ResourceManager.SPRITE_MULTI_TYPE + (int)player.compass * ResourceManager.SPRITE_MULTI_COMPASS + player.imageIndex]) {
					groupPlayer.gameObject.GetComponent<Image> ().sprite = ResourceManager.Instance.spritePlayerList [Convert.ToInt32 (player.spin) * ResourceManager.SPRITE_MULTI_TYPE + (int)player.compass * ResourceManager.SPRITE_MULTI_COMPASS + player.imageIndex];
				}
				if (groupPlayer.fallTime > 0) {
					if (groupPlayer.gameObject.GetComponent<Image> ().material != ResourceManager.Instance.materialReversalFall) {
						groupPlayer.gameObject.GetComponent<Image> ().material = ResourceManager.Instance.materialReversalFall;
					}
				} else if (groupPlayer.bathTime > 0) {
					if (groupPlayer.gameObject.GetComponent<Image> ().material != ResourceManager.Instance.materialReversalHide) {
						groupPlayer.gameObject.GetComponent<Image> ().material = ResourceManager.Instance.materialReversalHide;
					}
				} else if (groupPlayer.hideTime > 0) {
					if (groupPlayer.gameObject.GetComponent<Image> ().material != ResourceManager.Instance.materialReversalHide) {
						groupPlayer.gameObject.GetComponent<Image> ().material = ResourceManager.Instance.materialReversalHide;
					}
				} else {
					if (groupPlayer.gameObject.GetComponent<Image> ().material != ResourceManager.Instance.materialReversal) {
						groupPlayer.gameObject.GetComponent<Image> ().material = ResourceManager.Instance.materialReversal;
					}
				}
				if (groupPlayer.surpriseTime > 0) {
					if (!groupPlayer.gameObjectSurprise.activeSelf) {
						groupPlayer.gameObjectSurprise.SetActive (true);
					}
					if (groupPlayer.gameObjectSurprise.transform.localPosition.x != player.positionX + player.size / 2 || groupPlayer.gameObjectSurprise.transform.localPosition.y != player.positionY + player.size / 2 + 80) {
						groupPlayer.gameObjectSurprise.transform.localPosition = new Vector3 (player.positionX + player.size / 2, player.positionY + player.size / 2 + 80);
					}
				} else {
					if (groupPlayer.gameObjectSurprise.activeSelf) {
						groupPlayer.gameObjectSurprise.SetActive (false);
					}
				}
				if (groupPlayer.gameObjectLifeup.activeSelf != groupPlayer.lifeup.visible) {
					groupPlayer.gameObjectLifeup.SetActive (groupPlayer.lifeup.visible);
				}
				if (groupPlayer.lifeup.visible) {
					if (groupPlayer.gameObjectLifeup.transform.localPosition.x != groupPlayer.lifeup.positionX || groupPlayer.gameObjectLifeup.transform.localPosition.y != groupPlayer.lifeup.positionY) {
						groupPlayer.gameObjectLifeup.transform.localPosition = new Vector3 (groupPlayer.lifeup.positionX, groupPlayer.lifeup.positionY);
					}
				}
				if (groupPlayer.gameObjectParasol.activeSelf != groupPlayer.isParasol) {
					groupPlayer.gameObjectParasol.SetActive (groupPlayer.isParasol);
				}
				if (groupPlayer.gameObjectSweat.activeSelf != groupPlayer.isSweat) {
					groupPlayer.gameObjectSweat.SetActive (groupPlayer.isSweat);
				}
			}
		}
		{
			for (int i = 0; i < enemyList.Count; i++) {
				Enemy enemy = enemyList [i];
				GroupEnemy groupEnemy = groupEnemyList [i];
				if (groupEnemy.gameObject.activeSelf != enemy.visible) {
					groupEnemy.gameObject.SetActive (enemy.visible);
				}
				if (enemy.visible) {
					if (groupEnemy.gameObject.transform.parent != goLayerList [enemy.layer].transform) {
						groupEnemy.gameObject.transform.SetParent (goLayerList [enemy.layer].transform);
					}
					if (groupEnemy.gameObject.transform.localPosition.x != enemy.positionX || groupEnemy.gameObject.transform.localPosition.y != enemy.positionY) {
						groupEnemy.gameObject.transform.localPosition = new Vector3 (enemy.positionX, enemy.positionY);
					}
					if (groupEnemy.gameObject.GetComponent<Image> ().color != enemy.color) {
						groupEnemy.gameObject.GetComponent<Image> ().color = enemy.color;
					}
					if (groupEnemy.gameObject.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteEnemyList [(int)enemy.type * ResourceManager.SPRITE_MULTI_TYPE + (int)enemy.compass * ResourceManager.SPRITE_MULTI_COMPASS + enemy.imageIndex]) {
						groupEnemy.gameObject.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteEnemyList [(int)enemy.type * ResourceManager.SPRITE_MULTI_TYPE + (int)enemy.compass * ResourceManager.SPRITE_MULTI_COMPASS + enemy.imageIndex];
					}
					if (groupEnemy.fallTime > 0) {
						if (groupEnemy.gameObject.GetComponent<Image> ().material != ResourceManager.Instance.materialReversalFall) {
							groupEnemy.gameObject.GetComponent<Image> ().material = ResourceManager.Instance.materialReversalFall;
						}
					} else if (groupEnemy.bathTime > 0) {
						if (groupEnemy.gameObject.GetComponent<Image> ().material != ResourceManager.Instance.materialReversalHide) {
							groupEnemy.gameObject.GetComponent<Image> ().material = ResourceManager.Instance.materialReversalHide;
						}
					} else {
						if (groupEnemy.gameObject.GetComponent<Image> ().material != ResourceManager.Instance.materialReversal) {
							groupEnemy.gameObject.GetComponent<Image> ().material = ResourceManager.Instance.materialReversal;
						}
					}
					if (groupEnemy.noticeTime > 0) {
						if (!groupEnemy.gameObjectNotice.activeSelf) {
							groupEnemy.gameObjectNotice.SetActive (true);
						}
					} else {
						if (groupEnemy.gameObjectNotice.activeSelf) {
							groupEnemy.gameObjectNotice.SetActive (false);
						}
					}
					if (groupEnemy.lostTime > 0) {
						if (!groupEnemy.gameObjectLost.activeSelf) {
							groupEnemy.gameObjectLost.SetActive (true);
						}
					} else {
						if (groupEnemy.gameObjectLost.activeSelf) {
							groupEnemy.gameObjectLost.SetActive (false);
						}
					}
				}
			}
		}
		{
			bool isObstacleSibling = false;
			for (int i = 0; i < chipList.Count; i++) {
				Chip chip = chipList [i];
				GroupChip groupChip = groupChipList [i];
				if (groupChip.gameObjectTerrain.activeSelf != chip.terrain.visible) {
					groupChip.gameObjectTerrain.SetActive (chip.terrain.visible);
				}
				if (chip.terrain.visible) {
					if (groupChip.gameObjectTerrain.transform.parent != goLayerList [chip.terrain.layer].transform) {
						groupChip.gameObjectTerrain.transform.SetParent (goLayerList [chip.terrain.layer].transform);
					}
					if (groupChip.gameObjectTerrain.transform.localPosition.x != chip.positionX || groupChip.gameObjectTerrain.transform.localPosition.y != chip.positionY) {
						groupChip.gameObjectTerrain.transform.localPosition = new Vector3 (chip.positionX, chip.positionY);
					}
					if (groupChip.gameObjectTerrain.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteTerrainList [(int)chip.terrain.type * ResourceManager.SPRITE_MULTI_TYPE + chip.terrain.imageIndex]) {
						groupChip.gameObjectTerrain.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteTerrainList [(int)chip.terrain.type * ResourceManager.SPRITE_MULTI_TYPE + chip.terrain.imageIndex];
					}
				}
				if (groupChip.gameObjectHole.activeSelf != chip.hole.visible) {
					groupChip.gameObjectHole.SetActive (chip.hole.visible);
				}
				if (chip.hole.visible) {
					if (groupChip.gameObjectHole.transform.parent != goLayerList [chip.hole.layer].transform) {
						groupChip.gameObjectHole.transform.SetParent (goLayerList [chip.hole.layer].transform);
					}
					if (groupChip.gameObjectHole.transform.localPosition.x != chip.positionX || groupChip.gameObjectHole.transform.localPosition.y != chip.positionY) {
						groupChip.gameObjectHole.transform.localPosition = new Vector3 (chip.positionX, chip.positionY);
					}
					if (groupChip.gameObjectHole.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteHoleList [chip.hole.imageIndex]) {
						groupChip.gameObjectHole.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteHoleList [chip.hole.imageIndex];
					}
				}
				for (int j = 0; j < chip.obstacleList.Count; j++) {
					if (groupChip.gameObjectObstacleList [j].activeSelf != chip.obstacleList [j].visible) {
						groupChip.gameObjectObstacleList [j].SetActive (chip.obstacleList [j].visible);
					}
					if (chip.obstacleList [j].visible) {
						if (groupChip.gameObjectObstacleList [j].transform.parent != goLayerList [chip.obstacleList [j].layer].transform) {
							groupChip.gameObjectObstacleList [j].transform.SetParent (goLayerList [chip.obstacleList [j].layer].transform);
							isObstacleSibling = true;
						}
						if (groupChip.gameObjectObstacleList [j].transform.localPosition.x != chip.positionX || groupChip.gameObjectObstacleList [j].transform.localPosition.y != chip.positionY) {
							groupChip.gameObjectObstacleList [j].transform.localPosition = new Vector3 (chip.positionX, chip.positionY);
						}
						if (groupChip.gameObjectObstacleList [j].GetComponent<Image> ().sprite != ResourceManager.Instance.spriteObstacleList [(int)chip.obstacleList [j].type * ResourceManager.SPRITE_MULTI_TYPE + chip.obstacleList [j].imageIndex]) {
							groupChip.gameObjectObstacleList [j].GetComponent<Image> ().sprite = ResourceManager.Instance.spriteObstacleList [(int)chip.obstacleList [j].type * ResourceManager.SPRITE_MULTI_TYPE + chip.obstacleList [j].imageIndex];
							groupChip.gameObjectObstacleList [j].GetComponent<Image> ().SetNativeSize ();
						}
					}
				}
			}
			if (isObstacleSibling) {
				for (int i = 0; i < chipList.Count; i++) {
					for (int j = 0; j < chipList [i].obstacleList.Count; j++) {
						groupChipList [i].gameObjectObstacleList [chipList [i].obstacleList.Count - 1 - j].transform.SetAsFirstSibling ();
					}
				}
			}
		}
		{
			for (int i = 0; i < playerWeaponList.Count; i++) {
				Weapon weapon = playerWeaponList [i];
				GroupWeapon groupWeapon = groupPlayerWeaponList [i];
				if (groupWeapon.gameObject.activeSelf != weapon.visible) {
					groupWeapon.gameObject.SetActive (weapon.visible);
				}
				if (weapon.visible) {
					if (groupWeapon.gameObject.transform.parent != goLayerList [weapon.layer].transform) {
						groupWeapon.gameObject.transform.SetParent (goLayerList [weapon.layer].transform);
					}
					if (groupWeapon.gameObject.transform.localPosition.x != weapon.positionX || groupWeapon.gameObject.transform.localPosition.y != weapon.positionY) {
						groupWeapon.gameObject.transform.localPosition = new Vector3 (weapon.positionX, weapon.positionY);
					}
					if (groupWeapon.gameObjectImage.transform.localScale.x != weapon.scaleX || groupWeapon.gameObjectImage.transform.localScale.y != weapon.scaleY) {
						groupWeapon.gameObjectImage.transform.localScale = new Vector3 (weapon.scaleX, weapon.scaleY);
					}
					if (groupWeapon.gameObjectImage.transform.localRotation.z != weapon.rotation) {
						groupWeapon.gameObjectImage.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, weapon.rotation));
					}
					if (groupWeapon.gameObjectImage.GetComponent<Image> ().sprite != ResourceManager.Instance.spritePlayerWeapon) {
						groupWeapon.gameObjectImage.GetComponent<Image> ().sprite = ResourceManager.Instance.spritePlayerWeapon;
					}
				}
			}
		}
		{
			for (int i = 0; i < enemyWeaponList.Count; i++) {
				Weapon weapon = enemyWeaponList [i];
				GroupWeapon groupWeapon = groupEnemyWeaponList [i];
				if (groupWeapon.gameObject.activeSelf != weapon.visible) {
					groupWeapon.gameObject.SetActive (weapon.visible);
				}
				if (weapon.visible) {
					if (groupWeapon.gameObject.transform.parent != goLayerList [weapon.layer].transform) {
						groupWeapon.gameObject.transform.SetParent (goLayerList [weapon.layer].transform);
					}
					if (groupWeapon.gameObject.transform.localPosition.x != weapon.positionX || groupWeapon.gameObject.transform.localPosition.y != weapon.positionY) {
						groupWeapon.gameObject.transform.localPosition = new Vector3 (weapon.positionX, weapon.positionY);
					}
					if (groupWeapon.gameObjectImage.transform.localScale.x != weapon.scaleX || groupWeapon.gameObjectImage.transform.localScale.y != weapon.scaleY) {
						groupWeapon.gameObjectImage.transform.localScale = new Vector3 (weapon.scaleX, weapon.scaleY);
					}
					if (groupWeapon.gameObjectImage.transform.localRotation.z != weapon.rotation) {
						groupWeapon.gameObjectImage.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, weapon.rotation));
					}
					if (groupWeapon.gameObjectImage.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteEnemyWeapon) {
						groupWeapon.gameObjectImage.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteEnemyWeapon;
					}
				}
			}
		}
		{
			for (int i = 0; i < itemList.Count; i++) {
				Item item = itemList [i];
				GroupItem groupItem = groupItemList [i];
				if (groupItem.gameObject.activeSelf != item.visible) {
					groupItem.gameObject.SetActive (item.visible);
				}
				if (item.visible) {
					if (groupItem.gameObject.transform.parent != goLayerList [item.layer].transform) {
						groupItem.gameObject.transform.SetParent (goLayerList [item.layer].transform);
					}
					if (groupItem.gameObject.transform.localPosition.x != item.positionX || groupItem.gameObject.transform.localPosition.y != item.positionY) {
						groupItem.gameObject.transform.localPosition = new Vector3 (item.positionX, item.positionY);
					}
					if (groupItem.gameObject.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteItemList [(int)item.type * ResourceManager.SPRITE_MULTI_TYPE]) {
						groupItem.gameObject.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteItemList [(int)item.type * ResourceManager.SPRITE_MULTI_TYPE];
					}
				}
			}
		}
		{
			for (int i = 0; i < bonusList.Count; i++) {
				Bonus bonus = bonusList [i];
				GroupBonus groupBonus = groupBonusList [i];
				if (groupBonus.gameObject.activeSelf != bonus.visible) {
					groupBonus.gameObject.SetActive (bonus.visible);
				}
				if (bonus.visible) {
					if (groupBonus.gameObject.transform.parent != goLayerList [bonus.layer].transform) {
						groupBonus.gameObject.transform.SetParent (goLayerList [bonus.layer].transform);
					}
					if (groupBonus.gameObject.transform.localPosition.x != bonus.positionX || groupBonus.gameObject.transform.localPosition.y != bonus.positionY) {
						groupBonus.gameObject.transform.localPosition = new Vector3 (bonus.positionX, bonus.positionY);
					}
					if (groupBonus.gameObject.GetComponent<Image> ().color != bonus.color) {
						groupBonus.gameObject.GetComponent<Image> ().color = bonus.color;
					}
					if (groupBonus.gameObject.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteBonusList [(int)bonus.type * ResourceManager.SPRITE_MULTI_TYPE]) {
						groupBonus.gameObject.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteBonusList [(int)bonus.type * ResourceManager.SPRITE_MULTI_TYPE];
					}
				}
			}
		}
		{
			for (int i = 0; i < lightList.Count; i++) {
				Hakaima.Light light = lightList [i];
				GroupLight groupLight = groupLightList [i];
				if (groupLight.gameObject.activeSelf != light.visible) {
					groupLight.gameObject.SetActive (light.visible);
				}
				if (light.visible) {
					if (groupLight.gameObject.transform.parent != goLayerList [light.layer].transform) {
						groupLight.gameObject.transform.SetParent (goLayerList [light.layer].transform);
					}
					if (groupLight.gameObject.transform.localPosition.x != light.positionX || groupLight.gameObject.transform.localPosition.y != light.positionY) {
						groupLight.gameObject.transform.localPosition = new Vector3 (light.positionX, light.positionY);
					}
				}
			}
		}
		{
			for (int i = 0; i < specialItemList.Count; i++) {
				Item item = specialItemList [i];
				GroupItem groupItem = groupSpecialItemList [i];
				if (groupItem.gameObject.activeSelf != item.visible) {
					groupItem.gameObject.SetActive (item.visible);
				}
				if (item.visible) {
					if (groupItem.gameObject.transform.parent != goLayerList [item.layer].transform) {
						groupItem.gameObject.transform.SetParent (goLayerList [item.layer].transform);
					}
					if (groupItem.gameObject.transform.localPosition.x != item.positionX || groupItem.gameObject.transform.localPosition.y != item.positionY) {
						groupItem.gameObject.transform.localPosition = new Vector3 (item.positionX, item.positionY);
					}
					if (groupItem.gameObject.GetComponent<Image> ().sprite != ResourceManager.Instance.spritePlayerWeapon) {
						groupItem.gameObject.GetComponent<Image> ().sprite = ResourceManager.Instance.spritePlayerWeapon;
					}
				}
			}
		}
		{
			for (int i = 0; i < numberList.Count; i++) {
				Number number = numberList [i];
				GroupNumber groupNumber = groupNumberList [i];
				if (groupNumber.gameObject.activeSelf != number.visible) {
					groupNumber.gameObject.SetActive (number.visible);
				}
				if (number.visible) {
					if (groupNumber.gameObject.transform.localPosition.x != number.positionX || groupNumber.gameObject.transform.localPosition.y != number.positionY) {
						groupNumber.gameObject.transform.localPosition = new Vector3 (number.positionX, number.positionY);
					}
					if (groupNumber.gameObjectText.GetComponent<Text> ().color != number.color) {
						groupNumber.gameObjectText.GetComponent<Text> ().color = number.color;
					}
					if (groupNumber.gameObjectText.GetComponent<Text> ().text != number.value.ToString ()) {
						groupNumber.gameObjectText.GetComponent<Text> ().text = number.value.ToString ();
					}
				}
			}
		}
		{
			for (int i = 0; i < ownerItemList.Count; i++) {
				OwnerItem ownerItem = ownerItemList [i];
				GameObject goOwnerItem = goOwnerItemList [i];
				GameObject goOwnerItemFrame = goOwnerItemFrameList [i];
				switch (ownerItem.state) {
				case OwnerItem.State.NoHave:
					{
						if (goOwnerItem.activeSelf) {
							goOwnerItem.SetActive (false);
						}
						if (goOwnerItemFrame.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteUpperItemFrame) {
							goOwnerItemFrame.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteUpperItemFrame;
						}
					}
					break;
				case OwnerItem.State.Have:
				case OwnerItem.State.Use:
					{
						if (!goOwnerItem.activeSelf) {
							goOwnerItem.SetActive (true);
						}
						if (goOwnerItem.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteItemList [(int)ownerItem.type * ResourceManager.SPRITE_MULTI_TYPE]) {
							goOwnerItem.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteItemList [(int)ownerItem.type * ResourceManager.SPRITE_MULTI_TYPE];
						}
						switch (ownerItem.state) {
						case OwnerItem.State.Have:
							if (goOwnerItemFrame.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteUpperItemFrame) {
								goOwnerItemFrame.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteUpperItemFrame;
							}
							break;
						case OwnerItem.State.Use:
							if (goOwnerItemFrame.GetComponent<Image> ().sprite != ResourceManager.Instance.spriteUpperItemFrameSelect) {
								goOwnerItemFrame.GetComponent<Image> ().sprite = ResourceManager.Instance.spriteUpperItemFrameSelect;
							}
							break;
						}
					}
					break;
				}
			}
		}
		{
			if (score.now != score.pre) {
				goScore.GetComponent<Text> ().text = score.now.ToString ();
				score.pre = score.now;
			}
			if (score.high != score.preHigh) {
				goScoreHigh.GetComponent<Text> ().text = score.high.ToString ();
				score.preHigh = score.high;
			}
			if (life.now != life.pre) {
				goLife.GetComponent<Text> ().text = string.Format ("{0:00}", life.now).ToString ();
				life.pre = life.now;
			}
			if (myWeapon.now != myWeapon.pre) {
				goMyWeapon.GetComponent<Text> ().text = string.Format ("{0:00}", myWeapon.now).ToString ();
				myWeapon.pre = myWeapon.now;
			}
			if (remainingTime.now != remainingTime.pre) {
				TimeSpan span = TimeSpan.FromSeconds (remainingTime.now);
				goRemainingTime.GetComponent<Text> ().text = string.Format ("{0:00}:{1:00}:{2}", span.Minutes, span.Seconds, span.Milliseconds.ToString ("000").Substring (0, 2));
				remainingTime.pre = remainingTime.now;
			}
		}


		switch (state) {
		case State.Ready:
		case State.ReadyContinue:
			{
				if (collectReady.go.activeSelf) {
					if (collectReady.go.GetComponent<Image> ().color != collectReady.color) {
						collectReady.go.GetComponent<Image> ().color = collectReady.color;
					}
					bool active = collectReady.color.a >= 0.5f;
					if (collectReady.goStage.GetComponent<Text> ().enabled != active) {
						collectReady.goStage.GetComponent<Text> ().enabled = active;
					}
					if (collectReady.goDescription.GetComponent<Text> ().enabled != active) {
						collectReady.goDescription.GetComponent<Text> ().enabled = active;
					}
					if (collectReady.goClear.GetComponent<Text> ().enabled != active) {
						collectReady.goClear.GetComponent<Text> ().enabled = active;
					}
					if (collectReady.goReady.GetComponent<Text> ().enabled != active) {
						collectReady.goReady.GetComponent<Text> ().enabled = active;
					}
					if (collectReady.goGo.GetComponent<Text> ().enabled != active) {
						collectReady.goGo.GetComponent<Text> ().enabled = active;
					}
				}
			}
			break;
		case State.Clear:
			{
				if (collectClear.go.activeSelf) {
					if (collectClear.go.GetComponent<Image> ().color != collectClear.color) {
						collectClear.go.GetComponent<Image> ().color = collectClear.color;
					}
					if (collectClear.goTitle.transform.localPosition != collectClear.positionTitle) {
						collectClear.goTitle.transform.localPosition = collectClear.positionTitle;
					}
					if (collectClear.goEnemy.GetComponent<Text> ().color != collectClear.colorEnemy) {
						collectClear.goEnemy.GetComponent<Text> ().color = collectClear.colorEnemy;
					}
					if (collectClear.goEnemyScore.GetComponent<Text> ().color != collectClear.colorEnemyScore) {
						collectClear.goEnemyScore.GetComponent<Text> ().color = collectClear.colorEnemyScore;
					}
					if (collectClear.goEnemyScore.GetComponent<Text> ().text != score.enemy.ToString ()) {
						collectClear.goEnemyScore.GetComponent<Text> ().text = score.enemy.ToString ();
					}
					if (collectClear.goTime.GetComponent<Text> ().color != collectClear.colorTime) {
						collectClear.goTime.GetComponent<Text> ().color = collectClear.colorTime;
					}
					if (collectClear.goTimeScore.GetComponent<Text> ().color != collectClear.colorTimeScore) {
						collectClear.goTimeScore.GetComponent<Text> ().color = collectClear.colorTimeScore;
					}
					if (collectClear.goTimeScore.GetComponent<Text> ().text != score.timebonus.ToString ()) {
						collectClear.goTimeScore.GetComponent<Text> ().text = score.timebonus.ToString ();
					}
					if (collectClear.goBonus.GetComponent<Text> ().color != collectClear.colorBonus) {
						collectClear.goBonus.GetComponent<Text> ().color = collectClear.colorBonus;
					}
					if (collectClear.goRule.GetComponent<Image> ().color != collectClear.colorRule) {
						collectClear.goRule.GetComponent<Image> ().color = collectClear.colorRule;
					}
					if (collectClear.goTotal.GetComponent<Text> ().color != collectClear.colorTotal) {
						collectClear.goTotal.GetComponent<Text> ().color = collectClear.colorTotal;
					}
					if (collectClear.goTotalScore.GetComponent<Text> ().color != collectClear.colorTotalScore) {
						collectClear.goTotalScore.GetComponent<Text> ().color = collectClear.colorTotalScore;
					}
					if (collectClear.goTotalScore.GetComponent<Text> ().text != score.stage.ToString ()) {
						collectClear.goTotalScore.GetComponent<Text> ().text = score.stage.ToString ();
					}
					if (collectClear.goNextStage.GetComponent<Text> ().color != collectClear.colorNextStage) {
						collectClear.goNextStage.GetComponent<Text> ().color = collectClear.colorNextStage;
					}
					foreach (Bonus.Type type in Enum.GetValues (typeof(Bonus.Type))) {
						if (type == Bonus.Type.None)
							continue;
						if (collectClear.goBonusScoreList [(int)type].GetComponent<Text> ().color != collectClear.colorBonusScoreList [(int)type]) {
							collectClear.goBonusScoreList [(int)type].GetComponent<Text> ().color = collectClear.colorBonusScoreList [(int)type];
							collectClear.goBonusScoreList [(int)type].transform.Find ("Image").GetComponent<Image> ().color = collectClear.colorBonusScoreList [(int)type];
						}
						if (collectClear.goBonusScoreList [(int)type].GetComponent<Text> ().text != score.bonusList [(int)type].ToString ()) {
							collectClear.goBonusScoreList [(int)type].GetComponent<Text> ().text = score.bonusList [(int)type].ToString ();
						}
					}
					if (collectClear.goPlayer.transform.localPosition.x != collectClear.player.positionX || collectClear.goPlayer.transform.localPosition.y != collectClear.player.positionY) {
						collectClear.goPlayer.transform.localPosition = new Vector3 (collectClear.player.positionX, collectClear.player.positionY);
					}
					if (collectClear.goPlayer.transform.GetComponent<Image> ().sprite != ResourceManager.Instance.spritePlayerList [(int)collectClear.player.compass * ResourceManager.SPRITE_MULTI_COMPASS + collectClear.player.imageIndex]) {
						collectClear.goPlayer.transform.GetComponent<Image> ().sprite = ResourceManager.Instance.spritePlayerList [(int)collectClear.player.compass * ResourceManager.SPRITE_MULTI_COMPASS + collectClear.player.imageIndex];
					}
				}
			}
			break;
		case State.TutorialHelp:
			{
				if (goHelpPage.transform.localPosition.x != help.positionX) {
					goHelpPage.transform.localPosition = new Vector3 (help.positionX, goHelpPage.transform.localPosition.y);
				}
				if (goHelpArrowRight.activeSelf != help.isArrowRight) {
					goHelpArrowRight.SetActive (help.isArrowRight);
				}
				if (goHelpArrowLeft.activeSelf != help.isArrowLeft) {
					goHelpArrowLeft.SetActive (help.isArrowLeft);
				}
				goHelpPoint.transform.Find ("PointNow").localPosition = goHelpPoint.transform.Find ("Point" + help.nowPageIndex).localPosition;
			}
			break;
		}
	}



	private void SetWeapon ()
	{
		GameObject goWeapon = transform.Find ("UI/Information/ThrowWepon/Wepon").gameObject;

		if (MainManager.Instance.IsWeaponCharacter (MainManager.Instance.selectCharacter)) {
			goWeapon.transform.parent.gameObject.SetActive (true);
			goWeapon.GetComponent<Image> ().sprite = ResourceManager.Instance.spritePlayerWeapon;
		} else {
			goWeapon.transform.parent.gameObject.SetActive (false);
		}

		goMyWeapon.GetComponent<Text> ().text = string.Format ("{0:00}", myWeapon.now).ToString ();
	}



	private void OnPause (bool isPause)
	{
		if (state == State.Play) {
			this.isPause = isPause;
			collectPause.go.SetActive (isPause);
			if (this.isPause) {
				MainManager.Instance.bannerView.Show ();
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
			} else {
				MainManager.Instance.bannerView.Hide ();
				collectPause.goHowTo.SetActive (isPause);
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_CANCEL);
			}
		}
	}



	private void OnHowTo(bool isHowTo)
	{
		collectPause.goHowToImageJp.SetActive (Application.systemLanguage == SystemLanguage.Japanese);
		collectPause.goHowToImageEn.SetActive (Application.systemLanguage != SystemLanguage.Japanese);

		collectPause.goHowTo.SetActive (isHowTo);
		if (isHowTo) {
			SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
		} else {
			SoundManager.Instance.PlaySe (SoundManager.SeName.SE_CANCEL);
		}
	}
	
	
	private Player.Compass checkCompass;
	private Vector2 checkPosition;
	private Coroutine coroutine;
	private bool canThrough;
	private bool isDigged;

	private void OnTouchPointerDown (PointerEventData eventData)
	{
		if (MainManager.Instance.isTutorial) {
			if (PlayerPrefs.GetInt (Data.RECORD_IS_TUTORIAL_FIRST_HELP) == 1) {
				MainManager.Instance.NextStage (life.now, myWeapon.now);
				SetClearNextStage ();
			}
		}


		switch (state) {
		case State.Ready:
		case State.ReadyContinue:
		{
				if (!MainManager.Instance.isTutorial) {
					if (pattern == 0) {
						if (time > 0) {
							pattern = 1;
							time = 0;
						}
					}
				}
			}
			break;
		case State.Play:
			{
				isTouch = true;

				checkCompass = player.compass;
				checkPosition = eventData.pressPosition;
				coroutine = StartCoroutine (SetCanHoleCycle ());
			}
			break;
		case State.Clear:
			{
				if (pattern == 0) {
				} else if (pattern < 10) {
					pattern = 10;
					time = 0;
				} else if (pattern == 11) {
					if (!collectClear.isAllEnemyDie)
						PlayerPrefs.SetInt (Data.RECORD_ESCAPE, PlayerPrefs.GetInt (Data.RECORD_ESCAPE) + 1);

					bool isBoss = groupEnemyList.Exists (obj => obj.isBoss);
					if (isBoss) {
						MainManager.Instance.StoryEpilogue ();
					} else {
						MainManager.Instance.NextStage (life.now, myWeapon.now);
					}

					SetClearNextStage ();
				}
			}
			break;
		}
	}



	private IEnumerator SetCanHoleCycle ()
	{
		yield return new WaitForSeconds (0.2f);

		groupPlayer.canHoleCycle = true;
		isDigged = true;
	}



	private void OnTouchPointerUp (PointerEventData eventData)
	{
		if (state == State.Play) {
			isTouch = false;
			if (isDigged)
				canThrough = false;
			isDigged = false;

			if (playerNextCommand == PLAYER_NEXT_COMMAND_NONE)
				isCommandNoneClick = true;

			if (coroutine != null)
				StopCoroutine (coroutine);
		}

		goTrail.GetComponent<TrailRenderer> ().material.color = Color.white;
	}
	
	
	
	private void OnTouchDrag (PointerEventData eventData)
	{
		float ratio = 1.0f * Screen.width / Data.SCREEN_WIDTH;

		switch (state) {
		case State.Play:
			{
				Vector2 delta = eventData.delta / ratio;
				if (delta.magnitude >= 3) {
					Player.Compass compass;
					float radian = Mathf.Atan2 (delta.y, delta.x);
					if (radian < Mathf.PI / 4 * -3) {
						compass = Player.Compass.Left;
					} else if (radian < Mathf.PI / 4 * -1) {
						compass = Player.Compass.Bottom;
					} else if (radian < Mathf.PI / 4 * 1) {
						compass = Player.Compass.Right;
					} else if (radian < Mathf.PI / 4 * 3) {
						compass = Player.Compass.Top;
					} else {
						compass = Player.Compass.Left;
					}
					if (checkCompass != compass) {
						checkCompass = compass;
						checkPosition = eventData.position;
					}
				}
			
				Vector2 movePosition = (eventData.position - checkPosition) / ratio;
				if (playerNextCommand == PLAYER_NEXT_COMMAND_NONE) {
					if (movePosition.magnitude >= Data.EVENT_MOVE_MAGNITUDE_COMPASS) {
						playerNextCommand = PLAYER_NEXT_COMMAND_COMPASS;
						goTrail.GetComponent<TrailRenderer> ().material.color = Color.blue;
					}
				}
				if (playerNextCommand == PLAYER_NEXT_COMMAND_COMPASS) {
					if (movePosition.magnitude >= Data.EVENT_MOVE_MAGNITUDE_COMPASS) {
						if (playerNextCompass != checkCompass)
							groupPlayer.canHoleCycle = false;
						playerNextCompass = checkCompass;
					}
					if (movePosition.magnitude >= Data.EVENT_MOVE_MAGNITUDE_COMPASS_WALK) {
						playerNextCommand = PLAYER_NEXT_COMMAND_COMPASS_WALK;
						goTrail.GetComponent<TrailRenderer> ().material.color = Color.white;
					}
				}
				if (playerNextCommand == PLAYER_NEXT_COMMAND_COMPASS_WALK) {
					if (movePosition.magnitude >= Data.EVENT_MOVE_MAGNITUDE_COMPASS_WALK2) {
						playerNextCompass = checkCompass;
					}
				}
			}
			break;
		case State.TutorialHelp:
			{
				Vector2 delta = eventData.delta / ratio;
				if (delta.x < -5) {
					OnHelpNextPage ();
				} else if (delta.x > 5) {
					OnHelpPrevPage ();
				}
			}
			break;
		}

		goTrail.transform.localPosition = eventData.position / ratio;
	}



	private void OnItem (OwnerItem ownerItem)
	{
		if (state == State.Play) {
			if (ownerItem.state == OwnerItem.State.Have)
				ownerItem.state = OwnerItem.State.Use;
		}
	}



	private void OnContinue (int type)
	{
		if (state == State.Continue) {
			// 武器半分(2以上保持時).
			if (myWeapon.now > 1)
				myWeapon.now /= 2;
			continueCommand = type;
			SoundManager.Instance.StopSe ();
			SoundManager.Instance.PlaySe (type != CONTINUE_COMMAND_NO ? SoundManager.SeName.SE_OK : SoundManager.SeName.SE_CANCEL);
		}
	}
	
	
	
	private void OnHelpNextPage ()
	{
		if (!help.isMove) {
			help.Next ();
			if (help.isMove)
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_MOVE);
		}
	}
	
	
	
	private void OnHelpPrevPage ()
	{
		if (!help.isMove) {
			help.Prev ();
			if (help.isMove)
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_MOVE);
		}
	}
	
	
	
	private void OnVolume (bool isMute)
	{
		SoundManager.Instance.SetMute (isMute);
		collectPause.goVolumeOn.SetActive (!isMute);
		collectPause.goVolumeOff.SetActive (isMute);
		PlayerPrefs.SetInt (Data.SOUND_MUTE, isMute ? 1 : 0);
	}



	private void SetClearNextStage ()
	{
		keepCellList = null;
		PlayerPrefs.SetInt (Data.RECORD_SCORE_ALL, PlayerPrefs.GetInt (Data.RECORD_SCORE_ALL) + score.now - MainManager.Instance.score);
		MainManager.Instance.score = score.now;
		MainManager.Instance.scoreHigh = score.high;
		MainManager.Instance.RecordSave ();
	}


	
	private bool IsEnemyNextCommand (Enemy enemy)
	{
		int enemyPatternIndex = Data.GetEnemyData (enemy.type).patternIndex;
		Data.EnemyPatternData enemyPatternData = Data.enemyPatternDataList [enemyPatternIndex];

		int random = (int)(UnityEngine.Random.value * 100);
		if (random < enemyPatternData.rateWait)
			return false;
		return true;
	}



	private Enemy.Compass GetEnemyNextCompass (List<Chip> chipList, Player player, Enemy enemy, GroupEnemy groupEnemy)
	{
		Func<Enemy.Compass> turnRight = () => {
			switch (enemy.compass) {
			case Enemy.Compass.Right:
				return Enemy.Compass.Bottom;
			case Enemy.Compass.Left:
				return Enemy.Compass.Top;
			case Enemy.Compass.Top:
				return Enemy.Compass.Right;
			case Enemy.Compass.Bottom:
				return Enemy.Compass.Left;
			}
			return enemy.compass;
		};


		Func<Enemy.Compass> turnLeft = () => {
			switch (enemy.compass) {
			case Enemy.Compass.Right:
				return Enemy.Compass.Top;
			case Enemy.Compass.Left:
				return Enemy.Compass.Bottom;
			case Enemy.Compass.Top:
				return Enemy.Compass.Left;
			case Enemy.Compass.Bottom:
				return Enemy.Compass.Right;
			}
			return enemy.compass;
		};


		Func<Enemy.Compass> turnBack = () => {
			switch (enemy.compass) {
			case Enemy.Compass.Right:
				return Enemy.Compass.Left;
			case Enemy.Compass.Left:
				return Enemy.Compass.Right;
			case Enemy.Compass.Top:
				return Enemy.Compass.Bottom;
			case Enemy.Compass.Bottom:
				return Enemy.Compass.Top;
			}
			return enemy.compass;
		};


		Func<bool> isFollow = () => {
			bool success = true;
			float startPositionX = enemy.positionX + Data.SIZE_CHIP / 2;
			float startPositionY = enemy.positionY + Data.SIZE_CHIP / 2;
			float endPositionX = player.positionX + Data.SIZE_CHIP / 2;
			float endPositionY = player.positionY + Data.SIZE_CHIP / 2;
			while (success) {
				Vector2 distance = new Vector2 (endPositionX - startPositionX, endPositionY - startPositionY);
				int pointX = Mathf.FloorToInt (startPositionX / Data.SIZE_CHIP);
				int pointY = Mathf.FloorToInt (startPositionY / Data.SIZE_CHIP);
				Chip chip = chipList [pointX + pointY * Data.LENGTH_X];
				success = chip.obstacleList.TrueForAll (obj => Data.GetObstacleData (obj.type).isThrough);
				if (success)
				if (distance.magnitude <= Data.SIZE_CHIP)
					break;
				startPositionX += distance.normalized.x * Data.SIZE_CHIP;
				startPositionY += distance.normalized.y * Data.SIZE_CHIP;
			}
			return success;
		};
		
		
		List<Enemy.Compass> followCompassList = new List<Enemy.Compass> ();
		Func<bool> follow = () => {
			Func<int, int, Enemy.Compass, bool> locate = null;
			locate = (int pointX, int pointY, Enemy.Compass compass) => {
				if (player.pointX == pointX && player.pointY == pointY) {
					followCompassList.Add (compass);
					return true;
				}
				bool isWalk = true;
				isWalk = chipList [pointX + pointY * Data.LENGTH_X].terrain.type != Hakaima.Terrain.Type.River;
				chipList [pointX + pointY * Data.LENGTH_X].obstacleList.ForEach (obj => {
					if (isWalk)
						isWalk = Data.GetObstacleData (obj.type).isThrough;
				});
				bool success = false;
				if (isWalk) {
					if (!success)
					if (player.pointX - pointX > 0)
						success = locate (pointX + 1, pointY, Enemy.Compass.Right);
					if (!success)
					if (player.pointX - pointX < 0)
						success = locate (pointX - 1, pointY, Enemy.Compass.Left);
					if (!success)
					if (player.pointY - pointY > 0)
						success = locate (pointX, pointY + 1, Enemy.Compass.Top);
					if (!success)
					if (player.pointY - pointY < 0)
						success = locate (pointX, pointY - 1, Enemy.Compass.Bottom);
				}
				if (success)
					followCompassList.Add (compass);
				return success;
			};
			return locate (enemy.pointX, enemy.pointY, enemy.compass);
		};


		Enemy.Compass nextCompass = enemy.compass;
		{
			int enemyPatternIndex = Data.GetEnemyData (enemy.type).patternIndex;
			Data.EnemyPatternData enemyPatternData = Data.enemyPatternDataList [enemyPatternIndex];

			bool loop;
			do {
				loop = false;

				int rateStraight = enemyPatternData.rateStraight;
				int rateTurnRight = enemyPatternData.rateTurnRight + rateStraight;
				int rateTurnLeft = enemyPatternData.rateTurnLeft + rateTurnRight;
				int rateTurnBack = enemyPatternData.rateTurnBack + rateTurnLeft;
				int rateFollow = enemyPatternData.rateFollow + rateTurnBack;

				int random = (int)(UnityEngine.Random.value * (100 - enemyPatternData.rateWait));
				if (groupEnemy.isFollow) {
					random = rateFollow - 1;
				}
				if (random < rateStraight) {
					nextCompass = enemy.compass;
				} else if (random < rateTurnRight) {
					nextCompass = turnRight ();
				} else if (random < rateTurnLeft) {
					nextCompass = turnLeft ();
				} else if (random < rateTurnBack) {
					nextCompass = turnBack ();
				} else if (random < rateFollow) {
					if (player.state == Player.State.Damage) {
						enemyPatternData = Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL];
						loop = true;
						if (groupEnemy.isFollow) {
							groupEnemy.isFollow = false;
							groupEnemy.lostTime = 0.5f;
						}
						continue;
					} else if (groupPlayer.invincibleTime > 0 || groupPlayer.bathTime > 0 || groupPlayer.hideTime > 0) {
						enemyPatternData = Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL];
						loop = true;
						if (groupEnemy.isFollow) {
							groupEnemy.isFollow = false;
							groupEnemy.lostTime = 0.5f;
						}
						continue;
					} else if (groupPlayer.isParasol) {
						enemyPatternData = Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL];
						loop = true;
						if (groupEnemy.isFollow) {
							groupEnemy.isFollow = false;
							groupEnemy.lostTime = 0.5f;
						}
						continue;
					} else if (!isFollow ()) {
						enemyPatternData = Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL];
						loop = true;
						if (groupEnemy.isFollow) {
							groupEnemy.isFollow = false;
							groupEnemy.lostTime = 0.5f;
						}
						continue;
					}

					follow ();
					if (followCompassList.Count == 0) {
						enemyPatternData = Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL];
						loop = true;
						if (groupEnemy.isFollow) {
							groupEnemy.isFollow = false;
							groupEnemy.lostTime = 0.5f;
						}
						continue;
					} else if (followCompassList.Count == 1) {
						nextCompass = followCompassList [followCompassList.Count - 1];
					} else {
						nextCompass = followCompassList [followCompassList.Count - 2];
					}
					if (!groupEnemy.isFollow) {
						groupEnemy.isFollow = true;
						groupEnemy.noticeTime = 0.5f;
					}
				}
			} while (loop);
		}

		return nextCompass;
	}



	private List<Cell> GetCellList ()
	{
		List<Cell> cellList = new List<Cell> ();
		for (int i = 0; i < Data.LENGTH_X * Data.LENGTH_Y; i++) {
			Cell cell = new Cell ();
			cell.point = i;
			cell.pointX = i % Data.LENGTH_X;
			cell.pointY = i / Data.LENGTH_X;
			cellList.Add (cell);
		}


		if (MainManager.Instance.isTutorial) {
			for (int i = 0; i < cellList.Count; i++) {
				cellList [i].terrainType	= Data.tutorialStageData.terrainTypeList [i];
				cellList [i].obstacleType	= Data.tutorialStageData.obstacleTypeList [i];
				cellList [i].enemyType		= Data.tutorialStageData.enemyTypeList [i];
			}
			return cellList;
		}


		Data.StageData stageData = Data.GetStageData (MainManager.Instance.stage);

		List<Hakaima.Terrain.Type> terrainTypeList = new List<Hakaima.Terrain.Type> ();
		{
			int soil		= Mathf.Max (0, stageData.terrainTypeList [Hakaima.Terrain.Type.Soil]);
			int grass		= Mathf.Max (0, stageData.terrainTypeList [Hakaima.Terrain.Type.Grass])		+ soil;
			int muddy		= Mathf.Max (0, stageData.terrainTypeList [Hakaima.Terrain.Type.Muddy])		+ grass;
			int pavement	= Mathf.Max (0, stageData.terrainTypeList [Hakaima.Terrain.Type.Pavement])	+ muddy;
			int ice			= Mathf.Max (0, stageData.terrainTypeList [Hakaima.Terrain.Type.Ice])		+ pavement;

			for (int i = 0; i < cellList.Count; i++) {
				if (i < soil) {
					terrainTypeList.Add (Hakaima.Terrain.Type.Soil);
				} else if (i < grass) {
					terrainTypeList.Add (Hakaima.Terrain.Type.Grass);
				} else if (i < muddy) {
					terrainTypeList.Add (Hakaima.Terrain.Type.Muddy);
				} else if (i < pavement) {
					terrainTypeList.Add (Hakaima.Terrain.Type.Pavement);
				} else if (i < ice) {
					terrainTypeList.Add (Hakaima.Terrain.Type.Ice);
				}
			}
		}

		List<Hakaima.Obstacle.Type> obstacleTypeList = new List<Hakaima.Obstacle.Type> ();
		{
			int tree					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Tree];
			int stone					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stone]					+ tree;
			int tomb					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Tomb]					+ stone;
			int cartRight				= stageData.obstacleTypeList [Hakaima.Obstacle.Type.CartRight]				+ tomb;
			int well					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Well]					+ cartRight;
			int bale					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Bale]					+ well;
			int bathtub					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Bathtub]				+ bale;
			int stockpile				= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stockpile]				+ bathtub;
			int signboard				= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Signboard]				+ stockpile;
			int tower					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Tower]					+ signboard;
			int fallenTree				= stageData.obstacleTypeList [Hakaima.Obstacle.Type.FallenTree]				+ tower;
			int stump					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stump]					+ fallenTree;
			int bucket					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Bucket]					+ stump;
			int lantern					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Lantern]				+ bucket;
			int stupaFence				= stageData.obstacleTypeList [Hakaima.Obstacle.Type.StupaFence]				+ lantern;
			int stupa					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stupa]					+ stupaFence;
			int largeTreeRight			= stageData.obstacleTypeList [Hakaima.Obstacle.Type.LargeTreeRight]			+ stupa;
			int rubble					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Rubble]					+ largeTreeRight;
			int picket					= stageData.obstacleTypeList [Hakaima.Obstacle.Type.Picket]					+ rubble;

			for (int i = 0; i < cellList.Count; i++) {
				if (i < tree) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Tree);
				} else if (i < stone) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Stone);
				} else if (i < tomb) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Tomb);
				} else if (i < cartRight) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.CartRight);
				} else if (i < well) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Well);
				} else if (i < bale) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Bale);
				} else if (i < bathtub) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Bathtub);
				} else if (i < stockpile) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Stockpile);
				} else if (i < signboard) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Signboard);
				} else if (i < tower) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Tower);
				} else if (i < fallenTree) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.FallenTree);
				} else if (i < stump) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Stump);
				} else if (i < bucket) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Bucket);
				} else if (i < lantern) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Lantern);
				} else if (i < stupaFence) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.StupaFence);
				} else if (i < stupa) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Stupa);
				} else if (i < largeTreeRight) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.LargeTreeRight);
				} else if (i < rubble) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Rubble);
				} else if (i < picket) {
					obstacleTypeList.Add (Hakaima.Obstacle.Type.Picket);
				}
			}
		}

		List<Hakaima.Enemy.Type> enemyTypeList = new List<Hakaima.Enemy.Type> ();
		{
			int person		= stageData.enemyTypeList [Hakaima.Enemy.Type.Person];
			int ghost		= stageData.enemyTypeList [Hakaima.Enemy.Type.Ghost]		+ person;
			int soul		= stageData.enemyTypeList [Hakaima.Enemy.Type.Soul]			+ ghost;
			int skeleton	= stageData.enemyTypeList [Hakaima.Enemy.Type.Skeleton]		+ soul;
			int mummy		= stageData.enemyTypeList [Hakaima.Enemy.Type.Mummy]		+ skeleton;
			int shadowman	= stageData.enemyTypeList [Hakaima.Enemy.Type.Shadowman]	+ mummy;
			int golem		= stageData.enemyTypeList [Hakaima.Enemy.Type.Golem]		+ shadowman;
			int goblin		= stageData.enemyTypeList [Hakaima.Enemy.Type.Goblin]		+ golem;
			int parasol		= stageData.enemyTypeList [Hakaima.Enemy.Type.Parasol]		+ goblin;
			int kappa		= stageData.enemyTypeList [Hakaima.Enemy.Type.Kappa]		+ parasol;
			int tengu		= stageData.enemyTypeList [Hakaima.Enemy.Type.Tengu]		+ kappa;

			for (int i = 0; i < cellList.Count; i++) {
				if (i < person) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Person);
				} else if (i < ghost) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Ghost);
				} else if (i < soul) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Soul);
				} else if (i < skeleton) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Skeleton);
				} else if (i < mummy) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Mummy);
				} else if (i < shadowman) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Shadowman);
				} else if (i < golem) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Golem);
				} else if (i < goblin) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Goblin);
				} else if (i < parasol) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Parasol);
				} else if (i < kappa) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Kappa);
				} else if (i < tengu) {
					enemyTypeList.Add (Hakaima.Enemy.Type.Tengu);
				}
			}
		}

		List<Hakaima.Item.Type> itemTypeList = new List<Hakaima.Item.Type> ();
		{
			int sandal	= stageData.itemTypeList [Hakaima.Item.Type.Sandal];
			int hoe		= stageData.itemTypeList [Hakaima.Item.Type.Hoe]		+ sandal;
			int stone	= stageData.itemTypeList [Hakaima.Item.Type.Stone]		+ hoe;
			int amulet	= stageData.itemTypeList [Hakaima.Item.Type.Amulet]		+ stone;
			int parasol	= stageData.itemTypeList [Hakaima.Item.Type.Parasol]	+ amulet;

			for (int i = 0; i < cellList.Count; i++) {
				if (i < sandal) {
					itemTypeList.Add (Hakaima.Item.Type.Sandal);
				} else if (i < hoe) {
					itemTypeList.Add (Hakaima.Item.Type.Hoe);
				} else if (i < stone) {
					itemTypeList.Add (Hakaima.Item.Type.Stone);
				} else if (i < amulet) {
					itemTypeList.Add (Hakaima.Item.Type.Amulet);
				} else if (i < parasol) {
					itemTypeList.Add (Hakaima.Item.Type.Parasol);
				}
			}
		}


		{
			List<Cell> cellTerrainPossibleList = new List<Cell> (cellList);
			{
				int hIndex = cellTerrainPossibleList.FindIndex (obj => obj.pointX == Data.PLAYER_START_POINT_X && obj.pointY == Data.PLAYER_START_POINT_Y);
				int tIndex = terrainTypeList.FindIndex (obj => obj == Hakaima.Terrain.Type.Soil || obj == Hakaima.Terrain.Type.Grass);
				cellTerrainPossibleList [hIndex].terrainType = terrainTypeList [tIndex];
				cellTerrainPossibleList.RemoveAt (hIndex);
				terrainTypeList.RemoveAt (tIndex);
			}
			while (cellTerrainPossibleList.Count > 0 && terrainTypeList.Count > 0) {
				int hIndex = UnityEngine.Random.Range (0, cellTerrainPossibleList.Count);
				int tIndex = UnityEngine.Random.Range (0, terrainTypeList.Count);
				cellTerrainPossibleList [hIndex].terrainType = terrainTypeList [tIndex];
				cellTerrainPossibleList.RemoveAt (hIndex);
				terrainTypeList.RemoveAt (tIndex);
			}

			List<int> pointXList = Enumerable.Range (0, Data.LENGTH_X).ToList ();
			List<int> pointYList = Enumerable.Range (0, Data.LENGTH_Y).ToList ();
			{
				bool isX = false;
				bool isY = false;
				int pavement = stageData.terrainTypeList [Hakaima.Terrain.Type.Pavement];
				if (pavement == -1) {
					if (UnityEngine.Random.Range (0, 2) == 0) {
						isX = true;
					} else {
						isY = true;
					}
				} else if (pavement == -2) {
					isX = true;
					isY = true;
				}
				if (isX) {
					int pointX = pointXList [UnityEngine.Random.Range (0, pointXList.Count)];
					pointXList.Remove (pointX);
					cellList.FindAll (obj => obj.pointX == pointX).ForEach (obj => obj.terrainType = Hakaima.Terrain.Type.Pavement);
					cellList.FindAll (obj => obj.pointX == pointX - 1 && obj.pointY % 4 == 0).ForEach (obj => obj.obstacleType = Obstacle.Type.Rubble);
					cellList.FindAll (obj => obj.pointX == pointX + 1 && obj.pointY % 4 == 0).ForEach (obj => obj.obstacleType = Obstacle.Type.Rubble);
				}
				if (isY) {
					int pointY = pointYList [UnityEngine.Random.Range (0, pointYList.Count)];
					pointYList.Remove (pointY);
					cellList.FindAll (obj => obj.pointY == pointY).ForEach (obj => obj.terrainType = Hakaima.Terrain.Type.Pavement);
					cellList.FindAll (obj => obj.pointY == pointY - 1 && obj.pointX % 4 == 0).ForEach (obj => obj.obstacleType = Obstacle.Type.Rubble);
					cellList.FindAll (obj => obj.pointY == pointY + 1 && obj.pointX % 4 == 0).ForEach (obj => obj.obstacleType = Obstacle.Type.Rubble);
				}
				cellList.FindAll (obj => obj.terrainType == Hakaima.Terrain.Type.Pavement && obj.obstacleType == Obstacle.Type.Rubble).ForEach (obj => obj.obstacleType = Obstacle.Type.None);
			}
			{
				List<Cell> cellRiverList = null;
				bool isX = false;
				bool isY = false;
				int river = stageData.terrainTypeList [Hakaima.Terrain.Type.River];
				int bridge = stageData.terrainTypeList [Hakaima.Terrain.Type.Bridge];
				if (river == 1) {
					isX = true;
				} else if (river == 2) {
					isY = true;
				} else if (river == 3) {
					isX = true;
				} else if (river == 4) {
					isY = true;
				}
				if (isX) {
					pointXList = pointXList.FindAll (n => n != Data.PLAYER_START_POINT_X);
					int pointX = pointXList [UnityEngine.Random.Range (0, pointXList.Count)];
					if (river == 3)
						pointX = Data.PLAYER_START_POINT_X + 1;
					pointXList.Remove (pointX);
					cellRiverList = cellList.FindAll (obj => obj.pointX == pointX);
					int count = 0;
					while (cellRiverList.Count > 0) {
						int index = UnityEngine.Random.Range (0, cellRiverList.Count);
						if (count < bridge) {
							if (cellRiverList [index].pointY == 0) {
								int point = cellRiverList [index].pointX + Data.LENGTH_X * (cellRiverList [index].pointY + 1);
								if (cellList [point].terrainType != Hakaima.Terrain.Type.BridgeHorizontal) {
									cellRiverList [index].terrainType = Hakaima.Terrain.Type.BridgeHorizontal;
									cellRiverList.RemoveAt (index);
									count++;
									continue;
								}
							} else if (cellRiverList [index].pointY == Data.LENGTH_Y - 1) {
								int point = cellRiverList [index].pointX + Data.LENGTH_X * (cellRiverList [index].pointY - 1);
								if (cellList [point].terrainType != Hakaima.Terrain.Type.BridgeHorizontal) {
									cellRiverList [index].terrainType = Hakaima.Terrain.Type.BridgeHorizontal;
									cellRiverList.RemoveAt (index);
									count++;
									continue;
								}
							} else {
								int point1 = cellRiverList [index].pointX + Data.LENGTH_X * (cellRiverList [index].pointY + 1);
								int point2 = cellRiverList [index].pointX + Data.LENGTH_X * (cellRiverList [index].pointY - 1);
								if (cellList [point1].terrainType != Hakaima.Terrain.Type.BridgeHorizontal && cellList [point2].terrainType != Hakaima.Terrain.Type.BridgeHorizontal) {
									cellRiverList [index].terrainType = Hakaima.Terrain.Type.BridgeHorizontal;
									cellRiverList.RemoveAt (index);
									count++;
									continue;
								}
							}
						}
						cellRiverList [index].terrainType = Hakaima.Terrain.Type.River;
						cellRiverList.RemoveAt (index);
					}
				}
				if (isY) {
					pointYList = pointYList.FindAll (n => n != Data.PLAYER_START_POINT_Y);
					int pointY = pointYList [UnityEngine.Random.Range (0, pointYList.Count)];
					if (river == 4)
						pointY = Data.LENGTH_Y / 2;
					pointYList.Remove (pointY);
					cellRiverList = cellList.FindAll (obj => obj.pointY == pointY);
					int count = 0;
					while (cellRiverList.Count > 0) {
						int index = UnityEngine.Random.Range (0, cellRiverList.Count);
						if (count < bridge) {
							if (cellRiverList [index].pointX == 0) {
								int point = cellRiverList [index].pointX + 1 + Data.LENGTH_X * cellRiverList [index].pointY;
								if (cellList [point].terrainType != Hakaima.Terrain.Type.BridgeVertical) {
									cellRiverList [index].terrainType = Hakaima.Terrain.Type.BridgeVertical;
									cellRiverList.RemoveAt (index);
									count++;
									continue;
								}
							} else if (cellRiverList [index].pointX == Data.LENGTH_X - 1) {
								int point = cellRiverList [index].pointX - 1 + Data.LENGTH_X * cellRiverList [index].pointY;
								if (cellList [point].terrainType != Hakaima.Terrain.Type.BridgeVertical) {
									cellRiverList [index].terrainType = Hakaima.Terrain.Type.BridgeVertical;
									cellRiverList.RemoveAt (index);
									count++;
									continue;
								}
							} else {
								int point1 = cellRiverList [index].pointX + 1 + Data.LENGTH_X * cellRiverList [index].pointY;
								int point2 = cellRiverList [index].pointX - 1 + Data.LENGTH_X * cellRiverList [index].pointY;
								if (cellList [point1].terrainType != Hakaima.Terrain.Type.BridgeVertical && cellList [point2].terrainType != Hakaima.Terrain.Type.BridgeVertical) {
									cellRiverList [index].terrainType = Hakaima.Terrain.Type.BridgeVertical;
									cellRiverList.RemoveAt (index);
									count++;
									continue;
								}
							}
						}
						cellRiverList [index].terrainType = Hakaima.Terrain.Type.River;
						cellRiverList.RemoveAt (index);
					}
				}
			}
		}
		{
			List<Cell> cellEnemyPossibleList = cellList.FindAll (obj => {
				switch (obj.pointY) {
				case 0:
				case 1:
				case 2:
				case 3:
					return false;
				}
				switch (obj.terrainType) {
				case Hakaima.Terrain.Type.Soil:
				case Hakaima.Terrain.Type.Grass:
				case Hakaima.Terrain.Type.Muddy:
				case Hakaima.Terrain.Type.Pavement:
				case Hakaima.Terrain.Type.Ice:
					switch (obj.obstacleType) {
					case Obstacle.Type.None:
						return true;
					}
					break;
				}
				return false;
			});
			while (cellEnemyPossibleList.Count > 0 && enemyTypeList.Count > 0) {
				if (enemyTypeList.Exists (obj => obj == Enemy.Type.Tengu)) {
					List<Cell> list = cellEnemyPossibleList.FindAll (obj => (obj.pointX >= 3 && obj.pointX <= 6) && (obj.pointY >= 9 && obj.pointY <= 12));
					Cell cell = list [UnityEngine.Random.Range (0, list.Count)];
					cell.enemyType = Enemy.Type.Tengu;
					cellEnemyPossibleList.Remove (cell);
					enemyTypeList.Remove (Enemy.Type.Tengu);
				} else {
					int hIndex = UnityEngine.Random.Range (0, cellEnemyPossibleList.Count);
					int eIndex = UnityEngine.Random.Range (0, enemyTypeList.Count);
					cellEnemyPossibleList [hIndex].enemyType = enemyTypeList [eIndex];
					cellEnemyPossibleList.RemoveAt (hIndex);
					enemyTypeList.RemoveAt (eIndex);
				}
			}
		}
		{
			List<Cell> cellObstaclePossibleList = cellList.FindAll (obj => {
				switch (obj.point) {
				case (Data.PLAYER_START_POINT_X)		+ (Data.PLAYER_START_POINT_Y)		* Data.LENGTH_X:
				case (Data.PLAYER_START_POINT_X - 1)	+ (Data.PLAYER_START_POINT_Y)		* Data.LENGTH_X:
				case (Data.PLAYER_START_POINT_X + 1)	+ (Data.PLAYER_START_POINT_Y)		* Data.LENGTH_X:
				case (Data.PLAYER_START_POINT_X)		+ (Data.PLAYER_START_POINT_Y + 1)	* Data.LENGTH_X:
					return false;
				}
				if (obj.enemyType != Enemy.Type.None) {
					return false;
				}
				switch (obj.terrainType) {
				case Hakaima.Terrain.Type.Soil:
				case Hakaima.Terrain.Type.Grass:
				case Hakaima.Terrain.Type.Ice:
					if (obj.pointX + 1 < Data.LENGTH_X) {
						switch (cellList [(obj.pointX + 1) + obj.pointY * Data.LENGTH_X].terrainType) {
						case Hakaima.Terrain.Type.BridgeVertical:
						case Hakaima.Terrain.Type.BridgeHorizontal:
							return false;
						}
					}
					if (obj.pointX - 1 >= 0) {
						switch (cellList [(obj.pointX - 1) + obj.pointY * Data.LENGTH_X].terrainType) {
						case Hakaima.Terrain.Type.BridgeVertical:
						case Hakaima.Terrain.Type.BridgeHorizontal:
							return false;
						}
					}
					if (obj.pointY + 1 < Data.LENGTH_Y) {
						switch (cellList [obj.pointX + (obj.pointY + 1) * Data.LENGTH_X].terrainType) {
						case Hakaima.Terrain.Type.BridgeVertical:
						case Hakaima.Terrain.Type.BridgeHorizontal:
							return false;
						}
					}
					if (obj.pointY - 1 >= 0) {
						switch (cellList [obj.pointX + (obj.pointY - 1) * Data.LENGTH_X].terrainType) {
						case Hakaima.Terrain.Type.BridgeVertical:
						case Hakaima.Terrain.Type.BridgeHorizontal:
							return false;
						}
					}
					switch (obj.obstacleType) {
					case Obstacle.Type.None:
						return true;
					}
					break;
				}
				return false;
			});
			while (cellObstaclePossibleList.Count > 0 && obstacleTypeList.Count > 0) {
				int hIndex = UnityEngine.Random.Range (0, cellObstaclePossibleList.Count);
				int oIndex = UnityEngine.Random.Range (0, obstacleTypeList.Count);
				switch (obstacleTypeList [oIndex]) {
				case Obstacle.Type.LargeTreeRight:
					{
						Cell cellRight = cellObstaclePossibleList [hIndex];
						Cell cellLeft = cellObstaclePossibleList.Find (obj => obj.point == cellRight.point - 1);
						if (cellRight.pointX != 0) {
							if (cellLeft != null) {
								cellRight.obstacleType = Obstacle.Type.LargeTreeRight;
								cellLeft.obstacleType = Obstacle.Type.LargeTreeLeft;
								cellObstaclePossibleList.Remove (cellRight);
								cellObstaclePossibleList.Remove (cellLeft);
								obstacleTypeList.RemoveAt (oIndex);
							}
						}
					}
					break;
				case Obstacle.Type.CartRight:
					{
						List<Cell> list = cellObstaclePossibleList.FindAll (obj => (obj.pointX != 0 && obj.pointX != Data.LENGTH_X - 1));
						list = list.FindAll (obj => cellList [obj.point - 1].obstacleType == Obstacle.Type.None && cellList [obj.point + 1].obstacleType == Obstacle.Type.None);
						if (list.Count > 0) {
							Cell cell = list [UnityEngine.Random.Range (0, list.Count)];
							cell.obstacleType = Obstacle.Type.CartRight;
							cellObstaclePossibleList.Remove (cell);
							cellObstaclePossibleList.Remove (cellList.Find (obj => obj.point == cell.point - 1));
							cellObstaclePossibleList.Remove (cellList.Find (obj => obj.point == cell.point + 1));
							obstacleTypeList.Remove (Obstacle.Type.CartRight);
						} else {
							obstacleTypeList.RemoveAt (oIndex);
						}
					}
					break;
				case Obstacle.Type.Tower:
					{
						List<Cell> towerList = cellList.FindAll (obj => obj.obstacleType == Obstacle.Type.Tower);
						List<Cell> list = cellObstaclePossibleList;
						for (int i = 0; i < towerList.Count; i++) {
							Cell tower = towerList [i];
							list = list.FindAll (obj => obj.pointX != tower.pointX + 1 && obj.pointX != tower.pointX - 1);
							list = list.FindAll (obj => obj.pointY != tower.pointY + 1 && obj.pointY != tower.pointY - 1);
						}
						Cell cell = list [UnityEngine.Random.Range (0, list.Count)];
						cell.obstacleType = Obstacle.Type.Tower;
						cellObstaclePossibleList.Remove (cell);
						obstacleTypeList.Remove (Obstacle.Type.Tower);
					}
					break;
				case Obstacle.Type.Well:
					{
						List<Cell> wellList = cellList.FindAll (obj => obj.obstacleType == Obstacle.Type.Well);
						List<Cell> list = cellObstaclePossibleList;
						for (int i = 0; i < wellList.Count; i++) {
							Cell well = wellList [i];
							list = list.FindAll (obj => obj.pointX != well.pointX + 1 && obj.pointX != well.pointX - 1);
							list = list.FindAll (obj => obj.pointY != well.pointY + 1 && obj.pointY != well.pointY - 1);
						}
						Cell cell = list [UnityEngine.Random.Range (0, list.Count)];
						cell.obstacleType = Obstacle.Type.Well;
						cellObstaclePossibleList.Remove (cell);
						obstacleTypeList.Remove (Obstacle.Type.Well);
					}
					break;
				default:
					cellObstaclePossibleList [hIndex].obstacleType = obstacleTypeList [oIndex];
					cellObstaclePossibleList.RemoveAt (hIndex);
					obstacleTypeList.RemoveAt (oIndex);
					break;
				}
			}
			if (stageData.darkness <= 0.2f)
				cellList.FindAll (obj => obj.obstacleType == Obstacle.Type.Rubble).ForEach (obj => obj.obstacleType = Obstacle.Type.RubbleOff);
		}
		{
			List<Cell> cellItemPossibleList = cellList.FindAll (obj => {
				switch (obj.terrainType) {
				case Hakaima.Terrain.Type.Soil:
				case Hakaima.Terrain.Type.Grass:
				case Hakaima.Terrain.Type.Muddy:
					switch (obj.obstacleType) {
					case Hakaima.Obstacle.Type.None:
						return true;
					}
					break;
				}
				return false;
			});
			while (cellItemPossibleList.Count > 0 && itemTypeList.Count > 0) {
				int hIndex = UnityEngine.Random.Range (0, cellItemPossibleList.Count);
				int eIndex = UnityEngine.Random.Range (0, itemTypeList.Count);
				cellItemPossibleList [hIndex].itemType = itemTypeList [eIndex];
				cellItemPossibleList.RemoveAt (hIndex);
				itemTypeList.RemoveAt (eIndex);
			}
		}
		{
			List<Cell> cellHolePossibleList = cellList.FindAll (obj => {
				switch (obj.point) {
				case (Data.PLAYER_START_POINT_X) + (Data.PLAYER_START_POINT_Y) * Data.LENGTH_X:
					return false;
				}
				if (Data.GetTerrainData (obj.terrainType).isDigging)
				if (obj.obstacleType == Obstacle.Type.None)
				if (obj.enemyType == Enemy.Type.None)
					return true;
				return false;
			});
			int holeOpen = stageData.holeOpen;
			while (cellHolePossibleList.Count > 0 && holeOpen > 0) {
				int hIndex = UnityEngine.Random.Range (0, cellHolePossibleList.Count);
				cellHolePossibleList [hIndex].isHoleOpen = true;
				cellHolePossibleList.RemoveAt (hIndex);
				holeOpen--;
			}
		}

		return cellList;
	}



	private void CheckBackKey ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			OnPause (!isPause);
		}
	}



	private void OnDebugDamageClick ()
	{
		isDebugDamage = !isDebugDamage;
		transform.Find ("UI/Debug/Damage/Text").GetComponent<Text> ().text = string.Format ("敵当\n{0}", isDebugDamage ? "ON" : "OFF");
	}



	private void OnTwitter ()
	{
		// Add 2017.11.7
		#if UNITY_ANDROID
		SocialConnector.SocialConnector.Share (Language.sentence [Language.TWITTER], Data.URL, null);
		#elif UNITY_IOS
		SocialConnector.SocialConnector.Share (Language.sentence [Language.TWITTER], Data.URL_IOS, null);
		#endif
	}
}
