using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// item_categoryテーブルにマッピングされるEntityクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
//[Table("item_category")]
public class ItemCategoryEntity 
{
//    [Key]
//    [Column("id")]
    public int Id { get; set; }         //カテゴリId（主キー）
 //   [Column("name")]
    public string? Name { get; set; }   // カテゴリ名

    /// <summary>
    /// ナビゲーションプロパティ
    /// カテゴリに属する商品一覧
    /// </summary>
    public List<ItemEntity>? Items { get; set; }

    public override string? ToString()
    {
        return $"カテゴリId:{Id},カテゴリ名:{Name}";
    }
}