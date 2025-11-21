using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessingLab4
{
    public static class Algoritmic
    {
        public static BitmapSource ConvertRGBToGray(BitmapSource source)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(source);

            int width = writeableBitmap.PixelWidth;
            int height = writeableBitmap.PixelHeight;
            int stride = width * 4; // 4 байта на пиксель (BGRA)

            byte[] pixels = new byte[height * stride]; // Массив пикселей   
            writeableBitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte blue = pixels[i];
                byte green = pixels[i + 1];
                byte red = pixels[i + 2];

                byte gray = (byte)(0.3 * red + 0.59 * green + 0.11 * blue); // формула в оттенок серого 

                pixels[i] = gray;
                pixels[i + 1] = gray;
                pixels[i + 2] = gray;
            }

            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            return writeableBitmap;
        }
        public static BitmapSource contrastingImage(BitmapSource source)
        {
            int stride = (source.PixelWidth * source.Format.BitsPerPixel + 7) / 8;
            byte[] pixels = new byte[source.PixelHeight * stride];
            source.CopyPixels(pixels, stride, 0);

            double f_min = 255, f_max = 0;

            for(int i = 0; i < pixels.Length; i += 4)
            {
                byte brightness = pixels[i]; // Берём любой канал

                if (brightness < f_min) f_min = brightness;
                if (brightness > f_max) f_max = brightness;
            }

            if (f_min == f_max) return source; //проверка на однородность изображения

            double g_min = 0, g_max = 255;

            double a = (g_max - g_min) / (f_max - f_min);
            double b = (g_min * f_max - g_max * f_min) / (f_max - f_min);

            for(int i = 0; i < pixels.Length; i += 4)
            {
                byte brighness = pixels[i]; // Берём любой канал
                double newbrighness = a * brighness + b; // g = af + b
                byte newValue = (byte)Math.Max(g_min, Math.Min(g_max, newbrighness));

                pixels[i] = newValue;
                pixels[i + 1] = newValue;
                pixels[i + 2] = newValue;
            }

            return BitmapSource.Create(
                source.PixelWidth, source.PixelHeight,
                source.DpiX, source.DpiY,
                source.Format, null, pixels, stride
            );
        }
        //8 вариант увеличение контрастности

    }
}
