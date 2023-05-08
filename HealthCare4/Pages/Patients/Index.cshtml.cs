using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace HealthCare4.Pages.Patients
{
    public class IndexModel : PageModel
    {
		public List<PatientInfo> listPatients=new List<PatientInfo>();
        public void OnGet()
        {
			listPatients.Clear();
			try

			{
				string conString = "Data Source=.;Initial Catalog=HealthCare4;Integrated Security=True";

				using (SqlConnection con = new SqlConnection(conString)) 
				{ 

					con.Open();
					string sqlQuery = "select * from Patients";
					using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
					{
						using(SqlDataReader reader= cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								PatientInfo patientInfo = new PatientInfo();
								patientInfo.id = "" + reader.GetInt32(0);
								patientInfo.name = reader.GetString(1);
								patientInfo.email=reader.GetString(2);
								patientInfo.phone=reader.GetString(3);
								patientInfo.address=reader.GetString(4);
								patientInfo.createdAt = reader.GetDateTime(5).ToString();

								listPatients.Add(patientInfo);
								
							}
						}
					}

				}
			}catch (Exception ex)
			{
				Console.WriteLine("Exception" + ex.Message);
			}
        }
    }
	public class PatientInfo
	{ 
	public string id;
	public string name ;
	public string email;
	public string phone;
	public string address;
	public string createdAt;
    }
}
 