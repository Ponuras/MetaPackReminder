using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Users
{
    public class GetUsers
    {
        public List<Data.UserProfile> Execute(Expression<Func<Data.UserProfile, bool>> predicate)
        {
            Data.DataContextDataContext database = new Data.DataContextDataContext();
            return database.UserProfiles.Where(predicate).ToList();

        }
    }
}
