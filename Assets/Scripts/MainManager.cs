using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GoogleMobileAds.Api;
using Hakaima;


public class MainManager : MonoBehaviour
{

	private static MainManager instance;
	public static MainManager Instance {
		get {
			instance = instance ?? GameObject.FindObjectOfType<MainManager> ();
			return instance;
		}
	}


	
	private enum State
	{
		Title,
		Game,
		StoryPrologue,
		StoryEpilogue,
	}



	public const int START_LIFE = 5;
	public const int START_WEAPON = 0;
	public const int CONTINUE_LIFE = 2;


	private State state;
	private float time;

	public bool isTutorial			{ get; private set; }
	public int stage				{ get; private set; }
	public int score				{ get; set; }
	public int scoreHigh			{ get; set; }

	public int life {
		get {
			return PlayerPrefs.GetInt (Data.MY_LIFE);
		}
		set {
			if (value > 99)
				value = 99;
			PlayerPrefs.SetInt (Data.MY_LIFE, value);
		}
	}

	public int weapon {
		get {
			return PlayerPrefs.GetInt (Data.MY_WEAPON);
		}
		set {
			// For Debug.
			//if (PlayerPrefs.HasKey (Data.MY_WEAPON)) {
			//	PlayerPrefs.DeleteKey (Data.MY_WEAPON);
			//	value = 15;
			//}
			if (value > 99)
				value = 99;
			PlayerPrefs.SetInt (Data.MY_WEAPON, value);
		}
	}

	public BannerView bannerView		{ get; private set; }
	public InterstitialAd interstitial	{ get; private set; }

	public bool isDebug = false;
	public int debugStage = 1;

	[HideInInspector]
	public int selectCharacter;
	// 起動時に情報を出すための番号.
	[HideInInspector]
	public int informationNumber;
	[HideInInspector]
	public bool isInterstitialClose;

	private void InitBanner()
	{
		if (this.bannerView != null)
			this.bannerView.Destroy ();
		this.bannerView = new BannerView (Data.BANNER_ID, AdSize.Banner, AdPosition.Bottom);
		this.bannerView.LoadAd (new AdRequest.Builder ().Build ());
		this.bannerView.Hide ();
	}


	private void Start ()
	{
		Application.targetFrameRate = Data.TARGET_FRAME_RATE;
		Language.sentence = Application.systemLanguage == SystemLanguage.Japanese ? Language.sentenceJa : Language.sentenceEn;

//		enabled = false;
//		yield return StartCoroutine (RequestData ());
//		enabled = true;


		this.interstitial = new InterstitialAd (Data.INTERSTITIAL_ID);
		this.interstitial.LoadAd (new AdRequest.Builder ().Build ());

		this.state = State.Title;
		this.time = 0;

		this.SetResolution (Data.SCREEN_RESOLUTION);
		this.RecordLoad ();

		if (isDebug) {
			stage = debugStage - 1;
		}

		if (Screen.height == Data.SCREEN_IPHONE_X_HIEGHT) {
			Camera.main.orthographicSize = Data.SCREEN_IPHONE_X_WIDTH;
		}
	}



	private void Update ()
	{
		switch (state) {
		case State.Title:
			{
				if (time == 0) {
					foreach (Transform trans in transform) {
						Destroy (trans.gameObject);
					}
					GC.Collect ();
					Resources.UnloadUnusedAssets ();
					SoundManager.Instance.StopBgm ();
					SoundManager.Instance.StopSe ();

					InitBanner ();

					GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/TitleManager"));
					go.transform.SetParent (transform);

					List<Canvas> canvasList = new List<Canvas> (GameObject.FindObjectsOfType<Canvas> ());
					canvasList.ForEach (obj => obj.worldCamera = Camera.main);
				}
			}
			break;
		case State.Game:
			{
				if (time == 0) {
					foreach (Transform trans in transform) {
						Destroy (trans.gameObject);
					}
					GC.Collect ();
					Resources.UnloadUnusedAssets ();
					if (!this.isTutorial)
						SoundManager.Instance.StopBgm ();

					InitBanner ();

					GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/GameManager"));
					go.transform.SetParent (transform);
				
					List<Canvas> canvasList = new List<Canvas> (GameObject.FindObjectsOfType<Canvas> ());
					canvasList.ForEach (obj => obj.worldCamera = Camera.main);
				}
			}
			break;
		case State.StoryPrologue:
			{
				if (time == 0) {
					foreach (Transform trans in transform) {
						Destroy (trans.gameObject);
					}
					GC.Collect ();
					Resources.UnloadUnusedAssets ();
					SoundManager.Instance.StopBgm ();

					GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/StoryManager"));
					go.transform.SetParent (transform);
					go.GetComponent<StoryManager> ().SetDiscourse (StoryManager.Discourse.Prologue);
				
					List<Canvas> canvasList = new List<Canvas> (GameObject.FindObjectsOfType<Canvas> ());
					canvasList.ForEach (obj => obj.worldCamera = Camera.main);
				}
			}
			break;
		case State.StoryEpilogue:
			{
				if (time == 0) {
					foreach (Transform trans in transform) {
						Destroy (trans.gameObject);
					}
					GC.Collect ();
					Resources.UnloadUnusedAssets ();
					SoundManager.Instance.StopBgm ();

					GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/StoryManager"));
					go.transform.SetParent (transform);
					go.GetComponent<StoryManager> ().SetDiscourse (StoryManager.Discourse.Epilogue);
				
					List<Canvas> canvasList = new List<Canvas> (GameObject.FindObjectsOfType<Canvas> ());
					canvasList.ForEach (obj => obj.worldCamera = Camera.main);
				}
			}
			break;
		}
	
		time += Time.deltaTime;
	}



	private void OnApplicationPause (bool pauseStatus)
	{
	}



	public void StoryPrologue ()
	{
		this.isTutorial = true;
		this.state = State.StoryPrologue;
		this.time = 0;
	}



	public void StoryEpilogue ()
	{
		this.isTutorial = true;
		this.state = State.StoryEpilogue;
		this.time = 0;
	}



	public void TutorialStage ()
	{
		this.stage = 0;
		this.score = 0;
		if (this.life < START_LIFE)
			this.life = START_LIFE;
		this.weapon = START_WEAPON;

		this.state = State.Game;
		this.time = 0;
	}



	public void CurrentStage (int life, int weapon)
	{
		this.life = life;
		this.weapon = weapon;

		this.state = State.Game;
		this.time = 0;
	}



	public void NextStage (int life, int weapon)
	{
		if (this.isTutorial) {
			this.isTutorial = false;
			this.stage = 0;
		} else {
			this.stage = (stage + 1) % Data.stageDataList.Count;
		}
		this.life = life;
		this.weapon = weapon;

		this.state = State.Game;
		this.time = 0;
	}
	
	
	
	public void PreStage ()
	{
		this.stage = (stage + Data.stageDataList.Count - 1) % Data.stageDataList.Count;

		this.state = State.Game;
		this.time = 0;
	}



	public void SelectStage (int stage, int life, int weapon)
	{
		this.stage = stage;
		this.score = 0;
		this.life = life;
		this.weapon = weapon;
		
		this.state = State.Game;
		this.time = 0;

		if (this.stage >= 1)
			this.isTutorial = false;
	}



	public void Title ()
	{
		this.state = State.Title;
		this.time = 0;
	}



	public void RecordSave ()
	{
		PlayerPrefs.SetInt (Data.RECORD_IS_TUTORIAL, this.isTutorial ? 1 : 0);
		PlayerPrefs.SetInt (Data.RECORD_STAGE,		 this.stage);
		PlayerPrefs.SetInt (Data.RECORD_SCORE,		 this.score);
		PlayerPrefs.SetInt (Data.RECORD_SCORE_HIGH,	 this.scoreHigh);
	}



	public void RecordLoad ()
	{
		isTutorial = true;
		if (PlayerPrefs.HasKey (Data.RECORD_IS_TUTORIAL)) {
			isTutorial = PlayerPrefs.GetInt (Data.RECORD_IS_TUTORIAL) == 1;
		}
		if (PlayerPrefs.HasKey (Data.RECORD_STAGE)) {
			stage = PlayerPrefs.GetInt (Data.RECORD_STAGE);
		}
		if (PlayerPrefs.HasKey (Data.RECORD_SCORE)) {
			score = PlayerPrefs.GetInt (Data.RECORD_SCORE);
		}

		if (!PlayerPrefs.HasKey (Data.RECORD_SCORE_HIGH)) {
			PlayerPrefs.SetInt (Data.RECORD_SCORE_HIGH, 10000);
		} else if (PlayerPrefs.GetInt (Data.RECORD_SCORE_HIGH) < 10000) {
			PlayerPrefs.SetInt (Data.RECORD_SCORE_HIGH, 10000);
		}
		scoreHigh = PlayerPrefs.GetInt (Data.RECORD_SCORE_HIGH);
	}



	private void SetResolution (float resolution)
	{
		float screenRate = resolution / Screen.height;
		if (screenRate > 1)
			screenRate = 1;
		int width = (int)(Screen.width * screenRate);
		int height = (int)(Screen.height * screenRate);
		Screen.SetResolution (width, height, true, 15);
	}


	public void ShowInterstitial (Action action)
	{
		if (interstitial.IsLoaded ()) {
			interstitial.OnAdClosed += (sender, e) => {
				interstitial.Destroy ();
				interstitial = new InterstitialAd (Data.INTERSTITIAL_ID);
				interstitial.LoadAd (new AdRequest.Builder ().Build ());

				action ();
			};
			interstitial.Show ();
		}
	}


	public void ShowInterstitialNoMovie ()
	{
		// 広告を初期化する
		InterstitialAd interstitial = new InterstitialAd(Data.INTERSTITIAL_ID); 
		AdRequest.Builder builder = new AdRequest.Builder();
		AdRequest request = builder.Build();

		// 広告が表示可能になったときのコールバック
		interstitial.OnAdLoaded += (handler, EventArgs) =>
		{
			// 広告を表示する
			interstitial.Show();
		};

		// 広告が閉じられたときのコールバック
		interstitial.OnAdClosed += (handler, EventArgs) =>
		{
			// 後処理
			interstitial.Destroy();
			isInterstitialClose = true;
		};

		// 広告がエラーになったときのコールバック
		interstitial.OnAdFailedToLoad += (handler, EventArgs) =>
		{
			// エラー処理
			interstitial.Destroy();
		};

		// インタースティシャル広告のロード
		interstitial.LoadAd(request);
	}

	
	public bool IsWeaponCharacter (int num)
	{
		switch (num) {
		case 2:
		case 3:
		case 4:
			return true;
		}
		return false;
	}


	public int GetWeaponItemPercent ()
	{
		return 10;
	}


	public void AddWeapon ()
	{
		weapon += 10;
	}


	private static IEnumerator RequestData ()
	{
		WWW www;
		string text;
		string dataText = null;
		
		
		www = new WWW ("https://drive.google.com/uc?id=0BwLM9OUPdvBVMmhPUEZlT1RuZkk");
		while (!www.isDone) {
			yield return null;
		}
		text = www.text;
		www.Dispose ();
		www = null;
		{
			Data.enemyDataList = new Dictionary<int, Data.EnemyData> ();
			List<List<string>> list = new List<string> (text.Split ('\n')).Select (obj => new List<string> (obj.Split (',' ))).ToList ();
			{
				dataText += "\tpublic static Dictionary<int, EnemyData> enemyDataList = new Dictionary<int, EnemyData> (){\n";
				foreach (Hakaima.Enemy.Type type in Enum.GetValues (typeof(Hakaima.Enemy.Type))) {
					switch (type) {
					case Hakaima.Enemy.Type.Person:
					case Hakaima.Enemy.Type.Ghost:
					case Hakaima.Enemy.Type.Soul:
					case Hakaima.Enemy.Type.Skeleton:
					case Hakaima.Enemy.Type.Mummy:
					case Hakaima.Enemy.Type.Shadowman:
					case Hakaima.Enemy.Type.Golem:
					case Hakaima.Enemy.Type.Goblin:
					case Hakaima.Enemy.Type.Parasol:
					case Hakaima.Enemy.Type.Kappa:
					case Hakaima.Enemy.Type.Tengu:
						Data.enemyDataList [(int)type]				= new Data.EnemyData ();
						Data.enemyDataList [(int)type].score		= int.Parse (list [(int)type] [1]);
						Data.enemyDataList [(int)type].speed		= float.Parse (list [(int)type] [2]);
						Data.enemyDataList [(int)type].angrySpeed	= float.Parse (list [(int)type] [3]);
						Data.enemyDataList [(int)type].angryTime	= float.Parse (list [(int)type] [4]);
						Data.enemyDataList [(int)type].fallTime		= float.Parse (list [(int)type] [5]);
						Data.enemyDataList [(int)type].isFly		= int.Parse (list [(int)type] [6]) != 0;
						Data.enemyDataList [(int)type].isAvoidHole	= int.Parse (list [(int)type] [8]) != 0;
						Data.enemyDataList [(int)type].patternIndex	= int.Parse (list [(int)type] [7]);
						Data.enemyDataList [(int)type].lifeCount	= int.Parse (list [(int)type] [9]);


						dataText += string.Format ("\t\t{{(int)Enemy.Type.{0}", type) + ",\n";
						dataText += "\t\t\tnew EnemyData {\n";
						dataText += string.Format ("\t\t\t\tscore\t\t\t= {0},\n",		Data.enemyDataList [(int)type].score);
						dataText += string.Format ("\t\t\t\tspeed\t\t\t= {0}f,\n",		Data.enemyDataList [(int)type].speed);
						dataText += string.Format ("\t\t\t\tangrySpeed\t\t= {0}f,\n",	Data.enemyDataList [(int)type].angrySpeed);
						dataText += string.Format ("\t\t\t\tangryTime\t\t= {0},\n",		Data.enemyDataList [(int)type].angryTime);
						dataText += string.Format ("\t\t\t\tfallTime\t\t= {0},\n",		Data.enemyDataList [(int)type].fallTime);
						dataText += string.Format ("\t\t\t\tisFly\t\t\t= {0},\n",		Data.enemyDataList [(int)type].isFly.ToString ().ToLower ());
						dataText += string.Format ("\t\t\t\tisAvoidHole\t\t= {0},\n",	Data.enemyDataList [(int)type].isAvoidHole.ToString ().ToLower ());
						dataText += string.Format ("\t\t\t\tpatternIndex\t= {0},\n",	Data.enemyDataList [(int)type].patternIndex);
						dataText += string.Format ("\t\t\t\tlifeCount\t\t= {0},\n",		Data.enemyDataList [(int)type].lifeCount);
						dataText += "\t\t\t}\n";
						dataText += "\t\t},\n";
						break;
					}
				}
				dataText += "\t};";
			}
		}
		
		
		www = new WWW ("https://drive.google.com/uc?id=0BwLM9OUPdvBVMk1Ybjltam9IMW8");
		while (!www.isDone) {
			yield return null;
		}
		text = www.text;
		www.Dispose ();
		www = null;
		{
			Data.enemyPatternDataList = new Dictionary<int, Data.EnemyPatternData> ();
			List<List<string>> list = new List<string> (text.Split ('\n')).Select (obj => new List<string> (obj.Split (','))).ToList ();
			{
				dataText += "\n\n\n";
				dataText += "\tpublic static Dictionary<int, EnemyPatternData> enemyPatternDataList = new Dictionary<int, EnemyPatternData> (){\n";
				try {
					for (int i = 0; i < 20; i++) {
						Data.enemyPatternDataList [i]				= new Data.EnemyPatternData ();
						Data.enemyPatternDataList [i].rateWait		= (int)(float.Parse (list [i + 1] [1]) * 100);
						Data.enemyPatternDataList [i].rateStraight	= (int)(float.Parse (list [i + 1] [2]) * 100);
						Data.enemyPatternDataList [i].rateTurnRight	= (int)(float.Parse (list [i + 1] [4]) * 100);
						Data.enemyPatternDataList [i].rateTurnLeft	= (int)(float.Parse (list [i + 1] [3]) * 100);
						Data.enemyPatternDataList [i].rateTurnBack	= (int)(float.Parse (list [i + 1] [5]) * 100);
						Data.enemyPatternDataList [i].rateFollow	= (int)(float.Parse (list [i + 1] [7]) * 100);


						dataText += string.Format ("\t\t{{{0}", i) + ",\n";
						dataText += "\t\t\tnew EnemyPatternData {\n";
						dataText += string.Format ("\t\t\t\trateWait\t\t\t= {0},\n",		Data.enemyPatternDataList [i].rateWait);
						dataText += string.Format ("\t\t\t\trateStraight\t\t= {0},\n",		Data.enemyPatternDataList [i].rateStraight);
						dataText += string.Format ("\t\t\t\trateTurnRight\t\t= {0},\n",		Data.enemyPatternDataList [i].rateTurnRight);
						dataText += string.Format ("\t\t\t\trateTurnLeft\t\t= {0},\n",		Data.enemyPatternDataList [i].rateTurnLeft);
						dataText += string.Format ("\t\t\t\trateTurnBack\t\t= {0},\n",		Data.enemyPatternDataList [i].rateTurnBack);
						dataText += string.Format ("\t\t\t\trateFollow\t\t\t= {0},\n",		Data.enemyPatternDataList [i].rateFollow);
						dataText += "\t\t\t}\n";
						dataText += "\t\t},\n";
					}
				} catch (Exception e) {
					Debug.Log (e.Message);
				}
				Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL]				= new Data.EnemyPatternData ();
				Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateWait		= 20;
				Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateStraight	= 30;
				Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateTurnRight	= 20;
				Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateTurnLeft	= 20;
				Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateTurnBack	= 10;
				Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateFollow	= 0;


				dataText += string.Format ("\t\t{{{0}", Data.ENEMY_PATTERN_NORMAL) + ",\n";
				dataText += "\t\t\tnew EnemyPatternData {\n";
				dataText += string.Format ("\t\t\t\trateWait\t\t\t= {0},\n",		Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateWait);
				dataText += string.Format ("\t\t\t\trateStraight\t\t= {0},\n",		Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateStraight);
				dataText += string.Format ("\t\t\t\trateTurnRight\t\t= {0},\n",		Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateTurnRight);
				dataText += string.Format ("\t\t\t\trateTurnLeft\t\t= {0},\n",		Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateTurnLeft);
				dataText += string.Format ("\t\t\t\trateTurnBack\t\t= {0},\n",		Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateTurnBack);
				dataText += string.Format ("\t\t\t\trateFollow\t\t\t= {0},\n",		Data.enemyPatternDataList [Data.ENEMY_PATTERN_NORMAL].rateFollow);
				dataText += "\t\t\t}\n";
				dataText += "\t\t},\n";
				dataText += "\t};";
			}
		}
		
		
		www = new WWW ("https://drive.google.com/uc?id=0BwLM9OUPdvBVa1RzajRaVHdYMmc");
		while (!www.isDone) {
			yield return null;
		}
		text = www.text;
		www.Dispose ();
		www = null;
		{
			List<List<string>> list = new List<string> (text.Split ('\n')).Select (obj => new List<string> (obj.Split (','))).ToList ();
			{
				Data.terrainDataList = new Dictionary<int, Data.TerrainData> ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.None]					= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Soil]					= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Grass]					= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Muddy]					= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Pavement]				= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Ice]					= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.River]					= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Bridge]					= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeVertical]			= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeHorizontal]		= new Data.TerrainData ();
				Data.terrainDataList [(int)Hakaima.Terrain.Type.None]					.isThrough	= true;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Soil]					.isThrough	= int.Parse (list [1] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Grass]					.isThrough	= int.Parse (list [5] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Muddy]					.isThrough	= int.Parse (list [6] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Pavement]				.isThrough	= int.Parse (list [2] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Ice]					.isThrough	= int.Parse (list [7] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.River]					.isThrough	= int.Parse (list [3] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Bridge]					.isThrough	= int.Parse (list [4] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeVertical]			.isThrough	= int.Parse (list [4] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeHorizontal]		.isThrough	= int.Parse (list [4] [1]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.None]					.isDigging			= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Soil]					.isDigging			= int.Parse (list [1] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Grass]					.isDigging			= int.Parse (list [5] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Muddy]					.isDigging			= int.Parse (list [6] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Pavement]				.isDigging			= int.Parse (list [2] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Ice]					.isDigging			= int.Parse (list [7] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.River]					.isDigging			= int.Parse (list [3] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Bridge]					.isDigging			= int.Parse (list [4] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeVertical]			.isDigging			= int.Parse (list [4] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeHorizontal]		.isDigging			= int.Parse (list [4] [4]) != 0;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.None]					.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Soil]					.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Grass]					.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Muddy]					.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Pavement]				.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Ice]					.isSlip				= true;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.River]					.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.Bridge]					.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeVertical]			.isSlip				= false;
				Data.terrainDataList [(int)Hakaima.Terrain.Type.BridgeHorizontal]		.isSlip				= false;


				dataText += "\n\n\n";
				dataText += "\tpublic static Dictionary<int, TerrainData> terrainDataList = new Dictionary<int, TerrainData> (){\n";
				foreach (Hakaima.Terrain.Type type in Enum.GetValues (typeof(Hakaima.Terrain.Type))) {
					dataText += string.Format ("\t\t{{(int)Terrain.Type.{0}", type) + ",\n";
					dataText += "\t\t\tnew TerrainData {\n";
					dataText += string.Format ("\t\t\t\tisThrough\t= {0},\n",			Data.terrainDataList [(int)type].isThrough.ToString ().ToLower ());
					dataText += string.Format ("\t\t\t\tisDigging\t\t= {0},\n",			Data.terrainDataList [(int)type].isDigging.ToString ().ToLower ());
					dataText += string.Format ("\t\t\t\tisSlip\t\t\t= {0},\n",			Data.terrainDataList [(int)type].isSlip.ToString ().ToLower ());
					dataText += "\t\t\t}\n";
					dataText += "\t\t},\n";
				}
				dataText += "\t};";
			}
			{
				Data.obstacleDataList = new Dictionary<int, Data.ObstacleData> ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.None]					= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Tree]					= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stone]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Tomb]					= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombCollapse]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombCollapseEnd]		= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombPiece]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombPieceEnd]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.CartRight]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.CartLeft]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Well]					= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Bale]					= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Bathtub]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.BathtubCollapse]		= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.BathtubCollapseEnd]	= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stockpile]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.StockpileCollapse]	= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.StockpileCollapseEnd]	= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Signboard]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Tower]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.FallenTree]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stump]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Bucket]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Lantern]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.StupaFence]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stupa]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.LargeTreeRight]		= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.LargeTreeLeft]		= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Rubble]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.RubbleOff]			= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Picket]				= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.FallTombPiece]		= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.FallTombPieceEnd]		= new Data.ObstacleData ();
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.None]					.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Tree]					.isThrough	= int.Parse (list [12] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stone]				.isThrough	= int.Parse (list [11] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Tomb]					.isThrough	= int.Parse (list [9] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombCollapse]			.isThrough	= int.Parse (list [9] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombCollapseEnd]		.isThrough	= int.Parse (list [10] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombPiece]			.isThrough	= int.Parse (list [24] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.TombPieceEnd]			.isThrough	= int.Parse (list [24] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.CartRight]			.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.CartLeft]				.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Well]					.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Bale]					.isThrough	= false;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Bathtub]				.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.BathtubCollapse]		.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.BathtubCollapseEnd]	.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stockpile]			.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.StockpileCollapse]	.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.StockpileCollapseEnd]	.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Signboard]			.isThrough	= int.Parse (list [13] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Tower]				.isThrough	= int.Parse (list [25] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.FallenTree]			.isThrough	= int.Parse (list [21] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stump]				.isThrough	= int.Parse (list [20] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Bucket]				.isThrough	= int.Parse (list [14] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Lantern]				.isThrough	= int.Parse (list [18] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.StupaFence]			.isThrough	= int.Parse (list [16] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Stupa]				.isThrough	= int.Parse (list [15] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.LargeTreeRight]		.isThrough	= int.Parse (list [22] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.LargeTreeLeft]		.isThrough	= int.Parse (list [22] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Rubble]				.isThrough	= int.Parse (list [19] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.RubbleOff]			.isThrough	= int.Parse (list [19] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.Picket]				.isThrough	= int.Parse (list [17] [1]) != 0;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.FallTombPiece]		.isThrough	= true;
				Data.obstacleDataList [(int)Hakaima.Obstacle.Type.FallTombPieceEnd]		.isThrough	= true;


				dataText += "\n\n\n";
				dataText += "\tpublic static Dictionary<int, ObstacleData> obstacleDataList = new Dictionary<int, ObstacleData> (){\n";
				foreach (Hakaima.Obstacle.Type type in Enum.GetValues (typeof(Hakaima.Obstacle.Type))) {
					dataText += string.Format ("\t\t{{(int)Obstacle.Type.{0}", type) + ",\n";
					dataText += "\t\t\tnew ObstacleData {\n";
					dataText += string.Format ("\t\t\t\tisThrough\t= {0},\n",			Data.obstacleDataList [(int)type].isThrough.ToString ().ToLower ());
					dataText += "\t\t\t}\n";
					dataText += "\t\t},\n";
				}
				dataText += "\t};";
			}
		}
		
		
		www = new WWW ("https://drive.google.com/uc?id=0BwLM9OUPdvBVaTZ3Smx4Mi12WWc");
		while (!www.isDone) {
			yield return null;
		}
		text = www.text;
		www.Dispose ();
		www = null;
		{
			Data.stageDataList = new List<Data.StageData> ();
			List<List<string>> list = new List<string> (text.Split ('\n')).Select (obj => new List<string> (obj.Split (','))).ToList ();
			int j;
			{
				j = 1;
				int num = int.Parse (list [j] [0]);
				for (int i = 0; i < num; i++) {
					Data.stageDataList.Add (new Data.StageData ());
				}
				j++;
			}
			{
				j += 2;
				foreach (Data.StageData stageData in Data.stageDataList) {
					stageData.limitTime	= float.Parse (list [j] [1]);
					stageData.darkness	= float.Parse (list [j] [2]);
					stageData.mist		= float.Parse (list [j] [3]);
					stageData.sound		= int.Parse (list [j] [4]);
					j++;
				}
			}
			{
				j += 2;
				foreach (Data.StageData stageData in Data.stageDataList) {
					stageData.terrainTypeList = new Dictionary<Hakaima.Terrain.Type, int> ();
					stageData.terrainTypeList [Hakaima.Terrain.Type.Soil]		= int.Parse (list [j] [1]);
					stageData.terrainTypeList [Hakaima.Terrain.Type.Grass]		= int.Parse (list [j] [5]);
					stageData.terrainTypeList [Hakaima.Terrain.Type.Muddy]		= int.Parse (list [j] [6]);
					stageData.terrainTypeList [Hakaima.Terrain.Type.Pavement]	= int.Parse (list [j] [2]);
					stageData.terrainTypeList [Hakaima.Terrain.Type.Ice]		= int.Parse (list [j] [8]);
					stageData.terrainTypeList [Hakaima.Terrain.Type.River]		= int.Parse (list [j] [3]);
					stageData.terrainTypeList [Hakaima.Terrain.Type.Bridge]		= int.Parse (list [j] [4]);
					stageData.holeOpen											= int.Parse (list [j] [7]);
					j++;
				}
			}
			{
				j += 2;
				foreach (Data.StageData stageData in Data.stageDataList) {
					stageData.obstacleTypeList = new Dictionary<Hakaima.Obstacle.Type, int> ();
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Tree]					= int.Parse (list [j] [9]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stone]				= int.Parse (list [j] [8]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Tomb]					= int.Parse (list [j] [7]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.CartRight]			= int.Parse (list [j] [1]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Well]					= int.Parse (list [j] [3]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Bale]					= int.Parse (list [j] [4]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Bathtub]				= int.Parse (list [j] [6]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stockpile]			= int.Parse (list [j] [5]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Signboard]			= int.Parse (list [j] [10]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Tower]				= int.Parse (list [j] [21]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.FallenTree]			= int.Parse (list [j] [18]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stump]				= int.Parse (list [j] [17]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Bucket]				= int.Parse (list [j] [11]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Lantern]				= int.Parse (list [j] [15]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.StupaFence]			= int.Parse (list [j] [13]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Stupa]				= 0;
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.LargeTreeRight]		= int.Parse (list [j] [19]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Rubble]				= int.Parse (list [j] [16]);
					stageData.obstacleTypeList [Hakaima.Obstacle.Type.Picket]				= int.Parse (list [j] [14]);
					j++;
				}
			}
			{
				j += 2;
				foreach (Data.StageData stageData in Data.stageDataList) {
					stageData.enemyTypeList = new Dictionary<Hakaima.Enemy.Type, int> ();
					stageData.enemyTypeList [Hakaima.Enemy.Type.Person]		= int.Parse (list [j] [1]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Ghost]		= int.Parse (list [j] [2]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Soul]		= int.Parse (list [j] [3]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Skeleton]	= int.Parse (list [j] [4]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Mummy]		= int.Parse (list [j] [5]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Shadowman]	= int.Parse (list [j] [6]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Golem]		= int.Parse (list [j] [7]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Goblin]		= int.Parse (list [j] [8]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Parasol]	= int.Parse (list [j] [9]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Kappa]		= int.Parse (list [j] [10]);
					stageData.enemyTypeList [Hakaima.Enemy.Type.Tengu]		= 0;
					j++;
				}
				Data.stageDataList [Data.stageDataList.Count - 1].enemyTypeList [Hakaima.Enemy.Type.Tengu] = 1;
			}
			{
				j += 2;
				foreach (Data.StageData stageData in Data.stageDataList) {
					stageData.itemTypeList = new Dictionary<Hakaima.Item.Type, int> ();
					stageData.itemTypeList [Hakaima.Item.Type.Sandal]	= int.Parse (list [j] [1]);
					stageData.itemTypeList [Hakaima.Item.Type.Hoe]		= int.Parse (list [j] [2]);
					stageData.itemTypeList [Hakaima.Item.Type.Stone]	= int.Parse (list [j] [4]);
					stageData.itemTypeList [Hakaima.Item.Type.Amulet]	= int.Parse (list [j] [5]);
					stageData.itemTypeList [Hakaima.Item.Type.Parasol]	= int.Parse (list [j] [3]);
					j++;
				}
			}


			dataText += "\n\n\n";
			dataText += "\tpublic static List<StageData> stageDataList = new List<StageData> (){\n";
			for (int i = 0; i < Data.stageDataList.Count; i++) {
				Data.StageData stageData = Data.stageDataList [i];
				dataText += "\t\tnew StageData {\n";
				dataText += string.Format ("\t\t\tlimitTime = {0}f,\n", stageData.limitTime);
				dataText += string.Format ("\t\t\tdarkness = {0}f,\n", stageData.darkness);
				dataText += string.Format ("\t\t\tmist = {0}f,\n", stageData.mist);
				dataText += string.Format ("\t\t\tsound = {0},\n", stageData.sound);
				dataText += "\t\t\tterrainTypeList = new Dictionary<Terrain.Type, int> (){\n";
				foreach (Hakaima.Terrain.Type type in Enum.GetValues (typeof(Hakaima.Terrain.Type))) {
					switch (type){
					case Hakaima.Terrain.Type.Soil:
					case Hakaima.Terrain.Type.Grass:
					case Hakaima.Terrain.Type.Muddy:
					case Hakaima.Terrain.Type.Pavement:
					case Hakaima.Terrain.Type.Ice:
					case Hakaima.Terrain.Type.River:
					case Hakaima.Terrain.Type.Bridge:
						dataText += string.Format ("\t\t\t\t{{Terrain.Type.{0},\t\t{1}}},\n", type, stageData.terrainTypeList [type]);
						break;
					}
				}
				dataText += "\t\t\t},\n";
				dataText += "\t\t\tobstacleTypeList = new Dictionary<Obstacle.Type, int> (){\n";
				foreach (Hakaima.Obstacle.Type type in Enum.GetValues (typeof(Hakaima.Obstacle.Type))) {
					switch (type){
					case Hakaima.Obstacle.Type.Tree:
					case Hakaima.Obstacle.Type.Stone:
					case Hakaima.Obstacle.Type.Tomb:
					case Hakaima.Obstacle.Type.CartRight:
					case Hakaima.Obstacle.Type.Well:
					case Hakaima.Obstacle.Type.Bale:
					case Hakaima.Obstacle.Type.Bathtub:
					case Hakaima.Obstacle.Type.Stockpile:
					case Hakaima.Obstacle.Type.Signboard:
					case Hakaima.Obstacle.Type.Tower:
					case Hakaima.Obstacle.Type.FallenTree:
					case Hakaima.Obstacle.Type.Stump:
					case Hakaima.Obstacle.Type.Bucket:
					case Hakaima.Obstacle.Type.Lantern:
					case Hakaima.Obstacle.Type.StupaFence:
					case Hakaima.Obstacle.Type.Stupa:
					case Hakaima.Obstacle.Type.LargeTreeRight:
					case Hakaima.Obstacle.Type.Rubble:
					case Hakaima.Obstacle.Type.Picket:
						dataText += string.Format ("\t\t\t\t{{Obstacle.Type.{0},\t\t{1}}},\n", type, stageData.obstacleTypeList [type]);
						break;
					}
				}
				dataText += "\t\t\t},\n";
				dataText += "\t\t\tenemyTypeList = new Dictionary<Enemy.Type, int> (){\n";
				foreach (Hakaima.Enemy.Type type in Enum.GetValues (typeof(Hakaima.Enemy.Type))) {
					switch (type){
					case Hakaima.Enemy.Type.Person:
					case Hakaima.Enemy.Type.Ghost:
					case Hakaima.Enemy.Type.Soul:
					case Hakaima.Enemy.Type.Skeleton:
					case Hakaima.Enemy.Type.Mummy:
					case Hakaima.Enemy.Type.Shadowman:
					case Hakaima.Enemy.Type.Golem:
					case Hakaima.Enemy.Type.Goblin:
					case Hakaima.Enemy.Type.Parasol:
					case Hakaima.Enemy.Type.Kappa:
					case Hakaima.Enemy.Type.Tengu:
						dataText += string.Format ("\t\t\t\t{{Enemy.Type.{0},\t\t{1}}},\n", type, stageData.enemyTypeList [type]);
						break;
					}
				}
				dataText += "\t\t\t},\n";
				dataText += "\t\t\titemTypeList = new Dictionary<Item.Type, int> (){\n";
				foreach (Hakaima.Item.Type type in Enum.GetValues (typeof(Hakaima.Item.Type))) {
					switch (type){
					case Hakaima.Item.Type.Sandal:
					case Hakaima.Item.Type.Hoe:
					case Hakaima.Item.Type.Stone:
					case Hakaima.Item.Type.Amulet:
					case Hakaima.Item.Type.Parasol:
						dataText += string.Format ("\t\t\t\t{{Item.Type.{0},\t\t{1}}},\n", type, stageData.itemTypeList [type]);
						break;
					}
				}
				dataText += "\t\t\t},\n";
				dataText += "\t\t},\n";
			}
			dataText += "\t};";
		}
		
		
		www = new WWW ("https://drive.google.com/uc?id=0BwLM9OUPdvBVaGdyakdEWWh3dUk");
		while (!www.isDone) {
			yield return null;
		}
		text = www.text;
		www.Dispose ();
		www = null;
		{
			Data.tutorialStageData = new Data.TutorialStageData ();
			Data.tutorialStageData.terrainTypeList = new List<Hakaima.Terrain.Type> ();
			Data.tutorialStageData.obstacleTypeList = new List<Hakaima.Obstacle.Type> ();
			Data.tutorialStageData.enemyTypeList = new List<Hakaima.Enemy.Type> ();
			Data.tutorialStageData.playerDataList = new List<Data.TutorialStageData.PlayerData> ();
			Data.tutorialStageData.enemyDataListList = new List<List<Data.TutorialStageData.EnemyData>> ();
			List<List<string>> list = new List<string> (text.Split ('\n')).Select (obj => new List<string> (obj.Split (','))).ToList ();
			
			for (int i = 0; i < Data.LENGTH_X * Data.LENGTH_Y; i++) {
				int d = int.Parse (list [17 - i / Data.LENGTH_X] [i % Data.LENGTH_X]);
				if (d == 0) {
					Data.tutorialStageData.terrainTypeList.Add (Hakaima.Terrain.Type.Soil);
				} else if (d == 1) {
					Data.tutorialStageData.terrainTypeList.Add (Hakaima.Terrain.Type.Pavement);
				}
			}
			for (int i = 0; i < Data.LENGTH_X * Data.LENGTH_Y; i++) {
				int d = int.Parse (list [36 - i / Data.LENGTH_X] [i % Data.LENGTH_X]);
				if (d == -1) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.None);
				} else if (d == 0) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Tomb);
				} else if (d == 2) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Stone);
				} else if (d == 3) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Tree);
				} else if (d == 4) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Signboard);
				} else if (d == 5) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Bucket);
				} else if (d == 8) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Picket);
				} else if (d == 9) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Lantern);
				} else if (d == 10) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.RubbleOff);
				} else if (d == 11) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.Stump);
				} else if (d == 12) {
					Data.tutorialStageData.obstacleTypeList.Add (Hakaima.Obstacle.Type.FallenTree);
				}
			}
			for (int i = 0; i < Data.LENGTH_X * Data.LENGTH_Y; i++) {
				if (i == 2) {
					Data.tutorialStageData.enemyTypeList.Add (Hakaima.Enemy.Type.Mummy);
				} else if (i == 61) {
					Data.tutorialStageData.enemyTypeList.Add (Hakaima.Enemy.Type.Ghost);
				} else if (i == 77) {
					Data.tutorialStageData.enemyTypeList.Add (Hakaima.Enemy.Type.Skeleton);
				} else if (i == 118) {
					Data.tutorialStageData.enemyTypeList.Add (Hakaima.Enemy.Type.Ghost);
				} else if (i == 120) {
					Data.tutorialStageData.enemyTypeList.Add (Hakaima.Enemy.Type.Skeleton);
				} else {
					Data.tutorialStageData.enemyTypeList.Add (Hakaima.Enemy.Type.None);
				}
			}
			for (int i = 0; i < 300; i++) {
				Data.TutorialStageData.PlayerData playerData = new Data.TutorialStageData.PlayerData ();
				Data.tutorialStageData.playerDataList.Add (playerData);
				if (i <= 5) {
					playerData.compass = Player.Compass.Top;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS_WALK;
				} else if (i == 6) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS;
				} else if (i == 7) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
					playerData.waitTime = 3f;
					playerData.waitText = Language.sentence [Language.GAME_TUTORIAL_1];
				} else if (i <= 22) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
				} else if (i == 23) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
					playerData.isCommandNoneClick = true;
				} else if (i <= 28) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
				} else if (i <= 33) {
					playerData.compass = Player.Compass.Top;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS_WALK;
				} else if (i == 34) {
					playerData.compass = Player.Compass.Right;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS;
				} else if (i <= 43) {
					playerData.compass = Player.Compass.Right;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
				} else if (i == 44) {
					playerData.compass = Player.Compass.Right;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
					playerData.isCommandNoneClick = true;
				} else if (i <= 52) {
					playerData.compass = Player.Compass.Right;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
				} else if (i <= 53) {
					playerData.compass = Player.Compass.Top;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS_WALK;
				} else if (i == 54) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS;
				} else if (i == 55) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS;
					playerData.waitTime = 3f;
					playerData.waitText = Language.sentence [Language.GAME_TUTORIAL_2];
				} else if (i <= 134) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
					playerData.canHoleCycle = true;
				} else if (i <= 166) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
				} else if (i <= 246) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
					playerData.canHoleCycle = true;
				} else if (i <= 270) {
					playerData.compass = Player.Compass.Left;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_NONE;
				} else {
					playerData.compass = Player.Compass.Top;
					playerData.command = GameManager.PLAYER_NEXT_COMMAND_COMPASS_WALK;
				}
			}
			for (int i = 0; i < Data.tutorialStageData.enemyTypeList.Count; i++) {
				Data.tutorialStageData.enemyDataListList.Add (new List<Data.TutorialStageData.EnemyData> ());
			}
			for (int i = 0; i < 14; i++) {
				Data.TutorialStageData.EnemyData enemyData = new Data.TutorialStageData.EnemyData ();
				Data.tutorialStageData.enemyDataListList [0].Add (enemyData);
				if (i < 2)
					enemyData.compass = Enemy.Compass.Right;
				else if (i < 14)
					enemyData.compass = Enemy.Compass.Top;
			}
			for (int i = 0; i < 5; i++) {
				Data.TutorialStageData.EnemyData enemyData = new Data.TutorialStageData.EnemyData ();
				Data.tutorialStageData.enemyDataListList [1].Add (enemyData);
				enemyData.compass = Enemy.Compass.Right;
			}
			for (int i = 0; i < 10; i++) {
				Data.TutorialStageData.EnemyData enemyData = new Data.TutorialStageData.EnemyData ();
				Data.tutorialStageData.enemyDataListList [2].Add (enemyData);
				if (i < 3)
					enemyData.compass = Enemy.Compass.Left;
				else if (i < 10)
					enemyData.compass = Enemy.Compass.Top;
			}
			for (int i = 0; i < 5; i++) {
				Data.TutorialStageData.EnemyData enemyData = new Data.TutorialStageData.EnemyData ();
				Data.tutorialStageData.enemyDataListList [3].Add (enemyData);
				enemyData.compass = Enemy.Compass.Left;
			}
			for (int i = 0; i < 5; i++) {
				Data.TutorialStageData.EnemyData enemyData = new Data.TutorialStageData.EnemyData ();
				Data.tutorialStageData.enemyDataListList [4].Add (enemyData);
				enemyData.compass = Enemy.Compass.Right;
			}


			dataText += "\n\n\n";
			dataText += "\tpublic static TutorialStageData tutorialStageData = new TutorialStageData (){\n";
			dataText += "\t\tterrainTypeList = new List<Terrain.Type> (){\n";
			for (int i = 0; i < Data.tutorialStageData.terrainTypeList.Count; i++) {
				dataText += string.Format ("\t\t\tTerrain.Type.{0},\n", Data.tutorialStageData.terrainTypeList [i]);
			}
			dataText += "\t\t},\n";
			dataText += "\t\tobstacleTypeList = new List<Obstacle.Type> (){\n";
			for (int i = 0; i < Data.tutorialStageData.obstacleTypeList.Count; i++) {
				dataText += string.Format ("\t\t\tObstacle.Type.{0},\n", Data.tutorialStageData.obstacleTypeList [i]);
			}
			dataText += "\t\t},\n";
			dataText += "\t\tenemyTypeList = new List<Enemy.Type> (){\n";
			for (int i = 0; i < Data.tutorialStageData.enemyTypeList.Count; i++) {
				dataText += string.Format ("\t\t\tEnemy.Type.{0},\n", Data.tutorialStageData.enemyTypeList [i]);
			}
			dataText += "\t\t},\n";
			dataText += "\t\tplayerDataList = new List<TutorialStageData.PlayerData> (){\n";
			for (int i = 0; i < Data.tutorialStageData.playerDataList.Count; i++) {
				Data.TutorialStageData.PlayerData playerData = Data.tutorialStageData.playerDataList [i];
				dataText += "\t\t\tnew TutorialStageData.PlayerData {\n";
				dataText += string.Format ("\t\t\t\tcompass = Player.Compass.{0},\n", playerData.compass);
				dataText += string.Format ("\t\t\t\tcommand = {0},\n", playerData.command);
				dataText += string.Format ("\t\t\t\tisCommandNoneClick = {0},\n", playerData.isCommandNoneClick.ToString ().ToLower ());
				dataText += string.Format ("\t\t\t\tcanHoleCycle = {0},\n", playerData.canHoleCycle.ToString ().ToLower ());
				dataText += string.Format ("\t\t\t\twaitTime = {0},\n", playerData.waitTime);
				dataText += string.Format ("\t\t\t\twaitText = \"{0}\",\n", playerData.waitText);
				dataText += "\t\t\t},\n";
			}
			dataText += "\t\t},\n";
			dataText += "\t\tenemyDataListList = new List<List<TutorialStageData.EnemyData>> (){\n";
			for(int i = 0; i < Data.tutorialStageData.enemyDataListList.Count; i++){
				dataText += "\t\t\tnew List<TutorialStageData.EnemyData>() {\n";
				List<Data.TutorialStageData.EnemyData> enemyDataList = Data.tutorialStageData.enemyDataListList[i];
				for(int j = 0; j < enemyDataList.Count; j++){
					Data.TutorialStageData.EnemyData enemyData = enemyDataList[j];
					dataText += "\t\t\t\tnew TutorialStageData.EnemyData {\n";
					dataText += string.Format ("\t\t\t\t\tcompass = Enemy.Compass.{0},\n", enemyData.compass);
					dataText += "\t\t\t\t},\n";
				}
				dataText += "\t\t\t},\n";
			}
			dataText += "\t\t},\n";
			dataText += "\t};";
		}


//		string filePath = @"C:\Users\iwashidon\Documents\Hakaima\Assets\Sample\data.txt";
//		System.Text.Encoding enc = System.Text.Encoding.GetEncoding ("utf-8");
//		System.IO.File.WriteAllText (filePath, dataText, enc);
	}
}
