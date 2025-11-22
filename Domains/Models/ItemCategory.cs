using CS_DB_Sample.Domains.Exceptions;
namespace CS_DB_Sample.Domains.Models;
/// <summary>
/// 商品カテゴリを表すドメインオブジェクト
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
public class ItemCategory : IEquatable<ItemCategory>
{
    /// <summary>
    /// カテゴリId（永続化用の一意識別子）
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// カテゴリ名
    /// </summary>
    public string? Name { get; private set; }
    /// <summary>
    /// カテゴリに属する商品のリスト
    /// </summary>
    public List<Item>? Items { get; private set; }
    /// <summary>
    /// コンストラクタ
    /// nullや空のカテゴリ名は禁止
    /// </summary>
    /// <exception cref="DomainException">業務ルール違反の値があることを表す例外</exception>
    public ItemCategory(int id, string name)
    {
        Id = id;
        ChangeName(name);
    }
    /// <summary>
    /// カテゴリ名の変更
    /// </summary>
    /// <param name="name">カテゴリ名</param>
    /// <exception cref="DomainException"></exception>
    public void ChangeName(string name)
    {
        // カテゴリ名がnullまたは空の場合は例外をスロー
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("カテゴリ名は必須です。");
        Name = name;
    }
    /// <summary>
    /// カテゴリに属する商品のリストの変更
    /// </summary>
    /// <param name="items"></param>
    public void ChangeItems(List<Item>? items)
    {
        Items = items;
    }
    /// <summary>
    /// カテゴリIdの等価性検証
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(ItemCategory? other)
    {
        if (other is null) return false;
        return this.Id == other.Id;
    }
    /// <summary>
    /// プロパティの値を文字列に変換する
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"カテゴリId:{Id}, カテゴリ名:{Name}, カテゴリに属する商品{Items}";
    }
}