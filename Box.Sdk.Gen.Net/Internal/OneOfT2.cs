namespace Box.Sdk.Gen {
    public class OneOf<T0,T1> {
#if NETSTANDARD2_0 || NET472
        public T0 _val0 { get; }
#else
        public T0? _val0 { get; }
#endif
        
#if NETSTANDARD2_0 || NET472
        public T1 _val1 { get; }
#else
        public T1? _val1 { get; }
#endif
        
        public OneOf (T0 value) { _val0 = value; }
        
        public OneOf (T1 value) { _val1 = value; }
        
        public static implicit operator OneOf<T0,T1>(T0 value) => new OneOf<T0,T1>(value);
        
        public static implicit operator OneOf<T0,T1>(T1 value) => new OneOf<T0,T1>(value);
    }
}