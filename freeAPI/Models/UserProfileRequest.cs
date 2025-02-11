using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace freeAPI.Models
{
    public class UserProfileRequest
    {
        public int? ID_CODE { get; set; }
        public int? ID_ORGANIZATION { get; set; }
        public int? ID_ROLE { get; set; }
        public string USERID { get; set; } = string.Empty;
        public string PASSWORD { get; set; } = string.Empty;
        public string FBSOCIALID { get; set; } = string.Empty;
        public string GPSOCIALID { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public DateTime? EXPIRY_DATE { get; set; }
        public string EMPLOYEEID { get; set; } = string.Empty;
        public string user_department { get; set; } = string.Empty;
        public string user_designation { get; set; } = string.Empty;
        public string user_function { get; set; } = string.Empty;
        public string user_grade { get; set; } = string.Empty;
        public string user_status { get; set; } = string.Empty;
        public int? reporting_manager { get; set; }
        public int? is_reporting { get; set; }
        public string ref_id { get; set; } = string.Empty;

        // tbl_profile Properties
        public string FIRSTNAME { get; set; } = string.Empty;
        public string LASTNAME { get; set; } = string.Empty;
        public int? AGE { get; set; }
        public string LOCATION { get; set; } = string.Empty;
        public string EMAIL { get; set; } = string.Empty;
        public string MOBILE { get; set; } = string.Empty;
        public string GENDER { get; set; } = string.Empty;
        public string DESIGNATION { get; set; } = string.Empty;
        public string CITY { get; set; } = string.Empty;
        public string OFFICE_ADDRESS { get; set; } = string.Empty;
        public DateTime? DATE_OF_BIRTH { get; set; }
        public DateTime? DATE_OF_JOINING { get; set; }
        public string REPORTING_MANAGER { get; set; } = string.Empty;
        public string PROFILE_IMAGE { get; set; } = string.Empty;
        public string COLLEGE { get; set; } = string.Empty;
        public string GRADUATIONYEAR { get; set; } = string.Empty;
        public string STATE { get; set; } = string.Empty;
        public int? ResumeFlag { get; set; }
        public string ResumeLocation { get; set; } = string.Empty;
        public int? id_degree { get; set; }
        public int? id_stream { get; set; }
        public string COUNTRY { get; set; } = string.Empty;
        public int? STUDENT { get; set; }
        public int? NOTSTUDENT { get; set; }
        public string OTHERSTREAM { get; set; } = string.Empty;
        public int? id_foundation { get; set; }
        public string clg_city { get; set; } = string.Empty;
        public string clg_state { get; set; } = string.Empty;
        public string clg_country { get; set; } = string.Empty;
        public int? social_dp_flag { get; set; }

    }
}