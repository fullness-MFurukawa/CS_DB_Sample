using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS_DB_Sample.Infrastructures.Entities;
/// <summary>
/// departmentテーブルにマッピングされるEntityクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-15</date>
/// <version>1.0.0</version>
[Table("department")]
public class DepartmentEntity
{
    // id列のマッピングされる(主キー)
    [Key]
    public int Id       { get; set; }
    // name列のマッピングされる
    public string? Name { get; set; }

    public override string ToString()
    {
        return $"id = {Id} , name = {Name}";
    }
}