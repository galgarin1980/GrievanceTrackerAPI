using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareSiteGrievanceTracker_API.Model
{
    public class GrievanceTrackerRecord
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string StaffName { get; set; }
        public string PatientName { get; set; }
        public DateTime PatientDOB { get; set; }
        public string HIPPASubmitted { get; set; }
        public string MidasSubmitted { get; set; }
        public int PrescriptionNumber { get; set; }
        public string FirstTimeCustomer { get; set; }
        public DateTime DateComplaintReceived { get; set; }
        public DateTime TimeComplaintReceived { get; set; }
        public string PatientIssue { get; set; }
        public string Resolution { get; set; }
        public DateTime ResolutionDate { get; set; }
        public string ResolutionTime { get; set; }
        public string WasPatientSatisfied { get; set; }
        public string PatientFurtherReview { get; set; }
        public string WasCallEscalated { get; set; }
        public string IfYesToWhom { get; set; }
        public string IfYesDateTimeEscalation { get; set; }
        public DateTime EscalatedCallResolutionDate { get; set; }
        public string EscalatedCallResolutionTime { get; set; }
        public string EscalatedCallRecepient { get; set; }
        public string OverallResolutionComplete { get; set; }
        //public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Active { get; set; }
        public string Status { get; set; }
    }
}
