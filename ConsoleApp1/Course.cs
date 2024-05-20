using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTgBot
{
        public class Course
        {
            public uint Id { get; set; }
            private string name;
            public string Name
            {
                get => name;
                set
                {
                    if (value.Length < 4)
                    {
                        throw new ArgumentException("Занадто коротка назва курсу", nameof(value));
                    }
                    name = value;
                }
            }
            private string description;
            public string Description
            {
                get => description;
                set
                {
                    if (value.Length < 4)
                    {
                        throw new ArgumentException("Занадто короткий опис курсу", nameof(value));
                    }
                    description = value;
                }
            }
            public string ModulePath { get; set; }
            public uint AuthorId { get; set; }
            public string AuthorName { get; set; }
            public string AuthorSurname { get; set; }
            public string PicturePath { get; set; }
            private double rating;
            public double Rating
            {
                get => rating;
                set
                {
                    if (value > 10 || value < 0)
                    {
                        throw new ArgumentException("Wrong rating value");
                    }

                    if (rating == 0)
                    {
                        rating = value;
                        RatingsAmount++;
                    }
                    else
                    {
                        rating = ((rating * RatingsAmount) + value) / (RatingsAmount + 1);
                        RatingsAmount++;
                    }
                }
            }
            public int RatingsAmount { get; set; }
            private decimal cost;
            public decimal Cost
            {
                get => cost;
                set
                {
                    if (value < 0)
                    {
                        throw new ArgumentException("Wrong cost value");
                    }
                    else
                    {
                        cost = value;
                    }
                }
            }
            public uint BoughtCourseAmount { get; set; }
            private List<Teg> tegs;

            public List<Teg> Tegs { get => tegs; set => tegs = value; }
            public List<uint> RaitedUsersId { get; set; }

            public Course()
            {
                tegs = new List<Teg>();
                RaitedUsersId = new List<uint>();
                RatingsAmount = 0;
                rating = 0;
                BoughtCourseAmount = 0;
                ModulePath = $"Data\\Courses\\{Id}\\Modules.json";
                PicturePath = $"Data\\Courses\\{Id}\\Icon.bmp";
            }
            //public Course(string name, string description)
            //{
            //    Id = counter++;
            //    Name = name;
            //    Description = description;
            //    tegs = new List<Teg>();
            //    RaitedUsersId = new List<uint>();
            //    RatingsAmount = 0;
            //    rating = 0;
            //    BoughtCourseAmount = 0;
            //    ModulePath = $"Data\\Courses\\{Id}\\Modules.json";
            //    PicturePath = $"Data\\Courses\\{Id}\\Icon.bmp";
            //}
            //public Course(string name, string description, uint author, string authorName, string authorSurname, double rating, int ratingsAmount, decimal cost, List<Teg> tegs)
            //{
            //    Id = counter++;
            //    Name = name;
            //    Description = description;
            //    AuthorId = author;
            //    AuthorName = authorName;
            //    AuthorSurname = authorSurname;
            //    ModulePath = $"Data\\Courses\\{Id}\\Modules.json";
            //    PicturePath = $"Data\\Courses\\{Id}\\Icon.bmp";
            //    Cost = cost;
            //    Tegs = tegs;
            //    this.rating = rating;
            //    RatingsAmount = ratingsAmount;
            //    RaitedUsersId = new List<uint>();
            //}
            public Course(uint id, string name, string description, uint author, string authorName, string authorSurname, double rating, int ratingsAmount, decimal cost, uint boughtCourseAmount, List<Teg> tegs)
            {
                Id = id;
                Name = name;
                Description = description;
                AuthorId = author;
                AuthorName = authorName;
                AuthorSurname = authorSurname;
                ModulePath = $"Data\\Courses\\{Id}\\Modules.json";
                PicturePath = $"Data\\Courses\\{Id}\\Icon.bmp";
                Cost = cost;
                BoughtCourseAmount = boughtCourseAmount;
                this.tegs = tegs;
                Rating = rating;
                RatingsAmount = ratingsAmount;
                RaitedUsersId = new List<uint>();
            }
        
    }

}
