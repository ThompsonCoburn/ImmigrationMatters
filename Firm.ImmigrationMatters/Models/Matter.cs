using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Firm.ImmigrationMatters.Models
{
    public class Matter
    {
        public int Code { get; set; }
        public string clientId { get; set; }
        public string matterId { get; set; }
        public string name { get; set; }
        public string ClientName { get; set; }

        public string practiceArea { get; set; }
        [DisplayName("Nature")]
        public string _natureOfMatter { get; set; }
        public string ModifiedOn { get; set; }
        public List<MatterUsers> MatterUsers { get; set; }
        public string UserId { get; set; }

        public string _matter_uno { get; set; }
        public string MatterTypeCode { get; set; }
        public string OpenDate { get; set; }
        public string PracticeAreaCode { get; set; }
        public string PracticeAreaName { get; set; }
        public string matterBluesheet { get; set; }

        public PracticeArea PracticeArea { get; set; }
        public List<Relationship> Relationships { get; set; }
        public List<AdverseParty> AdverseParties { get; set; }
        public string _matterbluesheet { get; set; }
        public Client MatterClient { get; set; }
        public List<AdverseParty> MatterParties { get; set; }


    }

    public class MatterUsers { }
    public class PracticeArea { }
    public class Relationship { }
    public class AdverseParty { }
    public class Client { }
    



}