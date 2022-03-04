using BasicRep.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRep.Repositories
{
    public class Repository<T> where T : class
    {

        PerDbEntities db = new PerDbEntities();
        public void Sil(T ent)
        {
            set().Remove(ent);
        }
        public void Ekle(T ent)
        {
            set().Add(ent);
        }
        public void Kaydet()
        {
            db.SaveChanges();
        }
        public void Guncelle()
        {
            Kaydet();    
        }
        public T Bul(int id)
        {
            return set().Find(id);
        }
        public List<T> Listele()
        {
            return set().ToList();
        }
        public DbSet<T> set()
        {
            return db.Set<T>();
        }
        
    }
}
