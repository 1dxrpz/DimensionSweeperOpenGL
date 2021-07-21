using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimensionSweeperOpenGL.Engine
{
	public class Mesh
	{
		public List<Vector3> Vertices = new List<Vector3>();
		public List<(int, int, int)> VertexBuffer = new List<(int, int, int)>();
		public void Update()
		{
			for (int i = 0; i < Vertices.Count; i++)
			{
				Utils.context.DrawCircle(
				new Vector2(
					Vertices[i].WorldToViewport(Utils.MainCamera).X + Utils.MainCamera.Width / 2f,
					Vertices[i].WorldToViewport(Utils.MainCamera).Y + Utils.MainCamera.Height / 2f),
				2, 5, Color.White
				);
			}
		}
	}
}
