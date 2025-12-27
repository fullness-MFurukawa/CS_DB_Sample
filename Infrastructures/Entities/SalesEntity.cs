using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// salesテーブルにマッピングされるEntityクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version
public class SalesEntity
{
    //[Key]
    //[Column("id")]
    public int Id { get; set; }             // 売上Id（主キー）
    //[Column("sales_date")]
    public DateTime SalesDate { get; set; } // 売上日
    //[Column("total")]
    public int Total { get; set; }          // 売上合計
    //[Column("account_id")]
    public int AccountId { get; set; }      // アカウントId
    public override string? ToString()
    {
        return $"売上Id:{Id},売上日:{SalesDate},売上合計:{Total},アカウントId:{AccountId}";
    }
}