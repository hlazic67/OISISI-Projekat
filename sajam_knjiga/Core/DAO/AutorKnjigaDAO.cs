using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Core.Services;

namespace Core.DAO
{
    public class AutorKnjigaDAO
    {
        private List<AutorKnjiga> _veze;
        private Storage<AutorKnjiga> _storage;

        public AutorKnjigaDAO()
        {
            _storage = new Storage<AutorKnjiga>("autor_knjiga.csv");
            _veze = _storage.Load();
        }

        public AutorKnjiga? DodajVezu(int autorId, string isbn)
        {
            if (_veze.Exists(v => v.AutorId == autorId && v.ISBN == isbn)) return null;

            AutorKnjiga veza = new AutorKnjiga(autorId, isbn);
            _veze.Add(veza);
            _storage.Save(_veze);
            return veza;
        }

        public bool UkloniVezu(int autorId, string isbn)
        {
            int obrisan = _veze.RemoveAll(v => v.AutorId == autorId && v.ISBN == isbn);
            if (obrisan == 0) return false;

            _storage.Save(_veze);
            return true;
        }

        public void UkloniVezeAutora(int autorId)
        {
            _veze.RemoveAll(v => v.AutorId == autorId);
            _storage.Save(_veze);
        }

        public void UkloniVezeKnjige(string isbn)
        {
            _veze.RemoveAll(v => v.ISBN == isbn);
            _storage.Save(_veze);
        }

        public List<AutorKnjiga> GetAllVeze()
        {
            return _veze;
        }
    }
}
