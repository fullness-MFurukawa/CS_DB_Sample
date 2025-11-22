namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// 売上数量と小計金額集計結果を表すエンティティクラス  
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
public class ItemSalesSummary
{
    public int? ItemId { get; set; }
    public int TotalQuantity { get; set; }
    public int TotalSubtotal { get; set; }
}