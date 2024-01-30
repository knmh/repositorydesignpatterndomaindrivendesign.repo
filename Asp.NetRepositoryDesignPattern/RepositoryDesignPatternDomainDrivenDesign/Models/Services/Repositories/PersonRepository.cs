using Microsoft.EntityFrameworkCore;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;

namespace RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories
{
    public class PersonRepository<T, U> : IPersonRepository<Person, Guid?> where T : class
    {
        #region [Private States]
        private readonly OnlineShopDbContext _context;

        #endregion

        #region [Ctor]
        public PersonRepository(OnlineShopDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [SelectByIdAsync(Guid? id)]
        public async Task<Person> SelectByIdAsync(Guid? id)
        {
            using (var context = _context)
            {
                try
                {
                    var existingPerson = await context.Person.FindAsync(id);
                    return existingPerson;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await context.DisposeAsync();
                }
            }
        }
        #endregion

        #region [GetPersonByIdAsync(Guid? id)]
        public async Task<IEnumerable<Person>> GetPersonByIdAsync()
        {
            using (var context = _context)
            {
                try
                {
                    var existingPersons = await context.Person.ToListAsync();
                    return existingPersons;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await context.DisposeAsync();
                }
            }
        }
        #endregion

        #region [SelectAllAsync()]
        public async Task<IEnumerable<Person>> SelectAllAsync()
        {
            using (_context)
            {
                try
                {

                    var people = await _context.Person.ToListAsync();
                    return people;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (_context.Person != null) _context.Dispose();
                }

            }



        }

        #endregion

        //#region [DeleteAsync(Guid? id)]
        //public async Task DeleteAsync(Guid? id)
        //{
        //    using (_context)
        //    {
        //        try
        //        {
        //            if (id == null || _context.Person == null)
        //            {
        //                throw new ArgumentException("Person not found");
        //            }
        //            var existingPerson = await (from x in _context.Person
        //                                        where x.Id == id
        //                                        select x).FirstOrDefaultAsync();

        //            if (existingPerson != null)
        //            {
        //                _context.Entry(existingPerson).State = EntityState.Deleted;
        //                await _context.SaveChangesAsync();
        //            }

        //            //var q = (from x in _context.Person
        //            //         where x.Id == id
        //            //         select x).First();
        //            //_context.Entry(q).State = EntityState.Deleted;
        //            //_context.SaveChanges();
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //        finally
        //        {
        //            if (_context.Person != null) _context.Dispose();
        //        }

        //    }
        //}
        //#endregion

        #region [DeleteAsync(Person person)]
        public async Task<bool> DeleteAsync(Person person)
        {
            using (_context)
            {
                try
                {
                    if (_context.Person == null)
                    {
                        throw new ArgumentException("Entity set 'PersonIdentityDBContext.Person' is null.");
                    }
                    var existingPerson = await _context.Person.FindAsync(person.Id);
                    if (existingPerson != null)
                    {
                        _context.Person.Remove(existingPerson);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    // Ideally, log this exception
                    throw;
                }
            }
        }
        #endregion

        #region [InsertAsync(Person Person)]
        public async Task InsertAsync(Person person)
        {
            using (_context)
            {
                try
                {
                    //person.AbstractId = Guid.NewGuid().ToString();
                    var existingPerson = await _context.Person.AddAsync(person);
                    await _context.SaveChangesAsync();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (_context.Person != null) _context.Dispose();
                }
            }
        }
        #endregion

        #region [UpdateAsync(Person person)]
        public async Task UpdateAsync(Person person)
        {
            using (_context)
            {
                try
                {
                    if (person.Id == null)
                    {
                        throw new ArgumentNullException(nameof(person.Id));
                    }

                    var existingPerson = await _context.Person.FindAsync(person.Id);
                    if (existingPerson == null)
                    {
                        throw new ArgumentException("Person not found", nameof(person.Id));
                    }

                    existingPerson.FirstName = person.FirstName;
                    existingPerson.LastName = person.LastName;

                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion


        //#region [UpdateAsync(Guid? id)]
        //public async Task UpdateAsync(Guid? id)
        //{
        //    using (_context)
        //    {
        //        try
        //        {
        //            if (id == null)
        //            {
        //                throw new ArgumentNullException(nameof(id));
        //            }

        //            var existingPerson = await _context.Person.FindAsync(id);
        //            if (existingPerson == null)
        //            {
        //                throw new ArgumentException("Person not found");
        //            }


        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //        finally
        //        {
        //            if (_context != null)
        //            {
        //                _context.Dispose();
        //            }
        //        }
        //    }

        //}


        //#endregion
    }
}

