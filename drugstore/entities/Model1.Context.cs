﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace drugstore.entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class user95_dbEntities11 : DbContext
    {
        public user95_dbEntities11()
            : base("name=user95_dbEntities11")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<kurs_05_02_basket> kurs_05_02_basket { get; set; }
        public virtual DbSet<kurs_05_02_cure> kurs_05_02_cure { get; set; }
        public virtual DbSet<kurs_05_02_cure_category> kurs_05_02_cure_category { get; set; }
        public virtual DbSet<kurs_05_02_manufacturer> kurs_05_02_manufacturer { get; set; }
        public virtual DbSet<kurs_05_02_order> kurs_05_02_order { get; set; }
        public virtual DbSet<kurs_05_02_payment_method> kurs_05_02_payment_method { get; set; }
        public virtual DbSet<kurs_05_02_release_form> kurs_05_02_release_form { get; set; }
        public virtual DbSet<kurs_05_02_user> kurs_05_02_user { get; set; }
        public virtual DbSet<kusr_05_02_role> kusr_05_02_role { get; set; }
    }
}
