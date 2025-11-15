using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
///  Data Annotations(データ注釈)によるマッピングするEntityクラス
/// itemテーブルにマッピングされる
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-15</date>
/// <version>1.0.0</version>
[Table("item")]
public class Item
{
    [Key]
    public int Id         { get; set; } // 商品Id（主キー）
    public string? Name   { get; set; } // 商品名
    public int Price      { get; set; } // 単価
    [Column("category_id")]          
    public int CategoryId { get; set; } // カテゴリId

    public override string? ToString()
    {
        return $"商品Id:{Id},商品名:{Name},単価{Price},カテゴリId:{CategoryId}";
    }
}