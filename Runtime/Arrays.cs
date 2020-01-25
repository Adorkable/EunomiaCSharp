namespace Eunomia {
    public class Arrays {
        public static T[] AddToBack<T>(T add, T[] to) {
            var result = new T[to.Length + 1];
            for (int index = 0; index < to.Length; index++) {
                result[index] = to[index];
            }
            result[result.Length - 1] = add;
            return result;
        }
    }
}