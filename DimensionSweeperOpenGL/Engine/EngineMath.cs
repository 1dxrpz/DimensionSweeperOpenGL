using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimensionSweeperOpenGL.Engine
{
	public static class EngineMath
	{
		static public Texture2D GetTexture(Color color, GraphicsDevice graphicsDevice)
		{
			Color[] data = new Color[1] { color };
			Texture2D res = new Texture2D(graphicsDevice, 1, 1);
			res.SetData(data);
			return res;
		}
		static public Matrix Translate(Vector3 center)
		{
			return new Matrix(
				new Vector4(1, 0, 0, center.X),
				new Vector4(0, 1, 0, center.Y),
				new Vector4(0, 0, 1, center.Z),
				new Vector4(0, 0, 0, 1)
				);
		}
		static public Matrix MatrixVector(Vector3 vector)
		{
			return new Matrix(
				new Vector4(vector.X, 0, 0, 0),
				new Vector4(vector.Y, 0, 0, 0),
				new Vector4(vector.Z, 0, 0, 0),
				new Vector4(1, 0, 0, 0)
				);
		}
		static public float ScalarMultiply(this Vector3 vector, Vector3 mvector)
		{
			return (vector * mvector).X + (vector * mvector).Y + (vector * mvector).Z;
		}
		static public Vector3 RotateZ(this Vector3 vector, Vector3 center, float anglex = 0)
		{
			float r = GlobalParametrs.AngleType == AngleType.Degrees ? 180f / MathF.PI : 1;
			
			Matrix b = new Matrix(
				new Vector4(MathF.Cos(anglex / r), -MathF.Sin(anglex / r), 0, 0),
				new Vector4(MathF.Sin(anglex / r), MathF.Cos(anglex / r), 0, 0),
				new Vector4(0, 0, 1, 0),
				new Vector4(0, 0, 0, 1));

			Matrix res = b * MatrixVector(vector);
			return new Vector3(
				MathF.Round(res.M11, 2),
				MathF.Round(res.M21, 2),
				MathF.Round(res.M31, 2));
		}
		static public Vector3 RotateY(this Vector3 vector, Vector3 center, float angley = 0)
		{
			float r = GlobalParametrs.AngleType == AngleType.Degrees ? 180f / MathF.PI : 1f;
			Matrix c = new Matrix(
				new Vector4(MathF.Cos(angley / r), 0, MathF.Sin(angley / r), 0),
				new Vector4(0, 1, 0, 0),
				new Vector4(-MathF.Sin(angley / r), 0, MathF.Cos(angley / r), 0),
				new Vector4(0, 0, 0, 1));

			Matrix res = c * MatrixVector(vector);
			return new Vector3(
				MathF.Round(res.M11, 2),
				MathF.Round(res.M21, 2),
				MathF.Round(res.M31, 2));
		}
		static public Vector3 RotateX(this Vector3 vector, Vector3 center, float anglez)
		{
			float r = GlobalParametrs.AngleType == AngleType.Degrees ? 180f / MathF.PI : 1;
			Matrix b = new Matrix(
				new Vector4(1, 0, 0, 0),
				new Vector4(0, MathF.Cos(anglez / r), -MathF.Sin(anglez / r), 0),
				new Vector4(0, MathF.Sin(anglez / r), MathF.Cos(anglez / r), 0),
				new Vector4(0, 0, 0, 0));

			Matrix res = b * MatrixVector(vector);
			return new Vector3(
				MathF.Round(res.M11, 2),
				MathF.Round(res.M21, 2),
				MathF.Round(res.M31, 2));
		}
		static public Vector3 Rotate(this Vector3 vector, Vector3 center, Vector3 angles)
		{
			float r = GlobalParametrs.AngleType == AngleType.Degrees ? 180f / MathF.PI : 1;
			Matrix a = new Matrix(
				new Vector4(1, 0, 0, 0),
				new Vector4(0, MathF.Cos(angles.X / r), -MathF.Sin(angles.X / r), 0),
				new Vector4(0, MathF.Sin(angles.X / r), MathF.Cos(angles.X / r), 0),
				new Vector4(0, 0, 0, 0));
			Matrix b = new Matrix(
				new Vector4(MathF.Cos(angles.Z / r), -MathF.Sin(angles.Z / r), 0, 0),
				new Vector4(MathF.Sin(angles.Z / r), MathF.Cos(angles.Z / r), 0, 0),
				new Vector4(0, 0, 1, 0),
				new Vector4(0, 0, 0, 1));
			Matrix c = new Matrix(
				new Vector4(MathF.Cos(angles.Y / r), 0, MathF.Sin(angles.Y / r), 0),
				new Vector4(0, 1, 0, 0),
				new Vector4(-MathF.Sin(angles.Y / r), 0, MathF.Cos(angles.Y / r), 0),
				new Vector4(0, 0, 0, 1));

			Matrix res =  a * b * c * (MatrixVector(vector) - Translate(-center)) + Translate(-center);
			return new Vector3(
				MathF.Round(res.M11, 2),
				MathF.Round(res.M21, 2),
				MathF.Round(res.M31, 2));
		}

		static public Vector3 Scale(this Vector3 vector, float scalex = 1, float scaley = 1, float scalez = 1)
		{
			Matrix a = new Matrix(new Vector4(vector.X, 0, 0, 0), new Vector4(vector.Y, 0, 0, 0), new Vector4(vector.Z, 0, 0, 0), new Vector4());
			Matrix b = new Matrix(
				new Vector4(scalex, 0, 0, 0),
				new Vector4(0, scaley, 0, 0),
				new Vector4(0, 0, scalez, 0),
				new Vector4(0));
			Matrix res = b * a;
			return new Vector3(
				MathF.Round(res.M11, 2),
				MathF.Round(res.M21, 2),
				MathF.Round(res.M31, 2));
		}
		static public Vector3 Scale(this Vector3 vector, Vector3 scale)
		{
			Matrix a = new Matrix(
				new Vector4(vector.X, 0, 0, 0),
				new Vector4(vector.Y, 0, 0, 0),
				new Vector4(vector.Z, 0, 0, 0),
				new Vector4());
			Matrix b = new Matrix(
				new Vector4(scale.X, 0, 0, 0),
				new Vector4(0, scale.Y, 0, 0),
				new Vector4(0, 0, scale.Z, 0),
				new Vector4(0));
			Matrix res = b * a;
			return new Vector3(
				MathF.Round(res.M11, 2),
				MathF.Round(res.M21, 2),
				MathF.Round(res.M31, 2));
		}
		
		static public Vector3 WorldToViewport(this Vector3 vector, Camera camera)
		{
			Matrix tm = camera.TransformMatrix() * (MatrixVector(vector.Rotate(new Vector3(), camera.LookDirection)) + MatrixVector(camera.Position));
			
			return new Vector3(tm.M11 / tm.M41, tm.M21 / tm.M41, tm.M31 / tm.M41);
		}
	}
}
