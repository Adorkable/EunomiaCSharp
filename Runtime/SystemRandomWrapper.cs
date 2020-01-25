/// SystemRandomWrapper mostly exists to easily wrap serialization efforts
/// TODO: untested since it was moved out of Unity code
namespace Eunomia
{
    [System.Serializable]
    public class SystemRandomWrapper : System.Runtime.Serialization.ISerializable
    {
        private System.Random random;

        public SystemRandomWrapper(System.Random random = null)
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

        /// Serialization and Deserialization
        private static string SystemRandomKey = "System.Random";

        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            byte[] systemRandomSerialization = SerializeSystemRandom();

            info.AddValue(SystemRandomWrapper.SystemRandomKey, systemRandomSerialization);
        }

        protected SystemRandomWrapper(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            var systemRandomSerialization = (byte[])info.GetValue(SystemRandomWrapper.SystemRandomKey, typeof(byte[]));

            this.DeserializeSystemRandom(systemRandomSerialization);
        }

        private byte[] systemRandomSerialization;
        private byte[] SerializeSystemRandom()
        {
            if (this.random == null)
            {
                return new byte[] { };
            }
            var buffer = new System.IO.MemoryStream();
            System.Runtime.Serialization.IFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(buffer, this.random);
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
            this.random = (System.Random)binaryFormatter.Deserialize(buffer);
        }

        // void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() {
        //     this.systemRandomSerialization = this.SerializeSystemRandom();
        // }

        // void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() {
        //     this.DeserializeSystemRandom(this.systemRandomSerialization);
        // }
    }
}