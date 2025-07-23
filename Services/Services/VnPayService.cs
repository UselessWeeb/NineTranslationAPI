using Microsoft.Extensions.Options;
using Services.Interfaces;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Utils;

namespace Services.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly VnPaySettings _settings;

        public VnPayService(IOptions<VnPaySettings> options)
        {
            _settings = options.Value;
        }

        public string CreatePaymentUrl(float totalPrice, int orderId, string description, string clientIp)
        {
            const string version = "2.1.0";
            const string command = "pay";
            const string orderType = "other";
            const string locale = "vn";
            const string currency = "VND";
            const string bankCode = "NCB";

            long amount = (long)(totalPrice * 100); // Convert to VND * 100

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            string createDate = now.ToString("yyyyMMddHHmmss");

            var vnp_Params = new SortedDictionary<string, string>
    {
        { "vnp_Version", version },
        { "vnp_Command", command },
        { "vnp_TmnCode", _settings.TmnCode },
        { "vnp_Amount", amount.ToString() },
        { "vnp_CurrCode", currency },
        { "vnp_BankCode", bankCode },
        { "vnp_TxnRef", orderId.ToString() },
        { "vnp_OrderInfo", description },
        { "vnp_OrderType", orderType },
        { "vnp_Locale", locale },
        { "vnp_ReturnUrl", _settings.VnPayReturnUrl },
        { "vnp_IpAddr", clientIp },
        { "vnp_CreateDate", createDate }
    };

            // URL-encode key=value pairs for hash, same as ASP.NET sample
            var rawHashData = new StringBuilder();
            foreach (var kv in vnp_Params)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    rawHashData.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            // Remove the trailing '&'
            if (rawHashData.Length > 0)
                rawHashData.Length--;

            string secureHash = HmacSHA512(_settings.VnPayHashSecret, rawHashData.ToString());

            // Build final URL with URL-encoded values
            var query = new StringBuilder();
            foreach (var kv in vnp_Params)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    query.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            query.Append("vnp_SecureHash=" + secureHash);

            return _settings.VnPayUrl + "?" + query.ToString();
        }

        public bool VerifyHash(Dictionary<string, string> input)
        {
            if (!input.TryGetValue("vnp_SecureHash", out var receivedHash))
                return false;

            // Clone to avoid modifying original input
            var clonedInput = new Dictionary<string, string>(input);

            // Remove signature fields
            clonedInput.Remove("vnp_SecureHash");
            clonedInput.Remove("vnp_SecureHashType");

            // Sort the remaining keys
            var sorted = new SortedDictionary<string, string>(clonedInput);

            // Rebuild raw data string (URL-encoded)
            var rawData = string.Join("&", sorted
                .Where(p => !string.IsNullOrEmpty(p.Value))
                .Select(p => $"{WebUtility.UrlEncode(p.Key)}={WebUtility.UrlEncode(p.Value)}"));

            string calculatedHash = HmacSHA512(_settings.VnPayHashSecret, rawData);

            return string.Equals(receivedHash, calculatedHash, StringComparison.OrdinalIgnoreCase);
        }

        private string HmacSHA512(string key, string data)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}