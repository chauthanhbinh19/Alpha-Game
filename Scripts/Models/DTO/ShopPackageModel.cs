public class ShopPackageModel
    {
        public string PackageId { get; set; } = string.Empty;
        public string PackageName { get; set; } = string.Empty;
        
        // Loại gói: "COIN", "GEM", "STARTER", "EVENT"
        public string Category { get; set; } = string.Empty; 
        
        // Giá niêm yết bán thực tế (USD)
        public decimal PriceUsd { get; set; } 
        
        // Giá gốc trước khi giảm (gán null nếu không có khuyến mãi)
        public decimal? OriginalPriceUsd { get; set; } 
        
        // % Giảm giá hiển thị badge (VD: 20 -> 20% OFF)
        public int DiscountPercent { get; set; } 

        // Thông tin vật phẩm thưởng
        public string RewardCurrencyId { get; set; } = string.Empty;
        public string RewardCurrencyImage { get; set; } = string.Empty;
        public long RewardAmount { get; set; }

        // Trạng thái gói
        public bool IsActive { get; set; } = true;

        #region Helper Properties (Dùng hiển thị UI hoặc Logic)

        // Kiểm tra xem gói có đang chạy chương trình giảm giá hay không
        public bool IsDiscounted => DiscountPercent > 0 || (OriginalPriceUsd.HasValue && OriginalPriceUsd.Value > PriceUsd);

        // Tự động tính số tiền tiết kiệm được (USD)
        public decimal SavingsAmount => IsDiscounted && OriginalPriceUsd.HasValue && OriginalPriceUsd.Value > PriceUsd 
            ? OriginalPriceUsd.Value - PriceUsd 
            : 0m;

        #endregion
    }