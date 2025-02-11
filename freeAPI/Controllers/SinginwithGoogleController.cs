using freeAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Razor.Tokenizer;
using System.Web.UI;

namespace freeAPI.Controllers
{
    [EnableCorsAttribute(origins: "*", headers: "*", methods: "*")]
    public class SinginwithGoogleController : ApiController
    {
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type"); // Specify allowed headers here
            return response;
        }


        private readonly DBHelper dbHelper = new DBHelper();

        [HttpPost]
        [Route("api/user/add")]
        public IHttpActionResult InsertUser([FromBody] UserProfileRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            using (MySqlConnection conn = dbHelper.GetConnection())
            {
                
                try
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    // Check if EMAIL exists in tbl_profile
                    string checkEmailQuery = "SELECT COUNT(*) FROM tbl_profile WHERE EMAIL = @EMAIL";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkEmailQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@EMAIL", request.EMAIL);
                        int emailCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (emailCount > 0)
                        {
                            transaction.Rollback();
                            return BadRequest("Email already exists!");
                        }
                        else
                        {
                            // Insert into tbl_user
                            string userQuery = @"
                    INSERT INTO tbl_user 
                    (ID_CODE, ID_ORGANIZATION, ID_ROLE, USERID, PASSWORD, FBSOCIALID, GPSOCIALID, STATUS, UPDATEDTIME, EXPIRY_DATE, EMPLOYEEID, 
                    user_department, user_designation, user_function, user_grade, user_status, reporting_manager, is_reporting, ref_id) 
                    VALUES 
                    (@ID_CODE, @ID_ORGANIZATION, @ID_ROLE, @USERID, @PASSWORD, @FBSOCIALID, @GPSOCIALID, @STATUS, @UPDATEDTIME, @EXPIRY_DATE, @EMPLOYEEID, 
                    @user_department, @user_designation, @user_function, @user_grade, @user_status, @reporting_manager, @is_reporting, @ref_id);
                    SELECT LAST_INSERT_ID();"; // Get last inserted ID

                            int newUserId;
                            using (MySqlCommand cmd = new MySqlCommand(userQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ID_CODE", request.ID_CODE);
                                cmd.Parameters.AddWithValue("@ID_ORGANIZATION", 130);
                                cmd.Parameters.AddWithValue("@ID_ROLE", 103);
                                cmd.Parameters.AddWithValue("@USERID", request.USERID);
                                cmd.Parameters.AddWithValue("@PASSWORD", request.PASSWORD);
                                cmd.Parameters.AddWithValue("@FBSOCIALID", request.FBSOCIALID);
                                cmd.Parameters.AddWithValue("@GPSOCIALID", request.GPSOCIALID);
                                cmd.Parameters.AddWithValue("@STATUS", request.STATUS);
                                cmd.Parameters.AddWithValue("@UPDATEDTIME", DateTime.UtcNow);
                                cmd.Parameters.AddWithValue("@EXPIRY_DATE", request.EXPIRY_DATE ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@EMPLOYEEID", request.EMPLOYEEID);
                                cmd.Parameters.AddWithValue("@user_department", request.user_department);
                                cmd.Parameters.AddWithValue("@user_designation", request.user_designation);
                                cmd.Parameters.AddWithValue("@user_function", request.user_function);
                                cmd.Parameters.AddWithValue("@user_grade", request.user_grade);
                                cmd.Parameters.AddWithValue("@user_status", request.user_status);
                                cmd.Parameters.AddWithValue("@reporting_manager", request.reporting_manager);
                                cmd.Parameters.AddWithValue("@is_reporting", request.is_reporting);
                                cmd.Parameters.AddWithValue("@ref_id", request.ref_id);

                                newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // Insert into tbl_profile
                            string profileQuery = @"
                    INSERT INTO tbl_profile 
                    (ID_USER, FIRSTNAME, LASTNAME, AGE, LOCATION, EMAIL, MOBILE, GENDER, DESIGNATION, CITY, OFFICE_ADDRESS, DATE_OF_BIRTH, 
                    DATE_OF_JOINING, REPORTING_MANAGER, PROFILE_IMAGE, COLLEGE, GRADUATIONYEAR, STATE, ResumeFlag, ResumeLocation, id_degree, 
                    id_stream, COUNTRY, STUDENT, NOTSTUDENT, OTHERSTREAM, id_foundation, clg_city, clg_state, clg_country, social_dp_flag) 
                    VALUES 
                    (@ID_USER, @FIRSTNAME, @LASTNAME, @AGE, @LOCATION, @EMAIL, @MOBILE, @GENDER, @DESIGNATION, @CITY, @OFFICE_ADDRESS, @DATE_OF_BIRTH, 
                    @DATE_OF_JOINING, @REPORTING_MANAGER, @PROFILE_IMAGE, @COLLEGE, @GRADUATIONYEAR, @STATE, @ResumeFlag, @ResumeLocation, @id_degree, 
                    @id_stream, @COUNTRY, @STUDENT, @NOTSTUDENT, @OTHERSTREAM, @id_foundation, @clg_city, @clg_state, @clg_country, @social_dp_flag);"
                            ;

                            using (MySqlCommand cmd = new MySqlCommand(profileQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ID_USER", newUserId);
                                cmd.Parameters.AddWithValue("@FIRSTNAME", request.FIRSTNAME);
                                cmd.Parameters.AddWithValue("@LASTNAME", request.LASTNAME);
                                cmd.Parameters.AddWithValue("@AGE", request.AGE);
                                cmd.Parameters.AddWithValue("@LOCATION", request.LOCATION);
                                cmd.Parameters.AddWithValue("@EMAIL", request.EMAIL);
                                cmd.Parameters.AddWithValue("@MOBILE", request.MOBILE);
                                cmd.Parameters.AddWithValue("@GENDER", request.GENDER);
                                cmd.Parameters.AddWithValue("@DESIGNATION", request.DESIGNATION);
                                cmd.Parameters.AddWithValue("@CITY", request.CITY);
                                cmd.Parameters.AddWithValue("@OFFICE_ADDRESS", request.OFFICE_ADDRESS);
                                cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", request.DATE_OF_BIRTH ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@DATE_OF_JOINING", request.DATE_OF_JOINING ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@REPORTING_MANAGER", request.REPORTING_MANAGER);
                                cmd.Parameters.AddWithValue("@PROFILE_IMAGE", request.PROFILE_IMAGE);
                                cmd.Parameters.AddWithValue("@COLLEGE", request.COLLEGE);
                                cmd.Parameters.AddWithValue("@GRADUATIONYEAR", request.GRADUATIONYEAR);
                                cmd.Parameters.AddWithValue("@STATE", request.STATE);
                                cmd.Parameters.AddWithValue("@ResumeFlag", request.ResumeFlag);
                                cmd.Parameters.AddWithValue("@ResumeLocation", request.ResumeLocation);
                                cmd.Parameters.AddWithValue("@id_degree", request.id_degree);
                                cmd.Parameters.AddWithValue("@id_stream", request.id_stream);
                                cmd.Parameters.AddWithValue("@COUNTRY", request.COUNTRY);
                                cmd.Parameters.AddWithValue("@STUDENT", request.STUDENT);
                                cmd.Parameters.AddWithValue("@NOTSTUDENT", request.NOTSTUDENT);
                                cmd.Parameters.AddWithValue("@OTHERSTREAM", request.OTHERSTREAM);
                                cmd.Parameters.AddWithValue("@id_foundation", request.id_foundation);
                                cmd.Parameters.AddWithValue("@clg_city", request.clg_city);
                                cmd.Parameters.AddWithValue("@clg_state", request.clg_state);
                                cmd.Parameters.AddWithValue("@clg_country", request.clg_country);
                                cmd.Parameters.AddWithValue("@social_dp_flag", request.social_dp_flag);

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return Ok("User and Profile Inserted Successfully!");
                        }
                    }


                  
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }



    }
}
