
namespace ImageRender.Tests
{
    using System;
    using System.Drawing;

    public static class Compare
    {
        static int CompareColours(Color x, Color y)
        {
            return (int)(Math.Pow((int)x.R - y.R, 2) 
                + Math.Pow((int)x.B - y.B, 2) 
                + Math.Pow((int)x.G - y.G, 2));
        }

        public static bool Equals(Bitmap bmp1, Bitmap bmp2)
        {
            const double TOLERANCIA = 0.0001D;
            double totalErros = 0;
            
            for (int x = 0; x < bmp1.Width; x++)
                for (int y = 0; y < bmp1.Height; y++)
                    totalErros += CompareColours(bmp1.GetPixel(x, y), bmp2.GetPixel(x, y)) / 198608D;

            double erros = totalErros / (bmp1.Width * bmp1.Height);

            if (erros > TOLERANCIA)
                return false;

            return true;
        }
    }
}
