using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



namespace Hakaima
{
	
	public class Bonus
	{
		
		public enum Type
		{
			None,
			Bonus0,
			Bonus1,
			Bonus2,
			Bonus3,
			Bonus4,
			Bonus5,
			Bonus6,
		}
		
		
		public const float SIZE		= Data.SIZE_CHIP;
		public const int LAYER		= Data.LAYER_5;
		
		
		public Type type			{ get; private set; }
		public float positionX		{ get; private set; }
		public float positionY		{ get; private set; }
		public int layer			{ get; private set; }
		public bool visible			{ get; private set; }
		public Color color			{ get; private set; }
		
		public int pointX			{ get; private set; }
		public int pointY			{ get; private set; }
		public float size			{ get; private set; }
		
		public bool blind			{ get; private set; }
		public float blindTime		{ get; private set; }


		public void Init (Type type, int pointX, int pointY)
		{
			this.type = type;
			this.pointX = pointX;
			this.pointY = pointY;
			this.size = SIZE;
			
			this.positionX = this.size * this.pointX;
			this.positionY = this.size * this.pointY;
			this.layer = LAYER;
			this.visible = true;
			this.color = Color.white;
		}
		
		
		public void Move (float deltaTime, int frameRate)
		{
			if (blind) {
				Color color = Color.white;
				color.a = ((int)(this.blindTime * 2) % 2 == 0) ? 1 : 0;
				this.color = color;
				this.blindTime += deltaTime * 2f;
			} else {
				Color color = Color.white;
				color.a = 1;
				this.color = color;
			}
		}
		
		
		public void SetBlind (bool blind)
		{
			this.blind = blind;
			this.blindTime = 0;
		}
	}
	
}
