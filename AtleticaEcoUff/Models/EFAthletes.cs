using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtleticaEcoUff.Models
{
    public class EFAthletes : IAthletesMock
    {

        //db conn
        AthleticsModel db = new AthleticsModel();
        public IQueryable<Athlete> Athlete { get { return db.Athletes; } }
        public IQueryable<Sport> Sports { get { return db.Sports; } }

        public void Delete(Athlete athlete)
        {
            db.Athletes.Remove(athlete);
            db.SaveChanges();
        }

        public Athlete Save(Athlete athlete)
        {
            if (athlete.athlete_id == 0)

            { 
                // insert
                db.Athletes.Add(athlete);

            }

            else
            {
                // update   
                db.Entry(athlete).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();

            return athlete;

        }
    }
    }
