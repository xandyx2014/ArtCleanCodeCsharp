using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Models;

namespace DataAccess {
    public class StudentsXmlProvider : IDataProvider<Student> {
        private readonly XDocument doc;
        private readonly string xmlFilePath;

        public StudentsXmlProvider(string xmlFilePath) {
            this.xmlFilePath = xmlFilePath;
            doc = XDocument.Load(xmlFilePath);
        }

        public Student GetById(int id) {
            return GetElementById(id)?.FromXElement<Student>();
        }

        public void Change(Student newEntity) {
            var entityById = GetElementById(newEntity.Id);
            entityById.Attribute("FirstName").Value = newEntity.FirstName;
            entityById.Attribute("LastName").Value = newEntity.LastName;
            entityById.Attribute("Age").Value = newEntity.Age.ToString();
            entityById.Attribute("EmailAddress").Value = newEntity.EmailAddress;
        }

        public void Add(Student entity) {
            var element = entity.ToXElement<Student>();
            doc.Root.Add(element);
        }

        public void Remove(int id) {
            var entityById = GetElementById(id);
            entityById.Remove();
        }

        public IEnumerable<Student> GetAll() {
            return doc.Descendants("Student").Select(element => element.FromXElement<Student>());
        }

        public void SubmitChanges() {
            doc.Save(xmlFilePath);
        }

        private XElement GetElementById(int id) {
            var result = doc.Descendants("Student")
                .SingleOrDefault(element => (int) element.Attribute("Id") == id);
            return result;
        }
    }
}