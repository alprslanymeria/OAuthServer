using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.UnitOfWork
{
    // AMACIMIIZ DATABASE İŞLEMLERİNİ TEK BİR TRANSACTION ÜZERİNDEN GERÇEKLEŞTİRMEK.
    public interface IUnitOfWork
    {
        // TASK ASENKRON İŞLEMLERDE VOID'E KARŞILIK GELİR.
        Task CommitAsync();
        void Commit();
    }
}
