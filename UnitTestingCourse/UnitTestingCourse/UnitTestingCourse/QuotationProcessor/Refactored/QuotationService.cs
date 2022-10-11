using System.Collections.Generic;

namespace Business.QuotationProcessor.Refactored
{
    public class QuotationService
    {
        private readonly string _filePath;

        public QuotationService(string filePath)
        {
            _filePath = filePath;
        }

        public void AddQuotation(Record newRecord)
        {
            FilePersister p = new FilePersister(_filePath);
            List<Record> records = p.GetAllRecords();

            FileManager fm = new FileManager();
            FileAction action = fm.AddQuotation(records, newRecord);

            p.ApplyAction(action);
        }
    }
}
