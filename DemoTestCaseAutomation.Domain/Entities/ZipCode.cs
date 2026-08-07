namespace DemoTestCaseAutomation.Domain.Entities;

public class ZipCode
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int CityId { get; set; }
    public City City { get; set; } = null!;
}
