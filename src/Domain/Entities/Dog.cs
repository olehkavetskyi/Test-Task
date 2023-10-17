using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Dog : BaseEntity
{
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; } = null!;
    [JsonPropertyName("color")]
    [Required]
    public string Color { get; set; } = null!;
    [JsonPropertyName("tail_length")]
    [Range(0, int.MaxValue, ErrorMessage = "tail_length cannot be negative")]
    public int TailLength { get; set; }
    [JsonPropertyName("weight")]
    [Range(0, int.MaxValue, ErrorMessage = "weight cannot be negative")]
    public int Weight { get; set; }
}
