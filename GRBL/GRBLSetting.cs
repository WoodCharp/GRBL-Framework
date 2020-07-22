namespace GRBL
{
    public class GRBLSetting
    {
        public int ID { get; set; } = 0;
        public float Value { get; set; } = 0.0f;

        public GRBLSetting() { }

        public override string ToString()
        {
            return string.Format("${0}={1}", ID, Value);
        }
    }
}
