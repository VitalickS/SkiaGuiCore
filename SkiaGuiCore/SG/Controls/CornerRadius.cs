namespace SkiaGuiCore.SG.Controls
{
    public struct CornerRadius
    {
        public CornerRadius(float radius = 0)
        {
            TopLeft = TopRight = BottomLeft = BottomRight = radius;
        }
        public CornerRadius(float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }

        public float TopLeft { get; set; }
        public float TopRight { get; set; }
        public float BottomLeft { get; set; }
        public float BottomRight { get; set; }
        public bool IsEmpty => IsSymetric && TopLeft == 0f;
        public bool IsSymetric => TopLeft == TopRight && BottomLeft == BottomRight && TopLeft == BottomLeft;
    }
}