using TFLPortal.Entities;

namespace TFLPortal.Services;
public interface IProjectService{

  

    public  Task<List<Project>> GetProjects();

    public Project GetProject(int id);
    public bool Insert(Project project);
    public  bool Update(Project project);
    public  bool Delete(int id);
}
