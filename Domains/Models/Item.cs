using CS_DB_Sample.Domains.Exceptions;
namespace CS_DB_Sample.Domains.Models;
/// <summary>
/// 商品を表すドメインオブジェクト
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
public class Item : IEquatable<Item>
{
     /// <summary>
    /// 商品Id（永続化用の一意識別子）
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 商品名
    /// </summary>
    public string? Name { get; private set; }
    /// <summary>
    /// 単価
    /// </summary>
    public int Price { get; private set; }
    /// <summary>
    /// 商品カテゴリ
    /// </summary>
    public ItemCategory? Category { get; private set; }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id">商品Id</param>
    /// <param name="name">商品名</param>
    /// <param name="price">単価</param>
    /// <param name="category">商品カテゴリ</param>
    /// <exception cref="DomainException">業務ルール違反の値があることを表す例外</exception>
    public Item(int id, string name, int price, ItemCategory? category)
    {
        Id = id;
        ChangeName(name);
        ChangePrice(price);
        ChangeCategory(category);
    }
    /// <summary>
    /// 商品名を変更する
    /// </summary>
    /// <param name="newName">新しい商品名</param>
    /// <exception cref="DomainException">業務ルール違反の値があることを表す例外</exception>
    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException("商品名は必須です。");
        Name = newName;
    }
    /// <summary>
    /// 単価を変更する
    /// </summary>
    /// <param name="newPrice">新しい単価</param>
    /// <exception cref="DomainException">業務ルール違反の値があることを表す例外</exception>
    public void ChangePrice(int newPrice)
    {
        if (newPrice < 0)
            throw new DomainException("価格は0以上でなければなりません。");
        Price = newPrice;
    }
    /// <summary>
    /// 商品カテゴリを変更する
    /// </summary>
    /// <param name="newCategory">新しい商品カテゴリ</param>
    public void ChangeCategory(ItemCategory? newCategory)
    {
        Category = newCategory;
    }
    /// <summary>
    /// 商品Idの等価性検証
    /// </summary>
   /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Item? other)
    {
        if (other is null) return false;
        return Id == other.Id;
    }
    /// <summary>
    /// プロパティの値を文字列に変換する
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"商品Id:{Id}, 商品名:{Name}, 単価:{Price}, 商品カテゴリ:{Category}";
    }    
}