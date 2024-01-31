using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Firm.ImmigrationMatters.Models
{
    public class Matter
    {
            public Address[] addresses { get; set; }
            public Attachment[] attachments { get; set; }
            public int? totalAR { get; set; }
            public int? totalWIP { get; set; }
            public DateTime? lastBillDate { get; set; }
            public DateTime? lastWIPDate { get; set; }
            public bool? billable { get; set; }
            public string clientId { get; set; }
            public DateTime? closedOn { get; set; }
            public string contactId { get; set; }
            public string currencyISOCode { get; set; }
            public string defaultSecurity { get; set; }
            public string department { get; set; }
            public string description { get; set; }
            public string matterId { get; set; }
            public Matteruser[] matterUsers { get; set; }
            public string name { get; set; }
            public Note[] notes { get; set; }
            public string office { get; set; }
            public DateTime openedOn { get; set; }
            public string practiceArea { get; set; }
            public Relationship[] relationships { get; set; }
            public string status { get; set; }
            public Warning[] warnings { get; set; }
            public DateTime? createdOn { get; set; }
            public DateTime? modifiedOn { get; set; }
            public int? id { get; set; }
            public string clearanceSummary { get; set; }
            public string termsOfBusinessMemo { get; set; }
            public string jointClientGroupId { get; set; }
            public string cdsKey { get; set; }
            public DateTime? cdsModifiedOn { get; set; }
            public string _natureOfMatter { get; set; }
            public string _matterUno { get; set; }
            public string _totalHoursBilled5YP { get; set; }
            public string _totalHoursBilled4YP { get; set; }
            public string _totalHoursBilled3YP { get; set; }
            public string _totalHoursBilled2YP { get; set; }
            public string _totalHoursBilledLY { get; set; }
            public string _totalHoursBilledYTD { get; set; }
            public string _totalBillings5YP { get; set; }
            public string _totalBillings4YP { get; set; }
            public string _totalBillings3YP { get; set; }
            public string _totalBillings2YP { get; set; }
            public string _matteraolcode { get; set; }
            public string _matterbilldisc { get; set; }
            public string _matterbillfreq { get; set; }
            public string _matterbillsingleconsolidated { get; set; }
            public string _mattertimetypecode { get; set; }
            public string _totalBillingsLY { get; set; }
            public string _useclientpart { get; set; }
            public string _totalBillingsYTD { get; set; }
            public string _totalHoursBilledLifetime { get; set; }
            public string _totalBillingsLifetime { get; set; }
            public string _matter_uno { get; set; }
            public string _matterbluesheet { get; set; }
            public string _practiceUnit { get; set; }


        public class Address
        {
            public int? id { get; set; }
            public string addressType { get; set; }
            public string city { get; set; }
            public string contactName { get; set; }
            public string country { get; set; }
            public string email { get; set; }
            public string fax { get; set; }
            public string phone { get; set; }
            public string remoteId { get; set; }
            public string state { get; set; }
            public string[] streetAddress { get; set; }
            public string title { get; set; }
            public string webSite { get; set; }
            public string zipCode { get; set; }
        }

        public class Attachment
        {
            public string attachmentType { get; set; }
            public bool? confidential { get; set; }
            public string contentType { get; set; }
            public string content { get; set; }
            public string creatorId { get; set; }
            public string importanceLevel { get; set; }
            public string name { get; set; }
            public string notes { get; set; }
            public int? size { get; set; }
            public DateTime? modifiedOn { get; set; }
            public DateTime? createdOn { get; set; }
            public int? id { get; set; }
            public string modifiedBy { get; set; }
            public string documentsContentId { get; set; }
        }

        public class Matteruser
        {
            public int? allocation { get; set; }
            public string matterUserType { get; set; }
            public string userId { get; set; }
        }

        public class Note
        {
            public string authorId { get; set; }
            public bool? confidential { get; set; }
            public DateTime? createdOn { get; set; }
            public string message { get; set; }
            public DateTime? modifiedOn { get; set; }
        }

        public class Relationship
        {
            public string affiliationType { get; set; }
            public string comment { get; set; }
            public string createdBy { get; set; }
            public DateTime? createdOn { get; set; }
            public string description { get; set; }
            public string modifiedBy { get; set; }
            public DateTime? modifiedOn { get; set; }
            public string relatedEntityId { get; set; }
            public string relatedEntityType { get; set; }
            public string relatedParentEntityId { get; set; }
            public string relationshipStatus { get; set; }
            public string relationshipType { get; set; }
            public string[] relationshipTypes { get; set; }
            public string partyId { get; set; }
            public string partyType { get; set; }
            public int? id { get; set; }
            public string name { get; set; }
        }

        public class Warning
        {
            public string createdById { get; set; }
            public DateTime? createdOn { get; set; }
            public int? id { get; set; }
            public string modifiedById { get; set; }
            public DateTime? modifiedOn { get; set; }
            public string note { get; set; }
            public string warningType { get; set; }
        }

    }

}