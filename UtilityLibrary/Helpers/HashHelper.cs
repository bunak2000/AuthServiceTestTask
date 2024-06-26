﻿#region Imports
using System.Security.Cryptography;
using System.Text;
#endregion

namespace UtilityLibrary.Helpers
{
    public class HashHelper
    {
        #region Public Methods
        public static string GetSHA256(string rawString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawString));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion
    }
}
