using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TechresStandaloneSale.Helpers
{
    /// <summary>
    /// The valid encoding options able to be passed to the QR object
    /// </summary>
    public static class QREncodingOptions
    {
        public static string UTF { get { return "UTF-8"; } }
        public static string JIS { get { return "Shift_JIS"; } }
        public static string ISO { get { return "ISO-859-1"; } }
    }
    /// <summary>
    /// The tolerance level for errors when generating a QR code
    /// </summary>
    public static class QRErrorCorrectionLevel
    {
        public static string Default { get { return "L"; } }
        public static string FifteenPercent { get { return "M"; } }
        public static string TwentyFivePercent { get { return "Q"; } }
        public static string ThirtyPercent { get { return "H"; } }
    }

    public class QRCodeHelper
    {
        #region Private Properties
        private string ServiceUrl = "https://chart.googleapis.com/chart?cht=qr&chl=QRDATA&chs=WIDTHxHEIGHT&choe=ENCODING&chld=ERRORCORRECTIONLEVEL|MARGIN";
        /// <summary>
        /// Height of the generated image in pixels (default 100)
        /// </summary>
        private int _height { get; set; }
        /// <summary>
        /// Width of the generated image in pixels (default 100)
        /// </summary>
        private int _width { get; set; }
        /// <summary>
        /// Specify a QREncodingOptions value, default is QREncodingOptions.UTF
        /// </summary>
        private string _encoding { get; set; }
        /// <summary>
        /// The QRErrorCorrectionLevel value, default is 7%
        /// </summary>
        private string _errorcorrectionlevel { get; set; }
        /// <summary>
        /// The white margin around the QR code, measured in rows not pixels
        /// </summary>
        private int _margin { get; set; }
        #endregion

        #region Public Properties
        /// <summary>
        /// This is the URL or WarehouseSessionData that you'd like to encode within the QR code image
        /// </summary>
        public string _data { get; set; }
        #endregion

        #region Constructors
        public QRCodeHelper(string Data)
        {
            //  Set defaults
            _height = 300;
            _width = 300;
            _data = Data;
            _encoding = QREncodingOptions.UTF;
            _errorcorrectionlevel = QRErrorCorrectionLevel.Default;
            _margin = 4;
        }
        public QRCodeHelper(string Data, int width, int height) : this(Data)
        {
            _data = Data;
            _width = width;
            _height = height;
        }
        public QRCodeHelper(string Data, int width, int height, string encoding) : this(Data)
        {
            _data = Data;
            _width = width;
            _height = height;
            _encoding = encoding;
        }
        public QRCodeHelper(string Data, int width, int height, string encoding, string errorlevel) : this(Data)
        {
            _data = Data;
            _width = width;
            _height = height;
            _encoding = encoding;
            _errorcorrectionlevel = errorlevel;
        }
        public QRCodeHelper(string Data, int width, int height, string encoding, string errorlevel, int margin) : this(Data)
        {
            _data = Data;
            _width = width;
            _height = height;
            _encoding = encoding;
            _errorcorrectionlevel = errorlevel;
            _margin = margin;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Generates a URL which can be passed as an <img src='X' /> parameter for a website or a PictureBox.ImageLocation source for desktop development
        /// </summary>
        /// <returns></returns>
        public string GenerateUrl()
        {
            //  Replaces the service url parameters with the values specified in the constructor
            return ServiceUrl
                .Replace("QRDATA", _data)
                .Replace("HEIGHT", _height.ToString())
                .Replace("WIDTH", _width.ToString())
                .Replace("ENCODING", _encoding.ToString())
                .Replace("ERRORCORRECTIONLEVEL", _errorcorrectionlevel.ToString())
                .Replace("MARGIN", _margin.ToString());
        }
        #endregion
    }
}

