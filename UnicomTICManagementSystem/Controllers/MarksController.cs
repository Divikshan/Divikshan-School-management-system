using System.Collections.Generic;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class MarksController
    {
        private MarksRepository repo = new MarksRepository();

        public List<Marks> GetAllMarks()
        {
            return repo.GetAllMarks();
        }

        public void AddMark(Marks mark)
        {
            repo.AddMark(mark);
        }

        public void UpdateMark(Marks mark)
        {
            repo.UpdateMark(mark);
        }

        public void DeleteMark(int markID)
        {
            repo.DeleteMark(markID);
        }
    }
}
