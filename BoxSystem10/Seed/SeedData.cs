using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemData;
using SystemData.Models;

namespace BoxSystem10
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<SystemContext>();
           context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            // if (!context.User.Any())
            // {
          //  var names = new[] { "شارع الزاوية", "الهضبة الخضراء", "ميدان الجزائر","مصراته بريد","بريد سرت" };




            context.Region.AddRange(new Region { Name = "منطقة طرابلس" });
            context.Region.AddRange(new Region { Name = "منطقة الخليج"});
            context.SaveChangesAsync().Wait();


            context.Office.AddRange(new Office { Name = "شارع الزاوية",RegionId = 1 });
            context.Office.AddRange(new Office { Name = "الهضبة الخضراء", RegionId = 1 });

            context.Office.AddRange(new Office { Name = "ميدان الجزائر", RegionId = 1 });

            context.Office.AddRange(new Office { Name = "شارع الزاوية", RegionId = 1 });
            context.Office.AddRange(new Office { Name = "بريد مصراته", RegionId = 2 });
                context.Office.AddRange(new Office { Name = "بريد سرت", RegionId = 2 });
           
       
            string path = Directory.GetCurrentDirectory() + "\\Seed";

            var sqlFiles = Directory.GetFiles(path, "script_1.sql").OrderBy(x => x);

            foreach (var file in sqlFiles)
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file));
            }

            context.SaveChangesAsync().Wait();

           var r = context.Office.ToListAsync().Result;

            if (r != null)
            {
                context.Boxes.AddRange(new Boxes { NumberBox = 1001, State = false, IsUsed = true,  OfficeId = r[0].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1002, State = false, IsUsed = false, OfficeId = r[0].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1011, State = false, IsUsed = false, OfficeId = r[0].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1012, State = false, IsUsed = false, OfficeId = r[0].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1013, State = false, IsUsed = false, OfficeId = r[0].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1014, State = false, IsUsed = false, OfficeId = r[0].Id, TypeBoxId = 1 });

                context.Boxes.AddRange(new Boxes { NumberBox = 1003, State = false, IsUsed = false, OfficeId = r[1].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1004, State = false, IsUsed = false, OfficeId = r[1].Id, TypeBoxId = 2 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1005, State = false, IsUsed = false, OfficeId = r[1].Id, TypeBoxId = 2 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1006, State = false, IsUsed = false, OfficeId = r[2].Id, TypeBoxId = 2 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1007, State = false, IsUsed = false, OfficeId = r[2].Id, TypeBoxId = 3 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1008, State = false, IsUsed = false, OfficeId = r[2].Id, TypeBoxId = 3 });
                context.Boxes.AddRange(new Boxes { NumberBox = 1009, State = false, IsUsed = false, OfficeId = r[2].Id, TypeBoxId = 3 });

                context.Boxes.AddRange(new Boxes { NumberBox = 4001, State = false, IsUsed = false, OfficeId = r[3].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 4002, State = false, IsUsed = false, OfficeId = r[3].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 4011, State = false, IsUsed = false, OfficeId = r[3].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 4012, State = false, IsUsed = false, OfficeId = r[3].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 5013, State = false, IsUsed = false, OfficeId = r[4].Id, TypeBoxId = 1 });
                context.Boxes.AddRange(new Boxes { NumberBox = 5014, State = false, IsUsed = false, OfficeId = r[4].Id, TypeBoxId = 1 });
            }
            context.SaveChangesAsync().Wait();

            var sqlFiles2 = Directory.GetFiles(path, "script_2.sql").OrderBy(x => x);

            foreach (var file in sqlFiles2)
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file));
            }

            context.SaveChangesAsync().Wait();
        }




        public static async Task SeedUsers(UserManager<AppUser> userManager, SystemContext db)
        {

            var officesList = await db.Office.ToListAsync().ConfigureAwait(false);

            try{
                await userManager.CreateAsync(new AppUser
            {
                UserName = "office1",
                Email = "aa@aaa.com",
                IsAdmin = false,
                IsValid = true,
                Password = "123456",
                    OfficeId = officesList[0].Id,
                    Name = "سالم",
                PhoneNumber = "0925541214"
            }, "123456").ConfigureAwait(false);

            await userManager.CreateAsync(new AppUser
            {
                UserName = "office2",
                Email = "aa@aaa.com",
                IsAdmin = false,
                IsValid = true,
                Password = "123456",
                OfficeId = officesList[1].Id,
                Name = "Name Office 2",
                PhoneNumber = "0925541214"
            }, "123456").ConfigureAwait(false);

            await userManager.CreateAsync(new AppUser
            {
                UserName = "office3",
                Email = "aa@aaa.com",
                IsAdmin = false,
                IsValid=true,
                Password = "123456",
                OfficeId = officesList[2].Id,
                Name = "Name Office 3",
                PhoneNumber = "0925541214"
            }, "123456").ConfigureAwait(false);

            await userManager.CreateAsync(new AppUser
            {
                UserName = "esam",
                Email = "aa@aaa.com",
                IsAdmin = true,
                IsValid = true,
                Password = "123456",
                Name = "esam",
                PhoneNumber = "0921234561122"
            }, "123456").ConfigureAwait(false);

            await userManager.CreateAsync(new AppUser
            {
                UserName = "ans",
                Email = "aa@aaa.com",
                IsAdmin = true,
                IsValid = true,
                Password = "123456",
                Name = "ans",
                PhoneNumber = "0921234561122"
            }, "123456").ConfigureAwait(false);

            await userManager.CreateAsync(new AppUser
            {
                UserName = "abdo",
                Email = "aa@aaa.com",
                IsAdmin = true,
                IsValid = true,
                Password = "123456",
                Name = "abdo",
                PhoneNumber = "0921234561122"
            }, "123456").ConfigureAwait(false);

            await userManager.CreateAsync(new AppUser
            {
                UserName = "0927223001",
                Email = "aa@aaa.com",
                IsAdmin = false,
                IsValid = true,
                IsCustomer = true,

                Password = "123456",
                Name = "customer",
                PhoneNumber = "0927223001"
            }, "123456").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
              //  var r = ex;

            }
             var r = db.Users.FirstOrDefault(w => w.PhoneNumber == "0927223001");
         //      var dd = (from o in db.Customer where o.PhonNumber1 == r.PhoneNumber select o).FirstOrDefault();

       //      dd.UserId = r.Id;
         //    db.SaveChanges();
            try { 
              db.Customer.Add(new Customer {
                 Name = "مستخدم جديد",
                 PhonNumber1= r.PhoneNumber,
                 CustomerIdentityId = 1,
                 CustomerTypeId=1,
                 CustomerJobsId=1,
                 UserId = r.Id
             });
             db.SaveChanges();
            }
            catch(Exception ex)
            {
                Exception b = ex;
            }
             /*var dd = (from o in db.Customer where o.PhonNumber1 == r.PhoneNumber select o).FirstOrDefault();
             dd.UserId = r.user*/


        }
    }

}
