﻿using Microsoft.EntityFrameworkCore;
using System;
using I1JM39_HFT_2022231.Models;
using System.Net.NetworkInformation;

namespace I1JM39_HFT_2022231.Repository
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public GameDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("GameDb");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity
                .HasKey(game => game.GameId);

                entity
                .Property(game => game.GameId)
                .ValueGeneratedOnAdd();

                entity
                .HasMany(game => game.Characters)
                .WithOne(character => character.Game)
                .HasForeignKey(character => character.GameId)
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .HasOne(game => game.Developer)
                .WithOne(developer => developer.Game)
                .HasForeignKey<Developer>(developer => developer.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Developer>(entity =>
            {
                entity
                .HasKey(developer => developer.DeveloperId);

                entity
                .Property(developer => developer.DeveloperId)
                .ValueGeneratedOnAdd();

                entity
                .HasOne(developer => developer.Game)
                .WithOne(game => game.Developer)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Character>(entity =>
            {
                entity
                .HasKey(character => character.CharacterId);

                entity
                .Property(character => character.CharacterId)
                .ValueGeneratedOnAdd();

                entity
                .HasOne(character => character.Game)
                .WithMany(game => game.Characters)
                .HasForeignKey(character => character.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Game>().HasData(new Game[]
                {
                    new Game("1#Counter Strike:Global Offensive#3500#8,1#2012"),
                    new Game("2#VALORANT#0,00#9,2#2020"),
                    new Game("3#World Of Warcraft#10000#8,1#2004"),
                    new Game("4#League of Legends#0,00#5,0#2009"),
                    new Game("5#Half Life#1500#9,5#1998"),
                    new Game("6#Team Fortress 2#0,00#7,9#2007"),
                    new Game("7#Rocket League#3000#9,7#2015"),
                    new Game("8#Dead by Daylight#5000#6,0#2016"),
                    new Game("9#Grand Theft Auto V#15000#9,0#2013"),
                    new Game("10#Red Dead Redemption#9000#7,8#2010"),
                    new Game("11#Red Dead Redemption 2#9000#9,33#2018"),
                    new Game("12#Call of Duty: Black Ops III#15000#6,77#2015"),
                }); 
            modelBuilder.Entity<Developer>().HasData(new Developer[]
                {
                    new Developer("1#Valve Corporation#1"),
                    new Developer("2#Valve Corporation#5"),
                    new Developer("3#Valve Corporation#6"),

                    new Developer("4#Riot Games Inc.#2"),
                    new Developer("5#Riot Games Inc.#4"),

                    new Developer("6#Blizzard Entertainment#3"),
                    
                    new Developer("7#Psyonix Inc.#7"),
                    
                    new Developer("8#Behaviour Interactive#8"),
                    
                    new Developer("9#Rockstar Games#9"),
                    new Developer("10#Rockstar Games#10"),
                    new Developer("11#Rockstar Games#11"),

                    new Developer("12#Activision Blizzard#12"),


                });
            modelBuilder.Entity<Character>().HasData(new Character[]
                {
                    new Character("1#Counter Terrorist#1#1"),
                    new Character("2#Terrorist#1#1"),

                    new Character("3#Jett#2#2"),
                    new Character("4#Reyna#2#2"),
                    new Character("5#Raze#1#2"),
                    new Character("6#Omen#1#2"),
                    new Character("7#Brimstone#1#2"),
                    new Character("8#Astra#1#2"),
                    new Character("9#Neon#1#2"),
                    new Character("10#Chamber#2#2"),
                    new Character("11#Skye#1#2"),

                    new Character("12#Kil'jaeden#2#3"),
                    new Character("13#Archimonde#2#3"),
                    new Character("14#Deathwing#2#3"),
                    new Character("15#Arthas Menethil, The Lich King#2#3"),
                    new Character("16#Headless Horseman#3#3"),
                    new Character("17#Chub#3#3"),
                    new Character("18#Eiin#3#3"),

                    new Character("19#Ornn#1#4"),
                    new Character("20#Teemo#1#4"),
                    new Character("21#Katarina#1#4"),
                    new Character("22#Jax#1#4"),

                    new Character("23#G-Man#1#5"),
                    new Character("24#Wallace Breen#3#5"),

                    new Character("25#Spy#1#6"),
                    new Character("26#Scout#1#6"),

                    new Character("27#Octane#1#7"),
                    new Character("28#Takumi#1#7"),
                    new Character("29#Fennec#1#7"),

                    new Character("30#Michael Myers#2#8"),
                    new Character("31#Demogorgon#2#8"),
                    new Character("32#Bubba#2#8"),
                    new Character("33#Meg Thomas#1#8"),
                    new Character("34#Jake Park#1#8"),
                    new Character("35#Feng Min#1#8"),

                    new Character("36#Michael#1#9"),
                    new Character("37#Trevor#1#9"),
                    new Character("38#Franklin#1#9"),
                    new Character("39#Jimmy#2#9"),
                    new Character("40#Lester#2#9"),
                    new Character("41#Victor Vance#3#9"),
                    new Character("42#Diego Mendez#3#10"),


                    new Character("43#John Marston#1#10"),
                    new Character("44#Spouse#1#10"),
                    new Character("45#Arthur#2#10"),

                    new Character("46#John Marston#1#11"),
                    new Character("47#Spouse#1#11"),
                    new Character("48#Arthur#2#11"),

                    new Character("49#John Taylor#3#12"),
                    new Character("50#Ghost#1#12"),
                    new Character("51#Dylan Stone#2#12"),
                });
        }
    }
}
