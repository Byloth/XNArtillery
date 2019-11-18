using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ByloEngine;

namespace XNArtillery.Utility
{
    public class Functions
    {
        public static Vector2 Add(Vector2 values, Vector2 addend)
        {
            values.X += addend.X;
            values.Y += addend.Y;

            return values;
        }
        public static Vector2 Subtract(Vector2 values, Vector2 subtrahend)
        {
            values.X -= subtrahend.X;
            values.Y -= subtrahend.Y;

            return values;
        }

        public static int ResizeByWidth(float value)
        {
            return (int)(value * Global.MyResolution.Width) / Global.MaxWidth;
        }
        public static int ResizeByHeight(float value)
        {
            return (int)(value * Global.MyResolution.Height) / Global.MaxHeight;
        }

        public static int SizeByWidth(float value)
        {
            return (int)((value * Global.MaxWidth) / Global.MyResolution.Width);
        }
        public static int SizeByHeight(float value)
        {
            return (int)((value * Global.MaxHeight) / Global.MyResolution.Height);
        }

        public static Rectangle ResizedRectangle(float width, float height)
        {
            width = ResizeByWidth(width);
            height = ResizeByHeight(height);

            return new Rectangle(0, 0, (int)width, (int)height);
        }

        public static Vector2 Size(Vector2 values)
        {
            values.X = SizeByWidth(values.X);
            values.Y = SizeByHeight(values.Y);

            return values;
        }

        public static Vector2 SizedPosition(Rectangle values)
        {
            values.X = SizeByWidth(values.X);
            values.Y = SizeByHeight(values.Y);

            return new Vector2(values.X, values.Y);
        }

        public static bool IsInside(Vector2 position)
        {
            if ((position.X >= 0) & (position.Y >= 0) & (position.X <= Global.MyResolution.Width) & (position.Y <= Global.MyResolution.Height))
            {
                return true;
            }

            return false;
        }
    }
}
