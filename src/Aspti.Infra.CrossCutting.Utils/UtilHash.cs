﻿using System;
using System.Security.Cryptography;

namespace Aspti.Infra.CrossCutting.Utils
{
	public static class UtilHash
	{
		public static string HashPassword(string password)
		{
			byte[] salt;
			byte[] buffer2;
			if (password == null) throw new ArgumentNullException("password");
			using (var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
			{
				salt = bytes.Salt;
				buffer2 = bytes.GetBytes(0x20);
			}

			var dst = new byte[0x31];
			Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
			Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
			return Convert.ToBase64String(dst);
		}

		public static bool VerifyHashedPassword(string hashedPassword, string password)
		{
			byte[] buffer4;
			if (hashedPassword == null) return false;
			if (password == null) throw new ArgumentNullException("password");
			var src = Convert.FromBase64String(hashedPassword);
			if (src.Length != 0x31 || src[0] != 0) return false;
			var dst = new byte[0x10];
			Buffer.BlockCopy(src, 1, dst, 0, 0x10);
			var buffer3 = new byte[0x20];
			Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
			using (var bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
			{
				buffer4 = bytes.GetBytes(0x20);
			}

			return ByteArraysEqual(buffer3, buffer4);
		}

		private static bool ByteArraysEqual(byte[] b1, byte[] b2)
		{
			if (b1 == b2) return true;
			if (b1 == null || b2 == null) return false;
			if (b1.Length != b2.Length) return false;
			for (var i = 0; i < b1.Length; i++)
				if (b1[i] != b2[i])
					return false;
			return true;
		}
	}
}
