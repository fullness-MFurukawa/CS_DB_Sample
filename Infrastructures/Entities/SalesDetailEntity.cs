namespace CS_DB_Sample.Infrastructures.Entities;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.ComponentModel.DataAnnotations;
/// <summary>
/// Data Annotations(データ注釈)でマッピングするEntityクラス
/// sales_detailテーブルにマッピングされる
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
//[Table("sales_detail")]
public class SalesDetailEntity
{
    //[Key]
    //[Column("id")]
    public int Id { get; set; }        // 売上明細Id（主キー）
    //[Column("sales_id")]
    public int SalesId { get; set; }   // 売上Id (主キー)
    //[Column("quantity")]
    public int Quantity { get; set; }  // 数量
    //[Column("subtotal")]
    public int Subtotal { get; set; }  // 小計
    //[Column("item_id")]
    public int ItemId { get; set; }    // 商品Id

    /// <summary>
    /// ナビゲーションプロパティ
    /// 売上明細に紐づく商品
    /// </summary>
    //[ForeignKey("ItemId")]
    public ItemEntity? Item { get; set; }     // 商品

    public override string? ToString()
    {
        return $"売上明細Id:{Id},売上Id:{SalesId},数量:{Quantity},小計:{Subtotal}";
    }
}