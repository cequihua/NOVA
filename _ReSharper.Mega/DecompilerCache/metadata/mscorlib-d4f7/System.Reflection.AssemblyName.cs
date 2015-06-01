// Type: System.Reflection.AssemblyName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof (_AssemblyName))]
    [Serializable]
    public sealed class AssemblyName : _AssemblyName, ICloneable, ISerializable, IDeserializationCallback
    {
        public AssemblyName();

        [SecuritySafeCritical]
        public AssemblyName(string assemblyName);

        public string Name { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public Version Version { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public CultureInfo CultureInfo { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public string CodeBase { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public string EscapedCodeBase { [SecuritySafeCritical]
        get; }

        public ProcessorArchitecture ProcessorArchitecture { get; set; }
        public AssemblyNameFlags Flags { get; set; }

        public AssemblyHashAlgorithm HashAlgorithm { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public AssemblyVersionCompatibility VersionCompatibility { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public StrongNameKeyPair KeyPair { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public string FullName { [SecuritySafeCritical,
                                  TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        #region _AssemblyName Members

        void _AssemblyName.GetTypeInfoCount(out uint pcTInfo);
        void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);
        void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

        void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams,
                                  IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

        #endregion

        #region ICloneable Members

        public object Clone();

        #endregion

        #region IDeserializationCallback Members

        public void OnDeserialization(object sender);

        #endregion

        #region ISerializable Members

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context);

        #endregion

        [SecuritySafeCritical]
        public static AssemblyName GetAssemblyName(string assemblyFile);

        public byte[] GetPublicKey();
        public void SetPublicKey(byte[] publicKey);

        [SecuritySafeCritical]
        public byte[] GetPublicKeyToken();

        public void SetPublicKeyToken(byte[] publicKeyToken);
        public override string ToString();

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition);
    }
}
