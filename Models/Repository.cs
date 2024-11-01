namespace BookStoreMVC.Models
{
    public static class Repository
    {
        private static List<Employee> allEmployees = new List<Employee>();
        public static IEnumerable<Employee> AllEmployees
        {
            get { return allEmployees; }
        }
        public static void Create(Employee employee)
        {
            allEmployees.Add(employee);
        }
    
        public static void Delete(Employee employee)
        {
            allEmployees.Remove(employee);
        }
    }
}