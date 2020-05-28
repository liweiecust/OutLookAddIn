using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class VstsTestCaseResult
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Outcome { get; set; }

        public string Comment { get; set; }

        public List<string> AssociatedBugs { get; set; }

        public override int GetHashCode()
        {
            var hashCode = -2036573162;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Outcome);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(AssociatedBugs);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as VstsTestCaseResult);
        }

        public bool Equals(VstsTestCaseResult result)
        {
            return result != null &&
                   Id == result.Id &&
                   Description == result.Description &&
                   Outcome == result.Outcome &&
                   Comment == result.Comment &&
                   EqualityComparer<List<string>>.Default.Equals(AssociatedBugs, result.AssociatedBugs);
        }
    }
}
