using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Common
{
    public class Captcha
    {
        private Random Random = new Random();

        private Color GetRandomColor() {
            return Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
        }

        private Color GetRandomColor(int min , int max)
        {
            return Color.FromArgb(Random.Next(min,max), Random.Next(min, max), Random.Next(min, max));
        }

        //获取验证码文字
        public string GetCaptchaCode(int length) {
            string lowers = "abcdefghijklmnopqrsauvwxyz";
            string uppers = lowers.ToUpper();
            string digits = "0123456789";

            string str = lowers + uppers + digits;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(str[Random.Next(0, str.Length)]);
            }
            return sb.ToString();
        }

        //生产验证码图片
        public Image GetCaptchaImg(string CaptchaCode) {
            Bitmap img = new Bitmap(72, 25);
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.FromArgb(230, 230, 230));

            //干扰线
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(
                    new Pen(GetRandomColor(150, 256)),
                    new Point(Random.Next(0, 70), Random.Next(0, 25)),
                    new Point(Random.Next(0, 70), Random.Next(0, 25))
                    );
            }

            //绘画文字

            for (int i = 0; i < CaptchaCode.Length; i++)
            {
                g.DrawString(
                    CaptchaCode[i].ToString(),
                    new Font("calibri", 14),
                    new SolidBrush(GetRandomColor(0, 100)),
                    new Point(18 * i, 0)
                    );
            }

            //干扰点

            for (int i = 0; i < 100; i++)
            {
                img.SetPixel(Random.Next(0, 72), Random.Next(0,25), GetRandomColor(150, 256));
            }

            return img;
        }
    }
}
