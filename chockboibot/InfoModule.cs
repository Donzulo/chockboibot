using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace chockboibot
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("chock")]
        public async Task SayAsync(string msg)
        {
            if (String.IsNullOrWhiteSpace(msg) || msg.Length > 100) return;

            Bitmap chockboi = GetBitmapFromFileA(@"\img\chockmand.png");

            float imgWidth = chockboi.Width;
            float imgHeight = chockboi.Height;

            string secondText = "Jeg er stadigvæk i shock";

            RectangleF rectFirst = new RectangleF(0f, 0f, imgWidth, 220f);
            RectangleF rectSecond = new RectangleF(0f, imgHeight-200f, imgWidth, 200f);

            FontFamily font = new FontFamily("Impact");

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            using (Graphics g = Graphics.FromImage(chockboi))
            {
                // Instantiates two new GraphicsPaths
                GraphicsPath gPath1 = new GraphicsPath();
                GraphicsPath gPath2 = new GraphicsPath();

                // Adds string to the path with:
                gPath1.AddString(
                    msg,                       // <= User input (string)
                    font,                       // <= Predefined font (FontFamily)
                    (int)FontStyle.Regular,     // <= Font style casted to int
                    g.DpiY * 40 / 72,           // <= Font size em calculation <stolen>
                    rectFirst,                  // <= Predefined "textbox" to draw in
                    sf);                        // <= Predefined string format (StringFormat)

                gPath2.AddString(
                    secondText,                 // <= Predefined string
                    font,
                    (int)FontStyle.Regular,
                    g.DpiY * 40 / 72,
                    rectSecond,
                    sf);

                // Makes new black pen
                Pen bigger = new Pen(Brushes.Black);

                // Sets the size (width) of the pen and sets LineJoin
                // to Bevel to fix artifacts
                bigger.Width = 8.0f;
                bigger.LineJoin = LineJoin.Bevel;

                // Draws first path on picture and fills with white
                g.DrawPath(bigger, gPath1);
                g.FillPath(Brushes.White, gPath1);

                // Draws second path on picture and fills with white
                g.DrawPath(bigger, gPath2);
                g.FillPath(Brushes.White, gPath2);

                using (MemoryStream stream = new MemoryStream())
                {
                    chockboi.Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);

                    await Context.Channel.SendFileAsync(stream, "chockboi.png");
                }
            }
        }

        public Bitmap GetBitmapFromFileA(string imgFromRoot)
        {
            try
            {
                string currentDir = Directory.GetCurrentDirectory();

                Bitmap bm = (Bitmap)Image.FromFile(currentDir + imgFromRoot);
                return bm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}
