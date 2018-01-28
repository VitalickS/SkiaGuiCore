namespace SkiaGuiCore.SG
{
    public struct CornerRadius
    {
        public CornerRadius(float radius = 0)
        {
            TopLeft = TopRight = BottomLeft = BottomRight = radius;
            IsSymetric = TopLeft == TopRight && BottomLeft == BottomRight && TopLeft == BottomLeft;
        }
        public CornerRadius(float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
            IsSymetric = TopLeft == TopRight && BottomLeft == BottomRight && TopLeft == BottomLeft;
        }

        public static CornerRadius Parse(string radius)
        {
            if (string.IsNullOrWhiteSpace(radius)) return default(CornerRadius);

            var v = radius.Split(',');

            if (v.Length == 1)
            {
                return new CornerRadius(float.Parse(v[0]));
            }
            return new CornerRadius(float.Parse(v[0]), float.Parse(v[1]), float.Parse(v[2]), float.Parse(v[3]));
        }

        public float TopLeft { get; set; }
        public float TopRight { get; set; }
        public float BottomLeft { get; set; }
        public float BottomRight { get; set; }
        public bool IsEmpty => IsSymetric && TopLeft == 0f;
        public bool IsSymetric { get; }
    }
}