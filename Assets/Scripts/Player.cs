using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



namespace Hakaima
{

	public class Player
	{
		
		public enum State
		{
			None,
			Wait,
			Walk,
			Fall,
			Damage,
			Vanish,
		}

		public enum Compass
		{
			Right,
			Left,
			Top,
			Bottom,
		}


		public const int IMAGE_0		= 0;
		public const int IMAGE_1		= 1;
		public const int IMAGE_2		= 2;
		
		public const float SIZE			= Data.SIZE_CHIP;
		public const int LAYER			= Data.LAYER_8;
		public const int LAYER_FALL		= Data.LAYER_6;


		public State state				{ get; private set; }
		public float time				{ get; private set; }
		public bool isEnd				{ get; private set; }
		public Compass compass			{ get; private set; }

		public float positionX			{ get; private set; }
		public float positionY			{ get; private set; }
		public int layer				{ get; private set; }
		public bool visible				{ get; private set; }
		public Color color				{ get; private set; }
		public int imageIndex			{ get; private set; }
		public bool spin				{ get; private set; }

		public int pointX				{ get; private set; }
		public int pointY				{ get; private set; }
		public float size				{ get; private set; }
		public float speed				{ get; private set; }
		public bool blind				{ get; private set; }
		public Color blindColor			{ get; private set; }
		public float imageCoefficient	{ get; private set; }

		private State preState;
		private float preTime;
		private bool preIsEnd;
		private Compass preCompass;

		private float imageTime;
		private float blindTime;


		public Player ()
		{
			this.None ();
		}


		public void Init (int pointX, int pointY)
		{
			this.compass = Compass.Bottom;
			this.pointX = pointX;
			this.pointY = pointY;
			this.size = SIZE;

			this.positionX = this.size * this.pointX;
			this.positionY = this.size * this.pointY;
			this.layer = LAYER;
			this.visible = true;
			this.color = new Color (0, 0, 0, 1);
			this.imageIndex = IMAGE_0;
			this.Wait ();
		}


		public void Move (float deltaTime, int frameRate)
		{
			bool loop;
			do {
				loop = false;

				switch (this.state) {
				case State.Wait:
					{
						this.positionX = this.size * this.pointX;
						this.positionY = this.size * this.pointY;
					}
					break;
				case State.Walk:
					{
						switch (this.compass) {
						case Compass.Right:
							{
								this.positionX += this.speed;
								this.positionY = this.size * this.pointY;

								if (this.positionX >= this.size * this.pointX) {
									this.positionX = this.size * this.pointX;
									this.isEnd = true;
									this.Wait ();
									loop = true;
								}
							}
							break;
						case Compass.Left:
							{
								this.positionX -= this.speed;
								this.positionY = this.size * this.pointY;

								if (this.positionX <= this.size * this.pointX) {
									this.positionX = this.size * this.pointX;
									this.isEnd = true;
									this.Wait ();
									loop = true;
								}
							}
							break;
						case Compass.Top:
							{
								this.positionX = this.size * this.pointX;
								this.positionY += this.speed;

								if (this.positionY >= this.size * this.pointY) {
									this.positionY = this.size * this.pointY;
									this.isEnd = true;
									this.Wait ();
									loop = true;
								}
							}
							break;
						case Compass.Bottom:
							{
								this.positionX = this.size * this.pointX;
								this.positionY -= this.speed;

								if (this.positionY <= this.size * this.pointY) {
									this.positionY = this.size * this.pointY;
									this.isEnd = true;
									this.Wait ();
									loop = true;
								}
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
						this.imageTime += this.speed * this.imageCoefficient;
					}
					break;
				case State.Fall:
					{
					}
					break;
				case State.Damage:
					{
						float rot = time * 5 * 180 % 360;
						if (rot <= 45 + 90 * 0) {
							this.compass = Compass.Right;
						} else if (rot <= 45 + 90 * 1) {
							this.compass = Compass.Top;
						} else if (rot <= 45 + 90 * 2) {
							this.compass = Compass.Left;
						} else if (rot <= 45 + 90 * 3) {
							this.compass = Compass.Bottom;
						} else {
							this.compass = Compass.Right;
						}
						if (this.time >= 3f) {
							this.isEnd = true;
						}
					}
					break;
				case State.Vanish:
					{
					}
					break;
				}
			} while (loop);

			if (blind) {
				Color col = Math.Abs ((float)Math.Sin (Math.PI * this.blindTime)) * this.blindColor;
				this.color = new Color (col.r, col.g, col.b, this.color.a);
				this.blindTime += deltaTime * 2f;
			} else {
				this.color = new Color (0, 0, 0, this.color.a);
			}

			this.time += deltaTime;
		}


		private void None ()
		{
			if (this.state == State.None) {
				this.state = State.None;
				this.time = 0;
				this.isEnd = true;
			}
		}


		private void Wait ()
		{
			if (this.state == State.None || this.state == State.Walk || this.state == State.Fall) {
				this.state = State.Wait;
				this.time = 0;
				this.isEnd = true;
			}
		}


		public void Walk (Compass compass, bool isWalk)
		{
			if (this.state == State.Wait) {
				if (this.compass != compass) {
					this.compass = compass;
					this.imageIndex = IMAGE_0;
					this.imageTime = 0;
				}
				if (isWalk) {
					this.state = State.Walk;
					this.time = 0;
					this.isEnd = false;

					switch (this.compass) {
					case Compass.Right:
						this.pointX++;
						break;
					case Compass.Left:
						this.pointX--;
						break;
					case Compass.Top:
						this.pointY++;
						break;
					case Compass.Bottom:
						this.pointY--;
						break;
					}
				}
			}
		}
		
		
		public void Fall ()
		{
			if (this.state == State.Wait) {
				this.state = State.Fall;
				this.time = 0;
				this.isEnd = true;
				this.layer = LAYER_FALL;
			}
		}
		
		
		public void Climb ()
		{
			if (this.state == State.Fall) {
				this.Wait ();
				this.layer = LAYER;
			}
		}


		public void Damage ()
		{
			if (this.state == State.Wait || this.state == State.Walk || this.state == State.Fall) {
				this.preState = this.state;
				this.preTime = this.time;
				this.preIsEnd = this.isEnd;
				this.preCompass = this.compass;

				this.state = State.Damage;
				this.time = 0;
				this.isEnd = false;

				this.spin = true;
				this.imageIndex = IMAGE_0;
				this.imageTime = 0;
			}
		}


		public void Rise ()
		{
			if (this.state == State.Damage) {
				this.state = this.preState;
				this.time = this.preTime;
				this.isEnd = this.preIsEnd;
				this.compass = this.preCompass;

				this.spin = false;
				this.imageIndex = IMAGE_0;
				this.imageTime = 0;
			}
		}


		public void Vanish ()
		{
			if (this.state == State.Wait || this.state == State.Walk || this.state == State.Fall) {
				this.state = State.Vanish;
				this.visible = false;
			}
		}
		
		
		public void InSide ()
		{
			if (this.state == State.Wait || this.state == State.Walk) {
				this.layer = LAYER_FALL;
			}
		}
		
		
		public void OutSide ()
		{
			if (this.state == State.Wait || this.state == State.Walk) {
				this.layer = LAYER;
			}
		}


		public void SetPoint (int pointX, int pointY)
		{
			if (this.state == State.Wait) {
				this.pointX = pointX;
				this.pointY = pointY;
				this.positionX = this.size * this.pointX;
				this.positionY = this.size * this.pointY;
			}
		}
		
		
		public void SetSpeed (float speed)
		{
			if (this.state == State.Wait || this.state == State.Walk) {
				this.speed = speed;
			}
		}


		public void SetImageCoefficient (float imageCoefficient)
		{
			if (this.state == State.Wait || this.state == State.Walk) {
				this.imageCoefficient = imageCoefficient;
			}
		}
		
		
		public void SetBlind (bool blind, Color blindColor = default (Color))
		{
			if (this.state == State.Wait || this.state == State.Walk) {
				this.blind = blind;
				this.blindTime = 0;
				this.blindColor = blindColor;
				this.color = new Color (0, 0, 0, 1);
			}
		}
	}
}

