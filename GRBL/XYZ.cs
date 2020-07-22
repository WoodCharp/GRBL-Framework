namespace GRBL
{
    public struct XYZ
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public XYZ(float x, float y, float z) : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static XYZ operator +(XYZ a, XYZ b)
        {
            return new XYZ(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static XYZ operator -(XYZ a, XYZ b)
        {
            return new XYZ(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static XYZ operator *(XYZ a, XYZ b)
        {
            return new XYZ(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static XYZ operator /(XYZ a, XYZ b)
        {
            return new XYZ(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        /// <summary>
        /// Returns XYZ values
        /// </summary>
        /// <returns>Xvalue Yvalue Zvlaue</returns>
        public override string ToString()
        {
            return string.Format("X{0} Y{1} Z{2}",
                X.ToString().Replace(',', '.'),
                Y.ToString().Replace(',', '.'),
                Z.ToString().Replace(',', '.'));
        }
    }
}
