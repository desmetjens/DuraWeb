using AutoMapper;
using DuraWeb.Application.Mapping;
using DuraWeb.EF;
using Microsoft.EntityFrameworkCore;

namespace DuraWeb.Tests
{
  public abstract class DuraInMemoryTest
  {
    private readonly DbContextOptions<DuraContext> _options;

    protected DuraInMemoryTest()
    {
      Mapper.Initialize(cfg =>
      {
        cfg.AddProfile<DuraProfile>();
      });

      _options = new DbContextOptionsBuilder<DuraContext>()
       .UseInMemoryDatabase("Add_writes_to_database")
       .Options;
    }

    protected DuraContext CreateContext()
    {
      return new DuraContext(_options);
    }
  }
}
