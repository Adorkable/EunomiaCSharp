/// SystemRandomWrapper mostly exists to easily wrap serialization efforts
/// TODO: untested since it was moved out of Unity code
namespace Eunomia
{
    [System.Serializable]
    public class SerializableRandom : System.Runtime.Serialization.ISerializable
    {
        private System.Random random;

        public SerializableRandom(System.Random random = null)
        {
            if (random != null)
            {
                this.random = random;
            }
            else
            {
                this.random = new System.Random();
            }
        }

        public SerializableRandom(int seed)
        {
            random = new System.Random(seed);
        }

        /// Serialization and Deserialization
        private const string SystemRandomKey = "System.Random";

        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            byte[] systemRandomSerialization = SerializeSystemRandom();

            info.AddValue(SystemRandomKey, systemRandomSerialization);
        }

        protected SerializableRandom(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            var systemRandomSerialization = (byte[])info.GetValue(SystemRandomKey, typeof(byte[]));

            DeserializeSystemRandom(systemRandomSerialization);
        }

        private readonly byte[] systemRandomSerialization;
        private byte[] SerializeSystemRandom()
        {
            if (random == null)
            {
                return new byte[] { };
            }
            var buffer = new System.IO.MemoryStream();
            System.Runtime.Serialization.IFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(buffer, random);
            return buffer.ToArray();
        }

        private void DeserializeSystemRandom(byte[] serialization)
        {
            if (serialization.Length == 0)
            {
                return;
            }
            var buffer = new System.IO.MemoryStream(serialization);

            System.Runtime.Serialization.IFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            random = (System.Random)binaryFormatter.Deserialize(buffer);
        }
    }
}