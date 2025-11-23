namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// salesテーブルにマッピングされるEntityクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version
public class SalesEntity
{
    public int Id { get; set; }             // 売上Id（主キー）
    public DateTime SalesDate { get; set; } // 売上日
    public int Total { get; set; }          // 売上合計
    public int AccountId { get; set; }      // アカウントId
    public override string? ToString()
    {
        return $"売上Id:{Id},売上日:{SalesDate},売上合計:{Total},アカウントId:{AccountId}";
    }
}