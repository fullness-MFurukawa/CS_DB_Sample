using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// Data Annotations(データ注釈)によるマッピングするEntityクラス
/// item_stockテーブルにマッピングされる
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
[Table("item_stock")]
public class ItemStockEntity
{
    [Key]
    public int Id         { get; set; } // 商品在庫Id（主キー）
    public int Stock      { get; set; } // 在庫数
    [Column("item_id")]
    public int ItemId     { get; set; } // 商品Id
}