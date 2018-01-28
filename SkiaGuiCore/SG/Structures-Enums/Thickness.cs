using SkiaSharp;

namespace SkiaGuiCore.SG
{
    public struct Thickness
    {
        public Thickness(float thickness)
        {
            Left = Right = Top = Bottom = thickness;
        }

        public Thickness(float leftRight, float topBottom)
        {
            Left = Right = leftRight;
            Top = Bottom = topBottom;
        }

        public Thickness(float left, float top, float right, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
        public SKPointI Location => new SKPointI((int)Left, (int)Top);
        public bool IsSymmetric => Left == Right && Top == Bottom && Left == Top;
        public bool IsEmpty => IsSymmetric && Left == 0;

        public static Thickness Parse(string thickness)
        {
            if (string.IsNullOrWhiteSpace(thickness)) return default(Thickness);

            var v = thickness.Split(',');

            if (v.Length == 1)
            {
                return new Thickness(float.Parse(v[0]));
            }
            if (v.Length == 2)
            {
                return new Thickness(float.Parse(v[0]), float.Parse(v[1]));
            }

            return new Thickness(float.Parse(v[0]), float.Parse(v[1]), float.Parse(v[2]), float.Parse(v[3]));
        }

        public override string ToString()
        {
            if (Left == Right && Top == Bottom)
            {
                if (Left == Top) return $"{Left}";
                return $"{Left},{Top}";
            }
            return $"{Left},{Top},{Right},{Bottom}";
        }
    }
}