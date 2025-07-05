using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Platform.API.Projects.Domain.Model.Aggregates;

public partial class Project : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    ///     Gets or sets the date and time when the entity was created.
    /// </summary>
    /// <remarks>
    ///     This value is typically set automatically when the entity is first persisted.
    ///     It maps to the database column "CreatedAt".
    /// </remarks>
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    
    /// <summary>
    ///     Gets or sets the date and time when the entity was last updated.
    /// </summary>
    /// <remarks>
    ///     This value is typically updated automatically when the entity is modified.
    ///     It maps to the database column "UpdatedAt".
    /// </remarks>
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}