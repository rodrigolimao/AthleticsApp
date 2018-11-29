using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleticaEcoUff.Models
{
    public interface IAthletesMock
    {
        IQueryable<Athlete> Athlete { get; }
        IQueryable<Sport> Sports { get; }

        Athlete Save(Athlete athlete);

        void Delete(Athlete album);
    }
}
