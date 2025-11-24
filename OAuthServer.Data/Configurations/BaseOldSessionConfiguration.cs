using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public abstract class BaseOldSessionConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // BU DEĞERİ SHADOW PROPERTY OLARAK EKLEDİK. AMACIMIZ DOMAIN'I DAHA SADE TUTMAK. ÇÜNKÜ BUNLAR METADATA İÇERİR.
            builder.Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            // BU BASE ENTITY CONFIG İÇERİSİNDE ENTITY SPESİFİK DEĞERLERİ CONFIG ETMEMELİYİZ.
        }
    }
}
