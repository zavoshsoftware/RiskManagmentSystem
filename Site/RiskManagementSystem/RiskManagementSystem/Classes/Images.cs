using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;

namespace Xpertz.Base.Handler
{
    public class Images : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            var file = context.Request.FilePath.Replace(".ashx", String.Empty);
            var fileName = file.Substring(file.LastIndexOf('/') + 1);
            var extension = file.Substring(file.LastIndexOf('.'));

            int width;
            int height;
            int.TryParse(context.Request["w"], out width);
            int.TryParse(context.Request["h"], out height);

            var path = context.Server.MapPath(file);
            if (!File.Exists(path))
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }
            context.Response.Clear();
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(10));
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetValidUntilExpires(false);
            context.Response.AddHeader("content-disposition", "inline;filename=" + fileName);

            using (var fs = new FileStream(path,
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var img = new Bitmap(fs))
                {
                    using (var ms = new MemoryStream())
                    {
                        Bitmap bmpOut = null;

                        if (width > 0 && height == 0)
                        {
                            double tmp = img.Height / (double)img.Width;
                            bmpOut = GenerateThumb(img, width, (int)(width * tmp));
                        }
                        if (height > 0 && width == 0)
                        {
                            double tmp = img.Width / (double)img.Height;
                            bmpOut = GenerateThumb(img, (int)(height * tmp), height);
                        }
                        if (height > 0 && width > 0)
                        {
                            bmpOut = GenerateThumb(img, width, height);
                        }
                        if (height == 0 && width == 0)
                        {
                            bmpOut = GenerateThumb(img, img.Width, img.Height);
                        }
                        if (GetContentType(extension) == "image/jpeg")
                        {
                            var info = ImageCodecInfo.GetImageEncoders();
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                            if (bmpOut != null) bmpOut.Save(ms, info[1], encoderParameters);
                        }
                        else if (bmpOut != null) bmpOut.Save(ms, GetImageFormat(extension));
                        var arrImg = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(arrImg, 0, (int)ms.Length);
                        img.Dispose();
                        context.Response.ContentType = GetContentType(extension);

                        context.Response.BinaryWrite(arrImg);
                        context.Response.End();
                    }
                }
            }

        }

        private static Bitmap GenerateThumb(Bitmap bmp, int width, int height)
        {
            Bitmap result = null;

            try
            {
                var sourceWidth = bmp.Width;
                var sourceHeight = bmp.Height;
                if (width > sourceWidth) width = sourceWidth;
                if (height > sourceHeight) height = sourceHeight;
                var widthPercent = (width / (float)sourceWidth);
                var heightPercent = (height / (float)sourceHeight);
                var finalPercent = heightPercent < widthPercent ? widthPercent : heightPercent;
                var destWidth = (int)(sourceWidth * finalPercent);
                var destHeight = (int)(sourceHeight * finalPercent);
                result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                result.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                var graphics = Graphics.FromImage(result);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.FillRectangle(Brushes.White, 0, 0, width, height);
                graphics.DrawImage(bmp, new Rectangle(0, 0, destWidth, destHeight),
                    new Rectangle(0, 0, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
                bmp.Dispose();
                graphics.Dispose();
            }
            catch
            { }
            return result;
        }

        private static ImageFormat GetImageFormat(string ext)
        {
            switch (ext.ToLower())
            {
                case ".gif": return ImageFormat.Gif;
                case ".jpg":
                case ".jpeg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                case ".bmp": return ImageFormat.Bmp;
                default: throw new NotSupportedException("Invalid Image Format");
            }
        }
        private static string GetContentType(string ext)
        {
            switch (ext.ToLower())
            {
                case ".gif": return "image/gif";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".bmp": return "image/bmp";
                default: throw new NotSupportedException("Invalid Image Format");
            }
        }
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
