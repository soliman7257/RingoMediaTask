public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public int? ParentDepartmentId { get; set; }
    // Optionally include parent information or omit sub-departments
}
