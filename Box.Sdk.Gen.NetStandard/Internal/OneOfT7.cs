namespace Box.Sdk.Gen {
    public class OneOf<T0,T1,T2,T3,T4,T5,T6> {
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
        
#if NETSTANDARD2_0 || NET462
        public T3 _val3 { get; }
#else
        public T3? _val3 { get; }
#endif
        
#if NETSTANDARD2_0 || NET462
        public T4 _val4 { get; }
#else
        public T4? _val4 { get; }
#endif
        
#if NETSTANDARD2_0 || NET462
        public T5 _val5 { get; }
#else
        public T5? _val5 { get; }
#endif
        
#if NETSTANDARD2_0 || NET462
        public T6 _val6 { get; }
#else
        public T6? _val6 { get; }
#endif
        
        public OneOf (T0 value) { _val0 = value; }
        
        public OneOf (T1 value) { _val1 = value; }
        
        public OneOf (T2 value) { _val2 = value; }
        
        public OneOf (T3 value) { _val3 = value; }
        
        public OneOf (T4 value) { _val4 = value; }
        
        public OneOf (T5 value) { _val5 = value; }
        
        public OneOf (T6 value) { _val6 = value; }
        
        public static implicit operator OneOf<T0,T1,T2,T3,T4,T5,T6>(T0 value) => new OneOf<T0,T1,T2,T3,T4,T5,T6>(value);
        
        public static implicit operator OneOf<T0,T1,T2,T3,T4,T5,T6>(T1 value) => new OneOf<T0,T1,T2,T3,T4,T5,T6>(value);
        
        public static implicit operator OneOf<T0,T1,T2,T3,T4,T5,T6>(T2 value) => new OneOf<T0,T1,T2,T3,T4,T5,T6>(value);
        
        public static implicit operator OneOf<T0,T1,T2,T3,T4,T5,T6>(T3 value) => new OneOf<T0,T1,T2,T3,T4,T5,T6>(value);
        
        public static implicit operator OneOf<T0,T1,T2,T3,T4,T5,T6>(T4 value) => new OneOf<T0,T1,T2,T3,T4,T5,T6>(value);
        
        public static implicit operator OneOf<T0,T1,T2,T3,T4,T5,T6>(T5 value) => new OneOf<T0,T1,T2,T3,T4,T5,T6>(value);
        
        public static implicit operator OneOf<T0,T1,T2,T3,T4,T5,T6>(T6 value) => new OneOf<T0,T1,T2,T3,T4,T5,T6>(value);
    }
}