using System.Data;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using TFLPortal.Models;
using TFLPortal.Services;

namespace TFLPortal.Services;
public class ProjectService : IProjectService
{

  public readonly IConfiguration configuration;
  private readonly string _connectionString;
  public ProjectService(IConfiguration _configuration)
  {
    _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("connectionString");

    configuration = _configuration;
  }
  public bool Update(Project project)
  {
    bool status = false;
    MySqlConnection connection = new MySqlConnection();
    connection.ConnectionString = _connectionString;
    try
    {

      string query = "select * from projects";
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataAdapter adapter = new MySqlDataAdapter(command);
      DataSet dataSet = new DataSet();
      MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
      adapter.Fill(dataSet);
      DataTable dataTable = dataSet.Tables[0];

      DataColumn[] keyColumn = new DataColumn[1];
      keyColumn[0] = dataTable.Columns["id"];
      dataTable.PrimaryKey = keyColumn;

      DataRow row = dataTable.Rows.Find(project.Id);
      row["title"] = project.Title;
      row["description"]=project.Description;
      adapter.Update(dataSet);
      status = true;


    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      throw e;
    }
    return status;
  }


  public async Task<List<Project>> GetProjects()
  {
    List<Project> projects = new List<Project>();
    MySqlConnection connection = new MySqlConnection(_connectionString);
    try
    {
      string query = "select * from Projects";
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
      DataSet dataSet = new DataSet();
       await  dataAdapter.FillAsync(dataSet);
      DataTable dataTable = dataSet.Tables[0];
      foreach (DataRow dataRow in dataTable.Rows)
      {
        int id = int.Parse(dataRow["id"].ToString());
        string title = dataRow["title"].ToString();
        string description = dataRow["description"].ToString();
        DateTime startdate = DateTime.Parse(dataRow["startdate"].ToString());
        DateTime enddate = DateTime.Parse(dataRow["enddate"].ToString());
        string status = dataRow["status"].ToString();


        Project project = new Project()
        {
          Id = id,
          Title = title,
          Description = description,
          StartDate = startdate,
          EndDate = enddate,
          Status = status

        };
        projects.Add(project);
      }
    }
    catch (Exception e)
    {

      Console.WriteLine(e.Message);
    }
    return projects;
  }


public Project GetProject(int id){
  Project project = null;
  MySqlConnection connection = new MySqlConnection(_connectionString);
  try{
    string query="select * from projects";
    MySqlCommand command = new MySqlCommand(query,connection);
    MySqlDataAdapter adapter =new MySqlDataAdapter(command);
    DataSet dataSet = new DataSet();
    adapter.Fill(dataSet);
    DataTable dataTable = dataSet.Tables[0];
    DataColumn[] keycolumn = new DataColumn[1];
    keycolumn[0] = dataTable.Columns["id"];
    dataTable.PrimaryKey = keycolumn;
    DataRow dataRow = dataTable.Rows.Find(id);

        // int d = int.Parse(dataRow["id"].ToString());
        string title = dataRow["title"].ToString();
        string description = dataRow["description"].ToString();
        DateTime startdate = DateTime.Parse(dataRow["startdate"].ToString());
        DateTime enddate = DateTime.Parse(dataRow["enddate"].ToString());
        string status = dataRow["status"].ToString();
    

    project = new Project{
      Id=id,
      Title=title,
      Description =description,
      StartDate =startdate,
      EndDate =enddate,
      Status =status
    };

  }
  catch(Exception e){
    throw e;
  }

  return project;
}
  public bool Insert(Project project)
  {

    bool status = true;
    MySqlConnection connection = new MySqlConnection(_connectionString);
    try
    {
      string query = "select * from projects";
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
      DataSet dataSet = new DataSet();
      MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dataAdapter);
      dataAdapter.Fill(dataSet);
      DataTable dataTable = dataSet.Tables[0];

      DataRow row = dataTable.NewRow();
      row["id"] = project.Id;
      row["title"] = project.Title;
      row["description"] = project.Description;
      row["startdate"] = project.StartDate;
      row["enddate"] = project.EndDate;
      row["status"] = project.Status;
      dataTable.Rows.Add(row);
      dataAdapter.Update(dataSet);
      status = true;

    }
    catch (Exception e)
    {
      throw e;
    }

    return status;
  }


  public bool Delete(int id)
  {
    bool status = false;
    MySqlConnection connection = new MySqlConnection(_connectionString);
    try
    {
      string query = "select * from projects";
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
      MySqlCommandBuilder builder = new MySqlCommandBuilder(dataAdapter);
      DataSet dataSet = new DataSet();
      dataAdapter.Fill(dataSet);
      DataTable dataTable =dataSet.Tables[0];
      DataColumn[] keycolumn = new DataColumn[1];
      keycolumn[0] = dataTable.Columns["id"];
      dataTable.PrimaryKey = keycolumn;


      DataRow dataRow = dataTable.Rows.Find(id);
      dataRow.Delete();

      dataAdapter.Update(dataSet);

      status = true;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      throw e;
    }
    return status;
  }
}