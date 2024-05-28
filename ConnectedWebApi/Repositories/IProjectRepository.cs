using TFLPortal.Entities;

namespace TFLPortal.Repositories.Interfaces;
public interface IProjectRepository{

    public  Task<List<Project>> GetProjects();

    public Project GetProject(int id);
    public bool Insert(Project project);
    public Task <bool> Update(Project project);
    public  bool Delete(int id);
}
