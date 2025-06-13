using System.Text.Json.Serialization;
using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    string Username,
    string Password,

    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    UserTypes UserType,

    string FirstName,
    string LastName,
    string Email,
    string? Phone);