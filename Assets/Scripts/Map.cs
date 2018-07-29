using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



namespace Hakaima
{

	public class Terrain
	{

		public enum Type
		{
			None,
			Soil,
			Grass,
			Muddy,
			Pavement,
			Ice,
			River,
			Bridge,
			BridgeVertical,
			BridgeHorizontal,
		}


		public const int IMAGE_0	= 0;
		public const int IMAGE_1	= 1;
		public const int IMAGE_2	= 2;

		public const int LAYER		= Data.LAYER_0;


		public Type type			{ get; private set; }
		public int layer			{ get; private set; }
		public bool visible			{ get; private set; }
		public int imageIndex		{ get; private set; }

		private float imageTime;

		
		public void Set (Type type)
		{
			this.type = type;
			this.layer = LAYER;
			this.imageIndex = IMAGE_0;
			this.imageTime = 0;

			switch (this.type) {
			case Type.None:
				this.visible = false;
				break;
			default:
				this.visible = true;
				break;
			}
		}

		
		public void Move (float deltaTime, int frameRate)
		{
			switch (this.type) {
			case Type.River:
				{
					int index = (int)this.imageTime % 2;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += deltaTime * 3;
				}
				break;
			}
		}
	}



	public class Obstacle
	{

		public enum Type
		{
			None,
			Tree,
			Stone,
			Tomb,
			TombCollapse,
			TombCollapseEnd,
			TombPiece,
			TombPieceEnd,
			CartRight,
			CartLeft,
			Well,
			Bale,
			Bathtub,
			BathtubCollapse,
			BathtubCollapseEnd,
			Stockpile,
			StockpileCollapse,
			StockpileCollapseEnd,
			Signboard,
			Tower,
			FallenTree,
			Stump,
			Bucket,
			Lantern,
			StupaFence,
			Stupa,
			LargeTreeRight,
			LargeTreeLeft,
			Rubble,
			RubbleOff,
			Picket,
			FallTombPiece,
			FallTombPieceEnd,
		}


		public const int IMAGE_0		= 0;
		public const int IMAGE_1		= 1;
		public const int IMAGE_2		= 2;
		public const int IMAGE_3		= 3;
		public const int IMAGE_4		= 4;

		public const int LAYER_HIGH		= Data.LAYER_10;
		public const int LAYER_MIDDLE	= Data.LAYER_4;
		public const int LAYER_LOW		= Data.LAYER_3;


		public Type type			{ get; private set; }
		public int layer			{ get; private set; }
		public bool visible			{ get; private set; }
		public int imageIndex		{ get; private set; }

		private float imageTime;
		
		
		public void Set (Type type)
		{
			this.type = type;
			this.imageIndex = IMAGE_0;
			this.imageTime = 0;

			switch (this.type) {
			case Type.None:
			case Type.FallTombPiece:
			case Type.FallTombPieceEnd:
				this.visible = false;
				break;
			default:
				this.visible = true;
				break;
			}

			switch (this.type) {
			case Type.Tree:
			case Type.Tower:
			case Type.LargeTreeRight:
			case Type.LargeTreeLeft:
			case Type.Tomb:
			case Type.Lantern:
			case Type.Stupa:
			case Type.Rubble:
			case Type.RubbleOff:
				this.layer = LAYER_HIGH;
				break;
			case Type.TombCollapseEnd:
			case Type.TombPieceEnd:
			case Type.Bucket:
			case Type.FallTombPiece:
			case Type.FallTombPieceEnd:
				this.layer = LAYER_LOW;
				break;
			default:
				this.layer = LAYER_MIDDLE;
				break;
			}
		}

		
		public void Move (float deltaTime, int frameRate)
		{
			switch (this.type) {
			case Type.TombCollapse:
				{
					int index = (int)this.imageTime % 3;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += deltaTime * 6;
					if (index == 2) {
						this.Set (Type.TombCollapseEnd);
					}
				}
				break;
			case Type.TombPiece:
				{
					int index = (int)this.imageTime % 3;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += deltaTime * 6;
					if (index == 2) {
						this.Set (Type.TombPieceEnd);
					}
				}
				break;
			case Type.Bathtub:
				{
					int index = (int)this.imageTime % 3;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					case 2:
						this.imageIndex = IMAGE_2;
						break;
					}
					this.imageTime += deltaTime * 6;
				}
				break;
			case Type.BathtubCollapse:
				{
					int index = (int)this.imageTime % 3;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += deltaTime * 6;
					if (index == 2) {
						this.Set (Type.BathtubCollapseEnd);
					}
				}
				break;
			case Type.StockpileCollapse:
				{
					int index = (int)this.imageTime % 3;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += deltaTime * 6;
					if (index == 2) {
						this.Set (Type.StockpileCollapseEnd);
					}
				}
				break;
			case Type.Tower:
				{
					int index = (int)this.imageTime % 8;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					case 2:
						this.imageIndex = IMAGE_2;
						break;
					case 3:
						this.imageIndex = IMAGE_3;
						break;
					case 4:
						this.imageIndex = IMAGE_4;
						break;
					case 5:
						this.imageIndex = IMAGE_3;
						break;
					case 6:
						this.imageIndex = IMAGE_2;
						break;
					case 7:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += deltaTime * 6;
				}
				break;
			case Type.FallTombPiece:
				{
					int index = (int)this.imageTime % 3;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += deltaTime * 6;
					if (index == 2) {
						this.Set (Type.FallTombPieceEnd);
					}
				}
				break;
			}
		}
	}
	
	
	
	public class Hole
	{
		
		public enum State
		{
			Close,
			Middle1,
			Middle2,
			Middle3,
			Open,
		}


		public const int IMAGE_MIDDLE_1		= 1;
		public const int IMAGE_MIDDLE_2		= 2;
		public const int IMAGE_MIDDLE_3		= 3;
		public const int IMAGE_COMPLETE		= 4;

		public const int LAYER				= Data.LAYER_1;

		
		public State state			{ get; private set; }
		public int layer			{ get; private set; }
		public bool visible			{ get; private set; }
		public int imageIndex		{ get; private set; }

		private bool isOpen;


		private void Set (State state)
		{
			this.state = state;
			this.layer = LAYER;

			switch (this.state) {
			case State.Close:
				{
					this.visible = false;
				}
				break;
			case State.Middle1:
				{
					this.visible = true;
					this.imageIndex = IMAGE_MIDDLE_1;
				}
				break;
			case State.Middle2:
				{
					this.visible = true;
					this.imageIndex = IMAGE_MIDDLE_2;
				}
				break;
			case State.Middle3:
				{
					this.visible = true;
					this.imageIndex = IMAGE_MIDDLE_3;
				}
				break;
			case State.Open:
				{
					this.visible = true;
					this.imageIndex = IMAGE_COMPLETE;
				}
				break;
			}
		}


		public void Cycle ()
		{
			switch (this.state) {
			case State.Close:
				{
					this.Set (State.Middle1);
					this.isOpen = true;
				}
				break;
			case State.Middle1:
				{
					this.Set (isOpen ? State.Middle2 : State.Close);
				}
				break;
			case State.Middle2:
				{
					this.Set (isOpen ? State.Middle3 : State.Middle1);
				}
				break;
			case State.Middle3:
				{
					this.Set (isOpen ? State.Open : State.Middle2);
				}
				break;
			case State.Open:
				{
					this.Set (State.Middle3);
					this.isOpen = false;
				}
				break;
			}
		}


		public void Close ()
		{
			this.Set (State.Close);
		}
		
		
		public void Open ()
		{
			this.Set (State.Open);
		}
	}



	public class Chip
	{

		public const float SIZE		= Data.SIZE_CHIP;


		public Terrain terrain				{ get; private set; }
		public Hole hole					{ get; private set; }
		public List<Obstacle> obstacleList	{ get; private set; }
		public float positionX				{ get; private set; }
		public float positionY				{ get; private set; }

		public int pointX					{ get; private set; }
		public int pointY					{ get; private set; }
		public float size					{ get; private set; }


		public Chip ()
		{
			this.terrain = new Terrain ();
			this.obstacleList = new List<Obstacle> ();
			this.hole = new Hole ();
		}


		public void Init (int pointX, int pointY)
		{
			this.pointX = pointX;
			this.pointY = pointY;
			this.size = SIZE;

			this.positionX = this.size * this.pointX;
			this.positionY = this.size * this.pointY;
		}
		
		
		public void Move (float deltaTime, int frameRate)
		{
			this.terrain.Move (deltaTime, frameRate);
			this.obstacleList.ForEach (obj => obj.Move (deltaTime, frameRate));
		}

	}

}
