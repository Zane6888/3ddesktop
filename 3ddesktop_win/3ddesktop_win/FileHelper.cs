using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace _3DRoom
{
    public static class FileHelper
    {
        public const int IDLISTOFFSET = 78;

        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            try
            {
                System.Text.Encoding enc = System.Text.Encoding.UTF8;

                if (new FileInfo(shortcutFilename).Length > 10000)
                    throw new Exception("File to big");
                if (new FileInfo(shortcutFilename).Extension != ".lnk")
                    throw new Exception("File not a .lnk");

                Byte[] link = File.ReadAllBytes(shortcutFilename);
                string f = enc.GetString(link, 0, link.Length);

                //string compare = @"(([A-Z]\u003A)|(%[a-z]+%))(\\[\.a-z0-9\(\)\u0020{}\-_]+)+(\.[a-z0-9\(\)\u0020{}]+)?";
                string compare = @"[A-Za-z]\:\\\\[^\/\?\%\*\:\|\<\>]*\u0000+";

                return Environment.ExpandEnvironmentVariables(Regex.Match(f, compare, RegexOptions.IgnoreCase).Value.TrimEnd(new Char[] {'\u0000' }));
                /*
                string pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
                string filenameOnly = System.IO.Path.GetFileName(shortcutFilename);
                Byte[] link = File.ReadAllBytes(shortcutFilename);
                string target = "";
                

                int offset = IDLISTOFFSET;
                while (true)
                {
                    int size = Convert.ToInt32(link[offset + 1]) * 256 + Convert.ToInt32(link[offset]);
                    if (size == 0)
                        break;

                    target += enc.GetString(link,offset,size);
                    offset += size;
                
                }

                return target;*/

            }
            catch (Exception e)
            {
                throw e;
            }
            return string.Empty;
        }

        public static ByteArrayImg GetFileIcon(string file)
        { 
            string pathOnly = System.IO.Path.GetDirectoryName(file);
            string filenameOnly = System.IO.Path.GetFileName(file);

            ByteArrayImg bimg = new ByteArrayImg();
            Icon ic = Icon.ExtractAssociatedIcon(file);

        	Image i = ic.ToBitmap();
            Byte[] b = null;
			i.Save("x.png");
			
			
 			b = File.ReadAllBytes("x.png");
            bimg.i = i;
            bimg.height = i.Height;
            bimg.width = i.Width;
            bimg.bytes = b;
            i = null;
            System.IO.File.Delete("x.png");

            return bimg;
        }

    }
}
