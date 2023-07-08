using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using WordInEnglishPro.Model;
using Xamarin.Forms;

namespace WordInEnglishPro.Application_Context
{
    public class Application_ContextDB : DbContext
    {
        private const string DatabaseName = "English.db3";

        public DbSet<WordEN> WordsEN { get; set; }
        public DbSet<WordES> WordsES { get; set; }

        public Application_ContextDB()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String databasePath;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", DatabaseName);
                    break;

                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
                    break;

                default:
                    throw new NotImplementedException("Platform not supported");
            }
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}