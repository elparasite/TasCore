using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasGenerator.Model
{
    public abstract class Checkable<T>
    {
        public abstract bool Check(T first, T second);
        public bool Check(IEnumerable<T> groupsOfTeams, T second)
        {
            {
                if (groupsOfTeams == null || second == null)
                    return false;
                return groupsOfTeams.All(t => t == null || Check(t, second));

            }
        }
    }
}
