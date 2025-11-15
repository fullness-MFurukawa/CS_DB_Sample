using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// Data Annotations(データ注釈)によるマッピングするEntityクラス
/// sales_detailテーブルにマッピングされる
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-15</date>
/// <version>1.0.0</version>
[Table("sales_detail")]
public class SalesDetailEntity
{
    [Key]
    public int Id { get; set; }         // 売上明細Id（主キー）
    [Column("sales_id")]
    public int SalesId { get; set; }    // 売上Id (主キー)
    public int? Quantity { get; set; }  // 数量
    public int? Subtotal { get; set; }  // 小計
    [Column("item_id")]
    public int? ItemId { get; set; }    // 商品Id
}