using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HealthCare4.Pages.Patients
{
    public class CreateModel : PageModel
    {
        public PatientInfo patientInfo=new PatientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() {
            patientInfo.name = Request.Form["name"];
			patientInfo.email = Request.Form["email"];
			patientInfo.phone = Request.Form["phone"];
			patientInfo.address = Request.Form["address"];

            if(patientInfo.name.Length ==0||patientInfo.email.Length==0|| patientInfo.phone.Length==0|| patientInfo.address.Length == 0)
            {
                errorMessage = "All fiels are required";
                return;
            }

            //Save data

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=HealthCare4;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO Patients(name,email,phone,address) VALUES(@name,@email,@phone,@address)";
                    using(SqlCommand cmd=new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@name", patientInfo.name);
						cmd.Parameters.AddWithValue("@email", patientInfo.email);
						cmd.Parameters.AddWithValue("@phone", patientInfo.phone);
						cmd.Parameters.AddWithValue("@address", patientInfo.address);

                        cmd.ExecuteNonQuery();

					}

                }

            }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            //Clear the fields

            patientInfo.name = "";
			patientInfo.name = "";
			patientInfo.name = "";
			patientInfo.name = "";

            //success message
            successMessage = "Paient saved successfully";
            //Redirect
            Response.Redirect("Patients/Index");
		}
    }
}
