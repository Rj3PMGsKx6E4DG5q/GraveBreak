using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Language
{

	public const int START_CAUTION					= 0;

	public const int GAME_READY_CONDITION			= 1;
	public const int GAME_READY_APPEAR_NONE			= 2;
	public const int GAME_READY_APPEAR_SAMURAI		= 3;
	public const int GAME_READY_APPEAR_BOSS			= 4;

	public const int GAME_PAUSE						= 5;
	public const int GAME_CONTINUE					= 6;
	public const int GAME_HELP						= 7;
	public const int GAME_TUTORIAL_1				= 8;
	public const int GAME_TUTORIAL_2				= 9;

	public const int RECORD_ENEMY_DIE_TO_TOMB		= 10;
	public const int RECORD_ENEMY_DIE_TO_HOLE		= 11;
	public const int RECORD_TOMB_COLLAPSE			= 12;
	public const int RECORD_HOLE_OPEN				= 13;
	public const int RECORD_HOLE_CLOSE				= 14;
	public const int RECORD_HOLE_FALL				= 15;
	public const int RECORD_BONUS_APPEAR			= 16;
	public const int RECORD_BONUS_GET				= 17;
	public const int RECORD_ITEM_GET				= 18;
	public const int RECORD_DAMAGE					= 19;
	public const int RECORD_ESCAPE					= 20;
	public const int RECORD_MAX_TOMB_COLLAPSE		= 21;
	public const int RECORD_SCORE_ALL				= 22;
	public const int RECORD_CLEAR_TIME				= 23;

	public const int EXTRA_DESCRIPTION				= 30;
	public const int EXTRA_ITEM_TITLE				= 31;
	public const int EXTRA_ITEM_DESCRIPTION			= 32;
	public const int EXTRA_ITEM_DESCRIPTION_HAVE	= 33;
	public const int EXTRA_LIFE_TITLE				= 34;
	public const int EXTRA_LIFE_DESCRIPTION			= 35;
	public const int EXTRA_RECOMMENDED_TITLE		= 36;

	public const int TWITTER						= 40;
	public const int APPLICATION_QUIT				= 41;

	public const int ERROR_NONAME 					= 42;
	public const int ERROR_NOPASSWORD				= 43;
	public const int ERROR_LOGIN_INCORRECT 			= 44;
	public const int ERROR_SIGNUP_NOMATCH_PASSWORD	= 45;
	public const int ERROR_SIGNUP_INCORRECT			= 46;
	public const int LOGIN							= 47;
	public const int SIGNUP		 					= 48;
	public const int CONNECTING						= 49;
	public const int RANKING_NAME					= 50;
	public const int RANKING_PASSWORD				= 51;
	public const int RANKING_PASSWORD_CONFIRM		= 52;
	public const int RANKING_NAME_FORM				= 53;
	public const int RANKING_PASSWORD_FORM			= 54;
	public const int LOGOUT							= 55;
	public const int REQUEST_OVERLOAD				= 56;
	public const int RANKING_THISSYSTEM				= 57;
	public const int CHARANAME_SAMURAI				= 58;
	public const int CHARANAME_KUNOICHI				= 59;
	public const int CHARANAME_NINJA				= 60;
	public const int CHARANAME_MIKO					= 61;
	public const int CHARANAME_MUSUME				= 62;
	public const int CHARANAME_HACHI				= 63;
	public const int YOUGOTCHARA					= 64;
	public const int LIFEIS1UP						= 65;
	public const int GACHA_RESULT_GACHATICKETTEXT	= 66;
	public const int GACHA_LOTTERY_CHARA			= 67;
	public const int GACHA_LOTTERY_LIFE				= 68;
	public const int GACHA_SKIP						= 69;
	public const int LOGIN_BONUS_TEXT				= 70;
	public const int RANKING_FIRST_EXPLANATION		= 71;
	public const int RANKING_REGISTED_EXPLANATION	= 72;
	public const int LOGIN_BIGINNER					= 73;
	public const int LOGIN_FINISH					= 74;
	public const int RANKING_NAME_CHANGE_CORRECT	= 75;
	public const int RANKING_NAME_CHANGE_INCORRECT	= 76;
	public const int RANKING_NAME_CHANGE			= 77;
	public const int OFFLINE						= 78;


	public static Dictionary<int, string> sentence = sentenceJa;

	public static Dictionary<int, string> sentenceJa = new Dictionary<int, string> (){
		{START_CAUTION,					"CONTINUEを選ぶと続きから\nプレイできます。\nSTARTから行うと前回の記録は消えてしまいます。\n\n<color=#ff0000>本当に最初から始めますか？</color>"},

		{GAME_READY_CONDITION,			"クリア条件"},
		{GAME_READY_APPEAR_NONE,		"敵をすべて倒す\nもしくはタイムが０になるまで逃げる"},
		{GAME_READY_APPEAR_SAMURAI,		"侍以外の敵をすべて倒す\nもしくはタイムが０になるまで逃げる"},
		{GAME_READY_APPEAR_BOSS,		"ボスを倒す"},

		//{GAME_PAUSE,					"動画を視聴すると残機が５機増えます。"},
		//{GAME_CONTINUE,					"CONTINUEを選ぶとゲームオーバーしたステージの\n始めからゲームに復帰します。\n広告が出た時点で１機増えます。\nPLAY MOVIEを選ぶと動画広告が始まり、最後まで\n視聴すると残機が５機に増えてゲームに復帰出来ます。"},
		{GAME_PAUSE,					""},
		{GAME_CONTINUE,					"CONTINUEを選ぶとゲームオーバーしたステージの\n始めからゲームに復帰します。\nTITLEを選ぶとタイトルへ戻ります。"},
		{GAME_HELP,						"この画面は、次回以降表示されません。\nヘルプからいつでも確認出来ます。"},
		{GAME_TUTORIAL_1,				"墓で敵を倒せます。"},
		{GAME_TUTORIAL_2,				"穴に落として埋めると敵を倒せます。"},

		{RECORD_ENEMY_DIE_TO_TOMB,		"墓で敵を倒した回数\t\t\t"},
		{RECORD_ENEMY_DIE_TO_HOLE,		"穴埋めで敵を倒した回数\t\t"},
		{RECORD_TOMB_COLLAPSE,			"墓を倒した回数\t\t\t\t"},
		{RECORD_HOLE_OPEN,				"穴を掘った回数\t\t\t\t"},
		{RECORD_HOLE_CLOSE,				"穴を埋めた回数\t\t\t\t"},
		{RECORD_HOLE_FALL,				"穴に落ちた回数\t\t\t\t"},
		{RECORD_BONUS_APPEAR,			"ボーナスを出現させた回数\t"},
		{RECORD_BONUS_GET,				"ボーナスを取った回数\t\t"},
		{RECORD_ITEM_GET,				"アイテムを取った回数\t\t"},
		{RECORD_DAMAGE,					"やられた回数\t\t\t\t"},
		{RECORD_ESCAPE,					"逃げ切った回数\t\t\t\t"},
		{RECORD_MAX_TOMB_COLLAPSE,		"一度に倒した墓の最大数\t\t"},
		{RECORD_SCORE_ALL,				"総得点\t\t\t\t\t\t"},
		{RECORD_CLEAR_TIME,				"ステージ{0}の最速タイム"},
		
		{EXTRA_DESCRIPTION,				"動画を視聴するとゲーム開始から\n有利にプレイ出来ます。"},
		{EXTRA_ITEM_TITLE,				"アイテム獲得"},
		{EXTRA_ITEM_DESCRIPTION,		"アイテムを保持して\nゲームを開始できます"},
		{EXTRA_ITEM_DESCRIPTION_HAVE,	"すでに獲得済み"},
		{EXTRA_LIFE_TITLE,				"残機＋５"},
		{EXTRA_LIFE_DESCRIPTION,		"現在の残機\n99以上は増えません"},
		{EXTRA_RECOMMENDED_TITLE,		"新ゲームの紹介\n将棋駒でパズル"},

		{TWITTER,						"レトロ感満載のゲームがあるっぽい！"},
		{APPLICATION_QUIT,				"ゲームを終了しますか？"},

		{ERROR_NONAME,					"名前を正しく入力してください\n※全角半角英数字14文字まで"},
		{ERROR_NOPASSWORD,				"パスワードを正しく入力してください\n※半角英数字"},
		{ERROR_LOGIN_INCORRECT,			"ログインに失敗しました\nもう一度お試しください"},
		{ERROR_SIGNUP_NOMATCH_PASSWORD,	"パスワードが一致していません"},
		{ERROR_SIGNUP_INCORRECT,		"登録に失敗しました\n名前はすでに使われています"},
		{LOGIN,							"ログインしました"},
		{SIGNUP,						"登録が完了しました"},
		{CONNECTING,					"接続中..."},
		{RANKING_NAME,					"お名前:"},
		{RANKING_PASSWORD,				"パスワード:"},
		{RANKING_PASSWORD_CONFIRM,		"パスワード(確認):"},
		{RANKING_NAME_FORM,				"全角半角英数字14文字まで"},
		{RANKING_PASSWORD_FORM,			"半角英数字"},
		{LOGOUT,						"ログアウトしました"},
		{REQUEST_OVERLOAD,				"申し訳ございません。\nランキングサーバーの使用制限が超過しました。\n次月までお待ちください。"},
		{RANKING_THISSYSTEM,			"ランキングには、NIFTYクラウドを利用しています。"},
		{CHARANAME_SAMURAI,				"プレイヤー"},
		{CHARANAME_KUNOICHI,			"くノ一"},
		{CHARANAME_NINJA,				"忍者"},
		{CHARANAME_MIKO,				"巫女"},
		{CHARANAME_MUSUME,				"町娘"},
		{CHARANAME_HACHI,				"ハチ"},
		{YOUGOTCHARA,					"キャラを獲得しました！"},
		{LIFEIS1UP,						"残機が１つ増えました。"},
		{GACHA_RESULT_GACHATICKETTEXT,	"持っているガチャチケット数"},
		{GACHA_LOTTERY_CHARA,			"キャラクターを引く確率 20%"},
		{GACHA_LOTTERY_LIFE,			"残機数アップを引く確率 80%"},
		{GACHA_SKIP,					"タップでスキップ出来ます"},
		{LOGIN_BONUS_TEXT,				"いつも遊んでくれて\nありがとう！\nガチャチケットを2枚プレゼントします。\n\n<color=#ff0000>※アプリを起動すると、１日１回ガチャチケットが手に入る時があります。</color>"},
		{RANKING_FIRST_EXPLANATION,		"名前とパスワードは、登録完了時に自動で割り降られます。※後から変更可能です。\n登録完了後、名前とパスワードはアプリ内に保持しますので、次回よりランキングを選択後は、自動でログインします。"},
		{RANKING_REGISTED_EXPLANATION,	"アプリを消したり、データ消去を行うと再度、登録から行う必要があります。ご注意ください。"},
		{LOGIN_BIGINNER,				"初めての方へ"},
		{LOGIN_FINISH,					"完了しました"},
		{RANKING_NAME_CHANGE_CORRECT,	"成功しました"},
		{RANKING_NAME_CHANGE_INCORRECT,	"失敗しました\n別の名前で再度変更してください"},
		{RANKING_NAME_CHANGE,			"変更する"},
		{OFFLINE,						"オフラインのため利用出来ません"},
	};

	public static Dictionary<int, string> sentenceEn = new Dictionary<int, string> (){
		{START_CAUTION,					"Resume to play previous game if choose the CONTINUE button. Delete the previous game and play from 1st stage if choose START button. \n<color=#ff0000>Are you sure?</color>"},
		
		{GAME_READY_CONDITION,			"Condition of success."},
		{GAME_READY_APPEAR_NONE,		"Beat emenys all.Or run away by time zero."},
		{GAME_READY_APPEAR_SAMURAI,		"Beat without Security Samurai.Or run away by time zero."},
		{GAME_READY_APPEAR_BOSS,		"Beat the Boss!"},
		
		//{GAME_PAUSE,					"It is added 5 life\nif you will play movie."},
		//{GAME_CONTINUE,					"Restart to play from the stage when you choose\nCONTINUE button. And add a life if appear ads.\nThe movie ads begins when choose\nPLAY MOVIE button. If you finished to watch the\nmovie ads, you can continue to play with 5 life."},
		{GAME_PAUSE,					""},
		{GAME_CONTINUE,					"Restart to play from the stage when you choose\nCONTINUE button.\nIf you press the TITLE button to move to the title screen."},
		{GAME_HELP,						"This screen does not draw next time after.\nAlways confirm from help available."},
		{GAME_TUTORIAL_1,				"Beat enemies using tomb."},
		{GAME_TUTORIAL_2,				"Drop to the hole and fill it."},

		{RECORD_ENEMY_DIE_TO_TOMB,		"Total to beat enemy by tomb.\t"},
		{RECORD_ENEMY_DIE_TO_HOLE,		"Total to beat enemy for hole.\t"},
		{RECORD_TOMB_COLLAPSE,			"Total fell down tomb.\t\t\t"},
		{RECORD_HOLE_OPEN,				"Total to dig hole.\t\t\t\t"},
		{RECORD_HOLE_CLOSE,				"Total to fill hole.\t\t\t\t"},
		{RECORD_HOLE_FALL,				"Total to fall hole.\t\t\t\t"},
		{RECORD_BONUS_APPEAR,			"Total to appear bonus.\t\t\t"},
		{RECORD_BONUS_GET,				"Total to get a bonus.\t\t\t"},
		{RECORD_ITEM_GET,				"Total to get a item.\t\t\t"},
		{RECORD_DAMAGE,					"Total to decrease a life.\t\t"},
		{RECORD_ESCAPE,					"Total to ran away.\t\t\t\t"},
		{RECORD_MAX_TOMB_COLLAPSE,		"Num a tomb down at a time.\t\t"},
		{RECORD_SCORE_ALL,				"Total score in game.\t\t\t"},
		{RECORD_CLEAR_TIME,				"Best time of stage {0}."},
		
		{EXTRA_DESCRIPTION,				"If you watch a movie,\ncan play advantageous from start."},
		{EXTRA_ITEM_TITLE,				"Get the items"},
		{EXTRA_ITEM_DESCRIPTION,		"You can have items from start."},
		{EXTRA_ITEM_DESCRIPTION_HAVE,	"Gained already."},
		{EXTRA_LIFE_TITLE,				"Life + 5"},
		{EXTRA_LIFE_DESCRIPTION,		"Life of present.\n99 or more can not be added."},
		{EXTRA_RECOMMENDED_TITLE,		"Intro new game.\nShogi Puzzle."},

		{TWITTER,						"Let's play Samurai game!"},
		{APPLICATION_QUIT,				"Quit the Game?"},

		{ERROR_NONAME,					"A string of up to 14 characters and\nhalf-width alphanumeric."},
		{ERROR_NOPASSWORD,				"Incrrect password.\nhalf-width alphanumeric."},
		{ERROR_LOGIN_INCORRECT,			"The login attempt failed.\nPlease try again."},
		{ERROR_SIGNUP_NOMATCH_PASSWORD,	"No match password."},
		{ERROR_SIGNUP_INCORRECT,		"The regist attempt failed.\nAlready using user name."},
		{LOGIN,							"Logined."},
		{SIGNUP,						"Regist Completed."},
		{CONNECTING,					"Connecting..."},
		{RANKING_NAME,					"Name:"},
		{RANKING_PASSWORD,				"Password:"},
		{RANKING_PASSWORD_CONFIRM,		"   Re-Password:"},
		{RANKING_NAME_FORM,				"Half-width characters."},
		{RANKING_PASSWORD_FORM,			"Half-width characters."},
		{LOGOUT,						"Logged out."},
		{REQUEST_OVERLOAD,				"So sorry. To request is over load\nto ranking server.\nPlease wait next month."},
		{RANKING_THISSYSTEM,			"This ranking system uses NIFTY Cloud."},
		{CHARANAME_SAMURAI,				"PLAYER"},
		{CHARANAME_KUNOICHI,			"Kunoichi"},
		{CHARANAME_NINJA,				"Ninja"},
		{CHARANAME_MIKO,				"Miko"},
		{CHARANAME_MUSUME,				"Musume"},
		{CHARANAME_HACHI,				"Hachi"},
		{YOUGOTCHARA,					"You got a character！"},
		{LIFEIS1UP,						"Life is 1UP."},
		{GACHA_RESULT_GACHATICKETTEXT,	"You have Gacha Ticket."},
		{GACHA_LOTTERY_CHARA,			"Lottery of Character 20%"},
		{GACHA_LOTTERY_LIFE,			"Lottery of Life up 80%"},
		{GACHA_SKIP,					"Tap to skip"},
		{LOGIN_BONUS_TEXT,				"Thank you playing all the time!\nIt's presents two Gacha Ticket for you.\n\n<color=#ff0000>*When you start the game, you may get the Gacha Ticket once a day possibility.</color>"},
		{RANKING_FIRST_EXPLANATION,		"Your name and password are decided automatic when registed. *Possible to change after registed. After finish of regist, your name and password save in application. It is logined automatic next time and more. When you application uninstalled, or removed data of the smartphone, please login again."},
		{RANKING_REGISTED_EXPLANATION,	"If the appliation is removed or the data is removed, need to regist again. Please be careful."},
		{LOGIN_BIGINNER,				"For the first time"},
		{LOGIN_FINISH,					"Finish to regist"},
		{RANKING_NAME_CHANGE_CORRECT,	"Correct!"},
		{RANKING_NAME_CHANGE_INCORRECT,	"Incorrect.\nPlease another name."},
		{RANKING_NAME_CHANGE,			"Change"},
		{OFFLINE,						"No using for offline."},
	};
}
