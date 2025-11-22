namespace CS_DB_Sample.Domains.Exceptions;
/// <summary>
/// 業務ルール違反の値があることを表す例外クラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
public class DomainException : Exception
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="message">エラーメッセージ</param>
    public DomainException(string message) : base(message) {}
}