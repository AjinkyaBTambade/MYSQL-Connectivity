using TFLPortal.Repositories.Interfaces;
using TFLPortal.Services.Interfaces;
using TFLPortal.Entities;

namespace TFLPortal.Services;
public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;
    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
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
    public async Task <bool> Update(Project project)
     {
        return await _repository.Update(project);
    }
    public bool Delete(int id) 
    {
        return _repository.Delete(id);
    }


}
