namespace TaskAPI.Model
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public List<TaskModel> PrepareListTaskModel()
        {
            List<TaskModel> taskModels = new List<TaskModel>();

            for (int i = 1; i<=3; i++)
            {
                taskModels.Add(new TaskModel()
                {
                    Id = i,
                    Title = $"{i}. Task",
                    Description = $"{i}. Taskın açıklama metinidir.",
                    Status = i
                });
            }

            return taskModels;
        }
    }
}
