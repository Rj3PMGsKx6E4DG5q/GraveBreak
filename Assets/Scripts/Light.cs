using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



namespace Hakaima
{
	
	public class Light
	{
		
		public const float SIZE		= Data.SIZE_CHIP;

		
		public float positionX		{ get; private set; }
		public float positionY		{ get; private set; }
		public int layer			{ get; private set; }
		public bool visible			{ get; private set; }
		
		public int pointX			{ get; private set; }
		public int pointY			{ get; private set; }
		public float size			{ get; private set; }
		
		
		public void Init (int pointX, int pointY, int layer)
		{
			this.pointX = pointX;
			this.pointY = pointY;
			this.size = SIZE;
			
			this.positionX = this.size * this.pointX;
			this.positionY = this.size * this.pointY;
			this.layer = layer;
			this.visible = true;
		}
	}
	
}
