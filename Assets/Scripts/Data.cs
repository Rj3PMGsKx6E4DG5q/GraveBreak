using System.Collections;
using System.Collections.Generic;
using Hakaima;


public class Data
{

	public class PlayerData
	{
		public float speed			{ get; set; }
		public float sandalSpeed	{ get; set; }
		public float fallTime		{ get; set; }
		public float bathTime		{ get; set; }
		public float hideTime		{ get; set; }
	}


	public class EnemyData
	{
		public int score			{ get; set; }
		public float speed			{ get; set; }
		public float angrySpeed		{ get; set; }
		public float angryTime		{ get; set; }
		public float fallTime		{ get; set; }
		public bool isFly			{ get; set; }
		public bool isAvoidHole		{ get; set; }
		public int patternIndex		{ get; set; }
		public int lifeCount		{ get; set; }
	}


	public class EnemyPatternData
	{
		public int rateWait			{ get; set; }
		public int rateStraight		{ get; set; }
		public int rateTurnRight	{ get; set; }
		public int rateTurnLeft		{ get; set; }
		public int rateTurnBack		{ get; set; }
		public int rateFollow		{ get; set; }
	}


	public class TerrainData
	{
		public bool isThrough		{ get; set; }
		public bool isDigging		{ get; set; }
		public bool isSlip			{ get; set; }
	}
	
	
	public class ObstacleData
	{
		public bool isThrough		{ get; set; }
	}

	
	public class BonusData
	{
		public int score			{ get; set; }
	}


	public class StageData
	{
		public float limitTime									{ get; set; }
		public float darkness									{ get; set; }
		public float mist										{ get; set; }
		public int sound										{ get; set; }
		public string clear										{ get; set; }
		public Dictionary<Terrain.Type, int> terrainTypeList	{ get; set; }
		public Dictionary<Obstacle.Type, int> obstacleTypeList	{ get; set; }
		public Dictionary<Enemy.Type, int> enemyTypeList		{ get; set; }
		public Dictionary<Item.Type, int> itemTypeList			{ get; set; }
		public int holeOpen										{ get; set; }
	}


	public class TutorialStageData
	{
		public class PlayerData
		{
			public Player.Compass compass;
			public int command;
			public bool isCommandNoneClick;
			public bool canHoleCycle;
			public float waitTime;
			public string waitText;
		}

		public class EnemyData
		{
			public Enemy.Compass compass;
		}

		public List<Terrain.Type> terrainTypeList				{ get; set; }
		public List<Obstacle.Type> obstacleTypeList				{ get; set; }
		public List<Enemy.Type> enemyTypeList					{ get; set; }
		public List<PlayerData> playerDataList					{ get; set; }
		public List<List<EnemyData>> enemyDataListList			{ get; set; }
	}


	public const string URL								= "https://play.google.com/store/apps/details?id=com.KohchanStudio.GraveBreak";
	public const string URL_IOS							= "https://itunes.apple.com/us/app/grave-break/id1364386526?l=ja&ls=1&mt=8";
	public const string MORE_GAME_PACKAGENAME_ANDROID	= "market://details?id=com.KohchanStudio.ShogiPuzzle";
	//public const string MORE_GAME_PACKAGENAME_IOS		= "http://appstore.com/将棋駒でパズル";
	public const string MORE_GAME_PACKAGENAME_IOS		= "itms-apps://itunes.apple.com/app/id1330151898";

	public const int TARGET_FRAME_RATE					= 60;
	public const float DELTA_TIME						= 1f/60;

	public const int SCREEN_WIDTH						= 1080;
	public const int SCREEN_HEIGHT						= 1920;
	public const int SCREEN_RESOLUTION					= 1024;

	public const int LENGTH_X							= 10;
	public const int LENGTH_Y							= 17;

	public const float SIZE_CHIP						= 96;
	public const float SPEED_0							= 0;
	public const float SPEED_1							= 1;
	public const float SPEED_2							= 2;
	public const float SPEED_3							= 3;
	public const float SPEED_4							= 4;
	public const float SPEED_6							= 6;
	public const float SPEED_8							= 8;
	public const float SPEED_12							= 12;
	public const float SPEED_16							= 16;
	public const float SPEED_24							= 24;
	public const float SPEED_32							= 32;
	public const float SPEED_48							= 48;

	public const int LAYER_0							= 0;
	public const int LAYER_1							= 1;
	public const int LAYER_2							= 2;
	public const int LAYER_3							= 3;
	public const int LAYER_4							= 4;
	public const int LAYER_5							= 5;
	public const int LAYER_6							= 6;
	public const int LAYER_7							= 7;
	public const int LAYER_8							= 8;
	public const int LAYER_9							= 9;
	public const int LAYER_10							= 10;
	public const int LAYER_11							= 11;

	public const int PLAYER_START_POINT_X				= 4;
	public const int PLAYER_START_POINT_Y				= 0;

	public const int ENEMY_PATTERN_NORMAL				= 20;

	public const int EVENT_MOVE_MAGNITUDE_COMPASS		= 5;
	public const int EVENT_MOVE_MAGNITUDE_COMPASS_WALK	= 120;
	public const int EVENT_MOVE_MAGNITUDE_COMPASS_WALK2	= 20;

	public const int FROM_TIME_TO_SCORE_COEFFICIENT		= 6;

	public const string RECORD_ENEMY_DIE_TO_TOMB		= "recordEnemyDieToTomb";
	public const string RECORD_ENEMY_DIE_TO_HOLE		= "recordEnemyDieToHole";
	public const string RECORD_TOMB_COLLAPSE			= "recordTombCollapse";
	public const string RECORD_HOLE_OPEN				= "recordHoleOpen";
	public const string RECORD_HOLE_CLOSE				= "recordHoleClose";
	public const string RECORD_HOLE_FALL				= "recordHoleFall";
	public const string RECORD_BONUS_APPEAR				= "recordBonusAppear";
	public const string RECORD_BONUS_GET				= "recordBonusGet";
	public const string RECORD_ITEM_GET					= "recordItemGet";
	public const string RECORD_DAMAGE					= "recordDamage";
	public const string RECORD_ESCAPE					= "recordEscape";
	public const string RECORD_MAX_TOMB_COLLAPSE		= "recordMaxTombCollapse";
	public const string RECORD_SCORE_ALL				= "recordScoreAll";
	public const string RECORD_CLEAR					= "recordClear";
	public const string RECORD_CLEAR_TIME				= "recordClearTime";

	public const string RECORD_IS_TUTORIAL				= "isTutorial";
	public const string RECORD_IS_TUTORIAL_FIRST_HELP	= "isTutorialFirstHelp";
	public const string RECORD_STAGE					= "stage";
	public const string RECORD_SCORE					= "score";
	public const string RECORD_SCORE_HIGH				= "scoreHigh";

	public const string HELP_PATH_JAPANESE				= "Prefabs/Help";
	public const string HELP_PATH_ENGLISH				= "Prefabs/Help_En";

	public const string SOUND_MUTE						= "sound_mute";

	public const string MY_LIFE							= "my_life";
	public const string MY_WEAPON						= "my_weapon";

	public const string LOGIN_NAME						= "login_name";
	public const string LOGIN_PASSWORD					= "login_password";

	public const int CHARACTER_MAX						= 6;
	public const int CHARACTER_SAMURAI					= 0;
	public const int CHARACTER_KUNOICHI					= 1;
	public const int CHARACTER_MIKO						= 2;
	public const int CHARACTER_NINJA					= 3;
	public const int CHARACTER_MATIMUSUME				= 4;
	public const int CHARACTER_HACHI					= 5;

	public const string RECORD_CHARACTER				= "recordCharacter";
	public const string RECORD_CHARACTER_SELECT			= "recordCharacterSelect";
	public const string RECORD_GACHATICKET				= "recordGachaTicket";

	public const string INFORMATION						= "information";

	public const string LOGIN_TIME						= "loginTime";


	// Firebase.
	// 画面遷移.
	public const string FIREBASE_SCREEN_MENU				= "sMenu";
	public const string FIREBASE_SCREEN_GACHA				= "sGacha";
	public const string FIREBASE_SCREEN_RECORD				= "sRecord";
	public const string FIREBASE_SCREEN_RANKING				= "sRanking";
	public const string FIREBASE_SCREEN_HELP				= "sHelp";
	public const string FIREBASE_SCREEN_EXTRA				= "sExtra";
	public const string FIREBASE_SCREEN_SELECT_CHARACTER	= "sSelectCharacter";
	public const string FIREBASE_SCREEN_STAGECLEAR			= "sStageClearNo.";
	public const string FIREBASE_SCREEN_STAGECLEAR_RUN		= "sStageClearRunNo.";
	public const string FIREBASE_SCREEN_GAMECLEAR			= "sGameClear";
	public const string FIREBASE_SCREEN_GAMEOVER			= "sGameOver";
	public const string FIREBASE_SCREEN_PUASE				= "sPause";
	public const string FIREBASE_SCREEN_STAGE				= "sStageNo.";			// StageNo1といった形で使う.
	public const string FIREBASE_SCREEN_BONUS				= "sBonus";				// ログインボーナス.
	public const string FIREBASE_SCREEN_INFORMATION			= "sNotice";			// お知らせ.
	// クリック・結果.
	public const string FIREBASE_EVENT_START				= "eStart";				// Game Start.
	public const string FIREBASE_EVENT_CONTINUE				= "eContinue";			// Game Continue.
	public const string FIREBASE_EVENT_GACHA_PLAY			= "eGachaPlay";			// ガチャを回した.
	public const string FIREBASE_EVENT_GACHA_KUNOICHI		= "eGachaKunoichi";		// くノ一を手に入れた.
	public const string FIREBASE_EVENT_GACHA_NINJA			= "eGachaNinja";		// 忍者を手に入れた.
	public const string FIREBASE_EVENT_GACHA_MIKO			= "eGachaMiko";			// 巫女.
	public const string FIREBASE_EVENT_GACHA_MACHIMUSUME	= "eGachaMachimusume";	// 町娘.
	public const string FIREBASE_EVENT_GACHA_HACHI			= "eGachaHachi";		// ハチ.
	public const string FIREBASE_EVENT_GACHA_LIFE			= "eGachaLife";			// ライフ.
	public const string FIREBASE_EVENT_RANKING_LOGIN		= "eRankingLogin";		// ランキングにログイン.
	public const string FIREBASE_EVENT_RANKING_SIGNIN		= "eRankingSignin";		// ランキングを登録.
	public const string FIREBASE_EVENT_EXTRA_ITEM			= "eExtraItem";			// アイテムの動画広告.
	public const string FIREBASE_EVENT_EXTRA_LIFE			= "eExtraLife";			// 残機アップの動画広告.
	public const string FIREBASE_EVENT_EXTRA_MOREGAME		= "eMoreGame";			// 
	public const string FIREBASE_EVENT_TWITTER				= "eTwitter";			// Twitter選択.
	public const string FIREBASE_EVNET_START_TUTORIAL		= "eStartTutorial";		// チュートリアルスタート.
	public const string FIREBASE_EVNET_FINISH_TUTORIAL		= "eFinishTutorial";	// チュートリアル終了.
	// Event log for Ads.
	public const string FIREBASE_EVENT_GACHA_ADS	 		= "eGachaAds";			// ガチャの動画広告.
	public const string FIREBASE_EVENT_GACHA_BANNER_ADS	 	= "eGachaBannerAds";
	public const string FIREBASE_EVENT_STAGECLEAR_BANNER_ADS= "eStageClearBannerAds";
	public const string FIREBASE_EVENT_GAMEOVER_BANNER_ADS	= "eGameOverBannerAds";
	public const string FIREBASE_EVENT_PAUSE_ADS			= "ePauseAds";

	#if UNITY_ANDROID
	public const string BANNER_ID = "ca-app-pub-5046886791626891/5221430889";
	#elif UNITY_IPHONE
	public const string BANNER_ID = "ca-app-pub-5046886791626891/9460058798";
	#else
	public const string BANNER_ID = "ca-app-pub-5046886791626891/5221430889";
	#endif

	#if UNITY_ANDROID
	public const string INTERSTITIAL_ID = "ca-app-pub-5046886791626891/6085022007";
	#elif UNITY_IPHONE
	public const string INTERSTITIAL_ID = "ca-app-pub-5046886791626891/8427862932";
	#else
	public const string INTERSTITIAL_ID = "ca-app-pub-5046886791626891/6085022007";
	#endif

	// ガチャやゲームオーバー時用、Gacha Ticket or Life.
	#if UNITY_ANDROID
	public const string INTERSTITIAL_NOMOVIE_ID = "ca-app-pub-5046886791626891/3802222215";
	#elif UNITY_IPHONE
	public const string INTERSTITIAL_NOMOVIE_ID = "ca-app-pub-5046886791626891/3179129747";
	#else
	public const string INTERSTITIAL_NOMOVIE_ID = "ca-app-pub-5046886791626891/3802222215";
	#endif


	private static List<float> speedList = new List<float> (){
		SPEED_0,
		SPEED_1,
		SPEED_2,
		SPEED_3,
		SPEED_4,
		SPEED_6,
		SPEED_8,
		SPEED_12,
		SPEED_16,
		SPEED_24,
		SPEED_32,
		SPEED_48,
	};


	private static PlayerData playerData = new PlayerData (){
		speed		= SPEED_8,
		sandalSpeed = SPEED_12,
		fallTime	= 2,
		bathTime	= 10,
		hideTime	= 10,
	};


	public static Dictionary<int, BonusData> bonusDataList = new Dictionary<int, BonusData> (){
		{(int)Bonus.Type.Bonus0,
			new BonusData {
				score = 1000,
			}
		},
		{(int)Bonus.Type.Bonus1,
			new BonusData {
				score = 1000,
			}
		},
		{(int)Bonus.Type.Bonus2,
			new BonusData {
				score = 1000,
			}
		},
		{(int)Bonus.Type.Bonus3,
			new BonusData {
				score = 1000,
			}
		},
		{(int)Bonus.Type.Bonus4,
			new BonusData {
				score = 1000,
			}
		},
		{(int)Bonus.Type.Bonus5,
			new BonusData {
				score = 1000,
			}
		},
		{(int)Bonus.Type.Bonus6,
			new BonusData {
				score = 2000,
			}
		},
	};


	public static PlayerData GetPlayerData ()
	{
		return playerData;
	}


	
	public static float GetPlayerSpeed (Terrain.Type terrainType, bool sandal)
	{
		float speed = sandal ? playerData.sandalSpeed : playerData.speed;
		
		switch (terrainType) {
		case Terrain.Type.Muddy:
			{
				float muddySpeed = 0;
				for (int i = 0; i < speedList.Count; i++) {
					muddySpeed = speedList [i];
					if (muddySpeed >= 0.5f * speed)
						break;
				}
				speed = muddySpeed;
			}
			break;
		case Terrain.Type.Ice:
			{
				float iceSpeed = 0;
				for (int i = 0; i < speedList.Count; i++) {
					iceSpeed = speedList [i];
					if (iceSpeed >= 1.5f * speed)
						break;
				}
				speed = iceSpeed;
			}
			break;
		}

		return speed;
	}

	
	
	public static float GetPlayerImageCoefficient (Terrain.Type terrainType)
	{
		float imageCoefficient = 0.02f;
		if (terrainType == Terrain.Type.Ice)
			imageCoefficient = 0;

		return imageCoefficient;
	}



	public static EnemyData GetEnemyData (Enemy.Type type)
	{
		return enemyDataList [(int)type];
	}



	public static int GetEnemyScore (Enemy.Type type, bool angry)
	{
		int score = enemyDataList [(int)type].score;
		return (int)(score * (angry ? 1.5f : 1f));
	}



	public static float GetEnemySpeed (Enemy.Type type, Terrain.Type terrainType, bool angry, bool follow, bool timeup)
	{
		float speed = angry ? enemyDataList [(int)type].angrySpeed : enemyDataList [(int)type].speed;

		if (!GetEnemyData (type).isFly) {
			switch (terrainType) {
			case Terrain.Type.Muddy:
				{
					float muddySpeed = 0;
					for (int i = 0; i < speedList.Count; i++) {
						muddySpeed = speedList [i];
						if (muddySpeed >= 0.5f * speed)
							break;
					}
					speed = muddySpeed;
				}
				break;
			case Terrain.Type.Ice:
				{
					float iceSpeed = 0;
					for (int i = 0; i < speedList.Count; i++) {
						iceSpeed = speedList [i];
						if (iceSpeed >= 2f * speed)
							break;
					}
					speed = iceSpeed;
				}
				break;
			}
		}

		if (follow) {
			float followSpeed = 0;
			for (int i = 0; i < speedList.Count; i++) {
				followSpeed = speedList [i];
				if (followSpeed >= 1.5f * speed)
					break;
			}
			speed = followSpeed;
		}
		
		if (timeup) {
			float timeupSpeed = 0;
			for (int i = 0; i < speedList.Count; i++) {
				timeupSpeed = speedList [i];
				if (timeupSpeed >= 1.5f * speed)
					break;
			}
			speed = timeupSpeed;
		}

		return speed;
	}
	
	
	
	public static float GetEnemyImageCoefficient (Enemy.Type type, Terrain.Type terrainType)
	{
		float imageCoefficient = 0.02f;

		if (!GetEnemyData (type).isFly) {
			if (terrainType == Terrain.Type.Ice)
				imageCoefficient = 0;
		}

		return imageCoefficient;
	}

	
	
	public static TerrainData GetTerrainData (Terrain.Type type)
	{
		return terrainDataList [(int)type];
	}
	
	
	
	public static ObstacleData GetObstacleData (Obstacle.Type type)
	{
		return obstacleDataList [(int)type];
	}
	
	
	
	public static BonusData GetBonusData (Bonus.Type type)
	{
		return bonusDataList [(int)type];
	}

	
	
	public static StageData GetStageData (int stage)
	{
		return stageDataList [stage];
	}



	public static int GetLifeupIndex (int score){
		if (score < 3000) {
			return 1;
		} else if (score < 9000) {
			return 2;
		} else if (score < 27000) {
			return 3;
		} else {
			return (int)(1.0f * score / 27000) + 3;
		}
	}






	public static Dictionary<int, EnemyData> enemyDataList = new Dictionary<int, EnemyData> (){
		{(int)Enemy.Type.Person,
			new EnemyData {
				score			= 0,
				speed			= 1f,
				angrySpeed		= 3f,
				angryTime		= 3,
				fallTime		= 1,
				isFly			= false,
				isAvoidHole		= true,
				patternIndex	= 8,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Ghost,
			new EnemyData {
				score			= 600,
				speed			= 1.6f,
				angrySpeed		= 2f,
				angryTime		= 0,
				fallTime		= 0,
				isFly			= true,
				isAvoidHole		= false,
				patternIndex	= 2,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Soul,
			new EnemyData {
				score			= 700,
				speed			= 1f,
				angrySpeed		= 2f,
				angryTime		= 0,
				fallTime		= 0,
				isFly			= true,
				isAvoidHole		= false,
				patternIndex	= 3,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Skeleton,
			new EnemyData {
				score			= 100,
				speed			= 1f,
				angrySpeed		= 2f,
				angryTime		= 8,
				fallTime		= 8,
				isFly			= false,
				isAvoidHole		= false,
				patternIndex	= 1,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Mummy,
			new EnemyData {
				score			= 300,
				speed			= 1.2f,
				angrySpeed		= 2f,
				angryTime		= 8,
				fallTime		= 8,
				isFly			= false,
				isAvoidHole		= false,
				patternIndex	= 6,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Shadowman,
			new EnemyData {
				score			= 500,
				speed			= 3f,
				angrySpeed		= 6f,
				angryTime		= 4,
				fallTime		= 8,
				isFly			= false,
				isAvoidHole		= false,
				patternIndex	= 4,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Golem,
			new EnemyData {
				score			= 1000,
				speed			= 2f,
				angrySpeed		= 4.8f,
				angryTime		= 4,
				fallTime		= 5,
				isFly			= false,
				isAvoidHole		= false,
				patternIndex	= 7,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Goblin,
			new EnemyData {
				score			= 900,
				speed			= 2.4f,
				angrySpeed		= 4.8f,
				angryTime		= 8,
				fallTime		= 0,
				isFly			= true,
				isAvoidHole		= false,
				patternIndex	= 4,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Parasol,
			new EnemyData {
				score			= 1300,
				speed			= 2f,
				angrySpeed		= 4f,
				angryTime		= 8,
				fallTime		= 5,
				isFly			= false,
				isAvoidHole		= false,
				patternIndex	= 2,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Kappa,
			new EnemyData {
				score			= 1500,
				speed			= 3.2f,
				angrySpeed		= 12f,
				angryTime		= 3,
				fallTime		= 5,
				isFly			= false,
				isAvoidHole		= false,
				patternIndex	= 4,
				lifeCount		= 1,
			}
		},
		{(int)Enemy.Type.Tengu,
			new EnemyData {
				score			= 0,
				speed			= 3f,
				angrySpeed		= 6f,
				angryTime		= 10,
				fallTime		= 1,
				isFly			= false,
				isAvoidHole		= true,
				patternIndex	= 5,
				lifeCount		= 3,
			}
		},
	};
	
	
	public static Dictionary<int, EnemyPatternData> enemyPatternDataList = new Dictionary<int, EnemyPatternData> (){
		{0,
			new EnemyPatternData {
				rateWait			= 0,
				rateStraight		= 0,
				rateTurnRight		= 0,
				rateTurnLeft		= 0,
				rateTurnBack		= 0,
				rateFollow			= 100,
			}
		},
		{1,
			new EnemyPatternData {
				rateWait			= 25,
				rateStraight		= 30,
				rateTurnRight		= 15,
				rateTurnLeft		= 15,
				rateTurnBack		= 15,
				rateFollow			= 0,
			}
		},
		{2,
			new EnemyPatternData {
				rateWait			= 50,
				rateStraight		= 15,
				rateTurnRight		= 15,
				rateTurnLeft		= 15,
				rateTurnBack		= 5,
				rateFollow			= 0,
			}
		},
		{3,
			new EnemyPatternData {
				rateWait			= 20,
				rateStraight		= 15,
				rateTurnRight		= 30,
				rateTurnLeft		= 30,
				rateTurnBack		= 5,
				rateFollow			= 0,
			}
		},
		{4,
			new EnemyPatternData {
				rateWait			= 5,
				rateStraight		= 30,
				rateTurnRight		= 30,
				rateTurnLeft		= 30,
				rateTurnBack		= 5,
				rateFollow			= 0,
			}
		},
		{5,
			new EnemyPatternData {
				rateWait			= 20,
				rateStraight		= 20,
				rateTurnRight		= 20,
				rateTurnLeft		= 20,
				rateTurnBack		= 20,
				rateFollow			= 0,
			}
		},
		{6,
			new EnemyPatternData {
				rateWait			= 10,
				rateStraight		= 20,
				rateTurnRight		= 10,
				rateTurnLeft		= 10,
				rateTurnBack		= 0,
				rateFollow			= 50,
			}
		},
		{7,
			new EnemyPatternData {
				rateWait			= 15,
				rateStraight		= 30,
				rateTurnRight		= 15,
				rateTurnLeft		= 15,
				rateTurnBack		= 5,
				rateFollow			= 20,
			}
		},
		{8,
			new EnemyPatternData {
				rateWait			= 0,
				rateStraight		= 5,
				rateTurnRight		= 5,
				rateTurnLeft		= 5,
				rateTurnBack		= 5,
				rateFollow			= 80,
			}
		},
		{20,
			new EnemyPatternData {
				rateWait			= 20,
				rateStraight		= 30,
				rateTurnRight		= 20,
				rateTurnLeft		= 20,
				rateTurnBack		= 10,
				rateFollow			= 0,
			}
		},
	};
	
	
	public static Dictionary<int, TerrainData> terrainDataList = new Dictionary<int, TerrainData> (){
		{(int)Terrain.Type.None,
			new TerrainData {
				isThrough	= true,
				isDigging		= false,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.Soil,
			new TerrainData {
				isThrough	= true,
				isDigging		= true,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.Grass,
			new TerrainData {
				isThrough	= true,
				isDigging		= true,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.Muddy,
			new TerrainData {
				isThrough	= true,
				isDigging		= true,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.Pavement,
			new TerrainData {
				isThrough	= true,
				isDigging		= false,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.Ice,
			new TerrainData {
				isThrough	= true,
				isDigging		= true,
				isSlip			= true,
			}
		},
		{(int)Terrain.Type.River,
			new TerrainData {
				isThrough	= false,
				isDigging		= false,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.Bridge,
			new TerrainData {
				isThrough	= true,
				isDigging		= false,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.BridgeVertical,
			new TerrainData {
				isThrough	= true,
				isDigging		= false,
				isSlip			= false,
			}
		},
		{(int)Terrain.Type.BridgeHorizontal,
			new TerrainData {
				isThrough	= true,
				isDigging		= false,
				isSlip			= false,
			}
		},
	};
	
	
	public static Dictionary<int, ObstacleData> obstacleDataList = new Dictionary<int, ObstacleData> (){
		{(int)Obstacle.Type.None,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Tree,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Stone,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.Tomb,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.TombCollapse,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.TombCollapseEnd,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.TombPiece,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.TombPieceEnd,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.CartRight,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.CartLeft,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Well,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Bale,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.Bathtub,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.BathtubCollapse,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.BathtubCollapseEnd,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Stockpile,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.StockpileCollapse,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.StockpileCollapseEnd,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Signboard,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.Tower,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.FallenTree,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.Stump,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.Bucket,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Lantern,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.StupaFence,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.Stupa,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.LargeTreeRight,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.LargeTreeLeft,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.Rubble,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.RubbleOff,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.Picket,
			new ObstacleData {
				isThrough	= false,
			}
		},
		{(int)Obstacle.Type.FallTombPiece,
			new ObstacleData {
				isThrough	= true,
			}
		},
		{(int)Obstacle.Type.FallTombPieceEnd,
			new ObstacleData {
				isThrough	= true,
			}
		},
	};
	
	
	public static List<StageData> stageDataList = new List<StageData> (){
		new StageData {
			limitTime = 60f,
			darkness = 0f,
			mist = 0f,
			sound = 0,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		160},
				{Terrain.Type.Grass,		10},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		11},
				{Obstacle.Type.Stone,		5},
				{Obstacle.Type.Tomb,		43},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		0},
				{Obstacle.Type.Stump,		0},
				{Obstacle.Type.Bucket,		6},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		3},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		2},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		0},
				{Item.Type.Hoe,		0},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		0},
				{Item.Type.Parasol,		0},
			},
		},
		new StageData {
			limitTime = 90f,
			darkness = 0f,
			mist = 0f,
			sound = 0,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		140},
				{Terrain.Type.Grass,		20},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		10},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		10},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		43},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		3},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		0},
				{Obstacle.Type.Bucket,		5},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		3},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		1},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		1},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		2},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		0},
				{Item.Type.Hoe,		0},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		10},
				{Item.Type.Parasol,		1},
			},
		},
		new StageData {
			limitTime = 90f,
			darkness = 0f,
			mist = 0f,
			sound = 2,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		30},
				{Terrain.Type.Grass,		140},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		0},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		46},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		2},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		1},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		0},
				{Obstacle.Type.Stump,		0},
				{Obstacle.Type.Bucket,		1},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		4},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		1},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		1},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		2},
				{Enemy.Type.Mummy,		1},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		10},
				{Item.Type.Hoe,		10},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		2},
			},
		},
		new StageData {
			limitTime = 90f,
			darkness = 0f,
			mist = 0f,
			sound = 2,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		141},
				{Terrain.Type.Grass,		30},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		-1},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		11},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		46},
				{Obstacle.Type.CartRight,		1},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		1},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		0},
				{Obstacle.Type.Stump,		0},
				{Obstacle.Type.Bucket,		5},
				{Obstacle.Type.Lantern,		1},
				{Obstacle.Type.StupaFence,		4},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		3},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		1},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		1},
				{Enemy.Type.Mummy,		2},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		8},
				{Item.Type.Hoe,		8},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		3},
			},
		},
		new StageData {
			limitTime = 150f,
			darkness = 0f,
			mist = 0.4f,
			sound = 1,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		70},
				{Terrain.Type.Grass,		40},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		60},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		7},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		29},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		2},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		1},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		2},
				{Obstacle.Type.Stump,		0},
				{Obstacle.Type.Bucket,		3},
				{Obstacle.Type.Lantern,		5},
				{Obstacle.Type.StupaFence,		9},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		1},
				{Obstacle.Type.Rubble,		5},
				{Obstacle.Type.Picket,		2},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		1},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		1},
				{Enemy.Type.Mummy,		2},
				{Enemy.Type.Shadowman,		1},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		8},
				{Item.Type.Hoe,		8},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		3},
			},
		},
		new StageData {
			limitTime = 120f,
			darkness = 0.4f,
			mist = 0f,
			sound = 1,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		0},
				{Terrain.Type.Grass,		100},
				{Terrain.Type.Muddy,		10},
				{Terrain.Type.Pavement,		60},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		0},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		36},
				{Obstacle.Type.CartRight,		3},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		3},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		1},
				{Obstacle.Type.Bucket,		0},
				{Obstacle.Type.Lantern,		2},
				{Obstacle.Type.StupaFence,		6},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		1},
				{Enemy.Type.Ghost,		1},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		2},
				{Enemy.Type.Shadowman,		2},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		3},
			},
		},
		new StageData {
			limitTime = 90f,
			darkness = 0f,
			mist = 0f,
			sound = 0,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		130},
				{Terrain.Type.Grass,		40},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		1},
				{Terrain.Type.Bridge,		5},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		11},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		56},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		2},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		2},
				{Obstacle.Type.Stump,		1},
				{Obstacle.Type.Bucket,		5},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		6},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		5},
				{Obstacle.Type.Picket,		2},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		1},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		3},
				{Enemy.Type.Shadowman,		2},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		3},
			},
		},
		new StageData {
			limitTime = 120f,
			darkness = 0.2f,
			mist = 0f,
			sound = 2,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		90},
				{Terrain.Type.Grass,		60},
				{Terrain.Type.Muddy,		20},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		20},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		50},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		2},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		2},
				{Obstacle.Type.Bucket,		3},
				{Obstacle.Type.Lantern,		1},
				{Obstacle.Type.StupaFence,		0},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		1},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		1},
				{Enemy.Type.Shadowman,		5},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		1},
			},
		},
		new StageData {
			limitTime = 150f,
			darkness = 0.3f,
			mist = 0.2f,
			sound = 1,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		61},
				{Terrain.Type.Grass,		30},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		-1},
				{Terrain.Type.Ice,		80},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		6},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		34},
				{Obstacle.Type.CartRight,		2},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		3},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		1},
				{Obstacle.Type.FallenTree,		2},
				{Obstacle.Type.Stump,		2},
				{Obstacle.Type.Bucket,		3},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		4},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		2},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		2},
				{Enemy.Type.Skeleton,		1},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		2},
				{Enemy.Type.Golem,		1},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		0},
			},
		},
		new StageData {
			limitTime = 120f,
			darkness = 0.3f,
			mist = 0.2f,
			sound = 0,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		40},
				{Terrain.Type.Grass,		130},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		2},
				{Terrain.Type.Bridge,		2},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		11},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		56},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		4},
				{Obstacle.Type.Bale,		3},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		1},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		2},
				{Obstacle.Type.Bucket,		2},
				{Obstacle.Type.Lantern,		5},
				{Obstacle.Type.StupaFence,		6},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		3},
				{Enemy.Type.Ghost,		1},
				{Enemy.Type.Soul,		2},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		2},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		3},
			},
		},
		new StageData {
			limitTime = 120f,
			darkness = 0.3f,
			mist = 0.2f,
			sound = 0,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		45},
				{Terrain.Type.Grass,		45},
				{Terrain.Type.Muddy,		80},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		6},
				{Obstacle.Type.Stone,		0},
				{Obstacle.Type.Tomb,		36},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		3},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		1},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		3},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		2},
				{Obstacle.Type.Bucket,		0},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		6},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		2},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		2},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		2},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		2},
				{Enemy.Type.Golem,		2},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		2},
			},
		},
		new StageData {
			limitTime = 180f,
			darkness = 0f,
			mist = 0f,
			sound = 2,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		41},
				{Terrain.Type.Grass,		40},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		-1},
				{Terrain.Type.Ice,		90},
				{Terrain.Type.River,		2},
				{Terrain.Type.Bridge,		3},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		5},
				{Obstacle.Type.Stone,		40},
				{Obstacle.Type.Tomb,		-7},
				{Obstacle.Type.CartRight,		2},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		3},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		2},
				{Obstacle.Type.FallenTree,		2},
				{Obstacle.Type.Stump,		2},
				{Obstacle.Type.Bucket,		2},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		3},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		5},
				{Enemy.Type.Mummy,		2},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		2},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		1},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		2},
			},
		},
		new StageData {
			limitTime = 120f,
			darkness = 0.4f,
			mist = 0.2f,
			sound = 1,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		120},
				{Terrain.Type.Grass,		50},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		1},
				{Terrain.Type.Bridge,		4},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		18},
				{Obstacle.Type.Stone,		0},
				{Obstacle.Type.Tomb,		58},
				{Obstacle.Type.CartRight,		4},
				{Obstacle.Type.Well,		2},
				{Obstacle.Type.Bale,		2},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		0},
				{Obstacle.Type.FallenTree,		0},
				{Obstacle.Type.Stump,		0},
				{Obstacle.Type.Bucket,		8},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		15},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		2},
				{Enemy.Type.Ghost,		2},
				{Enemy.Type.Soul,		2},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		2},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		2},
			},
		},
		new StageData {
			limitTime = 150f,
			darkness = 0.4f,
			mist = 0.6f,
			sound = 2,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		50},
				{Terrain.Type.Grass,		120},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		11},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		56},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		4},
				{Obstacle.Type.Bathtub,		2},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		4},
				{Obstacle.Type.FallenTree,		0},
				{Obstacle.Type.Stump,		0},
				{Obstacle.Type.Bucket,		2},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		0},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		10},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		3},
				{Enemy.Type.Ghost,		1},
				{Enemy.Type.Soul,		1},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		1},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		2},
				{Enemy.Type.Goblin,		1},
				{Enemy.Type.Parasol,		1},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		4},
			},
		},
		new StageData {
			limitTime = 150f,
			darkness = 0.6f,
			mist = 0f,
			sound = 1,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		142},
				{Terrain.Type.Grass,		10},
				{Terrain.Type.Muddy,		20},
				{Terrain.Type.Pavement,		-2},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		10},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		51},
				{Obstacle.Type.CartRight,		2},
				{Obstacle.Type.Well,		2},
				{Obstacle.Type.Bale,		2},
				{Obstacle.Type.Bathtub,		0},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		6},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		1},
				{Obstacle.Type.Bucket,		5},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		1},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		2},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		2},
				{Enemy.Type.Ghost,		2},
				{Enemy.Type.Soul,		2},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		2},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		2},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		2},
			},
		},
		new StageData {
			limitTime = 300f,
			darkness = 0.3f,
			mist = 0.9f,
			sound = 2,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		35},
				{Terrain.Type.Grass,		15},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		-2},
				{Terrain.Type.Ice,		122},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		3},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		22},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		3},
				{Obstacle.Type.FallenTree,		4},
				{Obstacle.Type.Stump,		4},
				{Obstacle.Type.Bucket,		2},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		2},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		2},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		2},
				{Enemy.Type.Goblin,		2},
				{Enemy.Type.Parasol,		2},
				{Enemy.Type.Kappa,		2},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		0},
			},
		},
		new StageData {
			limitTime = 150f,
			darkness = 0.2f,
			mist = 0f,
			sound = 2,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		170},
				{Terrain.Type.Grass,		0},
				{Terrain.Type.Muddy,		0},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		1},
				{Terrain.Type.Bridge,		4},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		11},
				{Obstacle.Type.Stone,		50},
				{Obstacle.Type.Tomb,		8},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		0},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		3},
				{Obstacle.Type.FallenTree,		2},
				{Obstacle.Type.Stump,		1},
				{Obstacle.Type.Bucket,		15},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		8},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		1},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		2},
				{Enemy.Type.Soul,		1},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		2},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		2},
				{Enemy.Type.Goblin,		2},
				{Enemy.Type.Parasol,		0},
				{Enemy.Type.Kappa,		3},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		2},
			},
		},
		new StageData {
			limitTime = 150f,
			darkness = 0.4f,
			mist = 0.7f,
			sound = 1,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		152},
				{Terrain.Type.Grass,		0},
				{Terrain.Type.Muddy,		20},
				{Terrain.Type.Pavement,		-2},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		0},
				{Terrain.Type.Bridge,		0},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		10},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		51},
				{Obstacle.Type.CartRight,		6},
				{Obstacle.Type.Well,		2},
				{Obstacle.Type.Bale,		1},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		3},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		1},
				{Obstacle.Type.Bucket,		6},
				{Obstacle.Type.Lantern,		3},
				{Obstacle.Type.StupaFence,		1},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		3},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		1},
				{Enemy.Type.Goblin,		3},
				{Enemy.Type.Parasol,		3},
				{Enemy.Type.Kappa,		0},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		3},
			},
		},
		new StageData {
			limitTime = 180f,
			darkness = 0.4f,
			mist = 0.2f,
			sound = 1,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		150},
				{Terrain.Type.Grass,		0},
				{Terrain.Type.Muddy,		20},
				{Terrain.Type.Pavement,		0},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		1},
				{Terrain.Type.Bridge,		4},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		9},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		50},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		3},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		0},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		5},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		1},
				{Obstacle.Type.Bucket,		5},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		0},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		0},
				{Obstacle.Type.Picket,		2},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		0},
				{Enemy.Type.Soul,		1},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		2},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		0},
				{Enemy.Type.Goblin,		4},
				{Enemy.Type.Parasol,		2},
				{Enemy.Type.Kappa,		3},
				{Enemy.Type.Tengu,		0},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		10},
				{Item.Type.Amulet,		4},
				{Item.Type.Parasol,		2},
			},
		},
		new StageData {
			limitTime = 300f,
			darkness = 0.4f,
			mist = 0.5f,
			sound = 0,
			terrainTypeList = new Dictionary<Terrain.Type, int> (){
				{Terrain.Type.Soil,		90},
				{Terrain.Type.Grass,		40},
				{Terrain.Type.Muddy,		10},
				{Terrain.Type.Pavement,		30},
				{Terrain.Type.Ice,		0},
				{Terrain.Type.River,		4},
				{Terrain.Type.Bridge,		3},
			},
			obstacleTypeList = new Dictionary<Obstacle.Type, int> (){
				{Obstacle.Type.Tree,		8},
				{Obstacle.Type.Stone,		2},
				{Obstacle.Type.Tomb,		45},
				{Obstacle.Type.CartRight,		0},
				{Obstacle.Type.Well,		3},
				{Obstacle.Type.Bale,		0},
				{Obstacle.Type.Bathtub,		1},
				{Obstacle.Type.Stockpile,		2},
				{Obstacle.Type.Signboard,		0},
				{Obstacle.Type.Tower,		3},
				{Obstacle.Type.FallenTree,		1},
				{Obstacle.Type.Stump,		2},
				{Obstacle.Type.Bucket,		3},
				{Obstacle.Type.Lantern,		0},
				{Obstacle.Type.StupaFence,		5},
				{Obstacle.Type.Stupa,		0},
				{Obstacle.Type.LargeTreeRight,		0},
				{Obstacle.Type.Rubble,		5},
				{Obstacle.Type.Picket,		0},
			},
			enemyTypeList = new Dictionary<Enemy.Type, int> (){
				{Enemy.Type.Person,		0},
				{Enemy.Type.Ghost,		3},
				{Enemy.Type.Soul,		0},
				{Enemy.Type.Skeleton,		0},
				{Enemy.Type.Mummy,		0},
				{Enemy.Type.Shadowman,		0},
				{Enemy.Type.Golem,		3},
				{Enemy.Type.Goblin,		0},
				{Enemy.Type.Parasol,		3},
				{Enemy.Type.Kappa,		3},
				{Enemy.Type.Tengu,		1},
			},
			itemTypeList = new Dictionary<Item.Type, int> (){
				{Item.Type.Sandal,		3},
				{Item.Type.Hoe,		3},
				{Item.Type.Stone,		15},
				{Item.Type.Amulet,		8},
				{Item.Type.Parasol,		0},
			},
		},
	};
	
	
	public static TutorialStageData tutorialStageData = new TutorialStageData (){
		terrainTypeList = new List<Terrain.Type> (){
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Pavement,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
			Terrain.Type.Soil,
		},
		obstacleTypeList = new List<Obstacle.Type> (){
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.RubbleOff,
			Obstacle.Type.None,
			Obstacle.Type.RubbleOff,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Picket,
			Obstacle.Type.Picket,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tree,
			Obstacle.Type.None,
			Obstacle.Type.Picket,
			Obstacle.Type.Picket,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tree,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Lantern,
			Obstacle.Type.Lantern,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tree,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.RubbleOff,
			Obstacle.Type.None,
			Obstacle.Type.RubbleOff,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Lantern,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tree,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.Tomb,
			Obstacle.Type.Tree,
			Obstacle.Type.RubbleOff,
			Obstacle.Type.None,
			Obstacle.Type.RubbleOff,
			Obstacle.Type.Tree,
			Obstacle.Type.Tomb,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
			Obstacle.Type.None,
		},
		enemyTypeList = new List<Enemy.Type> (){
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.Mummy,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.Ghost,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.Skeleton,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.Ghost,
			Enemy.Type.None,
			Enemy.Type.Skeleton,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
			Enemy.Type.None,
		},
		playerDataList = new List<TutorialStageData.PlayerData> (){
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 1,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 3,
				waitText = Language.sentence [Language.GAME_TUTORIAL_1],
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = true,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 1,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = true,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Right,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 1,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 1,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 3,
				waitText = Language.sentence [Language.GAME_TUTORIAL_2],
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = true,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Left,
				command = 0,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
			new TutorialStageData.PlayerData {
				compass = Player.Compass.Top,
				command = 2,
				isCommandNoneClick = false,
				canHoleCycle = false,
				waitTime = 0,
				waitText = "",
			},
		},
		enemyDataListList = new List<List<TutorialStageData.EnemyData>> (){
			new List<TutorialStageData.EnemyData>() {
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
			},
			new List<TutorialStageData.EnemyData>() {
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
			},
			new List<TutorialStageData.EnemyData>() {
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Top,
				},
			},
			new List<TutorialStageData.EnemyData>() {
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Left,
				},
			},
			new List<TutorialStageData.EnemyData>() {
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
				new TutorialStageData.EnemyData {
					compass = Enemy.Compass.Right,
				},
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
			new List<TutorialStageData.EnemyData>() {
			},
		},
	};}
