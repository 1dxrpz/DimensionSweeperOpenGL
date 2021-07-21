using DimensionSweeperOpenGL.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DimensionSweeperOpenGL
{
	public class Game1 : Game
	{
		[DllImport("kernel32")]
		static extern bool AllocConsole();
		private GraphicsDeviceManager _graphics;
		private SpriteBatch ctx;

		Camera Camera = new Camera();
		DSDL Light = new DSDL();
		Mesh cube = new Mesh();

		public Game1()
		{
			AllocConsole();
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = false;
		}
		protected override void Initialize()
		{
			base.Initialize();
			cube.Vertices.Add(new Vector3(0, 0, 0));
			cube.Vertices.Add(new Vector3(0, 10, 0));
			cube.Vertices.Add(new Vector3(10, 10, 0));
			Utils.context = ctx;
		}
		protected override void LoadContent()
		{
			ctx = new SpriteBatch(GraphicsDevice);
			GlobalParametrs.AngleType = AngleType.Degrees;
			Camera.Position = new Vector3(-20f, 20f, 50f);
			Utils.MainCamera = Camera;
		}

		List<Vector3> Vertices = new List<Vector3>()
		{
			new Vector3(0, -10, 1),
			new Vector3(0, 0, 1),
			new Vector3(-10, 0, 1),
			new Vector3(-10, -10, 1),
			new Vector3(0, 0, 10f),
			new Vector3(-10, 0, 10f),
			new Vector3(0, -10, 10f),
			new Vector3(-10, -10, 10f)
		};
		Vector3 p4 = new Vector3(10, 0, 10);
		Vector3 p5 = new Vector3(10, 0, 9);
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			if (Keyboard.GetState().IsKeyDown(Keys.S))
			{
				Camera.Position.Z += 1f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.W))
			{
				Camera.Position.Z -= 1f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D))
			{
				Camera.Position.X -= 1f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.A))
			{
				Camera.Position.X += 1f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Q))
			{
				Camera.Position.Y += 1f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.E))
			{
				Camera.Position.Y -= 1f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D1))
			{
				Camera.FOV += .1f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D2))
			{
				Camera.FOV -= .1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.D3))
			{
				for (int i = 0; i < Vertices.Count; i++)
				{
					Vertices[i] = Vertices[i].RotateY(new Vector3(), MathF.PI / 2);
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D4))
			{
				for (int i = 0; i < Vertices.Count; i++)
				{
					Vertices[i] = Vertices[i].RotateZ(new Vector3(), MathF.PI / 2);
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D5))
			{
				for (int i = 0; i < Vertices.Count; i++)
				{
					Vertices[i] = Vertices[i].RotateX(new Vector3(), MathF.PI / 2);
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D6))
			{
				for (int i = 0; i < Vertices.Count; i++)
				{
					Vertices[i] = Vertices[i].Rotate(new Vector3(5, 5, 5), new Vector3(MathF.PI / 2, MathF.PI / 2, MathF.PI / 2));
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D7))
			{
				Light.Rotation = Light.Rotation.Rotate(new Vector3(), new Vector3(1, 1, 1));
				Console.WriteLine(Light.Rotation);
				Console.WriteLine(-Vector3.Normalize(Vector3.Cross(p4, p5)).ScalarMultiply(Light.Rotation));
			}
			
			
			if (Keyboard.GetState().IsKeyDown(Keys.Z))
			{
				Camera.LookDirection.X += Camera.MouseDelta.Y;
				Camera.LookDirection.Y += Camera.MouseDelta.X;
			}
			Camera.Update();
			base.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime)
		{
			
			Camera.Width = _graphics.PreferredBackBufferWidth;
			Camera.Height = _graphics.PreferredBackBufferHeight;
			
			GraphicsDevice.Clear(Color.Black);
			ctx.Begin();


			cube.Update();
			// DrawEdge(Vertices[0], Vertices[1], Color.White);
			// DrawEdge(Vertices[0], Vertices[3], Color.White);
			// DrawEdge(Vertices[1], Vertices[2], Color.White);
			// DrawEdge(Vertices[2], Vertices[3], Color.White);
			// DrawEdge(Vertices[4], Vertices[5], Color.White);
			// DrawEdge(Vertices[4], Vertices[6], Color.White);
			// DrawEdge(Vertices[5], Vertices[7], Color.White);
			// DrawEdge(Vertices[6], Vertices[7], Color.White);
			// DrawEdge(Vertices[0], Vertices[6], Color.White);
			// DrawEdge(Vertices[1], Vertices[4], Color.White);
			// DrawEdge(Vertices[2], Vertices[5], Color.White);
			// DrawEdge(Vertices[3], Vertices[7], Color.White);


			DrawEdge(new Vector3(100, 0, 0), new Vector3(-100, 0, 0), Color.Red);
			DrawEdge(new Vector3(0, 0, 100), new Vector3(0, 0, -100), Color.Blue);
			DrawEdge(new Vector3(0, 100, 0), new Vector3(0, -100, 0), Color.Yellow);

			//ctx.Draw(EngineMath.GetTexture(new Color(1, 1, 1, 1f), _graphics.GraphicsDevice),
			//	new Rectangle(0, 0, 100, 100), new Color(255, 255, 255) * -Vector3.Normalize(Vector3.Cross(p4, p5)).ScalarMultiply(Light.Rotation));
			ctx.End();
			base.Draw(gameTime);
		}
		void DrawEdge(Vector3 a, Vector3 b, Color color)
		{
			ctx.DrawLine(
					a.WorldToViewport(Camera).X + _graphics.PreferredBackBufferWidth / 2,
					a.WorldToViewport(Camera).Y + _graphics.PreferredBackBufferHeight / 2,
					b.WorldToViewport(Camera).X + _graphics.PreferredBackBufferWidth / 2,
					b.WorldToViewport(Camera).Y + _graphics.PreferredBackBufferHeight / 2,
					color
			);
		}
	}
	
}
