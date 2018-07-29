using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



namespace Hakaima
{
	
	public class Item
	{
		
		public enum Type
		{
			None,
			Sandal,
			Hoe,
			Stone,
			Amulet,
			Parasol,
			Weapon,
		}


		public const float SIZE		= Data.SIZE_CHIP;
		public const int LAYER		= Data.LAYER_2;


		public Type type			{ get; private set; }
		public float positionX		{ get; private set; }
		public float positionY		{ get; private set; }
		public int layer			{ get; private set; }
		public bool visible			{ get; private set; }

		public int pointX			{ get; private set; }
		public int pointY			{ get; private set; }
		public float size			{ get; private set; }


		public void Init (Type type, int pointX, int pointY)
		{
			this.type = type;
			this.pointX = pointX;
			this.pointY = pointY;
			this.size = SIZE;
			
			this.positionX = this.size * this.pointX;
			this.positionY = this.size * this.pointY;
			this.layer = LAYER;
			this.visible = false;
		}


		public void Appear ()
		{
			this.visible = true;
		}
	}
	
}
