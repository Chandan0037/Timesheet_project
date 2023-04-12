using Microsoft.AspNetCore.Mvc;
using TimeSheet.Data;
using TimeSheet.ExceptionFilter;
using TimeSheet.Implementation.Interface;
using TimeSheet.Model;
using static TimeSheet.Implementation.DomainModel.AdminDomainModel;

namespace TimeSheet.Implementation.Repository
{
    public class AdminRepo : IAdmin
    {

        private readonly TimeSheetContext _timesheetContext;
        public AdminRepo(TimeSheetContext timesheetContext)
        {
            _timesheetContext = timesheetContext;
        }

        //Client

        public void AddClient(AddClientModel model) 
        {
                var data = new Client();
                data.Client_Name = model.Client_Name;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;

                _timesheetContext.clients.Add(data);
                _timesheetContext.SaveChanges();              
        }

        public void EditClient(EditClientModel editClientModel)                                    
        {
            var ClientId = _timesheetContext.clients.FirstOrDefault(e => 
            e.Client_Id == editClientModel.Client_Id);
                     
                if (ClientId != null)
                {
                    ClientId.Client_Name = editClientModel.Client_Name;
                    ClientId.Modified_Date = DateTime.UtcNow.Date;
                    _timesheetContext.SaveChanges();
                }           
                else
                {
                throw new ClientIdException();
                }
        }

        public IQueryable<Client> GetByClientId(int id)                                   
        {
            var clients = _timesheetContext.clients.AsQueryable();
            var item = _timesheetContext.clients.FirstOrDefault(d => d.Client_Id == id);
            if (item != null)
            {
                return clients.Where(e => e.Client_Id == id);
            }
            else
            {
                throw new ClientIdException();
            }
        }

        public IQueryable<Client> GetAllClients()
        {
            return _timesheetContext.clients.Where(e => e.Is_Active == true).OrderBy(c => c.Client_Name).AsQueryable();
        }

        //Designation

        public void AddDesignation(PostDesignationModel postDesignationModel)                     
        {
            var table = _timesheetContext.designations.FirstOrDefault(e => e.Designation == postDesignationModel.Designation);
            if (table == null)
            {
                var data = new Designations();
                data.Designation = postDesignationModel.Designation;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;
                _timesheetContext.designations.Add(data);
                _timesheetContext.SaveChanges();
            }
            else
            {
                throw new DesignationNameException();
            }
        }

        public void EditDesignation(EditDesignationModel editDesignationModel)                                 
        {
            var DesignationIdCheck = _timesheetContext.designations.FirstOrDefault
                 (e => (e.Designation_Id == editDesignationModel.Designation_Id));
            var DesignationNameCheck = _timesheetContext.designations.FirstOrDefault
                 (e => (e.Designation == editDesignationModel.Designation));

            if (DesignationNameCheck == null)
            {
                if (DesignationIdCheck != null)
                {
                    DesignationIdCheck.Designation = editDesignationModel.Designation;
                    DesignationIdCheck.Modified_Date = DateTime.UtcNow.Date;
                    _timesheetContext.SaveChanges();  
                }
                else
                {
                    throw new DesignationIdException();
                }  
            }
            else
            {
                throw new DesignationNameException();
            }
        }

        public IQueryable<Designations> GetByDesignationId(int id)                                                 
        {
            var designations = _timesheetContext.designations.AsQueryable();
            var item = _timesheetContext.designations.FirstOrDefault(d => d.Designation_Id == id);
            if (item != null)
            {
                return designations.Where(e => e.Designation_Id == id);
            }
            else
            {
                throw new DesignationIdException();
            }
        }

        public IEnumerable<GetAllDesignationsByEmployeeModel> GetAllDesignationsByEmployee()                                        //GetAllDesignations
        {
            var data =  from a in _timesheetContext.designations
                        join b in _timesheetContext.employees
                        on a.Designation_Id equals b.Designation_Id
                        where (a.Is_Active == true)
                        select new { a, b } into t1
                        group t1 by new { t1.a.Designation, t1.a.Designation_Id, empcount = t1.b.Employee_Id == null ? 0 : 1 }
                         into g
                        orderby g.Key.Designation
                        select new GetAllDesignationsByEmployeeModel
                        {
                            Designation_Id = g.Key.Designation_Id,
                            Designation = g.Key.Designation,
                            No_of_Employees = g.Key.empcount == 0 ? 0 : g.Count()
                        };
            return data.ToList();
        }


        public IQueryable<Designations> GetAllDesignations()
        {
            return _timesheetContext.designations.Where(e => e.Is_Active == true).OrderBy(e => e.Designation).AsQueryable();
        }



        //Employee Type

        public void AddEmployeeType(PostEmployeeTypeModel postEmployeeTypeModel)                             
        {
            var table = _timesheetContext.employeeTypes.FirstOrDefault(e => e.Employee_Type == postEmployeeTypeModel.Employee_Type);

            if (table == null)
            {
                var data = new EmployeeType();
                data.Employee_Type = postEmployeeTypeModel.Employee_Type;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;

                _timesheetContext.employeeTypes.Add(data);
                _timesheetContext.SaveChanges();
            }
            else
            {
                throw new EmployeeTypeNameException();
            }
        }

        public void EditEmployeetype(EditEmployeeTypeModel editEmployeeTypeModel)                                //EditEmployeeType
        {
            var IdCheck = _timesheetContext.employeeTypes.FirstOrDefault
                 (e => (e.Employee_Type_Id == editEmployeeTypeModel.Employee_Type_Id));
            var NameCheck = _timesheetContext.employeeTypes.FirstOrDefault
                 (e => (e.Employee_Type == editEmployeeTypeModel.Employee_Type));

            if (NameCheck == null)
            {
                if (IdCheck != null)
                {
                    IdCheck.Employee_Type = editEmployeeTypeModel.Employee_Type;
                    IdCheck.Modified_Date = DateTime.UtcNow.Date;
                    _timesheetContext.SaveChanges();
                }
                else
                {
                    throw new EmployeeTypeIdException();
                }
            }
            else
            {
                throw new EmployeeTypeNameException();
            }
        }

        public IQueryable<EmployeeType> GetByEmployeeTypeId(int id)
        {
            var employeeType = _timesheetContext.employeeTypes.AsQueryable();
            var item = _timesheetContext.employeeTypes.FirstOrDefault(d => d.Employee_Type_Id == id);
            if (item != null)
            {
                return employeeType.Where(e => e.Employee_Type_Id == id);
            }
            else
            {
                throw new EmployeeTypeIdException();
            }
        }



        public IEnumerable<GetAllEmployeeTypeByEmployeeModel> GetAllEmployeeTypesByEmployee()                                           //GetAllEmployeeType
        {
            var data = (from a in _timesheetContext.employeeTypes
                        join b in _timesheetContext.employees
                        on a.Employee_Type_Id equals b.Employee_Type_Id
                        where (a.Is_Active == true)
                        select new { a, b } into t1
                        group t1 by new { t1.a.Employee_Type, t1.a.Employee_Type_Id }
                         into g
                        orderby g.Key.Employee_Type ascending
                        select new GetAllEmployeeTypeByEmployeeModel
                        {
                            Employee_Type_Id = g.Key.Employee_Type_Id,
                            Employee_Type = g.Key.Employee_Type,
                            No_of_Employees = g.Count()
                        });
            return data.ToList();
        }

        public IQueryable<EmployeeType> GetAllEmplyoeeTypes()
        {
            return _timesheetContext.employeeTypes.Where(e => e.Is_Active == true).OrderBy(e => e.Employee_Type).AsQueryable();
        }



    }
}
