public class TopupRequestDTO
    {
        public string TransactionId { get; set; } = string.Empty;
        public long PlayerId { get; set; }
        public string PackageId { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty; // e.g., "GOOGLE_PLAY", "APP_STORE", "VNPAY"
        public decimal ChargedAmount { get; set; }           // e.g., 25000.00
        public string ChargedCurrency { get; set; } = string.Empty; // e.g., "VND", "USD"
    }