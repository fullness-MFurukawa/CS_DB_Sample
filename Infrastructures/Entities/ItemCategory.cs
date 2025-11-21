using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// Data Annotations(データ注釈)にりマッピングするEntityクラス
/// item_categoryテーブルにマッピングされる
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
[Table("item_category")]
public class ItemCategory 
{
    [Key]
    public int Id { get; set; }         //カテゴリId（主キー）
    public string? Name { get; set; }   // カテゴリ名

    /// <summary>
    /// ナビゲーションプロパティ
    /// カテゴリに属する商品一覧
    /// </summary>
    public List<Item>? Items { get; set; }

    public override string? ToString()
    {
        return $"カテゴリId:{Id},カテゴリ名:{Name}";
    }
}