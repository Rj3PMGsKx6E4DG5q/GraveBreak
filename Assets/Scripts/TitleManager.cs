using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Hakaima;


public class TitleManager : MonoBehaviour
{

	public enum State
	{
		Menu,
		Record,
		Help,
		End,
	}

	
	
	public class Catalog
	{

		public int pageNum			{ get; private set; }
		public float positionX		{ get; private set; }
		public int nowPageIndex		{ get; private set; }
		public int prePageIndex		{ get; private set; }
		public bool isMove			{ get; private set; }
		public bool isArrowRight	{ get; private set; }
		public bool isArrowLeft		{ get; private set; }
		
		private float moveTime;
		
		
		public void Init (int pageNum)
		{
			this.pageNum = pageNum;
			this.positionX = 0;
			this.nowPageIndex = 0;
			this.prePageIndex = 0;
			this.isMove = false;
			this.isArrowRight = true;
			this.isArrowLeft = false;
		}
		
		
		public void Move (float deltaTime, int frameRate)
		{
			if (this.isMove) {
				this.moveTime += deltaTime * 2.5f;
				this.positionX = -(-(this.nowPageIndex - this.prePageIndex) * this.moveTime * (this.moveTime - 2) + this.prePageIndex) * Data.SCREEN_WIDTH;
				
				if (this.moveTime >= 1) {
					this.isMove = false;
				}
			}
		}

		
		public void Next ()
		{
			if (!this.isMove) {
				if (this.nowPageIndex < this.pageNum - 1) {
					this.prePageIndex = this.nowPageIndex;
					this.nowPageIndex++;
					this.isMove = true;
					this.moveTime = 0;
					this.isArrowRight = this.nowPageIndex < this.pageNum - 1;
					this.isArrowLeft = true;
				}
			}
		}
		
		
		public void Prev ()
		{
			if (!this.isMove) {
				if (this.nowPageIndex > 0) {
					this.prePageIndex = this.nowPageIndex;
					this.nowPageIndex--;
					this.isMove = true;
					this.moveTime = 0;
					this.isArrowRight = true;
					this.isArrowLeft = this.nowPageIndex > 0;
				}
			}
		}
	}



	private class Bird
	{
		public enum State
		{
			Wait,
			Fly,
		}


		public const int IMAGE_0	= 0;
		public const int IMAGE_1	= 1;


		public State state		{ get; private set; }
		public float time		{ get; private set; }

		public float positionX	{ get; private set; }
		public float positionY	{ get; private set; }
		public float scaleX		{ get; private set; }
		public float scaleY		{ get; private set; }
		public bool visible		{ get; private set; }
		public int imageIndex	{ get; private set; }

		private float speedX;
		private float speedY;
		private float imageTime;

		private float sidePositionX;


		public void Init (float sidePositionX)
		{
			this.sidePositionX = sidePositionX;
			this.Wait ();
		}


		public void Move (float deltaTime, int frameRate)
		{
			switch (this.state) {
			case State.Wait:
				{
				}
				break;
			case State.Fly:
				{
					this.positionX += this.speedX * deltaTime * frameRate;
					this.positionY += this.speedY * deltaTime * frameRate;
					if (Math.Abs (this.positionX) >= this.sidePositionX) {
						this.Wait ();
					}

					int index = ((int)this.imageTime % 4) >> 1;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += 0.1f * deltaTime * frameRate;
				}
				break;
			}
		}


		public void Wait ()
		{
			this.state = State.Wait;
			this.time = 0;
			this.visible = false;
		}


		public void Fly ()
		{
			this.state = State.Fly;
			this.time = 0;
			this.imageIndex = 0;
			this.imageTime = 0;
			this.visible = true;

			int distance = UnityEngine.Random.value * 2 < 1 ? 1 : -1;
			this.positionX = -this.sidePositionX * distance;
			this.positionY = 0 + UnityEngine.Random.value * 600;
			this.scaleX = distance;
			this.scaleY = 1;
			this.speedX = 4 * distance;
			this.speedY = 1;
		}
	}



	public class Cover
	{
		public bool visible { get; set; }
		public Color color	{ get; private set; }

		public Cover ()
		{
			this.visible = true;
			this.color = Color.black;
		}

		public void SetAlpha (float alpha)
		{
			Color color = Color.black;
			color.a = alpha;
			this.color = color;
		}

	}


	public const int RECORD_PAGE_NUM = 3;
	public const int HELP_PAGE_NUM = 9;

	private State state;
	private float time;

	private GameObject goMenu;
	private GameObject goRecord;
	private GameObject goHelp;
	private GameObject goEnd;
	private GameObject goCover;
	private GameObject goConceal;

	private GameObject goMenuLogo;
	private GameObject goMenuButtonStart;
	private GameObject goMenuButtonContinue;
	private GameObject goMenuButtonRecord;
	private GameObject goMenuButtonHelp;
	private GameObject goMenuVolumeOn;
	private GameObject goMenuVolumeOff;
	private GameObject goMenuBird;
	private GameObject goMenuTwitter;
	private GameObject goMenuHiscore;

	private GameObject goRecordPage;
	private GameObject goRecordPoint;
	private GameObject goRecordSwipe;
	private GameObject goRecordArrowRight;
	private GameObject goRecordArrowLeft;
	private GameObject goRecordButtonBack;

	private GameObject goHelpPage;
	private GameObject goHelpPoint;
	private GameObject goHelpSwipe;
	private GameObject goHelpArrowRight;
	private GameObject goHelpArrowLeft;
	private GameObject goHelpButtonBack;

	private GameObject goCatalogPage;
	private GameObject goCatalogPoint;
	private GameObject goCatalogArrowRight;
	private GameObject goCatalogArrowLeft;

	private GameObject goCaution;
	private GameObject goCautionButtonYes;
	private GameObject goCautionButtonNo;

	private GameObject goEndDescription;
	private GameObject goEndButtonYes;
	private GameObject goEndButtonNo;


	private Catalog catalog;
	private Bird bird;
	private Cover cover;

	private float birdIndex;


	public Sprite spriteLogo;
	public Sprite spriteLogoEn;

	private List<Sprite> spriteBirdList;
	[SerializeField]
	private Sprite spriteBird0;
	[SerializeField]
	private Sprite spriteBird1;
	[SerializeField]
	private Sprite spriteDragonFly0;
	[SerializeField]
	private Sprite spriteDragonFly1;

	private static bool isCoverOnce = true;



	private void Awake ()
	{
		Init ();

		this.state = State.Menu;
		Create ();
	}



	private void Update ()
	{
		Run ();
		Draw ();
	}



	private void Init ()
	{
		string path = Application.systemLanguage == SystemLanguage.Japanese ? Data.HELP_PATH_JAPANESE : Data.HELP_PATH_ENGLISH;

		goMenu							= transform.Find ("UI/Menu").gameObject;
		goRecord						= transform.Find ("UI/Record").gameObject;
		goHelp							= Instantiate (Resources.Load<GameObject> (path));
		goHelp							.transform.SetParent (transform.Find ("UI"));
		goCaution						= transform.Find ("UI/Caution").gameObject;
		goEnd							= transform.Find ("UI/End").gameObject;
		goCover							= transform.Find ("UI/Cover").gameObject;
		goConceal						= transform.Find ("UI/Conceal").gameObject;

		goMenuLogo						= goMenu.transform.Find ("Logo").gameObject;
		goMenuButtonStart				= goMenu.transform.Find ("ButtonStart").gameObject;
		goMenuButtonContinue			= goMenu.transform.Find ("ButtonContinue").gameObject;
		goMenuButtonRecord				= goMenu.transform.Find ("ButtonRecord").gameObject;
		goMenuButtonHelp				= goMenu.transform.Find ("ButtonHelp").gameObject;
		goMenuVolumeOn					= goMenu.transform.Find ("Volume/On").gameObject;
		goMenuVolumeOff					= goMenu.transform.Find ("Volume/Off").gameObject;
		goMenuBird						= goMenu.transform.Find ("Bird").gameObject;
		goMenuTwitter					= goMenu.transform.Find ("Twitter").gameObject;
		goMenuHiscore					= goMenu.transform.Find ("Hiscore/Hiscore").gameObject;

		goRecordPage					= goRecord.transform.Find ("Page").gameObject;
		goRecordPoint					= goRecord.transform.Find ("Point").gameObject;
		goRecordSwipe					= goRecord.transform.Find ("Swipe").gameObject;
		goRecordArrowRight				= goRecord.transform.Find ("ArrowRight").gameObject;
		goRecordArrowLeft				= goRecord.transform.Find ("ArrowLeft").gameObject;
		goRecordButtonBack				= goRecord.transform.Find ("ButtonBack").gameObject;

		goHelpPage						= goHelp.transform.Find ("Page").gameObject;
		goHelpPoint						= goHelp.transform.Find ("Point").gameObject;
		goHelpSwipe						= goHelp.transform.Find ("Swipe").gameObject;
		goHelpArrowRight				= goHelp.transform.Find ("ArrowRight").gameObject;
		goHelpArrowLeft					= goHelp.transform.Find ("ArrowLeft").gameObject;
		goHelpButtonBack				= goHelp.transform.Find ("ButtonBack").gameObject;
		Destroy (goHelp.transform.Find ("Attention").gameObject);

		goCautionButtonYes				= goCaution.transform.Find ("ButtonYes").gameObject;
		goCautionButtonNo				= goCaution.transform.Find ("ButtonNo").gameObject;

		goEndDescription				= goEnd.transform.Find ("Description").gameObject;
		goEndButtonYes					= goEnd.transform.Find ("ButtonYes").gameObject;
		goEndButtonNo					= goEnd.transform.Find ("ButtonNo").gameObject;


		goMenuLogo						.GetComponent<Image> ().sprite = Language.sentence == Language.sentenceEn ? spriteLogoEn : spriteLogo;
		goMenuLogo						.GetComponent<Image> ().SetNativeSize ();
//		goMenuButtonStart				.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonStartSelectCharacter ());
		goMenuButtonContinue			.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonContinue ());
		goMenuButtonRecord				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Record));
		goMenuButtonHelp				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Help));
		goMenuVolumeOn					.GetComponent<Button> ().onClick.AddListener (() => OnVolume (true));
		goMenuVolumeOff					.GetComponent<Button> ().onClick.AddListener (() => OnVolume (false));
		goMenuTwitter					.GetComponent<Button> ().onClick.AddListener (() => OnTwitter ());
		if (MainManager.Instance.isTutorial) {
			goMenuButtonStart.transform.localPosition = goMenuButtonContinue.transform.localPosition;
			goMenuButtonContinue.SetActive (false);
		}
		goMenuHiscore					.GetComponent<Text> ().text = MainManager.Instance.scoreHigh.ToString("D7");

		goRecordButtonBack				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));
		goRecordArrowRight				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogNextPage ());
		goRecordArrowLeft				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogPrevPage ());

		goHelpButtonBack				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));
		goHelpArrowRight				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogNextPage ());
		goHelpArrowLeft					.GetComponent<Button> ().onClick.AddListener (() => OnCatalogPrevPage ());
		goRecordSwipe					.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.Drag).callback.AddListener (eventData => OnSwipe ((PointerEventData)eventData));
		goHelpSwipe						.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.Drag).callback.AddListener (eventData => OnSwipe ((PointerEventData)eventData));

		goCaution						.transform.Find ("Text").GetComponent<Text> ().text = Language.sentence [Language.START_CAUTION];
		goCautionButtonYes				.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonStart ());
		goCautionButtonNo				.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonCaution (false));

		goEndDescription				.GetComponent<Text> ().text = Language.sentence [Language.APPLICATION_QUIT];
		goEndDescription				.GetComponent<Text> ().fontSize = Language.sentence == Language.sentenceJa ? 50 : 70;
		goEndButtonYes					.GetComponent<Button> ().onClick.AddListener (() => OnEnd ());
		goEndButtonNo					.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));

		if (MainManager.Instance.isTutorial) {
			goMenuButtonStart			.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonStart ());
		} else {
			goMenuButtonStart			.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonCaution (true));
		}


		bird 	= new Bird ();
		catalog = new Catalog ();
		cover 	= new Cover ();

		SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_TITLE);
		OnVolume (PlayerPrefs.GetInt (Data.SOUND_MUTE) == 1);


		spriteBirdList = new List<Sprite> (){
			spriteDragonFly0,
			spriteDragonFly1,
		};


		goConceal.SetActive (true);
		goCover.SetActive (isCoverOnce);
		isCoverOnce = false;
	}
	
	
	
	private void Create ()
	{
		goMenu.SetActive (false);
		goRecord.SetActive (false);
		goHelp.SetActive (false);
		goCaution.SetActive (false);
		goEnd.SetActive (false);

		switch (this.state) {
		case State.Menu:
			{
				goMenu.SetActive (true);

				time = 0;
				birdIndex = 0;
				bird.Init (800);
			}
			break;
		case State.Record:
			{
				goRecord.SetActive (true);

				string text = null;
				text += Language.sentence [Language.RECORD_ENEMY_DIE_TO_TOMB] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ENEMY_DIE_TO_TOMB)) + "\n";
				text += Language.sentence [Language.RECORD_ENEMY_DIE_TO_HOLE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ENEMY_DIE_TO_HOLE)) + "\n";
				text += Language.sentence [Language.RECORD_TOMB_COLLAPSE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_TOMB_COLLAPSE)) + "\n";
				text += Language.sentence [Language.RECORD_HOLE_OPEN] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_HOLE_OPEN)) + "\n";
				text += Language.sentence [Language.RECORD_HOLE_CLOSE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_HOLE_CLOSE)) + "\n";
				text += Language.sentence [Language.RECORD_HOLE_FALL] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_HOLE_FALL)) + "\n";
				text += Language.sentence [Language.RECORD_BONUS_APPEAR] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_BONUS_APPEAR)) + "\n";
				text += Language.sentence [Language.RECORD_BONUS_GET] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_BONUS_GET)) + "\n";
				text += Language.sentence [Language.RECORD_ITEM_GET] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ITEM_GET)) + "\n";
				text += Language.sentence [Language.RECORD_DAMAGE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_DAMAGE)) + "\n";
				text += Language.sentence [Language.RECORD_ESCAPE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ESCAPE)) + "\n";
				text += Language.sentence [Language.RECORD_MAX_TOMB_COLLAPSE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_MAX_TOMB_COLLAPSE)) + "\n";
				text += Language.sentence [Language.RECORD_SCORE_ALL] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_SCORE_ALL));
				goRecordPage.transform.Find ("Page0").GetComponent<Text> ().text = text;
				if (Language.sentence == Language.sentenceEn) {
					goRecordPage.transform.Find ("Page0").GetComponent<RectTransform> ().sizeDelta = new Vector2 (925, 1000);
				}

				string text0 = null;
				string text1 = null;
				for (int i = 0; i < 20; i++) {
					TimeSpan span = TimeSpan.FromSeconds (Data.GetStageData (i).limitTime - PlayerPrefs.GetFloat (Data.RECORD_CLEAR_TIME + i));
					string record = string.Format (Language.sentence [Language.RECORD_CLEAR_TIME] + "\t{1:00}:{2:00}:{3}\n", i + 1, span.Minutes, span.Seconds, span.Milliseconds.ToString ("000").Substring (0, 2));
					if (PlayerPrefs.GetInt (Data.RECORD_CLEAR + i) == 0) {
						record = string.Format (Language.sentence [Language.RECORD_CLEAR_TIME] + "\t--:--:--\n", i + 1);
					}
					if (i < 10) {
						text0 += record;
					} else {
						text1 += record;
					}
				}
				goRecordPage.transform.Find ("Page1").GetComponent<Text> ().text = text0;
				goRecordPage.transform.Find ("Page2").GetComponent<Text> ().text = text1;

				catalog.Init (RECORD_PAGE_NUM);
				goCatalogPage = goRecordPage;
				goCatalogPoint = goRecordPoint;
				goCatalogArrowRight = goRecordArrowRight;
				goCatalogArrowLeft = goRecordArrowLeft;
			}
			break;
		case State.Help:
			{
				goHelp.SetActive (true);

				catalog.Init (HELP_PAGE_NUM);
				goCatalogPage = goHelpPage;
				goCatalogPoint = goHelpPoint;
				goCatalogArrowRight = goHelpArrowRight;
				goCatalogArrowLeft = goHelpArrowLeft;
			}
			break;
		case State.End:
			{
				goEnd.SetActive (true);
			}
			break;
		}
	}



	private void Run ()
	{
		CheckBackKey ();


		switch (this.state) {
		case State.Menu:
			{
				if (time >= 0.05f)
					cover.visible = false;

				if (time >= birdIndex * 10) {
					birdIndex++;
					bird.Fly ();
				}
				bird.Move (Time.deltaTime, Data.TARGET_FRAME_RATE);

				time += Time.deltaTime;
			}
			break;
		case State.Record:
		case State.Help:
			{
				catalog.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
			}
			break;
		}
	}



	private void Draw ()
	{
		switch (this.state) {
		case State.Menu:
			{
				if (goCover.activeSelf != cover.visible) {
					goCover.SetActive (cover.visible);
				}
				if (cover.visible) {
					if (goCover.GetComponent<Image> ().color != cover.color) {
						goCover.GetComponent<Image> ().color = cover.color;
					}
				}
				if (goMenuBird.activeSelf != bird.visible) {
					goMenuBird.SetActive (bird.visible);
				}
				if (bird.visible) {
					if (goMenuBird.transform.localPosition.x != bird.positionX || goMenuBird.transform.localPosition.y != bird.positionY) {
						goMenuBird.transform.localPosition = new Vector3 (bird.positionX, bird.positionY);
					}
					if (goMenuBird.transform.localScale.x != bird.scaleX || goMenuBird.transform.localScale.y != bird.scaleY) {
						goMenuBird.transform.localScale = new Vector3 (bird.scaleX, bird.scaleY);
					}
					if (goMenuBird.GetComponent<Image> ().sprite != spriteBirdList [bird.imageIndex]) {
						goMenuBird.GetComponent<Image> ().sprite = spriteBirdList [bird.imageIndex];
					}
				}
			}
			break;
		case State.Record:
		case State.Help:
			{
				if (goCatalogPage.transform.localPosition.x != catalog.positionX) {
					goCatalogPage.transform.localPosition = new Vector3 (catalog.positionX, goCatalogPage.transform.localPosition.y);
				}
				if (goCatalogArrowRight.activeSelf != catalog.isArrowRight) {
					goCatalogArrowRight.SetActive (catalog.isArrowRight);
				}
				if (goCatalogArrowLeft.activeSelf != catalog.isArrowLeft) {
					goCatalogArrowLeft.SetActive (catalog.isArrowLeft);
				}
				goCatalogPoint.transform.Find ("PointNow").localPosition = goCatalogPoint.transform.Find ("Point" + catalog.nowPageIndex).localPosition;
			}
			break;
		}
	}
	
	
	
	private void OnMenuButtonStart ()
	{
		MainManager.Instance.StoryPrologue ();
		MainManager.Instance.RecordSave ();
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
	}

	
	private void OnMenuButtonCaution (bool active)
	{
		goCaution.SetActive (active);
		SoundManager.Instance.PlaySe (active ? SoundManager.SeName.SE_OK : SoundManager.SeName.SE_CANCEL);
	}



	private void OnMenuButtonContinue ()
	{
		MainManager.Instance.CurrentStage (MainManager.Instance.life, MainManager.Instance.weapon);
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
	}



	private void OnSwipe (PointerEventData eventData)
	{
		float ratio = 1.0f * Screen.width / Data.SCREEN_WIDTH;

		Vector2 delta = eventData.delta / ratio;
		if (delta.x < -5) {
			OnCatalogNextPage ();
		} else if (delta.x > 5) {
			OnCatalogPrevPage ();
		}
	}



	private void OnCatalogNextPage ()
	{
		if (!catalog.isMove) {
			catalog.Next ();
			if (catalog.isMove)
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_MOVE);
		}
	}



	private void OnCatalogPrevPage ()
	{
		if (!catalog.isMove) {
			catalog.Prev ();
			if (catalog.isMove)
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_MOVE);
		}
	}



	private void OnButton (State state, bool ok = true)
	{
		this.state = state;
		Create ();
		SoundManager.Instance.PlaySe (ok ? SoundManager.SeName.SE_OK : SoundManager.SeName.SE_CANCEL);
	}



	private void OnVolume (bool isMute)
	{
		SoundManager.Instance.SetMute (isMute);
		goMenuVolumeOn.SetActive (!isMute);
		goMenuVolumeOff.SetActive (isMute);
		PlayerPrefs.SetInt (Data.SOUND_MUTE, isMute ? 1 : 0);
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



	private void OnEnd ()
	{
		Application.Quit ();
	}

	

	private void CheckBackKey ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			switch (this.state) {
			case State.Menu:
				if (goCaution.activeSelf) {
					OnMenuButtonCaution (false);
				} else {
					OnButton (State.End);
				}
				break;
			default:
				OnButton (State.Menu, false);
				break;
			}
		}
	}

}
