using System;
using Google.Protobuf;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

public class ProtobufMessageAdaptor : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(IMessage);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }

    public class Adaptor : IMessage, CrossBindingAdaptorType
    {
        private ILRuntime.Runtime.Enviorment.AppDomain AppDomain;
        private ILTypeInstance Instance;

        public ILTypeInstance ILInstance
        {
            get
            {
                return Instance;
            }
        }

        public Adaptor()
        { }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appDomain, ILTypeInstance instance)
        {
            AppDomain = appDomain;
            Instance = instance;
        }

        private static readonly CrossBindingMethodInfo<CodedInputStream> mMergeFromMethod = new CrossBindingMethodInfo<CodedInputStream>("MergeFrom");
        public void MergeFrom(CodedInputStream input)
        {
            mMergeFromMethod.Invoke(Instance, input);
        }

        private static readonly CrossBindingMethodInfo<CodedOutputStream> mWriteToMethod = new CrossBindingMethodInfo<CodedOutputStream>("WriteTo");
        public void WriteTo(CodedOutputStream output)
        {
            mWriteToMethod.Invoke(Instance, output);
        }

        private static readonly CrossBindingFunctionInfo<int> mCalSizeMethod = new CrossBindingFunctionInfo<int>("CalculateSize");
        public int CalculateSize()
        {
            return mCalSizeMethod.Invoke(Instance);
        }

        public override string ToString()
        {
            IMethod method = AppDomain.ObjectType.GetMethod("ToString", 0);
            method = Instance.Type.GetVirtualMethod(method);
            if (method == null || method is ILMethod)
            {
                return Instance.ToString();
            }
            else
            {
                return Instance.Type.ToString();
            }
        }
    }
}
