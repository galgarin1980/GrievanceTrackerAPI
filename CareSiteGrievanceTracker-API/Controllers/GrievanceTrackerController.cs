using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareSiteGrievanceTracker_API.Model;
using CareSiteGrievanceTracker_API.Data;
using Dapper;

namespace CareSiteGrievanceTracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        // GET: api/<TempController>
        [HttpGet]
        public ActionResult<List<GrievanceTrackerRecord>> Get()
        {
            return GetDailyRecords();

        }

        [HttpGet("Filter/{username}")]
        //[Route("api/Temp/getbyusername")]
        public ActionResult<GrievanceTrackerRecord> GetByUsername(string username)
        {
            List<GrievanceTrackerRecord> tmpDataObj = GetDailyRecords().Where(x => x.Username == username).ToList();
            if (tmpDataObj == null)
            {
                return NotFound(new { Message = " No Record has not been found." });
            }

            return Ok(tmpDataObj);
        }


        // GET api/<TempController>/5
        [HttpGet("{id}")]
        public ActionResult<GrievanceTrackerRecord> Get(int id)
        {
            // Record By ID
            GrievanceTrackerRecord tmpDataObj = GetDailyRecords().FirstOrDefault(obj => obj.ID == id);
            if (tmpDataObj == null)
            {
                return NotFound(new { Message = " No Record has not been found." });
            }

            return Ok(tmpDataObj);
        }

        // POST api/<TempController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] GrievanceTrackerRecord grievanceTrackerRecord)
        {
            return Ok(PostDailyRecord(grievanceTrackerRecord));
        }

        // PUT api/<TempController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GrievanceTrackerRecord dailyRecord)
        {
            return Ok(UpdateDailyRecord(dailyRecord));
        }

        // DELETE api/<TempController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(DeleteDailyRecord(id));
        }

        //Data Access Layer Below Can be moved later if necessary
        private List<GrievanceTrackerRecord> GetDailyRecords()
        {
            string sql = @"SELECT ID, Username, StaffName, PatientName, PatientDOB, HIPPASubmitted, MidasSubmitted, 
                             PrescriptionNumber, FirstTimeCustomer, DateComplaintReceived, PatientIssue, Active, Status 
                             FROM CareSiteGrievanceTracker 
                             WHERE Active = 1
                             ORDER BY DateComplaintReceived DESC";

            using (DSN.AnalyticsWeb)
            {
                return DSN.AnalyticsWeb.Query<GrievanceTrackerRecord>(sql).ToList();
            }
        }

        private int PostDailyRecord(GrievanceTrackerRecord dailyRecord)
        {
            string sql = @"INSERT INTO CareSiteGrievanceTracker (Username, StaffName, PatientName, PatientDOB, HIPPASubmitted, MidasSubmitted, 
                                PrescriptionNumber, FirstTimeCustomer, DateComplaintReceived, PatientIssue, Active, Status)

                           VALUES (@Username, @StaffName, @PatientName, @PatientDOB, @HIPPASubmitted, @MidasSubmitted, 
                                @PrescriptionNumber, @FirstTimeCustomer, @DateComplaintReceived, @PatientIssue, 1, @Status)";

            int newID;
            using (DSN.AnalyticsWeb)
            {
                newID = DSN.AnalyticsWeb.ExecuteScalar<int>(sql, dailyRecord);
            }

            return newID;
        }

        private Boolean UpdateDailyRecord(GrievanceTrackerRecord dailyRecord)
        {
            string sql = @"UPDATE CareSiteGrievanceTracker SET
                                Username = @Username, 
                                StaffName = @StaffName,
                                PatientName = @PatientName,
                                PatientDOB = @PatientDOB,
                                HIPPASubmitted = @HIPPASubmitted,
                                MidasSubmitted = @MidasSubmitted,
                                PrescriptionNumber = @PrescriptionNumber,
                                FirstTimeCustomer = @FirstTimeCustomer,
                                DateComplaintReceived = @DateComplaintReceived,
                                PatientIssue = @PatientIssue,
                                Status = @Status
                                                                
                        WHERE ID = @ID";
            //ModifiedDate = @ModifiedDate

            int rowsAffected = 0;
            using (DSN.AnalyticsWeb)
            {
                rowsAffected = DSN.AnalyticsWeb.Execute(sql, dailyRecord);
            }

            return rowsAffected > 0;
        }

        private Boolean DeleteDailyRecord(int dailyRecordID)
        {
            string sql = $"UPDATE CareSiteGrievanceTracker SET Active = 0 WHERE ID = {dailyRecordID}";

            int rowsAffected = 0;
            using (DSN.AnalyticsWeb)
            {
                rowsAffected = DSN.AnalyticsWeb.Execute(sql);
            }

            return rowsAffected > 0;
        }
    }
}
