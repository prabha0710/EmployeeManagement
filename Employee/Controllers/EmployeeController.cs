using Employee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        string Connection = "Server=localhost\\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;";

        // GET: EmployeeController
        public ActionResult Index()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            EmployeeModel employeeModel = null;
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = Connection;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "ListEmployee";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlCon;
            sqlCon.Open();  
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                     employeeModel = new EmployeeModel();
                    employeeModel.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                    employeeModel.EmpName = sqlDataReader["EmpName"].ToString();
                    employeeModel.ContactNo = sqlDataReader["ContactNo"].ToString();
                    employeeModel.Address = sqlDataReader["Address"].ToString();
                    employeeModel.City = sqlDataReader["City"].ToString();
                    employeeModel.DepartmentName = sqlDataReader["DepartmentName"].ToString();
                    employeeModel.ProjectName = sqlDataReader["ProjectName"].ToString();
                    employeeModel.JoiningDate = Convert.ToDateTime(sqlDataReader["JoiningDate"]);
                    employeeList.Add(employeeModel);
                }
            }
            sqlCon.Close();
            return View(employeeList);
        }

        // GET: EmployeeController
        public ActionResult Details(int id)
        {
            EmployeeModel employeeModel = GetEmployeeDetails(id);
            return View(employeeModel);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel employeeModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = Connection;
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "EmployeeInsert";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Connection = sqlCon;
                sqlCommand.Parameters.AddWithValue("@EmpName", employeeModel.EmpName);
                sqlCommand.Parameters.AddWithValue("@ContactNo", employeeModel.ContactNo);
                sqlCommand.Parameters.AddWithValue("@Address", employeeModel.Address);
                sqlCommand.Parameters.AddWithValue("@City", employeeModel.City);
                sqlCommand.Parameters.AddWithValue("@DepartmentName", employeeModel.DepartmentName);
                sqlCommand.Parameters.AddWithValue("@ProjectName", employeeModel.ProjectName);
                sqlCommand.Parameters.AddWithValue("@JoiningDate", employeeModel.JoiningDate);
                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/
        public ActionResult Edit(int id)
        {
            EmployeeModel employeeModel = GetEmployeeDetails(id);
            return View(employeeModel);
        }

        // POST: EmployeeController/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeModel employeeModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = Connection;
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "EmployeeUpdate";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Connection = sqlCon;
                sqlCommand.Parameters.AddWithValue("@EmpId", id);
                sqlCommand.Parameters.AddWithValue("@EmpName", employeeModel.EmpName);
                sqlCommand.Parameters.AddWithValue("@ContactNo", employeeModel.ContactNo);
                sqlCommand.Parameters.AddWithValue("@Address", employeeModel.Address);
                sqlCommand.Parameters.AddWithValue("@City", employeeModel.City);
                sqlCommand.Parameters.AddWithValue("@DepartmentName", employeeModel.DepartmentName);
                sqlCommand.Parameters.AddWithValue("@ProjectName", employeeModel.ProjectName);
                sqlCommand.Parameters.AddWithValue("@JoiningDate", employeeModel.JoiningDate);
                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/
        public ActionResult Delete(int id)
        {
            EmployeeModel employeeModel = GetEmployeeDetails(id);
            return View(employeeModel);
        }

        // POST: EmployeeController/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EmployeeModel employeeModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = Connection;
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "EmployeeDelete";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Connection = sqlCon;
                sqlCommand.Parameters.AddWithValue("@EmpId", id);
                
                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private EmployeeModel GetEmployeeDetails(int id)
        {
            EmployeeModel employeeModel = new EmployeeModel();

            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = Connection;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "EmployeeDetail";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlCon;
            sqlCommand.Parameters.AddWithValue("@EmpId", id);
            sqlCon.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    employeeModel.EmpId = Convert.ToInt32(sqlDataReader["EmpId"]);
                    employeeModel.EmpName = sqlDataReader["EmpName"].ToString();
                    employeeModel.ContactNo = sqlDataReader["ContactNo"].ToString();
                    employeeModel.Address = sqlDataReader["Address"].ToString();
                    employeeModel.City = sqlDataReader["City"].ToString();
                    employeeModel.DepartmentName = sqlDataReader["DepartmentName"].ToString();
                    employeeModel.ProjectName = sqlDataReader["ProjectName"].ToString();
                    employeeModel.JoiningDate = Convert.ToDateTime(sqlDataReader["JoiningDate"]);
                }
            }
            
            sqlCon.Close();
            return employeeModel;
        }
    }
}
