using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

[HttpPost]
public IHttpActionResult CreateQRCode(QR model)
{
    using (var ms = new MemoryStream())
    {
        QRCodeHelp.GetCRCode(ms, model.StrContent,model.Size);
        var httpContext = HttpContext.Current;
        httpContext.Response.ContentType = "image/Png";
        httpContext.Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
        httpContext.Response.End();
    }
    return Ok();
}

public class QR
{
    public string StrContent { get; set; }
    public int Size { get; set; }
}

public class QRCodeHelp
{
    public static void GetCRCode(MemoryStream ms, string str, int size = 12)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        Bitmap bm = qrCode.GetGraphic(size);
        bm.Save(ms, ImageFormat.Png);
    }

    public static void GetCRCode(MemoryStream ms, string str, string logoUrl, int size = 12)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        logoUrl = HttpContext.Current.Server.MapPath(@logoUrl);
        Bitmap bm;
        try
        {
            bm = qrCode.GetGraphic(size, Color.Black, Color.White, (Bitmap)Bitmap.FromFile(logoUrl));
        }
        catch (Exception ex)
        {
            bm = qrCode.GetGraphic(size);
        }

        bm.Save(ms, ImageFormat.Png);
    }
}
