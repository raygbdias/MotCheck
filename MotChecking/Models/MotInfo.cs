using System.Globalization;
using System.Text.Json.Serialization;

namespace MotChecking.Models
{
    public class MotInfo
    {
        [JsonPropertyName("expiryDate")]
        public string? MotExpiryDate { get; set; }

        [JsonPropertyName("odometerValue")]
        public string? Mileage { get; set; }

        public int? DaysUntilExpiry
        {
            get
            {
                if (!string.IsNullOrEmpty(MotExpiryDate))
                {
                    if (DateTime.TryParseExact(MotExpiryDate, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
                    {
                        TimeSpan timeUntilExpiry = expiryDate.Date - DateTime.Today;
                        return (int)timeUntilExpiry.TotalDays;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
