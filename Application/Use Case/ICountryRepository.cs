using Fas7ny.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
  public interface ICountryRepository
{
    Task<List<Country>> GetAllAsync();
}

}
