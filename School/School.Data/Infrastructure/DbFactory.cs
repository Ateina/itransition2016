using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        SchoolContext dbContext;

        public SchoolContext Init()
        {
            return dbContext ?? (dbContext = new SchoolContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
