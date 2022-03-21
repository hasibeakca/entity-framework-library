using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book> //Ne tür class oldugunu söylüyor
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // throw new NotImplementedException(); ımplemenete edilmemiş hata fırlatmak
            builder.HasKey(p => p.Id); // Primary key görevi görür
            builder.Property(p => p.Id).ValueGeneratedOnAdd(); //ÖRN 3. VERİYİ ATLADI DİĞERİNİ 4 TEN DEĞİL 3 TEN DEVAM EDER - Identity(1,1)
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();// ISREQUİRED NOT NULL ANLAMINA GELİR
            builder.Property(p => p.Desc).HasMaxLength(200).IsRequired();

            builder.HasOne(p => p.SubCategoryFK).WithMany(p => p.Books).HasForeignKey(p => p.SubCategoryId); // BİRE ÇOK İLİŞKİSİ YAZILDI

        }
    }
}
