// Decompiled with JetBrains decompiler
// Type: lmaxdatafeed.Crc32
// Assembly: lmaxdatafeed, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DFEBD8BE-A1D3-43D8-B547-6EC80FD649BE
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\lmaxdatafeed.exe

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace lmaxdatafeed
{
  public sealed class Crc32 : HashAlgorithm
  {
    public const uint DefaultPolynomial = 3988292384;
    public const uint DefaultSeed = 4294967295;
    private static uint[] a;
    private readonly uint b;
    private readonly uint[] c;
    private uint d;

    public override int HashSize
    {
      get
      {
        return 32;
      }
    }

    public Crc32()
      : this(3988292384U, uint.MaxValue)
    {
    }

    public Crc32(uint polynomial, uint seed)
    {
      this.c = Crc32.b_m_c(polynomial);
      this.d = seed;
      this.b = seed;
    }

    public override void Initialize()
    {
      this.d = this.b;
    }

    protected override void HashCore(byte[] buffer, int start, int length)
    {
      this.d = Crc32.a22(this.c, this.d, (IList<byte>) buffer, start, length);
    }

    protected override byte[] HashFinal()
    {
      byte[] numArray = Crc32.a22(~this.d);
      this.HashValue = numArray;
      return numArray;
    }

    public static uint Compute(byte[] buffer)
    {
      return Crc32.Compute(uint.MaxValue, buffer);
    }

    public static uint Compute(uint seed, byte[] buffer)
    {
      return Crc32.Compute(3988292384U, seed, buffer);
    }

    public static uint Compute(uint polynomial, uint seed, byte[] buffer)
    {
      return ~Crc32.a22(Crc32.b_m_c(polynomial), seed, (IList<byte>) buffer, 0, buffer.Length);
    }

    private static uint[] b_m_c(uint A_0)
    {
      if (A_0 == 3988292384U && Crc32.a != null)
        return Crc32.a;
      uint[] numArray = new uint[256];
      for (int index1 = 0; index1 < 256; ++index1)
      {
        uint num = (uint) index1;
        for (int index2 = 0; index2 < 8; ++index2)
        {
          if (((int) num & 1) == 1)
            num = num >> 1 ^ A_0;
          else
            num >>= 1;
        }
        numArray[index1] = num;
      }
      if (A_0 == 3988292384U)
        Crc32.a = numArray;
      return numArray;
    }

    private static uint a22(uint[] A_0, uint A_1, IList<byte> A_2, int A_3, int A_4)
    {
      uint num = A_1;
      for (int index = A_3; index < A_4 - A_3; ++index)
        num = num >> 8 ^ A_0[(int) (uint) (UIntPtr) ((uint) A_2[index] ^ num & (uint) byte.MaxValue)];
      return num;
    }

    private static byte[] a22(uint A_0)
    {
      byte[] bytes = BitConverter.GetBytes(A_0);
      if (BitConverter.IsLittleEndian)
        Array.Reverse((Array) bytes);
      return bytes;
    }
  }
}
