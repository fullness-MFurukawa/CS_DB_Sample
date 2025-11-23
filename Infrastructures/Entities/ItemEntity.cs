// using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
///  Data Annotations(データ注釈)によるマッピングするEntityクラス
/// itemテーブルにマッピングされる
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
//[Table("item")]
public class ItemEntity
{
    //[Key]
    public int Id         { get; set; } // 商品Id（主キー）
    public string? Name   { get; set; } // 商品名
    public int Price      { get; set; } // 単価

    /// <summary>
    /// カテゴリID（外部キー）
    /// </summary>
   //[Column("category_id")]          
    public int CategoryId { get; set; } // カテゴリId

    /// <summary>
    /// ナビゲーションプロパティ
    /// 商品の属するカテゴリ
    /// 外部キー名をナビゲーションプロパティに付ける
    /// </summary>
    //[ForeignKey("CategoryId")]
    public ItemCategoryEntity? Category { get; set; }

    public override string? ToString()
    {
        return $"商品Id:{Id},商品名:{Name},単価{Price},カテゴリId:{CategoryId}";
    }
}