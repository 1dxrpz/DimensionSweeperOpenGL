using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimensionSweeperOpenGL
{
	public class Camera
	{
		public float FOV = 120f * MathF.PI / 180f;
		public Vector3 Position = new Vector3();
		public Vector3 Rotation = new Vector3();
		public Vector3 LookDirection = new Vector3(0, 0, 0);

		public float Near = .001f;
		public float Far = 10f;

		public int Height = 480;
		public int Width = 800;
		bool CursorLock = true;

		public Point MouseDelta = new Point();

		public void Update()
		{
			if (CursorLock)
			{
				MouseDelta = Mouse.GetState().Position;
				Mouse.SetPosition(Width / 2, Height / 2);
				MouseDelta = new Point(Width / 2, Height / 2) - MouseDelta;
			}
		}
		public Matrix TransformMatrix()
		{
			float ar = Width / Height;
			float fac = 1 / MathF.Tan(FOV / 2);

			return new Matrix(
				new Vector4(-fac / ar, 0, 0, 0),
				new Vector4(0, -fac, 0, 0),
				new Vector4(0, 0, (Far + Near) / ((Far - Near)), 1),
				new Vector4(0, 0, -2 * (Far * Near) / ((Far - Near)), 0)
			);
		}
	}
}
