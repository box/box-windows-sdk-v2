namespace Box.Sdk.Gen {
    public class OneOf<T0> {
#if NETSTANDARD2_0 || NET462
        public T0 _val0 { get; }
#else
        public T0? _val0 { get; }
#endif
        
        public OneOf (T0 value) { _val0 = value; }
        
        public static implicit operator OneOf<T0>(T0 value) => new OneOf<T0>(value);
    }
}