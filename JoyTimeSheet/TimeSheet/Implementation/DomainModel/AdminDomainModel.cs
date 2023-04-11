using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Implementation.DomainModel
{
    public class AdminDomainModel
    {
        //ClientModel
        public class AddClientModel
        {
            public string Client_Name { get; set; }
        }
        public class EditClientModel
        {
            public int Client_Id { get; set; }
            public string Client_Name { get; set; }
        }

        //DesignationModel

        public class PostDesignationModel
        {   
            public string Designation { get; set; }
        }
        public class EditDesignationModel
        {
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
        }
        public class GetAllDesignationsByEmployeeModel
        {
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
            public int No_of_Employees { get; set; }
        }

        //EmployeeType
        public class PostEmployeeTypeModel
        {
            public string Employee_Type { get; set; }          
        }
        public class EditEmployeeTypeModel
        {
            public int Employee_Type_Id { get; set; }
            public string Employee_Type { get; set; }
        }
        public class GetAllEmployeeTypeByEmployeeModel
        {
            public int Employee_Type_Id { get; set; }
            public string Employee_Type { get; set; }
            public int No_of_Employees { get; set; }
        }
 




      











    }
}
