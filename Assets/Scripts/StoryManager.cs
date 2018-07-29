using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hakaima;


public class StoryManager : MonoBehaviour
{

	public class StoryPerson
	{
		public enum State
		{
			Wait,
			Walk,
		}

		public enum Compass
		{
			Right,
			Left,
		}

		public const int IMAGE_0		= 0;
		public const int IMAGE_1		= 1;
		public const int IMAGE_2		= 2;

		public State state				{ get; private set; }
		public Compass compass			{ get; private set; }
		public float positionX			{ get; private set; }
		public float positionY			{ get; private set; }
		public float speed				{ get; private set; }
		public int imageIndex			{ get; private set; }
		public float imageCoefficient	{ get; private set; }
		private float imageTime;


		public void Init (float positionX, float positionY, float speed, float imageCoefficient)
		{
			this.state = State.Wait;
			this.compass = Compass.Right;
			this.positionX = positionX;
			this.positionY = positionY;
			this.speed = speed;
			this.imageIndex = IMAGE_0;
			this.imageCoefficient = imageCoefficient;
		}


		public void Move (float deltaTime, int frameRate)
		{
			switch (this.state) {
			case State.Wait:
				{
				}
				break;
			case State.Walk:
				{
					switch (this.compass) {
					case Compass.Right:
						{
							this.positionX += this.speed * deltaTime * frameRate;
						}
						break;
					case Compass.Left:
						{
							this.positionX -= this.speed * deltaTime * frameRate;
						}
						break;
					}
				
					int index = (int)this.imageTime % 4;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					case 2:
						this.imageIndex = IMAGE_0;
						break;
					case 3:
						this.imageIndex = IMAGE_2;
						break;
					}
					this.imageTime += this.speed * deltaTime * frameRate * this.imageCoefficient;
				}
				break;
			}
		}


		public void Wait (Compass compass)
		{
			this.state = State.Wait;
			this.compass = compass;
			this.imageIndex = IMAGE_0;
			this.imageTime = 0;
		}


		public void Walk (Compass compass)
		{
			this.state = State.Walk;
			this.compass = compass;
			this.imageIndex = IMAGE_0;
			this.imageTime = 0;
		}
	}
	
	
	
	public class StoryThankMask
	{
		public enum State
		{
			Wait,
			Active,
		}

		public State state	{ get; private set;}
		public float width	{ get; private set;}
		public float height	{ get; private set;}
		public float speed	{ get; private set;}
		
		public void Init (float width, float height, float speed)
		{
			this.width = width;
			this.height = height;
			this.speed = speed;
		}
		
		public void Move (float deltaTime, int frameRate)
		{
			switch (this.state) {
			case State.Wait:
				{
				}
				break;
			case State.Active:
				{
					this.height += this.speed * deltaTime * frameRate;
				}
				break;
			}
		}

		public void Wait ()
		{
			if (this.state == State.Active) {
				this.state = State.Wait;
			}
		}

		public void Active ()
		{
			if (this.state == State.Wait) {
				this.state = State.Active;
			}
		}
	}



	public class StoryCloud
	{
		public float positionX	{ get; private set; }
		public float positionY	{ get; private set; }
		public float speed		{ get; private set; }
		private float sidePositionX;

		public void Init (float positionX, float positionY, float speed, float sidePositionX){
			this.positionX = positionX;
			this.positionY = positionY;
			this.speed = speed;
			this.sidePositionX = sidePositionX;
		}

		public void Move (float deltaTime, int frameRate)
		{
			this.positionX += this.speed * deltaTime * frameRate;
			if (Mathf.Abs (this.positionX) >= this.sidePositionX) {
				this.positionX = this.sidePositionX * -this.speed / Mathf.Abs (this.speed);
			}
		}
	}



	public enum Discourse
	{
		None,
		Prologue,
		Epilogue,
	}


	private const int PATTERN_0		= 0;
	private const int PATTERN_1		= 1;
	private const int PATTERN_2		= 2;
	private const int PATTERN_3		= 3;
	private const int PATTERN_4		= 4;
	private const int PATTERN_5		= 5;
	private const int PATTERN_6		= 6;
	private const int PATTERN_7		= 7;
	private const int PATTERN_8		= 8;
	private const int PATTERN_9		= 9;
	private const int PATTERN_10	= 10;


	Discourse discourse;
	int pattern;
	float time;


	private StoryPerson player;
	private StoryPerson boss;
	private StoryPerson princess;
	private StoryThankMask thankMask;
	private List<StoryCloud> cloudList;

	private GameObject goTouch;
	private GameObject goCover;
	private GameObject goBackground;
	private GameObject goThank;
	private GameObject goThankMask;
	private GameObject goPlayer;
	private GameObject goBoss;
	private GameObject goPrincess;
	private GameObject goSelectStage;
	private List<GameObject> goCloudList;

	private GameObject goPlayerNotice;
	private GameObject goBossNotice;
	private GameObject goSelectStageBack;
	private GameObject goSelectStageNum;
	private GameObject goSelectStageRight;
	private GameObject goSelectStageLeft;
	private GameObject goSelectStageOn;


	private const float POSITION_Y			= 200;
	private const float SPEED				= 8;
	private const float IMAGE_COEFFICIENT	= 0.02f;

	private bool isSelectStage;
	private int selectStageIndex = 0;

	private bool isPause;

	private Dictionary<int, Sprite> spritePlayerList;
	
	[SerializeField]
	private Sprite spriteBossCompassRight0;
	[SerializeField]
	private Sprite spriteBossCompassRight1;
	[SerializeField]
	private Sprite spriteBossCompassRight2;
	[SerializeField]
	private Sprite spriteBossCompassLeft0;
	[SerializeField]
	private Sprite spriteBossCompassLeft1;
	[SerializeField]
	private Sprite spriteBossCompassLeft2;
	[HideInInspector]
	public Dictionary<int, Sprite> spriteBossList;



	private void Start ()
	{
		Init ();
		Create ();
		Draw ();
	}



	private void Update ()
	{
		Run ();
		Draw ();
	}



	private void Init ()
	{
		goTouch			= transform.Find ("UI/Touch").gameObject;
		goCover			= transform.Find ("UI/Cover").gameObject;
		goBackground	= transform.Find ("UI/Background").gameObject;
		goThank			= transform.Find ("UI/Thank").gameObject;
		goThankMask		= transform.Find ("UI/Thank/Mask").gameObject;
		goPlayer		= transform.Find ("UI/Player").gameObject;
		goBoss			= transform.Find ("UI/Boss").gameObject;
		goPrincess		= transform.Find ("UI/Princess").gameObject;
		goSelectStage	= transform.Find ("UI/SelectStage").gameObject;
		goCloudList		= new List<GameObject> (){
			transform.Find ("UI/Cloud/Cloud0").gameObject,
			transform.Find ("UI/Cloud/Cloud1").gameObject,
			transform.Find ("UI/Cloud/Cloud2").gameObject,
			transform.Find ("UI/Cloud/Cloud3").gameObject,
			transform.Find ("UI/Cloud/Cloud4").gameObject,
		};

		goPlayerNotice		= goPlayer.transform.Find ("Notice").gameObject;
		goBossNotice		= goBoss.transform.Find ("Notice").gameObject;
		goSelectStageBack	= goSelectStage.transform.Find ("Back").gameObject;
		goSelectStageNum	= goSelectStage.transform.Find ("Number").gameObject;
		goSelectStageRight	= goSelectStage.transform.Find ("ArrowRight").gameObject;
		goSelectStageLeft	= goSelectStage.transform.Find ("ArrowLeft").gameObject;
		goSelectStageOn		= goSelectStage.transform.Find ("ButtonOn").gameObject;

		goTouch.GetComponent<Button> ().onClick.AddListener (() => OnTouchClick ());
		goSelectStageRight.GetComponent<Button> ().onClick.AddListener (() => OnSelectStageNext ());
		goSelectStageLeft.GetComponent<Button> ().onClick.AddListener (() => OnSelectStagePrev ());
		goSelectStageOn.GetComponent<Button> ().onClick.AddListener (() => OnSelectStageOpen ());

		goThank.SetActive (false);
		goSelectStage.SetActive (false);
		goSelectStageBack.SetActive (false);
		goSelectStageNum.SetActive (false);
		goSelectStageRight.SetActive (false);
		goSelectStageLeft.SetActive (false);
		goSelectStageOn.SetActive (true);
		goPlayerNotice.SetActive (false);
		goBossNotice.SetActive (false);

		SetPlayer ();

		spriteBossList = new Dictionary<int, Sprite> (){
			{(int)StoryPerson.Compass.Right	* ResourceManager.SPRITE_MULTI_COMPASS + StoryPerson.IMAGE_0,	spriteBossCompassRight0	},
			{(int)StoryPerson.Compass.Right	* ResourceManager.SPRITE_MULTI_COMPASS + StoryPerson.IMAGE_1,	spriteBossCompassRight1	},
			{(int)StoryPerson.Compass.Right	* ResourceManager.SPRITE_MULTI_COMPASS + StoryPerson.IMAGE_2,	spriteBossCompassRight2	},
			{(int)StoryPerson.Compass.Left	* ResourceManager.SPRITE_MULTI_COMPASS + StoryPerson.IMAGE_0,	spriteBossCompassLeft0	},
			{(int)StoryPerson.Compass.Left	* ResourceManager.SPRITE_MULTI_COMPASS + StoryPerson.IMAGE_1,	spriteBossCompassLeft1	},
			{(int)StoryPerson.Compass.Left	* ResourceManager.SPRITE_MULTI_COMPASS + StoryPerson.IMAGE_2,	spriteBossCompassLeft2	},
		};
	}



	private void Create ()
	{
		player = new StoryPerson ();
		boss = new StoryPerson ();
		princess = new StoryPerson ();
		thankMask = new StoryThankMask ();

		cloudList = new List<StoryCloud> ();
		for (int i = 0; i < goCloudList.Count; i++) {
			StoryCloud cloud = new StoryCloud ();
			GameObject goCloud = goCloudList [i];
			cloud.Init (goCloud.transform.localPosition.x, goCloud.transform.localPosition.y, Random.value * 0.2f + 0.2f, 800);
			cloudList.Add (cloud);
		}
		goCover.SetActive (true);
		goCover.GetComponent<Image> ().color = Color.black;

		isPause = false;


		switch (discourse) {
		case Discourse.Prologue:
			{
				goBackground.GetComponent<Image> ().color = new Color (128f / 255, 255f / 255, 255f / 255);
				SoundManager.Instance.PlayBgm (SoundManager.BgmName.BGM_OPENING);
				isSelectStage = PlayerPrefs.GetInt (Data.RECORD_CLEAR + 0) == 1;
				goSelectStage.SetActive (isSelectStage);
			}
			break;
		case Discourse.Epilogue:
			{
				goBackground.GetComponent<Image> ().color = new Color (255f / 255, 171f / 255, 64f / 255);
				SoundManager.Instance.PlayBgm (SoundManager.BgmName.BGM_ENDING);
			}
			break;
		}
	}



	private void Run ()
	{
		if (isPause)
			return;
		
		switch (this.discourse) {
		case Discourse.Prologue:
			{
				if (pattern == PATTERN_0) {
					if (time == 0) {
						player.Init (480, POSITION_Y, SPEED, IMAGE_COEFFICIENT);
						boss.Init (-100, POSITION_Y, SPEED * 2, IMAGE_COEFFICIENT);
						princess.Init (600, POSITION_Y, SPEED * 2, IMAGE_COEFFICIENT);
					}
					Color color = Color.black;
					color.a = Mathf.Lerp (1, 0, time);
					goCover.GetComponent<Image> ().color = color;
					if (color.a <= 0) {
						pattern = PATTERN_1;
						time = 0;
						goCover.SetActive (false);
					}
				}
				if (pattern == PATTERN_1) {
					if (time >= 3) {
						pattern = PATTERN_2;
						time = 0;
					}
				}
				if (pattern == PATTERN_2) {
					if (time == 0) {
						boss.Walk (StoryPerson.Compass.Right);
					}
					if (boss.positionX >= princess.positionX - 50) {
						boss.Wait (StoryPerson.Compass.Right);
						pattern = PATTERN_3;
						time = 0;
					}
				}
				if (pattern == PATTERN_3) {
					if (time >= 0.5f) {
						pattern = PATTERN_4;
						time = 0;
					}
				}
				if (pattern == PATTERN_4) {
					if (time == 0) {
						boss.Walk (StoryPerson.Compass.Left);
						princess.Walk (StoryPerson.Compass.Left);
					}
					if (player.positionX > boss.positionX) {
						player.Wait (StoryPerson.Compass.Left);
						goPlayerNotice.SetActive (true);
					}
					if (boss.positionX <= -100) {
						pattern = PATTERN_5;
						time = 0;
					}
				}
				if (pattern == PATTERN_5) {
					if (time == 0) {
						player.Walk (StoryPerson.Compass.Left);
						goPlayerNotice.SetActive (false);
					}
					if (player.positionX <= -100) {
						pattern = PATTERN_6;
						time = 0;
					}
				}
				if (pattern == PATTERN_6) {
					if (time == 0) {
						goCover.SetActive (true);
						goSelectStage.SetActive (false);
					}
					Color color = Color.black;
					color.a = Mathf.Lerp (0, 1, time);
					goCover.GetComponent<Image> ().color = color;
					if (color.a >= 1) {
						if (isSelectStage)
							MainManager.Instance.SelectStage (selectStageIndex, MainManager.START_LIFE, MainManager.START_WEAPON);
						else
							MainManager.Instance.TutorialStage ();
					}
				}
			}
			break;
		case Discourse.Epilogue:
			{
				if (pattern == PATTERN_0) {
					if (time == 0) {
						player.Init (-100, POSITION_Y, SPEED, IMAGE_COEFFICIENT);
						boss.Init (480, POSITION_Y, SPEED * 2, IMAGE_COEFFICIENT);
						princess.Init (600, POSITION_Y, SPEED * 2, IMAGE_COEFFICIENT);
						thankMask.Init (1000, 0, 1);
					}
					Color color = Color.black;
					color.a = Mathf.Lerp (1, 0, time);
					goCover.GetComponent<Image> ().color = color;
					if (color.a <= 0) {
						pattern = PATTERN_1;
						time = 0;
						goCover.SetActive (false);
					}
				}
				if (pattern == PATTERN_1) {
					if (time == 0) {
						player.Walk (StoryPerson.Compass.Right);
					}
					if (player.positionX >= 50) {
						player.Wait (StoryPerson.Compass.Right);
					}
					if (time >= 1) {
						pattern = PATTERN_2;
						time = 0;
					}
				}
				if (pattern == PATTERN_2) {
					if (time == 0) {
						goBossNotice.SetActive (true);
					}
					if (time >= 0.5f) {
						pattern = PATTERN_3;
						time = 0;
					}
				}
				if (pattern == PATTERN_3) {
					if (time == 0) {
						goBossNotice.SetActive (false);
						boss.Wait (StoryPerson.Compass.Left);
					}
					if (time >= 0.7f) {
						pattern = PATTERN_4;
						time = 0;
					}
				}
				if (pattern == PATTERN_4) {
					if (time == 0) {
						boss.Walk (StoryPerson.Compass.Right);
					}
					if (time >= 1f) {
						pattern = PATTERN_5;
						time = 0;
					}
				}
				if (pattern == PATTERN_5) {
					if (time == 0) {
						player.Walk (StoryPerson.Compass.Right);
					}
					if (player.positionX >= 480) {
						player.Wait (StoryPerson.Compass.Right);
						boss.Wait (StoryPerson.Compass.Right);
						pattern = PATTERN_6;
						time = 0;
					}
				}
				if (pattern == PATTERN_6) {
					if (time == 0) {
						goThank.SetActive (true);
						thankMask.Active ();
					}
					if (thankMask.height >= 1000) {
						thankMask.Wait ();
						pattern = PATTERN_7;
						time = 0;
					}
				}
				if (pattern == PATTERN_7) {
				}
				if (pattern == PATTERN_10) {
					if (time == 0) {
						goCover.SetActive (true);
					}
					Color color = Color.black;
					color.a = Mathf.Lerp (0, 1, time);
					goCover.GetComponent<Image> ().color = color;
					if (color.a >= 1) {
						MainManager.Instance.Title ();
					}
				}
			}
			break;
		}

		player.Move (Time.deltaTime, Data.TARGET_FRAME_RATE);
		boss.Move (Time.deltaTime, Data.TARGET_FRAME_RATE);
		princess.Move (Time.deltaTime, Data.TARGET_FRAME_RATE);
		thankMask.Move (Time.deltaTime, Data.TARGET_FRAME_RATE);
		cloudList.ForEach (obj => obj.Move (Time.deltaTime, Data.TARGET_FRAME_RATE));

		time += Time.deltaTime;
	}



	private void Draw ()
	{
		{
			if (goPlayer.transform.localPosition.x != player.positionX || goPlayer.transform.localPosition.y != player.positionY) {
				goPlayer.transform.localPosition = new Vector3 (player.positionX, player.positionY);
			}
			if (goPlayer.GetComponent<Image> ().sprite != spritePlayerList [(int)player.compass * ResourceManager.SPRITE_MULTI_COMPASS + player.imageIndex]) {
				goPlayer.GetComponent<Image> ().sprite = spritePlayerList [(int)player.compass * ResourceManager.SPRITE_MULTI_COMPASS + player.imageIndex];
			}
		}
		{
			if (goBoss.transform.localPosition.x != boss.positionX || goBoss.transform.localPosition.y != boss.positionY) {
				goBoss.transform.localPosition = new Vector3 (boss.positionX, boss.positionY);
			}
			if (goBoss.gameObject.GetComponent<Image> ().sprite != spriteBossList [(int)boss.compass * ResourceManager.SPRITE_MULTI_COMPASS + boss.imageIndex]) {
				goBoss.gameObject.GetComponent<Image> ().sprite = spriteBossList [(int)boss.compass * ResourceManager.SPRITE_MULTI_COMPASS + boss.imageIndex];
			}
		}
		{
			if (goPrincess.transform.localPosition.x != princess.positionX || goPrincess.transform.localPosition.y != princess.positionY) {
				goPrincess.transform.localPosition = new Vector3 (princess.positionX, princess.positionY);
			}
		}
		{
			if (goThankMask.GetComponent<RectTransform> ().sizeDelta.x != thankMask.width || goThankMask.GetComponent<RectTransform> ().sizeDelta.y != thankMask.height) {
				goThankMask.GetComponent<RectTransform> ().sizeDelta = new Vector2 (thankMask.width, thankMask.height);
			}
		}
		{
			for (int i = 0; i < cloudList.Count; i++) {
				if (goCloudList [i].transform.localPosition.x != cloudList [i].positionX || goCloudList [i].transform.localPosition.y != cloudList [i].positionY) {
					goCloudList [i].transform.localPosition = new Vector3 (cloudList [i].positionX, cloudList [i].positionY);
				}
			}
		}
		{
			if (int.Parse (goSelectStageNum.GetComponent<Text> ().text) != selectStageIndex + 1) {
				goSelectStageNum.GetComponent<Text> ().text = (selectStageIndex + 1).ToString ();
			}
		}
	}



	public void SetDiscourse (Discourse discourse)
	{
		if (this.discourse == Discourse.None) {
			this.discourse = discourse;
			this.pattern = PATTERN_0;
			this.time = 0;
		}
	}



	private void OnTouchClick ()
	{
		switch (this.discourse) {
		case Discourse.Prologue:
			if (PlayerPrefs.GetInt (Data.RECORD_IS_TUTORIAL_FIRST_HELP) == 1) {
				if (this.pattern != PATTERN_6) {
					this.pattern = PATTERN_6;
					this.time = 0;
					this.isPause = false;
					this.player.Wait (this.player.compass);
					this.boss.Wait (this.boss.compass);
					this.princess.Wait (this.princess.compass);
					SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
				}
			}
			break;
		case Discourse.Epilogue:
			if (this.pattern == PATTERN_7) {
				this.pattern = PATTERN_10;
				this.time = 0;
			}
			break;
		}
	}



	private void OnSelectStageOpen ()
	{
		goSelectStageBack.SetActive (true);
		goSelectStageNum.SetActive (true);
		goSelectStageRight.SetActive (true);
		goSelectStageLeft.SetActive (true);
		goSelectStageOn.SetActive (false);
		isPause = true;
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
	}



	private void OnSelectStagePrev ()
	{
		selectStageIndex = (selectStageIndex + 20 - 1) % 20;
		if (PlayerPrefs.GetInt (Data.RECORD_CLEAR + selectStageIndex) == 0) {
			if (selectStageIndex >= 0 && PlayerPrefs.GetInt (Data.RECORD_CLEAR + (selectStageIndex - 1)) == 0) {
				OnSelectStagePrev ();
				return;
			}
		}
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
	}



	private void OnSelectStageNext ()
	{
		selectStageIndex = (selectStageIndex + 1) % 20;
		if (PlayerPrefs.GetInt (Data.RECORD_CLEAR + selectStageIndex) == 0) {
			if (selectStageIndex >= 0 && PlayerPrefs.GetInt (Data.RECORD_CLEAR + (selectStageIndex - 1)) == 0) {
				OnSelectStageNext ();
				return;
			}
		}
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
	}



	public void SetPlayer ()
	{
		spritePlayerList = new Dictionary<int, Sprite> (){
			{0					+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> ("Textures/player_right_0")	},
			{0					+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> ("Textures/player_right_1")	},
			{0					+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> ("Textures/player_right_2")	},
			{0					+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> ("Textures/player_left_0")	},
			{0					+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> ("Textures/player_left_1")	},
			{0					+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> ("Textures/player_left_2")	},
		};
	}

}
