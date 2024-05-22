using TFLPortal.Entities;

namespace TFLPortal.Repositories.Interfaces;
public interface IProjectDapperRepository{


    public  Task<List<Project>> GetProjects();
    public Project GetProject(int id);
    public bool Insert(Project project);
    public  bool Update(Project project);
    public  bool Delete(int id);
}
