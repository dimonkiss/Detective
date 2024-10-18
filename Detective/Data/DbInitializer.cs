using System;
using System.Linq;
using DetectiveAgencyProject.Models;

namespace DetectiveAgencyProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DetectiveAgencyDbContext context)
        {
            // Перевірка, чи є дані у таблиці Agency
            if (context.Agencies.Any())
            {
                return;   // База даних вже заповнена
            }

            // Додавання фейкових агентств
            var agencies = new Agency[]
            {
                new Agency { Name = "Sherlock Detective Agency" },
                new Agency { Name = "Mystery Solvers Inc." },
                new Agency { Name = "Eagle Eye Investigations" }
            };
            context.Agencies.AddRange(agencies);
            context.SaveChanges();

            // Переконатися, що дані агентств збережені перед додаванням детективів
            var agency1Id = agencies[0].AgencyId;
            var agency2Id = agencies[1].AgencyId;
            var agency3Id = agencies[2].AgencyId;

            // Додавання фейкових детективів
            var detectives = new DetectiveAgencyProject.Models.Detective[]
            {
               
                new DetectiveAgencyProject.Models.Detective { Name = "Sherlock Holmes", Experience = "10 years", Specialization = "Homicide", AgencyId = agency1Id },
                new DetectiveAgencyProject.Models.Detective { Name = "Nancy Drew", Experience = "5 years", Specialization = "Missing Persons", AgencyId = agency2Id },
                new DetectiveAgencyProject.Models.Detective { Name = "Hercule Poirot", Experience = "15 years", Specialization = "Fraud", AgencyId = agency3Id }
            };
            context.Detectives.AddRange(detectives);
            context.SaveChanges();

            // Переконатися, що дані детективів збережені перед додаванням справ і клієнтів
            var detective1Id = detectives[0].DetectiveId;
            var detective2Id = detectives[1].DetectiveId;

            // Додавання фейкових клієнтів
            var clients = new Client[]
            {
                new Client { Name = "John Doe", ContactInfo = "johndoe@gmail.com", RequestType = "Murder Investigation", AgencyId = agency1Id },
                new Client { Name = "Jane Smith", ContactInfo = "janesmith@yahoo.com", RequestType = "Fraud Investigation", AgencyId = agency3Id }
            };
            context.Clients.AddRange(clients);
            context.SaveChanges();

            var client1Id = clients[0].ClientId;
            var client2Id = clients[1].ClientId;

            // Додавання фейкових справ
            var cases = new Case[]
            {
                new Case { Description = "Investigate the murder of Mr. X", Status = "In Progress", DetectiveId = detective1Id, ClientId = client1Id },
                new Case { Description = "Find missing person Ms. Y", Status = "Solved", DetectiveId = detective2Id, ClientId = client2Id }
            };
            context.Cases.AddRange(cases);
            context.SaveChanges();

            var case1Id = cases[0].CaseId;
            var case2Id = cases[1].CaseId;

            // Додавання фейкових замовлень
            var orders = new Order[]
            {
                new Order { Description = "Complete murder investigation", Deadline = DateTime.Now.AddDays(30), Progress = "50%", DetectiveId = detective1Id, CaseId = case1Id },
                new Order { Description = "Prepare report on missing person case", Deadline = DateTime.Now.AddDays(15), Progress = "100%", DetectiveId = detective2Id, CaseId = case2Id }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();

            // Додавання фейкових звітів
            var reports = new Report[]
            {
                new Report { CreationDate = DateTime.Now, Details = "Report on murder case", DetectiveId = detective1Id, CaseId = case1Id },
                new Report { CreationDate = DateTime.Now, Details = "Final report on missing person", DetectiveId = detective2Id, CaseId = case2Id }
            };
            context.Reports.AddRange(reports);
            context.SaveChanges();
        }
    }
}
