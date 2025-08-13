namespace Box.Sdk.Gen {
    public class OneOf<T0,T1,T2> {
#if NETSTANDARD2_0 || NET462
        public T0 _val0 { get; }
#else
        public T0? _val0 { get; }
#endif
        
#if NETSTANDARD2_0 || NET462
        public T1 _val1 { get; }
#else
        public T1? _val1 { get; }
#endif
        
#if NETSTANDARD2_0 || NET462
        public T2 _val2 { get; }
#else
        public T2? _val2 { get; }
#endif
        
        public OneOf (T0 value) { _val0 = value; }
        
        public OneOf (T1 value) { _val1 = value; }
        
        public OneOf (T2 value) { _val2 = value; }
        
        public static implicit operator OneOf<T0,T1,T2>(T0 value) => new OneOf<T0,T1,T2>(value);
        
        public static implicit operator OneOf<T0,T1,T2>(T1 value) => new OneOf<T0,T1,T2>(value);
        
        public static implicit operator OneOf<T0,T1,T2>(T2 value) => new OneOf<T0,T1,T2>(value);
    }
}