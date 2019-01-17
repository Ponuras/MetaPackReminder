using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Handlers.Groups

{
    public class GetGroupsList
    {
        public List<Group> Execute(Expression<Func<Group, bool>> predicate)
        {
            Data.DataContextDataContext database = new Data.DataContextDataContext();
            return database.Groups.Where(predicate).ToList();

        }
       
    }
}
