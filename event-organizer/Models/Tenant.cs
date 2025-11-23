using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace event_organizer.Models
{
    [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public enum TenantType
    {
        food_truck,
        booth,
        space_only
    }
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        [JsonPropertyName("tenant_name")]
        public string TenantName { get; set; } = null!;

        [Required]
        [JsonPropertyName("tenant_type")]
        public TenantType TenantType { get; set; }

        [MaxLength(250)]
        [JsonPropertyName("tenant_phone")]
        public string? TenantPhone { get; set; }

        [MaxLength(500)]
        [JsonPropertyName("tenant_address")]
        public string? TenantAdress { get; set; }

        [MaxLength(50)]
        [JsonPropertyName("booth_num")]
        public string? BoothNum { get; set; }

        [JsonPropertyName("area_sm")]
        public double? AreaSm { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
