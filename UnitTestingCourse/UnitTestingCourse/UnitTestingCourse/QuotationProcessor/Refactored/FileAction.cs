namespace Business.QuotationProcessor.Refactored
{
    public struct FileAction
    {
        public Record Record { get; }
        public Action Action { get; }

        public FileAction(Record record, Action action)
        {
            Record = record;
            Action = action;
        }
    }
}