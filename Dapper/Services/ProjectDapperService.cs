using TFLPortal.Repositories.Interfaces;
using TFLPortal.Services.Interfaces;
using TFLPortal.Entities;

namespace TFLPortal.Services;
public class ProjectDapperService : IProjectDapperService
{
  private readonly IProjectDapperRepository _repository;
  public ProjectDapperService(IProjectDapperRepository repository)
  {
    _repository = repository;
  }

  public bool Update(Project project)
  {
    return _repository.Update(project);
  }


    public Task<List<Project>> GetProjects()
    {
        return _repository.GetProjects();
    }


  public Project GetProject(int id)
  {
    return _repository.GetProject(id);
  }
  public bool Insert(Project project)
  {
    return _repository.Insert(project);
  }


  public bool Delete(int id)
  {
    return _repository.Delete(id);
  }
}