using Microsoft.EntityFrameworkCore;
using ProjektTNAI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Repository.Concrete
{
    public class BaseRepository
    {
        protected AppDbContext Context;

        public BaseRepository(AppDbContext context)
        {
            //Context = AppDbContext.Create();
            Context = context;
        }
    }
}
