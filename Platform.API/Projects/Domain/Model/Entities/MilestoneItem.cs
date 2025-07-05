using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Entities;

/// <summary>
/// Abstract class representing a milestone item in a project
/// like a task or meeting.
/// </summary>
/// <remarks>
/// Commmon fields include milestone name, description, start and end dates and
/// the id of the milestone linked to this item.
/// </remarks>
public abstract class MilestoneItem :  IEntityWithCreatedUpdatedDate
{
    /// <summary>
    /// Represents the name of the milestone item.
    /// </summary>
    public MilestoneItemName Name { get; set; }
    
    /// <summary>
    /// Represents the description of the milestone item.
    /// </summary>
    public Description Description { get; set; }
    
    /// <summary>
    /// The date range for the milestone item, including start and end dates.
    /// </summary>
    public DateRange DateRange { get; set; }
    
    /// <summary>
    /// The unique identifier for the milestone item linked to the milestone item.
    /// </summary>
    public MilestoneId MilestoneId { get; set; }
    
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