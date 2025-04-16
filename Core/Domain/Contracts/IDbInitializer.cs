using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDbInitializer
    {
        // This method is used to initialize the database.
        // It can be used to create the database, apply migrations, or seed data.
        Task InitializeAsync();
    }
}
