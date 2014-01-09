using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Security.Cryptography;

namespace _3DRoom
{
    public class ByteArrayImg
    {
        public int height;
        public int width;
        public Byte[] bytes;
        public Image i;


        public string GetHash()
        {
            Byte[] b = new byte[i.Width * i.Height * 4];
            int c = 0;
            Bitmap bi = (Bitmap)i;
            for (int j = 0;j < i.Width;j++)
                for (int k = 0; k < i.Height; k++)
                {
                    Color cl = bi.GetPixel(j,k);
                    b[c] = cl.A;
                    c++;
                    b[c] = cl.R;
                    c++;
                    b[c] = cl.G;
                    c++;
                    b[c] = cl.B;
                    c++;
                }


            return CalculateMD5Hash(b);
        }

        private string CalculateMD5Hash(byte[] inputBytes)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] hashBytes = md5.ComputeHash(inputBytes);


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}
