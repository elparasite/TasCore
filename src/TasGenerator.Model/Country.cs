using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasGenerator.Model
{
    public class Country
    {

        public string Code { get; set; }
        public string Name { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.GetHashCode() == obj.GetHashCode();
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
        public static bool operator ==(Country country1, Country country2)
        {
            if ((object)country1 == null) return (object)country2 == null;
            return country1.Equals(country2);
        }

        public static bool operator !=(Country country1, Country country2)
        {
            return !(country1 == country2);
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
