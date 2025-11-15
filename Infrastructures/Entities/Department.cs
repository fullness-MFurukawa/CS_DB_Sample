namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// コンベンション規約による自動マッピングするEntityクラス
/// departmentテーブルにマッピングされる
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-15</date>
/// <version>1.0.0</version>
public class Department
{
    // 主キー
    // id列のマッピングされる
    public int Id       { get; set; }
    // 部署名
    // name列のマッピングされる
    public string? Name { get; set; }
}