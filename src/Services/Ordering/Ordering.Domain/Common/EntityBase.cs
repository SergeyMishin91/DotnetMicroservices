using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Common;

public abstract class EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; protected set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
}
